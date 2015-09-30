using System;
using System.Windows.Forms;

namespace VoxVisio
{
    abstract class ControlState: IDisposable
    {
        public abstract void VoiceInput(String voiceData, String grammarName);
        public abstract void EyeInput(IFixationData fixation);
        public abstract void KeyboardInput(Keys keyPressed);
        public abstract void Dispose();
    }
}
