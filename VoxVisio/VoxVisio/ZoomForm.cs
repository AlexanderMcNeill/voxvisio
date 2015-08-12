using EyeXFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tobii.EyeX.Framework;
using WindowsInput;

namespace VoxVisio
{
    public partial class ZoomForm : Form
    {
        private const int MAXZOOM = 3;
        private const int ZOOMTIME = 1;
        private Graphics g;
        private Bitmap bmp;
        private Graphics formGraphics;
        private int zoomCounter;
        private readonly InputSimulator inputSim;
        private KeyCombo inputKeys;
        private EyeXHost eyex;
        private Fixation fx;
        private Rectangle zoomRect;
        public ZoomForm(InputSimulator inputSim)
        {
            InitializeComponent();

            formGraphics = panel1.CreateGraphics();
            bmp = new Bitmap(Width, Height);
            g = Graphics.FromImage(bmp);

            eyex = new EyeXHost();
            eyex.CreateFixationDataStream(FixationDataMode.Sensitive).Next += (s, e) => Fixation(e.EventType, (int)e.X, (int)e.Y, e.Timestamp);
            eyex.Start();
            this.inputSim = inputSim;
            this.Visible = false;
        }

        public void Fixation(FixationDataEventType t, int x, int y, double timeStamp)
        {
            fx = new Fixation(new Point(x, y), eFixationPhase.finished);
        }

        public void DrawScreen()
        {

            formGraphics.DrawImage(bmp, zoomRect.X, zoomRect.Y, zoomRect.Width, zoomRect.Height);

        }

        public void startZoomClick(KeyCombo inputKeys)
        {
                
                int xPos = MousePosition.X - (Width / 2);
                int yPos = MousePosition.Y - (Height / 2);
                Top = yPos;
                Left = xPos;

                g.CopyFromScreen(Left, Top, 0, 0, new Size(Width, Height));

                int zoomWidth = bmp.Width * MAXZOOM;
                int zoomHeight = bmp.Height * MAXZOOM;

                int zoomXPos = -((zoomWidth - bmp.Width) / 2);
                int zoomYPos = -((zoomHeight - bmp.Height) / 2);

                zoomRect = new Rectangle(zoomXPos, zoomYPos, zoomWidth, zoomHeight);

                zoomCounter = 0;

                this.inputKeys = inputKeys;

                this.Visible = true;
                DrawScreen();
                zoomTimer.Start();

        }

        public void endZoomClick()
        {
            this.Visible = false;

            int zoomWidth = bmp.Width * MAXZOOM;
            int zoomHeight = bmp.Height * MAXZOOM;

            int borderX = (zoomWidth - bmp.Width) / 2;
            int borderY = (zoomHeight - bmp.Height) / 2;

            if (fx != null)
            {
                int mouseOnFormX = fx.GetFixationLocation().X - Left;
                int mouseOnFormY = fx.GetFixationLocation().Y - Top;

                Point mousePos = new Point(((mouseOnFormX + borderX) / MAXZOOM) + Left, ((mouseOnFormY + borderY) / MAXZOOM) + Top);

                ClickPoint(mousePos);
            }

            this.Hide();
        }

        private void ClickPoint(Point mousePos)
        {
            double mouseXPos = convertXToAbsolute(mousePos.X);
            double mouseYPos = convertYToAbsolute(mousePos.Y);
            inputSim.Mouse.MoveMouseTo(mouseXPos, mouseYPos);

            inputKeys.PressKeys();
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
