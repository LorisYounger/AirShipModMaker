using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using LinePutScript;
using System.Text;
using System.Threading;

namespace AirshipsModMaker
{
    public partial class FrmMain : Form
    {
        public static readonly string pathmain = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\LBSoft\Airship";

        AirShipModData data = new AirShipModData();//当前存档
        //全部模板
        List<Template> Templates = new List<Template>();
        string SavePath = "";//保存位置
        public FrmMain()
        {
            InitializeComponent();
            //new LpsDocument().AddLine(new Line(""));

            //新建文件夹
            DirectoryInfo info = new DirectoryInfo(pathmain);
            if (!info.Exists)
            {
                MessageBox.Show("You can creat mods with using this tool\nIf you think this tool is not bad, Subscribe and five star on steam", "Thanks For Using AirshipsModMaker");
                DirectoryInfo di = new DirectoryInfo(Environment.CurrentDirectory + @"\Template\");
                if (di.Exists)
                    di.MoveTo(pathmain);
                else
                    info.Create();
            }
            if (info.GetDirectories().Length == 0)//检查有没有模板
            {
                inputToolStripMenuItem_Click(null, null);
                return;
            }
            //加载Template
            foreach (DirectoryInfo di in info.GetDirectories())
            {
                Templates.Add(new Template(di.FullName));
            }

        }

        private void inputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(pathmain);
            MessageBox.Show("Copy Template Directory to this path, Then restart the tool", "input Template");
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
            Save();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = textBoxModName.Text;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                SavePath = saveFileDialog1.FileName;
                Save(SavePath);
                return;
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileInfo fi = new FileInfo(openFileDialog1.FileName);
                StreamReader sr = new StreamReader(fi.OpenRead(), Encoding.UTF8);
                data = new AirShipModData(sr.ReadToEnd(), Templates);
                sr.Close();
                sr.Dispose();
                if(data.ModName =="")
                {
                    MessageBox.Show("This File Is Not AirShipModMaker's File");
                    return;
                }
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
            FrmItem fi = new FrmItem(data.ItemID, Templates.ToArray());
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
            FrmItem fi = new FrmItem(mi, Templates.ToArray());
            if (fi.ShowDialog() == DialogResult.OK)
            {
                data.modItems[id] = fi.ModItem;
                listView1.Items[id].SubItems[0].Text = fi.ModItem.Name;
                listView1.Items[id].SubItems[1].Text = fi.ModItem.UseTemp.Name;
                listView1.Items[id].SubItems[2].Text = fi.ModItem.Info;
                //序列+1
                data.ItemID++;
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
            folderBrowserDialog1.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\AirshipsGame\mods\";
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
                        Thread.Sleep(10);//if Dekete it, it will have bug
                        di.Create();
                    }
                    else
                        return;
                }
                //先创建文件和图片
                StringBuilder lang = new StringBuilder();//strings

                fi.CopyTo(path + @"\logo.png");

                string tmp = new Random().Next().ToString("X");
                lang.AppendLine($"modulecategory_AMM{tmp}={data.ModName.Replace("\r", "").Replace("\n", @"\n")}");

                tmp = LPSMReplace(Properties.Resources.info, new Sub("id", tmp),
                    new Sub("name", data.ModName.Replace("\r", "").Replace("\n", @"\n")),
                    new Sub("info", data.ModInfo.Replace("\r", "").Replace("\n", @"\n")));//info.json

                fi = new FileInfo(path + @"\info.json");
                FileStream fs = fi.Create();
                byte[] buff = Encoding.UTF8.GetBytes(tmp);
                fs.Write(buff, 0, buff.Length);
                fs.Close();
                fs.Dispose();


                foreach (ModItem mi in data.modItems)
                {
                    di = new DirectoryInfo(path + @"\" + mi.UseTemp.Local);//检查文件夹是否存在
                    if (!di.Exists)
                        di.Create();

                    fi = new FileInfo(path + @"\" + mi.UseTemp.Local + @"\AMM" + mi.ItemID + ".json");//singlefile
                    tmp = LPSMReplace(mi.UseTemp.RepFile, mi.ToSubs()).Replace("|name:|", "AMM" + mi.ItemID);

                    fs = fi.Create();
                    buff = Encoding.UTF8.GetBytes(tmp);
                    fs.Write(buff, 0, buff.Length);
                    fs.Close();
                    fs.Dispose();

                    //lang
                    lang.AppendLine($"{mi.UseTemp.Prefix}AMM{mi.ItemID}={mi.Name}\r\n{mi.UseTemp.Prefix}desc_AMM{mi.ItemID}={mi.Info.Replace("\r","").Replace("\n", @"\n")}");

                    if (mi.UseTemp.IsFlipped)
                        lang.AppendLine($"{mi.UseTemp.Prefix}FLIPPED_AMM{mi.ItemID}={mi.Name}(Flipped)\r\n{mi.UseTemp.Prefix}desc_FLIPPED_AMM{mi.ItemID}={mi.Info.Replace("\r", "").Replace("\n", @"\n")}");
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
            }
        }

        public string LPSMReplace(string lpsm, params Sub[] subs)
        {
            foreach (Sub sub in subs)
            {
                lpsm = lpsm.Replace($"|{sub.Name}:|", sub.info);
            }
            return lpsm;
        }
    }

