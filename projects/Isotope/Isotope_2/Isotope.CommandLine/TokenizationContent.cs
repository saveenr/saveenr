using System.Collections.Generic;
using System.Linq;

namespace Isotope.CommandLine
{
    internal class TokenizationContent
    {
        private System.Text.StringBuilder m_current_token_text;

        public readonly List<Token> m_token_queue;

        private readonly string _punctuation;

        public TokenizationContent(string punctuation)
        {
            this.m_current_token_text = new System.Text.StringBuilder();
            this.m_token_queue = new List<Token>();
            this._punctuation = punctuation;
        }

        public string Punctuation
        {
            get { return _punctuation; }
        }

        public void store_character(char c)
        {
            m_current_token_text.Append(c);
        }


        public void store_token()
        {
            var token_text = this.m_current_token_text.ToString();
            var t = new Token(token_text);
            this.m_token_queue.Add(t);
            m_current_token_text = new System.Text.StringBuilder();
        }

        public IList<Token> GetTokens()
        {
            return this.m_token_queue.ToList();
        }

        public bool is_eof_char(int c)
        {
            return (c < 0);
        }

        public bool is_whitespace_char(char c)
        {
            return System.Char.IsWhiteSpace(c);
        }

        public bool is_punctuation(char c)
        {
            return (this._punctuation.IndexOf(c) >= 0);
        }
    }
}