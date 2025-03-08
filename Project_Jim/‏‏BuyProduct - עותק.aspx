<%@ Page Title="" Language="C#" MasterPageFile="~/forgeneral.Master" AutoEventWireup="true" CodeBehind="BuyProduct.aspx.cs" Inherits="Project_Jim.BuyProduct" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CssFiles/BuyProduct.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
    <img src="<%=image %>" />
        <h1><%=name %></h1>
    <h3 id="desc"><%=description %></h3>
      <h1><%=stars %>  <%=rating %></h1>
    </div>
</asp:Content>
