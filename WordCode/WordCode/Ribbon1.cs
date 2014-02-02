using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Tools.Ribbon;

/*
 Sample text
 * 
“Helo World”
it’s
‘he said’
 * */

namespace WordCode
{
    public partial class Ribbon1
    {
        private object myReplace = WdReplace.wdReplaceAll;
        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void run_replace(Find find, string src, string dest)
        {
            object objMissing = Type.Missing;
            find.Text = src;
            find.Replacement.Text = dest;
            find.Execute(ref objMissing, ref objMissing, ref objMissing, ref objMissing, ref objMissing,
                ref objMissing, ref objMissing, ref objMissing, ref objMissing, ref objMissing, ref myReplace,
                ref objMissing, ref objMissing, ref objMissing, ref objMissing);
        }

        private void button1_Click_1(object sender, RibbonControlEventArgs e)
        {
            remove_from_selection();
        }

        private void remove_from_selection()
        {
            var app = Globals.ThisAddIn.Application;


            var old_replace_quotes = app.Options.AutoFormatAsYouTypeReplaceQuotes;

            app.Options.AutoFormatAsYouTypeReplaceQuotes = false;
            try
            {
                var sel = app.Selection;
                var find = sel.Find;
                find.ClearFormatting();

                var rep = find.Replacement;
                rep.ClearFormatting();

                find.Forward = true;
                find.Wrap = WdFindWrap.wdFindContinue;
                find.Format = false;
                find.MatchCase = false;
                find.MatchWildcards = false;
                find.MatchSoundsLike = false;
                find.MatchAllWordForms = false;

                run_replace(find, "”", "\"");
                run_replace(find, "“", "\"");
                run_replace(find, "‘", "'");
                run_replace(find, "’", "'");
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                app.Options.AutoFormatAsYouTypeReplaceQuotes = old_replace_quotes;
            }
        }

        private void buttonRemoveFromDocument_Click(object sender, RibbonControlEventArgs e)
        {
            var app = Globals.ThisAddIn.Application;

            app.Selection.WholeStory();

            remove_from_selection();

        }
    }
}
