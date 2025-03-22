using ControllersProject.Controller;
using ControllersProject.Modal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TrainingServerAPI.Modals;

namespace TrainingServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersAPIController : ControllerBase
    {
        private Users_Controller cu = new Users_Controller();
        private readonly IConfiguration _configuration;

        // Constructor - IConfiguration is injected by ASP.NET Core
        public UsersAPIController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // Get User Data
        [HttpGet("GetUserData")]
        public JsonResult GetUserData(string query)
        {
            return new JsonResult(Ok(cu.GetData(query)));
        }
        [HttpGet("GetUser")]
        public JsonResult GetUser(string username)
        {
            User user = new User(username);
            return new JsonResult(Ok(user));

        }
        // Get Data Set
        [HttpGet("GetDataSet")]
        public JsonResult GetDataSet(string query)
        {
            return new JsonResult(Ok(cu.GetDataSet(query)));
        }

        // Check If User Exists
        [HttpGet("IsExist")]
        public JsonResult IsExist(int userid)
        {
            return new JsonResult(Ok(cu.IsExist(userid)));
        }

        // Edit User Password
        [HttpPost("EditPass")]
        public JsonResult EditPass(int userId, string newPass)
        {
            return new JsonResult(Ok(cu.EditPass(userId, newPass)));
        }

        // Get Access Key
        [HttpGet("GetAccessKey")]
        public JsonResult GetAccessKey(string username)
        {
            return new JsonResult(Ok(cu.GetAccessKey(username)));
        }

        // Get First Name
        [HttpGet("GetFirstName")]
        public JsonResult GetFirstName(string username)
        {
            return new JsonResult(Ok(cu.GetFirstName(username)));
        }

        // Get Password
        [HttpGet("GetPassword")]
        public JsonResult GetPassword(string username)
        {
            return new JsonResult(Ok(cu.GetPassword(username)));
        }

        // Get Phone Number
        [HttpGet("GetPhonenumber")]
        public JsonResult GetPhonenumber(string username)
        {
            return new JsonResult(Ok(cu.GetPhonenumber(username)));
        }

        // Get Gender
        [HttpGet("GetGender")]
        public JsonResult GetGender(string username)
        {
            return new JsonResult(Ok(cu.GetGender(username)));
        }

        // Get Gender Name
        [HttpGet("GetGenderName")]
        public JsonResult GetGenderName(string username)
        {
            return new JsonResult(Ok(cu.GetGenderName(username)));
        }

        // Get Date of Birth
        [HttpGet("GetDate")]
        public JsonResult GetDate(string username)
        {
            return new JsonResult(Ok(cu.GetDate(username)));
        }

        // Get Age
        [HttpGet("GetAge")]
        public JsonResult GetAge(string username)
        {
            return new JsonResult(Ok(cu.GetAge(username)));
        }

        // Get Username by ID
        [HttpGet("GetUsernameById")]
        public JsonResult GetUsernameById(int id)
        {
            return new JsonResult(Ok(cu.GetUsernameFromId(id)));
        }

        // Get User Goal
        [HttpGet("GetGoal")]
        public JsonResult GetGoal(string username)
        {
            return new JsonResult(Ok(cu.GetGoal(username)));
        }

        // Get User ID
        [HttpGet("GetUserId")]
        public JsonResult GetUserId(string username)
        {
            return new JsonResult(Ok(cu.GetUserId(username)));
        }

        // Get Email
        [HttpGet("GetEmail")]
        public JsonResult GetEmail(string username)
        {
            return new JsonResult(Ok(cu.GetEmail(username)));
        }
        [HttpGet("CheckSession")]
        public IActionResult CheckSession()
        {
            var token = Request.Cookies["AuthToken"];
            if (string.IsNullOrEmpty(token)) return Unauthorized(new { isAuthenticated = false });

            var jwtTokenAuth = new JwtTokenAuth(_configuration);
            var principal = jwtTokenAuth.ValidateJwtToken(token);

            if (principal != null)
            {
                var username = principal.FindFirst(ClaimTypes.Name)?.Value;
                var accesskey = principal.FindFirst("accesskey")?.Value;

                return Ok(new { isAuthenticated = true, username, accesskey });
            }

            return Unauthorized(new { isAuthenticated = false });
        }

        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            // Remove the JWT cookie (if you're using cookies)
            Response.Cookies.Delete("AuthToken");

            return Ok(new { message = "Logged out successfully" });
        }

        [HttpGet("TestData")]
        public IActionResult TestData(string username, string password)
        {
            bool userAuthenticated = cu.TestData(username, password);
            if (userAuthenticated)
            {
                string accesskey = cu.GetAccessKey(username).ToString();
                var jwtTokenAuth = new JwtTokenAuth(_configuration);
                var token = jwtTokenAuth.GenerateJwtToken(username, accesskey);

                Response.Cookies.Append("AuthToken", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.Now.AddHours(1)
                });

                return Ok(new { message = "Login successful" });
            }
            return Unauthorized("Invalid username or password");
        }



        // Add New User
        [HttpPost("AddUser")]
        public JsonResult AddUser(string username, string password, string firstname, string phonenumber, int gender, string date, int goal, int access, string email)
        {   if (cu.IsTaken(username))
                return new JsonResult(Ok(new { message = "This username is taken" }));
            if (cu.AddData(username, password, firstname, phonenumber, gender, date, goal, access, email))
                return new JsonResult( Ok(new { message = "Success" }));
            return new JsonResult( Ok(new { message = "Error" }));
            
        }

        // Check if Username is Taken
        [HttpGet("IsTaken")]
        public JsonResult IsTaken(string username)
        {
            return new JsonResult(Ok(cu.IsTaken(username)));
        }

        [HttpPut("EditProfile")]

        public JsonResult EditProfile(string username, string firstName,string password,string phonenumber,string email)
        {
            return new JsonResult(Ok(cu.EditUser(username,password,phonenumber,firstName,email)));
        }

        // Delete User
        [HttpDelete("DeleteUser")]
        public JsonResult DeleteUser(string username)
        {
            return new JsonResult(Ok(cu.DeleteUser(username)));
        }

        // Update User Profile
        [HttpPatch("UpdateProfile")]
        public JsonResult UpdateProfile(string username, string password, string date, string phonenumber, string firstname, int gender, int goal)
        {
            return new JsonResult(Ok(cu.UpdateDataForEdit(username, password, date, phonenumber, firstname, gender, goal)));
        }

        // Add User Request
        [HttpPost("AddUserRequest")]
        public JsonResult AddUserRequest(int userId, int duration, int daysAWeek, int experience, int typeOfPlan, int[] injuryList)
        {
            return new JsonResult(Ok(cu.AddUserRequest(userId, duration, daysAWeek, experience, typeOfPlan, injuryList)));
        }

        // Check if User has Filed a Request
        [HttpGet("AlreadyFiledRequest")]
        public JsonResult AlreadyFiledRequest(int userId)
        {
            return new JsonResult(Ok(cu.AlreadyFiledRequest(userId)));
        }

        //Log in records
        [HttpPost("AddLogInRecord")]
        public JsonResult AddLogInRecord(string username)
        {
            Log_In_Records_Controller lirc = new Log_In_Records_Controller();
            int userId = cu.GetUserId(username);
            return new JsonResult(Ok(lirc.InsertLogInRecord(userId)));
        }

        
    }
}
