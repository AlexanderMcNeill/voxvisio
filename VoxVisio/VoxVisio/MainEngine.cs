using FMUtils.KeyboardHook;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using VoxVisio.Screen_Overlay;
using VoxVisio.Singletons;

namespace VoxVisio
{
    public class MainEngine
    {
        private ControlState controlState;
        private List<Command> commandList;
        private SpeechRecognitionEngine speechRecognizer;
        private StateController stateController;

        public MainEngine()
        {
            commandList = SettingsSingleton.Instance().Commands;
            controlState = new CommandState();
            speechRecognizer = CreateSpeechRecogntionEngine();

            stateController = new StateController(controlState);

            EventSingleton.Instance().fixationEvent += sharedData_fixationEvent;
            EventSingleton.Instance().keyboardHook.KeyDownEvent += sharedData_keyboardEvent;
            speechRecognizer.SpeechRecognized += SpeechRecognised;
        }

        void sharedData_fixationEvent(Fixation newFixation)
        {
            stateController.Fixation(newFixation.GetFixationLocation());
            controlState.EyeInput(newFixation);
        }

        private SpeechRecognitionEngine CreateSpeechRecogntionEngine()
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

        public void sharedData_keyboardEvent(KeyboardHookEventArgs e)
        {
            controlState.KeyboardInput(e.Key);
        }

        public void SpeechRecognised(object sender, SpeechRecognizedEventArgs e)
        {
            controlState.VoiceInput(e.Result.Text, e.Result.Grammar.Name);
        }

       

        public Grammar createCommandGrammar()
        {
            var keywords = commandList.Select(coms => coms.GetKeyWord());

            Choices sList = new Choices();
            sList.Add(keywords.ToArray());


            sList.Add("start dictation");
            sList.Add("start scroll");
            sList.Add("stop scroll");
            sList.Add("start keyboard");
            sList.Add("stop keyboard");

            GrammarBuilder gb = new GrammarBuilder(sList);
            Grammar newCommandGrammar = new Grammar(gb);
            newCommandGrammar.Name = CommandState.GRAMMARNAME;

            return newCommandGrammar;
        }

        internal void close()
        {
            speechRecognizer.Dispose();
            EventSingleton.Instance().fixationEvent -= sharedData_fixationEvent;
            EventSingleton.Instance().keyboardHook.KeyDownEvent -= sharedData_keyboardEvent;
            speechRecognizer.SpeechRecognized -= SpeechRecognised;
        }
    }
}
