<%@ Page Title="" Language="C#" MasterPageFile="~/View/GeneralMasterPage.Master" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="Project_Gym.View.Test" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">        
        <asp:GridView ID="GridViewExercises" runat="server" AutoGenerateColumns="true" />
    </form>

</asp:Content>
