<%@ Page Title="" Language="C#" MasterPageFile="~/View/GeneralMasterPage.Master" AutoEventWireup="true" CodeBehind="Send_Review.aspx.cs" Inherits="Project_Gym.View.Send_Review" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
   <link rel="preconnect" href="https://fonts.googleapis.com">
<link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
      <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" integrity="sha512-ywle6BnZ6EaKjJfSLSFbCnqYltXL4UL4HsRKvoJ40gTkppb1k2rHvI8P0Wt/rlg+bO/sXtOvN8x/hf/ZG0Mq3w==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    <script src="Js%20Files/Forsendreviews.js"></script>
<link href="https://fonts.googleapis.com/css2?family=Big+Shoulders+Display&family=Open+Sans&display=swap" rel="stylesheet">
    <link href="CssFiles/SendReview.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 style="text-align:center;font-size:250%;font-family: 'Big Shoulders Display', cursive;font-family: 'Open Sans', sans-serif;">Leave a review!</h1>
    <form id="review" method="post" onsubmit="return checkall()">
        <h3>Title</h3>
    <input type="text" id="title" name="title" oninput="checktitlenow()"/>
    <h5 id="Etitle"></h5>
    <h3>What do you think about the website so far?</h3>
    <textarea oninput="checknow()" id="comment" name="comment" rows="9" cols="60" style="resize:none;"></textarea>
        <br />
    <h5 id="forcheck"></h5>
    <label for="rating">Rating:</label>
    <select id="rating" name="rating">
      <option value="1">★</option>
      <option value="2">★★</option>
      <option value="3">★★★</option>
      <option value="4">★★★★</option>
      <option value="5">★★★★★</option>
      </select>
        <br />
        <br />
<input type="submit" id="submit" name="submit" value="submit"/>
        <div id="msg" style="font-size:200%; text-align:center;text-decoration:double;color:black; font-family: 'Open Sans', sans-serif;">
          <%=messege %>
      </div>
        </form>
     
</asp:Content>
