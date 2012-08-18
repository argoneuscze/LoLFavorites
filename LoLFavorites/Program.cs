using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace LoLFavorites
{
    public class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 2)
            {
                if (args[0] == "/requiredNonManual")
                {
                    if (args[1] == "clean")
                    {
                        File.Delete("AutoUpdate.exe");
                        File.Move("AutoUpdate1.exe", "AutoUpdate.exe");

                        Process p = new Process();
                        p.StartInfo.Arguments = "/requiredNonManual " + 100;
                        p.StartInfo.FileName = "AutoUpdate.exe";

                        p.Start();
                    }
                }
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new WinForm.Form1());
            }
            
        }
    }
}
