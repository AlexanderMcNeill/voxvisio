using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VoxVisio.Screen_Overlay;
using WindowsInput;

namespace VoxVisio
{
    class SharedDataSingleton
    {
        private static SharedDataSingleton _singleton;

        //Form for displaying graphics over the screen
        public readonly OverlayForm overlayForm;

        //Form for the zoom click
        public readonly ZoomForm zoomForm;

        //Shared Timers
        //Shared timer for all objects that want to update themselves
        public readonly Timer updateTimer = new Timer();
        //Timer for objects that want to draw something to the screen
        public readonly Timer drawTimer = new Timer();

        //Constants for the timer delays
        public const int DRAWINTERVAL = 100;
        public const int UPDATEINTERVAL = 100;
        public readonly InputSimulator inputSimulator;

        protected SharedDataSingleton()
        {
            inputSimulator = new InputSimulator();

            //Setting up the forms that need to always be available to the program
            overlayForm = new OverlayForm();
            zoomForm = new ZoomForm(inputSimulator);

            overlayForm.Show();

            //Setting up and starting the timers
            updateTimer.Interval = UPDATEINTERVAL;
            updateTimer.Start();

            drawTimer.Interval = DRAWINTERVAL;
            drawTimer.Tick += drawTimer_Tick;
            drawTimer.Start();
        }

        void drawTimer_Tick(object sender, EventArgs e)
        {
            overlayForm.DrawOverlays();
        }

        public static SharedDataSingleton Instance()
        {
            // Uses lazy initialization.
            // Note: this is not thread safe.
            if (_singleton == null)
            {
                _singleton = new SharedDataSingleton();
            }

            return _singleton;
        }
 
    }
}
