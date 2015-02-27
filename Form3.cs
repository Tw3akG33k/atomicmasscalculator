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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            infobox.Text = "This program is free. If you had to pay money for this program, I would suggest trying to get your money back.\r\n\r\n"+
                "Author: Holden P. Lewis - All rights reserved.\r\nLanguage: C#";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
