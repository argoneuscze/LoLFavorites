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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                Config.autoUpdate = false;

            this.Close();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            if (Config.autoUpdate == false)
                checkBox1.Checked = true;
        }
    }
}
