namespace Isotope.Graph
{
    public class Node<TID, TDATA>
    {
        private TDATA _data;
        private TID ID;

        public TDATA Data
        {
            get { return _data; }
            set { _data = value; }
        }

        public TID Id
        {
            get { return ID; }
            set { ID = value; }
        }
    }
}