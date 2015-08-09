using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Windows;
using Point = System.Drawing.Point;


namespace Karna.Magnification
{
    public class Magnifier : IDisposable
    {
        private Form form;
        private IntPtr hwndMag;
        private float baseMagnification;
        private float currentMagnification;
        private bool initialized;
        private RECT magWindowRect = new RECT();
        private Timer timer;
        private Point magnifyCenter; // The center of where the magnifier is aimed (Not the window itself but the magnified image)
        private Point startZoomPoint;
        private bool isZooming;
        private int currentZoomStep;
        private const int MAXZOOMSTEPS = 200;
        private Timer zoomTimer;

        public Magnifier(Form form)
        {
            if (form == null)
                throw new ArgumentNullException("form");

            baseMagnification = 2.0f;
            currentMagnification = baseMagnification;
            isZooming = false;
            this.form = form;
            this.form.Resize += new EventHandler(form_Resize);
            this.form.FormClosing += new FormClosingEventHandler(form_FormClosing);

            timer = new Timer();
            timer.Tick += new EventHandler(timer_Tick);
            zoomTimer = new Timer {Interval = 100};
            zoomTimer.Tick += ZoomTimer_Tick;

            initialized = NativeMethods.MagInitialize();
            if (initialized)
            {
                SetupMagnifier();
                timer.Interval = NativeMethods.USER_TIMER_MINIMUM;
                timer.Enabled = true;
            }
        }

        /// <summary>
        /// Start the zoom function with the start point as the current location of the maginifier
        /// </summary>
        public void startZooming()
        {
            startZooming(magnifyCenter);
        }
        /// <summary>
        /// Start zooming with the provided point as the start location of the zoom
        /// </summary>
        /// <param name="p">Location to start the zoom from</param>
        public void startZooming(Point p)
        {
            isZooming = true;
            startZoomPoint = p;
            currentMagnification = baseMagnification;
            currentZoomStep = 0;
            zoomTimer.Enabled = isZooming;
        }

        private void ZoomTimer_Tick(object sender, EventArgs e)
        {
            POINT cursorPos = new POINT();
            NativeMethods.GetCursorPos(ref cursorPos);
            MoveWindowTowardsPos(new Point(cursorPos.x, cursorPos.y));
            currentZoomStep++;
            currentMagnification += (float)0.01;
            if (currentZoomStep > MAXZOOMSTEPS)
            {
                isZooming = false;
                zoomTimer.Enabled = isZooming;
            }
        }

        private void MoveWindowTowardsPos(Point moveTowards)
        {
            double vectorX = moveTowards.X - startZoomPoint.X;
            double vectorY = moveTowards.Y - startZoomPoint.Y;
            Vector v = new Vector(vectorX, vectorY );
            v.Normalize();
            Point newWindowPos = new Point((int)(form.Left + (form.Width / 2) + v.X*3), (int)(form.Top +(form.Height /2)+ v.Y*3));
            magnifyCenter = newWindowPos;
            setMagnifyWindowPos(newWindowPos);
        }
        

        void form_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer.Enabled = false;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            UpdateMaginifier();
        }

        void form_Resize(object sender, EventArgs e)
        {
            ResizeMagnifier();
        }

        ~Magnifier()
        {
            Dispose(false);
        }

        protected virtual void ResizeMagnifier()
        {
            if ( initialized && (hwndMag != IntPtr.Zero))
            {
                NativeMethods.GetClientRect(form.Handle, ref magWindowRect);
                // Resize the control to fill the window.
                NativeMethods.SetWindowPos(hwndMag, IntPtr.Zero,
                    magWindowRect.left, magWindowRect.top, magWindowRect.right, magWindowRect.bottom, 0);
            }
        }
        /// <summary>
        /// Sets the position of the form on the screen
        /// </summary>
        /// <param name="newPos">The new position to center the form on.</param>
        public void setMagnifyWindowPos(Point newPos)
        {
            form.Top = newPos.Y - (form.Height / 2);
            form.Left = newPos.X - (form.Width / 2);
        }

