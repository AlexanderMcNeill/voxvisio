using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.Native;
using VoxVisio.Properties;
using VoxVisio.Singletons;

namespace VoxVisio.DictationModes
{
    class DragonDictation : Dictation
    {
        private readonly InputSimulator inputSimulator;
        public DragonDictation()
        {
            inputSimulator = SharedObjectsSingleton.Instance().inputSimulator;
            StartDragon();
        }
        public override void VoiceInput(string input)
        {
            // Do nothing becuase dragon handles the tpying itself
        }

        public override void StartDictation()
        {
           inputSimulator.Keyboard.KeyPress(VirtualKeyCode.NUMPAD0);
        }

        public override void StopDictation()
        {
            inputSimulator.Keyboard.KeyPress(VirtualKeyCode.NUMPAD0);
        }

        public override void Dispose()
        {
            StopDragon();
        }

        private void StartDragon()
        {
            //Starting keyboard if there isn't already a keyboard instance running
            Process[] pname = Process.GetProcessesByName("natspeak");

            if (pname.Length == 0)
            {
                Process.Start(Settings.Default.DragonFileAddress);
            }
        }

        private void StopDragon()
        {
            Process[] procs = null;
            try
            {
                //Getting all processes that match the keyboard process name
                procs = Process.GetProcessesByName(Settings.Default.DragonFileAddress);

                if (procs.Length > 0)
                {
                    //Killing the first process by that name
                    Process keyboardProc = procs[0];

                    if (!keyboardProc.HasExited)
                    {
                        keyboardProc.CloseMainWindow();
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
    }
}
