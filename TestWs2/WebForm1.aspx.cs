using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestWs2
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            localhost.Practice service = new localhost.Practice();
            DataSet ds = service.GetDataSet("Exercise_List");
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
        }
    }
}