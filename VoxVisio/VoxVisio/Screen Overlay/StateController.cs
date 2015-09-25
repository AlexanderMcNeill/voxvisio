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
    public enum eState
    { 
        Dictation,
        Command
    }
    class StateController : Overlay
    {
        private const int HOTSPOTSIZE = 100;
        private const int MARGIN = 10;

        private List<StateHotspot> stateHotspots = new List<StateHotspot>();

        private OverlayForm overlayForm;

        private ControlState state;

        public StateController(ControlState state)
        {
            this.state = state;
            int top = Screen.PrimaryScreen.Bounds.Height / 2 - MARGIN / 2 - HOTSPOTSIZE;
            int left = Screen.PrimaryScreen.Bounds.Width - HOTSPOTSIZE;


            Rectangle commandStateHotspot = new Rectangle(left, top, HOTSPOTSIZE, HOTSPOTSIZE);
            Rectangle dictationStateHotspot = new Rectangle(left, top + HOTSPOTSIZE + MARGIN, HOTSPOTSIZE, HOTSPOTSIZE);

            stateHotspots.Add(new StateHotspot("Command", ChangeStateCommand, commandStateHotspot, true));
            stateHotspots.Add(new StateHotspot("Dication", ChangeStateDictation, dictationStateHotspot, false));

            overlayForm = SharedFormsSingleton.Instance().overlayForm;
            overlayForm.RegisterOverlay(this);

            EventSingleton.Instance().updateTimer.Tick += updateTimer_Tick;
        }

        public void ChangeStateCommand()
        {
            state.Dispose();
            state = new CommandState();
            stateHotspots[1].selected = false;
        }

        public void ChangeStateDictation()
        {
            state.Dispose();
            state = new DictationState();
            stateHotspots[0].selected = false;
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            foreach (StateHotspot sh in stateHotspots)
            {
                sh.Update();
            }
        }

        public void Fixation(Point fixation)
        {
            foreach (StateHotspot sh in stateHotspots)
            {
                sh.Fixation(fixation);
            }
        }

        public void Draw(Graphics g)
        {
            foreach (StateHotspot sh in stateHotspots)
            {
                sh.Draw(g);
            }
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
