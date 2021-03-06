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

namespace VoxVisio
{
    public class MainEngine
    {
        private ControlContext controlState;
        private EyeXHost eyex;
        private readonly InputSimulator inputSimulator;
        private CommandSingleton commandList;
        private SharedDataSingleton sharedData;
        private SpeechRecognitionEngine speechRecognizer = new SpeechRecognitionEngine();
        private Grammar commandGrammar;
        private Grammar dictationGrammar;

        public MainEngine()
        {
            sharedData = SharedDataSingleton.Instance();
            commandList = CommandSingleton.Instance();
            inputSimulator = sharedData.inputSimulator;
            
            controlState = new ControlContext();
            controlState.changedState += StateChanged;
            controlState.ControlState = new CommandState(inputSimulator, controlState);
            
            //System.Diagnostics.Process.Start("C:/Program Files (x86)/Nuance/NaturallySpeaking13/Program/natspeak.exe");

            SetupSpeechRecognition();

            //Instantiating and starting the eye tracker host
            eyex = new EyeXHost();
            eyex.CreateFixationDataStream(FixationDataMode.Sensitive).Next += (s, e) => Fixation(e.EventType, (int)e.X, (int)e.Y, e.Timestamp);
            eyex.Start();
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


        public void Fixation(FixationDataEventType t, int x, int y, double timeStamp)
        {
            Fixation fx = null;
            switch (t)
            {
                case FixationDataEventType.Begin:
                    fx = new Fixation(new Point(x, y), eFixationPhase.start);
                    controlState.EyeRequest(fx);
                    break;
                case FixationDataEventType.End:
                    fx = new Fixation(new Point(x,y),eFixationPhase.finished );
                    controlState.EyeRequest(fx);
                    break;
            }
        }

        public void SpeechRecognised(object sender, SpeechRecognizedEventArgs e)
        {

            if (controlState.ControlState.GetType() == typeof(CommandState) && e.Result.Grammar.Name == "command")
            {
                controlState.VoiceRequest(e.Result.Text);
            }
            else if (controlState.ControlState.GetType() == typeof(DictationState) && e.Result.Grammar.Name == "dictation")
            {
                controlState.VoiceRequest(e.Result.Text);
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

        public void StateChanged()
        {

        }

        internal void close()
        {
            speechRecognizer.Dispose();
            eyex.Dispose();
        }
    }
}
