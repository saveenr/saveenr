using System;

namespace Viziblr.Reporting.RDL2005
{
    public struct Color
    {
        public enum ColorType
        {

            None,
            RGB, 
            ColorName,
            Expression
        }
        private ColorType _type;

        public ColorType Type
        {
            get
            {
                return this._type;
            }
        }

        private int _rgb;
        private string _colorname;
        private string _expression;

        public bool HasValue
        {
            get
            {
                return this.Type != ColorType.None;
            }
        }

        public int RGB
        {
            get
            {
                if (this.Type!=ColorType.RGB)
                {
                    throw new System.ArgumentOutOfRangeException();
                }
                return this._rgb;
            }
            set
            {
                this.Clear();
                this._rgb = value;
                this._type= ColorType.RGB;
            }
        }

        public string ColorName
        {
            get
            {
                if (this.Type != ColorType.ColorName)
                {
                    throw new System.ArgumentOutOfRangeException();
                }
                return this._colorname;
            }
            set
            {
                this.Clear();
                this._colorname= value;
                this._type = ColorType.ColorName;
            }
        }

        public string Expression
        {
            get
            {
                if (this.Type != ColorType.Expression)
                {
                    throw new System.ArgumentOutOfRangeException();
                }
                return this._expression;
            }
            set
            {
                this.Clear();
                this._expression= value;
                this._type = ColorType.Expression;
            }
        }


        public void Clear()
        {
            this._type = ColorType.None;
            this._rgb= 0;
            this._colorname= null;
            this._expression= null;

        }


        public override string ToString()
        {
            if (this.Type==ColorType.ColorName)
            {
                return this.ColorName;
            }
            else if (this.Type == ColorType.Expression)
            {
                return this.Expression;
            }
            else if (this.Type == ColorType.RGB)
            {
                int r = this._rgb & 0x00ff0000 >> 16;
                int g = this._rgb & 0x0000ff00 >> 8;
                int b = this._rgb & 0x000000ff >> 8;

                string s = String.Format("#{0:x2}{1:x2}{2:x2}", r, g, b);
                return s;
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }

        }
    }
}