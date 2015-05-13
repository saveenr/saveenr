using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoMSChartingForWeb
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            var fmt1 = new Viziblr.WebCharting.Templates.BarChart();
            var data1 = Viziblr.WebCharting.DemoData.WidgetsByMonth();
            fmt1.Apply(this.Chart1,data1);

        }
    }
}