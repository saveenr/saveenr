using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DemoMergeImagesSideBySide
{
    public partial class Form1 : Form
    {
        private int margin = 100;
        private int header = 150;
        private int spacing = 200;
        System.Drawing.Brush background_color = System.Drawing.Brushes.White;

        public Form1()
        {
            InitializeComponent();

            this.textBoxSrcFolders.Text = Properties.Settings.Default.SourceFolders ?? "";
            this.textBoxOutputFolder.Text = Properties.Settings.Default.OutputFolder.Trim();

        }

        private void TextRun_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.SourceFolders = this.textBoxSrcFolders.Text;
            Properties.Settings.Default.OutputFolder = this.textBoxOutputFolder.Text;
            Properties.Settings.Default.Save();

            List<string> src_folders = this.textBoxSrcFolders.Text.Split(new char[] { '\n' }).ToList();
            src_folders = src_folders.Select(s => s.Trim()).ToList();
            src_folders = src_folders.Where(s => s.Length > 0).ToList();


            string dest_folder = this.textBoxOutputFolder.Text;

            var src_titles = src_folders.Select(s => System.IO.Path.GetFileName(s)).ToList();

            string pat = "*.png";

            var src_files = src_folders.Select(f => System.IO.Directory.GetFiles(f, pat)).ToList();
            var src_keys = src_files.Select(files => get_key_map(files)).ToList();

            using (var headerfont = new System.Drawing.Font("Segoe UI", 30.0f))
            {
                foreach (string k in src_keys[0].Keys)
                {

                    int num_bmps = src_folders.Count;
                    var bmps = new List<System.Drawing.Bitmap>(num_bmps);

                    foreach (int i in Enumerable.Range(0, num_bmps).Reverse())
                    {
                        string src_filename = src_keys[i][k];
                        var src_bmp = new System.Drawing.Bitmap(src_filename);
                        bmps.Add(src_bmp);
                    }

                    int total_bmp_width = bmps.Select(bmp => bmp.Width).Sum();
                    int max_bmp_height = bmps.Select(bmp => bmp.Height).Max();


                    var ws = bmps.Select(i => i.Width).ToList();

                    int num_spaces = System.Math.Max(0, num_bmps - 1);
                    int w = total_bmp_width + (2 * margin) + (num_spaces* spacing);
                    int h = max_bmp_height + (2 * margin) + header;

                    string dest_filename = System.IO.Path.Combine(dest_folder, k + ".png");

                    using (var combined_bmp = new System.Drawing.Bitmap(w, h))
                    {
                        int cur_x = margin;
                        int cur_y = margin + header;

                        using (var gfx = System.Drawing.Graphics.FromImage(combined_bmp))
                        {
                            gfx.FillRectangle(background_color, 0, 0, w, h);
                            foreach (var bmp in bmps)
                            {
                                gfx.DrawImage(bmp,cur_x,cur_y,bmp.Width,bmp.Height);
                                cur_x += bmp.Width + spacing;
                            }

                        }
                        combined_bmp.Save(dest_filename);

                    }

                    foreach (var bmp in bmps)
                    {
                        bmp.Dispose();
                    }
                }
            }
        }

        public Dictionary<string,string> get_key_map(IList<string> filenames)
        {
            var dic = new Dictionary<string, string>(filenames.Count);
            foreach (string filename in filenames)
            {
                string key = System.IO.Path.GetFileNameWithoutExtension(filename);
                dic[key] = filename;
            }
            return dic;
        }
    }
}
