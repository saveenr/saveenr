namespace Isotope.Types
{
    public struct NullableOrString<T> where T : struct
    {
        private T _val_data;
        private string _val_string;
        private System.Type _val_type;

        public NullableOrString(T value)
        {
            this._val_data = value;
            this._val_string = null;
            this._val_type = typeof (T);
        }

        public NullableOrString(string value)
        {
            this._val_data = default(T);
            this._val_string = value;
            this._val_type = typeof (string);
        }

        public bool HasValue
        {
            get { return this._val_type == typeof (T); }
        }

        public bool HasString
        {
            get { return this._val_type == typeof (string); }
        }

        public bool IsEmpty
        {
            get { return this._val_type == null; }
        }

        /// <summary>
        /// Gets or sets the string value
        /// </summary>
        public string String
        {
            get
            {
                if (this._val_type != typeof (string))
                {
                    string msg = string.Format("does not contain string");
                    throw new System.FieldAccessException(msg);
                }
                return this._val_string;
            }
            set
            {
                this._val_string = value;
                this._val_data = default(T);
                this._val_type = typeof (string);
            }
        }

        /// <summary>
        /// Gets or sets the typed (non-string) value
        /// </summary>
        public T Value
        {
            get
            {
                if (this._val_type != typeof (T))
                {
                    string msg = string.Format("Cell does not contain a number");

                    throw new System.FieldAccessException(msg);
                }
                return this._val_data;
            }
            set
            {
                this._val_data = value;
                this._val_string = null;
                this._val_type = typeof (T);
            }
        }

        /// <summary>
        /// Removes any string or non-string value
        /// </summary>
        public void Clear()
        {
            this._val_string = null;
            this._val_data = default(T);
            this._val_type = null;
        }

        /// <summary>
        /// string representation of the contents
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.GetAsString();
        }

        private string GetAsString()
        {
            if (this._val_type == typeof (string))
            {
                return this.String;
            }
            else if (this._val_type == typeof (T))
            {
                return this.Value.ToString();
            }
            else
            {
                string msg = string.Format("does not contain text or a number");
                throw new System.FieldAccessException(msg);
            }
        }

        /// <summary>
        /// Retrieved the string value
        /// </summary>
        /// <param name="defval"></param>
        /// <returns></returns>
        public string GetStringOrDefault(string defval)
        {
            if (this.HasString)
            {
                return this.String;
            }
            return defval;
        }

        /// <summary>
        /// retrieved the non-string value
        /// </summary>
        /// <param name="defval"></param>
        /// <returns></returns>
        public T GetValueOrDefault(T defval)
        {
            if (this.HasValue)
            {
                return this.Value;
            }
            return defval;
        }
    }
}