function seer(comment, user, title, id, filePath) {
    var modal = document.getElementById("com");
    modal.style.display = "block";

    // Set the header text based on user
    if (window.location.href === "https://localhost:44345/View/Mail.aspx")
        document.getElementById("header").innerHTML = user + "'s response";
    else
        document.getElementById("header").innerHTML = user + "'s review";

    document.getElementById("header").innerHTML += "<p id='inst'>*click anywhere in the screen to close the review</p>";

    // Set the username and title in the hidden input fields
    var usernameInput = document.getElementById("usernameToFind");
    usernameInput.value = user;
    var display = document.getElementById("display")
    display.innerHTML = comment
    var titleToInsert = document.getElementById("titleToSend");
    titleToInsert.value = title;
    console.log("id:" + id + " filepath:" + filePath)
    var status = document.getElementById(id + "status")
    ChangeUnseen()
    if (status.innerHTML != "No Status") {
    status.innerHTML = "Seen"
    }
    if (filePath.includes('responses')) {
        SaveSeenMessageMail(id, filePath)
    }
    if (filePath.includes('eview')) {
        SaveSeenMessageAdminReviews(id,filePath)
    }

}
function ChangeUnseen() {
    var unseenElements = document.getElementsByClassName("unseenMessages");

    for (var i = 0; i < unseenElements.length; i++) {
        var unseenCount = parseInt(unseenElements[i].innerHTML, 10);  

        if (!isNaN(unseenCount)) {
            unseenElements[i].innerHTML = unseenCount - 1; 
            if (unseenElements[i].innerHTML == "0")
                unseenElements[i].innerHTML = ""
        }
    }
}
function SaveSeenMessageMail(id, filePath) {
    fetch("Mail.aspx/UpdateStatus", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({ id: parseInt(id), filePath: filePath })
    })
        .then(response => response.json())
        .then(data => {
            console.log(data.d); // The Web Method response is in `data.d`
            if (data.d === "Status updated successfully.") {
                var status = document.getElementById(id + "status");
                if (status) {
                    status.innerHTML = "Seen";
                }
            } else {
                console.error(data.d);
            }
        })
        .catch(error => {
            console.error("Error during fetch:", error);
        });
}
function SaveSeenMessageAdminReviews(id, filePath) {
    fetch("SeeReviews.aspx/UpdateStatus", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({ id: parseInt(id), filePath: filePath })
    })
        .then(response => response.json())
        .then(data => {
            console.log(data.d); // The Web Method response is in `data.d`
            if (data.d === "Status updated successfully.") {
                var status = document.getElementById(id + "status");
                if (status) {
                    status.innerHTML = "Seen";
                }
            } else {
                console.error(data.d);
            }
        })
        .catch(error => {
            console.error("Error during fetch:", error);
        });
}
window.onclick = function (event) {
    var modal = document.getElementById("com");
    if (event.target == modal) {
        modal.style.display = "none";
        document.getElementById("respond").style.display = "none";
    }
}

function DisplayText() {
    // Show the response section
    document.getElementById("respond").style.display = "block";

    // Get the input field and the username
    var inp = document.getElementsByClassName("usernameToFind");
    var username = document.getElementById("username").innerHTML;

    // Set the value of the hidden input field to the username
    inp.value = username; // Correctly assign the username to the input's value
}
