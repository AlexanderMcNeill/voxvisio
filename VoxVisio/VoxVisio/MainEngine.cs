using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using EyeXFramework;

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
            eyex.CreateFixationDataStream(Tobii.EyeX.Framework.FixationDataMode.Sensitive).Next += (s, e) => Fixation(e.EventType ,(int)e.X, (int)e.Y, e.Timestamp);
        }

        public void Fixation(Tobii.EyeX.Framework.FixationDataEventType t, int x, int y, double timeStamp)
        {
            Fixation fx = null;
            if (t == Tobii.EyeX.Framework.FixationDataEventType.Begin)
            {
                fx = new Fixation(new Point(x, y), eFixationPhase.start);
            }
            if (t == Tobii.EyeX.Framework.FixationDataEventType.End)
            {
                fx = new Fixation(new Point(x,y),eFixationPhase.finished );
            }
            controlState.EyeRequest(fx);
        }
    }
}
