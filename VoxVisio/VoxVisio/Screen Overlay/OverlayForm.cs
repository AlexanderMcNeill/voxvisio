﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using VoxVisio.Singletons;

namespace VoxVisio.Screen_Overlay
{
    public partial class OverlayForm : Form
    {

        //Code to let mouse clicks fall through the form
        public enum GWL
        {
            ExStyle = -20
        }

        public enum WS_EX
        {
            Transparent = 0x20,
            Layered = 0x80000
        }

        public enum LWA
        {
            ColorKey = 0x1,
            Alpha = 0x8
        }

        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        public static extern int GetWindowLong(IntPtr hWnd, GWL nIndex);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        public static extern int SetWindowLong(IntPtr hWnd, GWL nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetLayeredWindowAttributes")]
        public static extern bool SetLayeredWindowAttributes(IntPtr hWnd, int crKey, byte alpha, LWA dwFlags);

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            int wl = GetWindowLong(this.Handle, GWL.ExStyle);
            wl = wl | 0x80000 | 0x20;
            SetWindowLong(this.Handle, GWL.ExStyle, wl);
            SetLayeredWindowAttributes(this.Handle, 0, 128, LWA.Alpha);
        }
        //==================================================================================================

        //Getting the get forground window method from user32
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        //Objects for smoothly drawing to the screen
        private Bitmap buffer;
        private Graphics bufferGraphics;
        private Graphics formGraphics;

        //List of the overlays that will be observing the overlay form
        private List<Overlay> overlays = new List<Overlay>();

        public OverlayForm()
        {
            InitializeComponent();
            
            TopLevel = true;
            TopMost = true;

            //Setting form to fill the primary screen of the computer
            Top = 0;
            Left = 0;

            Height = Screen.PrimaryScreen.Bounds.Height;
            Width = Screen.PrimaryScreen.Bounds.Width;

            //Creating buffer for graphics
            buffer = new Bitmap(Width, Height);
            bufferGraphics = Graphics.FromImage(buffer);

            //Getting reference to form graphics
            formGraphics = CreateGraphics();

            EventSingleton.Instance().drawTimer.Tick += drawTimer_Tick; 
        }

        void drawTimer_Tick(object sender, EventArgs e)
        {
            DrawOverlays();
        }

        //==========================================================================================================
        //Methods to manage observers
        public void RegisterOverlay(Overlay toAdd)
        {
            overlays.Add(toAdd);
        }

        public void RemoveOverlay(Overlay toRemove)
        {
            overlays.Remove(toRemove);
        }

        private void setTopMost()
        {
            if (GetForegroundWindow() == Process.GetCurrentProcess().MainWindowHandle)
            {
                TopMost = true;
            }
        }

        public void DrawOverlays()
        {
            setTopMost();
            //Clearing the buffer to the transparent color of the form
            bufferGraphics.Clear(BackColor);

            //Drawing each of the overlays to the screen
            foreach(Overlay o in overlays)
            {
                o.Draw(bufferGraphics);
            }

            //Drawing the buffer to the screen
            formGraphics.DrawImage(buffer, 0, 0);
        }

        //=============================================================================================================
        //Methods for changing the drawing order of the overlays

        public void MoveToFront(Overlay o)
        {
            
        }

        public void MoveToBack(Overlay o)
        { 
        
        }

        public void MoveUp(Overlay o)
        { 
        
        }

        public void MoveDown(Overlay o)
        { 
        
        }
    }
}
