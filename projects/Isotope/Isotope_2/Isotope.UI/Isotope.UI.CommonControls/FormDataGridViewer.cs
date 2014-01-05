using System.Windows.Forms;

namespace Isotope.UI.CommonControls
{
    public partial class FormDataGridViewer : Form
    {
        public FormDataGridViewer()
        {
            InitializeComponent();
        }

        public object DataSource
        {
            get { return _datasource; }
            set { _datasource = value; }
        }

        private void FormDataTableGridViewer_Load(object sender, System.EventArgs e)
        {

            if (this.DataSource!=null)
            {
                this.bindingSource1.DataSource = this.DataSource;
                this.dataGridView1.DataSource = this.bindingSource1;   
            }
            else
            {
                this.dataGridView1.Visible = false;
                this.Text = "No DataSource specified";
            }
        }

        private object _datasource;


        public DataGridView DataGridView
        {
            get
            {
                return this.dataGridView1;
            }
        }

        private void saveAsExcel2003XMLFormatToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (this.DataSource is System.Data.DataTable)
            {
                PickFilenameAndSave();
            }
            else
            {
                MessageBox.Show("Does not support the bound datasource");
            }
        }

        private void PickFilenameAndSave()
        {
            var form = new SaveFileDialog();
            if (form.ShowDialog()==DialogResult.OK)
            {
                string filename = form.FileName;
                var datatable = (System.Data.DataTable) this.DataSource;
                Isotope.Data.DataExporter.ToExcelXML(datatable,filename,"Sheet1",true);
            }
        }
    }
}
