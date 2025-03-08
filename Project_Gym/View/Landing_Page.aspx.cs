using System;
using System.Threading.Tasks;


namespace Project_Gym.View
{
    public partial class Landing_Page : System.Web.UI.Page
    {
        public string firstname, response;
        public string msg = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["firstname"] != null)
                firstname = Session["firstname"].ToString();
            if (!IsPostBack)
            {
                // Fetch and store muscle-exercise associations only on the first load (not on postbacks)
                //await InsertMuscleExerciseAssociationsAsync();
            }
        }

        private async Task InsertMuscleExerciseAssociationsAsync()
        {
            // Instantiate the controller for inserting associations
            /*  Model_Exercise me = new Model_Exercise();

              // Call the method to insert muscle-exercise associations
              await me.InsertExercisesAsync();
            var controller = new Controller_Exercises();
            await controller.ReapetForAllMuscles();
            */
        }

    }
}
