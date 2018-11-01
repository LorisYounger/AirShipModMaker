using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using LinePutScript;
using MultiLang;
namespace AirshipsModMaker
{
    public partial class FrmTemplate : Form
    {
        Template[] Templates;
        FrmMain frmMain;
        Lang lang;
        public FrmTemplate(Template[] templates, FrmMain frmMain,Lang lang)
        {
            InitializeComponent();
            Templates = templates;
            this.frmMain = frmMain;
            //语言操作
            if (lang != null && !lang.Default)
            {
                this.lang = lang;
                Translate(lang);
            }
            Rels();
        }
        /// <summary>
        /// 该Form的翻译方法
        /// </summary>
        /// <param name="lang">语言</param>
        public void Translate(Lang lang)
        {
            lang.Translate(this);
            //手动添加进行修改 例如 menu
            foreach (Line line in lang.FindLangForm(this).FindGroupLine("ToolStrip"))
            {
                foreach (var tmp in contextMenuStrip1.Items.Find(line.Info, true))
                {
                    tmp.Text = line.Text;
                }
            }
            foreach (ColumnHeader va in listView1.Columns)
            {
                var tmps = lang.FindLangForm(this).FindGroupLine("Header");
                var tmp = tmps.Find(x => x.Info == (string)va.Tag);
                if (tmp != null)
                    va.Text = tmp.Text;
            }
        }
        public void Rels()
        {
            listView1.Items.Clear();
            foreach (Template tmp in Templates)
            {
                listView1.Items.Add(tmp.Name);
                listView1.Items[listView1.Items.Count - 1].SubItems.Add(tmp.Author);
                listView1.Items[listView1.Items.Count - 1].SubItems.Add(tmp.Verison);
                if (!new DirectoryInfo(tmp.path).Exists)
                {
                    listView1.Items[listView1.Items.Count - 1].BackColor = Color.LightSalmon;
                    listView1.Items[listView1.Items.Count - 1].SubItems.Add("be removed");
                }
                else if (tmp.Error != "")
                {
                    listView1.Items[listView1.Items.Count - 1].BackColor = Color.LightCoral;
                    listView1.Items[listView1.Items.Count - 1].SubItems.Add("Error:" + tmp.Error);
                }
                else
                {
                    listView1.Items[listView1.Items.Count - 1].SubItems.Add(tmp.Info);
                }
            }
        }

        //move templates or template folder to Software Use.
        public static void UpdateTemplates(DirectoryInfo Path)
        {
            if (!Path.Exists)
                return;
            FileInfo fi = new FileInfo(Path.FullName + @"\info.lps");
            if (fi.Exists)//copy
            {
                DirectoryInfo di = new DirectoryInfo(Program.PathMain + @"\" + Path.Name);
                if (di.Exists)
                {
                    di.Delete(true);
                    Thread.Sleep(10);//if Delete it, it will have bug
                }
                Path.MoveTo(Program.PathMain + @"\" + Path.Name);
            }
            else//copy all
            {
                foreach (DirectoryInfo di in Path.GetDirectories())
                    UpdateTemplates(di);
            }
        }

        private void addTemplatesToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                UpdateTemplates(new DirectoryInfo(folderBrowserDialog1.SelectedPath));
                MessageBox.Show("Templat Installed (if have)\nIf you want to Load 'template',you need to Reload", "Templat Installed (if have)");
            }
        }

        private void reMoveTemplateToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (listView1.SelectedIndices.Count == 0)
                return;
            if (listView1.SelectedItems[0].SubItems[3].Text != "be removed")
                if (MessageBox.Show("Operation cannot be cancelled\n Are you Sure Delete This Template", "You are Try to Delete '" +
                   listView1.SelectedItems[0].Text + "'", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    new DirectoryInfo(Templates[listView1.SelectedIndices[0]].path).Delete(true);
                    Thread.Sleep(10);//if Delete it, it will have bug
                    listView1.SelectedItems[0].BackColor = Color.LightSalmon;
                    listView1.SelectedItems[0].SubItems[3].Text = "be removed";
                }
        }

        private void showOnExplorerToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (listView1.SelectedIndices.Count == 0)
                return;

            System.Diagnostics.Process.Start(Templates[listView1.SelectedIndices[0]].path);
        }

        private void reloadToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("You Will Lost All Work You Didn't Save!\nSure to Reload Template", "Reload Template", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                frmMain.Close();
                Program.Reload = true;
                this.Close();
            }
        }
    }
}
