namespace DemoIronPythonDynamicInterop
{
    public class MyDemoClass
    {
        private MyNonDynamic _mynondynamic = new MyNonDynamic();
        private System.Dynamic.ExpandoObject _expando1 = new System.Dynamic.ExpandoObject();
        private CustomExpando _expando2 = new CustomExpando();
        private CustomReadOnlyExpando _expando3 = new CustomReadOnlyExpando();


        public MyDemoClass()
        {
            Expando1.Foo = "Expando1";
            Expando1.Inc = new System.Func<int, int>(value => increment_integer(value, 10));

            Expando2.Foo = "Expando2";
            Expando2.Inc = new System.Func<int, int>(value => increment_integer(value, 20));

            Expando3.Foo = "Expando3";
            Expando3.Inc = new System.Func<int, int>(value => increment_integer(value, 30));
            _expando3.readonly_mode = true;
        }

        private static int increment_integer(int n, int d)
        {
            return n + d;
        }

        public MyNonDynamic MyNonDynamic1
        {
            get
            {
                return this._mynondynamic;
            }
        }

        public dynamic Expando1
        {
            get
            {
                return this._expando1;
            }
        }

        public dynamic Expando2
        {
            get
            {
                return this._expando2;
            }
        }

        public dynamic Expando3
        {
            get
            {
                return this._expando3;
            }
        }
    }
}