using System;
using System.Drawing;
using VoxVisio.States;

namespace VoxVisio.Screen_Overlay
{
    //Class that keeps track and draws hotspots for changing states
    public class StateHotspot
    {
        //Setting up event for when the hotspot is activated
        public delegate void HotspotActivatedEventHandler(StateHotspot sender, ControlState e);
        public event HotspotActivatedEventHandler OnSelected;

        //Constants for how long it takes to activate a hotspot and how fast it grows
        private const int FOCUSTIME = 40;
        private const int GROWTHSPEED = 10;

        //Images that represent what state the hotspot is in
        private Image activeImage;
        private Image inactiveImage;

        //Counter for keeping track of how close it is to being activated
        private int counter = 0;

        //Creating brush to how the color of the hotspots progress
        private SolidBrush brush = new SolidBrush(Color.FromArgb(150, Color.Red));

        private ControlState representedState;
        private Rectangle hotspotRect;
        public bool selected = false;
        private bool focused = false;

        public StateHotspot(ControlState representedState, Rectangle hotspotRect, bool selected, Image activeImage, Image inactiveImage)
        {
            //Setting what state the hotspot represents
            this.representedState = representedState;

            //Setting if it is currently selected
            this.selected = selected;

            //Setting the size and position of the hotspot
            this.hotspotRect = hotspotRect;

            //Setting the images it will use to display if it is active
            this.activeImage = activeImage;
            this.inactiveImage = inactiveImage;
        }

        public ControlState GetState()
        {
            return representedState;
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

                //Displaying how close they are to activating the state
                g.FillEllipse(brush, xPos, yPos, fillWidth, fillHeight);
                
            }
        }

        public void Update()
        {
            // Not updating if the state is already active
            if(!selected)
            {
                if (focused)
                {
                    if (counter < FOCUSTIME)
                    {
                        // Increasing counter if the user is focusing on the hotspot 
                        //and it is not ready to be activated yet
                        counter += GROWTHSPEED;
                    }
                    else
                    {
                        // Activating the state the hotspot represents
                        counter = 0;
                        selected = true;
                        OnSelected(this, representedState);
                    }
                }
                else if (counter > 0)
                {
                    // Decreasing the counter if the user is not looking at the hotspot
                    // And the hotspot isn't already set to 0
                    counter -= GROWTHSPEED;
                }
            }
        }

        public void Fixation(Point fixation)
        {
            // Setting focused to true if the user is currently looking inside of the hotspot bounds
            focused = hotspotRect.Contains(fixation);
        }
    }
}
