using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Diagnostics;

namespace LoLFavorites.Code
{
    public class Config
    {
        public static string version;

        public static bool firstRun = true;
        public static bool autoUpdate = true;

        public static void onFirstRun()
        {
            firstRun = false;
            saveConfig();
        }

        public static void loadConfig()
        {
            XDocument doc = XDocument.Load("config.xml");

            version = doc.Descendants("Config").Elements("version").First().Value;

            firstRun = getValue("firstRun", doc);
            autoUpdate = getValue("autoUpdate", doc);
        }

        public static void saveConfig()
        {
            XDocument doc = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XComment("This is an automatically generated file. Edit at your own risk."),                
                new XElement("Config",
                    new XElement("version", version)));

            addConfigOption("firstRun", firstRun, doc);
            addConfigOption("autoUpdate", autoUpdate, doc);            

            doc.Save("config.xml");
        }

        private static void addConfigOption(string name, bool func, XDocument doc)
        {            
            if (func)
                doc.Element("Config").Add(new XElement(name, "1"));
            else
                doc.Element("Config").Add(new XElement(name, "0"));
        }

        private static bool getValue(string name, XDocument doc)
        {
            var i = doc.Descendants("Config").Elements(name).First().Value;
            if (i == "1")
                return true;
            else
                return false;
        }
    }
}
