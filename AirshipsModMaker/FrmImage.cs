using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirshipsModMaker
{
    public partial class FrmImage : Form
    {
        private List<TempImage> TempImages = new List<TempImage>();
        TempImage[] NowShowTI;
        public TempImage NowSelect;

        public FrmImage(List<TempImage> tempImages)
        {
            InitializeComponent();
            TempImages = tempImages;
            this.DialogResult = DialogResult.None;
            Display();
        }
        public void Display()
        {
            listBoxSelect.Items.Clear();
            if (textBoxSeach.Text == "")
                NowShowTI = TempImages.ToArray();
            else
                NowShowTI = TempImages.FindAll(x => x.Name.ToLower().Contains(textBoxSeach.Text.ToLower())).ToArray();
            foreach (TempImage TI in NowShowTI)
            {
                listBoxSelect.Items.Add(TI.Name);
            }
        }

        private void listBoxSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxSelect.SelectedIndex == -1)
                return;
            else
                NowSelect = NowShowTI[listBoxSelect.SelectedIndex];
            pictureBoxDisplay.Image = NowSelect.DemoImage;
            labelName.Text = NowSelect.Name;
            labelSize.Text = $"Size: { NowSelect.VirtualSize.Width.ToString()}x{NowSelect.VirtualSize.Height.ToString()}\nReal:{ NowSelect.RealSize.Width.ToString()}x{NowSelect.RealSize.Height.ToString()}";
        }
        private void FrmModSet_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.None)
                this.DialogResult = DialogResult.Cancel;
        }
        private void textBoxSeach_TextChanged(object sender, EventArgs e)
        {
            Display();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (NowSelect == null)
                return;
            TempImages.Remove(NowSelect);
            pictureBoxDisplay.Image = null;
            if (!NowSelect.Path.ToLower().EndsWith("logo.png"))
                try
                {
                    new FileInfo(NowSelect.Path).Delete();
                }
                catch { MessageBox.Show("Delete Fail!"); return; }
            NowSelect = null;
            MessageBox.Show("Delete Success!");
            Display();
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

        private void buttonGetImage_Click(object sender, EventArgs e)
        {
            if (NowSelect == null)
                return;
            saveFileDialog1.FileName = NowSelect.Name + ".png";
            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            FileInfo fi = new FileInfo(NowSelect.Path);
            fi.CopyTo(saveFileDialog1.FileName);
            MessageBox.Show("Get Image Success!");
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            TempImage Ti = new TempImage(openFileDialog1.FileName);
            if (Ti.Path == "ERROR")
            {
                MessageBox.Show("Image Demand:Length / Height pixel is Multiple of 16\n(16 pixel=1 Module length)", "Import Fail");
                return;
            }
            FileInfo fi = new FileInfo(openFileDialog1.FileName);
            try
            {
                fi.CopyTo(Program.PathImage + '\\' + fi.Name, false);
            }
            catch
            {
                MessageBox.Show("There Have SameName Image, Please ReName and try again", "Import Fail");
            }
            MessageBox.Show("Import Success");
            TempImages.Add(new TempImage(Program.PathImage + '\\' + fi.Name));
            Display();
        }
    }
}
