using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TagLib;

namespace mp3_tag_fixup
{
    class Program
    {
        static void Main(string[] args)
        {

            string path = @"D:\music";
            foreach (var data in DirectoryAndFiles.Recurse(path,"*.mp3"))
            {
                foreach (string filename in data.Files)
                {
                    handle_mp3(filename);
                }
            }
        }

        public static void handle_mp3(string filename)
        {
            Console.WriteLine(filename);
            using (var tagfile = TagLib.File.Create(filename))
            {
                bool perform_save = false;

                perform_save = perform_save | fixup_title(filename, tagfile);
                perform_save = perform_save | fixup_album_artist(tagfile);

                if (perform_save)
                {
                    tagfile.SaveSafe();
                }
            }
        }

        private static bool fixup_album_artist(File tagfile)
        {
            bool perform_save = false;
            string [] album_artists = tagfile.Tag.AlbumArtists;
            string album_artists_merged = string.Join(",", album_artists);
            album_artists_merged = normalizename(album_artists_merged);

            string[] contributing_artists = tagfile.Tag.Performers;
            string contributing_artists_merged = string.Join(",", contributing_artists);
            contributing_artists_merged = normalizename(contributing_artists_merged);

            string[] composers = tagfile.Tag.Composers;
            string composers_merged = string.Join(",", composers);
            composers_merged = normalizename(composers_merged);

            if (album_artists_merged=="")
            {
                string [] new_album_artists = null;
                if (contributing_artists_merged!="")
                {
                    new_album_artists = tagfile.Tag.Performers;
                }
                else if (composers_merged != "")
                {
                    new_album_artists = tagfile.Tag.Composers;
                }

                if (new_album_artists!=null)
                {
                    Console.WriteLine("    album artist -> \"{0}\"", contributing_artists_merged);
                    tagfile.Tag.AlbumArtists = tagfile.Tag.Performers;
                    perform_save = true;
                }
            }
            return perform_save;
        }

        private static bool fixup_title(string filename, File tagfile)
        {
            bool perform_save = false;
            string title = tagfile.Tag.Title;
            if (novalue(title))
            {
                string newtitle = System.IO.Path.GetFileNameWithoutExtension(filename);
                tagfile.Tag.Title = newtitle;
                perform_save = true;
                Console.WriteLine("    title -> \"{0}\"", newtitle);
            }
            return perform_save;
        }

        private static bool novalue(string title)
        {
            return title==null || title.Trim().Length==0;
        }

        private static string normalizename(string s)
        {
            if (s==null)
            {
                return "";
            }

            var t = s.Trim().ToLower();
            if (t=="original soundtrack" || t == "soundtrack" || t=="various" || t=="various artists")
            {
                int x = 7;
                return "";
            }

            return s;
        }

    }

    public static class Extensions
    {
        public static void SaveSafe(this TagLib.File file)
        {
            bool debug = false;
            if (debug)
            {
                Console.WriteLine("DEBUG Saving file {0}", file.Name);
                
            }
            else
            {
                Console.WriteLine("Saving file {0}", file.Name);
                file.Save();
                
            }
        }
    }
}
