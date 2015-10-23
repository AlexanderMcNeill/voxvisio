using System;
using System.Drawing;
using VoxVisio.States;

namespace VoxVisio.Screen_Overlay
{
    public class StateHotspot
    {
        private Image activeImage;
        private Image inactiveImage;
        private eState state;
        private Action callback;
        private Rectangle hotspotRect;
        private int counter = 0;
        public bool selected = false;
        private bool focused = false;
        private SolidBrush brush = new SolidBrush(Color.FromArgb(150, Color.Red));

        public delegate void HotspotActivatedEventHandler(StateHotspot sender, eScrollState e);
        public event HotspotActivatedEventHandler OnChange;

        public StateHotspot(eState state, Rectangle hotspotRect, bool selected, Image activeImage, Image inactiveImage)
        {
            //Setting what state the hotspot represents
            this.state = state;

            //Setting if it is currently selected
            this.selected = selected;

            //Setting the size and position of the hotspot
            this.hotspotRect = hotspotRect;

            //Setting the images it will use to display if it is active
            this.activeImage = activeImage;
            this.inactiveImage = inactiveImage;
        }

        public void Draw(Graphics g)
        {
            if (selected)
            {
                //If the hotspot is currently selected displaying the active image
                g.DrawImage(activeImage, hotspotRect);
            }
            else
            {
                //If the hotspot is not selected displaying the inactive image
                g.DrawImage(inactiveImage, hotspotRect);

                //Getting how big the progress circle will be
                int fillWidth = (hotspotRect.Width / 100) * counter;
                int fillHeight = (hotspotRect.Width / 100) * counter;

                //Make the progress circle centered to the hotspot rectangle
                int xPos = (hotspotRect.Width - fillWidth) / 2 + hotspotRect.X;
                int yPos = (hotspotRect.Width - fillHeight) / 2 + hotspotRect.Y;

                g.FillEllipse(brush, xPos, yPos, fillWidth, fillHeight);
                
            }
        }

        public void Update()
        {
            if(!selected)
            {
                if (focused)
                {
                    if (counter < 40)
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
