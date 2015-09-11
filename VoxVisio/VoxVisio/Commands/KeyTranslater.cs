using System.Linq;
using System.Windows.Forms;
using WindowsInput.Native;

// https://msdn.microsoft.com/en-us/library/windows/desktop/dd375731(v=vs.85).aspx


namespace VoxVisio
{
    /// <summary>
    /// Represents a Virtual keycode and the string used to refer to it.
    /// </summary>
    struct KeyRep
    {
        public string Key;
        public VirtualKeyCode KeyCode;
        //public 
        public KeyRep(string key, VirtualKeyCode keyCode)
        {
            Key = key;
            KeyCode = keyCode;
        }
    }
    public class KeyTranslater
    {
<<<<<<< HEAD
        private static KeyRep[] _keyReps =
        {
            new KeyRep("m1", VirtualKeyCode.LBUTTON),
            new KeyRep("m2", VirtualKeyCode.RBUTTON),
            new KeyRep("m3", VirtualKeyCode.MBUTTON),
            new KeyRep("f1", VirtualKeyCode.F1),
            new KeyRep("f2", VirtualKeyCode.F2),
            new KeyRep("f3", VirtualKeyCode.F3),
            new KeyRep("f4", VirtualKeyCode.F4),
            new KeyRep("f5", VirtualKeyCode.F5),
            new KeyRep("f6", VirtualKeyCode.F6),
            new KeyRep("f7", VirtualKeyCode.F7),
            new KeyRep("f8", VirtualKeyCode.F8),
            new KeyRep("f9", VirtualKeyCode.F9),
            new KeyRep("f10", VirtualKeyCode.F10),
            new KeyRep("f11", VirtualKeyCode.F11),
            new KeyRep("f12", VirtualKeyCode.F12),
            new KeyRep("tab", VirtualKeyCode.TAB),
            new KeyRep("ctrl", VirtualKeyCode.CONTROL),
            new KeyRep("alt", VirtualKeyCode.MENU),
            new KeyRep("a", VirtualKeyCode.VK_A), 
            new KeyRep("b", VirtualKeyCode.VK_B), 
            new KeyRep("c", VirtualKeyCode.VK_C), 
            new KeyRep("d", VirtualKeyCode.VK_D), 
            new KeyRep("e", VirtualKeyCode.VK_E), 
            new KeyRep("f", VirtualKeyCode.VK_F), 
            new KeyRep("g", VirtualKeyCode.VK_G), 
            new KeyRep("h", VirtualKeyCode.VK_H), 
            new KeyRep("i", VirtualKeyCode.VK_I), 
            new KeyRep("j", VirtualKeyCode.VK_J), 
            new KeyRep("k", VirtualKeyCode.VK_K), 
            new KeyRep("l", VirtualKeyCode.VK_L), 
            new KeyRep("m", VirtualKeyCode.VK_M), 
            new KeyRep("n", VirtualKeyCode.VK_N), 
            new KeyRep("o", VirtualKeyCode.VK_O), 
            new KeyRep("p", VirtualKeyCode.VK_P), 
            new KeyRep("q", VirtualKeyCode.VK_Q), 
            new KeyRep("r", VirtualKeyCode.VK_R), 
            new KeyRep("s", VirtualKeyCode.VK_S), 
            new KeyRep("t", VirtualKeyCode.VK_T), 
            new KeyRep("u", VirtualKeyCode.VK_U), 
            new KeyRep("v", VirtualKeyCode.VK_V), 
            new KeyRep("w", VirtualKeyCode.VK_W), 
            new KeyRep("x", VirtualKeyCode.VK_X), 
            new KeyRep("y", VirtualKeyCode.VK_Y), 
            new KeyRep("z", VirtualKeyCode.VK_X), 
            new KeyRep("1", VirtualKeyCode.VK_1), 
            new KeyRep("2", VirtualKeyCode.VK_2), 
            new KeyRep("3", VirtualKeyCode.VK_3), 
            new KeyRep("4", VirtualKeyCode.VK_4), 
            new KeyRep("5", VirtualKeyCode.VK_5), 
            new KeyRep("6", VirtualKeyCode.VK_6), 
            new KeyRep("7", VirtualKeyCode.VK_7), 
            new KeyRep("8", VirtualKeyCode.VK_8), 
            new KeyRep("9", VirtualKeyCode.VK_9), 
            new KeyRep("0", VirtualKeyCode.VK_0), 
            new KeyRep("-", VirtualKeyCode.OEM_MINUS), 
            new KeyRep("+", VirtualKeyCode.OEM_PLUS), 
            new KeyRep("win", VirtualKeyCode.LWIN),
            new KeyRep("enter", VirtualKeyCode.RETURN),
            new KeyRep("left", VirtualKeyCode.LEFT),
            new KeyRep("right", VirtualKeyCode.RIGHT),
            new KeyRep("up", VirtualKeyCode.UP),
            new KeyRep("down", VirtualKeyCode.DOWN),
            new KeyRep("esc", VirtualKeyCode.ESCAPE),
            new KeyRep("eyexclick", VirtualKeyCode.LCONTROL),
            new KeyRep("shift", VirtualKeyCode.SHIFT),
            new KeyRep("del", VirtualKeyCode.DELETE)
        };
=======
        private static KeysConverter kc = new KeysConverter();
>>>>>>> edfa7cf087b8ae3a817562c5fa9c40802e78e1a8

        public static VirtualKeyCode GetKeyCode(string keyString)
        {            
            var toReturn = kc.ConvertFromString(keyString);
            return (VirtualKeyCode)toReturn;
        }
        public static string GetKeyString(VirtualKeyCode code)
        {      
            var toreturnCode = kc.ConvertToString((Keys)code);                
            return toreturnCode;
        }
    }

}
