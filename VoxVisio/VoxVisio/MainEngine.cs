using System;
using System.Drawing;
using EyeXFramework;
using Tobii.EyeX.Framework;

namespace VoxVisio
{
    public class MainEngine
    {
        private ControlContext controlState;
        private EyeXHost eyex;

        //TODO : Alex add in voice requiered classes

        public MainEngine()
        {
            controlState = new ControlContext(new StandardState());
            eyex = new EyeXHost();
            controlState.changedState += StateChanged;
            eyex.CreateFixationDataStream(FixationDataMode.Sensitive).Next += (s, e) => Fixation(e.EventType ,(int)e.X, (int)e.Y, e.Timestamp);
        }

        public void Fixation(FixationDataEventType t, int x, int y, double timeStamp)
        {
            Fixation fx = null;
            if (t == FixationDataEventType.Begin)
            {
                fx = new Fixation(new Point(x, y), eFixationPhase.start);
            }
            if (t == FixationDataEventType.End)
            {
                fx = new Fixation(new Point(x,y),eFixationPhase.finished );
            }
            controlState.EyeRequest(fx);
        }

        public void StateChanged()
        {
            //TODO : Do stuff here that needs to be done once state has changed
            if (controlState.ControlState.GetType() == typeof (StandardState))
            {
                
            }
            else if (controlState.ControlState.GetType() == typeof(DictationState))
            {
                
            }
           
        }
    }
}
