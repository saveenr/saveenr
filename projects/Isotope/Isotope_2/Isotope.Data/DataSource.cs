using System.Collections.Generic;
using System.Linq;

namespace Isotope.Data
{
    public abstract class DataSource
    {
        public string Name { get; set; }

        public DataSource()
        {
        }

        public DataSource(string name)
        {
            this.Name = name;
        }

        public IEnumerable<RowEnumerator> Rows
        {
            get { return this.EnumRows(); }
        }

        protected abstract IEnumerable<RowEnumerator> EnumRows();

        public abstract Schema GetSchema();

        public static DataTableDataSource FromDataTable(System.Data.DataTable datatable)
        {
            return new DataTableDataSource(datatable);
        }

        public struct RowEnumerator
        {
            public readonly int Index;
            public readonly object[] ItemArray;

            internal RowEnumerator(int index, object [] itemarray)
            {
                this.Index = index;
                this.ItemArray = itemarray;
            }
        }
    }
}