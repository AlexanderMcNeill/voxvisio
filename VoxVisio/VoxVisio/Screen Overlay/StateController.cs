using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using VoxVisio.Singletons;
using VoxVisio.States;

namespace VoxVisio.Screen_Overlay
{
    //Class that keeps track of the current state and manages the changing of the systems state
    class StateController : Overlay
    {
        private const int HOTSPOTSIZE = 100;
        private const int MARGIN = 10;

        private ControlState currentState;

        private StateHotspot[] stateHotspots;
        private StateHotspot selectedStateHotspot;

        public StateController()
        {
            //Creating the hotspots that will be used to change the states
            setupStateHotspots();

            //Setting the current state to command state
            currentState = stateHotspots[(int)eState.Command].GetState();
            selectedStateHotspot = stateHotspots[(int)eState.Command];
            selectedStateHotspot.selected = true;

            //Registering state controller to be drawn to the overlay form
            SharedFormsSingleton.Instance().overlayForm.RegisterOverlay(this);

            //Setting up state controller to listen to the update timer tick
            EventSingleton.Instance().updateTimer.Tick += updateTimer_Tick;
        }

        private void setupStateHotspots()
        {
            stateHotspots = new StateHotspot[Enum.GetNames(typeof(eState)).Length];

            // Getting the rectangles for the hotspots
            int top = Screen.PrimaryScreen.Bounds.Height / 2 - MARGIN / 2 - HOTSPOTSIZE;
            int left = Screen.PrimaryScreen.Bounds.Width - HOTSPOTSIZE;

            Rectangle commandStateHotspot = new Rectangle(left, top - HOTSPOTSIZE, HOTSPOTSIZE, HOTSPOTSIZE * 2);
            Rectangle dictationStateHotspot = new Rectangle(left, top + HOTSPOTSIZE + MARGIN, HOTSPOTSIZE, HOTSPOTSIZE * 2);

            // Creating the hotspots
            StateHotspot commandState = new StateHotspot(new CommandState(), commandStateHotspot, true, Properties.Resources.commandButtonsActive, Properties.Resources.commandButtonInactive);
            commandState.OnSelected += CommandState_OnSelected;
            stateHotspots[(int)eState.Command] = commandState;

            StateHotspot dictationState = new StateHotspot(new DictationState(), dictationStateHotspot, false, Properties.Resources.dictationButtonsActive, Properties.Resources.dictationButtonInactive);
            dictationState.OnSelected += CommandState_OnSelected;
            stateHotspots[(int)eState.Dictation] = dictationState;

            // Setting the current state to be the command state
            currentState = commandState.GetState();
        }

        private void CommandState_OnSelected(StateHotspot sender, ControlState newState)
        {
            // Stopping the current state
            currentState.Stop();

            // Switching to the new state
            currentState = newState;

            // Switching the selected state hotspot to the new state hotspot
            selectedStateHotspot.selected = false;
            selectedStateHotspot = sender;
            selectedStateHotspot.selected = true;

            // Starting the new state
            currentState.Start();
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            //Updating each of the state hotspots
            foreach (StateHotspot sh in stateHotspots)
            {
                sh.Update();
            }
        }

        public void Draw(Graphics g)
        {
            //Drawing eacho of the state hotspots to the overlay form
            foreach (StateHotspot sh in stateHotspots)
            {
                sh.Draw(g);
            }
        }

        public void VoiceInput(string voiceData, string grammarName)
        {
            //Sending the voice input to the current state
            currentState.VoiceInput(voiceData, grammarName);
        }

        public void EyeInput(IFixationData fixation)
        {
            //Sending the fixation data to each of the hotspots
            foreach (StateHotspot sh in stateHotspots)
            {
                sh.Fixation(fixation.GetFixationLocation());
            }

            //Passing fixation data to the current state
            currentState.EyeInput(fixation);
        }

        public void KeyboardInput(Keys keyPressed)
        {
            //Sending keyboard input to the current state
            currentState.KeyboardInput(keyPressed);
        }
    }
}
