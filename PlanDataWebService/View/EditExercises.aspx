<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage.Master" Async="true" AutoEventWireup="true" CodeBehind="EditExercises.aspx.cs" Inherits="PlanDataWebService.View.EditExercises" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CssFiles/ForEditExercises.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <form id="form1" runat="server">
        <div class="header">
            <h1>Edit Exercises</h1>
        </div>
        <div class="gridview-container">
            <div class="search-bar">
                <asp:TextBox ID="TxtSearch" runat="server" CssClass="search-input" Placeholder="Search by exercise name..."></asp:TextBox>
                <asp:Button ID="BtnSearch" runat="server" Text="Search" OnClick="BtnSearch_Click" CssClass="search-button" />
            </div>
            <asp:GridView ID="GridViewExercises" runat="server" AutoGenerateColumns="False" CssClass="gridview" 
                AllowPaging="True" PageSize="10" OnPageIndexChanging="GridViewExercises_PageIndexChanging"
                OnRowEditing="GridViewExercises_RowEditing" OnRowUpdating="GridViewExercises_RowUpdating"
                OnRowDeleting="GridViewExercises_RowDeleting" OnRowCancelingEdit="GridViewExercises_RowCancelingEdit" DataKeyNames="fldExercise_Id">
                <Columns>
                    <asp:BoundField DataField="fldExercise_Id" HeaderText="Exercise ID" ReadOnly="True" />
                    <asp:BoundField DataField="fldExercise_Name" HeaderText="Exercise Name" />
                    <asp:BoundField DataField="fldExercise_Desc" HeaderText="Exercise Descp" />
                    <asp:BoundField DataField="fldDifficulty" HeaderText="Difficulty" />
                    <asp:BoundField DataField="fldTime_To_Complete" HeaderText="Time to Complete" />
                    <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</asp:Content>
