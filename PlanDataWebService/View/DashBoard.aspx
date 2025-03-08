<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/View/MasterPage.Master" AutoEventWireup="true" CodeBehind="DashBoard.aspx.cs" Inherits="PlanDataWebService.View.DashBoard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">   
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script src="JsFiles/ForDashboard.js"></script>
    <link href="CssFiles/ForDashboard.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Redesigned Header -->
    <div class="dashboard-header">
        <h1>Welcome to Your Dashboard!</h1>
        <p>Track your activities and manage your interface with ease.</p>
    </div>

    <div class="dashboard-container">
        <!-- Split Container -->
        <div class="split-container">
            <!-- Left Panel -->
            <div class="left-panel">
                <!-- API Key Section -->
                <div class="api-key-box">
                    <h2>Generate API Key</h2>
                    <div id="apiKeyDisplay" class="api-key-display">
                        Click "Generate Key" to get your API key.
                    </div>
                    <button id="generateKeyBtn" class="generate-button" onclick="openModal()">Generate Key</button>
                </div>

                <!-- Modal for API Key -->
               <div id="apiKeyModal" class="modal">
    <div class="modal-content">
        <span class="close-btn" onclick="closeModal()">&times;</span>
        <h3>Your API Key:</h3>
        <p id="apiKeyText">Loading...</p>

        <!-- Popup Message -->
        <div class="popup">
            <span class="popuptext" id="successPopup">API Key copied to clipboard!</span>
        </div>

        <button class="copy-button" onclick="copyApiKey()">Copy Key</button>
    </div>
</div>

                <!-- Statistics Section -->
                <div class="statistics-box">
                    <h2>Your Activity Statistics</h2>
                    <div class="statistics-items">
                        <div class="stat-item">
                            <h3>Inserts</h3>
                            <span id="insertCount"><b><%= InsertCount %></b></span>
                        </div>
                        <div class="stat-item">
                            <h3>Updates</h3>
                            <span id="updateCount"><b><%= UpdateCount %></b></span>
                        </div>
                        <div class="stat-item">
                            <h3>Deletes</h3>
                            <span id="deleteCount"><b><%= DeleteCount %></b></span>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Right Panel -->
            <div class="right-panel">
                <!-- Filters Section -->
                <div class="filters">
                    <label for="period">Time Frame:</label>
                    <select id="period" onchange="drawChart()">
                        <option value="0">All time</option>
                        <option value="1">Last month</option>
                        <option value="2">Last 3 months</option>
                        <option value="3">Last 6 months</option>
                        <option value="4">Last year</option>
                    </select>

                    <label for="operation">Operation:</label>
                    <select id="operation" onchange="drawChart()">
                        <option value="0">Insert</option>
                        <option value="1">Update</option>
                        <option value="2">Delete</option>
                    </select>
                </div>

                <!-- Chart Container -->
                <div class="chart-container">
                    <div id="chart_div"></div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
