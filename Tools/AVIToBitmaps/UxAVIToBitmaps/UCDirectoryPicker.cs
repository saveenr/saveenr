using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace UxBitmapsToAVI
{
    public partial class UCDirectoryPicker : UserControl
    {
        public UCDirectoryPicker()
        {
            InitializeComponent();
        }


        [Category("Appearance"), Browsable(true)]
        public string Directory
        {
            get
            {
                string fname = this.textBoxDirectory.Text;
                fname = fname.Trim();
                return fname;
            }
            set
            {
                string fname = value;
                fname = fname.Trim();
                this.textBoxDirectory.Text = fname;
            }
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog1.SelectedPath  = this.Directory;

            DialogResult dr = this.folderBrowserDialog1.ShowDialog();
            if (DialogResult.OK == dr)
            {
                this.Directory = this.folderBrowserDialog1.SelectedPath;
            }
            else
            {
            }
        }
    }
}
