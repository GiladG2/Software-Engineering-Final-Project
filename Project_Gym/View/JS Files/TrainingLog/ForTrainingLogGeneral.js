
function showModal() {
    var modal = document.getElementById("modalContainer");
    modal.style.display = "block";
}

window.onclick = function (event) {
    var modal = document.getElementById("modalContainer");
    if (event.target == modal) {
        modal.style.display = "none";
    }
}
// Function to update the input value (either weight or reps)
// Update the value of the input field dynamically
function updateValue(inputId, changeAmount) {
    console.log(inputId)
    const input = document.getElementById(inputId);
    const currentValue = parseFloat(input.value) || 0; // Ensure the value is a valid number
    const newValue = currentValue + changeAmount;

    // Update value, ensuring it doesn't go below 0
    input.value = Math.max(newValue, 0).toFixed(1);
}

// Function to handle removing values dynamically
function remove(type, order) {
    const id = order === 0 ? type : `${type}-${order}`;
    const changeAmount = type === 'weight' ? -2.5 : -1; // Adjust step for weight or reps
    updateValue(id, changeAmount);
}

// Function to handle adding values dynamically
function add(type, order) {
    const id = order === 0 ? type : `${type}-${order}`;
    const changeAmount = type === 'weight' ? 2.5 : 1; // Adjust step for weight or reps
    updateValue(id, changeAmount);
}

// Function to validate input and ensure positive values
function validatePositiveWeight(type,order) {
    const id = order === 0 ? type : `${type}-${order}`;
    const input = document.getElementById(id);
    const currentValue = parseFloat(input.value);

    // Reset to 0 if the value is negative
    if (currentValue < 0 || isNaN(currentValue)) {
        input.value = 0;
    }
}




