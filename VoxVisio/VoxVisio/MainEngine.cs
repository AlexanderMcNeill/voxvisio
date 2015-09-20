using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using WindowsInput;
using EyeXFramework;
using Tobii.EyeX.Framework;
using System.Speech.Recognition;
using VoxVisio.Singletons;
using VoxVisio.Screen_Overlay;

namespace VoxVisio
{
    public class MainEngine
    {
        private ControlState controlState;
        private readonly InputSimulator inputSimulator;
        private SettingsSingleton _settingsList;
        private SharedDataSingleton sharedData;
        private SpeechRecognitionEngine speechRecognizer = new SpeechRecognitionEngine();
        private Grammar commandGrammar;
        private Grammar dictationGrammar;
        private StateController stateController;

        public MainEngine()
        {
            _settingsList = SettingsSingleton.Instance();
            controlState = new CommandState();
            SetupSpeechRecognition();
            EventSingleton.Instance().fixationEvent += sharedData_fixationEvent;
            stateController = new StateController(controlState);
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
            var keywords = _settingsList.Commands.Select(coms => coms.VoiceKeyword);
            Choices sList = new Choices();
            sList.Add(keywords.ToArray());
            sList.Add("start dictation");
            sList.Add("start scroll");
            sList.Add("stop scroll");
            GrammarBuilder gb = new GrammarBuilder(sList);
            commandGrammar = new Grammar(gb);
        }

        internal void close()
        {
            speechRecognizer.Dispose();
        }
    }
}
