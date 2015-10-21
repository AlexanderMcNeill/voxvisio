using System;
using System.Drawing;
using System.Windows.Forms;

namespace VoxVisio.Screen_Overlay
{
    class Toast : Overlay
    {
        private const int DISPLAYTIME = 20;
        private const int MESSAGEBOXWIDTH = 300;
        private const int MESSAGEBOXHEIGHT = 50;
        private int displayCount;
        private string message;
        private bool drawMessage;
        private Font textFont;
        private StringFormat stringFormat;
        private Rectangle messageBoxRect;

        public Toast()
        {
            textFont = new Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Point);
            Point centerPoint = new Point(Screen.PrimaryScreen.Bounds.Width /2, Screen.PrimaryScreen.Bounds.Height - 200);
            messageBoxRect = new Rectangle(centerPoint.X - MESSAGEBOXWIDTH / 2, centerPoint.Y - MESSAGEBOXHEIGHT / 2, MESSAGEBOXWIDTH, MESSAGEBOXHEIGHT);
            stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
        }

        public void NewMessage(string message)
        {
            this.message = message;
            drawMessage = true;
            displayCount = 0;
        }

        public void Draw(Graphics g)
        {
            if (drawMessage)
            {
                g.FillRectangle(Brushes.LightGray, messageBoxRect);
                g.DrawRectangle(Pens.Black, messageBoxRect);
                g.DrawString(message, textFont, Brushes.Black, messageBoxRect, stringFormat);

                displayCount++;

                if (displayCount > DISPLAYTIME)
                {
                    drawMessage = false;
                }
            }
        }
    }
}
