using FMUtils.KeyboardHook;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Windows.Forms;
using VoxVisio.Screen_Overlay;
using VoxVisio.Singletons;

namespace VoxVisio
{
    public class MainEngine
    {
        private List<Command> commandList;
        private SpeechRecognitionEngine speechRecognizer;
        private StateController stateController;

        public MainEngine()
        {
            commandList = SettingsSingleton.Instance().Commands;
            speechRecognizer = createSpeechRecogntionEngine();

            stateController = new StateController();

            EventSingleton.Instance().fixationEvent += sharedData_fixationEvent;
            EventSingleton.Instance().systemHook.KeyDown += sharedData_keyboardEvent;
            //EventSingleton.Instance().keyboardHook.KeyDownEvent += sharedData_keyboardEvent;
            speechRecognizer.SpeechRecognized += SpeechRecognised;
        }

        private void sharedData_fixationEvent(Fixation newFixation)
        {
            stateController.EyeInput(newFixation);
        }

        private SpeechRecognitionEngine createSpeechRecogntionEngine()
        {
            SpeechRecognitionEngine newSpeechRecognizer = new SpeechRecognitionEngine();

            //Setting up the grammars for the voice recognizer
            Grammar commandGrammar = createCommandGrammar();

            Grammar dictationGrammar = new DictationGrammar();
            dictationGrammar.Name = DictationState.GRAMMARNAME;

            //Setting up the voice recognizer to start listening for commands and send them to the SpeechRecognised method
            newSpeechRecognizer.RequestRecognizerUpdate();
            newSpeechRecognizer.LoadGrammar(dictationGrammar);
            newSpeechRecognizer.LoadGrammar(commandGrammar);
            newSpeechRecognizer.SetInputToDefaultAudioDevice();
            newSpeechRecognizer.RecognizeAsync(RecognizeMode.Multiple);

            return newSpeechRecognizer;
        }

        private void updateVoiceRecognition()
        {
            speechRecognizer.SpeechRecognized -= SpeechRecognised;
            speechRecognizer = createSpeechRecogntionEngine();
            speechRecognizer.SpeechRecognized += SpeechRecognised;
        }

        public void sharedData_keyboardEvent(object sender, KeyEventArgs keyEventArgs)
        {
            stateController.KeyboardInput(keyEventArgs.KeyCode);
        }

        public void SpeechRecognised(object sender, SpeechRecognizedEventArgs e)
        {
            stateController.VoiceInput(e.Result.Text, e.Result.Grammar.Name);
        }

       

        public Grammar createCommandGrammar()
        {
            var keywords = commandList.Select(coms => coms.GetKeyWord());

            Choices sList = new Choices();
            sList.Add(keywords.ToArray());


            sList.Add("start scroll");
            sList.Add("stop scroll");

            GrammarBuilder gb = new GrammarBuilder(sList);
            Grammar newCommandGrammar = new Grammar(gb);
            newCommandGrammar.Name = CommandState.GRAMMARNAME;

            return newCommandGrammar;
        }

        internal void close()
        {
            speechRecognizer.Dispose();
            EventSingleton.Instance().fixationEvent -= sharedData_fixationEvent;
            EventSingleton.Instance().systemHook.KeyDown -= sharedData_keyboardEvent;
            speechRecognizer.SpeechRecognized -= SpeechRecognised;
        }
    }
}
