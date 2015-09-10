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

namespace VoxVisio
{
    public class CommandSingleton
    {
        private static CommandSingleton _singleton;
        private List<Command> commands;
        private List<SpecialCommand> specialCommands;
        private Hook keyboardHook;

        protected CommandSingleton()
        {
            loadCommands();
            keyboardHook = new Hook("Global Action Hook");
            keyboardHook.KeyDownEvent += keyPressedDown;
        }

        private void keyPressedDown(KeyboardHookEventArgs e)
        {
            var keyPressed = e.Key;
        }


        // A list of all currently loaded commands
        public List<Command> Commands
        {
            get { return commands; }
        }

        public static CommandSingleton Instance()
        {
            // Uses lazy initialization.
            // Note: this is not thread safe.
            if (_singleton == null)
            {
                _singleton = new CommandSingleton();
            }

            return _singleton;
        }

        public void SetCommands(List<Command> commands)
        {
            this.commands = commands;
        }

        private void loadCommands()
        {
            commands = new List<Command>();
            string fileContents = Properties.Resources.Commands;
            using (StringReader reader = new StringReader(fileContents))//@"Commands.json"
            {
                JObject o = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
                JArray a = (JArray)o.SelectToken("command");

                foreach (JObject variable in a)
                {
                    commands.Add(new Command((string)variable["word"], (string)variable["keys"], SharedDataSingleton.Instance().inputSimulator));
                }
            }
        }
    }

    public class SpecialCommand
    {
        // Key that triggers the command

        // Delegate to the method that needs to be called on trigger
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
            return String.Join(",", toReturn);
        }
    }
}
