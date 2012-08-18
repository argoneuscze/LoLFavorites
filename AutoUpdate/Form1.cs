using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace AutoUpdate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();

            Process p = new Process();
            p.StartInfo.FileName = "AutoUpdate.exe";
            p.StartInfo.Arguments = "/requiredNonManual 100";
            p.Start();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            AutoUpdate.update(this); 
            this.Refresh();
        }
    }
}
