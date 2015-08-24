using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WindowsInput;

namespace VoxVisio
{
    class CommandState : ControlState
    {
        private List<IFixationData> _finishedFixations;
        private IFixationData _currentFixation;
        private int BUFFERSIZE = 8;
        private InputSimulator inputsim;
        private SettingsSingleton _settings;

        //Hotspots from scrolling up and down
        private Rectangle upScrollHotspot;
        private Rectangle downScrollHotspot;

        private bool inScrollState = false;

        public CommandState(InputSimulator inputsim)
        {
            _finishedFixations = new List<IFixationData>();
            _currentFixation = null;
            this.inputsim = inputsim;
            _settings = SettingsSingleton.Instance();

            //Creating the hotspots that allow a user to scroll up and down
            upScrollHotspot = new Rectangle(0, -100, Screen.PrimaryScreen.Bounds.Width, 100);
            downScrollHotspot = new Rectangle(0, Screen.PrimaryScreen.Bounds.Height, Screen.PrimaryScreen.Bounds.Width, 100);
        }

        public override void VoiceInput(string voiceData, ControlContext context)
        {
            //Checking all the cases that change state. This is for testing and will be changed in the future
            if (voiceData.Equals("dictation"))
            {
                context.ControlState = new DictationState(inputsim);
            }
            else if (voiceData.Equals("scroll"))
            {
                inScrollState = true;
            }
            else if (voiceData.Equals("exit scroll"))
            {
                inScrollState = false;
            }
            //Running a normal voice command
            else
            {
                //Getting the latest fixation and converting it to a absolute so the mouse can be moved to it
                IFixationData latestFixation = GetLatestFixation();
                double mouseXPos = convertXToAbsolute(latestFixation.GetFixationLocation().X);
                double mouseYPos = convertYToAbsolute(latestFixation.GetFixationLocation().Y);
                inputsim.Mouse.MoveMouseTo(mouseXPos, mouseYPos);

                //Firing the command
                _settings.Commands.Find(i => i.VoiceKeyword == voiceData).keyCombo.PressKeys();
            }
        }

        public override void EyeInput(ControlContext context, IFixationData fixation)
        {
            //If in the scroll state checking if the user is looking at one of the hotspots and scolling appropriately
            if (inScrollState)
            {
                if (upScrollHotspot.Contains(fixation.GetFixationLocation()))
                {
                    inputsim.Mouse.VerticalScroll(1);
                }
                else if (downScrollHotspot.Contains(fixation.GetFixationLocation()))
                {
                    inputsim.Mouse.VerticalScroll(-1);
                }
            }

            //Buffering the fixation data for later commands
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

        //Method for converting the X position in pixels to the absolute number needed from the input simulator
        private double convertXToAbsolute(int x)
        {
            return ((double)65535 * x) / (double)Screen.PrimaryScreen.Bounds.Width;
        }

        //Method for converting the Y position in pixels to the absolute number needed from the input simulator
        private double convertYToAbsolute(int y)
        {
            return ((double)65535 * y) / (double)Screen.PrimaryScreen.Bounds.Height;
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
