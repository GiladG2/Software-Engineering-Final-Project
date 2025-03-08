
function openModal() {
    // Send a request to the server to get the API key
    fetch("DashBoard.aspx/GetApiKey", {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({}) // Empty payload if no input is needed
    })
        .then(response => response.json())
        .then(data => {
            // Assuming the API returns the key in a property named 'd'
            const apiKey = data.d;
            document.getElementById('apiKeyText').textContent = apiKey;
            document.getElementById('apiKeyModal').style.display = 'block';
        })
        .catch(error => {
            console.error('Error fetching data:', error);
            alert("Failed to fetch API key. Please try again.");
        });
}


// Close the modal
function closeModal() {
    document.getElementById('apiKeyModal').style.display = 'none';
    document.getElementById('successPopup').classList.remove('show');  // Hide the success popup if open
}

// Copy the API key to clipboard
function copyApiKey() {
    const apiKey = document.getElementById('apiKeyText').textContent;
    const textArea = document.createElement('textarea');
    textArea.value = apiKey;
    document.body.appendChild(textArea);
    textArea.select();
    document.execCommand('copy');
    document.body.removeChild(textArea);

    // Show success popup instead of alert
    showSuccessPopup();
}

// Show success popup (message above Copy button)
// Show success popup (message above Copy button)
function showSuccessPopup() {
    const popup = document.getElementById('successPopup');
    popup.classList.add('show'); // Add the 'show' class to make it visible
    setTimeout(() => {
        popup.classList.remove('show'); // Remove the class after 2 seconds
    }, 2000);
}


function drawChart() {
    const period = document.getElementById('period').value;
    const operation = document.getElementById('operation').value;

    console.log("Selected Operation:", operation);
    console.log("Selected Period:", period);

    // Fetch the graph data from the server based on selected operation and period
    fetch("DashBoard.aspx/GetGraphData", {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            operationId: parseInt(operation),
            period: parseInt(period),
        }),
    })
        .then(response => response.json())
        .then(data => {
            console.log('Data received from server:', data);

            // Check if data is a string (handle the case where data might be wrapped in a 'd' field)
            if (typeof data.d === 'string') {
                data = JSON.parse(data.d);
            }

            // Check if data is in the expected array format
            if (!Array.isArray(data) || data.length === 0) {
                console.error("No data available for the given parameters.");

                // Create an empty DataTable
                const chartData = google.visualization.arrayToDataTable([
                    ['Date', 'Operation Count'], // Column headers
                    ['', 0]                      // Dummy row to create an empty chart
                ]);

                // Set options with a "No Data Found" title
                const options = {
                    title: 'No data found for selected operation',
                    titleTextStyle: { color: '#004080' },
                    hAxis: { title: 'Date', textStyle: { color: '#333' } },
                    vAxis: { title: 'Operation Count', textStyle: { color: '#333' } },
                    legend: 'none',
                    backgroundColor: '#f5f9ff', // Match page background color
                    chartArea: { width: '80%', height: '70%' },
                    annotations: {
                        textStyle: {
                            fontSize: 14,
                            bold: true,
                            color: '#333'
                        }
                    }
                };

                // Draw the empty chart
                const chart = new google.visualization.LineChart(document.getElementById('chart_div'));
                chart.draw(chartData, options);

                return;
            }

            // Format data for Google Charts
            const formattedData = [
                ['Date', 'Operation Count'],
                ...data.map(item => [item.date, item.value])
            ];

            // Create a DataTable
            const chartData = google.visualization.arrayToDataTable(formattedData);

            // Identify highest and lowest points (optional)
            const values = data.map(item => item.value);
            const maxValue = Math.max(...values);
            const minValue = Math.min(...values);
            const maxDate = data.find(item => item.value === maxValue).date;
            const minDate = data.find(item => item.value === minValue).date;

            // Chart options
            const options = {
                title: 'Operations Count Over Time',
                titleTextStyle: {
                    fontSize: 20,
                    fontWeight: 'bold',
                    color: '#004080', // Dark blue title color
                },
                hAxis: {
                    title: 'Date',
                    titleTextStyle: { color: '#004080', bold: true },
                    textStyle: { fontSize: 12, color: '#333' },
                    slantedText: true,
                    slantedTextAngle: 45,
                },
                vAxis: {
                    title: 'Operation Count',
                    titleTextStyle: { color: '#004080', bold: true },
                    textStyle: { fontSize: 12, color: '#333' },
                    gridlines: { color: '#eaeaea', count: 5 },
                },
                curveType: 'none', // Use straight lines for this chart
                legend: { position: 'bottom', textStyle: { color: '#004080', fontSize: 12 } },
                colors: ['#0073e6'], // Blue color for the line
                pointSize: 7,
                annotations: {
                    alwaysOutside: true,
                    textStyle: {
                        fontSize: 10,
                        bold: true,
                        color: '#333',
                    },
                },
                backgroundColor: '#f5f9ff', // Page background color
                chartArea: {
                    left: 60,
                    top: 50,
                    width: '80%',
                    height: '70%',
                    backgroundColor: '#ffffff',
                    borderRadius: 10,
                },
                tooltip: {
                    isHtml: true,
                    textStyle: { color: '#333' },
                },
                animation: {
                    startup: true,
                    duration: 1500,
                    easing: 'out',
                },
            };

            // Optional: Add annotations for highest and lowest points
            formattedData.push([maxDate, maxValue, `Highest Count: ${maxValue}`]);
            formattedData.push([minDate, minValue, `Lowest Count: ${minValue}`]);

            // Draw the chart
            const chart = new google.visualization.LineChart(document.getElementById('chart_div'));
            chart.draw(chartData, options);

            // Handle window resizing for responsiveness
            window.addEventListener('resize', () => chart.draw(chartData, options));
        })
        .catch(error => {
            console.error('Error fetching data:', error);
        });
}



// Initialize and load Google Charts
google.charts.load('current', { packages: ['corechart', 'line'] });

// Set the callback to draw the chart once Google Charts is loaded
google.charts.setOnLoadCallback(drawChart);
