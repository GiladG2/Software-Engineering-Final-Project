<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage.Master" AutoEventWireup="true" CodeBehind="Home_Page.aspx.cs" Inherits="PlanDataWebService.View.Home_Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CssFiles/ForHomePage.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <main class="main-content">
        <section class="hero">
            <div class="container">
                <h2>Welcome to DB Operations</h2>
                <p>Manage exercises and muscles seamlessly with our interactive server interface.</p>
                <button class="cta-button">Get Started</button>
            </div>
        </section>

        <section id="about" class="section about">
            <div class="container">
                <h2>About Database Interactions</h2>
                <p>Our platform allows you to add, update, and manage exercises and muscles with ease. Built with performance and scalability in mind, it ensures a seamless experience for all users.</p>
            </div>
        </section>

        <section id="features" class="section features">
            <div class="container">
                <h2>Features</h2>
                <div class="feature-grid">
                    <div class="feature-item">
                        <h3>Add Data</h3>
                        <p>Easily add exercises and muscles to the system.</p>
                    </div>
                    <div class="feature-item">
                        <h3>Update Data</h3>
                        <p>Modify existing entries with our intuitive interface.</p>
                    </div>
                    <div class="feature-item">
                        <h3>Manage System</h3>
                        <p>Comprehensive tools for managing the database efficiently.</p>
                    </div>
                </div>
            </div>
        </section>
    </main>

  

</asp:Content>
