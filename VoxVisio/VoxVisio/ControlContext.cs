using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoxVisio
{
    class ControlContext
    {
        private ControlState _state;

        public ControlContext(ControlState state)
        {
            this._state = state;
        }

        public ControlState ControlState
        {
            get { return _state; }
            set { _state = value; }
        }

        public void EyeRequest(Fixation fixation)
        {
            _state.EyeInput(this, fixation);
        }
        public void VoiceRequest(string voice)
        {
            _state.VoiceInput(voice, this);
        }

    }
}
