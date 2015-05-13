    public class PathMapper
    {

        public class RootPath
        {
            public string Value;

            public RootPath(string path)
            {
                if (!System.IO.Path.IsPathRooted(path))
                {
                    throw new System.ArgumentOutOfRangeException();
                }
                this.Value = path;
                if (!this.Value.EndsWith("\\"))
                {
                    this.Value = this.Value + "\\";
                }
            }

            public string Combine(string path)
            {
                AssertRelPath(path);
                return System.IO.Path.Combine(this.Value, path);
            }

            public bool Contains(string path)
            {
                AssertAbsPath(path);
                if (path.StartsWith(this.Value))
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }

            public static void AssertAbsPath(string path)
            {
                if (!System.IO.Path.IsPathRooted(path))
                {
                    throw new System.ArgumentOutOfRangeException("path", "Not an absolute path");
                }                
            }

            public static void AssertRelPath(string path)
            {
                if (System.IO.Path.IsPathRooted(path))
                {
                    throw new System.ArgumentOutOfRangeException("path", "Not an relative path");
                }
            }

            public string GetRel(string path)
            {
                if (this.Contains(path))
                {
                    return path.Substring(this.Value.Length);
                }
                else
                {
                    throw new System.ArgumentOutOfRangeException("path", "does not contain this path");
                }
            }

            public string MapTo(RootPath root, string path)
            {
                if (this.Contains(path))
                {
                    string rel = this.GetRel(path);
                    return root.Combine(rel);
                }
                else
                {
                    throw new System.ArgumentOutOfRangeException("path", "does not contain this path");
                }
            }
 
        }

        public RootPath Left;
        public RootPath Right;

        public PathMapper(string left, string right)
        {
            this.Left = new RootPath(left);
            this.Right = new RootPath(right);
        }

        public string LeftToRight(string path)
        {
            return this.Left.MapTo(this.Right, path);
        }

        public string RightToLeft(string path)
        {
            return this.Right.MapTo(this.Left, path);
        }

    }
