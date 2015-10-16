using System;
using System.Drawing;
using System.Windows.Forms;
using WindowsInput;
using VoxVisio.Properties;
using VoxVisio.Singletons;
using System.Resources;

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
        private Point lastFixation;


        public ScrollManager()
        {
            overlayForm = SharedFormsSingleton.Instance().overlayForm;;
            inputSimulator = SharedObjectsSingleton.Instance().inputSimulator;
            EventSingleton.Instance().updateTimer.Tick += updateTimer_Tick;

            scrollState = eScrollState.NOSCROLL;

            //Getting the images that will be used to 
            upArrow = new Bitmap(Properties.Resources.Arrow);
            upArrow.MakeTransparent();
            downArrow = new Bitmap(Properties.Resources.Arrow);
            downArrow.MakeTransparent();
            downArrow.RotateFlip(RotateFlipType.RotateNoneFlipY);

            upArrowFocused = new Bitmap(Properties.Resources.ArrowFocused);
            upArrowFocused.MakeTransparent();
            downArrowFocused = new Bitmap(Properties.Resources.ArrowFocused);
            downArrowFocused.MakeTransparent();
            downArrowFocused.RotateFlip(RotateFlipType.RotateNoneFlipY);

            lastFixation = new Point(0, 0);

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
                case "scroll":
                    double mouseXPos = convertXToAbsolute(lastFixation.X);
                    double mouseYPos = convertYToAbsolute(lastFixation.Y);
                    inputSimulator.Mouse.MoveMouseTo(mouseXPos, mouseYPos);
                    inputSimulator.Mouse.LeftButtonClick();
                    if (lastFixation.Y > Screen.PrimaryScreen.Bounds.Height / 2)
                    {
                        inputSimulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.NEXT);
                    }
                    else
                    {
                        inputSimulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.PRIOR);
                    }
                    break;
                default:
                    return false;
            }

            return true;
        }

        public void UpdateScroll(Point fixation)
        {
            lastFixation = fixation;

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
    }
}
