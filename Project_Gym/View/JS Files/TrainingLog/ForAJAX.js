google.charts.load('current', { packages: ['corechart', 'line'] });
google.charts.setOnLoadCallback(function () {
    fetch("Training_Log.aspx/GetPlanId", {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
    })
        .then(response => response.json())
        .then(data => {
            console.log('Data received from server:', data);
            if (typeof data.d === 'number') {
                let planId = data.d;  
                drawChart(planId);   
            } else {
                console.error('Expected plan ID to be a number, but got:', data.d);
            }
        })
        .catch(error => {
            console.error('Error fetching data:', error);
        });
});
document.addEventListener("DOMContentLoaded", function () {
    // your fetch code and functions go here
    function getProgReview(planId) {
    var msgSpan = document.getElementById("progReview")
    const exerciseList = document.getElementById('exercise-list');
    const exerciseId = exerciseList.value;
    const period = document.getElementById('period').value;
    fetch("Training_Log.aspx/GetProgressionReview", {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            planId: parseInt(planId),
            period: parseInt(period),
            exerciseId: parseInt(exerciseId)
        })
    })
        .then(response => response.json())
        .then(data => {
            console.log('Data received from server for feedback:', data);
            msgSpan.innerHTML = data.d
        })
        .catch(error => {
            console.error('Error fetching data:', error);
        });
    
}

});


function drawChart(planId) {
    const exerciseList = document.getElementById('exercise-list');
    const exerciseId = exerciseList.value;
    const exerciseName = exerciseList.options[exerciseList.selectedIndex].text;
    const period = document.getElementById('period').value;

    console.log("Period:", period);
    console.log("Exercise ID:", exerciseId);
    console.log("Plan ID:", planId);

    fetch("Training_Log.aspx/GetGraphData", {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            exerciseId: parseInt(exerciseId),
            planId: parseInt(planId),
            period: parseInt(period)
        })
    })
        .then(response => response.json())
        .then(data => {
            console.log('Data received from server:', data);

            if (typeof data.d === 'string') {
                data = JSON.parse(data.d);
            }

            if (!Array.isArray(data) || data.length === 0) {
                console.error("No data available for the given parameters.");

                // Create an empty DataTable
                const chartData = google.visualization.arrayToDataTable([
                    ['Date', 'Volume'], // Column headers
                    ['', 0]            // Dummy row to create an empty chart
                ]);

                // Set options with a "No Data Found" title
                const options = {
                    title: 'No data found for exercise ' + exerciseName,
                    hAxis: { title: 'Date', textStyle: { color: '#6f4f1e' } },
                    vAxis: { title: 'Volume', textStyle: { color: '#6f4f1e' } },
                    legend: 'none',
                    backgroundColor: '#f5f1e1', // Match vintage cream color
                    chartArea: { width: '80%', height: '70%' },
                    annotations: {
                        textStyle: {
                            fontSize: 14,
                            bold: true,
                            italic: true,
                            color: '#6f4f1e' // Match dark brown color
                        }
                    }
                };

                // Draw the empty chart
                const chart = new google.visualization.LineChart(document.getElementById('myChart'));
                chart.draw(chartData, options);

                return;
            }

            // Format data for Google Charts
            const formattedData = [
                ['Date', 'Volume'],
                ...data.map(item => [item.date, item.volume])
            ];

            const chartData = google.visualization.arrayToDataTable(formattedData);

            // Identify highest and lowest points
            const volumes = data.map(item => item.volume);
            const maxVolume = Math.max(...volumes);
            const minVolume = Math.min(...volumes);
            const maxDate = data.find(item => item.volume === maxVolume).date;
            const minDate = data.find(item => item.volume === minVolume).date;

            // Chart options
            const options = {
                title: `Progression at Exercise: ${exerciseName}`,
                titleTextStyle: {
                    fontName: 'Lora', // Match font with the page
                    fontSize: 20,
                    bold: true,
                    color: '#6f4f1e', // Dark brown for title
                },
                hAxis: {
                    title: 'Date',
                    titleTextStyle: { color: '#6f4f1e', bold: true },
                    textStyle: { fontSize: 12, color: '#6f4f1e' }, // Dark brown for text
                    slantedText: true,
                    slantedTextAngle: 45,
                },
                vAxis: {
                    title: 'Volume',
                    titleTextStyle: { color: '#6f4f1e', bold: true },
                    textStyle: { fontSize: 12, color: '#6f4f1e' },
                    gridlines: { color: '#eaeaea', count: 5 },
                    viewWindowMode: 'pretty',
                },
                curveType: 'none', // Change curve type to 'none' for straight lines
                legend: { position: 'bottom', textStyle: { color: '#6f4f1e', fontSize: 12 } },
                colors: ['#a1673f'], // Warm coffee color for the line
                pointSize: 7,
                annotations: {
                    alwaysOutside: true,
                    textStyle: {
                        fontName: 'Lora', // Matching font
                        fontSize: 10,
                        bold: true,
                        color: '#6f4f1e', // Dark brown for annotations
                    },
                },
                backgroundColor: '#f5f1e1', // Vintage light cream background
                chartArea: {
                    left: 60,
                    top: 50,
                    width: '80%',
                    height: '70%',
                    backgroundColor: '#ffffff', // White background for chart area
                    borderRadius: 10, // Rounded corners for the chart area
                },
                tooltip: {
                    isHtml: true,
                    textStyle: { color: '#6f4f1e' }, // Dark brown tooltip text
                },
                animation: {
                    startup: true,
                    duration: 1500,
                    easing: 'out',
                },
            };

            // Add annotations for highest and lowest points
            formattedData.push([maxDate, maxVolume, `Highest Volume: ${maxVolume}`]);
            formattedData.push([minDate, minVolume, `Lowest Volume: ${minVolume}`]);

            // Draw the chart
            const chart = new google.visualization.LineChart(document.getElementById('myChart'));
            chart.draw(chartData, options);

            // Handle window resizing for responsiveness
            window.addEventListener('resize', () => chart.draw(chartData, options));
        })
        .catch(error => {
            console.error('Error fetching graph data:', error);
        });
    getProgReview(planId)
}

