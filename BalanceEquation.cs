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
    public partial class BalanceEquation : Form
    {
        bool warned = false;

        public BalanceEquation()
        {
            InitializeComponent();
        }

        private void calculateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            balance();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void copyToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("[" + numbox1.Text + "," + numbox2.Text + "," + numbox3.Text + "," + numbox4.Text + "]");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            balance();
        }

        private void balance()
        {
            if (checkValid())
            {
                List<string> elements = new List<string>();

                List<string> firstboxelements = getElements(compound1.Text);
                List<string> secondboxelements = getElements(compound2.Text);
                List<string> thirdboxelements = getElements(compound3.Text);
                List<string> fourthboxelements = getElements(compound4.Text);

                List<int> firstboxsubscripts = getSubscripts(compound1.Text);
                List<int> secondboxsubscripts = getSubscripts(compound2.Text);
                List<int> thirdboxsubscripts = getSubscripts(compound3.Text);
                List<int> fourthboxsubscripts = getSubscripts(compound4.Text);

                for (int x = 0; x < firstboxelements.Count(); x++) if (!elements.Contains(firstboxelements[x])) elements.Add(firstboxelements[x]);
                for (int x = 0; x < secondboxelements.Count(); x++) if (!elements.Contains(secondboxelements[x])) elements.Add(secondboxelements[x]);

                for (int x = 1; x < maxcoefficient.Value + 1; x++)
                {
                    for (int y = 1; y < maxcoefficient.Value + 1; y++)
                    {
                        for (int q = 1; q < maxcoefficient.Value + 1; q++)
                        {
                            for (int r = 1; r < maxcoefficient.Value + 1; r++)
                            {
                                int[] numofeachelement = new int[elements.Count()];
                                for (int a = 0; a < firstboxelements.Count(); a++)
                                {
                                    int subscript = firstboxsubscripts[a];
                                    for (int b = 0; b < elements.Count(); b++)
                                    {
                                        if (elements[b] == firstboxelements[a]) numofeachelement[b] += x * subscript;
                                    }
                                }
                                for (int g = 0; g < secondboxelements.Count(); g++)
                                {
                                    int subscript = secondboxsubscripts[g];
                                    for (int h = 0; h < elements.Count(); h++)
                                    {
                                        if (elements[h] == secondboxelements[g]) numofeachelement[h] += y * subscript;
                                    }
                                }


                                int[] numofeachelement2 = new int[elements.Count()];
                                for (int a = 0; a < thirdboxelements.Count(); a++)
                                {
                                    int subscript = thirdboxsubscripts[a];
                                    for (int b = 0; b < elements.Count(); b++)
                                    {
                                        if (elements[b] == thirdboxelements[a]) numofeachelement2[b] += q * subscript;
                                    }
                                }
                                for (int g = 0; g < fourthboxelements.Count(); g++)
                                {
                                    int subscript = fourthboxsubscripts[g];
                                    for (int h = 0; h < elements.Count(); h++)
                                    {
                                        if (elements[h] == fourthboxelements[g]) numofeachelement2[h] += r * subscript;
                                    }
                                }

                                if (ArraysEqual(numofeachelement, numofeachelement2))
                                {
                                    numbox1.Text = x.ToString();
                                    numbox2.Text = y.ToString();
                                    numbox3.Text = q.ToString();
                                    numbox4.Text = r.ToString();
                                    x = (int)maxcoefficient.Value + 1;
                                    y = (int)maxcoefficient.Value + 1;
                                    q = (int)maxcoefficient.Value + 1;
                                    r = (int)maxcoefficient.Value + 1;

                                }
                                else if (x == maxcoefficient.Value && y == maxcoefficient.Value && q == maxcoefficient.Value && r == maxcoefficient.Value) MessageBox.Show("Try a higher max coefficient or check your equation.", "Failed to Balance", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                    }
                }
            }
            else MessageBox.Show("Invalid Equation. Elements don't match up!", "Matchup Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private bool ArraysEqual(int[] a1, int[] a2)
        {
            for (int x = 0; x < a1.Length; x++)
            {
                if (a1[x] != a2[x]) return false;
            }
            return true;
        }

        private bool checkValid()
        {
            List<string> elements = new List<string>();

            List<string> firstboxelements = getElements(compound1.Text);
            List<string> secondboxelements = getElements(compound2.Text);

            for (int x = 0; x < firstboxelements.Count(); x++)  if (!elements.Contains(firstboxelements[x])) elements.Add(firstboxelements[x]);
            for (int x = 0; x < secondboxelements.Count(); x++) if (!elements.Contains(secondboxelements[x])) elements.Add(secondboxelements[x]);
            
            
            List<string> elements2 = new List<string>();

            List<string> thirdboxelements = getElements(compound3.Text);
            List<string> fourthboxelements = getElements(compound4.Text);

            for (int x = 0; x < thirdboxelements.Count(); x++) if (!elements2.Contains(thirdboxelements[x])) elements2.Add(thirdboxelements[x]);
            for (int x = 0; x < fourthboxelements.Count(); x++) if (!elements2.Contains(fourthboxelements[x])) elements2.Add(fourthboxelements[x]);
            
            
            elements.Sort();
            elements2.Sort();

            if (elements.SequenceEqual(elements2)) return true;
            else return false;
        }

        private List<string> getElements(String Formula)
        {
            List<string> elementlist = new List<string>();
            char[] formulachars = Formula.ToCharArray();
            int formulacharslength = formulachars.Length;
            for (int x = 0; x < formulacharslength; )
            {
                string currentelement = "";
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
                            x++;
                            try
                            {
                                while (char.IsNumber(formulachars[x])) x++;
                            }
                            catch (Exception) { }
                        }
                    }
                    catch (Exception) { }
                }
                else x++;
                elementlist.Add(currentelement);
            }
            return elementlist;
        }

        private List<int> getSubscripts(String Formula)
        {
            List<int> elementsubscript = new List<int>();
            char[] formulachars = Formula.ToCharArray();
            int formulacharslength = formulachars.Length;
            for (int x = 0; x < formulacharslength; )
            {
                int currentsubscript = 1;
                if (char.IsUpper(formulachars[x]))
                {
                    x++;
                    try
                    {
                        if (char.IsLower(formulachars[x])) x++;
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
                else x++;
                elementsubscript.Add(currentsubscript);
            }
            return elementsubscript;
        }

        private void maxcoefficient_ValueChanged(object sender, EventArgs e)
        {
            if (maxcoefficient.Value > 30 && !warned)
            {
                warned = true;
                MessageBox.Show("Large values may cause lag in calculations");
            }
        }
    }
}