﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using WindowsInput;
using EyeXFramework;
using Tobii.EyeX.Framework;
using System.Speech.Recognition;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VoxVisio.Properties;
using VoxVisio.Singletons;

namespace VoxVisio
{
    public class MainEngine
    {
        private ControlState controlState;
        private readonly InputSimulator inputSimulator;
        private CommandSingleton commandList;
        private SharedDataSingleton sharedData;
        private SpeechRecognitionEngine speechRecognizer = new SpeechRecognitionEngine();
        private Grammar commandGrammar;
        private Grammar dictationGrammar;

        public MainEngine()
        {
            commandList = CommandSingleton.Instance();

            controlState = new CommandState();

            SetupSpeechRecognition();


            EventSingleton.Instance().fixationEvent += sharedData_fixationEvent;
        }

        void sharedData_fixationEvent(Fixation newFixation)
        {
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
            var keywords = commandList.Commands.Select(coms => coms.VoiceKeyword);
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
