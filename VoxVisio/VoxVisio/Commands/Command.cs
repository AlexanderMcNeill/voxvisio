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

/*
*   Notes:
*   VirtualKeyCodes are used by the input simulator to simulate key presses,
*   and Keys provided by the system hook whenever a key is pressed. VirtualKeyCodes and keys can be cast between eachother nativly.
*/

namespace VoxVisio
{
    public interface Command
    {
        void RunCommand();
        void LoadFromJson(JObject jsonData);
        JObject SaveToJson();
        string GetKeyWord();
        string GetCommandType();
    }


    public class KeyPressCommand : Command
    {
        // Key that triggers the command
        public Keys triggerKey;
        // Delegate to the method that needs to be called on trigger
        public string commandWord;

        public KeyPressCommand(string commandWord, Keys triggerKey)
        {
            this.triggerKey = triggerKey;
            this.commandWord = commandWord;
        }

        public KeyPressCommand(JObject jsonData)
        {
            LoadFromJson(jsonData);
        }

        //Looks through all of the standard commands 
        public void RunCommand()
        {
            SettingsSingleton.Instance().Commands.Find(x => x.GetKeyWord() == commandWord).RunCommand();
        }

        public void LoadFromJson(JObject jsonData)
        {
            string tempTriggerKey = (string) jsonData["trigger key"];         // Load String from json
            VirtualKeyCode vkCode = KeyTranslater.GetKeyCode(tempTriggerKey); // Convert the string to a keycode using the Keytranslator
            triggerKey = (Keys)vkCode;                                        // Cast the Vkeycode to a key and save to the class
            commandWord = (string)jsonData["command word"];
        }

        public JObject SaveToJson()
        {
            JObject toReturn = new JObject();
            toReturn["trigger key"] = KeyTranslater.GetKeyString((VirtualKeyCode)triggerKey);
            toReturn["command word"] = commandWord;
            toReturn["command type"] = GetCommandType();
            return toReturn;
        }


        public string GetKeyWord()
        {
            return commandWord;
        }

        public string GetCommandType()
        {
            return "key press trigger";
        }
    }

    public class CommandFactory
    {
        public static Command CreateCommandFromJson(JObject jsonData)
        {
            Command commandObject = null;
            switch ((string)jsonData["command type"])
            {
                case "key press trigger":
                    commandObject = new KeyPressCommand(jsonData);
                    break;
                case "voice command":
                    commandObject = new VoiceCommand(jsonData);
                    break;
            }
            return commandObject;
        }
    }

    public class VoiceCommand : Command
    {
        public string VoiceKeyword { set; get; }
        public KeyCombo keyCombo { set; get; }

        public VoiceCommand(string commandWord, string keyStrings, InputSimulator inputSimulator)
        {
            this.VoiceKeyword = commandWord;
            this.keyCombo = new KeyCombo(keyStrings, inputSimulator);
        }

        public VoiceCommand(JObject JsonData)
        {
            LoadFromJson(JsonData);
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

        public void RunCommand()
        {
            keyCombo.PressKeys();
        }

        public void LoadFromJson(JObject jsonData)
        {
            this.VoiceKeyword = (string)jsonData["voice keyword"];
            this.keyCombo = new KeyCombo((string)jsonData["keys"], SharedDataSingleton.Instance().inputSimulator);
        }

        public JObject SaveToJson()
        {
            JObject toReturn = new JObject();
            toReturn["voice keyword"] = VoiceKeyword;
            toReturn["keys"] = keyCombo.GetKeyString();
            toReturn["command type"] = GetCommandType();
            return toReturn;
        }

        public string GetKeyWord()
        {
            return VoiceKeyword;
        }

        public string GetCommandType()
        {
            return "voice command";
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
        private void PressDownKeys()
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
        private void ReleaseHeldKeys()
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
