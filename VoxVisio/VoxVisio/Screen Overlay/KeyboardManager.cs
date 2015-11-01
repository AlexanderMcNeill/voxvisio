using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxVisio.Properties;
using VoxVisio.Singletons;

namespace VoxVisio.Screen_Overlay
{
    public class KeyboardManager
    {
        private const string KEYBOARDPROCESSNAME = "OptiKey";

        public bool VoiceInput(string voiceData)
        {
            if (SettingsSingleton.Instance().OptiKeyEnabled) // HACK : There would be an easier way to do this with a change to the Keyboard manager's interaction structure.
            {
                switch (voiceData)
                {
                    case "start keyboard":
                        StartKeyboard();
                        return true;
                    case "stop keyboard":
                        StopKeyboard();
                        return true;
                    default:
                        return false;
                }
            }
            return false;
        }

        public void StopKeyboard()
        {
            Process[] procs = null;
            try
            {
                //Getting all processes that match the keyboard process name
                procs = Process.GetProcessesByName(KEYBOARDPROCESSNAME);

                if (procs.Length > 0)
                {
                    //Killing the first process by that name
                    Process optikeyProcess = procs[0];

                    if (!optikeyProcess.HasExited)
                    {
                        optikeyProcess.CloseMainWindow();
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

        public void StartKeyboard()
        {
            //Starting keyboard if there isn't already a keyboard instance running
            Process[] pname = Process.GetProcessesByName(KEYBOARDPROCESSNAME);

            if (pname.Length == 0)
            {
                Process.Start(Settings.Default.OptiKeyFileAddress);
            }
        }
    }
}
