using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace AutoUpdate
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
            if (args.Length == 2)
            {
                // args -> 0-99 what to update
                //         100  update complete, show 'update successful' message
                //         101  update complete, clean up and restart
                if (args[0] == "/requiredNonManual")
                {
                    if (Convert.ToInt32(args[1]) < 100)
                    {
                        AutoUpdate.updateArgs = Convert.ToInt32(args[1]);
                        Application.Run(new Form1());
                    }
                    else if (Convert.ToInt32(args[1]) == 100)
                    {
                        AutoUpdate.success = 0;
                        Application.Run(new Form2());
                    }
                    else if (Convert.ToInt32(args[1]) == 101)
                    {
                        Process p = new Process();
                        p.StartInfo.FileName = "LoLFavorites.exe";
                        p.StartInfo.Arguments = "/requiredNonManual clean";

                        p.Start();
                    }
                    else
                    {
                        AutoUpdate.success = 1;
                        Application.Run(new Form2());
                    }
                }
            }
            else
            {
                Application.Run(new Form2());
            }
        }
    }
}
