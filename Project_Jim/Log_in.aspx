<%@ Page Title="" Language="C#" MasterPageFile="~/forgeneral.Master" AutoEventWireup="true" CodeBehind="Log_in.aspx.cs" Inherits="Project_Jim.Log_in" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://fonts.googleapis.com/css2?family=Open+Sans:wght@500&display=swap" rel="stylesheet">
        <link href="CssFiles/ForLogSignEdit.css" rel="stylesheet" />
   
    <title>Log in</title>
    <script src="Js%20Files/forcoolstuff.js"></script>
    <style>
        #picforlog
        {
            float:left;
        }
        body {
background-color:white;

        }
   .navigator .dum{
       color:black;
       background-color:black
   }
   

   .navigator{
       margin-top:0;
   }


.up{
    padding-right:100px
    ;padding-bottom:10px;
}
strong{
    text-decoration:underline
}

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="edit">
           <h1 style="text-align:center;font-family: 'Open Sans', sans-serif">
               Login
           </h1>
            <form id="data" name="data" method="post">
                <h2>Enter Username</h2>
                <input class="up" type="text" id="usname" name="usname" placeholder="Enter your username" />
                <h2>Enter Password</h2>
                <input class="up" type="password" id="pass" name="pass" placeholder="Enter your password"  />
                <button onmouseup="ShowPassword()" id="eye" type="button"><i class="fa-sharp fa-solid fa-eye-slash"></i></button>
                <h2></h2>
                <input type="submit" id="submit" name="submit" value="Sign in" />
                <div style="text-align:center;color:white;">
                <%      
                    if (msg != null)
                        Response.Write($"<b style='color:#900C3F'>{msg}</b>");
                %>
                    
             </div>
           </form>
            <a style="text-decoration:none; color:black" href="Registeration.aspx"><h1> Don't have an account?  <strong>create one here!</strong> </h1> </a>
        </div>
</asp:Content>
