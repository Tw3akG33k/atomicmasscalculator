using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace atomicmasscalc
{
    public partial class PercentComposition : Form
    {

        List<string> elementsnamefile = new List<string>();
        List<double> elementsmassfile = new List<double>();

        public PercentComposition()
        {
            InitializeComponent();
            MessageBox.Show("Invalid function call to: \"PercentComposition.cs\"\r\nNo parameters given.","Invalid Function Call",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            this.Close();
        }

        public PercentComposition(List<string> elementlist, List<int> elementsubscript)
        {
            InitializeComponent();

            Assembly assembly = Assembly.GetExecutingAssembly();
            StreamReader sr = new StreamReader(assembly.GetManifestResourceStream("atomicmasscalc.listofelementnames.txt"));
            string line;
            if (sr.Peek() != -1)
            {
                while ((line = sr.ReadLine()) != null)
                {
                    elementsnamefile.Add(line);
                }
            }
            StreamReader sr1 = new StreamReader(assembly.GetManifestResourceStream("atomicmasscalc.listofelementmasses.txt"));
            if (sr1.Peek() != -1)
            {
                while ((line = sr1.ReadLine()) != null)
                {
                    elementsmassfile.Add(Convert.ToDouble(line));
                }
            }

            sr.Close();
            sr1.Close();

            double atomicmassprog = 0;
            for (int x = 0; x < elementlist.Count; x++)
            {
                for (int y = 0; y < elementsnamefile.Count; y++)
                {
                    if (elementlist[x].Equals(elementsnamefile[y]))
                    {
                        atomicmassprog += (elementsmassfile[y] * elementsubscript[x]);
                    }
                }
            }

            for (int x = 0; x < elementlist.Count; x++)
            {
                formulalabel.Text += elementlist[x];
                if (elementsubscript[x] != 1) formulalabel.Text += elementsubscript[x];
                for (int y = 0; y < elementsnamefile.Count; y++)
                {
                    if (elementlist[x].Equals(elementsnamefile[y]))
                    {
                        mainrichtextbox.Text += elementlist[x]+": "+String.Format("{0:##.##}", ((elementsmassfile[y] * elementsubscript[x]) / atomicmassprog * 100)) +"%\r\n";
                    }
                }
            }
        }

        private void okbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
