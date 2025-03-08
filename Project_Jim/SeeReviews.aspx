<%@ Page Title="" Language="C#" MasterPageFile="~/forgeneral.Master" AutoEventWireup="true" CodeBehind="SeeReviews.aspx.cs" Inherits="Project_Jim.SeeReviews" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <link href="CssFiles/ForSeeReviews.css" rel="stylesheet" />
    <link rel="preconnect" href="https://fonts.googleapis.com">
<link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="CssFiles/ForReviewModal.css" rel="stylesheet" />
    <script src="Js%20Files/RemoveXml.js"></script>
    <script src="Js%20Files/ForSee.js"></script>
    <link href="CssFiles/fordisplay.css" rel="stylesheet" />
<link href="https://fonts.googleapis.com/css2?family=Big+Shoulders+Display&family=Open+Sans&display=swap" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <header style="text-align:center;font-size:175%;font-family: 'Big Shoulders Display', cursive;font-family: 'Open Sans', sans-serif;">User's reviews</header>
    <hr style="width:58%;text-align:center;height:2px;background-color:grey" />
    <div id="reviews">
        <%=reviews %>
    </div>
    <div id="com" class="com">
        
       <div class="modal">
           <header id="header"> 
              
           </header>
        <p id="display"></p>
           <footer id="footer"></footer>
       </div>

</div>
</asp:Content>
