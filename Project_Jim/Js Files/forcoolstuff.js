function ShowPassword() {
    var pass = document.getElementById("pass");
    var eye = document.getElementById("eye");
    if (pass.type === "password") {
        pass.type = "text";
        eye.innerHTML = "<i class='fa-solid fa-eye' > </i>"
    }
    else {
        pass.type = "password";
        eye.innerHTML = "<i class='fa-sharp fa-solid fa-eye-slash'></i>"
    }
    
}
function Areyousure() {
    if (confirm("Are you sure you want to delete your profile?") == true)
        window.location.replace("Delete.aspx");
}