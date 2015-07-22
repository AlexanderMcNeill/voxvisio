using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VoxVisio
{
    public enum eTostState { 
        POPUP,
        DISPLAY,
        HIDE
    }
    public partial class ToastForm : Form
    {

        private const int ANIMATETIME = 30;
        private const int DISPLAYTIME = 50;
        private const int VELOCITY = 5;

        private eTostState toastState;

        private int counter = 0;
        private int startLocation;

        public ToastForm()
        {
            InitializeComponent();
            startLocation = Screen.PrimaryScreen.Bounds.Height + Height;
            Top = startLocation;
        }

        public void showToast(string toastMessage)
        {
            this.Invoke((MethodInvoker)delegate
            {
                counter = 0;
                Top = startLocation;
                toastState = eTostState.POPUP;
                lblToast.Text = toastMessage;
                toastTimer.Start();
            });
        }

        private void toastTimer_Tick(object sender, EventArgs e)
        {
            switch (toastState)
            { 
                case eTostState.POPUP:
                    if (counter < ANIMATETIME)
                    {
                        Top -= VELOCITY;
                    }
                    else
                    {
                        counter = 0;
                        toastState = eTostState.DISPLAY;
                    }
                    break;
                case eTostState.DISPLAY:
                    if (counter > DISPLAYTIME)
                    {
                        counter = 0;
                        toastState = eTostState.HIDE;
                    }
                    break;
                case eTostState.HIDE: 
                    if (counter < ANIMATETIME)
                    {
                        Top += VELOCITY;
                    }
                    else
                    {
                        toastTimer.Stop();
                    }
                    break;
            }

            counter++;
        }


    }
}
