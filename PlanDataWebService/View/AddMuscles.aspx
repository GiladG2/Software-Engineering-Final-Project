<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage.Master" Async="true" AutoEventWireup="true" CodeBehind="AddMuscles.aspx.cs" Inherits="PlanDataWebService.View.AddMuscles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CssFiles/ForAddExercise.css" rel="stylesheet" />
    <style>
        /* Styling for error messages */
        .error-message {
            color: red;
            font-size: 0.9em;
            margin-top: 4px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container2">
        <h1><%=msg %></h1>
        <h1>Add New Muscle</h1>
        <form id="addMuscleForm" method="POST">
            <div class="form-group">
                <label for="muscle_name">Muscle Name:</label>
                <input type="text" id="muscle_name" name="muscle_name" required />
                <div id="muscle_name_error" class="error-message"></div>
            </div>
            <div class="form-group">
                <label for="muscle_desc">Muscle Description:</label>
                <input type="text" id="muscle_desc" name="muscle_desc" required />
                <div id="muscle_desc_error" class="error-message"></div>
            </div>
            <div class="form-group">
                <label for="muscle_group">Muscle Group:</label>
                <select id="muscle_group" name="muscle_group" required>
                    <option value="0">Legs</option>
                    <option value="1">Push</option>
                    <option value="2">Arms</option>
                    <option value="3">Pull</option>
                </select>
            </div>
            <div class="form-group">
                <input id="submit" name="submit" type="submit" value="Add Muscle" />
            </div>
        </form>
    </div>

    <!-- Client-side JavaScript Validation -->
    <script type="text/javascript">
        document.getElementById("addMuscleForm").addEventListener("submit", function (e) {
            // Retrieve the field values
            var muscleName = document.getElementById("muscle_name").value;
            var muscleDesc = document.getElementById("muscle_desc").value;
            var valid = true;

            // Clear previous error messages
            document.getElementById("muscle_name_error").innerHTML = "";
            document.getElementById("muscle_desc_error").innerHTML = "";

            // Validate Muscle Name
            if (muscleName.trim() === "") {
                document.getElementById("muscle_name_error").innerHTML = "Muscle name cannot be empty.";
                valid = false;
            }
            // Validate Muscle Description
            if (muscleDesc.trim() === "") {
                document.getElementById("muscle_desc_error").innerHTML = "Muscle description cannot be empty.";
                valid = false;
            }

            // If any validation fails, prevent form submission
            if (!valid) {
                e.preventDefault();
            }
        });
    </script>
</asp:Content>
