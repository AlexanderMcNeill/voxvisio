using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoxVisio
{
    class StandardState : ControlState
    {
        private List<IFixationData> _finishedFixations;
        private IFixationData _currentFixation;
        private int BUFFERSIZE = 8;

        public StandardState()
        {
            _finishedFixations = new List<IFixationData>();
            _currentFixation = null;
        } 

        public override void VoiceInput(string voiceData, ControlContext context)
        {
            //context.ControlState = new DictationState(); //How to swap to dictation mode
        }

        public override void EyeInput(ControlContext context, IFixationData fixation)
        {
            buffering(fixation);
            
        }

        private void buffering(IFixationData fixation)
        {
            switch (fixation.GetFixationPhase())
            {
                case eFixationPhase.start:
                    _currentFixation = fixation;
                    break;
                case eFixationPhase.finished:
                    _currentFixation.setFixationFinished();
                    _finishedFixations.Add(_currentFixation);
                    _currentFixation = null;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            //Remove the oldest fixation if the buffer has reached a certain size
            if (_finishedFixations.Count > BUFFERSIZE)
            {
                _finishedFixations.RemoveAt(0);
            }
        }

        /// <summary>
        /// Return current fixation if it is not null, else returns the most recently finished fixation
        /// </summary>
        /// <returns>The most recent fixation.</returns>
        private IFixationData GetLatestFixation()
        {
            return _currentFixation ?? _finishedFixations.Last();
        }

    }
}
