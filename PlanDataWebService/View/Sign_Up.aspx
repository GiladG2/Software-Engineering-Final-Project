<%@ Page Title="Sign Up" Language="C#" MasterPageFile="~/View/MasterPage.Master" AutoEventWireup="true" CodeBehind="Sign_Up.aspx.cs" Inherits="PlanDataWebService.View.Sign_Up" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CssFiles/ForSignUp.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="signup-container">
        <div class="signup-box">       
            <%=msg %>
            <h2>Create Your Account</h2>
            <form id="signupForm" runat="server">
                <div class="form-group">
                    <label for="username">Username</label>
                    <input 
                        type="text" 
                        id="username" 
                        name="username" 
                        runat="server" 
                        placeholder="Enter your username" 
                        class="form-input" />
                </div>
                <div class="form-group">
                    <label for="email">Email</label>
                    <input 
                        type="email" 
                        id="email" 
                        name="email" 
                        runat="server" 
                        placeholder="Enter your email" 
                        class="form-input" />
                </div>
                <div class="form-group">
                    <label for="password">Password</label>
                    <input 
                        type="password" 
                        id="password" 
                        name="password" 
                        runat="server" 
                        placeholder="Enter your password" 
                        class="form-input" />
                </div>
                <div class="form-group">
                    <input type="submit" id="submit" name="submit" class="signup-btn" value="Sign Up" />
                </div>
            </form>
        </div>
    </div>
</asp:Content>
