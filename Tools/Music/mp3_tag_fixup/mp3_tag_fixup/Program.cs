using System;
using TagLib;

namespace mp3_tag_fixup
{
    class Program
    {
        static void Main(string[] args)
        {

            TagLib.Id3v2.Tag.DefaultVersion = 3;
            TagLib.Id3v2.Tag.ForceDefaultVersion = true;

            string path = @"D:\music\downloaded";
            Console.WriteLine(path);

            foreach (var data in DirectoryHelper.Recurse(path,"*.mp3"))
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
            album_artists_merged = NormalizeArtistName(album_artists_merged);

            string[] contributing_artists = tagfile.Tag.Performers;
            string contributing_artists_merged = string.Join(",", contributing_artists);
            contributing_artists_merged = NormalizeArtistName(contributing_artists_merged);

            string[] composers = tagfile.Tag.Composers;
            string composers_merged = string.Join(",", composers);
            composers_merged = NormalizeArtistName(composers_merged);

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
            if (IsNullOrEmpty(title))
            {
                string newtitle = System.IO.Path.GetFileNameWithoutExtension(filename);
                tagfile.Tag.Title = newtitle;
                perform_save = true;
                Console.WriteLine("    title -> \"{0}\"", newtitle);
            }
            return perform_save;
        }

        private static bool IsNullOrEmpty(string title)
        {
            return title==null || title.Trim().Length==0;
        }

        private static string NormalizeArtistName(string s)
        {
            if (s==null)
            {
                return "";
            }

            var t = s.Trim().ToLower();
            if (t=="original soundtrack" || t == "soundtrack" || t=="various" || t=="various artists")
            {
                return "";
            }

            return s;
        }

    }
}
