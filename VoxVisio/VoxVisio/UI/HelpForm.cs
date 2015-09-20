using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using VoxVisio.Singletons;

namespace VoxVisio.UI
{
    public partial class HelpForm : Form
    {
        private SettingsSingleton commandList;
        public HelpForm()
        {
            InitializeComponent();
            commandList = SettingsSingleton.Instance();
            PopulateCommandList();
        }

        private void PopulateCommandList()
        {

            //Currently only displays voice commands
            foreach (VoiceCommand c in commandList.Commands.OfType<VoiceCommand>())
            {
                lvCommandList.Items.Add(new ListViewItem(new string[] {c.VoiceKeyword, c.keyCombo.GetKeyString()}));
            }
        }
    }
}
