using System;

namespace GetBUILDConferenceSlides
{
    public static class ExtensionMethods
    {
        public static bool ContainsCaseInsesitive(this string s, string t)
        {
            bool contains = s.IndexOf(t, StringComparison.OrdinalIgnoreCase) >= 0;
            return contains;
        }

        public static bool EndsWithCaseInsensitive(this string s, string t)
        {
            bool result = s.EndsWith(t, StringComparison.OrdinalIgnoreCase);
            return result;
        }

        public static string GetBase(this System.Uri u)
        {
            string baseUrl = u.Scheme + "://" + u.Authority;
            return baseUrl;
        }

        public static FilenameSanitizer san = new FilenameSanitizer();

        public static string SanitizeForFileSystem(this string s)
        {
            return san.Sanitize(s);
        }
    }
}