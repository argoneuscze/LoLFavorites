using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows;
using System.Xml.Linq;

namespace LoLFavorites.Code
{
    public class LoadChampions
    {
        public static List<Champion> championList;
        private static List<string> championNameList;

        private static ToolTip tip;

        static LoadChampions()
        {
            championList = new List<Champion>();
            championNameList = new List<string>();
            tip = new ToolTip();

            tip.ReshowDelay = 500;
            tip.AutoPopDelay = 15000;
            tip.UseAnimation = false;
            tip.UseFading = false;
        }

        public static void loadAllChampions()
        {
            XDocument doc = XDocument.Load("champions.xml");
            foreach (var item in doc.Descendants("Champion"))
            {
                championNameList.Add(item.Value);
            }

            foreach (string champName in championNameList)
                loadChampion(champName);
        }

        public static void drawFilteredChampions(int selectedOption, Panel panel)
        {
            var temp = new List<Champion>();

            foreach (var item in PresetOptions.presetList[selectedOption].chosen)
            {    
                var champ = getChampByName(item);
                temp.Add(champ);  
            }

            drawChampions(panel, temp);

            return;            
        }

        public static void deleteAllChamps(Panel panel)
        {
            panel.Controls.Clear();
        }

        public static void drawChampions(Panel panel, List<Champion> list)
        {                
            for (int i = 0; i < list.Count; ++i)
            {
                Button button = new Button();
                button.Click += new EventHandler(buttonPress);
                button.MouseEnter += new EventHandler(buttonEnter);
                button.MouseLeave += new EventHandler(buttonLeave);

                button.Height = 105;
                button.Width = 95;
                button.Top = 6 + i / 8 * 111;
                button.Left = 6 + i % 8 * 101;

                button.Tag = list[i].name;

                button.TextImageRelation = TextImageRelation.ImageAboveText;
                button.Text = list[i].name;
                button.TextAlign = ContentAlignment.BottomCenter;
                button.ForeColor = Color.Black;
                button.BackColor = Color.White;

                button.Image = list[i].image;

                panel.Controls.Add(button);
            }            

            if (panel.Controls.Count > 32)
            {
                panel.AutoScroll = true;
                panel.Width = 834;
                panel.Left = 14;
            }
            else
            {
                panel.AutoScroll = false;
                panel.Width = 820;
                panel.Left = 18;
            }
        }

        private static void loadChampion(string name)
        {
            Champion champ = new Champion();
            champ.name = name;

            var imagePath = "./img/" + Regex.Replace(name, @"[\.' ]", "") + "Square.png";
            Bitmap img = new Bitmap(80, 80);
            if (File.Exists(imagePath))
            {
                var originalImage = Bitmap.FromFile(imagePath);
                var g = Graphics.FromImage(img);
                g.DrawImage(originalImage, 0, 0, 80, 80);
            }

            champ.image = img;

            championList.Add(champ);
        }

        private static void buttonPress(object sender, EventArgs e)
        {
            Button button = sender as Button;
            MessageBox.Show("Clicking champions does nothing yet :( (work in progress!)");
        }

        private static void buttonEnter(object sender, EventArgs e)
        {
            Button button = sender as Button;
            string groups = "";
            int count = 0;

            foreach (var item in PresetOptions.presetList)
            {
                if (item.chosen.Contains((string)button.Tag))
                {
                    ++count;

                    if (count % 3 == 0)
                        groups += "\n";

                    if (count == 1)
                        groups = item.name;
                    else
                        groups += ", " + item.name;
                }
            }

            if (groups == String.Empty)
                groups = "This champion doesn't belong\nto any preset.";

            showButtonTooltip(groups, button, (string)button.Tag);
        }

        private static void buttonLeave(object sender, EventArgs e)
        {
            Button button = sender as Button;
            tip.Active = false;
        }

        private static void showButtonTooltip(string text, Control ctrl, string name)
        {
            tip.Active = true;
            tip.UseFading = false;
            tip.UseAnimation = false;
            tip.ToolTipTitle = championNameList.Find(item => item == name) + " - Presets";
            tip.Show(text, ctrl, 0, 105, 15000);
        }

        public static Champion getChampByName(string name)
        {
            foreach (Champion champ in championList)
            {
                if (champ.name == name)
                    return champ;
            }
            return null;
        }
                
        public static List<string> getChampionNameList()
        {
            return championNameList;
        }        
    }
}
