using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LinePutScript;

namespace AirshipsModMaker
{
    public partial class UCSettingItem : UserControl
    {
        DeleteMSetin DeleteM;
        public UCSettingItem(ModSet modSet, ChangeText changeText, DeleteMSetin deleteMSetin)
        {
            InitializeComponent();
            ModSet = modSet;
            TextChange = changeText;
            DeleteM = deleteMSetin;

            toolTip1.SetToolTip(this, ModSet.Setin.info);
            toolTip1.SetToolTip(textBoxinput, ModSet.Setin.info);
            toolTip1.SetToolTip(labelInfoName, ModSet.Setin.info);
            if (ModSet.Value == "")
                textBoxinput.Text = ModSet.valuenomal;
            else
                textBoxinput.Text = ModSet.Value;
            labelInfoName.Text = ModSet.Setin.Mname;
        }
        public ModSet ModSet;
        public delegate void ChangeText(string txt);
        public ChangeText TextChange;
        private void textBoxinput_TextChanged(object sender, EventArgs e)
        {
            ModSet.Value = textBoxinput.Text;
        }
        private void UCSettingItem_Click(object sender, EventArgs e)
        {
            TextChange(ModSet.Setin.info + "\r\nDefault Value:" + ModSet.valuenomal);
        }

        private void buttonDelect_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Operation cannot be cancelled\n Are you Sure Delete This Stuff", "You are Try to Delete '" +
                            ModSet.Setin.Mname + "'", MessageBoxButtons.YesNo) == DialogResult.Yes)
                DeleteM(ModSet.Setin);
        }
        public delegate void DeleteMSetin(MSetin ms);
    }
}
