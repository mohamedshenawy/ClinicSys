let rootPath = "/PatientAppointment";


let saveButton = document.querySelector("#Save");

var dateRangePickerConfig = {
    opens: 'left',
    timePicker: true,
    timePicker24Hour: true,
    timePickerIncrement: 1,
    timePickerSeconds: true,
    locale: {
        format: 'HH:mm:ss'
    }
};

$(function () {
    $('input[name="daterange"]').daterangepicker(
        dateRangePickerConfig,
        function (start, end, label) {
            console.log("A new time selection was made: " + start.format('HH:mm:ss') + ' to ' + end.format('HH:mm:ss'));
        }
    ).on('show.daterangepicker', function (ev, picker) {
        picker.container.find(".calendar-table").hide();
    });
});


saveButton.addEventListener("click", saveStage);
function saveStage(e) {
    //collect object
    let object = collectObject();
    // validate object 
    let isValid = ValidateObject(object);
    if (isValid == false) {
        return;
    }
    //ajax request
    const url = object.Id ? `${rootPath}/update` : `${rootPath}/create`;
    const options = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(object)
    };
    // Make the fetch request
    fetch(url, options)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok ' + response.statusText);
            }
            return response.json(); // Parse the JSON from the response
        })
        .then(data => {
            if (data.status == 1) {
                Swal.fire({
                    title: "Done",
                    text: data.message,
                    icon: "success"
                }).then((result) => {
                    window.location.reload();
                });

            } else if (data.status == 2) {
                let msg = ``
                let errorCount = 1;
                data.message.forEach(e => {
                    msg += `<p>${errorCount++}:${e}</p>`
                })
                Swal.fire({
                    icon: "error",
                    title: "Error",
                    text: data.message,
                }); 
            } else {
                Swal.fire({
                    icon: "error",
                    title: "Error",
                    text: data.message,
                }); 

            }
        })
        .catch(error => {
            Swal.fire({
                icon: "error",
                title: "Error",
                text: data.message,
            });
        });

}

function collectObject() {
    let obj = {};
    //main object
    let mainItems = document.querySelectorAll(".MainObj");
    mainItems.forEach(item => {
        if (item.type != "checkbox") {
            obj[`${item.id}`] = item.id == "Id" && item.value == '' ? 0 : item.value;
        } else {

            obj[`${item.id}`] = item.checked == true ? true : false;
        }
    })
    
    return obj;
}

function ValidateObject(obj) {
    let isInvalidCounter = 0;
    
    if (obj["PatientId"] == null || obj["PatientId"] <= 0) {
        isInvalidCounter++;
        displayValidationOnElement("PatientId", true);
    } else {
        displayValidationOnElement("PatientId", false);
    }
    if (obj["DoctorId"] == null || obj["DoctorId"] <= 0) {
        isInvalidCounter++;
        displayValidationOnElement("DoctorId", true);
    } else {
        displayValidationOnElement("DoctorId", false);
    }

    if (obj["WorkingDayId"] == null || obj["WorkingDayId"] <= 0) {
        isInvalidCounter++;
        displayValidationOnElement("WorkingDayId", true);
    } else {
        displayValidationOnElement("WorkingDayId", false);
    }

    if (obj["Date"] == null || obj["Date"] <= 0) {
        isInvalidCounter++;
        displayValidationOnElement("Date", true);
    } else {
        displayValidationOnElement("Date", false);
    }

    if (obj["StartDate"] == null || obj["StartDate"] <= 0 || obj["EndDate"] == null || obj["EndDate"] <= 0) {
        isInvalidCounter++;
        displayValidationOnElement("DateRange", true);
    } else {
        displayValidationOnElement("DateRange", false);
    }

    if (isInvalidCounter > 0) {
        window.scroll({
            top: 0,
            left: 0,
            behavior: 'smooth'
        });
        return false;
    } else {
        return true;
    }
}


function displayValidationOnElement(elementId, showValidation) {
    var element = document.querySelector(`#${elementId}`);
    if (element != null) {
        if (showValidation == true) {
            element.style.border = "1px solid red";
        } else {
            element.style.border = "1px solid #ccc";
        }
    }
}


function setStartDateAndEndDate(element) {
    let startDate = element.value.split('-')[0];
    let endDate = element.value.split('-')[1];
    
    document.querySelector("#StartDate").value = startDate.trim();
    document.querySelector("#EndDate").value = endDate.trim();
}


function formatDateToISO(date) {
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0'); // Months are zero-based in JS
    const day = String(date.getDate()).padStart(2, '0');
    const hours = String(date.getHours()).padStart(2, '0');
    const minutes = String(date.getMinutes()).padStart(2, '0');
    const seconds = String(date.getSeconds()).padStart(2, '0');

    return `${hours}:${minutes}:${seconds}`; // ${year}-${month}-${day}T
}




document.querySelector("#DoctorId").addEventListener("change", function () {
    let Id = this.value;
    let requestUrl = `${rootPath}/GetDoctorWarkingDays?doctorId=${Id}`;
    fetch(requestUrl)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.text();
        })
        .then(HTMLContent => {
            let template = document.querySelector("#WorkingDaysDiv");
            template.innerHTML = HTMLContent;
            template.style.display = "block";

            let doctorWorkingStartDate = template.querySelector("#DoctorWorkingStartDate").value;
            let doctorWorkingEndDate = template.querySelector("#DoctorWorkingEndDate").value;

            
            // Update dateRangePickerConfig with new min and max dates
            dateRangePickerConfig.minDate = doctorWorkingStartDate;
            dateRangePickerConfig.maxDate = doctorWorkingEndDate;

            //$(function () {
            //    $('input[name="daterange"]').daterangepicker(
            //        dateRangePickerConfig,
            //        function (start, end, label) {
            //            console.log("A new time selection was made: " + start.format('HH:mm:ss') + ' to ' + end.format('HH:mm:ss'));
            //        }
            //    ).on('show.daterangepicker', function (ev, picker) {
            //        picker.container.find(".calendar-table").hide();
            //    });
            //})

        })
        .catch(error => {
            console.error('There was a problem with the fetch operation:', error);
        });
});
