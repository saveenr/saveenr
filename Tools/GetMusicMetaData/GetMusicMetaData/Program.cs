using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetMusicMetaData
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            var musicinfo = new List<MusicInfo>();

            // Identify which fields are needed

            var query = new Query();
            var col_title = query.Add(WDSHelper.System_Title);
            var col_filename = query.Add(WDSHelper.System_Filename);
            var col_path = query.Add(WDSHelper.System_ItemPathDisplay);
            var col_dur = query.Add(WDSHelper.System_Media_Duration);
            var col_artists = query.Add(WDSHelper.System_Music_Artist);
            var col_albumartist = query.Add(WDSHelper.System_Music_AlbumArtist);
            var col_album = query.Add(WDSHelper.System_Music_AlbumTitle);
            var col_composer = query.Add(WDSHelper.System_Music_Composer);
            var col_size = query.Add(WDSHelper.System_Size);
            var col_bitrate = query.Add(WDSHelper.System_Audio_EncodingBitrate);
            var col_year = query.Add(WDSHelper.System_Media_Year);

            var fields = query.Columns.Select(c => c.Field).ToList();
            
            // Connect to WDS and get the results
            var wds_connection = WDSHelper.GetWDSConnection();
            var wds_results = WDSHelper.GetWDSResults(fields, @"D:\music", ".mp3", wds_connection);

            int max_results_to_show = 500000;
            
            int n = 0;

            var field_values = new object[fields.Count];

            System.Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}",
    "Title",
    "Filename",
    "Path",
    "Duration",
    "Artist",
    "AlbumArtist",
    "AlbumTitle",
    "Composer",
    "Size",
    "Bitrate",
    "Year");

            while (wds_results.Read())
            {
                int num_fields = wds_results.GetValues(field_values);

                var mi = WDSHelper.GetMusicInfoFromRow(field_values, col_title, col_filename, col_path, col_dur, col_artists, col_albumartist, col_album, col_composer, col_size, col_bitrate, col_year);

                System.Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}", 
                    mi.Title, 
                    mi.Filename, 
                    mi.Path, 
                    mi.Duration, 
                    string.Join(",",mi.Artist), 
                    mi.AlbumArtist,
                    mi.AlbumTitle,
                    string.Join(",",mi.Composer),
                    mi.Size,
                    mi.Bitrate,
                    mi.Year);
    
                musicinfo.Add(mi);
                n += 1;
                if (n >= max_results_to_show)
                {
                    break;
                }
            }

            wds_results.Close();
            wds_connection.Close();

        }
    }
}