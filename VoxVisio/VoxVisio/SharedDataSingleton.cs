using EyeXFramework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tobii.EyeX.Framework;
using VoxVisio.Screen_Overlay;
using WindowsInput;

namespace VoxVisio
{

    delegate void FixationEvent(Fixation newFixation);
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

        public event FixationEvent fixationEvent;

        private EyeXHost eyex;

        protected SharedDataSingleton()
        {
            inputSimulator = new InputSimulator();

            //Setting up the forms that need to always be available to the program
            overlayForm = new OverlayForm();
            zoomForm = new ZoomForm();

            overlayForm.Show();

            //Setting up and starting the timers
            updateTimer.Interval = UPDATEINTERVAL;
            updateTimer.Start();

            drawTimer.Interval = DRAWINTERVAL;
            drawTimer.Tick += drawTimer_Tick;
            drawTimer.Start();

            fixationEvent += SharedDataSingleton_fixationEvent;

            //Instantiating and starting the eye tracker host
            eyex = new EyeXHost();
            eyex.CreateFixationDataStream(FixationDataMode.Sensitive).Next += (s, e) => fixationEvent(CreateFixation(e.EventType, (int)e.X, (int)e.Y, e.Timestamp));
            eyex.Start();
        }

        void SharedDataSingleton_fixationEvent(Fixation newFixation)
        {
            //throw new NotImplementedException();
        }

        public Fixation CreateFixation(FixationDataEventType t, int x, int y, double timeStamp)
        {
            Fixation fx = null;
            switch (t)
            {
                case FixationDataEventType.Begin:
                    fx = new Fixation(new Point(x, y), eFixationPhase.start);
                    break;
                case FixationDataEventType.End:
                    fx = new Fixation(new Point(x, y), eFixationPhase.finished);
                    break;
                case FixationDataEventType.Data:
                    fx = new Fixation(new Point(x, y), eFixationPhase.data);
                    break;
            }
            return fx;
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
