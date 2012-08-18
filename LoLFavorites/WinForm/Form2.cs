using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using LoLFavorites.Code;

namespace LoLFavorites.WinForm
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != String.Empty)
            {
                PresetOptions.createPreset(textBox1.Text.TrimEnd(), listBox1);
                textBox1.Text = String.Empty;
            }
            else
            {
                MessageBox.Show("Preset name is empty.");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
                PresetOptions.deletePreset((string)listBox1.SelectedItem, listBox1);
            else
                MessageBox.Show("No preset selected.");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
                PresetOptions.renamePreset(listBox1);
            else
                MessageBox.Show("No preset selected.");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
                PresetOptions.modifyPreset(listBox1.SelectedIndex);
            else
                MessageBox.Show("No preset selected.");
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            foreach (var item in PresetOptions.presetList)
                listBox1.Items.Add(item.name);

            if (Form1.comboBox1Index >= 0)
                listBox1.SelectedIndex = Form1.comboBox1Index;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0 && listBox1.SelectedIndex > 0)
            {
                int index = listBox1.SelectedIndex;

                var temp = listBox1.Items[index];
                listBox1.Items[index] = listBox1.Items[index - 1];
                listBox1.Items[index - 1] = temp;
                listBox1.SelectedIndex = index - 1;

                var temp2 = PresetOptions.presetList[index];
                PresetOptions.presetList[index] = PresetOptions.presetList[index - 1];
                PresetOptions.presetList[index - 1] = temp2;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0 && listBox1.SelectedIndex < listBox1.Items.Count - 1)
            {
                int index = listBox1.SelectedIndex;

                var temp = listBox1.Items[index];
                listBox1.Items[index] = listBox1.Items[index + 1];
                listBox1.Items[index + 1] = temp;
                listBox1.SelectedIndex = index + 1;

                var temp2 = PresetOptions.presetList[index];
                PresetOptions.presetList[index] = PresetOptions.presetList[index + 1];
                PresetOptions.presetList[index + 1] = temp2;
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                PresetOptions.savePresets();
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
