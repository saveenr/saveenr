namespace GetMusicMetaData
{
    public class Query
    {
        public System.Collections.Generic.List<QueryColumn> Columns;

        public Query()
        {
            this.Columns = new System.Collections.Generic.List<QueryColumn>();

        }

        public QueryColumn Add( WDSField f)
        {
            var col = new QueryColumn(f, this.Columns.Count);
            this.Columns.Add( col );
            return col;
        }
    }
}