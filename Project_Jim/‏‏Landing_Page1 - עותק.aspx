<%@ Page Title="" Language="C#" MasterPageFile="~/forgeneral.Master" AutoEventWireup="true" CodeBehind="Landing_Page1.aspx.cs" Inherits="Project_Jim.Landing_Page1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        body{
            background-image:url(https://assets.zyrosite.com/cdn-cgi/image/format=auto,w=1920,fit=crop/bronxltdlm/kulturistas-m7oWrM175eu7nJ9O.png);
            background-repeat:no-repeat;
            background-repeat:no-repeat; 
        }
        #first{
            font-size:150%
        }
        #log{
            width:100px;height:110px;
            float:left
        }
        
        #land
{
    
}
        
        p{
            color:white;
        }
       
    </style>
    <link rel="preconnect" href="https://fonts.googleapis.com">
<link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
<link href="https://fonts.googleapis.com/css2?family=Open+Sans:wght@300&family=PT+Serif&display=swap" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="main" style="text-align:left;color:white;font-size:300%; font-family: 'Open Sans', sans-serif;
font-family: 'PT Serif', serif;" >
            <h1>Welcome to OneLife </h1>
            <h2 id="first">Everything you wanted<br />to know about bodybuilding in one place</h2>
            <p>
                hi <%=firstname%>, well if you're  here then that's a good first step in<br /> 
                your journey if you want to go to the gym to better yourself, lose or gain weight or just want to have a healthier and 
                longer life.<br />
                my name is Gilad, and I'll make sure that by the time you done reading my website, you'll know everything you need and want to know about bodybuilding.
            </p>
       </div>

</asp:Content>
