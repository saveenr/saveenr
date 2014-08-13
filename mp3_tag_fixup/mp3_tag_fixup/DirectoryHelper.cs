using System.Collections.Generic;

namespace mp3_tag_fixup
{
    public static class DirectoryHelper
    {
        public static IEnumerable<DirectoryAndFiles> Recurse(string directory, string searchpattern)
        {
            var stack = new Stack<string>();
            stack.Push(directory);

            while (stack.Count > 0)
            {
                string cur_dir = stack.Pop();
                var cur_files = System.IO.Directory.GetFiles(cur_dir, searchpattern);

                var item = new DirectoryAndFiles(cur_dir,cur_files);
                yield return item;

                foreach (var sd in System.IO.Directory.GetDirectories(cur_dir))
                {
                    stack.Push(sd);
                }
            }
        }
    }

    public class DirectoryAndFiles
    {
        public readonly string Directory;
        public readonly string[] Files;

        public DirectoryAndFiles(string dir, string[] files)
        {
            this.Directory = dir;
            this.Files = files;
        }
    }
}