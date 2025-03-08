using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace Project_Jim
{
    public partial class Admin : System.Web.UI.Page
    {
        DataTable dt;
        public string tablename = "tblUsers";
        public string date;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                GetD();
            if (Session["fusername"] != null)
            {
                if (Session["accesskey"].ToString() == "0")
                    Response.Redirect("LogOut.aspx");
            }
            else
                Response.Redirect("Gallery1.aspx");

        }
        void GetD()
        {
            string file = "Database1.mdf";
            string str = $"select * from {tablename}";
            dt = AdoHelper.GetDataTable(file, str);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow ford = GridView1.Rows[e.RowIndex];
            string username = ford.Cells[0].Text;
            string str = $"DELETE  from {tablename} where fldUsername='{username}'";
            string fileName = "Database1.mdf";
            int check = AdoHelper.CheckInsert(fileName, str);
            GridView1.EditIndex = -1;
            GetD();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GetD();

        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GetD();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow changedRow = GridView1.Rows[e.RowIndex];
            string strID = changedRow.Cells[0].Text; // בלבד לקריאה בעמודה לנתון גישה
            string strPass = ((TextBox)changedRow.Cells[1].Controls[0]).Text;
            string strFname = ((TextBox)changedRow.Cells[2].Controls[0]).Text;
            string strPhone = ((TextBox)changedRow.Cells[3].Controls[0]).Text;
            string strGender = ((TextBox)changedRow.Cells[4].Controls[0]).Text;
            string strAccess = ((TextBox)changedRow.Cells[5].Controls[0]).Text;
            string strBday = ((TextBox)changedRow.FindControl("BDay")).Text;
            string sql =  $"UPDATE {tablename} SET fldPassowrd = '{strPass}', fldAccess = '{strAccess}', fldDate = '{strBday}', fldPhone = '{strPhone}', fldFirstbane = '{strFname}',fldGendetr = '{strGender}' WHERE fldUsername = '{strID}'";
            AdoHelper.CheckInsert("Database1.mdf", sql);
            GridView1.EditIndex = -1;
            GetD();
            ;
        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
           string sql = $"SELECT * FROM {tablename}";
            DataView dataView = new DataView(AdoHelper.GetDataTable("Database1.mdf", sql));
            dataView.Sort = e.SortExpression;
            GridView1.DataSource = dataView;
            GridView1.DataBind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GetD() ;
        }
    }
}