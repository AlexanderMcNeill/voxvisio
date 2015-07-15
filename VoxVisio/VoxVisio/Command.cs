using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using WindowsInput;
using WindowsInput.Native;

namespace VoxVisio
{
    public class CommandSingleton
    {
        private static CommandSingleton _singleton;
        private List<Command> commands;

        protected CommandSingleton()
        {
            
        }

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
    }

    /// <summary>
    /// Used for loading in the xml command strings before they are converted to their final form.
    /// </summary>
    [Serializable()]
    public class CommandStrings
    {
        public string commandWord { get; set; }
        
        public string keyStrings { get; set; }

        public CommandStrings(string commandWord, string keyStrings)
        {
            this.commandWord = commandWord;
            this.keyStrings = keyStrings;
        }
    }

    public class Command
    {
<<<<<<< HEAD
        public string VoiceKeyword { get; }
        public KeyCombo keyCombo { get; }
=======
        public string VoiceKeyword { set; get; }
        private KeyCombo keyCombo { set;  get; }
>>>>>>> df0751789113ca9dca14eae97175be5bd3d4bbe0

        public Command(CommandStrings temp, InputSimulator inputSimulator)
        {
            this.VoiceKeyword = temp.commandWord;
            this.keyCombo = new KeyCombo(temp.keyStrings, inputSimulator);
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
                inputSimulator.Keyboard.KeyDown(virtualKeyCode);
            }
        }

        public void ReleaseHeldKeys()
        {
            foreach (VirtualKeyCode virtualKeyCode in Keys)
            {
                inputSimulator.Keyboard.KeyUp(virtualKeyCode);
            }
        }
    }
}
