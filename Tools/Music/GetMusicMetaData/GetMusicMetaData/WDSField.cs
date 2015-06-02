namespace GetMusicMetaData
{
    public class WDSField
    {
        public string Name;
        public FieldType FieldType;

        public WDSField(string n, FieldType t)
        {
            this.Name = n;
            this.FieldType = t;
        }

        public object GetObject(System.Data.OleDb.OleDbDataReader reader, int i)
        {
            return reader.GetValue(i);
        }

        public void UpdateString(string v, out string value)
        {
            value = v;
        }

    }
}