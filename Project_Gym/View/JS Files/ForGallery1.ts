function openImage(pic) {
    var img = document.getElementById("full")
    img.style.display = "flex"
    var forimg = (document.getElementById("fimage") as HTMLImageElement);
    forimg.src = pic
}
function closeimage() {
    var img = document.getElementById("full")
    img.style.display = "none"
}