using System;
using System.Drawing;
using WindowsInput;

namespace VoxVisio
{
    class DictationState : ControlState
    {

        private Rectangle endDictationHotspot;
        private int endDictationCounter = 0;
        private const int SELECTIONTIME = 3000;
        private InputSimulator inputsim;

        public DictationState(InputSimulator inputsim)
        {
            this.inputsim = inputsim;
        }

        public override void VoiceInput(string voiceData, ControlContext context)
        {
            throw new NotImplementedException();
        }

        public override void EyeInput(ControlContext context, IFixationData fixation)
        {
            if (endDictationHotspot.Contains(fixation.GetFixationLocation()))
            {
                endDictationCounter++;
            }
            throw new NotImplementedException();
        }
    }
}
