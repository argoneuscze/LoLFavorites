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
using System.Text;
using System.Net;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

using HtmlAgilityPack;
using System.Xml.Linq;

namespace LoLFavorites.Code
{
    public class AutoUpdate
    {
        private static List<string> wikiChampList;

        public static int updateNeeded = -1;

        static AutoUpdate()
        {
            wikiChampList = new List<string>();
        }

        public static void checkUpdates()
        {
            bool champUpdate = checkChampionUpdates();
            //bool appUpdate = checkAppUpdates();

            /*if (champUpdate && appUpdate)
            {
                updateNeeded = 0;
            }*/
            if (champUpdate)
                updateNeeded = 1;
            /*else if (champUpdate && !appUpdate)
            {
                updateNeeded = 1;
            }*/
            /*else if (!champUpdate && appUpdate)
            {
                updateNeeded = 2;
            }*/
        }

        private static bool checkAppUpdates()
        {
            WebClient client = new WebClient();
            var data = client.DownloadData("https://www.dropbox.com/s/crql7nxex33la9g/version.txt?dl=1");
            string version = Encoding.UTF8.GetString(data);

            if (Config.version != version)
                return true;

            return false;
        }

        private static bool checkChampionUpdates()
        {
            var url = "http://leagueoflegends.wikia.com/wiki/List_of_champions";
            var web = new HtmlWeb();
            var doc = web.Load(url);            
            
            foreach (HtmlNode item in doc.DocumentNode.SelectNodes("//table[3]/tr/td/span/a"))
            {
                wikiChampList.Add(item.Attributes["title"].Value);
            }

            if (LoadChampions.championList.Count < wikiChampList.Count)
                return true;

            string path = @"./img/";
            System.IO.DirectoryInfo d = new System.IO.DirectoryInfo(path);
            if (d.GetFiles().Length - 1 < wikiChampList.Count)
                return true;

            return false;
        }

        public static void update()
        {
            Application.Exit();

            var id = updateNeeded;

            // updateNeeded -> 0 - update both
            //                 1 - update champions
            //                 2 - update app

            Process p = new Process();
            p.StartInfo.FileName = "AutoUpdate.exe";
            p.StartInfo.Arguments = "/requiredNonManual " + id;
            p.Start();
        }
    }
}
