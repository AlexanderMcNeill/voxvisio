using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Security.Policy;
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
        private bool isZooming;
        private int currentZoomStep;
        private const int MAXZOOMSTEPS = 180;
        private Timer zoomTimer;
        public Point currentMousePos { get; set; }

        public Magnifier(Form form)
        {
            if (form == null)
                throw new ArgumentNullException("form");

            baseMagnification = 1.2f;
            currentMagnification = baseMagnification;
            isZooming = false;
            this.form = form;
            this.form.Resize += new EventHandler(form_Resize);
            this.form.FormClosing += new FormClosingEventHandler(form_FormClosing);

            timer = new Timer();
            timer.Tick += new EventHandler(timer_Tick);
            zoomTimer = new Timer {Interval = 15};
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
            isZooming = true;
            currentMagnification = baseMagnification;
            currentZoomStep = 0;
            zoomTimer.Enabled = isZooming;
        }

        private void ZoomTimer_Tick(object sender, EventArgs e)
        {
            //POINT cursorPos = new POINT();
            //NativeMethods.GetCursorPos(ref cursorPos); // Giving weird calculations on multi monitors
            //MoveMagWindowTowardsPos(new Point(cursorPos.x, cursorPos.y));
            
            MoveMagWindowTowardsPos(currentMousePos);
            currentZoomStep++;
            currentMagnification += (float)0.1;
            
            if (currentZoomStep > MAXZOOMSTEPS)
            {
                isZooming = false;
                zoomTimer.Enabled = isZooming;
            }
        }

        private void MoveMagWindowTowardsPos(Point moveTowards)
        {
            Point mvt = moveTowards;

            double nX = mvt.X - (magWindowRect.right/2);
            double nY = mvt.Y - (magWindowRect.bottom/2);
            Vector moveVector = new Vector(nX, nY);
            moveVector.Normalize();
            int newX = (int)(magnifyCenter.X + moveVector.X);
            int newY = (int) (magnifyCenter.Y + moveVector.Y);
            //TODO: Implement stopping when looking exactly on point
            Point newWindowPos = new Point(newX,newY);
            magnifyCenter = newWindowPos;
            //setMagnifyWindowPos(newWindowPos);
            UpdateMaginifier();
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

            

            int width = (int)((magWindowRect.right - magWindowRect.left) / currentMagnification);
            int height = (int)((magWindowRect.bottom - magWindowRect.top) / currentMagnification);

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

        private Point _getWindowCenter()
        {
            Point centerPoint = new Point(form.DisplayRectangle.X + form.DisplayRectangle.Width, form.DisplayRectangle.Y + form.DisplayRectangle.Height);
            return centerPoint;
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
