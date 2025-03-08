<%@ Page Title="" Language="C#" MasterPageFile="~/View/GeneralMasterPage.Master" AutoEventWireup="true" CodeBehind="Training_Plans.aspx.cs" Inherits="Project_Gym.View.Training_Plans" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CssFiles/ForTrainingPlan.css" rel="stylesheet" />
    <script src="Js%20Files/FormSteps.js"></script>
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <script src="Js%20Files/ForTrainingPlans.js"></script>
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Lato:wght@300;400&display=swap" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <% if (!filedARequest) { %>
    <div class="center-container">
        <form id="Form1" runat="server">
            <!-- Progress Bar -->
            <div class="progress-bar">
                <div class="progress-step active">1</div>
                <div class="progress-step">2</div>
                <div class="progress-step">3</div>
            </div>

            <!-- Steps -->
            <div class="step" id="step-one">
                <h1>Step one: Getting to know you</h1>
                <div class="select">
                    <label for="days">How many days a week do you want to train?</label>
                    <select name="days" id="days">
                        <option value="2">2</option>
                        <option value="3">3</option>
                        <option value="4">4</option>
                        <option value="5">5</option>
                        <option value="6">6</option>
                    </select>
                </div>
                <br />
                <div class="select">
                    <label for="length">What is your preferred workout length?</label>
                    <select name="duration" id="duration">
                        <option value="1">30-60 minutes</option>
                        <option value="2">1-1.5 hours</option>
                        <option value="3">>2 hours</option>
                    </select>
                </div>
                <br />
                <div class="select">
                    <label for="injuries">Are you prone to injury in a specific area?</label>
                    <select id="injuries" name="injuries[]" multiple>
                        <option value="0">None</option>
                        <option value="1">Lower back/spine</option>
                        <option value="2">Knees</option>
                        <option value="3">Shoulders</option>
                        <option value="4">Elbows</option>
                    </select>
                </div>
                <br />
                <div class="select">
                    <label for="exp">What is your current lifting experience?</label>
                    <select id="exp" name="exp">
                        <option value="1">beginner</option>
                        <option value="2">3-6 months</option>
                        <option value="3">0.5 - 1.5 years</option>
                        <option value="4">1.5 - 3 years</option>
                        <option value="5">3-5 years</option>
                        <option value="6">>5 years</option>
                    </select>
                </div>
            </div>

            <div class="step" id="step-two" style="display: none;">
                <h1>Step two: Your goals</h1>
                <div class="select">
                    <label for="typeOfPlan">What type of training do you want to do?</label>
                    <select name="typeOfPlan" id="typeOfPlan">
                        <option value="1">Athletic-aerobic</option>
                        <option value="2">Bodybuilding</option>
                        <option value="3">Powerlifting</option>
                    </select>
                </div>
                <div class="select">
                    <label for="OtherHobbies">Do you play any sports?</label>
                    <select id="OtherHobbies">
                        <option>none</option>
                        <option>tennis</option>
                        <option>basketball/football</option>
                        <option>rugby</option>
                    </select>
                </div>
            </div>

            <div class="step" id="step-three">
                <h1>Step three</h1>
                <input type="submit" value="submit and get a plan" name="submit" id="submit" />
            </div>
            <!-- Navigation Buttons -->
            <div class="form-navigation">
                <button type="button" id="prev-button" disabled>Previous</button>
                <button type="button" id="next-button">Next</button>
            </div>
        </form>
    </div>
    <% } else { %>
        <% if (hasAplan) { %>   
    
    <div class="center-container">
            <form id="Plan" runat="server">
                    <h1 style="text-align:center">Your Personalized Plan</h1>
                <div id="linkContainer">
                                <div id="react-root"></div>  <!-- ADD THIS LINE -->
 <script type="text/babel">
      ReactDOM.render(<EditableText />, document.getElementById('react-root'));
    </script>
    <a id="editPlan" class="edit" href="#">Edit Plan</a>
    <a id="editReq" class="edit" href="Edit_Requests.aspx">Edit Requests</a>
</div>
                    <%=response %>
                <a class="pdf-link" href="<%=pdfLink %>" download>Download as PDF<i class="fa-solid fa-download"></i></a>
                    <!--<a href ="/Downloads/planView.xml" download="<%=downloadName%>">Download your plan!</a>-->

</form>
                </div>            
        <% } else { 
                %>    
    <div class="center-container">

     <form id="Form3" runat="server">
         <h1 style="text-align:center">Your soon to be plan</h1>
         <%= response %> <!-- This outputs the plan content -->
         <div>
             <asp:Button ID="submitPlan" CssClass="submitPlan" runat="server" Text="Save Plan" OnClick="InsertPlan" />
             <asp:Button ID="replacePlan" CssClass="replacePlan" runat="server" Text="Replace Plan" OnClick="ReGeneratePlan" />
         </div>

     </form>

     </div>
            
        <% } %>
    <% } %>
</asp:Content>
