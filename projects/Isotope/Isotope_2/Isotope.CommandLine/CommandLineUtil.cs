using System.IO;

namespace Isotope.CommandLine
{
    /// <summary>
    /// This class is intended for use by those writing command line applications.
    /// </summary>
    /// <remarks>
    /// Many of these functions directly output to the console</remarks>
    public static class CommandLineUtil
    {
        /// <summary>
        /// Terminates the cmd line app with an error message
        /// </summary>
        /// <param name="s"></param>
        private static void ExitWithError(string s)
        {
            System.Console.WriteLine("");
            System.Console.WriteLine("ERROR: " + s);
            System.Console.WriteLine("Exiting");
            System.Console.WriteLine("");

            System.Environment.Exit(-1);
        }

        /// <summary>
        /// Deletes a file if the file exists. If it does not exists, silently continues
        /// </summary>
        /// <param name="path"></param>
        public static void DeleteFileIfExists(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        /// <summary>
        /// Checks if the parent path for the given path exists. If it does not,then exits the application with an error
        /// </summary>
        /// <param name="path"></param>
        public static void CheckParentPathExists(string path)
        {
            string parent_path = Path.GetDirectoryName(path);
            if (Directory.Exists(parent_path)) return;

            string msg = string.Format("Path {0} does not exist", parent_path);
            ExitWithError(msg);
        }

        /// <summary>
        /// Checks wether a directory exists. If it does not, then exists the application with an error.
        /// </summary>
        /// <param name="path"></param>
        public static void CheckDirectoryExists(string path)
        {
            if (Directory.Exists(path)) return;

            string msg = string.Format("Path {0} does not exist", path);
            ExitWithError(msg);
        }

        /// <summary>
        /// Checks wether a file exists. If it does not, then exists the application with an error.
        /// </summary>
        /// <param name="filename"></param>
        public static void CheckFileExists(string filename)
        {
            if (File.Exists(filename)) return;

            string msg = string.Format("File {0} does not exist", filename);
            ExitWithError(msg);
        }

        /// <summary>
        /// If the given filname does not end with the extension the application exits with an error.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="ext"></param>
        public static void CheckHasExtension(string filename, string ext)
        {
            ext = ext.ToLower();
            string actual_ext = Path.GetExtension(filename).ToLower(System.Globalization.CultureInfo.InvariantCulture);
            if (ext == actual_ext) return;

            string msg = string.Format("File {0} does have correct extension {1}", filename, ext);
            ExitWithError(msg);
        }

        /// <summary>
        /// Given a string will try to parse "yes" answers into true and "no" no answers into false
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool? TryParseYesNo(string s)
        {
            if (s == null)
            {
                return null;
            }
            if (s.Length < 1)
            {
                return null;
            }
            s = s.ToLower();
            switch (s)
            {
                case "y":
                    return true;
                case "yes":
                    return true;
                case "n":
                    return false;
                case "no":
                    return false;
                default:
                    {
                        return null;
                    }
            }
        }

        /// <summary>
        /// Converts a string containing a yes or no value to a bool (y=true,yes=true,n=false,no=false,else=defval)
        /// </summary>
        /// <param name="s">input string</param>
        /// <param name="defval">what to default to</param>
        /// <returns>the bool value</returns>
        public static bool ParseYesNo(string s, bool defval)
        {
            var v = TryParseYesNo(s);
            if (!v.HasValue)
            {
                return defval;
            }
            return v.Value;
        }

        public static bool ParseYesNo(string s)
        {
            var v = TryParseYesNo(s);
            if (!v.HasValue)
            {
                throw new System.FormatException("must be one of one of Yes, Y, No, or N");
            }
            return v.Value;
        }

        public static string BoolToYesNoString(bool v)
        {
            return v ? "Yes" : "No";
        }
    }
}