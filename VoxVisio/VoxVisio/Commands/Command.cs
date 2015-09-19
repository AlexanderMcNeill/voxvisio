using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Native;
using FMUtils.KeyboardHook;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VoxVisio.Singletons;

// KeysConverter
// Keys <=> Cast VirtualKeyCode

namespace VoxVisio
{
    public class SettingsSingleton
    {
        private static SettingsSingleton _singleton;
        private List<Command> commands;
        public readonly List<SpecialCommand> specialCommands;
        public readonly Hook keyboardHook;

        protected SettingsSingleton()
        {
            loadCommands();
            specialCommands = new List<SpecialCommand>();
            keyboardHook = new Hook("Global Action Hook");
            
        }

        

        public void addSpecialCommand(SpecialCommand toAddCommand)
        {
            specialCommands.Add(toAddCommand);
        }


        // A list of all currently loaded commands
        public List<Command> Commands
        {
            get { return commands; }
        }

        public static SettingsSingleton Instance()
        {
            // Uses lazy initialization.
            // Note: this is not thread safe.
            if (_singleton == null)
            {
                _singleton = new SettingsSingleton();
            }

            return _singleton;
        }

        public void SetCommands(List<Command> commands)
        {
            this.commands = commands;
        }
        private void saveCommands()
        {

            JsonSerializer serializer = new JsonSerializer();            
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (StreamWriter sw = new StreamWriter(@"c:\json.txt"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;

                writer.WriteStartObject();
                writer.WritePropertyName("command");
                writer.WriteStartArray();
                foreach (var item in commands)
                {
                    var dict = item.getDict();
                    writer.WriteStartObject();
                    foreach (var dictItem in dict)
                    {
                        writer.WritePropertyName(dictItem.Key);
                        writer.WriteValue(dictItem.Value);
                    }
                    writer.WriteEndObject();
                }
                writer.WriteEndArray();
                writer.WriteEndObject();

                writer.Close();             
               
            }
        }


        private void loadCommands()
        {
            var tempList = new List<Command>();
            string fileContents = Properties.Resources.Commands;
            using (StringReader reader = new StringReader(fileContents))//@"Commands.json"
            {
                JObject o = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
                JArray a = (JArray)o.SelectToken("command");

                foreach (JObject variable in a)
                {
<<<<<<< HEAD
                    tempList.Add(new Command((string)variable["word"], (string)variable["keys"], SharedDataSingleton.Instance().inputSimulator));
=======
                    commands.Add(new Command((string)variable["word"], (string)variable["keys"], SharedObjectsSingleton.Instance().inputSimulator));
>>>>>>> alex
                }
            }
            commands = tempList;
            //saveCommands();
        }

    }

    public class SpecialCommand
    {
        // Key that triggers the command
        public readonly Keys triggerKey;
        // Delegate to the method that needs to be called on trigger
        public readonly string commandWord;

        public SpecialCommand(string commandWord, Keys triggerKey)
        {
            this.triggerKey = triggerKey;
            this.commandWord = commandWord;
        }
        

    }

    public class Command
    {
        public string VoiceKeyword { set; get; }
        public KeyCombo keyCombo { set; get; }

        public Command(string commandWord, string keyStrings, InputSimulator inputSimulator)
        {
            this.VoiceKeyword = commandWord;
            this.keyCombo = new KeyCombo(keyStrings, inputSimulator);
        }

        public Dictionary<string,string> getDict()
        {
            var toReturn = new Dictionary<string, string>
            {
                {"voice keyword", VoiceKeyword},
                {"keys", keyCombo.GetKeyString()}
            };
            return toReturn;
        }
    }

    public class KeyCombo
    {
        public List<VirtualKeyCode> Keys;
        private readonly InputSimulator inputSimulator;

        public KeyCombo(string keysString, InputSimulator inputSimulator)
        {
            Keys = new List<VirtualKeyCode>();
            this.inputSimulator = inputSimulator;
            foreach (string VARIABLE in keysString.Split(','))
            {
                var keyCode = KeyTranslater.GetKeyCode(VARIABLE);
                Keys.Add(keyCode);
            }
        }

        public void PressKeys()
        {
            PressDownKeys();
            ReleaseHeldKeys();
        }
        public void PressDownKeys()
        {
            foreach (VirtualKeyCode virtualKeyCode in Keys)
            {
                switch (virtualKeyCode)
                {
                    case VirtualKeyCode.LBUTTON:
                        inputSimulator.Mouse.LeftButtonClick();
                        break;
                    case VirtualKeyCode.RBUTTON:
                        inputSimulator.Mouse.RightButtonClick();
                        break;
                    default:
                        inputSimulator.Keyboard.KeyDown(virtualKeyCode);
                        break;
                }
            }
        }

        public void ReleaseHeldKeys()
        {
            foreach (VirtualKeyCode virtualKeyCode in Keys)
            {
                inputSimulator.Keyboard.KeyUp(virtualKeyCode);
            }
        }
        public string GetKeyString()
        {
            var toReturn =
                from k in Keys
                select KeyTranslater.GetKeyString(k);
            return String.Join(", ", toReturn);
        }
    }
}
