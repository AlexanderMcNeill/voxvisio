using System;

namespace VoxVisio
{
    abstract class ControlState: IDisposable
    {
        public abstract void VoiceInput(String voiceData); // TODO : Alex check if this datatype is fine for your system
        public abstract void EyeInput(IFixationData fixation);
        public abstract void Dispose();
    }
}