//פעולה שמוסיפה תרגיל לתצוגה של יומן האימונים
async function addExerciseBox(userId, planId) {
    // Get the template
    const template = document.getElementById('exercise-template');

    // Clone the template content
    const clone = template.content.cloneNode(true);

    // Get selected exercise details
    const selectedExerciseId = document.getElementById('exercise-list').value;
    const selectedExerciseName = document.getElementById('exercise-list').selectedOptions[0].text;
    const reps = document.getElementById('reps').value;
    const weight = document.getElementById('weight').value;

    // Set dynamic values in the cloned content
    clone.querySelector('.exercise-name').textContent = selectedExerciseName;
    clone.querySelector('.exercise-reps').textContent = reps;
    clone.querySelector('.exercise-weight').textContent = weight;

    const date = document.getElementById('date').value;

    try {
        // Await the order value from getHighestOrder
        const order = await getHighestOrder(planId, date);

        // Set dynamic onclick functions with exerciseId, planId, and date
        const editButton = clone.querySelector('.edit-btn');
        const deleteButton = clone.querySelector('.delete-btn');
        editButton.setAttribute('onclick', `editExercise(${selectedExerciseId}, ${planId}, '${date}', ${order})`);
        deleteButton.setAttribute('onclick', `deleteExercise(${selectedExerciseId}, ${planId}, '${date}', ${order})`);

        // Assign a unique id to the exercise box container
        const exerciseBox = clone.querySelector('.exercise-box');
        if (exerciseBox) {
            exerciseBox.classList.add('exercise-box'); // Ensure the shared class remains
            exerciseBox.id = `exercise-box-${order}`; // Assign a unique ID based on the order
        }

        // Append the cloned content to the exercise list container
        document.getElementById('exerciseList').appendChild(clone);

        // Save the exercise in the backend
        saveExercise(userId, planId);
    } catch (error) {
        console.error("Error getting highest order:", error);
    }
}

//קבלת המספר הסידורי הכי גבוה של תרגיל באימון ביומן האימונים בצורה א-סינכרונית
async function getHighestOrder(planId, date) {
    try {
        //קבלת המספר הסידורי הכי גבוה באימון מסוים ביומן האימונים
        const response = await fetch("Training_Log.aspx/GetMaxOrder", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                planId: parseInt(planId),
                date: date
            })
        });

        if (!response.ok) {
            throw new Error("Network response was not ok");
        }

        const data = await response.json();
        return parseInt(data.d, 10); //מחזיר את המספר הסידורי
    } catch (error) {
        console.error("Error during fetch:", error);
        // מחזיר נאל על מנת לייצג כי לא נמצא מספר סידורי, 
        //יומן האימונים ריק בתאריך המבוקש
        return null; 
    }
}


