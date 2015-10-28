using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VoxVisio.UI
{
    public partial class MainSystemTray : Form
    {
        private MainEngine mainEngine;
        private SettingsForm settingsForm;

        private readonly NotifyIcon notifyicon;
        private IContainer component;

        public MainSystemTray()
        {
            InitializeComponent();
            
            mainEngine = new MainEngine();
            settingsForm = new SettingsForm();

            this.component = new Container();
            // Create the NotifyIcon. 
            notifyicon = new NotifyIcon(component);
            notifyicon.MouseDoubleClick += notifyIcon_MouseDoubleClick;

            // The Icon property sets the icon that will appear 
            // in the systray for this application.
            Icon icon = Properties.Resources.favicon;
            notifyicon.Icon = icon;


            // The Text property sets the text that will be displayed, 
            // in a tooltip, when the mouse hovers over the systray icon.
            notifyicon.Text = "VoxVisio is Running";

            this.WindowState = FormWindowState.Minimized;
            notifyicon.Visible = true;
            this.ShowInTaskbar = false;
            this.Hide();

            //this.Resize += frmMain_Resize;


        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Anything that needs to happen when the tray icon is clicked goes here.

            settingsForm.ShowDialog(this);

        }
       

        private void MainSystemTray_Load(object sender, EventArgs e)
        {
            
        }
    }
}
