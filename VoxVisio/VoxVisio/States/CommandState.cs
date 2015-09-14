using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VoxVisio.Screen_Overlay;
using WindowsInput;
using FMUtils.KeyboardHook;

namespace VoxVisio
{
    class CommandState : ControlState
    {
        private List<IFixationData> _finishedFixations;
        private IFixationData _currentFixation;
        private int BUFFERSIZE = 8;
        private InputSimulator inputsim;
        private SettingsSingleton _settingsList;

        private ScrollManager scrollManager;
        private ZoomForm zoomForm;

        private ControlContext context;

        public CommandState(InputSimulator inputsim, ControlContext context)
        {
            _finishedFixations = new List<IFixationData>();
            _currentFixation = null;
            this.inputsim = inputsim;
            _settingsList = SettingsSingleton.Instance();
            this.context = context;
            this.zoomForm = SharedDataSingleton.Instance().zoomForm;

            scrollManager = new ScrollManager();
            _settingsList.keyboardHook.KeyDownEvent += keyPressedDown;
            _settingsList.specialCommands.Add(new SpecialCommand("click", Keys.F12));

        }
        private void keyPressedDown(KeyboardHookEventArgs e)
        {
            // Gets the associated command word from the pressed key
            var firstOrDefault = _settingsList.specialCommands.FirstOrDefault(x => x.triggerKey == e.Key);
            if (firstOrDefault == null) return;
            string commandWord = firstOrDefault.commandWord;
            //Call the voice input method with the assicated command word
            VoiceInput(commandWord);
        }

        public void ZoomCLick()
        {
            KeyCombo keyCombo = _settingsList.Commands.Find(i => i.VoiceKeyword == "click").keyCombo;
            zoomForm.startZoomClick(keyCombo);
        }

        public override void VoiceInput(string voiceData)
        {
            //Getting the latest fixation and converting it to a absolute so the mouse can be moved to it
            IFixationData latestFixation = GetLatestFixation();
            double mouseXPos = convertXToAbsolute(latestFixation.GetFixationLocation().X);
            double mouseYPos = convertYToAbsolute(latestFixation.GetFixationLocation().Y);
            inputsim.Mouse.MoveMouseTo(mouseXPos, mouseYPos);

            //Checking all the cases that change state. This is for testing and will be changed in the future
            if (voiceData.Equals("start dictation"))
            {
                context.ControlState = new DictationState(inputsim, context);
            }
            else if (voiceData.Equals("start scroll"))
            {
                scrollManager.Start();
            }
            else if (voiceData.Equals("stop scroll"))
            {
                scrollManager.Stop();
            }
            //Running a normal voice command
            else
            {


                //Firing the command
                KeyCombo keyCombo = _settingsList.Commands.Find(i => i.VoiceKeyword == voiceData).keyCombo;

                if (keyCombo.Keys.Contains(WindowsInput.Native.VirtualKeyCode.LBUTTON) || keyCombo.Keys.Contains(WindowsInput.Native.VirtualKeyCode.RBUTTON))
                {
                    zoomForm.startZoomClick(keyCombo);
                }
                else
                {
                    keyCombo.PressKeys();
                }
            }
        }

        public override void EyeInput(IFixationData fixation)
        {

            //Buffering the fixation data for later commands
            buffering(fixation);

            scrollManager.UpdateScroll(fixation.GetFixationLocation());
            zoomForm.Fixation(fixation.GetFixationLocation());
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
