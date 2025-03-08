<%@ Page Title="" Language="C#" MasterPageFile="~/View/GeneralMasterPage.Master" AutoEventWireup="true" CodeBehind="Edit_Requests.aspx.cs" Inherits="Project_Gym.View.EditRequests" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CssFiles/ForEditRequests.css" rel="stylesheet" />
    <script src="Js%20Files/FormSteps.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="text-align:center; font-size:15px;color:red;font-family:inherit">
        <%=msg %>
    </div>
    <div class="editContainer">
       <form id="form1" runat="server"   >
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
            <select name="days" id="days" runat="server">
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
            <select name="duration" id="duration" runat="server">
    <option value="1">30-60 minutes</option>
    <option value="2">1-1.5 hours</option>
    <option value="3">>2 hours</option>
</select>

        </div>
        <br />
        <div class="select">
            <label for="injuries">Are you prone to injury in a specific area?</label>
            <select id="injuries"  name="injuries[]" multiple >
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
            <select id="exp" name="exp" runat="server">
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
            <select name="typeOfPlan" id="typeOfPlan" runat="server">
                <option value="1">Athletic-aerobic</option>
                <option value="2">Bodybuilding</option>
                <option value="3">Powerlifting</option>
            </select>
        </div>
        <div class="select">
            <label for="OtherHobbies">Do you play any sports?</label>
            <select id="OtherHobbies" runat="server">
                <option>none</option>
                <option>tennis</option>
                <option>basketball/football</option>
                <option>rugby</option>
            </select>
        </div>
    </div>

    <div class="step" id="step-three">
        <h1>Step three</h1>
        <input type="submit" value="submit and get a new plan" name="submit" id="submit" />
    </div>
    <!-- Navigation Buttons -->
    <div class="form-navigation">
        <button type="button" id="prev-button" disabled>Previous</button>
        <button type="button" id="next-button">Next</button>
    </div>
</form>

    </div>
</asp:Content>
