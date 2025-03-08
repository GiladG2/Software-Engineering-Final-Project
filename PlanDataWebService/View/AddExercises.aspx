<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage.Master" Async="true" AutoEventWireup="true" CodeBehind="AddExercises.aspx.cs" Inherits="PlanDataWebService.View.AddExercises" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CssFiles/ForAddExercise.css" rel="stylesheet" />
    <script type="text/javascript">
        function validateForm() {
            var exerciseName = document.getElementById("exercise_name").value;
            var exerciseDesc = document.getElementById("exercise_desc").value;
            var difficulty = document.getElementById("difficulty").value;
            var timeToComplete = document.getElementById("time_to_complete").value;
            var muscle = document.getElementById("muscle").value;
            var errorMessage = "";

            // Check if Exercise Name is filled
            if (exerciseName == "") {
                errorMessage += "Exercise Name is required.\n";
            }

            // Check if Exercise Description is filled
            if (exerciseDesc == "") {
                errorMessage += "Exercise Description is required.\n";
            }

            // Check if Difficulty is selected
            if (difficulty == "") {
                errorMessage += "Please select a difficulty level.\n";
            }

            // Check if Time to Complete is selected
            if (timeToComplete == "") {
                errorMessage += "Please select a time to complete.\n";
            }

            // Check if Muscle is selected
            if (muscle == "") {
                errorMessage += "Please select a muscle.\n";
            }

            // If there are any errors, show them
            if (errorMessage != "") {
                alert(errorMessage);
                return false; // Prevent form submission
            }

            return true; // Allow form submission
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container2">
        <h2 style="text-align:center"><%=msg %></h2>
        <h1>Add New Exercise</h1>
        <form method="POST" onsubmit="return validateForm()">
            <!-- Exercise Name -->
            <div class="form-group">
                <label for="exercise_name">Exercise Name:</label>
                <input type="text" id="exercise_name" name="exercise_name" required>
            </div>

            <!-- Exercise Description -->
            <div class="form-group">
                <label for="exercise_desc">Exercise Description:</label>
                <textarea id="exercise_desc" name="exercise_desc" rows="4" required></textarea>
            </div>

            <!-- Difficulty -->
            <div class="form-group">
                <label for="difficulty">Difficulty:</label>
                <select id="difficulty" name="difficulty" required>
                    <option value="">Select Difficulty</option>
                    <option value="1">1 - Easy</option>
                    <option value="3.5">3.5 - Medium</option>
                    <option value="5">5 - Hard</option>
                </select>
            </div>

            <!-- Time to Complete -->
            <div class="form-group">
                <label for="time_to_complete">Time to Complete:</label>
                <select id="time_to_complete" name="time_to_complete" required>
                    <option value="">Select Time to Complete</option>
                    <option value="1">1 - 5-15 min</option>
                    <option value="2">2 - 15-30 min</option>
                    <option value="3">3 - 30 min - 1 hour</option>
                </select>
            </div>

            <!-- Muscle -->
            <div class="form-group">
                <label for="muscle">Muscle:</label>
                <%=dropDown %>
            </div>

            <!-- Submit Button -->
            <div class="form-group">
                <input type="submit" id="submit" name="submit" value="Add exercise" />
            </div>
        </form>
    </div>
</asp:Content>
