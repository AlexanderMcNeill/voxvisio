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
using VoxVisio.Properties;

namespace VoxVisio
{
    public class MainEngine
    {
        private ControlContext controlState;
        private EyeXHost eyex;
        private readonly InputSimulator inputSimulator;
        private CommandSingleton commandList;

        private List<Command> commands; 

        private SpeechRecognitionEngine speechRecognizer = new SpeechRecognitionEngine();
        private Grammar commandGrammar;
        private Grammar dictationGrammar;

        public MainEngine()
        {
            inputSimulator = new InputSimulator();
            
            controlState = new ControlContext(new StandardState(inputSimulator));
            controlState.changedState += StateChanged;



            loadCommands();

            //Setting up the grammars for the voice recognizer
            loadCommandGrammar();
            dictationGrammar = new DictationGrammar();

            commandList = CommandSingleton.Instance();
            commandList.SetCommands(commands);

            //Setting up the voice recognizer to start listening for commands and send them to the SpeechRecognised method
            speechRecognizer.RequestRecognizerUpdate();
            speechRecognizer.LoadGrammar(commandGrammar); 
            speechRecognizer.SpeechRecognized += SpeechRecognised;
            speechRecognizer.SetInputToDefaultAudioDevice();
            speechRecognizer.RecognizeAsync(RecognizeMode.Multiple);

            //Instantiating and starting the eye tracker host
            eyex = new EyeXHost();
            eyex.CreateFixationDataStream(FixationDataMode.Sensitive).Next += (s, e) => Fixation(e.EventType, (int)e.X, (int)e.Y, e.Timestamp);
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
            commands = new List<Command>();
            using (StreamReader reader = File.OpenText(@"Commands.json"))
            {
                JObject o = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
                JArray a = (JArray)o.SelectToken("command");
                
                foreach (JObject variable in a)
                {
                    commands.Add(new Command((string)variable["word"], (string)variable["keys"] , inputSimulator));
                }
            }
        }

        public void loadCommandGrammar()
        {
            var keywords = commands.Select(coms => coms.VoiceKeyword);
            Choices sList = new Choices();
            sList.Add(keywords.ToArray());
            sList.Add("dictation");
            GrammarBuilder gb = new GrammarBuilder(sList);
            commandGrammar = new Grammar(gb);
        }

        public void StateChanged()
        {
            if (controlState.ControlState.GetType() == typeof (StandardState))
            {
                speechRecognizer.UnloadAllGrammars();
                speechRecognizer.LoadGrammar(commandGrammar);
            }
            else if (controlState.ControlState.GetType() == typeof(DictationState))
            {
                speechRecognizer.UnloadAllGrammars();
                speechRecognizer.LoadGrammar(dictationGrammar);
                
            }
        }
    }
}
