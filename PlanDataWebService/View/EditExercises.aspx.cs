using ControllersProject.Controller;
using PlanDataWebService.Controller;
using PlanDataWebService.DBServer;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace PlanDataWebService.View
{
    public partial class EditExercises : System.Web.UI.Page
    {
        static string serverDB = "ServerDB.mdf"; // Adjust path as needed
        private Exercise_Controller ec = new Exercise_Controller(serverDB);
        Analytics_Controller ac = new Analytics_Controller();
        private int userId;
        private DBOperations dbOperations = new DBOperations();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
                Response.Redirect("Home_Page.aspx");
            string username = Session["username"].ToString();
                // Fetch user ID using Controller_Users
                Controller_Users cu = new Controller_Users();
                 userId = cu.GetUserIdByUsername(username);
            if (!IsPostBack)
            {
                
                LoadGridView();
            }
        }

        private void LoadGridView()
        {
            DataSet ds = ec.GetAllExercises();
            if (ds != null && ds.Tables.Count > 0)
            {
                GridViewExercises.DataSource = ds.Tables[0];
                GridViewExercises.DataBind();
            }
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            string searchQuery = TxtSearch.Text.Trim();
            DataSet ds = ec.GetAllExercises();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                DataTable filteredTable = ds.Tables[0].Clone();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    if (row["fldExercise_Name"].ToString().ToLower().Contains(searchQuery.ToLower()))
                    {
                        filteredTable.ImportRow(row);
                    }
                }
                GridViewExercises.DataSource = filteredTable;
            }
            else
            {
                GridViewExercises.DataSource = ds.Tables[0];
            }
            GridViewExercises.DataBind();
        }

        protected void GridViewExercises_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewExercises.EditIndex = e.NewEditIndex;
            LoadGridView();
        }

        protected async void GridViewExercises_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridViewExercises.Rows[e.RowIndex];
            int exerciseId = Convert.ToInt32(GridViewExercises.DataKeys[e.RowIndex].Value);
            string exerciseName = ((TextBox)row.Cells[1].Controls[0]).Text;
            string exerciseDesc = ((TextBox)row.Cells[2].Controls[0]).Text;
            int difficulty = Convert.ToInt32(((TextBox)row.Cells[3].Controls[0]).Text);
            int timeToComplete = Convert.ToInt32(((TextBox)row.Cells[4].Controls[0]).Text);

            ec.EditExercise(exerciseId, exerciseName, exerciseDesc, difficulty, timeToComplete);
                dbOperations.EditExercise(exerciseId, exerciseName, exerciseDesc, difficulty, timeToComplete);
               ac.AddUpdate(userId);
            GridViewExercises.EditIndex = -1;
            LoadGridView();
        }

        protected async void GridViewExercises_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int exerciseId = Convert.ToInt32(GridViewExercises.DataKeys[e.RowIndex].Value);
            ec.DeleteExercise(exerciseId);
                try
                {
                await System.Threading.Tasks.Task.Run(() => dbOperations
                        .DeleteExercise(exerciseId)
                        ); // Flattens the Task<TaskOfBoolean> to Task<bool>
                }
                catch (Exception ex)
                {
                    // Log the exception or handle it appropriately
                    Response.Write($"Error: {ex.Message}");
                }
            
            ac.AddDelete(userId);
            LoadGridView();
        }

        protected void GridViewExercises_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewExercises.EditIndex = -1;
            LoadGridView();
        }

        protected void GridViewExercises_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewExercises.PageIndex = e.NewPageIndex;
            LoadGridView();
        }
    }
}