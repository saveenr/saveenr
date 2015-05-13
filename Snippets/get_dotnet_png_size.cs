
        public static long GetDotNetPNGSize(string filename, System.IO.MemoryStream memstream)
        {
            // resets the memory stream
            // encodes as PNG
            // and returns the number of bytes in the stream
            using (var bmp0 = new System.Drawing.Bitmap(filename))
            {
                memstream.SetLength(0);
                bmp0.Save(memstream, System.Drawing.Imaging.ImageFormat.Png);

                long memfilesize = memstream.Length;
                return memfilesize;
            }
        }