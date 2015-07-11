using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyeXFramework;

namespace VoxVisio
{
    public class MainEngine
    {
        private ControlContext controlState;
        private EyeXHost eyex;

        public MainEngine()
        {
            controlState = new ControlContext(new StandardState());
        }
    }
}
