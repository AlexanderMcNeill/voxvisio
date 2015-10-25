using WindowsInput;
using WindowsInput.Native;
using VoxVisio.Singletons;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using VoxVisio.Properties;
using VoxVisio.Screen_Overlay;
using VoxVisio.DictationModes;

namespace VoxVisio.States
{
    class DictationState : ControlState
    {
        public const string GRAMMARNAME = "DictationGrammar";

        private InputSimulator inputsim;
        private Toast toast;
        private Dictation dictation;
        public DictationState()
        {
            inputsim = SharedObjectsSingleton.Instance().inputSimulator;
            toast = SharedFormsSingleton.Instance().ToastOverlay;
            
            if (SettingsSingleton.Instance().DragonEnabled)
            {
                dictation = new DragonDictation();
            }
            else
            {
                dictation = new WindowsDictation();
            }
            Settings.Default.PropertyChanged += CheckState;

        }

        public override void VoiceInput(string voiceData, string grammarName)
        {
            if (grammarName == CommandState.GRAMMARNAME)
            {
                toast.NewMessage("Commands can't be used in dictation mode");
            }
            else
            {
                
                dictation.VoiceInput(voiceData);
            }
        }

        // Checks if the dictation class is still of the correct type, and changes it if requiered
        public void CheckState(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "DragonEnabled")
            {
                return;
            }
            if (Settings.Default.DragonEnabled && dictation.GetType() == typeof (WindowsDictation))
            {
                dictation.Dispose();
                dictation = new DragonDictation();
            }
            else if (!Settings.Default.DragonEnabled && dictation.GetType() == typeof(DragonDictation))
            {
                dictation.Dispose();
                dictation = new WindowsDictation();
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
            dictation.StartDictation();
        }

        public override void Stop()
        {
            dictation.StopDictation();
        }
        
    }
}
