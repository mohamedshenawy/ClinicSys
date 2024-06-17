let rootPath = "/doctor";


// Initialize Select2
$(document).ready(function () {
    $(function () {
        $('input[name="daterange"]').daterangepicker({
            opens: 'left',
            timePicker: true,
            timePicker24Hour: true,
            timePickerIncrement: 1,
            timePickerSeconds: true,
            locale: {
                format: 'HH:mm:ss'
            }
        }, function (start, end, label) {
            console.log("A new time selection was made: " + start.format('HH:mm:ss') + ' to ' + end.format('HH:mm:ss'));
        }).on('show.daterangepicker', function (ev, picker) {
            picker.container.find(".calendar-table").hide();
        });;
    });

    $(document).ready(function () {
        $('.js-example-basic-multiple').select2();
    });
});
function editItem(element) {
    let parentRow = element.closest("tr");
    let obj = collectRowObject(parentRow);
    let hiddenIdInput = document.querySelector("#Id");
    hiddenIdInput.value = obj["Id"];
    let nameInput = document.querySelector("#Name");
    nameInput.value = obj["Name"];

}

function removeDataFromModal() {
    let hiddenIdInput = document.querySelector("#Id");
    hiddenIdInput.value = ""
    let nameInput = document.querySelector("#Name");
    nameInput.value = ""
}

function deleteItem(itemId) {
    Swal.fire({
        title: "Are you sure you want to delete this item?",
        showCancelButton: true,
        confirmButtonText: "confirm"
    }).then((result) => {
        /* Read more about isConfirmed, isDenied below */
        if (result.isConfirmed) {
            ConfirmDelete(itemId)
        }
    });
}
function ConfirmDelete(itemId) {
    let requestUrl = `${rootPath}/delete/${itemId}`
    fetch(requestUrl)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.text();
        })
        .then(data => {
            data = JSON.parse(data);
            if (data.status > 0) {
                Swal.fire({
                    title: "Done",
                    text: data.message,
                    icon: "success"
                }).then((result) => {
                    window.location.reload();
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


function Save(e) {
    //collect object
    let object = collectObject();
    let workingDays = collectWorkingDays();
    object["DoctorWorkingDays"] = workingDays;
    // validate object 
    let isValid = ValidateObject(object);
    if (workingDays.length <= 0) {
        isValid = false;
        $('.js-example-basic-multiple').css('border', '1px solid red');
    } else {
        isValid = true;
        $('.js-example-basic-multiple').css('border', '1px solid #CCC');
    }
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

function collectWorkingDays() {
    let lst = [];
    //main object
    let workingDaysIds = $('.js-example-basic-multiple').val();
    workingDaysIds.forEach(item => {
        lst.push({
            "WorkingDayId": item
        })
    })
    return lst;
}


function ValidateObject(obj) {
    let isInvalidCounter = 0;
    //name
    if (obj["Name"] == null || obj["Name"].length <= 0) {
        isInvalidCounter++;
        displayValidationOnElement("Name", true);
    } else {
        displayValidationOnElement("Name", false);
    }


    if (obj["ClinicId"] == null || obj["ClinicId"].length <= 0 || obj["ClinicId"] == "") {
        isInvalidCounter++;
        displayValidationOnElement("ClinicId", true);
    } else {
        displayValidationOnElement("ClinicId", false);
    }
    
    if (isInvalidCounter > 0) {
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


function collectRowObject(row) {
    let obj = {};
    //main object
    let IdInput = row.querySelector(`.Id`);
    //Id
    obj[`Id`] = IdInput.innerText;
    //Name
    let nameInput = row.querySelector(`.Name`);
    obj[`Name`] = nameInput.innerText;
    return obj;
}

function setStartDateAndEndDate(element) {
    let startDate = element.value.split('-')[0];
    let endDate = element.value.split('-')[1];

    document.querySelector("#StartDate").value = startDate
    document.querySelector("#EndDate").value = endDate;
}