function editExercise(exerciseId, planId, date, order) {
    // Get the exercise container for the given order
    var exerciseContainer = document.getElementById(`exercise-box-${order}`);

    if (!exerciseContainer) {
        console.error("Exercise container not found for order:", order);
        return;
    }

    // Get the reps, weight, and edit button elements
    const repsElement = exerciseContainer.querySelector('.exercise-reps');
    const weightElement = exerciseContainer.querySelector('.exercise-weight');
    const editButton = exerciseContainer.querySelector('.edit-btn');

    if (!repsElement || !weightElement || !editButton) {
        if (!repsElement) console.error("Reps element not found in container:", exerciseContainer);
        if (!weightElement) console.error("Weight element not found in container:", exerciseContainer);
        if (!editButton) console.error("Edit button not found in container:", exerciseContainer);
        return;
    }

    // Get the current values of reps and weight
    const reps = repsElement.textContent || 0;
    const weight = weightElement.textContent || 0;

    // Replace the reps span with the new input structure
    const repsContainer = document.createElement('div');
    repsContainer.classList.add('reps-container');
    repsContainer.innerHTML = `
        <span class="reps-label">Reps:</span>
        <button data-action="minus" onclick="remove( 'reps',${order})">-</button>
        <input id="reps-${order}" type="number" value="${reps}" min="0" onchange="validatePositiveWeight('reps',${order} )" />
        <button data-action="plus" onclick="add( 'reps',${order})">+</button>
    `;
    repsElement.parentNode.replaceWith(repsContainer);

    // Replace the weight span with the new input structure
    const weightContainer = document.createElement('div');
    weightContainer.classList.add('weight-container');
    weightContainer.innerHTML = `
        <span class="weight-label">Weight [kg]:</span>
        <button data-action="minus" onclick="remove('weight', ${order})">-</button>
        <input id="weight-${order}" type="number" value="${weight}" min="0" step="0.1" onchange="validatePositiveWeight('weight', ${order})" />
        <button data-action="plus" onclick="add('weight', ${order})">+</button>
    `;
    weightElement.parentNode.replaceWith(weightContainer);

    // Change the edit button to "Save" and update its onclick handler using setAttribute
    editButton.textContent = "Save";
    editButton.setAttribute("onclick", `saveChanges(${exerciseId}, ${planId}, '${date}', ${order})`);
}

function saveChanges(exerciseId, planId, date, order) {
    // Get the input values for reps and weight
    var repsInput = document.getElementById(`reps-${order}`);
    var weightInput = document.getElementById(`weight-${order}`);
    var reps = repsInput.value;
    var weight = weightInput.value;
    const date2 = document.getElementById('date').value;
    console.log(reps)
    console.log(weight)

    // Send the data to the server
    fetch("Training_Log.aspx/SaveLogChanges", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            exerciseId: parseInt(exerciseId),
            planId: parseInt(planId),
            date: date2,
            order: parseInt(order),
            reps: parseInt(reps),
            weightKg: parseFloat(weight)
        })
    })
        .then(response => response.json())
        .then(data => {
            // After successfully saving, restore the original UI

            // Get the exercise container
            var exerciseContainer = document.getElementById(`exercise-box-${order}`);
            // Restore the original reps and weight structure
            exerciseContainer.innerHTML = `
            <h3 class="exercise-name">${exerciseContainer.querySelector('h3.exercise-name').textContent}</h3>
            <p>Reps: <span class="exercise-reps">${reps}</span></p>
            <p>Weight: <span class="exercise-weight">${weight}</span> kg</p>
            <div class="button-container">
                <button class="edit-btn">Edit</button>
                <button class="delete-btn">Delete</button>
            </div>
            <hr />
        `;

            // Reapply the event listeners using setAttribute for edit and delete buttons
            var editButton = exerciseContainer.querySelector('.edit-btn');
            editButton.setAttribute("onclick", `editExercise(${exerciseId}, ${planId}, '${date2}', ${order})`);

            var deleteButton = exerciseContainer.querySelector('.delete-btn');
            deleteButton.setAttribute("onclick", `deleteExercise(${exerciseId}, ${planId}, '${date2}', ${order})`);
            drawChart(planId)

        })
        .catch(error => {
            console.error("Error saving changes:", error);
        });
}

