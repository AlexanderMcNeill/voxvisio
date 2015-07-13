﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace VoxVisio
{
    class DictationState : ControlState
    {

        private Rectangle endDictationHotspot;
        private int endDictationCounter = 0;
        private const int SELECTIONTIME = 3000;

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
