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

namespace atomicmasscalc
{
    public partial class Grams2Moles : Form
    {

        List<string> elementsnamefile = new List<string>();
        List<double> elementsmassfile = new List<double>();
        bool realtime = false;

        public Grams2Moles()
        {
            MessageBox.Show("An error may have occured. Real time has been disabled for this window.\r\nPlease contact the developer and let him know what you did before you got this message.");
            InitializeComponent();
        }

        public Grams2Moles(bool isRealTime)
        {
            realtime = isRealTime;
            InitializeComponent();
        }

        private void Grams2Moles_Load(object sender, EventArgs e)
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
            //calculateg2m();
        }

        private void closebutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void calculatebutton_Click(object sender, EventArgs e)
        {
            calculateg2m();
        }

        public void calculateg2m()
        {
            double grams = 0;
            try
            {
                grams = Convert.ToDouble(gramstextbox.Text);
            }
            catch (Exception) { }
            string formula = ofelementtextbox.Text;
            molesofwhatlabel.Text = "mol " + formula;
            
            //////////////////////////////
            //////////////////////////////

            List<string> elementlist = new List<string>();
            List<int> elementsubscript = new List<int>();
            char[] formulachars = formula.ToCharArray();
            int formulacharslength = formulachars.Length;
            for (int x = 0; x < formulacharslength; )
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

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            double atomicmassprog = 0;
            for (int x = 0; x < elementlist.Count; x++)
            {
                for (int gh = 0; gh < 112; gh++)
                {
                    if (elementlist[x].Equals(elementsnamefile[gh]))
                    {
                        atomicmassprog += (elementsmassfile[gh] * elementsubscript[x]);
                    }
                }
            }
            double finalamount = grams/atomicmassprog;
            finalmolestextbox.Text = finalamount.ToString();
        }

        public void calculatem2g()
        {
            double moles = 0;
            try
            {
                moles = Convert.ToDouble(finalmolestextbox.Text);
            }
            catch (Exception) { }
            string formula = ofelementtextbox.Text;
            molesofwhatlabel.Text = "mol " + formula;

            //////////////////////////////
            //////////////////////////////

            List<string> elementlist = new List<string>();
            List<int> elementsubscript = new List<int>();
            char[] formulachars = formula.ToCharArray();
            int formulacharslength = formulachars.Length;
            for (int x = 0; x < formulacharslength; )
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

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            double atomicmassprog = 0;
            for (int x = 0; x < elementlist.Count; x++)
            {
                for (int gh = 0; gh < 112; gh++)
                {
                    if (elementlist[x].Equals(elementsnamefile[gh]))
                    {
                        atomicmassprog += (elementsmassfile[gh] * elementsubscript[x]);
                    }
                }
            }
            double finalamount = moles * atomicmassprog;
            gramstextbox.Text = finalamount.ToString();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void calculateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            calculateg2m();
        }

        private void gramstextbox_TextChanged(object sender, EventArgs e)
        {
            if(realtime && switchbutton.Text == "G2M") calculateg2m();
        }

        private void ofelementtextbox_TextChanged(object sender, EventArgs e)
        {
            if (realtime)
            {
                if (switchbutton.Text == "G2M") calculateg2m();
                else calculatem2g();
            }
        }

        private void switchbutton_Click(object sender, EventArgs e)
        {
            if (switchbutton.Text == "G2M")
            {
                switchbutton.Text = "M2G";
                gramstextbox.ReadOnly = true;
                finalmolestextbox.ReadOnly = false;
            }
            else
            {
                switchbutton.Text = "G2M";
                gramstextbox.ReadOnly = false;
                finalmolestextbox.ReadOnly = true;
            }
        }

        private void finalmolestextbox_TextChanged(object sender, EventArgs e)
        {
            if (realtime && switchbutton.Text == "M2G") calculatem2g();
        }
    }
}