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
        public UCSettingItem(ModSet modSet, ChangeText changeText)
        {
            InitializeComponent();
            ModSet = modSet;
            TextChange = changeText;

            toolTip1.SetToolTip(this, ModSet.info);
            toolTip1.SetToolTip(textBoxinput, ModSet.info);
            toolTip1.SetToolTip(labelInfoName, ModSet.info);
            if (ModSet.Value == "")
                textBoxinput.Text = ModSet.valuenomal;
            else
                textBoxinput.Text = ModSet.Value;
            labelInfoName.Text = ModSet.Mname;
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
            TextChange(ModSet.info);
        }
    }
}
