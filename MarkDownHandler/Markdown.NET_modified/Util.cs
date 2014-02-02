using System.Security.Cryptography;
using System.Text;

namespace MarkdownDotNET
{
    internal class Util
    {
        /// <summary>
        /// Calculate an MD5 hash of an arbitrary string
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ComputeMD5(string text)
        {
            MD5 algo = MD5.Create();
            byte[] plainText = Encoding.UTF8.GetBytes(text);
            byte[] hashedText = algo.ComputeHash(plainText);
            string res = null;

            foreach (byte b in hashedText)
            {
                res += b.ToString("x2");
            }

            return res;
        }

        /// <summary>
        /// This is to emulate what's evailable in PHP
        /// </summary>
        /// <param name="text"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string RepeatString(string text, int count)
        {
            int total_len = text.Length*count;
            var sb = new System.Text.StringBuilder(total_len);

            for (int i = 0; i < count; i++)
            {
                sb.Append(text);
            }

            return sb.ToString();
        }
    }
}