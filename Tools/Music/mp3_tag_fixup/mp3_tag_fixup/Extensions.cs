using System;

namespace mp3_tag_fixup
{
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