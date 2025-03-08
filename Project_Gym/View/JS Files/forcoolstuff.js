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
function UnderConstruction() {
    alert("This page is under construction");
}
window.addEventListener('DOMContentLoaded', (event) => {
    // Function to update the background color based on the content
    function updateUnseenMessagesStyle(unseenMessagesElement) {
        if (unseenMessagesElement && !unseenMessagesElement.textContent.trim()) {
            unseenMessagesElement.style.backgroundColor = 'white'; // Change background to white
            unseenMessagesElement.style.color = 'white'; // Change text color to white (optional)
        } else {
            unseenMessagesElement.style.backgroundColor = '#ff4b5c'; // Original background color
            unseenMessagesElement.style.color = 'white'; // White text color
        }
    }

    // Get all elements with the 'unseenMessages' class
    var unseenMessagesElements = document.querySelectorAll('.unseenMessages');

    // Loop through each unseenMessages element and set the initial style
    unseenMessagesElements.forEach(updateUnseenMessagesStyle);

    // Create a MutationObserver to monitor changes to the content of each unseenMessages element
    const observer = new MutationObserver((mutationsList) => {
        mutationsList.forEach(mutation => {
            if (mutation.type === 'childList' || mutation.type === 'characterData') {
                updateUnseenMessagesStyle(mutation.target); // Update style of the changed element
            }
        });
    });

    // Configuration for the observer (watch for text content changes)
    const config = { childList: true, characterData: true, subtree: true };

    // Start observing each unseenMessages element
    unseenMessagesElements.forEach(element => {
        observer.observe(element, config);
    });
});
