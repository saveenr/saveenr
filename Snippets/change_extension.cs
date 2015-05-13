        public static string ChangeExtension(string filename, string ext)
        {
            var path = System.IO.Path.GetDirectoryName(filename);
            var s = System.IO.Path.GetFileNameWithoutExtension(filename);
            return System.IO.Path.Combine(path,s + ext);
        }