using DVLD_Driving_License_Managemet.Applications;
using DVLD_Driving_License_Managemet.Applications.Driving_License_Apps;
using DVLD_Driving_License_Managemet.Applications.Manage_Applications_Types;
using DVLD_Driving_License_Managemet.Applications.ManageTestTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Driving_License_Managemet
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }
    }
}
