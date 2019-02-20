using System;
using System.Windows.Forms;
using LinePutScript;
using MultiLang;

namespace AirshipsModMaker
{
    public partial class FrmItem : Form
    {
        ModItem TmpItem;//正在编辑的物品
        public ModItem ModItem;//输出的moditem
        public int Nowed;//当前正在编辑的物品id
        Template[] Templates;
        MSetin[] MSetins;
        Lang lang;
        FrmMain frmMain;
        public FrmItem(ModItem mi, FrmMain frmmain, Lang lang)
        {
            this.DialogResult = DialogResult.Cancel;
            InitializeComponent();
            //语言操作
            if (lang != null && !lang.Default)
            {
                this.lang = lang;
                Translate(lang);
            }
            frmMain = frmmain;
            Templates = frmMain.Templates.ToArray();
            MSetins = frmMain.MSetins.ToArray();
            relstmp();
            for (int i = 0; i < Templates.Length; i++)
                if (Templates[i] == mi.UseTemp)
                {
                    comboBoxItemTemplare.SelectedIndex = i;
                    break;
                }
            TmpItem = new ModItem(mi);
            Nowed = TmpItem.ItemID;
            textBoxName.Text = TmpItem.Name;
            textBoxItemInfo.Text = TmpItem.Info;
            setShow();
        }//加载
        public FrmItem(int id, FrmMain frmmain, Lang lang)
        {
            this.DialogResult = DialogResult.Cancel;
            InitializeComponent();
            //语言操作
            if (lang != null && !lang.Default)
            {
                this.lang = lang;
                Translate(lang);
            }
            frmMain = frmmain;
            Templates = frmMain.Templates.ToArray();
            MSetins = frmMain.MSetins.ToArray();
            Nowed = id;
            addCustomStuffToolStripMenuItem.Visible = false;
            relstmp();
        }//新建
        void relstmp()//刷新 comboBoxItemTemplare
        {
            comboBoxItemTemplare.Items.Clear();
            foreach (Template tm in Templates)
            {
                if (tm.Error == "")
                    comboBoxItemTemplare.Items.Add(tm.Name);
            }
        }

        /// <summary>
        /// 该Form的翻译方法
        /// </summary>
        /// <param name="lang">语言</param>
        public void Translate(Lang lang)
        {
            lang.Translate(this);
            //手动添加进行修改 例如 menu
            foreach (Line line in lang.FindLangForm(this).FindGroupLine("menu"))
                foreach (var tmp in menuStrip1.Items.Find(line.Info, true))
                {
                    tmp.Text = line.Text;
                }
            foreach (Line line in lang.FindLangForm(this).FindGroupLine(".ToolTip"))
            {
                foreach (var tmp in this.Controls.Find(line.Info.Split('.')[0], true))
                {
                    toolTip1.SetToolTip(tmp, line.Text);
                }
            }
        }
        private void DelectModSetin(MSetin ms)
        {
            int id = TmpItem.Data.FindIndex(x => x.Setin == ms);
            flowLayoutPanelShow.Controls.RemoveAt(id);
            TmpItem.Data.RemoveAt(id);
        }

        int odsl = -1;//防止选择到同一个index时刷新
        private void comboBoxItemTemplare_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxItemTemplare.SelectedIndex == odsl)
                return;
            else
                odsl = comboBoxItemTemplare.SelectedIndex;
            addCustomStuffToolStripMenuItem.Visible = true;
            Template tm = Templates[odsl];
            TmpItem = new ModItem(Nowed, "", "", tm, MSetins);
            textBoxName.Text = TmpItem.UseTemp.Name;
            textBoxItemInfo.Text = TmpItem.UseTemp.Info;
            setShow();
        }
        public void setShow()//展示全部的数据
        {
            //try
            //{
            //    pictureBoxdisplay.Image = TmpItem.UseTemp.DemoImage;
            //}
            //catch
            //{
            //    pictureBoxdisplay.Image = Properties.Resources.nomal_image;
            //}
            pictureBoxdisplay.Image = TmpItem.GetImage.DemoImage;

            toolTip1.SetToolTip(labelT2, "This Templare is Made By " + TmpItem.UseTemp.Author);
            //展示数据
            flowLayoutPanelShow.Controls.Clear();
            UCSettingItem uC;
            foreach (ModSet ms in TmpItem.Data)
            {
                uC = new UCSettingItem(ms, ChangeText, DelectModSetin);
                flowLayoutPanelShow.Controls.Add(uC);
            }
        }
        public void ChangeText(string str) => textBoxlable.Text = str;

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

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }
        private void saveAndCloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Save();
            Close();
        }
        private void dontSaveAndCloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            Close();
        }
        private void addCustomStuffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmModSet modSet = new FrmModSet(MSetins, lang);
            if (modSet.ShowDialog() == DialogResult.OK)
            {
                if (TmpItem.Data.Find(x => x.Setin.Name == modSet.NowSelect.Name) != null)
                {
                    MessageBox.Show("It Have Same Stuff in this Items");
                    return;
                }
                ModSet ms = new ModSet()
                {
                    Setin = modSet.NowSelect,
                    valuenomal = modSet.NowSelect.valuenomal,
                    Value = modSet.NowSelect.valuenomal,
                };
                TmpItem.Data.Add(ms);
                UCSettingItem uC = new UCSettingItem(ms, ChangeText, DelectModSetin);
                flowLayoutPanelShow.Controls.Add(uC);
                flowLayoutPanelShow.ScrollControlIntoView(uC);
            }
        }

        private void buttonChooseImage_Click(object sender, EventArgs e)
        {
            if (TmpItem.UseTemp.Prefix == "armour_")
            {
                MessageBox.Show("Armour is Not Support Now\nSubscribe and Thumbs-up on steam To Let author Update faster");
                return;
            }
            FrmImage Fi = new FrmImage(frmMain.TempImages);
            if (Fi.ShowDialog() == DialogResult.OK)
            {
                if(Fi.NowSelect.VirtualSize != TmpItem.UseTemp.Image.VirtualSize)
                {
                    MessageBox.Show($"This Item Image is {Fi.NowSelect.VirtualSize.ToString()}\n You Select {TmpItem.UseTemp.Image.VirtualSize.ToString()}","Size Error");
                    return;
                }
                TmpItem.ItemImage = Fi.NowSelect;
                setShow();
            }
        }
    }
}
