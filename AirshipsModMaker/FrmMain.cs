using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using LinePutScript;
using System.Text;
using System.Threading;
using MultiLang;

namespace AirshipsModMaker
{
    public partial class FrmMain : Form
    {
        AirShipModData data = new AirShipModData();//当前存档 Current archive

        //全部模板 All templates
        public List<Template> Templates = new List<Template>();
        public List<TempImage> TempImages = new List<TempImage>();

        //子项集合 Subitem set
        public List<MSetin> MSetins = new List<MSetin>();
        string SavePath = "";//保存位置 Save position


        //语言项目
        public List<Lang> Langs = new List<Lang>();

        /// <summary>
        /// 该Form的翻译方法 The Translation Method of the Form
        /// </summary>
        /// <param name="lang">语言 language</param>
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
            //版本号加上
            this.Text += Program.Verizon;
#if SAFE
            this.Text += " (Safe Mode)";
#endif
        }

        public Lang lang()
        {
            return Langs.Find(x => x.Language == Properties.Settings.Default.Lang);
        }
        public void LangClick(object sender, System.EventArgs e)
        {
            ToolStripMenuItem mi = (ToolStripMenuItem)sender;
            Properties.Settings.Default.Lang = mi.Text;
            Properties.Settings.Default.Save();
            var lang = Langs.Find(x => x.Language == mi.Text);
            Translate(lang);
        }
        public FrmMain()
        {
            InitializeComponent();
            //chack update first
#if !DEBUG
            string update = UpdateCheck();
            if (update != "")
            {
                if (MessageBox.Show(update + "\nUpdate Now?", "There have new Update", MessageBoxButtons.YesNo) == DialogResult.Yes)
#if SAFE
                    System.Diagnostics.Process.Start(@"http://download.exlb.org/AirShipModMaker/AirShipModMaker_SafeMode.zip");
#else
                    System.Diagnostics.Process.Start(@"http://download.exlb.org/AirShipModMaker/AirShipModMaker.zip");
#endif
            }
#endif
            //多语言支持

            //收集全部语言
            DirectoryInfo info = new DirectoryInfo(Application.StartupPath + @"\lang");
            if (info.Exists)
            {
                Lang tmp;
                StreamReader sr;
                foreach (FileInfo fi in info.GetFiles("*.lang"))
                {
                    sr = new StreamReader(fi.OpenRead(), Encoding.UTF8);
                    tmp = new Lang(sr.ReadToEnd(), "AirshipsModMaker");
                    sr.Close();
                    sr.Dispose();
                    if (!tmp.Language.Contains("ERROR"))
                    {
                        Langs.Add(tmp);
                        languageToolStripMenuItem.DropDownItems.Add(Langs.Last().Language, null, LangClick);
                    }
                }
                //加载语言选项
                if (Properties.Settings.Default.Lang != "")
                {
                    var lang = Langs.Find(x => x.Language == Properties.Settings.Default.Lang);
                    if (lang != null)
                    {
                        if (!lang.Default)//判断是不是主语言，如果是，就不翻译(节约资源)
                            Translate(lang);
                        else
                        {
                            this.Text += Program.Verizon;
#if SAFE
                            this.Text += " (Safe Mode)";
#endif
                        }
                    }
                    else
                        Properties.Settings.Default.Lang = "";
                }
                else
                {
                    this.Text += Program.Verizon;
#if SAFE
                    this.Text += " (Safe Mode)";
#endif
                }
            }
            else
            {
                this.Text += Program.Verizon;
#if SAFE
                this.Text += " (Safe Mode)";
#endif
            }
            //设置项目加载
            versionChackToolStripMenuItem.Checked = Properties.Settings.Default.VersionCheck;



            //新建文件夹
            info = new DirectoryInfo(Program.PathMain);
#if !SAFE            
            if (!info.Exists)
            {
                MessageBox.Show("You can creat mods with using this tool\nIf you think this tool is not bad, Subscribe and Thumbs-up on steam", "Thanks For Using AirshipsModMaker");
                DirectoryInfo di = new DirectoryInfo(Environment.CurrentDirectory + @"\Template\");
                if (di.Exists)
                {
                    info.Parent.Create();
                    MoveFolder(di, Program.PathMain);//防止出 试图将一个目录移到不同的卷。 IOException
                }
                else
                    info.Create();
            }
            else
            {
                DirectoryInfo di = new DirectoryInfo(Environment.CurrentDirectory + @"\Template\");
                if (di.Exists)
                {
                    FrmTemplate.UpdateTemplates(di);
                }
            }


#endif
            if (info.GetDirectories().Length == 0)//检查有没有模板
            {
#if !SAFE
                FrmTemplate.UpdateTemplates(new DirectoryInfo(Environment.CurrentDirectory + @"\Template\"));
                if (info.GetDirectories().Length == 0)//第二次检查
                {

                    MessageBox.Show("Template No find\nPlase choose Tempalte/Tempaltes folder", "Template No find");
                    if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                    {
                        FrmTemplate.UpdateTemplates(new DirectoryInfo(folderBrowserDialog1.SelectedPath));
                        Program.Reload = true;
                    }
                    Close();
                }
#else
                MessageBox.Show("Template No find", "Safe Mod");
                Close();
#endif
            }
            //加载Template
            foreach (DirectoryInfo di in info.GetDirectories())
            {
                Templates.Add(new Template(di.FullName, MSetins));
            }
            //加载图片库
            info = new DirectoryInfo(Program.PathImage);
            if (!info.Exists)
            {
                info.Create();
            }
            else
            {
                foreach (FileInfo fi in info.GetFiles().Where(x => x.Extension == ".png"))
                    TempImages.Add(new TempImage(fi.FullName));
            }
            foreach (Template tmp in Templates.FindAll(x => x.Error == ""))
            {
                TempImages.Add(tmp.Image);
            }
            for (int i = 0; i < TempImages.Count; i++)
            {
                if (TempImages[i].Path == "ERROR")
                {
                    TempImages.Remove(TempImages[i]);
                    i--;
                }
            }
        }


        private void inputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Program.PathMain);
            //MessageBox.Show("Copy Template Directory to this path, Then restart the tool", "input Template");
        }


        bool isChange = false;//判断是否修改过
        public bool Save()
        {
            if (SavePath != "")
            {
                Save(SavePath);
                return true;
            }
            else
            {
                saveFileDialog1.FileName = textBoxModName.Text;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    SavePath = saveFileDialog1.FileName;
                    Save(SavePath);
                    return true;
                }
            }
            return false;
        }
        public void Save(string path)
        {
            isChange = false;
            //将全部数据导入data
            data.ModName = textBoxModName.Text;
            data.ModInfo = textBoxModInfo.Text;


            //文件操作
            FileInfo fi = new FileInfo(path);
            FileStream fs = fi.Create();
            byte[] buff = Encoding.UTF8.GetBytes(data.ToLps());
            fs.Write(buff, 0, buff.Length);
            fs.Close();
            fs.Dispose();
            SavePath = openFileDialog1.FileName;
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isChange)
                switch (MessageBox.Show("You Will Lost All Work You Didn't Save!\nSave Now?or Cancel Close This Tool", "AirShips Mod Maker Are Closeing", MessageBoxButtons.YesNoCancel))
                {
                    case DialogResult.Yes:
                        Save();
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
        }

        private void SavetoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (Save())
                MessageBox.Show("SaveSuccess");
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = textBoxModName.Text;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                SavePath = saveFileDialog1.FileName;
                Save(SavePath);
                MessageBox.Show("SaveSuccess");
                return;
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileInfo fi = new FileInfo(openFileDialog1.FileName);
                StreamReader sr = new StreamReader(fi.OpenRead(), Encoding.UTF8);
                AirShipModData tmp = new AirShipModData(sr.ReadToEnd(), Templates.ToArray(), MSetins.ToArray());
                sr.Close();
                sr.Dispose();
                if (tmp.ModName == "")
                {
                    MessageBox.Show("This File Is Not AirShipModMaker's File \nor File Corrupted \nor Do not have Template what File needed", "File Open Fail");
                    return;
                }
                data = tmp;
                textBoxModName.Text = data.ModName;
                textBoxModInfo.Text = data.ModInfo;
                try
                {
                    if (data.Modlogo != "" && new FileInfo(data.Modlogo).Exists)
                        pictureBoxModImage.Image = Image.FromFile(data.Modlogo);
                    else
                    {
                        data.Modlogo = "";
                        pictureBoxModImage.Image = Properties.Resources.nomal_image;
                    }
                }
                catch
                {
                    data.Modlogo = "";
                    pictureBoxModImage.Image = Properties.Resources.nomal_image;
                }

                //item
                listView1.Items.Clear();
                foreach (ModItem mi in data.modItems)
                {
                    listView1.Items.Add(mi.ItemID.ToString());
                    listView1.Items[listView1.Items.Count - 1].SubItems.Add(mi.Name);
                    listView1.Items[listView1.Items.Count - 1].SubItems.Add(mi.UseTemp.Name);
                    listView1.Items[listView1.Items.Count - 1].SubItems.Add(mi.Info);
                }

                SavePath = openFileDialog1.FileName;
            }
        }

        private void pictureBoxModImage_Click(object sender, EventArgs e)
        {
            if (openFileDialogimage.ShowDialog() == DialogResult.OK)
            {
                data.Modlogo = openFileDialogimage.FileName;
                pictureBoxModImage.Image = Image.FromFile(data.Modlogo);
            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isChange = true;
            FrmItem fi = new FrmItem(data.ItemID, this, lang());
            if (fi.ShowDialog() == DialogResult.OK)
            {
                data.modItems.Add(fi.ModItem);

                listView1.Items.Add(fi.ModItem.ItemID.ToString());
                listView1.Items[listView1.Items.Count - 1].SubItems.Add(fi.ModItem.Name);
                listView1.Items[listView1.Items.Count - 1].SubItems.Add(fi.ModItem.UseTemp.Name);
                listView1.Items[listView1.Items.Count - 1].SubItems.Add(fi.ModItem.Info);
                //序列+1
                data.ItemID++;
            }
        }

        private void textBoxModName_TextChanged(object sender, EventArgs e)
        {
            isChange = true;
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count == 0)
                return;

            int id = listView1.SelectedIndices[0];
            ModItem mi = data.modItems[id];
            isChange = true;
            FrmItem fi = new FrmItem(mi, this, lang());
            if (fi.ShowDialog() == DialogResult.OK)
            {
                data.modItems[id] = fi.ModItem;
                listView1.Items[id].SubItems[1].Text = fi.ModItem.Name;
                listView1.Items[id].SubItems[2].Text = fi.ModItem.UseTemp.Name;
                listView1.Items[id].SubItems[3].Text = fi.ModItem.Info;
                ////序列+1
                //data.ItemID++;
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count == 0)
                return;

            int id = listView1.SelectedIndices[0];

            if (MessageBox.Show("Operation cannot be cancelled\n Are you Sure Delete This Item", "You are Try to Delete '" +
                data.modItems[id].Name + "'", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                data.modItems.RemoveAt(id);
                listView1.Items.RemoveAt(id);
            }
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isChange)
                Save();
#if SAFE
            folderBrowserDialog1.SelectedPath = Environment.CurrentDirectory;
#else
            folderBrowserDialog1.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\AirshipsGame\mods\";
#endif

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                FileInfo fi;
                if (data.Modlogo != "")
                {
                    fi = new FileInfo(data.Modlogo);
                    if (!fi.Exists)
                    {
                        MessageBox.Show("Mod Logo image was Lost");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Mod Logo image was Lost");
                    return;
                }

                data.ModName = textBoxModName.Text;
                data.ModInfo = textBoxModInfo.Text;

                string path = folderBrowserDialog1.SelectedPath + $"\\{data.ModName}";

                DirectoryInfo di = new DirectoryInfo(path);
                if (!di.Exists)
                    di.Create();
                else
                {
                    if (MessageBox.Show("There have Same Name folder,Do you want to cover it?", "Export", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        di.Delete(true);
                        Thread.Sleep(10);//if Delete it, it will have bug
                        di.Create();
                    }
                    else
                        return;
                }
                //先创建文件和图片
                StringBuilder lang = new StringBuilder();//strings

                fi.CopyTo(path + @"\logo.png", true);

                //lang.AppendLine($"modulecategory_AMM{data.Outputid}={data.ModName.Replace("\r", "").Replace("\n", @"\n")}");

                StringBuilder tmp = new StringBuilder(LPSMReplace(Properties.Resources.info, new Sub("id", data.Outputid),
                    new Sub("name", data.ModName.Replace("\r", "").Replace("\n", @"\n")),
                    new Sub("info", data.ModInfo.Replace("\r", "").Replace("\n", @"\n"))));//info.json

                fi = new FileInfo(path + @"\info.json");
                FileStream fs = fi.Create();
                byte[] buff = Encoding.UTF8.GetBytes(tmp.ToString());
                fs.Write(buff, 0, buff.Length);
                fs.Close();
                fs.Dispose();

                //给图片准备好文件夹
                di = new DirectoryInfo(path + @"\SpritesheetBundle");
                if (!di.Exists)
                    di.Create();
                di = new DirectoryInfo(path + @"\images");
                if (!di.Exists)
                    di.Create();

                //图片加载位置准备好
                fi = new FileInfo(path + @"\SpritesheetBundle\bunck.json");
                fs = fi.Create();
                buff = Encoding.UTF8.GetBytes(Properties.Resources.bunck.Replace("|id:|", data.Outputid));
                fs.Write(buff, 0, buff.Length);
                fs.Close();
                fs.Dispose();

                //开始准备图片
                Properties.Resources.modmaker_b.Save(path + $"\\images\\modmaker_b{data.Outputid}.png");
                Properties.Resources.modmaker_f.Save(path + $"\\images\\modmaker_f{data.Outputid}.png");

                //一会要使用的图片和参数
                int NowX = 0, NowY = 0, NextY = 0;//X用于确定位置，Y用于确定下一次位置，当X>=30的时候,跳到NowY+NextY,X=0
                Bitmap BM = new Bitmap(Properties.Resources.modmaker);
                Graphics graphics = Graphics.FromImage(BM);

                foreach (ModItem mi in data.modItems)
                {
                    di = new DirectoryInfo(path + @"\" + mi.UseTemp.Local);//检查文件夹是否存在
                    if (!di.Exists)
                        di.Create();

                    fi = new FileInfo(path + @"\" + mi.UseTemp.Local + @"\AMM" + mi.ItemID + ".json");//singlefile

                    tmp.Clear();//清空文字缓存


                    if (mi.UseTemp.Prefix == "mod_")//不支持盔甲
                    {
                        //图片处理
                        graphics.DrawImage(mi.GetImage.DemoImage, NowX, NowY, mi.GetImage.RealSize.Width, mi.GetImage.RealSize.Height);

                        //输出信息
                        tmp.AppendLine($"\"appearance\": {{\"src\": \"modmaker{data.Outputid}\",\"x\": {NowX / 16},\"y\": {NowY / 16},\"w\": {mi.GetImage.VirtualSize.Width},\"h\": {mi.GetImage.VirtualSize.Height}}},");

                        //下一步骤
                        NowX += mi.GetImage.RealSize.Width;
                        if (NextY < mi.GetImage.RealSize.Height)
                            NextY = mi.GetImage.RealSize.Height;
                        if (NowX >= 480)//30*16
                        {
                            NowX = 0;
                            NowY += NextY;
                            NextY = 0;
                        }
                    }
                    /*LPSMReplace(mi.UseTemp.RepFile, mi.ToSubs()).Replace("|name:|", "AMM" + mi.ItemID);*/

                    foreach (ModSet ms in mi.Data)
                    {
                        tmp.Append($"\"{ms.Setin.Name}\": ");
                        if (ms.Setin.ModSetType == MSetin.SetType.STRING)
                            tmp.AppendLine($"\"{ms.Value}\",");
                        else
                            tmp.AppendLine(ms.Value + ',');
                    }


                    fs = fi.Create();
                    buff = Encoding.UTF8.GetBytes(mi.UseTemp.RepFile.Replace("|name:|", "AMM" + data.Outputid + mi.ItemID).Replace("|lpsm:|", tmp.ToString()));
                    fs.Write(buff, 0, buff.Length);
                    fs.Close();
                    fs.Dispose();

                    //lang
                    if (mi.UseTemp.Prefix == "mod_")
                        lang.AppendLine($"mod_AMM{data.Outputid}{mi.ItemID}={mi.Name}\r\nmod_desc_AMM{data.Outputid}{mi.ItemID}={mi.Info.Replace("\r", "").Replace("\n", @"\n")}");
                    else
                        lang.AppendLine($"{mi.UseTemp.Prefix}AMM{data.Outputid}{mi.ItemID}={mi.Name}\r\n{mi.UseTemp.Prefix}AMM{data.Outputid}{mi.ItemID}_desc={mi.Info.Replace("\r", "").Replace("\n", @"\n")}");

                    if (mi.UseTemp.IsFlipped)
                        lang.AppendLine($"{mi.UseTemp.Prefix}FLIPPED_AMM{data.Outputid}{mi.ItemID}={mi.Name}(Flipped)\r\n{mi.UseTemp.Prefix}desc_FLIPPED_AMM{data.Outputid}{mi.ItemID}={mi.Info.Replace("\r", "").Replace("\n", @"\n")}");
                }
                //图片处理
                BM.Save(path + $"\\images\\modmaker{data.Outputid}.png");

                //lang
                di = new DirectoryInfo(path + @"\strings");
                di.Create();
                fi = new FileInfo(path + @"\strings\en.properties");
                fs = fi.Create();
                buff = Encoding.UTF8.GetBytes(lang.ToString());
                fs.Write(buff, 0, buff.Length);
                fs.Close();
                fs.Dispose();

                MessageBox.Show("Export Success!");
                //Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\AirshipsGame\mods\"              
#if SAFE
                MessageBox.Show($"If you want to use this mod,You need copy \n'{path}'\nto\n'{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) }\\AirshipsGame\\mods\'", "Safe Mode Prompt", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
#endif
            }
        }

        public string LPSMReplace(string lpsm, params Sub[] subs)
        {
            foreach (Sub sub in subs)
            {
                lpsm = lpsm.Replace($"|{sub.Name}:|", sub.Info);
            }
            return lpsm;
        }

        private void onGitHubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"https://github.com/LorisYounger/AirShipModMaker/issues");
        }

        private void authorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("AirShip Mod Maker is Made by Loris\nIf you think this tool is not bad, Subscribe and Thumbs-up on steam", "Thanks For Using AirshipsModMaker");
        }

        private void sourceCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"https://github.com/LorisYounger/AirShipModMaker");
        }

        private void updateSoftwareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string update = UpdateCheck();
            if (update != "")
            {
                if (MessageBox.Show(update + "\nUpdate Now?", "There have new Update", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    System.Diagnostics.Process.Start(@"http://download.exlb.org/AirShipModMaker/AirShipModMaker.zip");
            }
            else
            {
                MessageBox.Show("Your Software are up-to-date", "Update Check");
            }
        }
    
        public string UpdateCheck()
        {
            string ReadText;
            try
            {
                string UrlAdress = "http://download.exlb.org/verizon.asp?airshipmodmaker";//测试版
                System.IO.Stream stream = System.Net.WebRequest.Create(UrlAdress).GetResponse().GetResponseStream();
                //注意urladress为你上面的网页地址。
                StreamReader sr = new StreamReader(stream, System.Text.Encoding.UTF8);
                ReadText = sr.ReadToEnd(); //由于这里并非读取全部文件，这里正常为空
                sr.Dispose(); //关闭流
                ReadText = System.Uri.UnescapeDataString(ReadText);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nForm:\n" + ex.Source, "{Plese Send to Author} Update Check Web Error" );
                return "";
            }
            try
            {
                string[] tmps = ReadText.Split('\n');
                if (tmps.First().Contains(Program.Verizon))
                    return "";
                ReadText = "What's New";
                foreach (string tmp in tmps)
                {
                    if (!tmp.Contains(Program.Verizon))
                    {
                        ReadText += "\n" + tmp;
                    }
                    else
                        break;
                }
                return ReadText;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nForm:\n" + ex.Source, "{Plese Send to Author} Update Check Error: " + ex.Source);
                return "";
            }
        }

        public static void MoveFolder(DirectoryInfo di,string path)
        {
            CopyFolder(di.FullName, path);
            di.Delete(true);
        }

        public static void CopyFolder(string strFromPath, string strToPath)
        {
            //如果源文件夹不存在，则创建
            if (!Directory.Exists(strFromPath))
            {
                Directory.CreateDirectory(strFromPath);
            }
            //取得要拷贝的文件夹名
            string strFolderName = strFromPath.Substring(strFromPath.LastIndexOf("\\") +
              1, strFromPath.Length - strFromPath.LastIndexOf("\\") - 1);
            //如果目标文件夹中没有源文件夹则在目标文件夹中创建源文件夹
            if (!Directory.Exists(strToPath + "\\" + strFolderName))
            {
                Directory.CreateDirectory(strToPath + "\\" + strFolderName);
            }
            //创建数组保存源文件夹下的文件名
            string[] strFiles = Directory.GetFiles(strFromPath);
            //循环拷贝文件
            for (int i = 0; i < strFiles.Length; i++)
            {
                //取得拷贝的文件名，只取文件名，地址截掉。
                string strFileName = strFiles[i].Substring(strFiles[i].LastIndexOf("\\") + 1, strFiles[i].Length - strFiles[i].LastIndexOf("\\") - 1);
                //开始拷贝文件,true表示覆盖同名文件
                File.Copy(strFiles[i], strToPath + "\\" + strFolderName + "\\" + strFileName, true);
            }
            //创建DirectoryInfo实例
            DirectoryInfo dirInfo = new DirectoryInfo(strFromPath);
            //取得源文件夹下的所有子文件夹名称
            DirectoryInfo[] ZiPath = dirInfo.GetDirectories();
            for (int j = 0; j < ZiPath.Length; j++)
            {
                //获取所有子文件夹名
                string strZiPath = strFromPath + "\\" + ZiPath[j].ToString();
                //把得到的子文件夹当成新的源文件夹，从头开始新一轮的拷贝
                CopyFolder(strZiPath, strToPath + "\\" + strFolderName);
            }
        }
        private void getMoreTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"http://download.exlb.org/?rootPath=./AirShipModMaker/Templates");
        }

        private void onStreamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"https://steamcommunity.com/sharedfiles/filedetails/?id=1533155962");
        }

        private void subscribeAtSteamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"https://steamcommunity.com/sharedfiles/filedetails/?id=1533155962");
        }

        private void templateManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmTemplate(this, lang()).ShowDialog();
        }

        private void howToUseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"https://www.exlb.org/airshipmodmaker/");
        }

        private void stuffManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tmp = new FrmModSet(MSetins.ToArray(), lang());
            tmp.buttonSelect.Visible = false;
            tmp.ShowDialog();
        }

        private void versionChackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.VersionCheck = !Properties.Settings.Default.VersionCheck;
            versionChackToolStripMenuItem.Checked = Properties.Settings.Default.VersionCheck;
        }

        private void imageManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tmp = new FrmImage(TempImages);
            tmp.buttonSelect.Visible = false;
            tmp.ShowDialog();
        }
    }

}
