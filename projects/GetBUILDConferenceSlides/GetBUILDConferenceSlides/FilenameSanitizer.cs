using System.Linq;
using System.Text;

namespace GetBUILDConferenceSlides
{
    public class FilenameSanitizer
    {
        private System.Text.StringBuilder sb;
        private string replacement_char = "_";

        public FilenameSanitizer()
        {
            
        }

        private enum State
        {
            Start,
            Good,
            Bad
        }

        public string Sanitize(string input)
        {


            string s = input.Trim();

            if (this.sb == null)
            {
                this.sb = new StringBuilder(s.Length);
            }

            State state = State.Start;

            char[] badchars = System.IO.Path.GetInvalidFileNameChars();
            foreach (char c in s)
            {
                if (state == State.Start)
                {
                    if (char.IsWhiteSpace(c))
                    {
                        sb.Append(replacement_char);
                        state = State.Bad;
                    }
                    else if (badchars.Contains(c))
                    {
                        sb.Append(replacement_char);
                        state = State.Bad;
                    }
                    else
                    {
                        sb.Append(c);
                        state = State.Good;
                    }
                }
                else if (state == State.Good)
                {
                    if (char.IsWhiteSpace(c))
                    {
                        sb.Append(replacement_char);
                        state = State.Bad;
                    }
                    else if (badchars.Contains(c))
                    {
                        sb.Append(replacement_char);
                        state = State.Bad;
                    }
                    else
                    {
                        sb.Append(c);
                        state = State.Good;
                    }
                }
                else if (state == State.Bad)
                {
                    if (char.IsWhiteSpace(c))
                    {
                        // do nothing 
                        state = State.Bad;
                    }
                    else if (badchars.Contains(c))
                    {
                        // do nothing 
                        state = State.Bad;
                    }
                    else
                    {
                        sb.Append(c);
                        state = State.Good;
                    }
                }
                else
                {
                    throw new System.ArgumentException();
                }
            }

            string result = sb.ToString();

            this.sb.Clear();
            return result;

        }

    }
}