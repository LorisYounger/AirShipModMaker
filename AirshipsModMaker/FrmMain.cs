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
        List<Template> Templates = new List<Template>();
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
                    di.MoveTo(Program.PathMain);
                else
                    info.Create();
            }
#endif
            if (info.GetDirectories().Length == 0)//检查有没有模板
            {
#if !SAFE
                FrmTemplate.UpdateTemplates(new DirectoryInfo(Environment.CurrentDirectory + @"\Template\"));
                if (info.GetDirectories().Length == 0)
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
                if (data.Modlogo != "" && new FileInfo(data.Modlogo).Exists)
                    pictureBoxModImage.Image = Image.FromFile(data.Modlogo);
                else
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
            FrmItem fi = new FrmItem(data.ItemID, Templates.ToArray(), MSetins.ToArray(), lang());
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
            FrmItem fi = new FrmItem(mi, Templates.ToArray(), MSetins.ToArray(), lang());
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

                fi.CopyTo(path + @"\logo.png");

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


                foreach (ModItem mi in data.modItems)
                {
                    di = new DirectoryInfo(path + @"\" + mi.UseTemp.Local);//检查文件夹是否存在
                    if (!di.Exists)
                        di.Create();

                    fi = new FileInfo(path + @"\" + mi.UseTemp.Local + @"\AMM" + mi.ItemID + ".json");//singlefile

                    /*LPSMReplace(mi.UseTemp.RepFile, mi.ToSubs()).Replace("|name:|", "AMM" + mi.ItemID);*/
                    tmp.Clear();
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
                MessageBox.Show(ex.Message, ex.Source);
                return "";
            }
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
            new FrmTemplate(Templates.ToArray(), this, lang()).ShowDialog();
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
    }

    /// <summary>
    /// 整个Mod数据
    /// </summary>
    public class AirShipModData
    {
        public string ModName = "";
        public string ModInfo;
        public string Modlogo = "";
        public List<ModItem> modItems = new List<ModItem>();
        public int ItemID = 0;//用于编辑(自增)
        public string Outputid = new Random().Next().ToString("X");

        public string ToLps()
        {
            LpsDocument lps = new LpsDocument();
            lps.AddLine(new Line("airshipmod", "UserSave", "", new Sub("name", ModName), new Sub("info", ModInfo), new Sub("logo", Modlogo), new Sub("id", Outputid)));
            foreach (ModItem mi in modItems)
                lps.AddLine(mi.ToLine());
            return lps.ToString();
        }
        public AirShipModData() { }
        public AirShipModData(string lps, Template[] templates, MSetin[] MSetins)
        {
#if !DEBUG
            try
            {
#endif
                LpsDocument lpsd = new LpsDocument(lps);
                if (lpsd.Read().Name != "airshipmod" && lpsd.Read().Info.ToLower() != "usersave")
                    return;
                ModName = lpsd.Read().Find("name").Info;
                ModInfo = lpsd.Read().Find("info").Info;
                if (lpsd.Read().Find("id") != null)
                    Outputid = lpsd.Read().Find("id").info;//output id
                Modlogo = lpsd.ReadNext().Find("logo").Info;
                while (lpsd.ReadCanNext())
                    modItems.Add(new ModItem(ItemID++, lpsd.ReadNext(), templates, MSetins));
#if !DEBUG
            }
            catch
            {
                ModName = "";
            }
#endif
        }
    }
    /// <summary>
    /// Mod中的物品栏
    /// </summary>
    public class ModItem
    {
        public string Name;
        public string Info;
        public Template UseTemp;//所使用模板
        public List<ModSet> Data = new List<ModSet>();
        public int ItemID;
        //案例 name#data:|
        public Line ToLine()//转换成行
        {
            Line li = new Line("temp", UseTemp.Name, "", new Sub("name", Name), new Sub("info", Info));
            li.AddRange(ToSubs());
            return li;
        }
        public Sub[] ToSubs()//转换成Subs
        {
            List<Sub> subs = new List<Sub>();
            foreach (ModSet ms in Data)
                subs.Add(ms.ToSub());
            return subs.ToArray();
        }
        public ModItem(int id, Line line, Template[] templates, MSetin[] mSetins)//加载
        {
            ItemID = id;
            UseTemp = templates.FirstOrDefault(x => x.Name == line.Find("temp").Info);
            Name = line.Find("name").Info;
            Info = line.Find("info").Info;
            Line tmp;
            MSetin mstmp;
            for (int i = 2; i < line.Subs.Count; i++)
            {
                tmp = UseTemp.Data.FindLine(line.Subs[i].Name);
                if (tmp == null)
                {
                    mstmp = mSetins.FirstOrDefault(x => x.Name == line.Subs[i].Name);
                    if (mstmp == null)
                        mstmp = mSetins.FirstOrDefault(x => x.Name == line.Subs[i].Name);

                    Data.Add(new ModSet()
                    {
                        Setin = mSetins.First(x => x.Name == line.Subs[i].Name),
                        Value = line.Subs[i].Info,
                        valuenomal = mstmp.valuenomal
                    });
                }
                else
                {
                    Data.Add(new ModSet()
                    {
                        Setin = mSetins.First(x => x.Name == line.Subs[i].Name),
                        Value = line.Subs[i].Info,
                        valuenomal = tmp.Find("nomal").Info
                    });
                }
            }
        }

        //现在template 单独设置项所需内容为
        //name 用作定位(ps:直接就是名字)
        //nomal 标准值
        //skip 如有需要跳过读取

        public ModItem(int id, string name, string info, Template usrTemp, MSetin[] mSetins)//新建ModItem
        {
            ItemID = id;
            Name = name;
            Info = info;
            UseTemp = usrTemp;
            UseTemp.Data.LineNode = 1;
            while (UseTemp.Data.ReadCanNext())
                Data.Add(new ModSet
                {
                    Setin = mSetins.First(x => x.Name == UseTemp.Data.Read().Name),
                    Value = UseTemp.Data.Read().Find("nomal").Info,
                    valuenomal = UseTemp.Data.ReadNext().Find("nomal").Info,
                });
        }
        public ModItem(ModItem mi)
        {
            Name = mi.Name;
            Data = mi.Data.ToList();
            Info = mi.Info;
            ItemID = mi.ItemID;
            UseTemp = mi.UseTemp;
        }
    }
    /// <summary>
    /// Mod中的物品中的单项设置
    /// </summary>
    public class ModSet
    {
        public MSetin Setin;//这是固定信息，属于不会变的那种

        private string value;

        public string valuenomal;
        public string Value
        {
            get => value; set
            {
                switch (Setin.ModSetType)
                {
                    case MSetin.SetType.UINT:
                        if (IsUnsignInt(value))
                            this.value = value;
                        break;
                    case MSetin.SetType.INT:
                        if (IsInt(value))
                            this.value = value;
                        break;
                    case MSetin.SetType.NUMBER:
                        if (IsNumeric(value))
                            this.value = value;
                        break;
                    case MSetin.SetType.UNUMBER:
                        if (IsUNumeric(value))
                            this.value = value;
                        break;
                    default:
                        this.value = value;
                        break;
                }
            }
        }
        public static bool IsNumeric(string value)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(value, @"^-?([1-9]\d*\.\d*|0\.\d*[1-9]\d*|0?\.0+|0)$");
        }
        public static bool IsUNumeric(string value)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(value, @"^[1-9]\d*\.\d*|0\.\d*[1-9]\d*|0?\.0+|0$");
        }
        public static bool IsInt(string value)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(value, @"^-?[1-9]\d*|0$");
        }
        public static bool IsUnsignInt(string value)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(value, @"^[1-9]\d*|0$"); ;
        }
        public Sub ToSub()
        {
            return new Sub(Setin.Name, Value);
        }

    }
    ///// <summary>
    ///// 从全部MS中找到的
    ///// </summary>
    //public class TempMSetin
    //{


    //    public MSetin GetMSetin(string name)
    //    {
    //        return MSetins.FirstOrDefault(x => x.Name == name);
    //    }

    //    public void AddMsetin(MSetin mSetin)
    //    {
    //        if (GetMSetin(mSetin.Name) == null)
    //            MSetins.Add(mSetin);
    //    }

    //}
    //这是ModSet的固定信息
    public class MSetin
    {
        public SetType ModSetType = SetType.STRING;
        /// <summary>
        /// 名字
        /// </summary>
        public string Name;
        /// <summary>
        /// 给用户的名字
        /// </summary>
        public string Mname;
        public string info;
        /// <summary>
        /// is Not Obsolete,It Only No Recommend 
        /// </summary>
        //[Obsolete()]
        public string valuenomal;
        //来自哪里
        public string From;

        public ModSet ToModSet()
        {
            return new ModSet();
        }
        public enum SetType
        {
            UINT, INT, NUMBER, UNUMBER, STRING
        }

    }


    /// <summary>
    /// 模板文件
    /// </summary>
    public class Template
    {
        public string Name;
        public string Info;
        public string RepFile;//用来替换的文件
        public string Author;//作者
        public string Local;//位置
        public string Prefix;//前缀
        public string Verison = "N/A";
        public bool IsFlipped = false;//是不是IsFlipped
        public LpsDocument Data;//替换的数据
        public string path;
        public string Error = "";

        public Image DemoImage
        {
            get => Image.FromFile(path + @"\logo.png");
        }

        public Template(string path, List<MSetin> mSetins)
        {
            try
            {
                this.path = path;
                FileInfo fi = new FileInfo(path + @"\json.lpsm");
                StreamReader sr;
                sr = new StreamReader(fi.OpenRead(), System.Text.Encoding.UTF8);
                RepFile = sr.ReadToEnd().Replace("\r", "");
                sr.Dispose();

                //读取完成

                Data = new LpsDocument();
                fi = new FileInfo(path + @"\info.lps");
                sr = new StreamReader(fi.OpenRead(), System.Text.Encoding.UTF8);
                Data = new LpsDocument(sr.ReadToEnd());
                sr.Dispose();
                sr = null;

                //判断版本
                if (Properties.Settings.Default.VersionCheck)
                {
                    if (Data.Read().Find("ver") != null)
                        Verison = Data.Read().Find("ver").GetInfo();
                    if (!(ModSet.IsUNumeric(Verison) && Convert.ToDouble(Verison) >= 2))
                    {
                        Error = "Verison too old";
                    }
                    else if (Convert.ToDouble(Verison) >= 3)
                    {
                        Error = "Verison too High, Upgreat software to support";
                    }
                }

                //获取信息

                Name = Data.Read().Find("name").GetInfo();
                Info = Data.Read().Find("info").GetInfo();
                Author = Data.Read().Find("author").GetInfo();
                Local = Data.Read().Find("local").GetInfo();
                Prefix = Data.Read().Find("prefix").GetInfo();
                IsFlipped = (Data.Read().Find("flipped") != null);


                //加载到MSetins

                Data.LineNode = 1;
                MSetin tmp;
                while (Data.ReadCanNext())
                {
                    tmp = mSetins.FirstOrDefault(x => x.Name == Data.Read().Name);
                    if (tmp == null && Data.Read().Find("skip") == null)//如果读取到跳过，则跳过:(有引发bug的可能性)
                    {
                        mSetins.Add(new MSetin()
                        {
                            Name = Data.Read().Name,
                            info = Data.Read().Find("info").info,
                            valuenomal = Data.Read().Find("nomal").info,
                            ModSetType = (MSetin.SetType)Enum.Parse(typeof(MSetin.SetType), Data.Read().Find("type").info.ToUpper()),
                            Mname = Data.Read().Find("name").info,
                            From = Name
                        });
                    }
                    else
                    {
                        Data.LineNode++;
                    }
                }

            }
            catch (Exception e)
            {
                Error = e.Message;//cath error
            }
        }
    }
}
