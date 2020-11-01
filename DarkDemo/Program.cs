using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace DarkDemo
{
    static class Program
    {
        public static MainForm f;
        public static Hashtable metadata;
        public static string DATABASE_PASSWORD="spaceil";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            LoginForm login = new LoginForm();
            DialogResult dr = login.ShowDialog();
            if (dr == DialogResult.OK)
            {
       
                f = new MainForm(login.currentUser);
                Application.Run(f);
                
            }
       
            Application.Exit();
        }
    }
}
