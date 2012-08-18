using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoUpdate
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (AutoUpdate.success == 0) // success
                label1.Visible = true;
            else if (AutoUpdate.success == 1) // failed
                label2.Visible = true;
            else if (AutoUpdate.success == -1) // opened updater manually
                label3.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
