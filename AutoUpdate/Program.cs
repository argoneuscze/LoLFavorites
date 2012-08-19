/*
 * Copyright (C) 2012 Tomáš Drbota
 * 
 * This file is part of LoLFavorites.

 * LoLFavorites is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.

 * LoLFavorites is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.

 * You should have received a copy of the GNU General Public License
 * along with LoLFavorites.  If not, see <http://www.gnu.org/licenses/>.
 */

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
