using System.Collections.Generic;

// Add a Reference to WMPLib at c:\windows\system32\wmp.dll
// for VS2010 and VS2010 - make sure that for the WMPLib 
//    reference you set Embed Interop Types to FALSE
using System.IO;
using CsvHelper;

namespace ExportWMPMetdataToCSV
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                System.Console.WriteLine("Syntax: ExportWMPMetadataToCSV.exe <outfile> [<top>]");
                return;
            }

            int? top = null;

            if (args.Length >= 2)
            {
                top = int.Parse(args[1]);
            }

            string output_filename = args[0];
            var normalized_names = new Dictionary<string, string>();
            foreach (var attr in all_attributes)
            {
                string name = attr.Replace("/", "_");
                normalized_names[attr] = name;
            }

            var wmp = new WMPLib.WindowsMediaPlayerClass();
            var item_collection = wmp.getAll();

            using (var writer = File.CreateText(output_filename))
            using (var csv = new CsvWriter(writer))
            {
                // Write CSV Header
                for (int i = 0; i < all_attributes.Length; i++)
                {
                    string h = normalized_names[all_attributes[i]];
                    csv.WriteField(h);
                }
                csv.NextRecord();

                for (int i = 0; i < item_collection.count; i++)
                {
                    if (top.HasValue && i>= top.Value)
                    {
                        break;
                    }

                    System.Console.WriteLine("WMP Item #: {0}", i + 1);
                    // write a csv line for each item in the collection
                    var item = item_collection.get_Item(i);

                    for (int j = 0; j < all_attributes.Length; j++)
                    {
                        var attr_value = item.getItemInfo(all_attributes[j]) ?? "";
                        csv.WriteField(attr_value);
                    }
                    csv.NextRecord();
                }
            }

            System.Console.WriteLine("DONE");

        }

        private static string[] all_attributes = new string[]
                                               {
                                                   "AcquisitionTime",
                                                   "AlbumID",
                                                   "AlbumIDAlbumArtist",
                                                   "Author",
                                                   "AverageLevel",
                                                   "Bitrate",
                                                   "BuyNow",
                                                   "BuyTickets",
                                                   "Copyright",
                                                   "CurrentBitrate",
                                                   "Duration",
                                                   "FileSize",
                                                   "FileType",
                                                   "Is_Protected",
                                                   "IsVBR",
                                                   "MediaType",
                                                   "MoreInfo",
                                                   "PeakValue",
                                                   "ProviderLogoURL",
                                                   "ProviderURL",
                                                   "RecordingTime",
                                                   "ReleaseDate",
                                                   "RequestState",
                                                   "SourceURL",
                                                   "SyncState",
                                                   "Title",
                                                   "TrackingID",
                                                   "UserCustom1",
                                                   "UserCustom2",
                                                   "UserEffectiveRating",
                                                   "UserLastPlayedTime",
                                                   "UserPlayCount",
                                                   "UserPlaycountAfternoon",
                                                   "UserPlaycountEvening",
                                                   "UserPlaycountMorning",
                                                   "UserPlaycountNight",
                                                   "UserPlaycountWeekday",
                                                   "UserPlaycountWeekend",
                                                   "UserRating",
                                                   "UserServiceRating",
                                                   "WM/AlbumArtist",
                                                   "WM/AlbumTitle",
                                                   "WM/Category",
                                                   "WM/Composer",
                                                   "WM/Conductor",
                                                   "WM/ContentDistributor",
                                                   "WM/ContentGroupDescription",
                                                   "WM/EncodingTime",
                                                   "WM/Genre",
                                                   "WM/GenreID",
                                                   "WM/InitialKey",
                                                   "WM/Language",
                                                   "WM/Lyrics",
                                                   "WM/MCDI",
                                                   "WM/MediaClassPrimaryID",
                                                   "WM/MediaClassSecondaryID",
                                                   "WM/Mood",
                                                   "WM/ParentalRating",
                                                   "WM/Period",
                                                   "WM/ProtectionType",
                                                   "WM/Provider",
                                                   "WM/ProviderRating",
                                                   "WM/ProviderStyle",
                                                   "WM/Publisher",
                                                   "WM/SubscriptionContentID",
                                                   "WM/SubTitle",
                                                   "WM/TrackNumber",
                                                   "WM/UniqueFileIdentifier",
                                                   "WM/WMCollectionGroupID",
                                                   "WM/WMCollectionID",
                                                   "WM/WMContentID",
                                                   "WM/Writer"
                                               };
    }
}
