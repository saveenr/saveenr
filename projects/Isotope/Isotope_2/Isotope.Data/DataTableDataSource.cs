using System.Collections.Generic;

namespace Isotope.Data
{
    public class DataTableDataSource : DataSource
    {
        private Schema schema;
        private System.Data.DataTable datatable;

        public DataTableDataSource(string name, System.Data.DataTable datatable) :
            base(name)
        {
            init(datatable);
            this.Name = name;
        }

        public DataTableDataSource(System.Data.DataTable dt) :
            base()
        {
            init(dt);
        }

        private void init(System.Data.DataTable dt)
        {
            this.schema = new Schema();
            foreach (System.Data.DataColumn col in dt.Columns)
            {
                schema.AddField(col.ColumnName, col.DataType, col);
            }
            this.datatable = dt;
            this.Name = dt.TableName;
        }

        protected override IEnumerable<DataSource.RowEnumerator> EnumRows()
        {
            int n = 0;
            foreach (System.Data.DataRow row in this.DataTable.Rows)
            {
                var row_item = new DataSource.RowEnumerator(n++, row.ItemArray);
                yield return row_item;
            }
        }

        public Schema Schema
        {
            get { return this.schema; }
        }

        public System.Data.DataTable DataTable
        {
            get { return datatable; }
        }

        public override Schema GetSchema()
        {
            return this.Schema;
        }
    }
}