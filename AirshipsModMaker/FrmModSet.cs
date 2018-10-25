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
    public partial class FrmModSet : Form
    {
        List<MSetin> MSetins;
        MSetin[] NowShowMS;
        public MSetin NowSelect;
        public FrmModSet(MSetin[] mSetins)
        {
            InitializeComponent();
            MSetins = mSetins.ToList();
            this.DialogResult = DialogResult.None;
            Display();
        }

        private void FrmModSet_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.None)
                this.DialogResult = DialogResult.Cancel;
        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            if (NowSelect == null)
            {
                MessageBox.Show("You Select Nothing");
                return;
            }
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void listBoxSelect_Click(object sender, EventArgs e)
        {
            if (listBoxSelect.SelectedIndex == -1)
                return;
            else
                NowSelect = NowShowMS[listBoxSelect.SelectedIndex];

            textBoxlable.Text = $"{NowSelect.Mname}\r\nFrom: {NowSelect.From}\r\nExample Value: {NowSelect.valuenomal}\r\n\r\n{NowSelect.info}";
        }

        private void textBoxSeach_TextChanged(object sender, EventArgs e)
        {
            Display();
        }
        public void Display()
        {
            listBoxSelect.Items.Clear();
            if (textBoxSeach.Text == "")
                NowShowMS = MSetins.ToArray();
            else
                NowShowMS = MSetins.FindAll(x => x.Mname.ToLower().Contains(textBoxSeach.Text.ToLower())).ToArray();
            foreach (MSetin MS in NowShowMS)
            {
                listBoxSelect.Items.Add(MS.Mname);
            }
        }
    }
}
