using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Threading;

namespace atomicmasscalc
{
    public partial class Updating : Form
    {
        /*
        public Updating()
        {
            InitializeComponent();
        }
         */

        public Updating(String path)
        {
            InitializeComponent();
            WebClient wc = new WebClient();
            wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(progressChanged);
            wc.DownloadFileCompleted += new AsyncCompletedEventHandler(completed);
            int x = 1;
            while(File.Exists(path+"\\atomicmasscalc"+x+".zip"))
            {
                x++;
            }
            wc.DownloadFileAsync(new Uri("http://www.programdata.webs.com//atomicmasscalc.zip"),path+"\\atomicmasscalc"+x+".zip");
        }

        private void progressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void completed(object sender, AsyncCompletedEventArgs e)
        {
            //MessageBox.Show("Download completed!", "Done");
            MessageBox.Show("Update complete. Please visit: "+Application.StartupPath+"\\ to extract the program.", "Complete",MessageBoxButtons.OK,MessageBoxIcon.Information);
            this.Close();
        }
    }
}