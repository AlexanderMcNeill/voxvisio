using System.Linq;
using System.Windows.Forms;
using WindowsInput;
using VoxVisio.Screen_Overlay;
using VoxVisio.Singletons;
using VoxVisio.Commands;
using System;
using System.Collections.Generic;
using VoxVisio.UI;

namespace VoxVisio.States
{
    class CommandState : ControlState
    {
        public const string GRAMMARNAME = "CommandGrammar";

        private IFixationData latestFixation;
        private InputSimulator inputsim;
        private List<Command> commandList;
        private ScrollManager scrollManager;
        private ZoomForm zoomForm;
        private KeyboardManager keyboardManager;
        private Toast toastOverlay;

        public CommandState()
        {
            inputsim = SharedObjectsSingleton.Instance().inputSimulator;
            commandList = SettingsSingleton.Instance().Commands;
            zoomForm = SharedFormsSingleton.Instance().zoomForm;

            scrollManager = new ScrollManager();
            keyboardManager = new KeyboardManager();
            toastOverlay = SharedFormsSingleton.Instance().ToastOverlay;
        }

        public override void VoiceInput(string voiceData, string grammarName)
        {
            if (grammarName.Equals(GRAMMARNAME))
            {
                //Getting the latest fixation and converting it to a absolute so the mouse can be moved to it
                double mouseXPos = convertXToAbsolute(latestFixation.GetFixationLocation().X);
                double mouseYPos = convertYToAbsolute(latestFixation.GetFixationLocation().Y);
                inputsim.Mouse.MoveMouseTo(mouseXPos, mouseYPos);

                //Running a normal voice command
                if (!scrollManager.VoiceInput(voiceData) && !keyboardManager.VoiceInput(voiceData))
                {
                    //Load the command that matches the command word, that isnt a key press command.
                    Command commandToFire = commandList.FirstOrDefault(i => i.GetKeyWord() == voiceData && i.GetCommandType() != eCommandType.KeyPressCommand);
                    commandToFire?.RunCommand();

                }
            }
            else 
            {
                toastOverlay.NewMessage("Sorry, I didn't catch that.\nPlease try again.");
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

        public override void KeyboardInput(Keys keyPressed)
        {
            // Gets the associated command word from the pressed key
            var firstOrDefault = commandList.OfType<KeyPressCommand>().FirstOrDefault(x => x.triggerKey == keyPressed);
            if (firstOrDefault == null) return;
            string commandWord = firstOrDefault.commandWord;

            //Call the voice input method with the assicated command word
            VoiceInput(commandWord, GRAMMARNAME);
        }

        public override void Start()
        {
            
        }

        public override void Stop()
        {
            scrollManager.Stop();
            keyboardManager.StopKeyboard();
        }
    }
}
