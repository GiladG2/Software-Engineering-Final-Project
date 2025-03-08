
/*
google.charts.load('current', { packages: ['corechart', 'line'] });
google.charts.setOnLoadCallback(function () {
    drawChart(325, 1, 1); // Default values for exerciseId, planId, and period
});

function drawChart(planId) {
    // Define the POST request data
    exerciseId = document.getElementById('exercise-list').value
    period = 0
    console.log("period " + period)
    console.log("exercise id " + exerciseId)
    console.log("plan id" + planId)
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
            console.log('Data received from server:', data);  // Log the data for inspection

            // Check if the data is a string and parse it
            if (typeof data.d === 'string') {
                // Parse the string into a JavaScript array
                data = JSON.parse(data.d);
            }

            // Ensure that the data is an array and has valid content
            if (!Array.isArray(data) || data.length === 0) {
                console.error("No data available for the given parameters.");
                // Optionally, handle the case of no data here (e.g., display a message)
                return;
            }

            // Format the data for Google Charts
            const formattedData = [
                ['Date', 'Volume'], // Column headers
                ...data.map(item => [item.date, item.volume]) // Convert data to the correct format
            ];

            // Create the DataTable for Google Charts
            const chartData = google.visualization.arrayToDataTable(formattedData);

            // Set the chart options
            const options = {
                title: 'Progression at Exercise ' + exerciseId,
                hAxis: { title: 'Date' },
                vAxis: { title: 'Volume' },
                legend: 'none'
            };

            // Draw the chart
            const chart = new google.visualization.LineChart(document.getElementById('myChart'));
            chart.draw(chartData, options);
        })
        .catch(error => {
            console.error('Error fetching graph data:', error);
        });
}
*/