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
