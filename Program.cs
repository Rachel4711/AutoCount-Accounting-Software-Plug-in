using AutoCount.Authentication;
using PlugIn_1.Forms;
using System;
using System.Windows.Forms;

namespace PlugIn_1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        public static UserSession session { get; set; }
        public static Form main_form { get; set; }
        public static bool appHasLoaded { get; set; }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);

            session = null;
            appHasLoaded = false;

            main_form = new MainForm();

            do
            {
                if (!main_form.IsDisposed)
                {
                    Application.Run(main_form);
                }
            } while (Form.ActiveForm != null);
        }
    }
}
