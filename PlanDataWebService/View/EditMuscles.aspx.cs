using System;
using System.Data;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using ControllersProject.Controller;
using PlanDataWebService.Controller; // Import the controller namespace
using PlanDataWebService.DBServer;

namespace PlanDataWebService.View
{
    public partial class EditMuscles : System.Web.UI.Page
    {
        static string serverDB = "ServerDB.mdf"; // Adjust path as needed
        private Muscles_Controller musclesController = new Muscles_Controller(serverDB);
        Analytics_Controller ac = new Analytics_Controller();
        int userId;
        private DBOperations dBOperations = new DBOperations();
        protected void Page_Load(object sender, EventArgs e) { 
            if (Session["username"] == null)
                Response.Redirect("Home_Page.aspx");
            string username = Session["username"].ToString();
                // Fetch user ID using Controller_Users
                Controller_Users cu = new Controller_Users();
                userId = cu.GetUserIdByUsername(username);
            if (!IsPostBack)
            {
                
                LoadGridViewMuscles();
            }
        }

        private void LoadGridViewMuscles()
        {
            DataSet ds = musclesController.GetMuscles();
            if (ds != null && ds.Tables.Count > 0)
            {
                GridViewMuscles.DataSource = ds.Tables[0];
                GridViewMuscles.DataBind();
            }
        }

        protected void BtnSearchMuscles_Click(object sender, EventArgs e)
        {
            string searchQuery = TxtSearchMuscles.Text.Trim();
            DataSet ds = musclesController.GetMuscles();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                DataTable filteredTable = ds.Tables[0].Clone();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    if (row["fldMuscle_Name"].ToString().ToLower().Contains(searchQuery.ToLower()))
                    {
                        filteredTable.ImportRow(row);
                    }
                }
                GridViewMuscles.DataSource = filteredTable;
            }
            else
            {
                GridViewMuscles.DataSource = ds.Tables[0];
            }
            GridViewMuscles.DataBind();
        }

        protected void GridViewMuscles_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewMuscles.EditIndex = e.NewEditIndex;
            LoadGridViewMuscles();
        }

        protected async void GridViewMuscles_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridViewMuscles.Rows[e.RowIndex];
            int muscleId = Convert.ToInt32(GridViewMuscles.DataKeys[e.RowIndex].Value);
            string muscleName = ((TextBox)row.Cells[1].Controls[0]).Text;

            // Call the controller to update the muscle
            musclesController.EditMuscle(muscleId, muscleName);
            dBOperations.EditMuscle(muscleId, muscleName);
            ac.AddUpdate(userId);
            GridViewMuscles.EditIndex = -1;
            LoadGridViewMuscles();
        }

        protected void GridViewMuscles_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int muscleId = Convert.ToInt32(GridViewMuscles.DataKeys[e.RowIndex].Value);

            // Call the controller to delete the muscle
            musclesController.DeleteMuscle(muscleId);
            ac.AddDelete(userId);
            LoadGridViewMuscles();
        }

        protected void GridViewMuscles_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewMuscles.EditIndex = -1;
            LoadGridViewMuscles();
        }

        protected void GridViewMuscles_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewMuscles.PageIndex = e.NewPageIndex;
            LoadGridViewMuscles();
        }
    }
}
