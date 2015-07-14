﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using WindowsInput;
using EyeXFramework;
using Tobii.EyeX.Framework;
using System.Speech.Recognition;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace VoxVisio
{
    public class MainEngine
    {
        private ControlContext controlState;
        private EyeXHost eyex;
        private readonly InputSimulator inputSimulator;

        private List<Command> commands; 

        //TODO : Alex add in voice requiered classes
        private SpeechRecognitionEngine speechRecognizer = new SpeechRecognitionEngine();
        private Grammar commandGrammar;
        private Grammar dictationGrammar;

        public MainEngine()
        {
            controlState = new ControlContext(new StandardState(inputSimulator));
            eyex = new EyeXHost();
            inputSimulator = new InputSimulator();
            controlState.changedState += StateChanged;
            eyex.CreateFixationDataStream(FixationDataMode.Sensitive).Next += (s, e) => Fixation(e.EventType ,(int)e.X, (int)e.Y, e.Timestamp);
            commands = new List<Command>();
            commandGrammar = null; //TODO: Add dans get grammar method
            dictationGrammar = new DictationGrammar();

            speechRecognizer.RequestRecognizerUpdate();
            speechRecognizer.LoadGrammar(commandGrammar);
            speechRecognizer.SpeechRecognized += SpeechRecognised;
            speechRecognizer.SetInputToDefaultAudioDevice();
            speechRecognizer.RecognizeAsync(RecognizeMode.Multiple);
        }


        public void Fixation(FixationDataEventType t, int x, int y, double timeStamp)
        {
            Fixation fx = null;
            if (t == FixationDataEventType.Begin)
            {
                fx = new Fixation(new Point(x, y), eFixationPhase.start);
            }
            if (t == FixationDataEventType.End)
            {
                fx = new Fixation(new Point(x,y),eFixationPhase.finished );
            }
            controlState.EyeRequest(fx);
        }

        public void SpeechRecognised(object sender, SpeechRecognizedEventArgs e)
        {
            controlState.VoiceRequest(e.Result.Text);
        }

        public void loadCommands()
        {
            List<CommandStrings> commandStrings = new List<CommandStrings>();
            using (StreamReader reader = File.OpenText(@"c:\person.json"))
            {
                JArray oa = (JArray) JToken.ReadFrom(new JsonTextReader(reader));
                foreach (JObject VARIABLE in oa)
                {
                    commandStrings.Add(new CommandStrings((string)VARIABLE.Property("word"), (string)VARIABLE.Property("keys"))); 
                }
                foreach (var VARIABLE in commandStrings)
                {
                    commands.Add(new Command(VARIABLE, inputSimulator));
                }
            }
        }

        public void StateChanged()
        {
            
            if (controlState.ControlState.GetType() == typeof (StandardState))
            {
                speechRecognizer.LoadGrammar(commandGrammar);
            }
            else if (controlState.ControlState.GetType() == typeof(DictationState))
            {
                speechRecognizer.LoadGrammar(dictationGrammar);
                
            }
           
        }
    }
}
