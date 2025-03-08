<%@ Page Title="" Language="C#" MasterPageFile="~/View/GeneralMasterPage.Master" AutoEventWireup="true" CodeFile="Landing_Page.aspx.cs" Inherits="Project_Gym.View.Landing_Page" Async="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CssFiles/ForLandingPage.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css2?family=Open+Sans:wght@500&display=swap" rel="stylesheet">
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Lato&display=swap" rel="stylesheet">
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Open+Sans:wght@300&family=PT+Serif&display=swap" rel="stylesheet">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div id="main">
    <h1 class="first">Welcome to OneLife</h1>
    <h2 class="first">Everything you wanted<br />to know about bodybuilding & powerlifting in one place</h2>
    <p>
        Hi <%= firstname %>, well if you're here then that's a good first step in your journey.<br />
        If you want to go to the gym to better yourself, lose or gain weight, or just want to have a healthier and
        longer life, then you're in the right place.<br />
        My name is Gilad, and I'll make sure that by the time you are done reading my website, you'll know everything you need and want to know about bodybuilding and powerlifting.
    </p>
       <%=msg %>
    <%=response    %>
</div>

</asp:Content>
