using System;
using System.Windows.Forms;
using VoxVisio.Singletons;
using VoxVisio.UI;

namespace VoxVisio
{
    public partial class VoxVisio : Form
    {
        private const int HIDDENYPOS = -80;
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
