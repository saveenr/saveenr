using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FilenameTimestamp
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    public class Stamp
    {
        public string Year = null;
        public string Month = null;
        public string Day = null;
        public bool year_is_an_int = false;
        public bool month_is_an_int = false;
        public bool day_is_an_int = false;
        public int in_stamp_year;
        public int in_stamp_month;
        public int in_stamp_day;
        
        public Stamp()
        {
        }

        public Stamp(string y, string m, string d)
        {
            this.Year = y;
            this.Month = m;
            this.Day = d;


        }

        public void calc()
        {
            year_is_an_int = int.TryParse(this.Year, out in_stamp_year);
            month_is_an_int = int.TryParse(this.Month, out in_stamp_month);
            day_is_an_int = int.TryParse(this.Day, out in_stamp_day);
        }




    }

    public class ParseResult
    {
        public string Remainder=null;
        public Stamp Stamp = null;

    }

    public class Stamper
    {
        public string [] tokenize(string s)
        {

            s = s.Replace("(", "");
            s = s.Replace("[", "");
            s = s.Replace(")", "");
            s = s.Replace("]", "");

            string[] tokens = s.Split("-".ToCharArray());
            if (tokens.Length != 3)
            {
                throw new Exception();
            }
            return tokens;
        }

        public string strip_trailing_chars(string s, char t)
        {
            string S=s;
            while ( (S.Length>0) && (S[S.Length - 1] == t) )
            {
                S = S.Substring(0, S.Length - 1);
            }
            return S;
        }
        public ParseResult parse(string input_filename)
        {
            ParseResult result = new ParseResult();
            if (input_filename == null)
            {
                throw new NullReferenceException();
            }

            if (input_filename.Length == 0)
            {
                result.Remainder = input_filename;
                result.Stamp = null;
                return result;

            }

            Stamp stamp_struct = new Stamp();
            string redigit = "([0-9]|[xX])";
            string redash = @"\-";
            string releft = @"(([\[|\(])*)";
            string reright = @"(([\]|\)])*)";

            string reyyyymmdd = (releft) + (redigit + "{4}") + redash + (redigit + "{1,2}") + redash + (redigit + "{1,2}") + (reright);
            string remmddyyyy = (releft) + (redigit + "{1,2}") + redash + (redigit + "{1,2}") + redash + (redigit + "{4}") + (reright);  

            System.Text.RegularExpressions.Regex regex_pattern;
            System.Text.RegularExpressions.Match regex_match;
            System.Text.RegularExpressions.Capture regex_capture = null;

            regex_pattern = new System.Text.RegularExpressions.Regex(reyyyymmdd);
            regex_match = regex_pattern.Match(input_filename);

            if (regex_match.Success)
            {
                regex_capture = regex_match.Captures[0];
                string[] tokens = this.tokenize(regex_capture.ToString());
                stamp_struct.Year=tokens[0];
                stamp_struct.Month = tokens[1];
                stamp_struct.Day=tokens[2];
                stamp_struct.calc();
                result.Stamp=stamp_struct;
            }
            else
            {
                regex_pattern = new System.Text.RegularExpressions.Regex(remmddyyyy);
                regex_match = regex_pattern.Match(input_filename);
                if (regex_match.Success)
                {
                    regex_capture = regex_match.Captures[0];
                    string[] tokens = this.tokenize(regex_capture.ToString());
                    stamp_struct.Year = tokens[2];
                    stamp_struct.Month = tokens[0];
                    stamp_struct.Day = tokens[1];
                    stamp_struct.calc();
                    result.Stamp = stamp_struct;
                }
                else
                {
                    result.Remainder = input_filename;
                    result.Stamp =null;
                    return result;
                }

            }

            if (regex_match == null)
            {
                throw new Exception();
            }

            string before = "";
            string after = "";

            if (regex_capture.Length > 0)
            {
                if (regex_capture.Index < 1)
                {
                    before = "";
                    after = input_filename.Substring(regex_capture.Index + regex_capture.Length);
                }
                else
                {
                    before = input_filename.Substring(0, regex_capture.Index);
                    after = input_filename.Substring(regex_capture.Index + regex_capture.Length);
                }

            }

            result.Remainder = before + after;

            return result;

        }


        public void GetRenameInfo(string in_name, System.DateTime in_disktime, out string out_name, out System.DateTime out_disktime)
        {
            var res = this.parse(in_name);
            if (res == null)
            {
                throw new Exception();
            }

            string name_base = System.IO.Path.GetFileNameWithoutExtension(res.Remainder);
            name_base = strip_trailing_chars(name_base, '-');

            string name_ext = System.IO.Path.GetExtension(res.Remainder);
            
            string name_stamp = null;

            if (res.Stamp != null)
            {
                // there is a stamp in the filename
                
                bool file_stamp_is_valid_date = res.Stamp.year_is_an_int && res.Stamp.month_is_an_int && res.Stamp.day_is_an_int ;

                if (file_stamp_is_valid_date)
                {
                    name_stamp = string.Format("-({0:0000}-{1:00}-{2:00})", res.Stamp.in_stamp_year, res.Stamp.in_stamp_month, res.Stamp.in_stamp_day);
                    out_disktime = new DateTime(res.Stamp.in_stamp_year, res.Stamp.in_stamp_month, res.Stamp.in_stamp_day);
                    out_name = name_base + name_stamp + name_ext;
                }
                else
                {
                    name_stamp = string.Format("-({0}-{1}-{2})", res.Stamp.Year, res.Stamp.Month, res.Stamp.Day );
                    out_disktime = in_disktime ;
                    out_name = name_base + name_stamp + name_ext;
                }

            }
            else
            {
                // there is no a stamp in the filename, use the lastmod

                name_stamp = string.Format("-({0:0000}-{1:00}-{2:00})", in_disktime.Year, in_disktime.Month, in_disktime.Day );
                out_disktime = in_disktime ;
                out_name = name_base + name_stamp + name_ext;

            }

         

        }

        public RenameResult GetRenameInfo2(string in_name, System.DateTime in_disktime)
        {
            string out_name;
            System.DateTime out_disktime;

            var res = this.parse(in_name);
            if (res == null)
            {
                throw new Exception();
            }

            string name_base = System.IO.Path.GetFileNameWithoutExtension(res.Remainder);
            name_base = strip_trailing_chars(name_base, '-');

            string name_ext = System.IO.Path.GetExtension(res.Remainder);

            string name_stamp = null;

            if (res.Stamp != null)
            {
                // there is a stamp in the filename

                bool file_stamp_is_valid_date = res.Stamp.year_is_an_int && res.Stamp.month_is_an_int && res.Stamp.day_is_an_int;

                if (file_stamp_is_valid_date)
                {
                    name_stamp = string.Format("-({0:0000}-{1:00}-{2:00})", res.Stamp.in_stamp_year, res.Stamp.in_stamp_month, res.Stamp.in_stamp_day);
                    out_disktime = new DateTime(res.Stamp.in_stamp_year, res.Stamp.in_stamp_month, res.Stamp.in_stamp_day);
                    out_name = name_base + name_stamp + name_ext;
                }
                else
                {
                    name_stamp = string.Format("-({0}-{1}-{2})", res.Stamp.Year, res.Stamp.Month, res.Stamp.Day);
                    out_disktime = in_disktime;
                    out_name = name_base + name_stamp + name_ext;
                }

            }
            else
            {
                // there is no a stamp in the filename, use the lastmod

                name_stamp = string.Format("-({0:0000}-{1:00}-{2:00})", in_disktime.Year, in_disktime.Month, in_disktime.Day);
                out_disktime = in_disktime;
                out_name = name_base + name_stamp + name_ext;

            }

            var rr = new RenameResult();
            rr.OldName = in_name;
            rr.NewName = out_name;
            rr.NewDiskTime = out_disktime;

            return rr;
        }

    }

    public class RenameResult
    {
        public string OldName;
        public string NewName;
        public System.DateTime NewDiskTime;
    }


}


