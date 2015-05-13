
        public static long GetFileSize(string path)
        {
            var fi = new System.IO.FileInfo(path);
            return fi.Length;
        }