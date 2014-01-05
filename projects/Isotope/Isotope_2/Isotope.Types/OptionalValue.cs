namespace Isotope.Types
{
    public struct OptionalValue<T>
    {
        private T _value;
        private bool _hasvalue;

        public bool Hasvalue
        {
            get { return this._hasvalue; }
        }

        public OptionalValue(T value)
        {
            this._value = value;
            this._hasvalue = true;
        }

        public T Value
        {
            get
            {
                if (this.Hasvalue)
                {
                    return this._value;
                }
                else
                {
                    string msg = string.Format("No value stored");
                    throw new System.ArgumentException(msg);
                }
            }
            set
            {
                this._value = value;
                this._hasvalue = true;
            }
        }


        public T GetValue(T default_val)
        {
            if (this.Hasvalue)
            {
                return this._value;
            }
            return default_val;
        }


        public void Clear()
        {
            if (this._hasvalue)
            {
                this._hasvalue = false;
            }
        }

        public void UpdateFrom(OptionalValue<T> other)
        {
            if (other._hasvalue)
            {
                this.Value = other.Value;
            }
        }

        public static implicit operator T(OptionalValue<T> ov)
        {
            return ov.Value;
        }

        public static implicit operator OptionalValue<T>(T value)
        {
            return new OptionalValue<T>(value);
        }

        public override string ToString()
        {
            return this.Value.ToString();
        }

        public static readonly OptionalValue<T> Empty = new OptionalValue<T>();
    }
}