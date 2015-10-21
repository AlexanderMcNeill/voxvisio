using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoxVisio.Screen_Overlay
{
    public class KeyboardManager
    {
        private const string KEYBOARDPROCESSNAME = "OptiKey";
        private const string KEYBOARDPROGRAMPATH = "C:/Program Files (x86)/OptiKey/OptiKey.exe";

        public bool VoiceInput(string voiceData)
        {
            switch (voiceData)
            {
                case "start keyboard":
                    startKeyboard();
                    return true;
                case "stop keyboard":
                    killKeyboard();
                    return true;
                default:
                    return false;
            }
        }

        private void killKeyboard()
        {
            Process[] procs = null;

            try
            {
                //Getting all processes that match the keyboard process name
                procs = Process.GetProcessesByName(KEYBOARDPROCESSNAME);

                if (procs.Length > 0)
                {
                    //Killing the first process by that name
                    Process mspaintProc = procs[0];

                    if (!mspaintProc.HasExited)
                    {
                        mspaintProc.CloseMainWindow();
                    }
                }
                
            }
            finally
            {
                if (procs != null)
                {
                    foreach (Process p in procs)
                    {
                        p.Dispose();
                    }
                }
            }
        }

        private void startKeyboard()
        {
            //Starting keyboard if there isn't already a keyboard instance running
            Process[] pname = Process.GetProcessesByName(KEYBOARDPROCESSNAME);

            if (pname.Length == 0)
            {
                Process.Start(KEYBOARDPROGRAMPATH);
            }
        }
    }
}
