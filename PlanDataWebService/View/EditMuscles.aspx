<%@ Page Title="Edit Muscles" Language="C#"  MasterPageFile="~/View/MasterPage.Master" Async="true" AutoEventWireup="true" CodeBehind="EditMuscles.aspx.cs" Inherits="PlanDataWebService.View.EditMuscles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CssFiles/ForEditMuscles.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <form id="form1" runat="server">
        <div class="header">
            <h1>Edit Muscles</h1>
        </div>
        <div class="gridview-container">
            <div class="search-bar">
                <asp:TextBox ID="TxtSearchMuscles" runat="server" CssClass="search-input" Placeholder="Search by muscle name..."></asp:TextBox>
                <asp:Button ID="BtnSearchMuscles" runat="server" Text="Search" OnClick="BtnSearchMuscles_Click" CssClass="search-button" />
            </div>
            <asp:GridView ID="GridViewMuscles" runat="server" AutoGenerateColumns="False" CssClass="gridview" 
                AllowPaging="True" PageSize="10" OnPageIndexChanging="GridViewMuscles_PageIndexChanging"
                OnRowEditing="GridViewMuscles_RowEditing" OnRowUpdating="GridViewMuscles_RowUpdating"
                OnRowDeleting="GridViewMuscles_RowDeleting" OnRowCancelingEdit="GridViewMuscles_RowCancelingEdit" DataKeyNames="fldMuscle_Id">
                <Columns>
                    <asp:BoundField DataField="fldMuscle_Id" HeaderText="Muscle ID" ReadOnly="True" />
                    <asp:BoundField DataField="fldMuscle_Name" HeaderText="Muscle Name" />
                    <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</asp:Content>
