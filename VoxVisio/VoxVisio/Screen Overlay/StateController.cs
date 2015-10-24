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
        private ControlState[] states;
        private StateHotspot[] stateHotspots;
        private StateHotspot selectedStateHotspot;

        public StateController()
        {
            //Creating states
            states = createStateArray();

            //Creating the hotspots that will be used to change the states
            stateHotspots = createStateHotspots();

            //Setting the current state to command state
            currentState = states[(int)eState.Command];
            selectedStateHotspot = stateHotspots[(int)eState.Command];
            selectedStateHotspot.selected = true;

            //Registering state controller to be drawn to the overlay form
            SharedFormsSingleton.Instance().overlayForm.RegisterOverlay(this);

            //Setting up state controller to listen to the update timer tick
            EventSingleton.Instance().updateTimer.Tick += updateTimer_Tick;
        }

        private ControlState[] createStateArray()
        { 
            ControlState[] states = new ControlState[Enum.GetNames(typeof(eState)).Length];

            states[(int)eState.Command] = new CommandState();
            states[(int)eState.Dictation] = new DictationState();

            return states;
        }

        private StateHotspot[] createStateHotspots()
        {
            StateHotspot[] stateHotspots = new StateHotspot[Enum.GetNames(typeof(eState)).Length];

            int top = Screen.PrimaryScreen.Bounds.Height / 2 - MARGIN / 2 - HOTSPOTSIZE;
            int left = Screen.PrimaryScreen.Bounds.Width - HOTSPOTSIZE;

            Rectangle commandStateHotspot = new Rectangle(left, top - HOTSPOTSIZE, HOTSPOTSIZE, HOTSPOTSIZE * 2);
            Rectangle dictationStateHotspot = new Rectangle(left, top + HOTSPOTSIZE + MARGIN, HOTSPOTSIZE, HOTSPOTSIZE * 2);

            StateHotspot commandState = new StateHotspot(eState.Command, commandStateHotspot, false, Properties.Resources.commandButtonsActive, Properties.Resources.commandButtonInactive);
            commandState.OnSelected += commandState_OnSelected;
            stateHotspots[(int)eState.Command]= commandState;

            StateHotspot dictationState = new StateHotspot(eState.Dictation, dictationStateHotspot, false, Properties.Resources.dictationButtonsActive, Properties.Resources.dictationButtonInactive);
            dictationState.OnSelected += commandState_OnSelected;
            stateHotspots[(int)eState.Dictation] = dictationState;

            return stateHotspots;
        }

        void commandState_OnSelected(StateHotspot sender, eState newState)
        {
            // Stopping the current state
            currentState.Stop();

            // Switching to the new state
            currentState = states[(int)newState];

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
