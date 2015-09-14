using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VoxVisio.Screen_Overlay
{
    class StateController : Overlay
    {
        private const int HOTSPOTSIZE = 50;
        private const int MARGIN = 10;

        private Rectangle commandStateHotspot;
        private int commandStateCounter = 0;

        private Rectangle dictationStateHotspot;
        private int dictationStateCounter = 0;

        private CommandState state;

        public StateController(CommandState state)
        {
            this.state = state;

        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        

        public void Draw(Graphics g)
        { 
        
        }
    }
}
