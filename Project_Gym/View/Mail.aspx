<%@ Page Title="" Language="C#" MasterPageFile="~/View/GeneralMasterPage.Master" AutoEventWireup="true" CodeBehind="Mail.aspx.cs" Inherits="Project_Gym.View.Mail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CssFiles/ForSeeReviews.css" rel="stylesheet" />
    <script src="Js%20Files/ForSeeResponse.js"></script>
    <script src="Js%20Files/ForSeeReview.js"></script>
<script src="Js%20Files/ForSeeReview.js"></script>
                   <script src="Js%20Files/ForSee.js"></script>
      <link rel="preconnect" href="https://fonts.googleapis.com">
    <link href="CssFiles/ForSeeResponseModal.css" rel="stylesheet" />
<link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Big+Shoulders+Display&family=Open+Sans&display=swap" rel="stylesheet">

</asp:Content>  
    
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <header style="text-align:center;font-size:175%;font-family: 'Big Shoulders Display', cursive;font-family: 'Open Sans', sans-serif;">Mail</header>
    <hr  />
    <div id="reviews">
        <%=reviews %>
    </div>
    <div id="com" class="com">
        
       <div class="modal">
           <header id="header"> 
              
           </header>
        <p id="display"></p>
           <footer id="footer">
               <button onclick="DisplayText()"> Respond</button>
<div id="respond" class="respond">
    <form id="respondToUser" name="respondToUser" runat="server" method="post">
        <textarea id="comment" name="comment" rows="9" cols="30" style="resize:vertical;"></textarea>
        <br />
        <label style="color:white" for="usernameToFind">To: </label>
        <input id="usernameToFind" name="usernameToFind" readonly="readonly" />     
        <input id="titleToSend" name="titleToSend" type="hidden"/>     
        <input type="submit" id="submit" name="submit" />
           </footer>
    </form>
</div>
</div>
</asp:Content>
