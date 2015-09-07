using System;
using System.Drawing;
using VoxVisio.Screen_Overlay;
using WindowsInput;

namespace VoxVisio
{
    class DictationState : ControlState
    {

        private InputSimulator inputsim;
        private Hotspot closeHotspot;
        private ControlContext context;
        public DictationState(InputSimulator inputsim, ControlContext context)
        {
            this.inputsim = inputsim;
           
            this.context = context;
            closeHotspot = new Hotspot(new Rectangle(0, 0, 100, 100), HotspotCallback);

            inputsim.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.NUMPAD0);
        }

        public override void VoiceInput(string voiceData)
        {
            
        }

        public void HotspotCallback()
        {
            context.ControlState = new CommandState(inputsim, context);
            inputsim.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.NUMPAD0);
        }

        public override void EyeInput(IFixationData fixation)
        {
            closeHotspot.update(fixation.GetFixationLocation());
        }
    }
}
