namespace Isotope.Math
{
    public struct Matrix4x4
    {
        public double M11, M12, M13, M14;
        public double M21, M22, M23, M24;
        public double M31, M32, M33, M34;
        public double M41, M42, M43, M44;

        public Matrix4x4(
            double m11,
            double m12,
            double m13,
            double m14,
            double m21,
            double m22,
            double m23,
            double m24,
            double m31,
            double m32,
            double m33,
            double m34,
            double m41,
            double m42,
            double m43,
            double m44
            )
        {
            M11 = m11;
            M12 = m12;
            M13 = m13;
            M14 = m14;
            M21 = m21;
            M22 = m22;
            M23 = m23;
            M24 = m24;
            M31 = m31;
            M32 = m32;
            M33 = m33;
            M34 = m34;
            M41 = m41;
            M42 = m42;
            M43 = m43;
            M44 = m44;
        }

        public Matrix4x4 Multiply(Matrix4x4 m)
        {
            var result = new Matrix4x4();

            result.M11 = this.M11*m.M11 + this.M12*m.M21 + this.M13*m.M31 + this.M14*m.M41;
            result.M12 = this.M11*m.M12 + this.M12*m.M22 + this.M13*m.M32 + this.M14*m.M42;
            result.M13 = this.M11*m.M13 + this.M12*m.M23 + this.M13*m.M33 + this.M14*m.M43;
            result.M14 = this.M11*m.M14 + this.M12*m.M24 + this.M13*m.M34 + this.M14*m.M44;

            result.M21 = this.M21*m.M11 + this.M22*m.M21 + this.M23*m.M31 + this.M24*m.M41;
            result.M22 = this.M21*m.M12 + this.M22*m.M22 + this.M23*m.M32 + this.M24*m.M42;
            result.M23 = this.M21*m.M13 + this.M22*m.M23 + this.M23*m.M33 + this.M24*m.M43;
            result.M24 = this.M21*m.M14 + this.M22*m.M24 + this.M23*m.M34 + this.M24*m.M44;

            result.M31 = this.M31*m.M11 + this.M32*m.M21 + this.M33*m.M31 + this.M34*m.M41;
            result.M32 = this.M31*m.M12 + this.M32*m.M22 + this.M33*m.M32 + this.M34*m.M42;
            result.M33 = this.M31*m.M13 + this.M32*m.M23 + this.M33*m.M33 + this.M34*m.M43;
            result.M34 = this.M31*m.M14 + this.M32*m.M24 + this.M33*m.M34 + this.M34*m.M44;

            result.M41 = this.M41*m.M11 + this.M42*m.M21 + this.M43*m.M31 + this.M44*m.M41;
            result.M42 = this.M41*m.M12 + this.M42*m.M22 + this.M43*m.M32 + this.M44*m.M42;
            result.M43 = this.M41*m.M13 + this.M42*m.M23 + this.M43*m.M33 + this.M44*m.M43;
            result.M44 = this.M41*m.M14 + this.M42*m.M24 + this.M43*m.M34 + this.M44*m.M44;

            return result;
        }

        public static Matrix4x4 operator *(Matrix4x4 a, Matrix4x4 b)
        {
            return a.Multiply(b);
        }

        public static Vector4 operator *(Matrix4x4 m, Vector4 v)
        {
            return new Vector4(
                (m.M11*v.M1) + (m.M12*v.M2) + (m.M13*v.M3) + (m.M14*v.M4),
                (m.M21*v.M1) + (m.M22*v.M2) + (m.M23*v.M3) + (m.M24*v.M4),
                (m.M31*v.M1) + (m.M32*v.M2) + (m.M33*v.M3) + (m.M34*v.M4),
                (m.M41*v.M1) + (m.M42*v.M2) + (m.M43*v.M3) + (m.M44*v.M4)
                );
        }
    }
}