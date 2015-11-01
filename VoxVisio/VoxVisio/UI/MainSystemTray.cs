using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VoxVisio.UI
{
    public partial class MainSystemTray : Form
    {
        private MainEngine mainEngine;
        private SettingsForm settingsForm;
        private ContextMenu contextMenu;
        private MenuItem menuItemExit;
        private MenuItem menuItemSettings;

        private readonly NotifyIcon notifyicon;
        private IContainer component;

        public MainSystemTray()
        {
            InitializeComponent();
            
            mainEngine = new MainEngine(this);
            settingsForm = new SettingsForm();

            this.component = new Container();
            // Create the NotifyIcon. 
            notifyicon = new NotifyIcon(component);
            notifyicon.MouseClick += notifyIcon_MouseClick;

            //Setting up the right click menu for the icon
            contextMenu = new ContextMenu();
            menuItemExit = new MenuItem();
            menuItemSettings = new MenuItem();
            contextMenu.MenuItems.AddRange(new MenuItem[] { menuItemSettings, menuItemExit });
            menuItemSettings.Index = 0;
            menuItemSettings.Text = "Settings";
            menuItemSettings.Click += delegate { ShowSettings(); };
            menuItemExit.Index = 1;
            menuItemExit.Text = "Exit";
            menuItemExit.Click += menuItem1_Click;
            notifyicon.ContextMenu = contextMenu;

            // The Icon property sets the icon that will appear 
            // in the systray for this application.
            Icon icon = Properties.Resources.favicon;
            notifyicon.Icon = icon;


            // The Text property sets the text that will be displayed, 
            // in a tooltip, when the mouse hovers over the systray icon.
            notifyicon.Text = "VoxVisio is Running";

            WindowState = FormWindowState.Minimized;
            notifyicon.Visible = true;
            ShowInTaskbar = false;
            Hide();

            //this.Resize += frmMain_Resize;

        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            //Anything that needs to happen when the tray icon is clicked goes here.
            ShowSettings();
        }

        private void ShowSettings()
        {
            settingsForm.ShowDialog(this);
        }

        public void ExitProgram()
        {
            // The exit call must wait for a second for all the other processes to start before exiting the program otherwise 
            // Object disposal errors occur.
            new Thread(delegate () {
                Thread.Sleep(1000);
                Application.Exit();
            }).Start();
        }
       

        private void MainSystemTray_Load(object sender, EventArgs e)
        {
            
        }

        private void MainSystemTray_FormClosing(object sender, FormClosingEventArgs e)
        {
            notifyicon.Icon = null;
        }
    }
}
