using System.Linq;
using System.Windows.Forms;
using WindowsInput;
using FMUtils.KeyboardHook;
using VoxVisio.Screen_Overlay;
using VoxVisio.Singletons;

namespace VoxVisio
{
    class CommandState : ControlState
    {
        private IFixationData latestFixation;
        private InputSimulator inputsim;
        private SettingsSingleton _settingsList;
        private ScrollManager scrollManager;
        private ZoomForm zoomForm;

        public CommandState()
        {
            inputsim = SharedObjectsSingleton.Instance().inputSimulator;
            _settingsList = SettingsSingleton.Instance();
            zoomForm = SharedFormsSingleton.Instance().zoomForm;

            scrollManager = new ScrollManager();
            _settingsList.keyboardHook.KeyDownEvent += keyPressedDown;
            //_settingsList.saveCommands();

        }
        private void keyPressedDown(KeyboardHookEventArgs e)
        {
            // Gets the associated command word from the pressed key
            //var firstOrDefault = _settingsList.Commands.FirstOrDefault(x => x.triggerKey == e.Key);
            var firstOrDefault =  _settingsList.Commands.OfType<KeyPressCommand>().FirstOrDefault(x => x.triggerKey == e.Key);
            if (firstOrDefault == null) return;
            string commandWord = firstOrDefault.commandWord;
            //Call the voice input method with the assicated command word
            VoiceInput(commandWord);
        }

        public override void VoiceInput(string voiceData)
        {
            //Getting the latest fixation and converting it to a absolute so the mouse can be moved to it
            double mouseXPos = convertXToAbsolute(latestFixation.GetFixationLocation().X);
            double mouseYPos = convertYToAbsolute(latestFixation.GetFixationLocation().Y);
            inputsim.Mouse.MoveMouseTo(mouseXPos, mouseYPos);

            //Checking all the cases that change state. This is for testing and will be changed in the future
            if (voiceData.Equals("start scroll"))
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
                //Load the command that matches the command word, that isnt a key press command.
                Command commandToFire = _settingsList.Commands.FirstOrDefault(i => i.GetKeyWord() == voiceData && i.GetCommandType() != eCommandType.KeyPressCommand);
                commandToFire.RunCommand();
                
            }
        }

        public override void EyeInput(IFixationData fixation)
        {
            latestFixation = fixation;

            scrollManager.UpdateScroll(fixation.GetFixationLocation());
            zoomForm.Fixation(fixation.GetFixationLocation());
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

        public override void Dispose()
        {
            //Dispose of the scroll manager
        }
    }
}
