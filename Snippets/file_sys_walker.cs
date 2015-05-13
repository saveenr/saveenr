public static class FileSysWalker
{

	public static long GetFileSize(string path)
	{
		var fi = new System.IO.FileInfo(path);
		return fi.Length;
	}

	public struct DirTreeWalkRecord
	{
		public string Path;
		public string[] Files;
	}

	public static IEnumerable<string> RecurseFolder(string path)
	{
		var stack = new Stack<string>();
		stack.Push(path);
		while (stack.Count > 0)
		{
			string cur_folder = stack.Pop();
			yield return cur_folder;
			var subfolders = System.IO.Directory.GetDirectories(cur_folder);
			for (int i = 0; i < subfolders.Length; i++)
			{
				stack.Push(subfolders[i]);
			}
		}
	}

	public static IEnumerable<DirTreeWalkRecord> RecurseFolderAndFiles(string path, string pattern)
	{
		foreach (string curpath in FileSysWalker.RecurseFolder(path))
		{
			var rec = new DirTreeWalkRecord();
			rec.Path = curpath;
			rec.Files = System.IO.Directory.GetFiles(curpath, pattern);
			yield return rec;
		}
	}


    public void demo()
    {
        string path_a = @"D:\foobar";

        foreach (var rec_a in FileSysWalker.RecurseFolderAndFiles(path_a, "*.*"))
        {
            foreach (string file_a in rec_a.Files)
            {
            }

        }
    }

	
}