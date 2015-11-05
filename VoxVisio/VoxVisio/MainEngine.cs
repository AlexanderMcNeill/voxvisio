using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Speech.Recognition;
using System.Threading;
using System.Windows.Forms;
using VoxVisio.Screen_Overlay;
using VoxVisio.Singletons;
using VoxVisio.Commands;
using VoxVisio.States;
using VoxVisio.UI;

namespace VoxVisio
{
    public class MainEngine
    {
        private List<Command> commandList;
        private SpeechRecognitionEngine speechRecognizer;
        private StateController stateController;

        public MainEngine(MainSystemTray form)
        {
            //Fetch the list of commands
            commandList = SettingsSingleton.Instance().Commands;
            // Create the speech recognition engine
            speechRecognizer = createSpeechRecogntionEngine(form);

            stateController = new StateController();

            //Register to the event that provides the fixations
            EventSingleton.Instance().fixationEvent += sharedData_fixationEvent;
            //Register to the event that fires when keyboard keys are pressed
            EventSingleton.Instance().systemHook.KeyDown += sharedData_keyboardEvent;
            speechRecognizer.SpeechRecognized += SpeechRecognised;
        }

        private void sharedData_fixationEvent(Fixation newFixation)
        {
            stateController.EyeInput(newFixation);
        }

        private SpeechRecognitionEngine createSpeechRecogntionEngine(MainSystemTray form)
        {
            SpeechRecognitionEngine newSpeechRecognizer = new SpeechRecognitionEngine(CultureInfo.CurrentCulture);

            //Setting up the grammars for the voice recognizer
            Grammar commandGrammar = createCommandGrammar();
            commandGrammar.Weight = 1f;


            Grammar dictationGrammar = new DictationGrammar("grammar:dictation");
            dictationGrammar.Name = DictationState.GRAMMARNAME;
            dictationGrammar.Weight = .5f;
          

            //Setting up the voice recognizer to start listening for commands and send them to the SpeechRecognised method
            newSpeechRecognizer.RequestRecognizerUpdate();
            newSpeechRecognizer.LoadGrammar(commandGrammar);
            newSpeechRecognizer.LoadGrammar(dictationGrammar);
            try
            {
                newSpeechRecognizer.SetInputToDefaultAudioDevice();
                newSpeechRecognizer.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch (System.InvalidOperationException)
            {
                if (MessageBox.Show(
                    "You do not have an audio capture device installed \nPlease install a microphone and restart the program",
                    "No Capture Device", MessageBoxButtons.OK) == DialogResult.OK)
                {
                    form.ExitProgram();
                }
                
            }
            

            return newSpeechRecognizer;
        }

        //private void updateVoiceRecognition()
        //{
        //    speechRecognizer.SpeechRecognized -= SpeechRecognised;
        //    speechRecognizer = createSpeechRecogntionEngine();
        //    speechRecognizer.SpeechRecognized += SpeechRecognised;
        //}

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
            sList.Add("scroll");
            sList.Add("start keyboard");
            sList.Add("stop keyboard");

            GrammarBuilder gb = new GrammarBuilder(sList);
            gb.Culture = Thread.CurrentThread.CurrentCulture;

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
