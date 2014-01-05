using System;
using System.Collections.Generic;

namespace DemoIronPythonDynamicInterop
{
    public class CustomExpando : System.Dynamic.DynamicObject
    {
        protected Dictionary<string, object> _memberdic = new Dictionary<string, object>();

        public override bool TrySetMember(System.Dynamic.SetMemberBinder binder, object value)
        {
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

        public override bool TryGetMember(System.Dynamic.GetMemberBinder binder, out object result)
        {
            if (_memberdic.ContainsKey(binder.Name))
            {
                result = _memberdic[binder.Name];
                return true;
            }
            else
            {
                bool v = base.TryGetMember(binder, out result);
                return v;
            }
        }

        public override bool TryInvokeMember(System.Dynamic.InvokeMemberBinder binder, object[] args, out object result)
        {
            if (_memberdic.ContainsKey(binder.Name) && _memberdic[binder.Name] is Delegate)
            {
                result = (_memberdic[binder.Name] as Delegate).DynamicInvoke(args);
                return true;
            }
            else
            {
                bool v = base.TryInvokeMember(binder, args, out result);
                return v;
            }
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return _memberdic.Keys;
        }
    }
}