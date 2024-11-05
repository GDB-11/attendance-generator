document.addEventListener('DOMContentLoaded', (event) => {
    const fieldset = document.querySelector('fieldset');
    const radioButtons = fieldset.querySelectorAll('input[type="radio"]');
    const secondFileDiv = document.getElementById('second-file');

    // Save the initial content of the div
    const originalContent = secondFileDiv.innerHTML;

    // Function to get the selected value
    function getSelectedValue() {
        const selectedRadio = fieldset.querySelector('input[type="radio"]:checked');
        return selectedRadio ? selectedRadio.value : null;
    }

    // Function to update the div content based on selected value
    function updateDivContent() {
        const selectedValue = getSelectedValue();
        if (selectedValue === 'Normal') {
            secondFileDiv.innerHTML = ''; // Clear the content
        } else if (selectedValue === 'Lego') {
            secondFileDiv.innerHTML = originalContent; // Restore the content
        }
    }

    // Event listener for change event
    radioButtons.forEach(radio => {
        radio.addEventListener('change', updateDivContent);
    });

    // Initial check to set the correct content on load
    updateDivContent();
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    const numSessionsSelect = document.getElementById('num-sessions');
    const datesSessionsDiv = document.getElementById('dates-sessions');

    numSessionsSelect.addEventListener('change', updateDatePickers);

    function updateDatePickers() {
        const today = new Date();
        const minDate = today.toISOString().split('T')[0];

        let numSessions = parseInt(numSessionsSelect.value);

        datesSessionsDiv.innerHTML = '<div name="dates-container" class="flex flex-wrap gap-4"></div>';
        const datesContainer = document.getElementsByName('dates-container')[0];

        for (let i = 0; i < numSessions; i++) {
            const dateContainer = document.createElement('div');

            if (numSessions === 2) {
                dateContainer.className = 'flex-1 min-w-[20%]';
            } else {
                dateContainer.className = 'flex-1 min-w-[45%] md:min-w-[22%] 2xl:min-w-[10%]';
            }

            const label = document.createElement('label');
            label.className = 'block mb-2 text-sm font-medium text-gray-900 dark:text-gray-300';
            label.textContent = `Clase ${i + 1}`;
            const datePicker = document.createElement('input');
            datePicker.type = 'date';
            datePicker.min = minDate;
            datePicker.className = 'block w-full mb-3 px-3 py-2 rounded-lg border border-gray-300 bg-white text-base text-gray-900 placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-sky-300 focus:border-sky-300 dark:bg-gray-700 dark:text-gray-300 dark:placeholder-gray-500 dark:border-gray-600 dark:focus:ring-sky-600 dark:focus:border-sky-600';
            dateContainer.appendChild(label);
            dateContainer.appendChild(datePicker);
            datesContainer.appendChild(dateContainer);
        }
    }

    updateDatePickers();
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    const studentsStatusSection = document.getElementById('students-status-section');

    // Save the initial content of the div
    const studentsStatusSectionContent = studentsStatusSection.innerHTML;
});