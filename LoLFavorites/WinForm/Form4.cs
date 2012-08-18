using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LoLFavorites.WinForm
{
    public partial class Form4 : Form
    {
        public ListBox default1 = new ListBox();
        public ListBox default2 = new ListBox();

        public Form4()
        {
            InitializeComponent();            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();

            listBox1.Items.AddRange(default1.Items);
            listBox2.Items.AddRange(default2.Items);

            updateLabels();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                int previndex = -1;

                if (listBox1.SelectedItems.Count == 1 && listBox1.SelectedIndex < listBox1.Items.Count - 1)
                    previndex = listBox1.SelectedIndex;

                string[] list = new string[listBox1.SelectedItems.Count];
                listBox1.SelectedItems.CopyTo(list, 0);
                foreach (var item in list)
                {
                    listBox2.Items.Add(item);
                    listBox1.Items.Remove(item);
                }

                if (previndex >= 0)
                    listBox1.SelectedIndex = previndex;

                updateLabels();
            }            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex >= 0)
            {
                int previndex = -1;

                if (listBox2.SelectedItems.Count == 1 && listBox2.SelectedIndex < listBox2.Items.Count - 1)
                    previndex = listBox2.SelectedIndex;

                string[] list = new string[listBox2.SelectedItems.Count];
                listBox2.SelectedItems.CopyTo(list, 0);
                foreach (var item in list)
                {
                    listBox1.Items.Add(item);
                    listBox2.Items.Remove(item);
                }

                if (previndex >= 0)
                    listBox2.SelectedIndex = previndex;

                updateLabels();
            }   
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox2.Items.AddRange(listBox1.Items);
            listBox1.Items.Clear();

            updateLabels();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listBox1.Items.AddRange(listBox2.Items);
            listBox2.Items.Clear();

            updateLabels();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            updateLabels();
        }

        private void updateLabels()
        {
            label1.Text = "Favorites list (" + listBox2.Items.Count + ")";
            label2.Text = "Champions list (" + listBox1.Items.Count + ")";
        }
    }
}
