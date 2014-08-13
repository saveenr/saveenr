using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FileNameTimeStampApp
{
    public partial class FormFileRenamerApp : Form
    {
        public FormFileRenamerApp()
        {
            InitializeComponent();

            this.listViewInputFiles.AllowDrop = true;
            this.listViewInputFiles.DragDrop += new System.Windows.Forms.DragEventHandler(this.dragdrop);
            this.listViewInputFiles.DragEnter += new System.Windows.Forms.DragEventHandler(this.dragenter);

        }

        private void dragenter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void dragdrop(object sender, DragEventArgs e)
        {
            var x = (string []) e.Data.GetData(DataFormats.FileDrop);

            if (x==null)
            {
                return;
            }


            foreach (string file in x)
            {
                var i = new RenameItem(file);
                this.listViewInputFiles.Items.Add( i );
                Console.WriteLine(file);
            }


        }

        private void buttonScriptToClipboard_Click(object sender, EventArgs e)
        {
            var sb = new StringBuilder();
            var items = this.listViewInputFiles.Items.AsEnumerable()
                .Select( i => i.Tag)
                .Cast<RenameItem>();

            var strings = items.Select(i => string.Format("REN \"{0}\" \"{1}\"",i.GetOldFull(),i.GetNewFull()));
            foreach (var s in strings )
            {
                sb.AppendLine(s);
               
            }

            var t = sb.ToString();
            System.Windows.Forms.Clipboard.SetText(t);
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            this.listViewInputFiles.Items.Clear();
        }
    }

    public class RenameItem 
    {
        public string Path;
        public string OldName;
        public string NewName;
        public System.DateTime NewDiskTime;
        public System.IO.FileInfo Info;

        public string GetOldFull()
        {
            return System.IO.Path.Combine(this.Path, this.OldName);
        }

        public string GetNewFull()
        {
            return System.IO.Path.Combine(this.Path, this.NewName);
        }

        public RenameItem(string path )
        {
            this.Path = System.IO.Path.GetDirectoryName(path);
            this.OldName = System.IO.Path.GetFileName(path);
            this.Info = new System.IO.FileInfo(path);

            var stamper = new FilenameTimestamp.Stamper();
            var res = stamper.parse(path);
            this.NewName = res.Remainder;

            var lastmod = this.Info.LastWriteTime;
            var create = this.Info.CreationTime;

            var disktime = EXT.Min(lastmod,create);
            var rename_result = stamper.GetRenameInfo2(this.OldName, disktime);
            this.NewName = rename_result.NewName;
            this.NewDiskTime = rename_result.NewDiskTime;

        }

    }

    public static class EXT
    {
        public static void Add( this ListView.ListViewItemCollection col, RenameItem i )
        {
            var lvi = new ListViewItem(new[] { i.Path, i.Info.CreationTime.ToShortDateString(), i.Info.LastWriteTime.ToShortDateString(), i.OldName, i.NewName });
            lvi.Tag = i;
            col.Add(lvi);
        }

        public static System.DateTime Min(System.DateTime a, System.DateTime b)
        {
            if ( a< b)
            {
                return a;
            }
            else
            {
                return b;
            }
            
        }

        public static IEnumerable<System.Windows.Forms.ListViewItem> AsEnumerable( this System.Windows.Forms.ListView.ListViewItemCollection col )
        {
            foreach (System.Windows.Forms.ListViewItem i in col)
            {
                yield return i;
            }
        }

        public static System.DateTime Max(System.DateTime a, System.DateTime b)
        {
            if (a > b)
            {
                return a;
            }
            else
            {
                return b;
            }

        }

    }
}
