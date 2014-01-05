using System.Collections.Generic;
using System.Linq;

namespace Isotope.CommandLine
{
    public static class Tokenizer
    {
        static Tokenizer()
        {
        }

        public static IList<Token> Tokenize(string input_textr, string punctutation)
        {
            var ctx = new TokenizationContent(punctutation);
            var state = TokenizationState.START;
            const char double_quote = '\"';

            foreach (char c in input_textr)
            {
                if (state == TokenizationState.START)
                {
                    if (ctx.is_whitespace_char(c))
                    {
                        state = TokenizationState.START;
                    }
                    else if (c == double_quote)
                    {
                        state = TokenizationState.QUOTING;
                    }
                    else if (ctx.is_punctuation(c))
                    {
                        state = TokenizationState.START;
                    }
                    else
                    {
                        state = TokenizationState.STORING;
                        ctx.store_character(c);
                    }
                }
                else if (state == TokenizationState.STORING)
                {
                    if (ctx.is_whitespace_char(c))
                    {
                        ctx.store_token();
                        state = TokenizationState.START;
                    }
                    else if (ctx.is_punctuation(c))
                    {
                        ctx.store_token();
                        state = TokenizationState.START;                        
                    }
                    else if (c==double_quote)
                    {
                        ctx.store_token();
                        state = TokenizationState.QUOTING;
                    }
                    else if (ctx.is_punctuation(c))
                    {
                        ctx.store_token();
                        state = TokenizationState.START;
                    }
                    else
                    {
                        ctx.store_character(c);
                        state = TokenizationState.STORING;
                    }
                }
                else if (state == TokenizationState.QUOTING)
                {
                    if (c == double_quote)
                    {
                        ctx.store_token();
                        state = TokenizationState.START;
                    }
                    else
                    {
                        ctx.store_character(c);
                        state = TokenizationState.QUOTING;
                    }                   
                }
            }

            if ((state == TokenizationState.STORING) || (state == TokenizationState.QUOTING))
            {
                ctx.store_token();
            }
            return ctx.m_token_queue.ToList();
        }
    }
}