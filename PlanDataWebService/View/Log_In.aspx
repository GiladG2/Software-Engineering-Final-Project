<%@ Page Title="Log In" Language="C#" MasterPageFile="~/View/MasterPage.Master" AutoEventWireup="true" CodeBehind="Log_In.aspx.cs" Inherits="PlanDataWebService.Modal.Log_In" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CssFiles/ForLogin.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="login-container">
        <div class="login-box">
            <%=msg %>
            <h2>Log In</h2>
            <form id="loginForm" runat="server">
                <div class="form-group">
                    <label for="username">Username:</label>
                    <input type="text" id="username" name="username" runat="server" placeholder="Enter your username" class="form-input" />
                </div>
                <div class="form-group">
                    <label for="password">Password:</label>
                    <input type="password" id="password" name="password" runat="server" placeholder="Enter your password" class="form-input" />
                </div>
                <div class="form-actions">
                    <input type="submit" id="submit" name="submit" class="login-btn" value="Log In" />
                </div>
            </form>
        </div>
    </div>
</asp:Content>
