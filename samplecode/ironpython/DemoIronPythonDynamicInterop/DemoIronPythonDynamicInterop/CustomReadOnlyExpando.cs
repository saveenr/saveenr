using System;

namespace DemoIronPythonDynamicInterop
{
    public class CustomReadOnlyExpando : CustomExpando
    {
        internal bool readonly_mode = false;

        public override bool TrySetMember
            (System.Dynamic.SetMemberBinder binder, object value)
        {
            if (this.readonly_mode)
            {
                throw new Exception("Cannot set this value at runtime");
            }

            if (!_memberdic.ContainsKey(binder.Name))
            {
                _memberdic.Add(binder.Name, value);
            }
            else
            {
                _memberdic[binder.Name] = value;
            }

            return true;
        }
    }
}