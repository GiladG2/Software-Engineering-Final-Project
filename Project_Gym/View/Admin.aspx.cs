using ControllersProject.Controller;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ControllersProject.DAL;
namespace Project_Gym.View
{
    public partial class Admin : System.Web.UI.Page
    {
        DataTable dt;
        public string tablename = "tblUsers";
        public string date;
        Users_Controller controller = new Users_Controller();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                GetD();
            if (Session["fusername"] != null)
            {
                if (Session["accesskey"].ToString() == "0")
                    Response.Redirect("Log_Out.aspx");
            }
            else
                Response.Redirect("Landing_Page.aspx");

        }
        void GetD(string searchQuery = "", string role = "All")
        {
            string query = $"SELECT * FROM {tablename}";
            dt = controller.GetData(query);

            DataView dv = dt.DefaultView;
            string filter = "";

            if (!string.IsNullOrEmpty(searchQuery))
            {
                filter += $"fldUsername LIKE '%{searchQuery}%'";
            }

            if (role != "All")
            {
                if (!string.IsNullOrEmpty(filter))
                    filter += " AND ";

                string roleFilter = role == "Admin" ? "7" : "0";
                filter += $"fldAccess = {roleFilter}";
            }

            if (!string.IsNullOrEmpty(filter))
            {
                dv.RowFilter = filter;
            }

            GridView1.DataSource = dv;
            GridView1.DataBind();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow ford = GridView1.Rows[e.RowIndex];
            string username = ford.Cells[1].Text;
            bool check = controller.DeleteUser(username);
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

            // Ensure that the row exists and is not null
            if (changedRow != null)
            {
                // Get the UserId from the first column (assumed to be read-only)
                string strId = changedRow.Cells[0].Text;

                // Only use FindControl for the editable fields
                string strUsername = changedRow.Cells[1].Text; // Read-only column
                string strPass = ((TextBox)changedRow.FindControl("txtPassword")).Text; // Editable field
                string strFname = ((TextBox)changedRow.FindControl("txtFirstName")).Text; // Editable field
                string strPhone = ((TextBox)changedRow.FindControl("txtPhone")).Text; // Editable field
                string strGender = changedRow.Cells[5].Text; // Read-only field (gender is in BoundField)
                int gender = 0;

                // Determine gender value (if needed, adjust logic to get the gender correctly from the cell or control)
                if (strGender.Equals("male", StringComparison.OrdinalIgnoreCase) || strGender.Equals("True", StringComparison.OrdinalIgnoreCase))
                {
                    gender = 1; // Male
                }

                string strAccess = changedRow.Cells[6].Text; // Read-only field (access is in BoundField)
                string strBday = ((TextBox)changedRow.FindControl("BDay")).Text; // Editable field

                // Proceed with updating the data
                controller.UpdateDataForEdit(strUsername, strPass, strBday, strPhone, strFname, gender, controller.GetGoal(strUsername));

                // Reset the edit index and refresh data
                GridView1.EditIndex = -1;
                GetD(); // Refresh GridView data
            }
        }


        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sql = $"SELECT * FROM {tablename}";
            DataView dataView = new DataView(AdoHelper.GetDataTable("DB.mdf", sql));
            dataView.Sort = e.SortExpression;
            GridView1.DataSource = dataView;
            GridView1.DataBind();
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GetD();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchQuery = txtSearch.Text;
            string selectedRole = ddlRole.SelectedValue;
            GetD(searchQuery, selectedRole);
        }

        protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedRole = ddlRole.SelectedValue;
            string searchQuery = txtSearch.Text;
            GetD(searchQuery, selectedRole);
        }
    }
}