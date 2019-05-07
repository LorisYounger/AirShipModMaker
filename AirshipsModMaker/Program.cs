using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirshipsModMaker
{
    static class Program
    {

#if !SAFE
        public static readonly string PathMain = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\LBSoft\Airship";
        public static readonly string PathImage = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\LBSoft\AirshipImage";
#else
        public static readonly string PathMain = Environment.CurrentDirectory + @"\Template";//safemode
#endif


        public static bool Reload = true;
        public static readonly string version = "beta 6";
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
#if !DEBUG
            while (Reload)
            {
                Reload = false;

                try
                {
#endif
                    Application.Run(new FrmMain());
#if !DEBUG
                }
                catch (Exception e)
                {
                    if (e.HResult != -2146232798)
                    {
                        if (MessageBox.Show(e.Message + "\nForm:\n" + e.Source + "\n\nPress Retry to ReOpen ModMaker", "Fatal Error {Plese Send to Author} " + e.HResult, MessageBoxButtons.RetryCancel) == DialogResult.Cancel)
                            return;
                    }
                }

            }
#endif
        }
    }
}
