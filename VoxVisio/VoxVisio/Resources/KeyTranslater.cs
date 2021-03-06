﻿using System.Windows.Forms;
using WindowsInput.Native;

// https://msdn.microsoft.com/en-us/library/windows/desktop/dd375731(v=vs.85).aspx


namespace VoxVisio.Resources
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

    /// <summary>
    /// The KeyTranslator class is used to convert VirtualKeyCodes to and from their string representations, and is refered to statically.
    /// </summary>
    public class KeyTranslater
    {
        
        private static KeysConverter kc = new KeysConverter();

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
