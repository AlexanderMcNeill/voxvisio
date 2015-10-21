using System;
using System.Drawing;
using System.Windows.Forms;
using WindowsInput;
using VoxVisio.Singletons;

namespace VoxVisio
{
    public partial class ZoomForm : Form
    {
        private const int ZOOMTIME = 1;
        private Graphics g;
        private Bitmap bmp;
        private Graphics formGraphics;
        private int zoomCounter;
        private readonly InputSimulator inputSim;
        private KeyCombo inputCommand;
        private Point fx;
        private Rectangle zoomRect;
        private Point formOffset;
        private bool running = false;
        public ZoomForm()
        {
            InitializeComponent();
            formGraphics = panel1.CreateGraphics();
            bmp = new Bitmap(Width, Height);
            g = Graphics.FromImage(bmp);

            //Getting the input simulator from the shared objects
            this.inputSim = SharedObjectsSingleton.Instance().inputSimulator;
            this.Visible = false;
        }

        public void Fixation(Point fixationLocation)
        {
            fx = fixationLocation;
            /*
            if (!Bounds.Contains(fx) && running)
            {
                zoomTimer.Stop();
                running = false;
                Invoke(new Action(() => this.Hide()));
            }
             */
        }

        public void DrawScreen()
        {
            formGraphics.DrawImage(bmp, zoomRect.X, zoomRect.Y, zoomRect.Width, zoomRect.Height);
        }

        public void startZoomClick(KeyCombo inputCommand)
        {
            if(!running)
            {
            running = true;
                //this.ClientSize = SettingsSingleton.Instance().ZoomFormSize;
                int xPos = MousePosition.X - (Width / 2);
                int yPos = MousePosition.Y - (Height / 2);
                GetFormPos(xPos, yPos);

                g.FillRectangle(Brushes.Black, Bounds);
                g.CopyFromScreen(xPos, yPos, 0, 0, new Size(Width, Height));
                
                int zoomWidth = (int)(bmp.Width * 3);
                int zoomHeight = (int)(bmp.Height * 3);

                int zoomXPos = -((zoomWidth - bmp.Width) / 2);
                int zoomYPos = -((zoomHeight - bmp.Height) / 2);

                zoomRect = new Rectangle(zoomXPos, zoomYPos, zoomWidth, zoomHeight);

                zoomCounter = 0;

                this.inputCommand = inputCommand;

                this.Visible = true;
                DrawScreen();
                zoomTimer.Start();
            }
        }

        public void GetFormPos(int xPos, int yPos)
        {
            formOffset = new Point();
            if (xPos < 0)
            {
                formOffset.X = xPos;
                Left = 0;
            }
            else if (xPos + Width > Screen.PrimaryScreen.Bounds.Width)
            {
                formOffset.X = (xPos + Width) - Screen.PrimaryScreen.Bounds.Width;
                Left = Screen.PrimaryScreen.Bounds.Width - Width;
            }
            else
            {
                formOffset.X = 0;
                Left = xPos;
            }

            if (yPos < 0)
            {
                formOffset.Y = yPos;
                Top = 0;
            }
            else if (yPos + Height > Screen.PrimaryScreen.Bounds.Height)
            {
                formOffset.Y = (yPos + Height) - Screen.PrimaryScreen.Bounds.Height;
                Top = Screen.PrimaryScreen.Bounds.Height - Height;
            }
            else
            {
                formOffset.Y = 0;
                Top = yPos;
            }
        }

        public void endZoomClick()
        {
            this.Visible = false;

            int zoomWidth = (int)(bmp.Width * SettingsSingleton.Instance().ZoomMagnification);
            int zoomHeight = (int)(bmp.Height * SettingsSingleton.Instance().ZoomMagnification);

            int borderX = (zoomWidth - bmp.Width) / 2;
            int borderY = (zoomHeight - bmp.Height) / 2;

            if (fx != null)
            {
                int mouseOnFormX = fx.X - Left;
                int mouseOnFormY = fx.Y - Top;

                Point mousePos = new Point((int)(((mouseOnFormX + borderX) / SettingsSingleton.Instance().ZoomMagnification) + Left), (int)(((mouseOnFormY + borderY) / SettingsSingleton.Instance().ZoomMagnification) + Top));

                mousePos.X += formOffset.X;
                mousePos.Y += formOffset.Y;

                ClickPoint(mousePos);
            }
            running = false;
            this.Hide();
        }

        private void ClickPoint(Point mousePos)
        {
            double mouseXPos = convertXToAbsolute(mousePos.X);
            double mouseYPos = convertYToAbsolute(mousePos.Y);
            inputSim.Mouse.MoveMouseTo(mouseXPos, mouseYPos);

            inputCommand.PressKeys();
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

        private void zoomTimer_Tick(object sender, EventArgs e)
        {
            DrawScreen();
            if (zoomCounter == ZOOMTIME)
            {
                zoomTimer.Stop();
                endZoomClick();
            }
            else
            {
                zoomCounter++;
            }

        }
    }
}
