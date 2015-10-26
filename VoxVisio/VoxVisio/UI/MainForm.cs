﻿using System;
using System.Diagnostics;
using System.Windows.Forms;
using VoxVisio.Singletons;
using VoxVisio.UI;

namespace VoxVisio
{
    public partial class VoxVisio : Form
    {
        private const int HIDDENYPOS = -80;
        private const int DISPLAYTIME = 50;
        private int counter = 0;
        private MainEngine mainEngine;
        private HelpForm helpForm;
        private SettingsForm settingsForm;
        //TODO: Tell alex the form is never showing and crashing when arrow button is clicked

        public VoxVisio()
        {
            InitializeComponent();
            TopMost = true;
            Top = HIDDENYPOS;
            Left = Screen.PrimaryScreen.Bounds.Width / 2 - Width / 2;

            mainEngine = new MainEngine();

            settingsForm = new SettingsForm();
            helpForm = new HelpForm();

            EventSingleton.Instance().updateTimer.Tick += updateTimer_Tick;
        }


        private void showForm()
        {
            this.Top = 0;
            counter = 0;
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            if (counter > DISPLAYTIME && this.Top > HIDDENYPOS)
            {
                this.Top -= 10;
            }
            else
            {
                counter += 1;
            }
            
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            helpForm.ShowDialog(this);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            settingsForm.ShowDialog(this);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            mainEngine.close();
            
            Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            EventSingleton.Instance().updateTimer.Tick -= updateTimer_Tick;
            base.OnFormClosing(e);
            EventSingleton.Instance().Dispose();
        }
    }


}
