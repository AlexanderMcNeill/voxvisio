using System;

namespace VoxVisio
{
    abstract class ControlState
    {
        public abstract void VoiceInput(String voiceData, ControlContext context); // TODO : Alex check if this datatype is fine for your system
        public abstract void EyeInput(ControlContext context, IFixationData fixation); 
    }
}
