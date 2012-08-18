﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using LoLFavorites.Code;

namespace LoLFavorites.WinForm
{
    public partial class Form1 : Form
    {
        public static int comboBox1Index;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {            
            Main.load();            

            foreach (var item in PresetOptions.presetList)
                comboBox1.Items.Add(item.name);

            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WinForm.Form2 form2 = new Form2();
            comboBox1Index = comboBox1.SelectedIndex;
            form2.ShowDialog();

            if (form2.DialogResult == DialogResult.OK)
            {
                panel2.Controls.Clear();
                comboBox1.Items.Clear();
                foreach (var item in PresetOptions.presetList)
                    comboBox1.Items.Add(item.name);

                if (form2.listBox1.SelectedIndex >= 0)
                    comboBox1.SelectedIndex = form2.listBox1.SelectedIndex;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadChampions.deleteAllChamps(panel2);
            LoadChampions.drawFilteredChampions(comboBox1.SelectedIndex, panel2);
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            this.Text = "LoLFavorites v" + Config.version + " (by argoneus)";

            if (LoLFavorites.Code.AutoUpdate.updateNeeded >= 0)
            {
                var form = new LoLFavorites.WinForm.Form5();
                form.ShowDialog();
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Config.saveConfig();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WinForm.Form6 form = new WinForm.Form6();
            form.ShowDialog();
        }
    }
}