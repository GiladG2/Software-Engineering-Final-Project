<%@ Page Title="" Language="C#" Async="true" MasterPageFile="~/View/GeneralMasterPage.Master" AutoEventWireup="true" CodeBehind="Training_Log.aspx.cs" Inherits="Project_Gym.View.Training_Log" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
<link href="CssFiles/ForTrainingLog.css" rel="stylesheet" />
    <script src="Js%20Files/TrainingLog/ForAJAX.js"></script>
    <script src="Js%20Files/TrainingLog/ForGraphs.js"></script>
    <script src="Js%20Files/TrainingLog/ForTrainingLogGeneral.js"></script>
    <link href="https://fonts.googleapis.com/css2?family=Lora:wght@400;700&family=Playfair+Display:wght@400;700&display=swap" rel="stylesheet">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dashboard">
        <h1>Welcome again!</h1>
        <h2 onclick="showModal()">Add a workout <br /> </h2>     
        <div>
            <h2 id="test">Graph</h2>
            <label for="period">Time frame: </label>
            <select id="period" onchange="drawChart(<%=planId %>)" >
                <option value="0">All time</option>
                <option value="1">Last month</option>
                <option value="2">Last 3 months</option>
                <option value="3">Last 6 months</option>
                <option value="4">Last year</option>
            </select>
        </div>
        <div id="myChart" style="width:700px; max-width:600px;"></div>
    </div>
    <div id="modalContainer">
        <div id="modal">
           <template id="exercise-template">
    <div class="exercise-box">
        <h3 class="exercise-name"></h3>
        <p>Reps: <span class="exercise-reps"></span></p>
        <p>Weight: <span class="exercise-weight"></span> kg</p>
        <div class="button-container">
            <button class="edit-btn">Edit</button>
            <button class="delete-btn">Delete</button>
        </div>
        <hr />
    </div>
</template>

            <%=exercises %>
            <input type="date" id="date" value="<%=date %>" onchange="changeLog(<%=planId %>)"/>
            <div id="reps-container">
                <span id="reps-label">Reps:</span>
                <button data-action="minus" onclick="remove('reps',0)">-</button>
                <input id="reps" type="number" value="0" min="0" onchange="validatePositiveWeight('reps',0)" />
                <button data-action="plus" onclick="add('reps',0)">+</button>
            </div>

            <div id="weight-container">
                <span id="weight-label">Weight [kg]:</span>
                <button data-action="minus" onclick="remove('weight',0)">-</button>
                <input id="weight" type="number" value="0" min="0" step="0.1" onchange="validatePositiveWeight('weight',0)" />
                <button data-action="plus" onclick="add('weight',0)">+</button>
            </div>
            <button onclick="addExerciseBox(<%=userId %>,<%=planId %>);">Save</button>
            <div id="exerciseList">
                <%=log %>
            </div>  
        </div>
    </div>
    <div id="feedback-section">
        <button id="get-feedback-btn" onclick="openFeedbackModal(<%=userId %>,<%=planId%>)">Get Feedback</button>
    </div>
    
    <!-- Feedback Modal -->
    <div id="feedback-modal-container" class="modal-container">
    <div id="feedback-modal" class="modal">
        <button class="close-btn" onclick="closeFeedbackModal()">×</button>
        <div class="modal-content">
            <!-- Feedback will be dynamically inserted here -->
        </div>
    </div>
</div>

</asp:Content>

