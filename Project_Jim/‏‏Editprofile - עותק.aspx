<%@ Page Title="" Language="C#" MasterPageFile="~/forgeneral.Master" AutoEventWireup="true" CodeBehind="Editprofile.aspx.cs" Inherits="Project_Jim.Editprofile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link href="CssFiles/ForLogSignEdit.css" rel="stylesheet" />
    <script src="Js%20Files/EditValidation.js"></script>
    <style>
        input[type='radio'] {
    accent-color:#5F4C4C;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="edit" name="edit" method="post" onsubmit="return checkal()">  
        <div style="text-align:center">
                     
                            <% 
                                if (msg == "The values have been updated")
                                    Response.Write($"<h1 style='color:green'>{msg}</h1>");
                                else
                                {
                                    Response.Write($"<h1 style='color:red'>{msg}</h1>");
                                    Response.Write($"<h4 style='color:red;text-align:center'>{msg2}</h4>");
                                }

                         %>
                 </div>
               <h1>First name</h1>
               <input class="inp" id="first" name="first" type="text"   value="<%=firstname%>"/>
                <h4 id="Efirst"></h4>
                <h1>Password</h1>
                <input class="inp" id="pass" name="pass" type="text" value="<%=password%>"/>
                <h4 id="Epass"></h4>
                <h1>Phone number</h1>
                <input class="inp" id="phone" name="phone" type="text"   value="<%=phonenumber %>" />
                 <h4 id="Ephone"></h4>
                <h1>Your birthday</h1>
                <h3>Your current birthday is: <%=date%></h3>
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
                <h1>Your goal</h1>
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
                 

    </form>
</asp:Content>
