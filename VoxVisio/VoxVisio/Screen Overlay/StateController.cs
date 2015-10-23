using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using VoxVisio.Singletons;
using VoxVisio.States;

namespace VoxVisio.Screen_Overlay
{
    class StateController : Overlay
    {
        private const int HOTSPOTSIZE = 100;
        private const int MARGIN = 10;

        private List<StateHotspot> stateHotspots = new List<StateHotspot>();
        private OverlayForm overlayForm;
        private ControlState state;
        private CommandState commandState;
        private DictationState dictationState;

        public StateController()
        {
            //Creating states
            commandState = new CommandState();
            dictationState = new DictationState();

            //Setting the current state to command state
            state = commandState;

            //Creating the hotspots that will be used to change the states
            stateHotspots = createStateHotspots();

            //Registering state controller to be drawn to the overlay form
            overlayForm = SharedFormsSingleton.Instance().overlayForm;
            overlayForm.RegisterOverlay(this);

            //Setting up state controller to listen to the update timer tick
            EventSingleton.Instance().updateTimer.Tick += updateTimer_Tick;
        }

        private List<StateHotspot> createStateHotspots()
        {
            List<StateHotspot> stateHotspots = new List<StateHotspot>();

            int top = Screen.PrimaryScreen.Bounds.Height / 2 - MARGIN / 2 - HOTSPOTSIZE;
            int left = Screen.PrimaryScreen.Bounds.Width - HOTSPOTSIZE;

            Rectangle commandStateHotspot = new Rectangle(left, top - HOTSPOTSIZE, HOTSPOTSIZE, HOTSPOTSIZE * 2);
            Rectangle dictationStateHotspot = new Rectangle(left, top + HOTSPOTSIZE + MARGIN, HOTSPOTSIZE, HOTSPOTSIZE * 2);


            stateHotspots.Add(new StateHotspot(eState.Command, commandStateHotspot, true, Properties.Resources.commandButtonsActive, Properties.Resources.commandButtonInactive));
            stateHotspots.Add(new StateHotspot(eState.Dictation, dictationStateHotspot, false, Properties.Resources.dictationButtonsActive, Properties.Resources.dictationButtonInactive));

            return stateHotspots;
        }

        public void StateChanged(eState newState)
        {
            switch (newState)
            { 
                case eState.Command:
                    state = commandState;
                    break;
                case eState.Dictation:
                    state = dictationState;
                    break;
            }
        }
        public void ChangeStateCommand()
        {
            state = commandState;
            stateHotspots[1].selected = false;
        }

        public void ChangeStateDictation()
        {
            state = dictationState;
            stateHotspots[0].selected = false;
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

        public override void VoiceInput(string voiceData, string grammarName)
        {
            //Sending the voice input to the current state
            state.VoiceInput(voiceData, grammarName);
        }

        public override void EyeInput(IFixationData fixation)
        {
            //Sending the fixation data to each of the hotspots
            foreach (StateHotspot sh in stateHotspots)
            {
                sh.Fixation(fixation.GetFixationLocation());
            }

            //Passing fixation data to the current state
            state.EyeInput(fixation);
        }

        public override void KeyboardInput(Keys keyPressed)
        {
            //Sending keyboard input to the current state
            state.KeyboardInput(keyPressed);
        }
    }
}
