﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoxVisio
{
    abstract class ControlState
    {
        public abstract void VoiceInput(String voiceData); // TODO : Alex check if this datatype is fine for your system
        public abstract void EyeInput(); 
    }
}