/*


        self.assertEqual( get_new_name_and_time( "", (2001,01,02)) , ("[2001-01-02]", (2001,01,02)) )
        self.assertEqual( get_new_name_and_time( "foo", (2002,02,03) ) , ("foo[2002-02-03]", (2002,02,03)) )

        # different years
        self.assertEqual( get_new_name_and_time( "foo2004-03-02", (2001,01,02) ) , ("foo[2001-01-02]", (2001,1,2) ) )
        self.assertEqual( get_new_name_and_time( "foo2004-03-02", (2005,06,07) ) , ("foo[2004-03-02]", (2004,3,2) ) )
        self.assertEqual( get_new_name_and_time( "foo2004-03-02.jpg", (2001,01,02) ) , ("foo[2001-01-02].jpg", (2001,1,2) ) )
        self.assertEqual( get_new_name_and_time( "foo2004-03-02.[].txt", (2005,06,07) ) , ("foo[2004-03-02].txt", (2004,3,2) ) )
        self.assertEqual( get_new_name_and_time( "foo(....)2004-03-02.[].txt", (2005,06,07) ) , ("foo[2004-03-02].txt", (2004,3,2) ) )

        #equal years & months
        self.assertEqual( get_new_name_and_time( "foo2004-03-02[][][]", (2004,03,04) ) , ("foo[2004-03-02]", (2004,3,2) ) )
        self.assertEqual( get_new_name_and_time( "foo2004-03-02.gif.txt", (2004,03,01) ) , ("foo.gif[2004-03-01].txt", (2004,3,1) ) )

        #equal months
        self.assertEqual( get_new_name_and_time( "foo(2004-03-02]", (2004,02,02) ) , ("foo[2004-02-02]", (2004,2,2) ) )
        self.assertEqual( get_new_name_and_time( "foo2004-03-02", (2004,04,02) ) , ("foo[2004-03-02]", (2004,3,2) ) )
        self.assertEqual( get_new_name_and_time( "foo[2004-03-02)", (2004,04,02) ) , ("foo[2004-03-02]", (2004,3,2) ) )
        self.assertEqual( get_new_name_and_time( "foo (1)", (2004,04,02) ) , ("foo (1)[2004-04-02]", (2004,4,2) ) )
        self.assertEqual( get_new_name_and_time( "foo (1) [2005-06-07]", (2004,04,02) ) , ("foo (1) [2004-04-02]", (2004,4,2) ) )

        self.assertEqual( get_new_name_and_time( "foo()", (2004,03,02) ) , ("foo[2004-03-02]", (2004,03,02)) )
        #self.assertEqual( get_new_name_and_time( "xAxx-xx-xx", d_a ) , None )

        """
        Additional cases
        tet cases for negative numbers
        change [ ] to ( )
        raise exception if input contains an os separator or if it contains invalid filename characters
        add case where there is no effective change
        make sure only a single space separates the stamp from the name
        what happens if there are two dates in the filename
        what happens if the dates are not possible (not really in calendar)
        what happens if days/months exceed ranges
        what is min max range for year
        DONE handle single digit numbers in timestamps
        """

    def test_1(self) :
        self.x( "foo", "bar" )
    
if __name__ == '__main__':
    unittest.main()

*/
