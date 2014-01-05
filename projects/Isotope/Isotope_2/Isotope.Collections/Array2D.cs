namespace Isotope.Collections
{
    public class Array2D<T>
    {
        protected T[,] array;
        protected int num_cols;
        protected int num_rows;

        public Array2D(int cols, int rows)
        {
            this.array = new T[rows, cols];
            this.num_cols = cols;
            this.num_rows = rows;
        }

        /// <summary>
        /// The number of rows
        /// </summary>
        public int RowCount
        {
            get
            {
                return this.num_rows;
            }
        }

        public int ColumnCount
        {
            get
            {
                return this.num_cols;
            }
        }

        /// <summary>
        /// Returns the (formula, result) pair at row, col
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public T this[int row, int col]
        {
            get { return this.array[row, col]; }
        }

        public T[,] RawArray
        {
            get { return this.array; }
        }
    }
}