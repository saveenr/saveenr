namespace Isotope.GraphViz
{
    public struct ColorRGB32Bit
    {
        private readonly byte _a;
        private readonly byte _r;
        private readonly byte _g;
        private readonly byte _b;

        public ColorRGB32Bit(byte r, byte g, byte b)
        {
            _a = 0xff;
            _r = r;
            _g = g;
            _b = b;
        }

        public ColorRGB32Bit(byte a, byte r, byte g, byte b)
        {
            _a = a;
            _r = r;
            _g = g;
            _b = b;
        }

        public ColorRGB32Bit(byte a, ColorRGB32Bit c)
        {
            _a = a;
            _r = c.R;
            _g = c.G;
            _b = c.B;
        }

        public ColorRGB32Bit(short a, short r, short g, short b)
        {
            _a = (byte)a;
            _r = (byte)r;
            _g = (byte)g;
            _b = (byte)b;
        }

        public ColorRGB32Bit(short r, short g, short b)
        {
            _a = (byte)0xff;
            _r = (byte)r;
            _g = (byte)g;
            _b = (byte)b;
        }

        public ColorRGB32Bit(int rgb)
        {
            ColorUtil.GetARGBBytes((uint)rgb, out _a, out _r, out _g, out _b);
        }

        public ColorRGB32Bit(uint rgb)
        {
            ColorUtil.GetARGBBytes(rgb, out _a, out _r, out _g, out _b);
        }

        public byte A
        {
            get { return _a; }
        }

        public byte R
        {
            get { return _r; }
        }

        public byte G
        {
            get { return _g; }
        }

        public byte B
        {
            get { return _b; }
        }

        public override string ToString()
        {
            var s = string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}({1},{2},{3},{4})",
                                  typeof(ColorRGB32Bit).Name, this._a, this._r, this._g, this._b);
            return s;
        }

        public static explicit operator int(ColorRGB32Bit color)
        {
            return color.ToInt();
        }

        public static explicit operator ColorRGB32Bit(int rgbint)
        {
            return new ColorRGB32Bit(rgbint);
        }

        public string ToWebColorString()
        {
            return ColorUtil.ToWebColorString(this._r, this._g, this._b);
        }

        public string ToWebColorStringA()
        {
            return ColorUtil.ToWebColorString(this._a, this._r, this._g, this._b);
        }
        public override bool Equals(object other)
        {
            return other is ColorRGB32Bit && Equals((ColorRGB32Bit)other);
        }

        public static bool operator ==(ColorRGB32Bit lhs, ColorRGB32Bit rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(ColorRGB32Bit lhs, ColorRGB32Bit rhs)
        {
            return !lhs.Equals(rhs);
        }

        private bool Equals(ColorRGB32Bit other)
        {
            return (this._a == other._a && this._r == other._r && this._g == other._g && this._b == other._b);
        }

        public override int GetHashCode()
        {
            return ToInt();
        }

        public int ToInt()
        {
            return (int)this.ToUInt();
        }

        public uint ToUInt()
        {
            return (uint)((this._a << 24) | (this._r << 16) | (this._g << 8) | (this._b));
        }
    }
}