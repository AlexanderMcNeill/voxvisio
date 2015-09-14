using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Speech.Recognition;
using System.Windows.Forms;
using EyeXFramework;
using VoxVisio.Properties;

namespace VoxVisio
{
    public partial class VoxVisio : Form
    {
        private readonly NotifyIcon notifyicon;
        private IContainer component;
        private MainEngine mainEngine;
        private SettingsSingleton _settingsList;


        public VoxVisio()
        {
            InitializeComponent();

            this.component = new Container();
            // Create the NotifyIcon. 
            notifyicon = new NotifyIcon(component);
            notifyicon.MouseDoubleClick += notifyIcon_MouseDoubleClick;

            // The Icon property sets the icon that will appear 
            // in the systray for this application.
            Icon icon = Resources.favicon;
            notifyicon.Icon = icon;
            

            // The Text property sets the text that will be displayed, 
            // in a tooltip, when the mouse hovers over the systray icon.
            notifyicon.Text = "VoxVisio";

            this.Resize += frmMain_Resize;

            mainEngine = new MainEngine();
            _settingsList = SettingsSingleton.Instance();
            populateList();
        }

        private void VoxVisio_Load(object sender, EventArgs e)
        {

        }



        private void populateList()
        {
            foreach (var VARIABLE in _settingsList.Commands)
            {
                lvCommandList.Items.Add(new ListViewItem(new string[] {VARIABLE.VoiceKeyword, VARIABLE.keyCombo.GetKeyString()}));
            }
        }


        // Minimize the program to the icon tray
        private void frmMain_Resize(object sender, EventArgs e)
        {
            switch (this.WindowState)
            {
                case FormWindowState.Minimized:
                    notifyicon.Visible = true;
                    this.ShowInTaskbar = false;
                    this.Hide();
                    break;
                case FormWindowState.Normal:
                    notifyicon.Visible = false;
                    this.Show();
                    break;
            }
        }
        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            notifyicon.Visible = false;
        }

        private void fIileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void VoxVisio_FormClosing(object sender, FormClosingEventArgs e)
        {
            mainEngine.close();
        }
    }


}
