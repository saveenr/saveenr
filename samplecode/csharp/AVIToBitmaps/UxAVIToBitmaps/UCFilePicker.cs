using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace UxBitmapsToAVI
{
    public partial class UCFilePicker : UserControl
    {
        bool m_allow_editing = true;
        public UCFilePicker()
        {
            InitializeComponent();
            this.AllowEditing = false;
        }



        [Category("Appearance"), Browsable(true)]
        public string Filename
        {
            get 
            {
                string fname = this.textBoxFile.Text;
                fname = fname.Trim();
                return fname; 
            }
            set 
            {
                string fname = value;
                fname = fname.Trim();
                this.textBoxFile.Text = fname; 
            }
        }


        [Category("Appearance"), Browsable(true)]
        public bool AllowEditing
        {
            get { return this.m_allow_editing ; }
            set { 
                this.m_allow_editing = value;
                this.textBoxFile.Enabled = this.m_allow_editing; 
            }
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {

            this.openFileDialog1.FileName = this.Filename;
            DialogResult dr = this.openFileDialog1.ShowDialog();
            if (DialogResult.OK == dr)
            {
                this.textBoxFile.Text = this.openFileDialog1.FileName;
            }
            else
            {
            }

        }

    }
}
