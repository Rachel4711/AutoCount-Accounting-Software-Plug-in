using AutoCount.Authentication;
using PlugIn_1.Forms;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PlugIn_1
{
    static class Program
    {
        public static UserSession session { get; set; }
        public static Form main_form { get; set; }
        public static Form pro_form { get; set; }
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

    static class WindowActivator
    {
        [DllImport("user32.dll")]
        internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        internal static extern bool SetForegroundWindow(IntPtr hWnd);

        internal const int SW_RESTORE = 9; // Restores a minimized window
    }
}
