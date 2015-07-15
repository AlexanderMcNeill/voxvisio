using System;
using System.Drawing;
using WindowsInput;

namespace VoxVisio
{
    class DictationState : ControlState
    {

        private InputSimulator inputsim;
        private HotspotForm hotspotForm;

        public DictationState(InputSimulator inputsim)
        {
            this.inputsim = inputsim;
            hotspotForm = new HotspotForm();
            hotspotForm.Show();
        }

        public override void VoiceInput(string voiceData, ControlContext context)
        {
            inputsim.Keyboard.TextEntry(voiceData);
        }

        public override void EyeInput(ControlContext context, IFixationData fixation)
        {
            if (fixation != null) //shouldnt need this need to figure out why we are getting nulls
            {
                hotspotForm.updateHotspot(fixation.GetFixationLocation());
                if (hotspotForm.PercentFill >= 100)
                {
                    //hotspotForm.Close(); Can't close this form due to being on the wrong thread
                    context.ControlState = new StandardState(inputsim);
                }
            }
        }
    }
}
