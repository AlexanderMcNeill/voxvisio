using FMUtils.KeyboardHook;
using System.Linq;
using System.Speech.Recognition;
using VoxVisio.Screen_Overlay;
using VoxVisio.Singletons;

namespace VoxVisio
{
    public class MainEngine
    {
        private ControlState controlState;
        private SettingsSingleton _settingsList;
        private SpeechRecognitionEngine speechRecognizer = new SpeechRecognitionEngine();
        private Grammar commandGrammar;
        private Grammar dictationGrammar;
        private StateController stateController;

        public MainEngine()
        {
            _settingsList = SettingsSingleton.Instance();
            controlState = new CommandState();
            SetupSpeechRecognition();

            stateController = new StateController(controlState);

            EventSingleton.Instance().fixationEvent += sharedData_fixationEvent;
            EventSingleton.Instance().keyboardHook.KeyDownEvent += sharedData_keyboardEvent;
        }

        void sharedData_fixationEvent(Fixation newFixation)
        {
            stateController.Fixation(newFixation.GetFixationLocation());
            controlState.EyeInput(newFixation);
        }

        private void SetupSpeechRecognition()
        {

            //Setting up the grammars for the voice recognizer
            loadCommandGrammar();
            dictationGrammar = new DictationGrammar();
            dictationGrammar.Name = "dictation";
            commandGrammar.Name = "command";
            //Setting up the voice recognizer to start listening for commands and send them to the SpeechRecognised method
            speechRecognizer.RequestRecognizerUpdate();
            speechRecognizer.LoadGrammar(dictationGrammar);
            speechRecognizer.LoadGrammar(commandGrammar);
            speechRecognizer.SpeechRecognized += SpeechRecognised;
            speechRecognizer.SetInputToDefaultAudioDevice();
            speechRecognizer.RecognizeAsync(RecognizeMode.Multiple);
        }

        public void sharedData_keyboardEvent(KeyboardHookEventArgs e)
        {
            controlState.KeyboardInput(e.Key);
        }

        public void SpeechRecognised(object sender, SpeechRecognizedEventArgs e)
        {
            if (controlState.GetType() == typeof(CommandState) && e.Result.Grammar.Name == "command")
            {
                controlState.VoiceInput(e.Result.Text);
            }
            else if (controlState.GetType() == typeof(DictationState) && e.Result.Grammar.Name == "dictation")
            {
                controlState.VoiceInput(e.Result.Text);
            }
        }

       

        public void loadCommandGrammar()
        {
            var keywords = _settingsList.Commands.Select(coms => coms.GetKeyWord());
            Choices sList = new Choices();
            sList.Add(keywords.ToArray());
            sList.Add("start dictation");
            sList.Add("start scroll");
            sList.Add("stop scroll");
            sList.Add("start keyboard");
            sList.Add("stop keyboard");
            GrammarBuilder gb = new GrammarBuilder(sList);
            commandGrammar = new Grammar(gb);
        }

        internal void close()
        {
            speechRecognizer.Dispose();
        }
    }
}
