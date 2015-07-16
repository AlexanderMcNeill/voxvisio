using System;
using System.Collections.Generic;
using System.Linq;
using WindowsInput;

namespace VoxVisio
{
    class StandardState : ControlState
    {
        private List<IFixationData> _finishedFixations;
        private IFixationData _currentFixation;
        private int BUFFERSIZE = 8;
        private InputSimulator inputsim;
        private CommandSingleton commandList;

        public StandardState(InputSimulator inputsim)
        {
            _finishedFixations = new List<IFixationData>();
            _currentFixation = null;
            this.inputsim = inputsim;
            commandList = CommandSingleton.Instance();
        }

        public override void VoiceInput(string voiceData, ControlContext context)
        {
            if (voiceData.Equals("dictation"))
            {
                context.ControlState = new DictationState(inputsim);
            }
            else
            {
                inputsim.Mouse.MoveMouseTo(GetLatestFixation().GetFixationLocation().X,
                    GetLatestFixation().GetFixationLocation().Y);
                commandList.Commands.Find(i => i.VoiceKeyword == voiceData).keyCombo.PressKeys();
            }
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
                    if (_currentFixation != null)
                    {
                        _currentFixation.setFixationFinished();
                        _finishedFixations.Add(_currentFixation);
                        _currentFixation = null;
                    }
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
