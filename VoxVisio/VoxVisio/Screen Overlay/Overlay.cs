using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoxVisio.Screen_Overlay
{
    public interface Overlay
    {
        void Draw(Graphics g);
    }
}
