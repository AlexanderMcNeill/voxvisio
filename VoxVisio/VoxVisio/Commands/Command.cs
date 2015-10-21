using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Native;
using Newtonsoft.Json.Linq;
using VoxVisio.Singletons;

/*
*   Notes:
*   VirtualKeyCodes are used by the input simulator to simulate key presses,
*   and Keys provided by the system hook whenever a key is pressed. VirtualKeyCodes and keys can be cast between eachother nativly.
*/

namespace VoxVisio
{
    public enum eCommandType
    {
        VoiceCommand,
        KeyPressCommand,
        OpenProgramCommand,
    }
    public interface Command
    {
        void RunCommand();
        void LoadFromJson(JObject jsonData);
        JObject SaveToJson();
        string GetKeyWord();
        eCommandType GetCommandType();
    }

    public class OpenProgramCommand : Command
    {
        // How to start a program System.Diagnostics.Process.Start("C:/Program Files (x86)/OptiKey/OptiKey.exe");
        private string ProgramLocation;
        private string KeyWord;

        public OpenProgramCommand(string programLocation, string KeyWord)
        {
            this.ProgramLocation = programLocation;
            this.KeyWord = KeyWord;
        }

        public OpenProgramCommand(JObject jsonData)
        {
            LoadFromJson(jsonData);
        }

        public void RunCommand()
        {
            Process.Start(ProgramLocation);
        }

        public void LoadFromJson(JObject jsonData)
        {
            ProgramLocation = (string) jsonData["program location"];
            KeyWord =  (string)jsonData["keyword"];
        }

        public JObject SaveToJson()
        {
            JObject toReturn = new JObject();
            toReturn["program location"] = ProgramLocation;
            toReturn["keyword"] = KeyWord;
            toReturn["command type"] = GetCommandType().ToString();
            return toReturn;
        }

        public string GetKeyWord()
        {
            return KeyWord;
        }

        public string GetProgramLocation()
        {
            return ProgramLocation;
        }
        public eCommandType GetCommandType()
        {
            return eCommandType.OpenProgramCommand;
        }
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
            toReturn["command type"] = GetCommandType().ToString();
            return toReturn;
        }


        public string GetKeyWord()
        {
            return commandWord;
        }

        public eCommandType GetCommandType()
        {
            return eCommandType.KeyPressCommand;
        }
    }

    public class CommandFactory
    {
        public static Command CreateCommandFromJson(JObject jsonData)
        {
            Command commandObject = null;
            
            string commandType= (string) jsonData["command type"];

            if (commandType == eCommandType.KeyPressCommand.ToString())
            {
                commandObject = new KeyPressCommand(jsonData);
            } else if (commandType == eCommandType.VoiceCommand.ToString())
            {
                commandObject = new VoiceCommand(jsonData);
            }
            else if (commandType == eCommandType.OpenProgramCommand.ToString())
            {
                commandObject = new OpenProgramCommand(jsonData);
            }

            return commandObject;
        }
    }

    public class VoiceCommand : Command
    {
        public string VoiceKeyword { set; get; }
        public KeyCombo keyCombo { set; get; }

        private string keyStrings;

        public VoiceCommand(string commandWord, string keyStrings, InputSimulator inputSimulator)
        {
            this.keyStrings = keyStrings;
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
            if ((keyCombo.GetKeyString().Contains("LButton") || keyCombo.GetKeyString().Contains("RButton")) && SettingsSingleton.Instance().ZoomEnabled)
            {
                SharedFormsSingleton.Instance().zoomForm.startZoomClick(keyCombo);
            }
            else
            {
                keyCombo.PressKeys();
            }
        }

        public void PressKeys()
        {
            keyCombo.PressKeys();
        }

        public void LoadFromJson(JObject jsonData)
        {
            this.VoiceKeyword = (string)jsonData["voice keyword"];
            this.keyStrings = (string)jsonData["keys"];
            this.keyCombo = new KeyCombo((string)jsonData["keys"], SharedObjectsSingleton.Instance().inputSimulator);
        }

        public JObject SaveToJson()
        {
            JObject toReturn = new JObject();
            toReturn["voice keyword"] = VoiceKeyword;
            toReturn["keys"] = keyCombo.GetKeyString();
            toReturn["command type"] = GetCommandType().ToString();
            return toReturn;
        }

        public string GetKeyWord()
        {
            return VoiceKeyword;
        }

        public string GetKeyStrings()
        {
            return keyStrings;
        }

        public eCommandType GetCommandType()
        {
            return eCommandType.VoiceCommand;
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
