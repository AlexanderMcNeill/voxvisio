using System;
using System.Drawing;
using System.Windows.Forms;
using WindowsInput;
using VoxVisio.Properties;
using VoxVisio.Singletons;

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
        //Far into the screen the hotspots show
        private const int HOTSPOTSIZE = 200;

        private Rectangle topHotspot;
        private Rectangle bottomHotspot;
        private eScrollState scrollState;
        private InputSimulator inputSimulator;
        private bool running = false;
        private Bitmap upArrowFocused;
        private Bitmap downArrowFocused;
        private Bitmap upArrow;
        private Bitmap downArrow;
        private OverlayForm overlayForm;


        public ScrollManager()
        {
            overlayForm = SharedFormsSingleton.Instance().overlayForm;;
            inputSimulator = SharedObjectsSingleton.Instance().inputSimulator;
            EventSingleton.Instance().updateTimer.Tick += updateTimer_Tick;

            scrollState = eScrollState.NOSCROLL;

            //Getting the images that will be used to 
            upArrow = new Bitmap(Resources.Arrow);
            upArrow.MakeTransparent();
            downArrow = new Bitmap(Resources.Arrow);
            downArrow.MakeTransparent();
            downArrow.RotateFlip(RotateFlipType.RotateNoneFlipY);

            upArrowFocused = new Bitmap(Resources.ArrowFocused);
            upArrowFocused.MakeTransparent();
            downArrowFocused = new Bitmap(Resources.ArrowFocused);
            downArrowFocused.MakeTransparent();
            downArrowFocused.RotateFlip(RotateFlipType.RotateNoneFlipY);

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

        public bool VoiceInput(string voiceData)
        {
            switch (voiceData)
            { 
                case "start scroll":
                    Start();
                    break;
                case "stop scroll":
                    Stop();
                    break;
                default:
                    return false;
            }

            return true;
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

            switch (scrollState)
            {
                case eScrollState.SCROLLUP:
                    g.DrawImage(upArrowFocused, topHotspot);
                    g.DrawImage(downArrow, bottomHotspot);
                    break;
                case eScrollState.SCROLLDOWN:
                    g.DrawImage(upArrow, topHotspot);
                    g.DrawImage(downArrowFocused, bottomHotspot);
                    break;
                default:
                    g.DrawImage(upArrow, topHotspot);
                    g.DrawImage(downArrow, bottomHotspot);
                    break;
            }
        }
    }
}
