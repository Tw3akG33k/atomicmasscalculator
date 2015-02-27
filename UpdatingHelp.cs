using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace atomicmasscalc
{
    public partial class UpdatingHelp : Form
    {
        public UpdatingHelp()
        {
            InitializeComponent();
        }

        private void UpdatingHelp_Load(object sender, EventArgs e)
        {
            maintextbox.Text = "To manually download an updated version of this program, please visit\r\nhttp://www.programdata.webs.com//atomicmasscalc.zip";
        }

        private void maintextbox_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        public void closewindow()
        {
            this.Close();
        }
        
        private void updatebutton_Click(object sender, EventArgs e)
        {
            Updating update = new Updating(Application.StartupPath);
            update.ShowDialog();
            this.Close();
        }
    }
}
