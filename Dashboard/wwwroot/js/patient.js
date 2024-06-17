let rootPath = "/patient";

function editItem(element) {
    let parentRow = element.closest("tr");
    let obj = collectRowObject(parentRow);
    let hiddenIdInput = document.querySelector("#Id");
    hiddenIdInput.value = obj["Id"];
    let nameInput = document.querySelector("#Name");
    nameInput.value = obj["Name"];
    let birthdate = document.querySelector("#BirthDate");
    birthdate.value = obj["BirthDate"];

}

function removeDataFromModal() {
    let hiddenIdInput = document.querySelector("#Id");
    hiddenIdInput.value = ""
    let nameInput = document.querySelector("#Name");
    nameInput.value = ""
    let birthdate = document.querySelector("#BirthDate");
    birthdate.value = '';
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
    //name
    if (obj["Name"] == null || obj["Name"].length <= 0) {
        isInvalidCounter++;
        displayValidationOnElement("Name", true);
    } else {
        displayValidationOnElement("Name", false);
    }

    if (obj["BirthDate"] == null || obj["BirthDate"].length <= 0) {
        isInvalidCounter++;
        displayValidationOnElement("BirthDate", true);
    } else {
        displayValidationOnElement("BirthDate", false);
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
    let birthdate = row.querySelector(`.BirthDate`);
    obj[`BirthDate`] = formatDate(birthdate.innerText);
    return obj;
}

function formatDate(date) {
    var date = new Date(date); 

    // Format the date as YYYY-MM-DD
    var year = date.getFullYear();
    var month = ('0' + (date.getMonth() + 1)).slice(-2); 
    var day = ('0' + date.getDate()).slice(-2); 

    var formattedDate = `${year}-${month}-${day}`;

    return formattedDate;
}