        public virtual void UpdateMaginifier()
        {
            if ((!initialized) || (hwndMag == IntPtr.Zero))
                return;

            POINT mousePoint = new POINT(magnifyCenter.X, magnifyCenter.Y);
            RECT sourceRect = new RECT();

            

            int width = (int)((magWindowRect.right - magWindowRect.left) / baseMagnification);
            int height = (int)((magWindowRect.bottom - magWindowRect.top) / baseMagnification);

            sourceRect.left = mousePoint.x - width / 2;
            sourceRect.top = mousePoint.y - height / 2;


            // Don't scroll outside desktop area.
            if (sourceRect.left < 0)
            {
                sourceRect.left = 0;
            }
            if (sourceRect.left > NativeMethods.GetSystemMetrics(NativeMethods.SM_CXSCREEN) - width)
            {
                sourceRect.left = NativeMethods.GetSystemMetrics(NativeMethods.SM_CXSCREEN) - width;
            }
            sourceRect.right = sourceRect.left + width;

            if (sourceRect.top < 0)
            {
                sourceRect.top = 0;
            }
            if (sourceRect.top > NativeMethods.GetSystemMetrics(NativeMethods.SM_CYSCREEN) - height)
            {
                sourceRect.top = NativeMethods.GetSystemMetrics(NativeMethods.SM_CYSCREEN) - height;
            }
            sourceRect.bottom = sourceRect.top + height;

            if (this.form == null)
            {
                timer.Enabled = false;
                return;
            }

            if (this.form.IsDisposed)
            {
                timer.Enabled = false;
                return;
            }

            // Set the source rectangle for the magnifier control.
            NativeMethods.MagSetWindowSource(hwndMag, sourceRect);

            // Reclaim topmost status, to prevent unmagnified menus from remaining in view. 
            NativeMethods.SetWindowPos(form.Handle, NativeMethods.HWND_TOPMOST, 0, 0, 0, 0,
                (int)SetWindowPosFlags.SWP_NOACTIVATE | (int)SetWindowPosFlags.SWP_NOMOVE | (int)SetWindowPosFlags.SWP_NOSIZE);

            // Force redraw.
            NativeMethods.InvalidateRect(hwndMag, IntPtr.Zero, true);
        }

        public float BaseMagnification
        {
            get { return baseMagnification; }
            set
            {
                if (baseMagnification != value)
                {
                    baseMagnification = value;
                    // Set the baseMagnification factor.
                    Transformation matrix = new Transformation(baseMagnification);
                    NativeMethods.MagSetWindowTransform(hwndMag, ref matrix);
                }
            }
        }

        public System.Drawing.Point MagnifyCenter
        {
            get { return magnifyCenter; }
            set { magnifyCenter = value; }
        }

        protected void SetupMagnifier()
        {
            if (!initialized)
                return;

            IntPtr hInst;

            hInst = NativeMethods.GetModuleHandle(null);

            // Make the window opaque.
            form.AllowTransparency = true;
            form.TransparencyKey = Color.Empty;
            form.Opacity = 255;

            // Create a magnifier control that fills the client area.
            NativeMethods.GetClientRect(form.Handle, ref magWindowRect);
            hwndMag = NativeMethods.CreateWindow((int)ExtendedWindowStyles.WS_EX_CLIENTEDGE, NativeMethods.WC_MAGNIFIER,
                "MagnifierWindow", (int)WindowStyles.WS_CHILD | (int)MagnifierStyle.MS_SHOWMAGNIFIEDCURSOR |
                (int)WindowStyles.WS_VISIBLE,
                magWindowRect.left, magWindowRect.top, magWindowRect.right, magWindowRect.bottom, form.Handle, IntPtr.Zero, hInst, IntPtr.Zero);

            if (hwndMag == IntPtr.Zero)
            {
                return;
            }

            // Set the baseMagnification factor.
            Transformation matrix = new Transformation(baseMagnification);
            NativeMethods.MagSetWindowTransform(hwndMag, ref matrix);
        }

        protected void RemoveMagnifier()
        {
            if (initialized)
                NativeMethods.MagUninitialize();
        }

        protected virtual void Dispose(bool disposing)
        {
            timer.Enabled = false;
            if (disposing)
                timer.Dispose();
            timer = null;
            form.Resize -= form_Resize;
            RemoveMagnifier();
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
