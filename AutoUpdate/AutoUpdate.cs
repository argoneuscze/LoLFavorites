using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Web;
using System.Net;
using System.IO;

using System.Xml.Linq;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace AutoUpdate
{
    class AutoUpdate
    {
        public static int success = -1;
        public static int updateArgs = -1;

        private static Form form;

        public static void update(Form form)
        {
            AutoUpdate.form = form;
            bool updated = false;

            if (updateArgs == 0)
            {
                if (updateApp() && updateChampions())
                    updated = true;
            }
            else if (updateArgs == 1)
            {
                if (updateChampions())
                    updated = true;
            }
            else if (updateArgs == 2)
            {
                if (updateApp())
                    updated = true;
            }

            Application.Exit();

            Process p = new Process();

            p.StartInfo.FileName = "AutoUpdate.exe";

            if (updated)
            {
                p.StartInfo.Arguments = "/requiredNonManual " + 100;
            }
            else
            {
                p.StartInfo.Arguments = "/requiredNonManual " + 102;
            }

            if (File.Exists("AutoUpdate1.exe"))
            {
                p.StartInfo.FileName = "AutoUpdate1.exe";
                p.StartInfo.Arguments = "/requiredNonManual " + 101;
            }

            p.Start();
        }

        private static bool updateApp()
        {
            List<string> files = new List<string>();
            files.Add("https://www.dropbox.com/s/co6mt591p8eoo3m/LoLFavorites.exe?dl=1");
            files.Add("https://www.dropbox.com/s/mz96u4pzzmgpqbr/config.xml?dl=1");
            files.Add("https://www.dropbox.com/s/ihxcq4boiiuztnb/AutoUpdate1.exe?dl=1");

            WebClient client = new WebClient();
            
            foreach (string file in files)
            {
                string filename = Regex.Match(file, @"^.*/(.+\.(?:exe|xml))\?dl=1$").Groups[1].Value;
                MessageBox.Show(filename);
                client.DownloadFile(file, filename);
            }

            return true;
        }

        private static bool updateChampions()
        {
            XDocument doc = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XComment("This is an automatically generated file. Edit at your own risk."),
                new XElement("Champions"));

            var url = "http://leagueoflegends.wikia.com/wiki/List_of_champions";
            var web = new HtmlWeb();
            var webdoc = web.Load(url);

            var champList = new List<string>();

            foreach (HtmlNode item in webdoc.DocumentNode.SelectNodes("//table[3]/tr/td/span/a"))
            {
                string champ = HtmlAgilityPack.HtmlEntity.DeEntitize(item.Attributes["title"].Value);
                doc.Element("Champions").Add(new XElement("Champion", champ));
                champList.Add(champ);
            }

            doc.Save("champions.xml");

            string path = @"./img/";
            System.IO.DirectoryInfo d = new System.IO.DirectoryInfo(path);
            var files = d.GetFiles();

            foreach (var champ in champList)
            {
                bool exists = false;

                string champFileName = Regex.Replace(champ, @"[\.' ]", "") + "Square.png";

                foreach (var file in files)
                {
                    if (file.Name == champFileName)
                        exists = true;
                }

                if (!exists)
                {
                    string champUrl = Regex.Replace(HttpUtility.UrlEncode(champ), @"\+", "_");
                    url = "http://leagueoflegends.wikia.com/wiki/" + champUrl;
                    webdoc = web.Load(url);
                    string pictureurl = "";

                    foreach (var node in webdoc.DocumentNode.SelectNodes("//img"))
                    {
                        if (node.Attributes["src"] != null && node.Attributes["height"] != null)
                        {
                            if (node.Attributes["src"].Value.Contains(champFileName) && node.Attributes["height"].Value == "120")
                                pictureurl = node.Attributes["src"].Value;
                        }
                    }

                    if (pictureurl != String.Empty)
                    {
                        string filepath = "./img/" + champFileName;
                        WebClient client = new WebClient();
                        client.DownloadFile(pictureurl, filepath);
                    }
                }
            }

            return true;
        }
    }
}
