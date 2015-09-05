using System;
using System.Drawing;
using WindowsInput;

namespace VoxVisio
{
    class DictationState : ControlState
    {

        private InputSimulator inputsim;
        private HotspotForm hotspotForm;
        private ZoomForm zoomForm;

        public DictationState(InputSimulator inputsim, ZoomForm zoomForm)
        {
            this.inputsim = inputsim;

            //Creating the hotspot form that allows the user to exit the state
            hotspotForm = new HotspotForm();
            hotspotForm.Show();

            inputsim.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.NUMPAD0);
            this.zoomForm = zoomForm;
        }

        public override void VoiceInput(string voiceData, ControlContext context)
        {
            
        }

        public override void EyeInput(ControlContext context, IFixationData fixation)
        {
            //Updating the hotspot and checking if it has been looked at long enough to exit the state
            hotspotForm.updateHotspot(fixation.GetFixationLocation());
            if (hotspotForm.PercentFill >= 100)
            {
                //Closing the form and changing to the command state
                hotspotForm.requestClose();
                context.ControlState = new StandardState(inputsim, zoomForm);
                inputsim.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.NUMPAD0);
            }
        }
    }
}
