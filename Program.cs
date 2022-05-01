using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BTLQLSV.GUI;
using System.Configuration;
using BTLQLSV.Entities;

namespace BTLQLSV
{
    public static class Program
    {
        public static string stcon;
        public static User user;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            stcon = ConfigurationManager.ConnectionStrings["strcon"].ConnectionString;
            Application.Run(new FrmDangNhap());
        }
    }
}
