<%@ Page Title="" Language="C#" MasterPageFile="~/forgeneral.Master" AutoEventWireup="true" CodeBehind="Shop.aspx.cs" Inherits="Project_Jim.Shop" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CssFiles/ForShop.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="navContainer"><div id="shopNav">  
        <h3>search</h3>
        <select>
            <option>category</option>
            <option>supplements</option>
            <option>weights</option>
            <option>clothes</option>
        </select>
        <select>
    <option>price</option>
    <option>lowest to highest</option>
    <option>highest to lowest</option>
        </select>
    </div></div>
    <div id="products-container">
        <%=tableHtml %>
    </div>
</asp:Content>
