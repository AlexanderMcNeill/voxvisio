using WindowsInput;
using WindowsInput.Native;
using VoxVisio.Singletons;
using System;
using System.Windows.Forms;

namespace VoxVisio
{
    class DictationState : ControlState
    {
        public const string GRAMMARNAME = "DictationGrammar";

        private InputSimulator inputsim;
        public DictationState()
        {
            this.inputsim = SharedObjectsSingleton.Instance().inputSimulator;

            if(SettingsSingleton.Instance().DragonEnabled)
                inputsim.Keyboard.KeyPress(VirtualKeyCode.NUMPAD0);
        }

        public override void VoiceInput(string voiceData, string grammarName)
        {
            if (!SettingsSingleton.Instance().DragonEnabled && grammarName.Equals(GRAMMARNAME))
                inputsim.Keyboard.TextEntry(voiceData);
        }

        public override void EyeInput(IFixationData fixation)
        {

        }

        public override void KeyboardInput(Keys keyPressed)
        {

        }

        public override void Dispose()
        {
            if (SettingsSingleton.Instance().DragonEnabled)
                inputsim.Keyboard.KeyPress(VirtualKeyCode.NUMPAD0);
        }
    }
}
