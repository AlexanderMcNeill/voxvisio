namespace VoxVisio
{
    public delegate void StateChanged();
    class ControlContext
    {
        private ControlState _state;
        public event StateChanged changedState;

        public ControlContext()
        {
            
        }

        public ControlState ControlState
        {
            get { return _state; }
            set
            {
                if (_state != value)
                {
                    _state = value;
                    changedState();
                }
            }
        }

        public void EyeRequest(Fixation fixation)
        {
            _state.EyeInput(fixation);
        }
        public void VoiceRequest(string voice)
        {
            _state.VoiceInput(voice);
        }

    }
}
