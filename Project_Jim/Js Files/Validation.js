function checka() {
    var check = checkfi();
    var check = checkusername() && check;
    var check = pass() && check;
    var check = phone() && check;
    return check;
}
function checkfi() {
    var v = document.getElementById("first").value;
    var s = document.getElementById("first").style;
    var error = null;
    var firstl = /^[A-Z]/; //אות ראשונה חייבת להיות גדולה
    var a = /^[A-Z][a-z]{1,}$/; // מהאות השנייה, השם חייב להיות באותיות אנגליות קטנות
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
        s.borderColor = "#900C3F";
        document.getElementById("Efirst").innerHTML = error;
        document.getElementById("Efirst").style.color = "red";
        return false;
    }
    s.borderColor = "#66ff00";
    document.getElementById("Efirst").innerHTML = "";
    return true;
}
function checkusername() {
    var error = null;
    var v = document.getElementById("gainus").value, s = document.getElementById("gainus").style;
    var check = /^(?=.{1,12}$).+/; //בודק ששם המשתמש לא ריק או מעל 12 תווים
    if (check.test(v) == false)
        error = "A username cannot be longer than 12 characters or empty";
    s.border = "solid";
    s.borderWidth = "4px";
    s.borderRadius = "5px";
    if (error != null) {
        s.borderColor = "#900C3F";
        document.getElementById("Eusername").innerHTML = error;
        document.getElementById("Eusername").style.color = "red";
        return false;
    }
    s.borderColor = "#66ff00";
    document.getElementById("Eusername").innerHTML = "";
    return true;
}
function pass() {
    var error = null;
    var v = document.getElementById("pass").value, s = document.getElementById("pass").style;
    var check = /^(?=.{1,}$).*/; //בודק שהסיסמה לא ריקה
    var must = /^(?=.*[!#@%^&>{}])(?=.{1,}$).*/; //: !@%^&<}{}בודק שלסיסמה יש לפחות אחד מהתווים הבאים
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
        s.borderColor = "#900C3F";
        document.getElementById("Epass").innerHTML = error;
        document.getElementById("Epass").style.color = "red";
        return false;
    }
    s.borderColor = "#66ff00";
    document.getElementById("Epass").innerHTML = "";
    return true;
}
function phone() {
    var error = null;
    var v = document.getElementById("phone").value, s = document.getElementById("phone").style;
    var checkphone = /^(?:\+972|0)(?:-?|\s?)\d{1,2}(?:-?|\s?)\d{3}(?:-?|\s?)\d{4}$/;
    //בודק שהטלפון הוא באחד מהפורמטים הבאים:
    if (!(checkphone.test(v))) {
        error = "Your phone is not a valid israelian phone";
    }
    s.border = "solid";
    s.borderWidth = "4px";
    s.borderRadius = "5px";
    if (error != null) {
        s.borderColor = "#900C3F";
        document.getElementById("Ephone").innerHTML = error;
        document.getElementById("Ephone").style.color = "red";
        return false;
    }
    s.borderColor = "#66ff00";
    document.getElementById("Ephone").innerHTML = "";
    return true;
}
//# sourceMappingURL=Validation.js.map