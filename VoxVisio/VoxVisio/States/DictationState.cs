using System;
using System.Drawing;
using VoxVisio.Screen_Overlay;
using VoxVisio.Singletons;
using WindowsInput;

namespace VoxVisio
{
    class DictationState : ControlState
    {

        private InputSimulator inputsim;
        public DictationState()
        {
            this.inputsim = SharedObjectsSingleton.Instance().inputSimulator;

            inputsim.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.NUMPAD0);
        }

        public override void VoiceInput(string voiceData)
        {
            
        }

        public override void EyeInput(IFixationData fixation)
        {

        }

        public override void Dispose()
        {
             inputsim.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.NUMPAD0);
        }
    }
}
