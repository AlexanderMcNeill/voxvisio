using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.Native;

namespace VoxVisio.Commands
{
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
