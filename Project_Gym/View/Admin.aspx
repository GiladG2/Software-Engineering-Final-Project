<%@ Page Title="" Language="C#" MasterPageFile="~/View/GeneralMasterPage.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="Project_Gym.View.Admin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link rel="preconnect" href="https://fonts.googleapis.com">
<link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
<link href="https://fonts.googleapis.com/css2?family=Tilt+Neon&display=swap" rel="stylesheet">
    <link href="CssFiles/ForAdminPanel.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="Admin-Container">
   <form runat="server">
        <asp:TextBox ID="txtSearch" runat="server" CssClass="search-box" placeholder="Search by username..."/>
       <asp:DropDownList ID="ddlRole" runat="server" CssClass="filter-users" AutoPostBack="true" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged">
<asp:ListItem Value="All" Text="All roles"/>
            <asp:ListItem Value="Admin" Text="Admin" />
            <asp:ListItem Value="User" Text="User" />
        </asp:DropDownList>
        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="search-button" OnClick="btnSearch_Click"/>
      
       <div style="padding-top:20px">
<asp:GridView  ID="GridView1" HorizontalAlign="Center" runat="server" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" AutoGenerateColumns="false" AllowSorting="true"
     OnRowDeleting="GridView1_RowDeleting" AllowPaging="true" PageSize="10" OnPageIndexChanging="GridView1_PageIndexChanging"  OnRowEditing="GridView1_RowEditing" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowUpdating="GridView1_RowUpdating" OnSorting="GridView1_Sorting">
    <FooterStyle BackColor="#db6b04" />
    <HeaderStyle BackColor="#db6b04" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#db6b04" ForeColor="white" HorizontalAlign="Right" />
    <RowStyle BackColor="#ffffff" ForeColor="#db6b04"  />
    <SelectedRowStyle BackColor="#db6b04" Font-Bold="True" ForeColor="White" />
    <SortedAscendingCellStyle BackColor="#FBFBF2" />
    <SortedAscendingHeaderStyle BackColor="#848384" />
    <SortedDescendingCellStyle BackColor="#EAEAD3" />
    <SortedDescendingHeaderStyle BackColor="#575357" />
   
    <Columns>
      
        <asp:BoundField DataField="user_Id" HeaderText="User_Id" ReadOnly="true" />
       <asp:BoundField DataField="fldUsername" HeaderText="Username" ReadOnly="true" />
            <asp:TemplateField HeaderText="Password">
<ItemTemplate>
    <asp:Label ID="lblPassword" runat="server" Text='<%# Bind("fldPassword") %>'></asp:Label>
</ItemTemplate>
<EditItemTemplate>
    <asp:TextBox ID="txtPassword" runat="server" Text='<%# Bind("fldPassword") %>' />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPassword" ErrorMessage="A password cannot be empty" CssClass="error-message" ForeColor="Red"/>
    <asp:RegularExpressionValidator ID="regPassword" runat="server" 
        ControlToValidate="txtPassword" 
        ValidationExpression="^(?!\s*$)(?=.*[!#@%^&>{}]).*$" 
        ErrorMessage="Invalid phone number format."
        CssClass="error-message" ForeColor="Red" />
</EditItemTemplate>
</asp:TemplateField>
        <asp:TemplateField HeaderText="Name">
    <ItemTemplate>
        <asp:Label ID="lblFirstName" runat="server" Text='<%# Bind("fldFirstName") %>'></asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <asp:TextBox ID="txtFirstName" runat="server" Text='<%# Bind("fldFirstName") %>' />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFirstName" ErrorMessage="A first name cannot be empty" CssClass="error-message" ForeColor="Red"/>

        <asp:RegularExpressionValidator ID="regFirstName" runat="server" 
            ControlToValidate="txtFirstName" 
            ValidationExpression="^(?!\s*$)[A-Z][a-z]+$" ErrorMessage="Invalid phone number format."
            CssClass="error-message" ForeColor="Red" />
    </EditItemTemplate>
</asp:TemplateField>


        <asp:TemplateField HeaderText="Phone">
    <ItemTemplate>
        <asp:Label ID="lblPhone" runat="server" Text='<%# Bind("fldPhone") %>'></asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <asp:TextBox ID="txtPhone" runat="server" Text='<%# Bind("fldPhone") %>' />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPhone" ErrorMessage="A phone cannot be empty" CssClass="error-message" ForeColor="Red"/>
        <asp:RegularExpressionValidator ID="regPhone" runat="server"
            ControlToValidate="txtPhone" 
            ValidationExpression="^(?!\s*$)(?:\+972|0)(?:-?|\s?)\d{1,2}(?:-?|\s?)\d{3}(?:-?|\s?)\d{4}$" ErrorMessage="Invalid phone number format."
            CssClass="error-message" ForeColor="Red" />
    </EditItemTemplate>
</asp:TemplateField>
        <asp:BoundField datafield="fldGender" HeaderText="Gender" ReadOnly="true"/>
        <asp:BoundField DataField="fldAccess" HeaderText="Access key" ReadOnly="true"/>
        <asp:TemplateField HeaderText="Date">
                <ItemTemplate> 
                    <asp:Label ID="date" runat="server" Text='<%#Bind("fldDate", "{0:dd/MM/yyyy}") %>'  ></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>  
                    <asp:TextBox ID="BDay" runat="server" Text='<%#Bind("fldDate", "{0:yyyy-MM-dd}") %>' TextMode="Date"></asp:TextBox>  
                </EditItemTemplate>
            </asp:TemplateField>
        <asp:commandfield ButtonType="Button" SelectText="select" ShowSelectButton="true" HeaderText="Select"/>
        <asp:commandfield ButtonType="Button" DeleteText="delete" ShowDeleteButton="true" HeaderText="Delete"/>
        <asp:CommandField ButtonType="Button" EditText="edit"  ShowEditButton="true" HeaderText="Edit"/>
    </Columns>
    
</asp:GridView>
       </div>

    </form>
    <div style="text-align:center; font-size:300%">
    <a href="Registration.aspx" style="color:black;text-decoration:none;font-family: 'Tilt Neon', cursive;">Create a new user</a>
    </div>
</div>
    
    
</asp:Content>