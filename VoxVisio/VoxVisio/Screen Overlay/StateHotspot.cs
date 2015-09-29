using System;
using System.Drawing;

namespace VoxVisio.Screen_Overlay
{
    public class StateHotspot
    {

        private string name;
        private Action callback;
        private Rectangle hotspotRect;
        private int counter = 0;
        public bool selected = false;
        private bool focused = false;

        public StateHotspot(string name, Action callback, Rectangle hotspotRect, bool selected)
        {
            this.name = name;
            this.selected = selected;
            this.callback = callback;
            this.hotspotRect = hotspotRect;
        }

        public void Draw(Graphics g)
        {
            Font font1 = new Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Point);
            if (selected)
            {
                g.FillEllipse(Brushes.Green, hotspotRect);
                
            }
            else
            {
                g.FillEllipse(Brushes.Blue, hotspotRect);

                //Getting how big the progress circle will be
                int fillWidth = (hotspotRect.Width / 100) * counter;
                int fillHeight = (hotspotRect.Width / 100) * counter;

                //Make the progress circle centered to the hotspot rectangle
                int xPos = (hotspotRect.Width - fillWidth) / 2 + hotspotRect.X;
                int yPos = (hotspotRect.Width - fillHeight) / 2 + hotspotRect.Y;

                g.FillEllipse(Brushes.Red, xPos, yPos, fillWidth, fillHeight);
            }
            g.DrawString(name, font1, Brushes.YellowGreen, hotspotRect);
        }

        public void Update()
        {
            if(!selected)
            {
                if (focused)
                {
                    if (counter < 100)
                    {
                        counter += 10;
                    }
                    else
                    {
                        counter = 0;
                        selected = true;
                        callback();
                    }
                }
                else if (counter > 0)
                {
                    counter -= 10;
                }
            }
        }

        public void Fixation(Point fixation)
        {
            focused = hotspotRect.Contains(fixation);
        }
    }
}
