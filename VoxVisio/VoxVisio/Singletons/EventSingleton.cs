﻿using System;
using System.Drawing;
using System.Speech.Recognition;
using System.Windows.Forms;
using EyeXFramework;
//using FMUtils.KeyboardHook;
using Tobii.EyeX.Framework;
using Gma.System.MouseKeyHook;

namespace VoxVisio.Singletons
{
    delegate void FixationEvent(Fixation newFixation);

    class EventSingleton : IDisposable
    {
        private static EventSingleton _singleton;

        //Constants for the timer delays
        public const int DRAWINTERVAL = 100;
        public const int UPDATEINTERVAL = 100;

        //Shared Timers
        //Shared timer for all objects that want to update themselves
        public readonly Timer updateTimer = new Timer();
        //Timer for objects that want to draw something to the screen
        public readonly Timer drawTimer = new Timer();


        public readonly EyeXHost eyex;
        public event FixationEvent fixationEvent;
        public IKeyboardMouseEvents systemHook = Hook.GlobalEvents();

        protected EventSingleton()
        {
            //Setting up and starting the timers
            updateTimer.Interval = UPDATEINTERVAL;
            updateTimer.Start();

            drawTimer.Interval = DRAWINTERVAL;
            drawTimer.Start();

            //keyboardHook = new Hook("Global Action Hook");
            //systemHook = new Hook();

            fixationEvent += EventSingleton_fixationEvent;

            //Instantiating and starting the eye tracker host
            eyex = new EyeXHost();
            eyex.CreateFixationDataStream(FixationDataMode.Sensitive).Next += (s, e) => fixationEvent(CreateFixation(e.EventType, (int)e.X, (int)e.Y));
            eyex.Start();
            
        }

        public void setMouseFixationsStatus(bool status)
        {
            systemHook.MouseMove -= mouseToFixation;
            if (status == true)
            {
                systemHook.MouseMove += mouseToFixation;
            }

            
        }

        private void mouseToFixation(object s, MouseEventArgs e)
        {
            fixationEvent(CreateFixation(FixationDataEventType.End, e.X, e.Y));
        }

        void EventSingleton_fixationEvent(Fixation newFixation)
        {
            //throw new NotImplementedException();
        }

        public static EventSingleton Instance()
        {
            // Uses lazy initialization.
            // Note: this is not thread safe.
            if (_singleton == null)
            {
                _singleton = new EventSingleton();
            }

            return _singleton;
        }

        private Fixation CreateFixation(FixationDataEventType fixationDataEventType, int x, int y)
        {
            Fixation fx = null;
            switch (fixationDataEventType)
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

        public void Dispose()
        {
            eyex.Dispose();
            //speechRecognizer.Dispose();
        }
    }
}
