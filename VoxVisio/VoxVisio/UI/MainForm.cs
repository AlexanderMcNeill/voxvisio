using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Speech.Recognition;
using System.Windows.Forms;
using EyeXFramework;
using VoxVisio.Properties;
using VoxVisio.UI;
using VoxVisio.Singletons;

namespace VoxVisio
{
    public partial class VoxVisio : Form
    {
        private const int HIDDENYPOS = -80;
        private readonly NotifyIcon notifyicon;
        private IContainer component;
        private MainEngine mainEngine;
        private HelpForm helpForm;
        private SettingsForm settingsForm;


        public VoxVisio()
        {
            InitializeComponent();
            TopMost = true;
            Top = 0;
            Left = Screen.PrimaryScreen.Bounds.Width / 2 - Width / 2;

            mainEngine = new MainEngine();

            settingsForm = new SettingsForm();
            helpForm = new HelpForm();
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
            base.OnFormClosing(e);
            EventSingleton.Instance().Dispose();
        }
    }


}
