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
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;

namespace LoLFavorites.Code
{
    public class PresetOptions
    {
        public static List<Preset> presetList = new List<Preset>();

        public static void createPreset(string name, ListBox listbox)
        {
            bool alreadyExists = false;

            foreach (string item in listbox.Items)
            {
                if (item == name)
                    alreadyExists = true;
            }

            if (alreadyExists)
                MessageBox.Show("A preset with this name already exists.");
            else
            {
                listbox.Items.Add(name);
                presetList.Add(new Preset());
                presetList[presetList.Count - 1].name = name;
                listbox.SelectedIndex = listbox.Items.Count - 1;
            }
        }

        public static void deletePreset(string name, ListBox listbox)
        {
            int prevpos = listbox.SelectedIndex;
            presetList.RemoveAt(listbox.SelectedIndex);
            listbox.Items.Remove(name);

            if (listbox.Items.Count > prevpos)
                listbox.SelectedIndex = prevpos;
        }

        public static void renamePreset(ListBox listbox)
        {
            WinForm.Form3 form = new WinForm.Form3();
            form.ShowDialog();

            if (form.DialogResult == DialogResult.OK)
            {
                int index = listbox.SelectedIndex;
                listbox.Items.Remove(listbox.SelectedItem);
                listbox.Items.Insert(index, form.textBox1.Text);
                presetList[index].name = form.textBox1.Text;
            }            
        }

        public static bool exists(string name)
        {           
            foreach (Preset item in presetList)
            {
                if (item.name == name)
                    return true;
            }

            return false;
        }

        public static void modifyPreset(int index)
        {
            WinForm.Form4 form = new WinForm.Form4();            

            foreach (string name in presetList[index].chosen)
            {
                form.listBox2.Items.Add(name);
            }

            foreach (string item in LoadChampions.getChampionNameList())
            {
                bool exists = false;

                foreach (string listItem in form.listBox2.Items)
                {
                    if (item == listItem)
                        exists = true;
                }

                if (!exists)
                    form.listBox1.Items.Add(item);
            }

            form.default1.Items.AddRange(form.listBox1.Items);
            form.default2.Items.AddRange(form.listBox2.Items);

            form.ShowDialog();

            if (form.DialogResult == DialogResult.OK)
            {
                presetList[index].chosen.Clear();
                foreach (string item in form.listBox2.Items)
                {
                    presetList[index].chosen.Add(item);
                }
            }
        }

        public static void loadPresets()
        {
            XDocument doc = XDocument.Load("presets.xml");
            foreach (var element in doc.Descendants("Preset"))
            {
                presetList.Add(new Preset
                {
                    name = element.Attribute("Name").Value
                });
                foreach (var name in element.Descendants("Champion"))
                {
                    presetList[presetList.Count - 1].chosen.Add(name.Value);
                }
            }
        }

        public static void savePresets()
        {            
            XDocument doc = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XComment("This is an automatically generated file. Edit at your own risk."),
                new XElement("Presets"));

                for (int i = 0; i < presetList.Count; ++i)
                {
                    doc.Element("Presets").Add(new XElement("Preset",
                        new XAttribute("ID", i),
                        new XAttribute("Name", presetList[i].name)));

                        foreach (var item in presetList[i].chosen)
                        {
                            doc.Element("Presets").Elements("Preset").ElementAt(i).Add(new XElement("Champion", item));
                        }
                }

            doc.Save("presets.xml");                    
        }
    }
}