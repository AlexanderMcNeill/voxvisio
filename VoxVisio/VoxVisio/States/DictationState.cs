using WindowsInput;
using WindowsInput.Native;
using VoxVisio.Singletons;
using System;
using System.Windows.Forms;
using VoxVisio.Screen_Overlay;

namespace VoxVisio
{
    class DictationState : ControlState
    {
        public const string GRAMMARNAME = "DictationGrammar";

        private InputSimulator inputsim;
        private Toast toast;
        public DictationState()
        {
            this.inputsim = SharedObjectsSingleton.Instance().inputSimulator;
            toast = SharedFormsSingleton.Instance().ToastOverlay;
        }

        public override void VoiceInput(string voiceData, string grammarName)
        {
            if (grammarName == CommandState.GRAMMARNAME)
            {
                toast.NewMessage("Commands can't be used in dictation mode");
            }
            else
            {
                if (!SettingsSingleton.Instance().DragonEnabled && grammarName.Equals(GRAMMARNAME))
                    inputsim.Keyboard.TextEntry(voiceData);
            }
            
        }

        public override void EyeInput(IFixationData fixation)
        {

        }

        public override void KeyboardInput(Keys keyPressed)
        {

        }

        public override void Start()
        {
            if (SettingsSingleton.Instance().DragonEnabled)
                inputsim.Keyboard.KeyPress(VirtualKeyCode.NUMPAD0);
        }

        public override void Stop()
        {
            if (SettingsSingleton.Instance().DragonEnabled)
                inputsim.Keyboard.KeyPress(VirtualKeyCode.NUMPAD0);
        }
    }
}