    /// <summary>
    /// 整个Mod数据
    /// </summary>
    public class AirShipModData
    {
        public string ModName;
        public string ModInfo;
        public string Modlogo = "";
        public List<ModItem> modItems = new List<ModItem>();
        public int ItemID = 0;//用于编辑(自增)

        public string ToLps()
        {
            LpsDocument lps = new LpsDocument();
            lps.AddLine(new Line("airshipmod", "UserSave", "", new Sub("name", ModName), new Sub("info", ModInfo), new Sub("logo", Modlogo)));
            foreach (ModItem mi in modItems)
                lps.AddLine(mi.ToLine());
            return lps.ToString();
        }
        public AirShipModData() { }
        public AirShipModData(string lps, List<Template> templates)
        {
            LpsDocument lpsd = new LpsDocument();
            lpsd = new LpsDocument(lps);
            if (lpsd.Read().Name != "airshipmod" && lpsd.Read().Info.ToLower() != "usersave")
                return;
            ModName = lpsd.Read().Find("name").Info;
            ModInfo = lpsd.Read().Find("info").Info;
            Modlogo = lpsd.ReadNext().Find("logo").Info;
            while (lpsd.ReadCanNext())
                modItems.Add(new ModItem(ItemID++, lpsd.ReadNext(), templates));
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
        public ModItem(int id, Line line, List<Template> templates)
        {
            ItemID = id;
            UseTemp = templates.FirstOrDefault(x => x.Name == line.Find("temp").Info);
            Name = line.Find("name").Info;
            Info = line.Find("info").Info;
            Line tmp;
            for (int i = 2; i < line.Subs.Count; i++)
            {
                tmp = UseTemp.Data.FindLine(line.Subs[i].Name);
                Data.Add(new ModSet()
                {
                    Name = line.Subs[i].Name,
                    Value = line.Subs[i].Info,
                    info = tmp.Find("info").Info,
                    Mname = tmp.Find("name").info,
                    ModSetType = (ModSet.SetType)Enum.Parse(typeof(ModSet.SetType), tmp.Find("type").info.ToUpper()),
                    valuenomal = tmp.Find("nomal").Info
                });
            }
        }
        public ModItem(int id, string name, string info, Template usrTemp)
        {
            ItemID = id;
            Name = name;
            Info = info;
            UseTemp = usrTemp;
            UseTemp.Data.LineNode = 1;
            while (UseTemp.Data.ReadCanNext())
                Data.Add(new ModSet
                {
                    Name = UseTemp.Data.Read().Name,
                    info = UseTemp.Data.Read().Find("info").info,
                    ModSetType = (ModSet.SetType)Enum.Parse(typeof(ModSet.SetType), UseTemp.Data.Read().Find("type").info.ToUpper()),
                    Mname = UseTemp.Data.Read().Find("name").info,
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
        public SetType ModSetType = SetType.STRING;
        private string value;
        public string Name;
        public string Mname;//展示给用户的名字

        public string info;
        public string valuenomal;
        public string Value
        {
            get => value; set
            {
                switch (ModSetType)
                {
                    case SetType.UINT:
                        if (IsUnsignInt(value))
                            this.value = value;
                        break;
                    case SetType.INT:
                        if (IsInt(value))
                            this.value = value;
                        break;
                    case SetType.NUMBER:
                        if (IsNumeric(value))
                            this.value = value;
                        break;
                    case SetType.UNUMBER:
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
            return new Sub(Name, Value);
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
        public bool IsFlipped = false;//是不是IsFlipped
        public LpsDocument Data;//替换的数据
        public Image DemoImage;
        public Template(string path)
        {
            FileInfo fi = new FileInfo(path + @"\json.lpsm");
            StreamReader sr;
            sr = new StreamReader(fi.OpenRead(), System.Text.Encoding.Default);
            RepFile = sr.ReadToEnd().Replace("\r", "");
            sr.Dispose();

            Data = new LpsDocument();
            fi = new FileInfo(path + @"\info.lps");
            sr = new StreamReader(fi.OpenRead(), System.Text.Encoding.Default);
            Data = new LpsDocument(sr.ReadToEnd());
            sr.Dispose();
            sr = null;

            Name = Data.Read().Find("name").GetInfo();
            Info = Data.Read().Find("info").GetInfo();
            Author = Data.Read().Find("author").GetInfo();
            Local = Data.Read().Find("local").GetInfo();
            Prefix = Data.Read().Find("prefix").GetInfo();
            DemoImage = Image.FromFile(path + @"\logo.png");

            IsFlipped = (Data.Read().Find("flipped") != null);
        }
    }
}
