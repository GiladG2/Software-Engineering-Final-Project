document.addEventListener('DOMContentLoaded', function () {
    const steps = document.querySelectorAll('.step');
    const progressSteps = document.querySelectorAll('.progress-step');
    const nextButton = document.getElementById('next-button');
    const prevButton = document.getElementById('prev-button');
    
    let currentStep = 0;

    function updateSteps() {
        steps.forEach((step, index) => {
            step.style.display = index === currentStep ? 'block' : 'none';
        });

        progressSteps.forEach((step, index) => {
            step.classList.toggle('active', index <= currentStep);
        });

        prevButton.disabled = currentStep === 0;
        nextButton.disabled = currentStep === steps.length - 1;
    }

    nextButton.addEventListener('click', function () {
        if (currentStep < steps.length - 1) {
            currentStep++;
            updateSteps();
        }
    });

    prevButton.addEventListener('click', function () {
        if (currentStep > 0) {
            currentStep--;
            updateSteps();
        }
    });

    updateSteps(); // Initialize the form on page load
});
//popup
