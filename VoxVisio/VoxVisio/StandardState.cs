using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoxVisio
{
    class StandardState : ControlState
    {
        public override void VoiceInput(string voiceData, ControlContext context)
        {
            throw new NotImplementedException();
        }

        public override void EyeInput(ControlContext context, IFixationData fixation)
        {
            throw new NotImplementedException();
        }
    }
}
