using WindowsInput;
using WindowsInput.Native;
using VoxVisio.Singletons;
using System;
using System.Windows.Forms;

namespace VoxVisio
{
    class DictationState : ControlState
    {

        private InputSimulator inputsim;
        public DictationState()
        {
            this.inputsim = SharedObjectsSingleton.Instance().inputSimulator;

            inputsim.Keyboard.KeyPress(VirtualKeyCode.NUMPAD0);
        }

        public override void VoiceInput(string voiceData, string grammarName)
        {
            
        }

        public override void EyeInput(IFixationData fixation)
        {

        }

        public override void KeyboardInput(Keys keyPressed)
        {
            throw new NotImplementedException();
        }

        public override void Dispose()
        {
             inputsim.Keyboard.KeyPress(VirtualKeyCode.NUMPAD0);
        }
    }
}
