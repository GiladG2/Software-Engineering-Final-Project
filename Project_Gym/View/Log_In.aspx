<%@ Page Title="" Language="C#" MasterPageFile="~/View/GeneralMasterPage.Master" AutoEventWireup="true" CodeFile="Log_In.aspx.cs" Inherits="Project_Gym.View.Log_In1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://fonts.googleapis.com/css2?family=Open+Sans:wght@500&display=swap" rel="stylesheet">
    <link rel="preconnect" href="https://fonts.googleapis.com">
<link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
<link href="https://fonts.googleapis.com/css2?family=Lato&display=swap" rel="stylesheet">
   <link rel="preconnect" href="https://fonts.googleapis.com">
    <link href="CssFiles/Log_In.css" rel="stylesheet" />
<link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
<link href="https://fonts.googleapis.com/css2?family=Lato:ital,wght@0,300;0,400;0,700;1,300&display=swap" rel="stylesheet">
    <title>Log in</title>
    <script src="Js%20Files/forcoolstuff.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="container">
        <div id="left-panel">    
            <h1 style="text-align:center">Sign in</h1>
             <form id="data" name="data" method="post">
     <h2>Username</h2>
     <input class="field inputs" type="text" id="usname" name="usname" placeholder="Enter your username" />
     <h2>Password</h2>
     <input class="field inputs" type="password" id="pass" name="pass" placeholder="Enter your password"  />
     <button onmouseup="ShowPassword()" id="eye" type="button"><i class="fa-sharp fa-solid fa-eye-slash"></i></button>
     <input type="submit" class="field" id="submit" name="submit" value="Sign in" />
     <div style="text-align:center;color:white;">
     <%      
         if (msg != null)
             Response.Write($"<b style='color:#900C3F'>{msg}</b>");
     %>
  </div>
</form>
        </div>
        <div id ="right-panel">
            <h1 style="font-family: 'Lato', sans-serif;">Welcome Back!</h1>
            <h2 style="font-family: 'Lato', sans-serif;font-weight: 300;">Don't have an account?</h2>
            <a href="Registration.aspx">Sign up</a>
        </div>
    </div>
</asp:Content>

