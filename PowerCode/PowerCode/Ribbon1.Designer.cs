namespace PowerCode
{
    partial class Ribbon1 : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public Ribbon1()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tab1 = this.Factory.CreateRibbonTab();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.tab_powercode = this.Factory.CreateRibbonTab();
            this.group_fixquotes = this.Factory.CreateRibbonGroup();
            this.button_selection = this.Factory.CreateRibbonButton();
            this.button_slide = this.Factory.CreateRibbonButton();
            this.button_doc = this.Factory.CreateRibbonButton();
            this.group2 = this.Factory.CreateRibbonGroup();
            this.button_fit_to_slide_width = this.Factory.CreateRibbonButton();
            this.button_fit_to_slide_height = this.Factory.CreateRibbonButton();
            this.checkBox_auto_center = this.Factory.CreateRibbonCheckBox();
            this.tab1.SuspendLayout();
            this.tab_powercode.SuspendLayout();
            this.group_fixquotes.SuspendLayout();
            this.group2.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this.group1);
            this.tab1.Label = "TabAddIns";
            this.tab1.Name = "tab1";
            // 
            // group1
            // 
            this.group1.Label = "group1";
            this.group1.Name = "group1";
            // 
            // tab_powercode
            // 
            this.tab_powercode.Groups.Add(this.group_fixquotes);
            this.tab_powercode.Groups.Add(this.group2);
            this.tab_powercode.Label = "DevDoc";
            this.tab_powercode.Name = "tab_powercode";
            // 
            // group_fixquotes
            // 
            this.group_fixquotes.Items.Add(this.button_selection);
            this.group_fixquotes.Items.Add(this.button_slide);
            this.group_fixquotes.Items.Add(this.button_doc);
            this.group_fixquotes.Label = "Fix Quotes";
            this.group_fixquotes.Name = "group_fixquotes";
            // 
            // button_selection
            // 
            this.button_selection.Label = "Selection";
            this.button_selection.Name = "button_selection";
            this.button_selection.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button_selection_Click);
            // 
            // button_slide
            // 
            this.button_slide.Label = "Slide";
            this.button_slide.Name = "button_slide";
            this.button_slide.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button_slide_Click);
            // 
            // button_doc
            // 
            this.button_doc.Label = "Document";
            this.button_doc.Name = "button_doc";
            this.button_doc.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button_doc_Click);
            // 
            // group2
            // 
            this.group2.Items.Add(this.button_fit_to_slide_width);
            this.group2.Items.Add(this.button_fit_to_slide_height);
            this.group2.Items.Add(this.checkBox_auto_center);
            this.group2.Label = "Resize Image";
            this.group2.Name = "group2";
            // 
            // button_fit_to_slide_width
            // 
            this.button_fit_to_slide_width.Label = "Fit to slide width";
            this.button_fit_to_slide_width.Name = "button_fit_to_slide_width";
            this.button_fit_to_slide_width.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button_fit_to_slide_width_Click);
            // 
            // button_fit_to_slide_height
            // 
            this.button_fit_to_slide_height.Label = "Fit to slide height";
            this.button_fit_to_slide_height.Name = "button_fit_to_slide_height";
            this.button_fit_to_slide_height.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button_fit_to_slide_height_Click);
            // 
            // checkBox_auto_center
            // 
            this.checkBox_auto_center.Checked = true;
            this.checkBox_auto_center.Label = "Auto Center";
            this.checkBox_auto_center.Name = "checkBox_auto_center";
            // 
            // Ribbon1
            // 
            this.Name = "Ribbon1";
            this.RibbonType = "Microsoft.PowerPoint.Presentation";
            this.Tabs.Add(this.tab1);
            this.Tabs.Add(this.tab_powercode);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.Ribbon1_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.tab_powercode.ResumeLayout(false);
            this.tab_powercode.PerformLayout();
            this.group_fixquotes.ResumeLayout(false);
            this.group_fixquotes.PerformLayout();
            this.group2.ResumeLayout(false);
            this.group2.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        private Microsoft.Office.Tools.Ribbon.RibbonTab tab_powercode;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group_fixquotes;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button_selection;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button_slide;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button_doc;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group2;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button_fit_to_slide_width;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button_fit_to_slide_height;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox checkBox_auto_center;
    }

    partial class ThisRibbonCollection
    {
        internal Ribbon1 Ribbon1
        {
            get { return this.GetRibbon<Ribbon1>(); }
        }
    }
}
