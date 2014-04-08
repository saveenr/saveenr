using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastStringSplit
{
    public struct SplitResult
    {
        public readonly int Ordinal;
        public readonly System.Text.StringBuilder StringBuilder;

        public SplitResult(int ordinal, System.Text.StringBuilder sb)
        {
            this.Ordinal = ordinal;
            this.StringBuilder = sb;
        }

        public string GetString()
        {
            if (this.StringBuilder == null)
            {
                return null;
            }

            return this.StringBuilder.ToString();
        }
    }

    public class Splitter
    {
        public System.Text.StringBuilder sb;
        public readonly char delim;

        public Splitter(char d)
        {
            this.delim = d;
        }

        public string[] Split(string text)
        {
            return this.SplitInteractive(text).Select(i=>i.GetString()).ToArray();
        }

        public IEnumerable<SplitResult> SplitInteractive(string text)
        {
            if (text == null)
            {
                yield break;
            }

            if (text.Length == 0)
            {
                yield break;
            }

            int o = 0;
            int state = 0;

            foreach (char c in text)
            {
                if (state == 0)
                {
                    if (c == this.delim)
                    {
                        yield return this.GetEmpty(o);
                        o++;
                        state = 2;
                    }
                    else
                    {
                        this.StoreFirst(c);
                        // do not increment ordinal
                        state = 1;
                    }
                }
                else if (state == 1)
                {
                    if (c == this.delim)
                    {
                        yield return this.GetContent(o);
                        this.sb.Clear();
                        o++;
                        state = 2;
                    }
                    else
                    {
                        this.Store(c);
                        // do not increment ordinal
                        state = 1;
                    }                    
                }
                else if (state == 2)
                {
                    if (c == this.delim)
                    {
                        yield return this.GetEmpty(o);
                        o++;
                        state = 2;
                    }
                    else
                    {
                        this.StoreFirst(c);
                        // do not increment ordinal
                        state = 1;
                    }                    

                }
                else
                {
                    throw new Exception();
                }

                o++;
            }

            if (this.sb.Length > 0)
            {
                yield return this.GetContent(o);
                this.sb.Clear();
            }

        }

        private SplitResult GetEmpty(int ordinal)
        {
            var r = new SplitResult(ordinal, null);
            return r;
        }

        private SplitResult GetContent(int ordinal)
        {
            var r = new SplitResult(ordinal, this.sb);
            return r;
        }


        private void StoreFirst(char c)
        {
            if (this.sb == null)
            {
                this.sb = new StringBuilder();
            }
            else
            {
                this.sb.Clear();
            }

            this.Store(c);
        }

        private void Store(char c)
        {

            this.sb.Append(c);
        }

    }
}
