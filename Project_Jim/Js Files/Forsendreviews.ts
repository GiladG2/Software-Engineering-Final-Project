function haveto(event) {
    event.preventDefault();
    alert("you have to log in to contact us")
    return false;
}
function checkall() {
    var check = checkcomment();
    var check = checktitle() && check;
    return check;
}
function checktitlenow() {
    var title = (document.getElementById("title") as HTMLInputElement).value;
    var regex = /^(?!\s*$).{1,36}$/ //בודק שהכותרת לא ריקה, או מעל 36 תווים
    var error = null;
    if (!(regex.test(title)))
        error = "your comment cannot be above 36 characters or empty, please make sure that your title is as condenced as possible"
    if (error != null) {
        document.getElementById("Etitle").innerHTML = error;
        document.getElementById("Etitle").style.color = "red";
        return false;
    }
    document.getElementById("Etitle").innerHTML = "";
    return true;
}

function checktitle() {
    var title = (document.getElementById("title") as HTMLInputElement).value;
    var regex = /^(?!\s*$).{1,36}$/
    var error = null;
    if (!(regex.test(title)))
        error = "your comment cannot be above 36 characters or empty, please make sure that your title is as condenced as possible"
    if (error != null) {
        document.getElementById("Etitle").innerHTML = error;
        document.getElementById("Etitle").style.color = "red";
        return false;
    }
    document.getElementById("Etitle").innerHTML = "";
    return true;
}
function checknow() {
    var error = null;
    
    var regex = /^[^@#$%^'*,{}\[\]]*$/;
    var comment = (document.getElementById("comment") as HTMLInputElement).value
    var s = document.getElementById("comment").style;
        if (!(regex.test(comment)))
            error = "your comment cannot have this characters @#$'%,^*{}[] "
   
    if (error != null) {
        document.getElementById("forcheck").innerHTML = error;
        document.getElementById("forcheck").style.color = "red";
        return false;
    }
    document.getElementById("forcheck").innerHTML = "";
    return true;
}
function checkcomment() {
    var error = null;
    var regex = /^[^@#$%^'*,{}\[\]]*$/; // @#$%^'*{}\ :בודק אם אחד מהתווים הבאים מופיע בתגובה
    var comment = (document.getElementById("comment") as HTMLInputElement).value
    var s = document.getElementById("comment").style;
    if (comment.length ==0) //בודק שהתגובה לא ריקה
        error = "your comment cannot be empty!";
    else {
       
            if (!(regex.test(comment)))
                error = "your comment cannot have this characters @#$%',^*{}[] "
        
    }
    if (error != null) {
        document.getElementById("forcheck").innerHTML = error;
        document.getElementById("forcheck").style.color = "red";
        return false;
    }
    document.getElementById("forcheck").innerHTML = "";
    return true;
}