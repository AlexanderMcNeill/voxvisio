using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput;
using VoxVisio.Singletons;

namespace VoxVisio.DictationModes
{
    class WindowsDictation : Dictation
    {
        private readonly InputSimulator inputSimulator;
        public WindowsDictation()
        {
            inputSimulator = SharedObjectsSingleton.Instance().inputSimulator;
        }
        public override void VoiceInput(string input)
        {
            inputSimulator.Keyboard.TextEntry(input);
        }

        public override void StartDictation()
        {
            
        }

        public override void StopDictation()
        {
            
        }

        public override void Dispose()
        {
            
        }
    }
}
