// Function to open the modal
function openModal() {
    document.getElementById("termsModal").style.display = "block";
}

// Function to close the modal
function closeModal() {
    document.getElementById("termsModal").style.display = "none";
}

// Close the modal if the user clicks outside of the modal-content
window.onclick = function (event) {
    if (event.target == document.getElementById("termsModal")) {
        closeModal();
    }
}
