using System;
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

namespace VoxVisio
{
    public class MainEngine
    {
        private ControlContext controlState;
        private EyeXHost eyex;
        private readonly InputSimulator inputSimulator;
        private CommandSingleton commandList;

        private List<Command> commands; 

        //TODO : Alex add in voice requiered classes
        private SpeechRecognitionEngine speechRecognizer = new SpeechRecognitionEngine();
        private Grammar commandGrammar;
        private Grammar dictationGrammar;

        public MainEngine()
        {
            controlState = new ControlContext(new DictationState(inputSimulator)); //Change back to command state once working
            eyex = new EyeXHost();
            inputSimulator = new InputSimulator();
            controlState.changedState += StateChanged;
            eyex.CreateFixationDataStream(FixationDataMode.Sensitive).Next += (s, e) => Fixation(e.EventType ,(int)e.X, (int)e.Y, e.Timestamp);
            //loadCommands(); need to fix your file reading daniel
            //loadCommandGrammar();
            dictationGrammar = new DictationGrammar();
            commandList = CommandSingleton.Instance();
            commandList.SetCommands(commands);

            speechRecognizer.RequestRecognizerUpdate();
            speechRecognizer.LoadGrammar(dictationGrammar); //Change back to command grammar once working
            speechRecognizer.SpeechRecognized += SpeechRecognised;
            speechRecognizer.SetInputToDefaultAudioDevice();
            speechRecognizer.RecognizeAsync(RecognizeMode.Multiple);

            eyex.Start();
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
            controlState.VoiceRequest(e.Result.Text);
        }

        public void loadCommands()
        {
            var commandStrings = new List<CommandStrings>();
            using (StreamReader reader = File.OpenText(@"Commands.json"))
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

        public void loadCommandGrammar()
        {
            var keywords = commands.Select(coms => coms.VoiceKeyword);
            Choices sList = new Choices();
            sList.Add(keywords.ToArray());
            commandGrammar = new Grammar(new GrammarBuilder(sList));
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
