function seer(comment,user) {
    var modal = document.getElementById("com")
    modal.style.display = "block"
    document.getElementById("display").innerHTML = comment;
    document.getElementById("header").innerHTML = user + "'s review";
    document.getElementById("header").innerHTML += "<p id='inst'>*click anywhere in the screen to close the review</p>"
}
window.onclick = function (event) {
    var modal = document.getElementById("com")
    if (event.target == modal) {
        modal.style.display = "none";
    }
}