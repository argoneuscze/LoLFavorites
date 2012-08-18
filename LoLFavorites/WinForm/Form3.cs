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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            button2.DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == String.Empty)
                MessageBox.Show("Can't change name to empty");
            else
            {
                if (LoLFavorites.Code.PresetOptions.exists(textBox1.Text))              
                    MessageBox.Show("A preset with this name already exists!");
                else
                {
                    this.DialogResult = DialogResult.OK;
                }
            }

        }
    }
}
