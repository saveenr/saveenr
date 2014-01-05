using System;
using Isotope.Reporting.RDL2005;

namespace Isotope.Reporting
{
    public static class ExtensionMethods
    {

        public static System.Xml.Linq.XElement RS_AddElement(this System.Xml.Linq.XElement el, string name)
        {
            var child = new System.Xml.Linq.XElement(RDLINFO.RS_Namespace.GetName(name));
            el.Add(child);
            return child;
        }

        public static System.Xml.Linq.XElement RS_SetElementValue(this System.Xml.Linq.XElement el, string name,string val)
        {
            var child = new System.Xml.Linq.XElement(RDLINFO.RS_Namespace.GetName(name));
            el.Add(child);
            child.Value = val;
            return child;
        }


        public static System.Xml.Linq.XElement RS_SetElementValueCOND(this System.Xml.Linq.XElement el, string name, string val)
        {
            if (val!=null)
            {
                var child = new System.Xml.Linq.XElement(RDLINFO.RS_Namespace.GetName(name));
                el.Add(child);
                child.Value = val;
                return child;
                
            }
            else
            {
                return null;
            }
        }




        public static System.Xml.Linq.XElement RS_SetElementValueCOND(this System.Xml.Linq.XElement el, string name, Isotope.Reporting.RDL2005.Color color)
        {

            if (color.HasValue)
            {
                var child = new System.Xml.Linq.XElement(RDLINFO.RS_Namespace.GetName(name));
                el.Add(child);
                child.Value = color.ToString();
                return child;

            }
            else
            {
                return null;
            }
        }

        public static System.Xml.Linq.XElement RS_SetElementValueCOND<T>(this System.Xml.Linq.XElement el, string name, T? val) where T: struct
        {
            return el.RS_SetElementValueCOND(name, val, "");
        }

        public static System.Xml.Linq.XElement RS_SetElementValueCOND<T>(this System.Xml.Linq.XElement el, string name, T? val, string unit) where T : struct
        {
            if (val.HasValue)
            {
                var child = new System.Xml.Linq.XElement(RDLINFO.RS_Namespace.GetName(name));
                el.Add(child);
                child.Value = val.Value.ToString() + unit;
                return child;

            }
            else
            {
                return null;
            }
        }


        public static System.Xml.Linq.XElement RS_SetElementValueCONDBOOL(this System.Xml.Linq.XElement el, string name, bool? val) 
        {
            if (val.HasValue)
            {
                var child = new System.Xml.Linq.XElement(RDLINFO.RS_Namespace.GetName(name));
                el.Add(child);
                child.Value = val.Value ? "true" : "false";
                return child;

            }
            else
            {
                return null;
            }
        }

        public static void RS_SetAttributeValueCONDBOOL(this System.Xml.Linq.XElement el, string name, bool? val)
        {
            if (val.HasValue)
            {
                el.SetAttributeValue(name, val.Value ? "true" : "false");

            }
        }

        public static System.Xml.Linq.XElement RS_SetElementValueCOND<T>(this System.Xml.Linq.XElement el, string name, T? val, Func<T,string> fs) where T : struct
        {
            if (val.HasValue)
            {
                var child = new System.Xml.Linq.XElement(RDLINFO.RS_Namespace.GetName(name));
                el.Add(child);
                child.Value = fs(val.Value);
                return child;

            }
            else
            {
                return null;
            }
        }

    }
}