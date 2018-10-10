using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirshipsModMaker
{
    static class Program
    {
        public static readonly string PathMain = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\LBSoft\Airship";
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
        }
    }
}
