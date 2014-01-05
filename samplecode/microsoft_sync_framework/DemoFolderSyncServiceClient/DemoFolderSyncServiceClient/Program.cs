using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DemoFolderSyncServiceClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new FolderSyncService.FolderSyncServiceClient();
            string outputfolder = @"D:\foldersync";

            var files_info = client.GetFileStatus();
            foreach ( var file in files_info)
            {
                Console.WriteLine("File Name=\"{0}\" Size=\"{1}\" ",file.Name, file.Size);
            }

            if ( !System.IO.Directory.Exists(outputfolder ))
            {
                System.IO.Directory.CreateDirectory(outputfolder);
            }
            foreach (var file in files_info)
            {
                string localfilename = System.IO.Path.Combine(
                    outputfolder, file.Name);

                bool download = false;
                if (!System.IO.File.Exists(localfilename))
                {
                    Console.WriteLine("Local file does not exist, setting download to true");
                    download = true;
                }
                else
                {
                    var local_info = new FileInfo(localfilename);
                    Console.WriteLine("UTC remote {0} vs local {1}", file.CreateTimeUtc, local_info.LastWriteTimeUtc);

                    if (file.CreateTimeUtc > local_info.LastWriteTimeUtc)
                    {
                        Console.WriteLine("Remote is more recent that local");
                        download = true;
                    }

                }

                if (download)
                {
                    if (System.IO.File.Exists(localfilename))
                    {
                        System.IO.File.Delete(localfilename);
                    }

                    using (var fp = System.IO.File.Create(localfilename))
                    {
                        Console.WriteLine("Downloading {0}", file.Name);
                        int bytesread = 0;
                        int bufsize = 16384; // max array length in wcf
                        while (bytesread < file.Size)
                        {
                            Console.WriteLine("Downloading bytes {0} {1} total={2}", file.Name, bytesread, file.Size);
                            var received_bytes = client.Download(file.Name, bytesread, bufsize);

                            if (received_bytes.Length == 0)
                            {
                                break;
                            }

                            fp.Write(received_bytes,0,received_bytes.Length);
                            bytesread += received_bytes.Length;
                        }

                        if (bytesread != file.Size)
                        {
                            throw new SystemException("didn't read correct number of bytes");
                        }
                        fp.Close();
                    }


                }

            }

        }
    }
}
