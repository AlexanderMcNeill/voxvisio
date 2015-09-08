using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsInput;

namespace ZoomFormTest
{
    public partial class Form1 : Form
    {
        //How far the program will zoom
        private const int MAXZOOM = 3;
        //The delay till the click is fired
        private const int ZOOMTIME = 2;

        //The bitmap and graphics for taking the screen shot for the zoom
        private Graphics zoomGraphics;
        private Bitmap zoomBmp;

        //The forms graphics for displaying the zoomed image
        private Graphics formGraphics;

        //The input simulator for simulating the click
        private InputSimulator inputSim; 

        //The top left of where the program is zooming
        private Rectangle zoomLocation;

        //The top left of where the form will be displayed

        private int zoomCounter = 0;
        public Form1()
        {
            InitializeComponent();

            //Setting up the graphics objects
            formGraphics = panel1.CreateGraphics();
            zoomBmp = new Bitmap(Width, Height); 
            zoomGraphics = Graphics.FromImage(zoomBmp);

            zoomLocation = getZoomLocation(MousePosition);
            zoomGraphics.CopyFromScreen(zoomLocation.Location, new Point(0, 0), zoomLocation.Size);
            Point formPos = getFormPosition(zoomLocation, Screen.PrimaryScreen.Bounds);
            Left = formPos.X;
            Top = formPos.Y;

            inputSim = new InputSimulator();
        }

        //Method that checks if the zoom bounds is off the screen setting the form to display it on the screen
        public Point getFormPosition(Rectangle zoomLocation, Rectangle screenBounds)
        {
            Point formPos = new Point();

            //Setting x position.
            if (zoomLocation.Left < 0)
            {
                formPos.X = 0;
            }
            else if (zoomLocation.Right > screenBounds.Width)
            {
                formPos.X = screenBounds.Width - zoomLocation.Width;
            }
            else
            {
                formPos.X = zoomLocation.X;
            }

            //Setting y position
            if (zoomLocation.Top < 0)
            {
                formPos.Y = 0;
            }
            else if (zoomLocation.Bottom > screenBounds.Height)
            {
                formPos.Y = screenBounds.Height - zoomLocation.Height;
            }
            else
            {
                formPos.Y = zoomLocation.Y;
            }

            return formPos;
        }

        public Rectangle getZoomLocation(Point zoomPoint)
        {
            //Creating a rectangle to hold the area that the progran us zooming into
            Rectangle zoomLocation = new Rectangle();
            
            //Offseting the zoom position so that the zoom point is in the center
            zoomLocation.X = MousePosition.X - (Width / 2);
            zoomLocation.Y = MousePosition.Y - (Height / 2);
            zoomLocation.Height = Height;
            zoomLocation.Width = Width;
            return zoomLocation;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawScreen();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            
            if (zoomCounter == ZOOMTIME)
            {
                timer1.Stop();
                endZoomClick();
            }
            else
            { 
                zoomCounter++;
            }

            
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            startZoomClick();

            
        }

        public void DrawScreen()
        {

            int zoomWidth = zoomBmp.Width * MAXZOOM;
            int zoomHeight = zoomBmp.Height * MAXZOOM;

            int zoomXPos = -((zoomWidth - zoomBmp.Width) / 2);
            int zoomYPos = -((zoomHeight - zoomBmp.Height) / 2);


            formGraphics.DrawImage(zoomBmp, zoomXPos, zoomYPos, zoomWidth, zoomHeight);
        }

        public void startZoomClick()
        {
            this.Show();
            zoomCounter = 0;
            zoomLocation = getZoomLocation(MousePosition);
            zoomGraphics.FillRectangle(Brushes.DarkGray, 0, 0, zoomBmp.Width, zoomBmp.Height);
            zoomGraphics.CopyFromScreen(zoomLocation.Location, new Point(0, 0), zoomLocation.Size);
            Point formPos = getFormPosition(zoomLocation, Screen.PrimaryScreen.Bounds);
            Left = formPos.X;
            Top = formPos.Y;
            DrawScreen();
            timer1.Start();
        } 


        public void endZoomClick()
        {
            this.Hide();

            int zoomWidth = zoomBmp.Width * MAXZOOM;
            int zoomHeight = zoomBmp.Height * MAXZOOM;

            int borderX = (zoomWidth - zoomBmp.Width) / 2;
            int borderY = (zoomHeight - zoomBmp.Height) / 2;

            int mouseOnFormX = MousePosition.X - zoomLocation.Left;
            int mouseOnFormY = MousePosition.Y - zoomLocation.Right;

            Point mousePos = new Point(((mouseOnFormX + borderX)/ MAXZOOM) + Left, ((mouseOnFormY + borderY) / MAXZOOM) + Top);

            ClickPoint(mousePos);
        }

        private void ClickPoint(Point mousePos)
        {
            double mouseXPos = convertXToAbsolute(mousePos.X);
            double mouseYPos = convertYToAbsolute(mousePos.Y);
            inputSim.Mouse.MoveMouseTo(mouseXPos, mouseYPos);
            
            inputSim.Mouse.LeftButtonClick();
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

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            { 
                case Keys.Space:
                    startZoomClick();
                    break;
                case Keys.Up:
                    zoomCounter++;
                    break;
                case Keys.Down:
                    zoomCounter--;
                    break;
            }
        }
    }
}
