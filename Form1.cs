using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Net;
using System.Runtime.InteropServices;

namespace atomicmasscalc
{
    public partial class Form1 : Form
    {

        List<string> elementlist = new List<string>();
        List<int> elementsubscript = new List<int>();

        List<string> elementsnamefile = new List<string>();
        List<double> elementsmassfile = new List<double>();

        public Form1()
        {
            InitializeComponent();
            realtimecheckbox.Checked = true;
        }

        private void calculatebutton_Click(object sender, EventArgs e)
        {
            basic();
            refocus();
        }

        private void basic()
        {
            elementlist.Clear();
            elementsubscript.Clear();
            String formula = formulabox.Text;
            char[] formulachars = formula.ToCharArray();
            int formulacharslength = formulachars.Length;
            for (int x = 0; x < formulacharslength;)
            {
                string currentelement = "";
                int currentsubscript = 1;
                if (char.IsUpper(formulachars[x]))
                {
                    currentelement += formulachars[x];
                    x++;
                    try
                    {
                        if (char.IsLower(formulachars[x]))
                        {
                            currentelement += formulachars[x];
                            x++;
                        }
                        if (char.IsNumber(formulachars[x]))
                        {
                            currentsubscript = Convert.ToInt32(formulachars[x] + "");
                            x++;
                            try
                            {
                                while (char.IsNumber(formulachars[x]))
                                {
                                    currentsubscript = (currentsubscript * 10) + Convert.ToInt32(formulachars[x] + "");
                                    x++;
                                }
                            }
                            catch (Exception) { }
                        }
                    }
                    catch (Exception) { }
                }
                else
                {
                    x++;
                }
                elementlist.Add(currentelement);
                elementsubscript.Add(currentsubscript);
            }
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            double atomicmassprog = 0;
            for (int x = 0; x < elementlist.Count; x++)
            {
                for (int gh = 0; gh < elementsnamefile.Count; gh++)
                {
                    if (elementlist[x].Equals(elementsnamefile[gh]))
                    {
                        atomicmassprog += (elementsmassfile[gh] * elementsubscript[x]);
                    }
                }
            }
            finalmassbox.Text = atomicmassprog + "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            formulabox.Text = "";
            finalmassbox.Text = "0";
            refocus();
        }

        private void formulabox_TextChanged(object sender, EventArgs e)
        {
            if (realtimecheckbox.Checked)
            {
                basic();
            }
        }

        private void closebutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void realtimecheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (realtimecheckbox.Checked)
            {
                basic();
            }
            refocus();
        }

        private void instructionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }

        private void creditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.ShowDialog();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

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
            Thread t1 = new Thread(checkforupdates);
            t1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Grams2Moles g2m = new Grams2Moles(realtimecheckbox.Checked);
            g2m.ShowDialog();
            refocus();
        }

        private void gramsMolesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Grams2Moles g2m = new Grams2Moles(realtimecheckbox.Checked);
            g2m.ShowDialog();
            refocus();
        }

        private void calculateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            basic();
        }

        public void refocus()
        {
            formulabox.Focus();
        }

        public void checkforupdates()
        {
            try
            {
                updatestatuslabel.Text = "Checking for updates...";
                string url = "http://programdata.webs.com//amcupdate.txt";
                string data = "0";
                WebClient client = new WebClient();
                data = client.DownloadString(url);
                int dataint = Convert.ToInt32(data);
                if (dataint<=7) 
                {
                    updatestatuslabel.Text = "No updates available.";
                }
                else
                {
                    updatestatuslabel.Text = "Update Available. Click here.";
                }
            }
            catch (Exception) 
            {
                updatestatuslabel.Text = "Unable to check for updates.";
            }
        }

        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread t1 = new Thread(checkforupdates);
            t1.Start();
        }

        private void updateHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdatingHelp uh = new UpdatingHelp();
            uh.ShowDialog();
        }

        private void percentCompositionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            basic();
            PercentComposition pc = new PercentComposition(elementlist, elementsubscript);
            pc.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            basic();
            PercentComposition pc = new PercentComposition(elementlist, elementsubscript);
            pc.ShowDialog();
            refocus();
        }

        private void updatestatuslabel_Click(object sender, EventArgs e)
        {
            UpdatingHelp uh = new UpdatingHelp();
            uh.ShowDialog();
        }

        private void copyMassToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(finalmassbox.Text);
        }

        private void balanceEquationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BalanceEquation be = new BalanceEquation();
            be.ShowDialog();
        }

        private void balanceButton_Click(object sender, EventArgs e)
        {
            BalanceEquation be = new BalanceEquation();
            be.ShowDialog();
        }
    }
}