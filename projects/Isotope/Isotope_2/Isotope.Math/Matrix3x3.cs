namespace Isotope.Math
{
    public struct Matrix3x3
    {
        public double M11, M12, M13;
        public double M21, M22, M23;
        public double M31, M32, M33;

        public Matrix3x3(
            double m11,
            double m12,
            double m13,
            double m21,
            double m22,
            double m23,
            double m31,
            double m32,
            double m33)
        {
            M11 = m11;
            M12 = m12;
            M13 = m13;
            M21 = m21;
            M22 = m22;
            M23 = m23;
            M31 = m31;
            M32 = m32;
            M33 = m33;
        }

        public Matrix3x3 Multiply(Matrix3x3 m)
        {
            var result = new Matrix3x3();

            result.M11 = this.M11*m.M11 + this.M12*m.M21 + this.M13*m.M31;
            result.M12 = this.M11*m.M12 + this.M12*m.M22 + this.M13*m.M32;
            result.M13 = this.M11*m.M13 + this.M12*m.M23 + this.M13*m.M33;

            result.M21 = this.M21*m.M11 + this.M22*m.M21 + this.M23*m.M31;
            result.M22 = this.M21*m.M12 + this.M22*m.M22 + this.M23*m.M32;
            result.M23 = this.M21*m.M13 + this.M22*m.M23 + this.M23*m.M33;

            result.M31 = this.M31*m.M11 + this.M32*m.M21 + this.M33*m.M31;
            result.M32 = this.M31*m.M12 + this.M32*m.M22 + this.M33*m.M32;
            result.M33 = this.M31*m.M13 + this.M32*m.M23 + this.M33*m.M33;

            return result;
        }

        public static Matrix3x3 operator *(Matrix3x3 a, Matrix3x3 b)
        {
            return a.Multiply(b);
        }

        public static Vector3 operator *(Matrix3x3 m, Vector3 v)
        {
            return new Vector3(
                (m.M11*v.M1) + (m.M12*v.M2) + (m.M13*v.M3),
                (m.M21*v.M1) + (m.M22*v.M2) + (m.M23*v.M3),
                (m.M31*v.M1) + (m.M32*v.M2) + (m.M33*v.M3)
                );
        }
    }
}