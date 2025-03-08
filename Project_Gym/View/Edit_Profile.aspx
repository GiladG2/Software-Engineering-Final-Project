<%@ Page Title="" Language="C#" MasterPageFile="~/View/GeneralMasterPage.Master" AutoEventWireup="true" CodeFile="Edit_Profile.aspx.cs" Inherits="Project_Gym.View.Edit_Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link href="CssFiles/ForLogSignEdit.css" rel="stylesheet" />
    <script src="Js%20Files/EditValidation.js"></script>
    <link href="CssFiles/ForEditProfile.css" rel="stylesheet" />
    <style>
        input[type='radio'] {
    accent-color:#5F4C4C;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div style="text-align:center">
                     
                            <% 
                                if (msg == "Your profile has been updated")
                                    Response.Write($"<h1 style='color:green'>{msg}</h1>");
                                else
                                {
                                    Response.Write($"<h1 style='color:red'>{msg}</h1>");
                                    Response.Write($"<h4 style='color:red;text-align:center'>{msg2}</h4>");
                                }

                         %>
                 </div>
        <div id="container">
                <form id="edit" name="edit" method="post" onsubmit="return checkal()">  
             <div id="left-panel">
                <h2>First name</h2>
               <input class="inp" id="first" name="first" type="text"   value="<%=firstname%>"/>
                <h4 id="Efirst"></h4>
                <h2>Password</h2>
                <input class="inp" id="pass" name="pass" type="text" value="<%=password%>"/>
                <h4 id="Epass"></h4>
                <h2>Phone number</h2>
                <input class="inp" id="phone" name="phone" type="text"   value="<%=phonenumber %>" />
                         <h4 id="Ephone"></h4>

             </div>
            <div id="right-panel">

        <h1>Your birthday</h1>
  <h2 >Your current birthday is: <%=date%></h2>
<label style="padding-left:20px" for="date">Update your birthday:</label>
<input id="date" name="date" type="date" value="<%=date%>" />
                <h1>Your gender</h1>
<%
            if (gender == 1)
            {
                Response.Write("are you sure you want to change your gender from male to female?");
                Response.Write("<input id='gender' name='gender' type='radio' value='male' checked/>");
                Response.Write("<label>no</label>");
                Response.Write("<input id='gender' name='gender' type='radio' value='female' />");
                Response.Write("<label>yes</label>");
            }

            else
            {
                Response.Write("are you sure you want to change your gender from female to male?");
                Response.Write("<input id='gender' name='gender' type='radio' value='female' checked/>");
                Response.Write("<label>no</label>");
                Response.Write("<input id='gender' name='gender' type='radio' value='male' />");
                Response.Write("<label>yes</label>");
            }
            %>
        <h2>Your goal</h2>
        <%
            if (goal == 1)
            {
                Response.Write("do you want to change your goal to gain weight?");
                Response.Write("<input id='goal' name='goal' type='radio' value='lose' checked/>");
                Response.Write("<label>no</label>");
                Response.Write("<input id='goal' name='goal' type='radio' value='gain' />");
                Response.Write("<label>yes</label>");
            }
            else
    {
                Response.Write("do you want to change your goal to lose weight?");
                Response.Write("<input id='goal' name='goal' type='radio' value='gain' checked/>");
                Response.Write("<label>no</label>");
                Response.Write("<input id='goal' name='goal' type='radio' value='lose' />");
                Response.Write("<label>yes</label>");
    }
            %>
        
        <br />
         <input type="submit" value="submit" id="submit" name="submit" />
            </div>
                        </form>

        </div>
              
               
                 

</asp:Content>
