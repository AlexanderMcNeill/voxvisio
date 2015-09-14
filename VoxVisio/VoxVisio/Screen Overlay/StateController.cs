using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VoxVisio.Singletons;

namespace VoxVisio.Screen_Overlay
{
    class StateController : Overlay
    {
        private const int HOTSPOTSIZE = 100;
        private const int MARGIN = 10;

        private Rectangle commandStateHotspot;
        private int commandStateCounter = 0;
        private bool commandStateFocused = false;

        private Rectangle dictationStateHotspot;
        private int dictationStateCounter = 0;
        private bool dictationStateFocused = false;

        private OverlayForm overlayForm;

        private ControlState state;

        public StateController(ControlState state)
        {
            this.state = state;

            int top = Screen.PrimaryScreen.Bounds.Height / 2 - MARGIN / 2 - HOTSPOTSIZE;
            int left = Screen.PrimaryScreen.Bounds.Width - HOTSPOTSIZE;

            commandStateHotspot = new Rectangle(left, top, HOTSPOTSIZE, HOTSPOTSIZE);
            dictationStateHotspot = new Rectangle(left, top + HOTSPOTSIZE + MARGIN, HOTSPOTSIZE, HOTSPOTSIZE);

            overlayForm = SharedFormsSingleton.Instance().overlayForm;
            overlayForm.RegisterOverlay(this);

            EventSingleton.Instance().updateTimer.Tick += updateTimer_Tick;
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            if (commandStateFocused)
            {
                if (commandStateCounter < 100)
                {
                    commandStateCounter += 10;
                }
                else
                {
                    dictationStateCounter = 0;
                    commandStateCounter = 0;
                    state.Dispose();
                    state = new CommandState();
                }
            }
            else if (commandStateCounter > 0)
            {
                commandStateCounter -= 10;
            }

            if (dictationStateFocused)
            {
                if (dictationStateCounter < 100)
                {
                    dictationStateCounter += 10;
                }
                else
                {
                    dictationStateCounter = 0;
                    commandStateCounter = 0;

                    state.Dispose();
                    state = new DictationState();
                }
            }
            else if (dictationStateCounter > 0)
            {
                dictationStateCounter -= 10;
            }
        }

        public void Fixation(Point fixation)
        {
            commandStateFocused = commandStateHotspot.Contains(fixation);
            dictationStateFocused = dictationStateHotspot.Contains(fixation);
        }

        public void Draw(Graphics g)
        {
            DrawHotspot(g, commandStateHotspot, commandStateCounter);
            DrawHotspot(g, dictationStateHotspot, dictationStateCounter);
        }

        public void DrawHotspot(Graphics g, Rectangle hotspotRect, int percentFill)
        {
            g.FillEllipse(Brushes.Blue, hotspotRect);

            //Getting how big the progress circle will be
            int fillWidth = (hotspotRect.Width / 100) * percentFill;
            int fillHeight = (hotspotRect.Width / 100) * percentFill;

            //Make the progress circle centered to the hotspot rectangle
            int xPos = (hotspotRect.Width - fillWidth) / 2 + hotspotRect.X;
            int yPos = (hotspotRect.Width - fillHeight) / 2 + hotspotRect.Y;
            g.FillEllipse(Brushes.Red, xPos, yPos, fillWidth, fillHeight);
        }
    }
}
