namespace Isotope.Reporting.RDL2005
{
    public class DataRegion : Node
    {

        public string DatasetName;
        public NodeCollection<Filter> Filters;
        //string.pagebreakatstart
        //pagebreakat edn
        //keeptogether
        //norows
        public DataRegion()
        {
            this.Filters = new NodeCollection<Filter>();
            
        }
    }
}