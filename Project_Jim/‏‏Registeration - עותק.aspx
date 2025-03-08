<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registeration.aspx.cs" Inherits="Project_Jim.Dummy" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="preconnect" href="https://fonts.googleapis.com"/>
<link rel="preconnect" href="https://fonts.gstatic.com" crossorigin/>
    <link href="CssFiles/ForLogSignEdit.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css2?family=Open+Sans:wght@500&display=swap" rel="stylesheet"/>
    <script src="https://kit.fontawesome.com/7eff39c866.js" crossorigin="anonymous"></script>
    <script src="Js%20Files/forcoolstuff.js"></script>
    <link href="CssFiles/ForEyeButton.css" rel="stylesheet" />
    <script src="Js%20Files/Validation.js"></script>
        <script src="https://kit.fontawesome.com/7eff39c866.js" crossorigin="anonymous"></script>
    <style>
        #gain{
            margin-left:450px;
            float:left;
            margin-right:300px;
            font-family: 'Open Sans', sans-serif;
        }
        .inp {padding-right:50px
        }
        h1{
            color:white;
        }
        select{
            font-size:150%
        }
        .last{
            padding-right:150px;
                        padding-left:150px;
             white-space:nowrap;
            text-align:center;
            border-radius:10px;
            color:white;
            
            font-family: 'Open Sans', sans-serif;
        }
        label{
                        font-family: 'Open Sans', sans-serif;

        }
        #submit{
            color:black;
            background-color:white;
        }
        #reset{
            background-color: black;
            color:#DCDCDC;
        }
        #forback{float:left;font-size:150%; margin-left:500px   

        }
    
   .access option:hover{
      background-color:blueviolet;
   }
   #msg{
       font-size:200%; 
       text-align:center;
       text-decoration:double;
       color:white;
       font-family: 'Open Sans', sans-serif;
   }
    </style>


</head>
<body>
   
        <div id="sign">
            <div id="msg" style="font-size:200%; text-align:center;text-decoration:double;color:white; font-family: 'Open Sans', sans-serif;">
          <%=msg %>
      </div>
            <form id="edit" name="gain" method="post" onsubmit="return checka()">
                <h1>First name</h1>
                <input class="inp" id="first" name="first" type="text" placeholder="Your first name goes here!" />
                <h4 class="eror" id="Efirst"></h4>
                <h1>Username</h1>
                <input class="inp" id="gainus" name="gainus" type="text"  placeholder="Your username goes here!"/>
                <h4 class="eror" id="Eusername"></h4>
                <h1>Password</h1>
                <input class="inp" id="pass" name="pass" type="password"  placeholder="Your password goes here!"/>
                <button onmouseup="ShowPassword()" id="eye" type="button"><i class="fa-sharp fa-solid fa-eye-slash"></i></button>
                <h4 id="Epass"></h4>
                <h1>Phone number</h1>
                <input class="inp" id="phone" name="phone" type="text" placeholder="Your phone number goes here!" />
                <h4 id="Ephone"></h4>
                <h1>Your birthday</h1>
                <input id="date" name="date" type="date" />
                <h1>Your gender</h1>
                <input id="m" name="gender" type="radio" value="male" />
                <label> Male</label>
                <input id="f" name="gender" type="radio" value="female"/>
                <label>Female</label>
                <h1>Your goal</h1>
                <input id="lose" name="goal" type="radio" value="lose"/>
                <label><strong>lose weight</strong> </label>
                <input id="gainweight" name="goal" type="radio" value="gain" />
                <label><strong>gain weight</strong></label>
                <br />

                <%
                    if(Session["fusername"]!=null)
                    {
                        Response.Write("<h1>What type of user would you like this user to be?</h1>");
                        Response.Write("<select name='access' id='access'>");
                        Response.Write("<option value='0'>Normal</option>");
                        Response.Write("<option value='7'>Admin</option>");
                        Response.Write("</select>");
                    }
                    %>
                
                    <fieldset style="color:white">
    <legend style="color:white;">what sports do you do? besides going to the gym
    </legend>

    <div style="color:white">
      <input type="checkbox" id="boxing" name="boxing"  />
      <label for="boxing">boxing</label>
    </div>

    <div>
      <input type="checkbox" id="running" name="running" />
      <label for="runing">running</label>
    </div>
     <div>
      <input type="checkbox" id="climbing" name="climbing" />
      <label for="climbing">climbing</label>
    </div>
    </fieldset>
                <input id="submit" class="last" name="submit" type="submit" value="submit" />
                <input id="reset" class="last" name="reset" type="reset" value="reset" />
            </form>
            
        </div>
      
</body>
</html>