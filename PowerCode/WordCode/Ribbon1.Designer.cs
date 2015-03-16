namespace WordCode
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
            this.tab_WordCode = this.Factory.CreateRibbonTab();
            this.group_fix_quotes = this.Factory.CreateRibbonGroup();
            this.button_remove_from_selection = this.Factory.CreateRibbonButton();
            this.button_remove_from_document = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.tab_WordCode.SuspendLayout();
            this.group_fix_quotes.SuspendLayout();
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
            // tab_WordCode
            // 
            this.tab_WordCode.Groups.Add(this.group_fix_quotes);
            this.tab_WordCode.Label = "DevDoc";
            this.tab_WordCode.Name = "tab_WordCode";
            // 
            // group_fix_quotes
            // 
            this.group_fix_quotes.Items.Add(this.button_remove_from_selection);
            this.group_fix_quotes.Items.Add(this.button_remove_from_document);
            this.group_fix_quotes.Label = "Fix Quotes";
            this.group_fix_quotes.Name = "group_fix_quotes";
            // 
            // button_remove_from_selection
            // 
            this.button_remove_from_selection.Label = "Selection";
            this.button_remove_from_selection.Name = "button_remove_from_selection";
            this.button_remove_from_selection.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button_remove_from_selection_Click);
            // 
            // button_remove_from_document
            // 
            this.button_remove_from_document.Label = "Document";
            this.button_remove_from_document.Name = "button_remove_from_document";
            this.button_remove_from_document.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button_remove_from_document_Click);
            // 
            // Ribbon1
            // 
            this.Name = "Ribbon1";
            this.RibbonType = "Microsoft.Word.Document";
            this.Tabs.Add(this.tab1);
            this.Tabs.Add(this.tab_WordCode);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.Ribbon1_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.tab_WordCode.ResumeLayout(false);
            this.tab_WordCode.PerformLayout();
            this.group_fix_quotes.ResumeLayout(false);
            this.group_fix_quotes.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        private Microsoft.Office.Tools.Ribbon.RibbonTab tab_WordCode;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group_fix_quotes;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button_remove_from_selection;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button_remove_from_document;
    }

    partial class ThisRibbonCollection
    {
        internal Ribbon1 Ribbon1
        {
            get { return this.GetRibbon<Ribbon1>(); }
        }
    }
}
