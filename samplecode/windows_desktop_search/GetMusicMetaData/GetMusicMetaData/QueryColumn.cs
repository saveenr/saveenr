namespace GetMusicMetaData
{
    public class QueryColumn
    {
        public WDSField Field;
        public int Ordinal;

        public QueryColumn(WDSField f, int ord)
        {
            this.Field = f;
            this.Ordinal = ord;
        }
    }
}