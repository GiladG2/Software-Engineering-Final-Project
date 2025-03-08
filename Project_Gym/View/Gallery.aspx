<%@ Page Title="" Language="C#" MasterPageFile="~/View/GeneralMasterPage.Master" AutoEventWireup="true" CodeFile="Gallery.aspx.cs" Inherits="Project_Gym.View.Gallery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="preconnect" href="https://fonts.googleapis.com">
<link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
<link href="https://fonts.googleapis.com/css2?family=Open+Sans&display=swap" rel="stylesheet">
    <link rel="preconnect" href="https://fonts.googleapis.com">
<link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
<link href="https://fonts.googleapis.com/css2?family=Big+Shoulders+Display&display=swap" rel="stylesheet">
    <link href="CssFiles/ForGallery.css" rel="stylesheet" />
    <link rel="preconnect" href="https://fonts.googleapis.com">
<link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
<link href="https://fonts.googleapis.com/css2?family=Open+Sans:wght@500&display=swap" rel="stylesheet">
    <script src="Js%20Files/ForGallery1.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div style="text-align:center;color:black;font-family: 'Big Shoulders Display', cursive;">
         <h1>Welcome to my gallery<br /> here, you will see popular photos that are related to the bodybuilding world!</h1>
     </div>
    <div id="full">
    <img id="fimage" src="https://i.redd.it/7hf2j25sxeq61.png" />
    <span onclick="closeimage()">X</span>
    </div>
<div id="gallery">
     <table align="center" ; border="0">
                 
                <tr>
                    
                    <td>
                        <img src="pics%20for%20home%20page/636d45eb8fd83aa04e621b362ae1a699%20(1).jpg"
                            width="350"
                            height="300" onclick="openImage(this.src)"/>
                        </td>
                    <td >
                        <img src="https://i.redd.it/7hf2j25sxeq61.png" 
                            width="350"
                            height="300" onclick="openImage(this.src)"/>
                        </td>
                    <td>
                        <img src="pics%20for%20home%20page/arnold-1024x754.jpg"
                            width="350"
                            height="300"
                            onclick="openImage(this.src)"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        <img src="pics%20for%20home%20page/Franco-no2-e1469703534821.jpg" width="350"height="300" onclick="openImage(this.src)"/>                  

                        </td>
                    <td> <img src="pics%20for%20home%20page/download%20(3).jpg"
                            width="350"
                            height="300"
                        onclick="openImage(this.src)"/></td>
                    <td>
                        <img src="https://staticc.sportskeeda.com/editor/2022/07/ba8ed-16585063239335-1920.jpg"
                            width="350"
                            height="300"
                            onclick="openImage(this.src)"/>
                    </td>
                </tr>
                   
            </table>
</div>
            
       
</asp:Content>
