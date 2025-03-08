<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Registration.aspx.cs" Inherits="Project_Gym.View.Registration" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registration</title>
    <link rel="preconnect" href="https://fonts.googleapis.com"/>
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin/>
    <link href="CssFiles/ForRegistration.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css2?family=Open+Sans:wght@500&display=swap" rel="stylesheet"/>
    <script src="https://kit.fontawesome.com/7eff39c866.js" crossorigin="anonymous"></script>
    <script src="Js Files/forcoolstuff.js"></script>
    <link href="CssFiles/ForEyeButton.css" rel="stylesheet" />
    <script src="Js Files/Validation.js"></script>
    <script src="Js Files/TermsAndConditions.js"></script>
</head>
<body>
    <div id="msg" style="font-size:200%; text-align:center; text-decoration:double; color:#0096ff; font-family: 'Open Sans', sans-serif;">
        <%= msg %>
    </div>
    <div id="container">
        <form id="edit" name="gain" method="post" onsubmit="return checka()">
            <div id="left-panel">
                <h1>About you</h1>
                <h2>First name</h2>
                <input class="inp" id="first" name="first" type="text" placeholder="Your first name goes here!" />
                <h4 class="eror" id="Efirst"></h4>
                <h2>Username</h2>
                <input class="inp" id="gainus" name="gainus" type="text" placeholder="Your username goes here!" />
                <h4 class="eror" id="Eusername"></h4>
                <h2>Password</h2>
                <input class="inp" id="pass" name="pass" type="password" placeholder="Your password goes here!" />
                <button style="color:white;" onmouseup="ShowPassword()" id="eye" type="button">
                    <i class="fa-sharp fa-solid fa-eye-slash"></i>
                </button>
                <h2>Email</h2>
<input class="inp" id="email" name="email" type="text" placeholder="Your email address goes here!" />
                <h4 id="Epass"></h4>
                <%
                    if(Session["fusername"] != null)
                    {
                        Response.Write("<label for='access'>What type of user would you like this user to be?</label>");
                        Response.Write("<select name='access' id='access'>");
                        Response.Write("<option value='0'>Normal</option>");
                        Response.Write("<option value='7'>Admin</option>");
                        Response.Write("</select>");
                    }
                %>
            </div>
            <div id="right-panel">
                <h1>General information</h1>
                <h2>Phone number</h2>
                <input class="inp" id="phone" name="phone" type="text" placeholder="Your phone number goes here!" />
                
                <h4 id="Ephone"></h4>
                <label for="date">Your birthday</label>
                <input id="date" name="date" type="date" value="<%= date %>" />
                <h2>Your gender</h2>
                <input id="m" name="gender" type="radio" value="male" />
                <label> Male</label>
                <input id="f" name="gender" type="radio" value="female"/>
                <label> Female</label>
                <h2>Your goal</h2>
                <input id="lose" name="goal" type="radio" value="lose"/>
                <label><strong> Lose weight</strong></label>
                <input id="gainweight" name="goal" type="radio" value="gain" />
                <label><strong> Gain weight</strong></label>
                <br />
                <br />
                <input type="checkbox" class="custom-checkbox" id="agree" name="agree" required="required" />
                <label for="agree">I agree to the <a id="tac" href="javascript:void(0);" onclick="openModal()">terms and conditions</a> of this site</label>
                <input type="submit" id="submit" name="submit" value="Sign in" />
            </div>
        </form>
    </div>

    <!-- Modal Structure -->
    <div id="termsModal" class="modal">
        <div class="modal-content">
            <span class="close-button" onclick="closeModal()">&times;</span>
            <h2 style="text-align:center">Terms and Conditions</h2>
            <p style="text-align:center">Terms and Conditions for Use of Training Plans
                <br />
                                <br />
                <br />

                          <p style="text-align:center">  1. Introduction
</p>
Welcome to One Life (“Site”). By accessing and using the Site, you agree to comply with and be bound by the following terms and conditions (“Terms”). If you do not agree with these Terms, please do not use the Site.
                <br />
                <br />
<p style="text-align:center">2. License Grant</p>

Upon registering or accessing the Site, you are granted a limited, non-exclusive, non-transferable, revocable license to view and use the training plans and related content provided by the Site (“Training Plans”) solely for your personal use.
                <br />
                <br />
<p style="text-align:center">3. Restriction on Sharing and Publication</p>

You agree that you will not:

Publish, distribute, or share any Training Plans provided by the Site in any form or medium, including but not limited to websites, social media, forums, or any other public or private platform.
Reproduce, modify, or create derivative works based on the Training Plans without explicit written consent from the Site.
Use the Training Plans for any commercial purpose, including selling, licensing, or otherwise exploiting the content for monetary gain.
                <br />
<br />
<p style="text-align:center">4. Intellectual Property Rights</p>

All Training Plans and related content are the intellectual property of One Life and are protected by copyright, trademark, and other applicable laws. Your use of the Training Plans is subject to these Terms and does not grant you any ownership rights to the content.
                <br />
                <br />
<p style="text-align:center">5. User Responsibility</p>

You are responsible for ensuring that any use of the Training Plans complies with these Terms. Any unauthorized use or distribution of the Training Plans may result in legal action and/or termination of your access to the Site.
                <br />
                <br />
<p style="text-align:center">6. Termination</p>

One Life reserves the right to terminate or suspend your access to the Site and its services if you breach any of these Terms, including the restrictions on publishing or sharing the Training Plans.
                <br />                <br />

<p style="text-align:center">7. Changes to Terms</p>

The Site may update these Terms from time to time. Your continued use of the Site following any changes constitutes your acceptance of the new Terms. It is your responsibility to review these Terms periodically.
                <br />
                <br />
<p style="text-align:center">8. Contact Information</p>

If you have any questions about these Terms or need further information, please contact us at OneLifeHelp@gmail.com.
                <br />
                <br />
<p style="text-align:center">9. Governing Law</p> 

These Terms are governed by and construed in accordance with the laws of the State of Israel, without regard to its conflict of law principles.

.</p>
        </div>
    </div>
</body>
</html>
