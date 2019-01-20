using LinePutScript;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirshipsModMaker
{

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
        public string Outputid = new Random().Next().ToString("X");//用于防止多个MOD重叠

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
        public TempImage ItemImage;//null 为 默认      
        public TempImage GetImage
        {
            get
            {
                if (ItemImage == null)
                {
                    return UseTemp.Image;
                }
                return ItemImage;
            }
        }
        //案例 name#data:|
        public Line ToLine()//转换成行
        {
            Line li = new Line("temp", UseTemp.Name, "", new Sub("name", Name), new Sub("info", Info), new Sub("image", GetImage.Path));
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

            //定义从哪里开始读起
            int i = 2;

            //如果找到图片就从3开始读
            if (line.Find("image") != null)
            {
                i = 3;
                TempImage ti = new TempImage(line.Find("image").Info);
                if (ti.Path != "ERROR")
                    ItemImage = ti;
            }
            Line tmp;
            MSetin mstmp;
            for (; i < line.Subs.Count; i++)
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
        public ModItem(ModItem mi)//拷贝一个一摸一样的ITEM
        {
            Name = mi.Name;
            Data = mi.Data.ToList();
            Info = mi.Info;
            ItemID = mi.ItemID;
            UseTemp = mi.UseTemp;
            ItemImage = mi.ItemImage;
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
            return double.TryParse(value, out double nb);
        }
        public static bool IsUNumeric(string value)
        {
            if (double.TryParse(value, out double nb))
            {
                return nb >= 0;
            }
            else
                return false;
        }
        public static bool IsInt(string value)
        {
            return int.TryParse(value, out int nb);
        }
        public static bool IsUnsignInt(string value)
        {
            if (int.TryParse(value, out int nb))
            {
                return nb >= 0;
            }
            else
                return false;
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
        public TempImage Image;
        public Image DemoImage
        {
            get => Image.DemoImage;
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

                //获取信息

                Name = Data.Read().Find("name").GetInfo();
                Info = Data.Read().Find("info").GetInfo();
                Author = Data.Read().Find("author").GetInfo();
                Local = Data.Read().Find("local").GetInfo();
                Prefix = Data.Read().Find("prefix").GetInfo();
                IsFlipped = Data.Read().Find("flipped") != null;

                //判断版本
                if (Properties.Settings.Default.VersionCheck)
                {
                    if (Data.Read().Find("ver") != null)
                        Verison = Data.Read().Find("ver").GetInfo();
                    else
                    {
                        Error = "No Verison"; return;
                    }

                    if (!ModSet.IsUNumeric(Verison) || Convert.ToDouble(Verison) < 3)
                    {
                        Error = "Verison too old"; return;
                    }
                    else if (Convert.ToDouble(Verison) >= 4)
                    {
                        Error = "Verison too High, Upgreat software to support"; return;
                    }
                }

                Image = new TempImage(path + @"\logo.png", Name);

                if (Image.Path == "ERROR")
                {
                    if (Prefix == "mod_")
                    {
                        Error = "Template Image Load Fail";
                        return;
                    }
                    else
                    {
                        Image.Path = path + @"\logo.png";
                    }

                }

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
    /// <summary>
    /// 贴图图片
    /// </summary>
    /// 距离=16
    public class TempImage
    {
        public string Path;
        public string Name;
        public Image DemoImage
        {
            get => Image.FromFile(Path);
        }
        public Size RealSize;
        public Size VirtualSize;
        public TempImage(string path, string name = "")
        {
            try
            {
                if (name == "")
                {
                    Name = path.Split('\\').Last().Split('.').First();
                }
                else
                {
                    Name = name;
                }


                Path = path;
                RealSize = DemoImage.Size;
                if (RealSize.Height % 16 != 0 || RealSize.Width % 16 != 0)
                {
                    Path = "ERROR";
                    return;
                }
                VirtualSize = new Size(RealSize.Width / 16, RealSize.Height / 16);
            }
            catch
            {
                Path = "ERROR";
            }
        }
    }
}
