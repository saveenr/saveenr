using System;
using System.Linq;
using MSACCESS = Microsoft.Office.Interop.Access;

namespace ExportAccessAttachments3
{
    class Program
    {
        static void Main(string[] args)
        {
            const string fieldname_filename = "FileName";
            const string fieldname_filedata = "FileData";


            string outputfolder = @"D:\attachments";
            string dbfilename = @"D:\\AX6Reports.accdb";
            string tablename = "AX6Reports";
            var prefix_fieldnames = new[] { "Name", "Design" };
            string attachment_fieldname = "Attachments";

            var dbe = new MSACCESS.Dao.DBEngine();
            var db = dbe.OpenDatabase(dbfilename, false, false, "");
            var rstype = MSACCESS.Dao.RecordsetTypeEnum.dbOpenDynaset;
            var locktype = MSACCESS.Dao.LockTypeEnum.dbOptimistic;
            string selectclause = string.Format("SELECT * FROM {0}", tablename);
            var rs = db.OpenRecordset(selectclause, rstype, 0, locktype);
            rs.MoveFirst();
            int row_count = 0;

            while (!rs.EOF)
            {
                var prefix_values = prefix_fieldnames.Select(s => rs.Fields[s].Value).ToArray();
                var attachment_rs = (MSACCESS.Dao.Recordset2)rs.Fields[attachment_fieldname].Value;
                int attachment_count = 0;
                while (!attachment_rs.EOF)
                {

                    var field_filename = attachment_rs.Fields[fieldname_filename].Value;

                    var field_attachment = (MSACCESS.Dao.Field2)attachment_rs.Fields[fieldname_filedata];
                    if (field_attachment != null)
                    {
                        if (field_attachment.Value != null)
                        {

                            string prefix = "";
                            if (prefix_fieldnames.Length > 0)
                            {
                                prefix = string.Format("{0}__", string.Join("__", prefix_values));
                                prefix = prefix.Replace(" ", "_");
                                prefix = prefix.Replace(":", "_");
                                prefix = prefix.Replace("/", "_");
                            }

                            var dest_fname = System.IO.Path.Combine(outputfolder, prefix + field_filename);

                            if (System.IO.File.Exists(dest_fname))
                            {
                                System.IO.File.Delete(dest_fname);
                            }

                            field_attachment.SaveToFile(dest_fname);
                        }
                    }

                    attachment_rs.MoveNext();
                    attachment_count++;
                }
                attachment_rs.Close();
                Console.WriteLine(row_count);
                row_count++;
                rs.MoveNext();
            }

            rs.Close();
        }
    }
}
