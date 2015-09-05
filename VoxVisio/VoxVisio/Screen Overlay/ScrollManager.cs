using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsInput;

namespace VoxVisio.Screen_Overlay
{
    public enum eScrollState
    {
        SCROLLUP,
        SCROLLDOWN,
        NOSCROLL
    }

    class ScrollManager : Overlay
    {
        private const int HOTSPOTSIZE = 200;

        private Rectangle topHotspot;
        private Rectangle bottomHotspot;
        private eScrollState scrollState;
        private InputSimulator inputSimulator;
        private bool running = false;

        private OverlayForm overlayForm;


        public ScrollManager()
        {
            SharedDataSingleton sharedData = SharedDataSingleton.Instance();
            overlayForm = sharedData.overlayForm;
            inputSimulator = sharedData.inputSimulator;
            sharedData.updateTimer.Tick += updateTimer_Tick;

            scrollState = eScrollState.NOSCROLL;

            setupHotspots();
        }

        void updateTimer_Tick(object sender, EventArgs e)
        {
            if (running)
            {
                RunScroll();
            }
        }

        //Creating the hotspots that allow a user to scroll up and down
        private void setupHotspots()
        {
            //Getting the screen size
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;

            topHotspot = new Rectangle(0, 0, screenWidth, HOTSPOTSIZE);
            bottomHotspot = new Rectangle(0, screenHeight - HOTSPOTSIZE, screenWidth, HOTSPOTSIZE);
        }

        public void RunScroll()
        {
            
                switch (scrollState)
                {
                    case eScrollState.SCROLLUP:
                        inputSimulator.Mouse.VerticalScroll(1);
                        break;
                    case eScrollState.SCROLLDOWN:
                        inputSimulator.Mouse.VerticalScroll(-1);
                        break;
                }
        }

        public void Start()
        {
            overlayForm.RegisterOverlay(this);
            running = true;
        }

        public void Stop()
        {
            overlayForm.RemoveOverlay(this);
            running = false;
        }

        public void UpdateScroll(Point fixation)
        {
            //Checking if the fixation falls in one of the hotspots and changing to the 
            //corrosponding scroll state if it does
            if (topHotspot.Contains(fixation))
            {
                scrollState = eScrollState.SCROLLUP;
            }
            else if (bottomHotspot.Contains(fixation))
            {
                scrollState = eScrollState.SCROLLDOWN;
            }
            else
            {
                scrollState = eScrollState.NOSCROLL;
            }
        }

        public void Draw(Graphics g)
        {
            g.FillRectangle(Brushes.Blue, topHotspot);
            g.FillRectangle(Brushes.Blue, bottomHotspot);
        }
    }
}
