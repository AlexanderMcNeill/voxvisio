using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using WindowsInput;
using WindowsInput.Native;

namespace VoxVisio
{

    /// <summary>
    /// Used for loading in the xml command strings before they are converted to their final form.
    /// </summary>
    [Serializable()]
    class CommandStrings
    {
        public string commandWord { get; set; }
        
        public string keyStrings { get; set; }

        public CommandStrings(string commandWord, string keyStrings)
        {
            this.commandWord = commandWord;
            this.keyStrings = keyStrings;
        }
    }
    class Command
    {
        public string VoiceKeyword { set; get; }
        private KeyCombo keyCombo { set;  get; }

        public Command(CommandStrings temp, InputSimulator inputSimulator)
        {
            this.VoiceKeyword = temp.commandWord;
            this.keyCombo = new KeyCombo(temp.keyStrings, inputSimulator);
        }
    }

    class KeyCombo
    {
        public List<VirtualKeyCode> Keys;
        private readonly InputSimulator inputSimulator;

        public KeyCombo(string keysString, InputSimulator inputSimulator)
        {
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
