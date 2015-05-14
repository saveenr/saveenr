
using System;
using SMA = System.Management.Automation;

namespace DemoPowerShellModule
{

    [SMA.Cmdlet("Get", "DemoObject1")]
    public class Get_DemoObject1: SMA.Cmdlet
    {
        protected override void ProcessRecord()
        {
            this.WriteObject(Common.items,true);
        }
    }

    [SMA.Cmdlet("Get", "DemoObject2")]
    public class Get_DemoObject2: SMA.Cmdlet
    {
        protected override void ProcessRecord()
        {
            this.WriteObject(Common.items,false);
        }
    }

    [SMA.Cmdlet("Get", "DemoObject3")]
    public class Get_DemoObject3: SMA.Cmdlet
    {
        protected override void ProcessRecord()
        {
            foreach (var i in Common.items)
            {
                this.WriteObject(i);
            }
        }
    }

    [SMA.Cmdlet("Get", "DemoObject4")]
    public class Get_DemoObject4: SMA.Cmdlet
    {
        protected override void ProcessRecord()
        {
            try
            {
                foreach (var i in Common.items)
                {
                    this.WriteObject(i);
                }
            }
            catch (Exception)
            {               
                this.WriteVerbose("Caught Exception");
            }
        }
    }


    public static class Common
    {
        public static string[] items = {"A", "B", "C"};
    }
}