function sendEmail() {
    fetch("Training_Log.aspx/SendEmail", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            userId: parseInt(43)
        })
    })
}





function deleteExercise(exerciseId, planId, date, order) {
    const date2 = document.getElementById('date').value;
    fetch("Training_Log.aspx/DeleteLoggedExercise", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            exerciseId: parseInt(exerciseId),
            planId: parseInt(planId),
            date: date2,
            order: parseInt(order),
        })
    })
        .then(response => response.json())  // Ensure response is parsed as JSON
        .then(data => {
            var exerciseBox = document.getElementById(`exercise-box-${order}`);
            exerciseBox.remove();
            console.log(data.d);  // Log success message
            var exerciseList = document.getElementById('exerciseList')
            drawChart(planId)
        })

}



function saveExercise(userId, planId) {
    
    var date = document.getElementById('date').value;
    var selectedExercise = document.getElementById('exercise-list').value;
    var reps = document.getElementById('reps').value;
    var weight = document.getElementById('weight').value;
    fetch("Training_Log.aspx/SaveExercise", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            planId: parseInt(planId), reps: parseInt(reps)
            , weight: parseInt(weight), exerciseId: parseInt(selectedExercise)
            , userId: parseInt(userId),
            date: date
        })
    })
        .then(response => response.json())
        .then(data => {
            drawChart(planId)
        })
        .catch(error => {
            console.error("Error during fetch:", error);
        });
}

function changeLog(planId) {
    var date = document.getElementById('date').value;
    fetch("Training_Log.aspx/GetLog", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            date: date,
            planId: parseInt(planId)
        })
    })
        .then(response => response.json())  
        .then(data => {
            var htmlContent = data.d;

            var decodedHtml = decodeHtmlEntities(htmlContent);

            document.getElementById('exerciseList').innerHTML = decodedHtml;
        })
        .catch(error => {
            console.error("Error fetching log:", error);
        });
}

function decodeHtmlEntities(text) {
    var doc = new DOMParser().parseFromString(text, "text/html");
    return doc.body.innerHTML;  
}

function openFeedbackModal(userId, planId) {
    const modal = document.getElementById('feedback-modal-container');
    if (modal) {
        const selectedExercise = document.getElementById('exercise-list').value;
        const period = document.getElementById('period').value;
        const modalContent = document.querySelector('#feedback-modal .modal-content');

        // Clear the modal content while loading
        modalContent.innerHTML = "<p>Loading feedback...</p>";

        fetch("Training_Log.aspx/GetFeedbackForExercise", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                userId: parseInt(userId),
                planId: parseInt(planId),
                period: parseInt(period),
                exerciseId: parseInt(selectedExercise)
            })
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error("Failed to fetch feedback");
                }
                return response.json();
            })
            .then(data => {
                // Assuming the server returns the feedback as a string
                if (data.d) {
                    modalContent.innerHTML = data.d; // Set the content of the modal to the feedback
                } else {
                    modalContent.innerHTML = "<p>No feedback available.</p>";
                }
            })
            .catch(error => {
                modalContent.innerHTML = `<p>Error: ${error.message}</p>`;
            });

        // Add 'visible' class to show the modal  
        modal.classList.add('visible');
    }
}


function closeFeedbackModal() {
    const modal = document.getElementById('feedback-modal-container');
    if (modal) {
        modal.classList.remove('visible'); // Remove 'visible' class to hide the modal
    }
}

document.getElementById('get-feedback-btn').addEventListener('click', openFeedbackModal);

document.querySelector('#feedback-modal .close-btn').addEventListener('click', closeFeedbackModal);

document.getElementById('feedback-modal-container').addEventListener('click', function (event) {
    if (event.target === this) {
        closeFeedbackModal();
    }
});
function switchExercise(planId, currentExcId, NewExcId) {
    fetch("Training_Log.aspx/UpdatePlan", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            planId: parseInt(planId),
            exerciseId: parseInt(currentExcId),
            newExerciseId: parseInt(NewExcId)
        })
    })
        .then(response => {
            closeFeedbackModal()
            var selectedExercise = document.getElementById('exercise-list').value;
            if (!response.ok) {
                throw new Error("Failed to fetch feedback");
            }
            return response.json();
        })

    }

