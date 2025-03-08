function checkal() {
    var check = checkfirstname();
    check = password() && check;
    check = phonenumber() && check;
    return check;
    function checkfirstname() {
        var v = document.getElementById("first").value;
        var s = document.getElementById("first").style;
        var error = null;
        var firstl = /^[A-Z]/;
        var a = /^[A-Z][a-z]{1,}$/;
        if (!(firstl.test(v))) {
            error = "Your name must start with an English uppercase letter and it cannot be empty!";
        }
        else {
            if (!(a.test(v)))
                error = "Your name from the second letter on should be lowercase english letter";
        }
        s.border = "solid";
        s.borderWidth = "4px";
        s.borderRadius = "5px";
        if (error != null) {
            s.borderColor = "#ffcc00";
            document.getElementById("Efirst").innerHTML = error;
            document.getElementById("Efirst").style.color = "#ffcc00";
            return false;
        }
        s.borderColor = "#66ff00";
        document.getElementById("Efirst").innerHTML = "";
        return true;
    }
    function password() {
        var error = null;
        var v = document.getElementById("pass").value, s = document.getElementById("pass").style;
        var check = /^(?=.{1,}$).*/;
        var must = /^(?=.*[!#@%^&>{}])(?=.{1,}$).*/;
        if (check.test(v) == false)
            error = "A password cannot be empty";
        else {
            if (!(must.test(v)))
                error = "Your password must have at least one of those characters !#@%^&>{}";
        }
        s.border = "solid";
        s.borderWidth = "4px";
        s.borderRadius = "5px";
        if (error != null) {
            s.borderColor = "#ffcc00";
            document.getElementById("Epass").innerHTML = error;
            document.getElementById("Epass").style.color = "#ffcc00";
            return false;
        }
        s.borderColor = "#66ff00";
        document.getElementById("Epass").innerHTML = "";
        return true;
    }
}
function phonenumber() {
    var error = null;
    var v = document.getElementById("phone").value, s = document.getElementById("phone").style;
    var checkphone = /^(?:\+972|0)(?:-?|\s?)\d{1,2}(?:-?|\s?)\d{3}(?:-?|\s?)\d{4}$/;
    if (!(checkphone.test(v))) {
        error = "Your phone is not a valid israelian phone";
    }
    s.border = "solid";
    s.borderWidth = "4px";
    s.borderRadius = "5px";
    if (error != null) {
        s.borderColor = "#ff9900";
        document.getElementById("Ephone").innerHTML = error;
        document.getElementById("Ephone").style.color = "#ff9900";
        return false;
    }
    s.borderColor = "#66ff00";
    document.getElementById("Ephone").innerHTML = "";
    return true;
}
//# sourceMappingURL=EditValidation.js.map