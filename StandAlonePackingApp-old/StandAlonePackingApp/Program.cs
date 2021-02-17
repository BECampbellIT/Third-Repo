using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using StandAlonePackingLib;

namespace StandAlonePackingApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            bool configMode = false;
            for (int i = 0; i < args.Length;i++ )
            {
                string a = args[i].ToLower();
                if (a.Equals("-i") || a.Equals("-c"))
                {
                    configMode = true;
                    break;
                }
            }
            if (configMode)
                Application.Run(new SettingsScreen());
            else
            {
                if (!CommonData.Initialise())
                {
                    MessageBox.Show("Error reading configuration XML file(s) - see log", "Can't Read Configuration File(s)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                Application.Run(new LogonScreen());

                if (ThisApp.user == null)
                    Application.Exit();
                else
                {
                    if (ThisApp.user.accessPackCarton)
                        // User can pack cartons - start with the main carton packing screen
                        Application.Run(new MainScreen());
                    else
                    {
                        bool img = ThisApp.user.accessBlockImage || ThisApp.user.accessSendImage || ThisApp.user.accessMaintainImage;

                        if (ThisApp.user.accessUserAdmin && img)
                            // User can't pack cartons but can do stuff with printer images and users - start with actions screen
                            Application.Run(new ActionsScreen());

                        else if (img)
                            // User can only do stuff with printer images - start with image maintenance screen
                            Application.Run(new PrinterImagesDialog());

                        else if (ThisApp.user.accessUserAdmin)
                            // User can only maintain users - start with user maintenance screen
                            Application.Run(new UserAdminScreen());

                        else
                            // This user isn't authorised for any operations
                            MessageBox.Show(string.Format("User {0} has no authorisations",ThisApp.user.name), "No Authoistations Defined", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }
    }
}
