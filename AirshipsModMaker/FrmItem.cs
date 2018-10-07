using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirshipsModMaker
{
    public partial class FrmItem : Form
    {
        ModItem TmpItem;
        public ModItem ModItem;//输出的moditem
        public int Nowed;//当前正在编辑的物品id
        Template[] Templates;
        public FrmItem(ModItem mi, Template[] templates)
        {
            this.DialogResult = DialogResult.Cancel;
            InitializeComponent();
            Templates = templates;
            relstmp();
            comboBoxItemTemplare.SelectedText = mi.UseTemp.Name;
            TmpItem = new ModItem(mi);
            Nowed = TmpItem.ItemID;
            setShow();
        }
        public FrmItem(int id, Template[] templates)
        {
            this.DialogResult = DialogResult.Cancel;
            InitializeComponent();
            Nowed = id;
            Templates = templates;
            relstmp();
        }
        void relstmp()//刷新 comboBoxItemTemplare
        {
            comboBoxItemTemplare.Items.Clear();
            foreach (Template tm in Templates)
            {
                comboBoxItemTemplare.Items.Add(tm.Name);
            }
        }


        int odsl = -1;//防止选择到同一个index时刷新
        private void comboBoxItemTemplare_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxItemTemplare.SelectedIndex == odsl)
                return;
            else
                odsl = comboBoxItemTemplare.SelectedIndex;
            Template tm = Templates[odsl];
            TmpItem = new ModItem(Nowed, "", "", tm);
            setShow();
        }
        public void setShow()//展示全部的数据
        {
            textBoxName.Text = TmpItem.UseTemp.Name;
            textBoxItemInfo.Text = TmpItem.UseTemp.Info;
            pictureBoxdisplay.Image = TmpItem.UseTemp.DemoImage;
            toolTip1.SetToolTip(labelT2, "This Templare is Made By " + TmpItem.UseTemp.Author);
            //展示数据
            flowLayoutPanelShow.Controls.Clear();
            UCSettingItem uC;
            foreach (ModSet ms in TmpItem.Data)
            {
                uC = new UCSettingItem(ms, ChangeText);
                flowLayoutPanelShow.Controls.Add(uC);
            }
        }
        public void ChangeText(string str) => textBoxlable.Text = str;
        private void buttonsave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Save();
            Close();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            Close();
        }
        public void Save()
        {
            if (comboBoxItemTemplare.SelectedIndex == -1)
            {
                this.DialogResult = DialogResult.No;
                return;
            }
            TmpItem.Name = textBoxName.Text;
            TmpItem.Info = textBoxItemInfo.Text;

            ModItem = new ModItem(TmpItem);
        }
        private void FrmItem_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (comboBoxItemTemplare.SelectedIndex == -1)
            {
                this.DialogResult = DialogResult.No;
                return;
            }
            if (DialogResult == DialogResult.Cancel)
            {
                switch (MessageBox.Show("You Will Lost All Change You Didn't Save!\nSave Now?or Cancel Close This windows\n" +
                    "Tips:Click Button To Save/Close You Worlk", "Item Editor Are Closeing", MessageBoxButtons.YesNoCancel))
                {
                    case DialogResult.Yes:
                        Save();
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
        }
    }
}
