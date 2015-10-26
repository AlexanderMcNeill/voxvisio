using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoxVisio.DictationModes
{
    abstract class Dictation : IDisposable
    {
        public abstract void VoiceInput(string input);
        public abstract void StartDictation();
        public abstract void StopDictation();
        public abstract void Dispose();
    }
}
