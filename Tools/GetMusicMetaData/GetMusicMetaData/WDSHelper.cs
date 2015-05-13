
using System;
using System.Linq;
using System.Collections.Generic;

namespace GetMusicMetaData
{
    public class WDSHelper
    {
        public static WDSField System_Title = new WDSField("System.Title", FieldType.String);
        public static WDSField System_Filename = new WDSField("System.FileName", FieldType.String);
        public static WDSField System_ItemPathDisplay = new WDSField("System.ItemPathDisplay", FieldType.String);
        public static WDSField System_Media_Duration = new WDSField("System.Media.Duration", FieldType.Decimal);
        public static WDSField System_Music_Artist = new WDSField("System.Music.Artist", FieldType.StringArray);
        public static WDSField System_Music_AlbumArtist = new WDSField("System.Music.AlbumArtist", FieldType.String);
        public static WDSField System_Music_AlbumTitle = new WDSField("System.Music.AlbumTitle", FieldType.String);
        public static WDSField System_Music_Composer = new WDSField("System.Music.Composer", FieldType.StringArray);
        public static WDSField System_Size = new WDSField("System.Size ", FieldType.Decimal);

        public static WDSField System_Audio_EncodingBitrate = new WDSField("System.Audio.EncodingBitrate",
                                                                             FieldType.Int32);

        public static WDSField System_Media_Year = new WDSField("System.Media.Year", FieldType.Int32);

        public static System.Data.OleDb.OleDbDataReader GetWDSResults(IList<WDSField> fields, string scope, string extention,  System.Data.OleDb.OleDbConnection wds_connection)
        {
            var fieldnames = System.String.Join(",", fields.Select(f => f.Name));

            var sb = new System.Text.StringBuilder();
            sb.AppendFormat(@"SELECT {0} ", fieldnames);
            sb.AppendFormat(@"FROM SYSTEMINDEX  ");

            var clauses = new List<string>();
            if (scope != null)
            {
                clauses.Add(string.Format(@"SCOPE='{0}' ", scope));
            }
            if (extention != null)
            {
                clauses.Add(string.Format(@"System.ItemType='{0}' ", extention));
            }

            if (clauses.Count > 0)
            {
                sb.AppendFormat(@"WHERE ");

                int n = 0;
                foreach (string clause in clauses)
                {
                    if (n > 0)
                    {
                        sb.Append(" AND ");
                    }
                    sb.Append(clause);
                    n++;
                }
            }

            string wds_query = sb.ToString();

            var x = System.String.Format(
                    @"SELECT {0} FROM SYSTEMINDEX WHERE SCOPE='{1}' AND System.ItemType = '.mp3'", fieldnames, scope);
            var wds_command = new System.Data.OleDb.OleDbCommand(wds_query, wds_connection);
            return wds_command.ExecuteReader();
        }

        public static System.Data.OleDb.OleDbConnection GetWDSConnection()
        {
            string wds_connection_string = @"Provider=Search.CollatorDSO;Extended Properties='Application=Windows';";
            var wds_connection = new System.Data.OleDb.OleDbConnection(wds_connection_string);
            wds_connection.Open();
            return wds_connection;
        }

        public static string GetString(object o)
        {
            var t = o.GetType();
            if (t==typeof(System.DBNull))
            {
                return null;
            }
            else
            {
                return (string) o;
            }
        }

        public static decimal GetDecimal(object o)
        {
            var t = o.GetType();
            if (t == typeof(System.DBNull))
            {
                return System.Decimal.Zero;
            }
            else
            {
                return (decimal)o;
            }
        }

        public static int GetInt32(object o)
        {
            var t = o.GetType();
            if (t == typeof(System.DBNull))
            {
                return 0;
            }
            else
            {
                return (int)o;
            }
        }
        public static string[] GetStringArray(object o)
        {
            var t = o.GetType();
            if (t == typeof(System.DBNull))
            {
                return new string[0];
            }
            else
            {
                return (string [])o;
            }
        }

        public static MusicInfo GetMusicInfoFromRow(object[] values, QueryColumn col_title, QueryColumn col_filename, QueryColumn col_path, QueryColumn col_dur, QueryColumn col_artists, QueryColumn col_albumartist, QueryColumn col_album, QueryColumn col_composer, QueryColumn col_size, QueryColumn col_bitrate, QueryColumn col_year)
        {
            var item = new MusicInfo();
            item.Title = GetString( values[col_title.Ordinal] );
            item.Filename= GetString(values[col_filename.Ordinal]);
            item.Path = GetString(values[col_path.Ordinal]);
            item.Duration = GetDecimal(values[col_dur.Ordinal]);
            item.Artist = GetStringArray(values[col_artists.Ordinal]);
            item.AlbumArtist= GetString(values[col_albumartist.Ordinal]);
            item.AlbumTitle= GetString(values[col_album.Ordinal]);
            item.Composer = GetStringArray(values[col_composer.Ordinal]);
            item.Size = GetDecimal(values[col_size.Ordinal]);
            item.Bitrate= GetInt32(values[col_bitrate.Ordinal]);
            item.Year= GetInt32(values[col_year.Ordinal]);
            return item;
        }
    }
}