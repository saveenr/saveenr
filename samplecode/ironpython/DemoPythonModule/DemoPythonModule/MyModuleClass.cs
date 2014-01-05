using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//http://osdir.com/ml/ironpy/2009-11/msg00019.html

/*

import sys
import clr
sys.path.append(r"D:\DemoPythonModule\DemoPythonModule\bin\Debug")
clr.AddReferenceToFile("DemoPythonModule.dll")
import mymodule

 */

[assembly: IronPython.Runtime.PythonModule("mymodule", typeof(DemoPythonModule.MyModuleClass))]

namespace DemoPythonModule
{

    public static class MyModuleClass
    {
        [System.Runtime.CompilerServices.SpecialName] 
        public static void PerformModuleReload(
            IronPython.Runtime.PythonContext context, 
            IronPython.Runtime.PythonDictionary dict)
        {
            context.DomainManager.LoadAssembly(typeof(System.Xml.Linq.XDocument).Assembly);

            dict["FOO"] = "bar";
            System.Console.WriteLine("Module Load");
        }

        private static readonly object _stateKey = new object();

        public static object get_state(IronPython.Runtime.CodeContext context, object value)
        {
            object prev_value = null;

            if (context.LanguageContext.HasModuleState(_stateKey))
            {
                prev_value = context.LanguageContext.GetModuleState(_stateKey);
            }

            context.LanguageContext.SetModuleState(_stateKey, value);

            return prev_value;
        }
 
    }
}
