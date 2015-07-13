using System.Linq;
using WindowsInput.Native;

// https://msdn.microsoft.com/en-us/library/windows/desktop/dd375731(v=vs.85).aspx


namespace VoxVisio
{
    struct KeyCombo
    {
        public string Key;
        public VirtualKeyCode KeyCode;
        //public 
        public KeyCombo(string key, VirtualKeyCode keyCode)
        {
            Key = key;
            KeyCode = keyCode;
        }
    }
    class KeyTranslater
    {
        private KeyCombo[] keyCombos =
        {
            new KeyCombo("m1", VirtualKeyCode.LBUTTON),
            new KeyCombo("m2", VirtualKeyCode.RBUTTON),
            new KeyCombo("m3", VirtualKeyCode.MBUTTON),
            new KeyCombo("f1", VirtualKeyCode.F1),
            new KeyCombo("f2", VirtualKeyCode.F2),
            new KeyCombo("f3", VirtualKeyCode.F3),
            new KeyCombo("f4", VirtualKeyCode.F4),
            new KeyCombo("f5", VirtualKeyCode.F5),
            new KeyCombo("f6", VirtualKeyCode.F6),
            new KeyCombo("f7", VirtualKeyCode.F7),
            new KeyCombo("f8", VirtualKeyCode.F8),
            new KeyCombo("f9", VirtualKeyCode.F9),
            new KeyCombo("f10", VirtualKeyCode.F10),
            new KeyCombo("f11", VirtualKeyCode.F11),
            new KeyCombo("f12", VirtualKeyCode.F12),
            new KeyCombo("tab", VirtualKeyCode.TAB),
            new KeyCombo("ctrl", VirtualKeyCode.CONTROL),
            new KeyCombo("alt", VirtualKeyCode.MENU),
            new KeyCombo("a", VirtualKeyCode.VK_A), 
            new KeyCombo("b", VirtualKeyCode.VK_B), 
            new KeyCombo("c", VirtualKeyCode.VK_C), 
            new KeyCombo("d", VirtualKeyCode.VK_D), 
            new KeyCombo("e", VirtualKeyCode.VK_E), 
            new KeyCombo("f", VirtualKeyCode.VK_F), 
            new KeyCombo("g", VirtualKeyCode.VK_G), 
            new KeyCombo("h", VirtualKeyCode.VK_H), 
            new KeyCombo("i", VirtualKeyCode.VK_I), 
            new KeyCombo("j", VirtualKeyCode.VK_J), 
            new KeyCombo("k", VirtualKeyCode.VK_K), 
            new KeyCombo("l", VirtualKeyCode.VK_L), 
            new KeyCombo("m", VirtualKeyCode.VK_M), 
            new KeyCombo("n", VirtualKeyCode.VK_N), 
            new KeyCombo("o", VirtualKeyCode.VK_O), 
            new KeyCombo("p", VirtualKeyCode.VK_P), 
            new KeyCombo("q", VirtualKeyCode.VK_Q), 
            new KeyCombo("r", VirtualKeyCode.VK_R), 
            new KeyCombo("s", VirtualKeyCode.VK_S), 
            new KeyCombo("t", VirtualKeyCode.VK_T), 
            new KeyCombo("u", VirtualKeyCode.VK_U), 
            new KeyCombo("v", VirtualKeyCode.VK_V), 
            new KeyCombo("w", VirtualKeyCode.VK_W), 
            new KeyCombo("x", VirtualKeyCode.VK_X), 
            new KeyCombo("y", VirtualKeyCode.VK_Y), 
            new KeyCombo("z", VirtualKeyCode.VK_X), 
            new KeyCombo("1", VirtualKeyCode.VK_1), 
            new KeyCombo("2", VirtualKeyCode.VK_2), 
            new KeyCombo("3", VirtualKeyCode.VK_3), 
            new KeyCombo("4", VirtualKeyCode.VK_4), 
            new KeyCombo("5", VirtualKeyCode.VK_5), 
            new KeyCombo("6", VirtualKeyCode.VK_6), 
            new KeyCombo("7", VirtualKeyCode.VK_7), 
            new KeyCombo("8", VirtualKeyCode.VK_8), 
            new KeyCombo("9", VirtualKeyCode.VK_9), 
            new KeyCombo("0", VirtualKeyCode.VK_0), 
            new KeyCombo("-", VirtualKeyCode.OEM_MINUS), 
            new KeyCombo("+", VirtualKeyCode.OEM_PLUS), 
            new KeyCombo("win", VirtualKeyCode.LWIN),
            new KeyCombo("enter", VirtualKeyCode.RETURN),
            new KeyCombo("left", VirtualKeyCode.LEFT),
            new KeyCombo("right", VirtualKeyCode.RIGHT),
            new KeyCombo("up", VirtualKeyCode.UP),
            new KeyCombo("down", VirtualKeyCode.DOWN)
        };

        public VirtualKeyCode GetKeyCode(string keyString)
        {
            var toreturnCode =
                from keyStrings in keyCombos
                where keyStrings.Key == keyString
                select keyStrings.KeyCode;
            return toreturnCode.First();
        }
    }

}
