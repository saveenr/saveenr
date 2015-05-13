        public static void SaveJPEG(System.Drawing.Image bmp0, System.IO.Stream stream, long quality)
        {
            if ((quality < 0) || (quality>100))
            {
                throw new System.ArgumentOutOfRangeException("quality");
            }

            var jpg_encoder = GetEncoder(System.Drawing.Imaging.ImageFormat.Jpeg);
            var parameters = new System.Drawing.Imaging.EncoderParameters(1);
            var jpg_qual_encoder = System.Drawing.Imaging.Encoder.Quality;
            var jpg_qual_parameters = new System.Drawing.Imaging.EncoderParameter(jpg_qual_encoder, quality);
            parameters.Param[0] = jpg_qual_parameters;
            bmp0.Save(stream, jpg_encoder, parameters);
        }

        public static void SaveJPEG(System.Drawing.Image bmp0, string filename,long quality)
        {
            var stream = System.IO.File.Create(filename);
            SaveJPEG(bmp0, stream,quality);
        }

        private static System.Drawing.Imaging.ImageCodecInfo GetEncoder(System.Drawing.Imaging.ImageFormat format)
        {

            var codecs = System.Drawing.Imaging.ImageCodecInfo.GetImageDecoders();

            foreach (var codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }