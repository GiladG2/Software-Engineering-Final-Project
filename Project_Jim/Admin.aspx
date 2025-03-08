<%@ Page Title="" Language="C#" MasterPageFile="~/forgeneral.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="Project_Jim.Admin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link rel="preconnect" href="https://fonts.googleapis.com">
<link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
<link href="https://fonts.googleapis.com/css2?family=Tilt+Neon&display=swap" rel="stylesheet">
    <style>
        body{
            background-color:#c0c0c0;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
<asp:GridView  ID="GridView1" HorizontalAlign="Center" runat="server" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" AutoGenerateColumns="false" AllowSorting="true"
     OnRowDeleting="GridView1_RowDeleting" AllowPaging="true" PageSize="10" OnPageIndexChanging="GridView1_PageIndexChanging"  OnRowEditing="GridView1_RowEditing" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowUpdating="GridView1_RowUpdating" OnSorting="GridView1_Sorting">
    <FooterStyle BackColor="#CCCC99" />
    <HeaderStyle BackColor="#000000" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
    <RowStyle BackColor="#c0c0c0" ForeColor="#ffffff"  />
    <SelectedRowStyle BackColor="#5F4C4C" Font-Bold="True" ForeColor="White" />
    <SortedAscendingCellStyle BackColor="#FBFBF2" />
    <SortedAscendingHeaderStyle BackColor="#848384" />
    <SortedDescendingCellStyle BackColor="#EAEAD3" />
    <SortedDescendingHeaderStyle BackColor="#575357" />
    <Columns>
        <asp:BoundField DataField="fldUsername" HeaderText="Username" ReadOnly="true" SortExpression="fldUsername"/>
        <asp:BoundField  DataField="fldPassowrd" HeaderText="Password" />
        <asp:BoundField DataField="fldFirstBane" HeaderText="Name" />
        <asp:BoundField DataField="fldPhone" HeaderText="Phone" />
        <asp:BoundField datafield="fldGendetr" HeaderText="Gender"/>
        <asp:BoundField DataField="fldAccess" HeaderText="Access key"/>
        <asp:TemplateField HeaderText="Date">
                <ItemTemplate> 
                    <asp:Label ID="date" runat="server" Text='<%#Bind("fldDate", "{0:dd/MM/yyyy}") %>'  ></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>  
                    <asp:TextBox ID="BDay" runat="server" Text='<%#Bind("fldDate", "{0:yyyy-MM-dd}") %>' TextMode="Date"></asp:TextBox>  
                </EditItemTemplate>
            </asp:TemplateField>
        <asp:commandfield ButtonType="Button" ShowSelectButton="true" HeaderText="Select"/>
        <asp:commandfield ButtonType="Button" ShowDeleteButton="true" HeaderText="Delete"/>
        <asp:CommandField ButtonType="Button"  ShowEditButton="true" HeaderText="Edit"/>
    </Columns>
    
</asp:GridView>
    </form>
    <div style="text-align:center; font-size:300%">
    <a href="Registeration.aspx" style="color:black;text-decoration:none;font-family: 'Tilt Neon', cursive;">Create a new user</a>
    </div>
    
</asp:Content>
