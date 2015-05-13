
        public static void CreatePathForFile(string filename)
        {
            if (!System.IO.Path.IsPathRooted(filename))
            {
                throw new System.ArgumentException();
            }

            var p = System.IO.Path.GetDirectoryName(filename);
            System.IO.Directory.CreateDirectory(p);
        }