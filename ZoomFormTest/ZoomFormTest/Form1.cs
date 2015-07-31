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
        private const int MAXZOOM = 4;
        private const int ZOOMTIME = 2;
        private Graphics g;
        private Bitmap bmp;
        private Graphics formGraphics;
        private int screenWidth;
        private int screenHeight;
        private InputSimulator inputSim; 

        private int zoomCounter = 1;
        public Form1()
        {
            InitializeComponent();
            formGraphics = panel1.CreateGraphics();
            bmp = new Bitmap(Width, Height); 
            g = Graphics.FromImage(bmp);
            screenWidth = Screen.PrimaryScreen.Bounds.Width;
            screenHeight = Screen.PrimaryScreen.Bounds.Height;
            inputSim = new InputSimulator();
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

            int xPos = MousePosition.X - (Width / 2);
            int yPos = MousePosition.Y - (Height / 2);

            g.CopyFromScreen(xPos, yPos, 0, 0, new Size(Width, Height));

            int zoomWidth = bmp.Width * MAXZOOM;
            int zoomHeight = bmp.Height * MAXZOOM;

            int zoomXPos = -((zoomWidth - bmp.Width) / 2);
            int zoomYPos = -((zoomHeight - bmp.Height) / 2);


            formGraphics.DrawImage(bmp, zoomXPos, zoomYPos, zoomWidth, zoomHeight);
            

            Top = yPos;
            Left = xPos;
        }

        public void startZoomClick()
        {
            this.Show();
            zoomCounter = 0;
            DrawScreen();
            timer1.Start();
        } 


        public void endZoomClick()
        {
            this.Hide();

            int zoomWidth = bmp.Width * MAXZOOM;
            int zoomHeight = bmp.Height * MAXZOOM;

            int borderX = (zoomWidth - bmp.Width) / 2;
            int borderY = (zoomHeight - bmp.Height) / 2;

            int mouseOnFormX = MousePosition.X - Left;
            int mouseOnFormY = MousePosition.Y - Top;

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
