let rootPath = "/PatientAppointment";


$(document).ready(function () {
    $(function () {
        $('input[name="daterange"]').daterangepicker({
            opens: 'left',
            //startDate: document.querySelector("#StratDate").value,
            //endDate: document.querySelector("#EndDate").value,
            locale: {
                format: 'YYYY/MM/DD'
            }
        }, function (start, end, label) {
        }).on('show.daterangepicker', function (ev, picker) {
            picker.container.find(".calendar-table").show();
        });
        $('input[name="daterange"]').val('');
        $('#clear-daterange').on('click', function () {
            $('input[name="daterange"]').val('');
            document.querySelector("#StartDate").value = ""
            document.querySelector("#EndDate").value = ""
        });
    });

    //$(document).ready(function () {
    //    $('.js-example-basic-multiple').select2();
    //});
});


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
                    filter()
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



function setStartDateAndEndDate(element) {
    let startDate = element.value.split('-')[0];
    let endDate = element.value.split('-')[1];

    document.querySelector("#StartDate").value = startDate
    document.querySelector("#EndDate").value = endDate;
}

//////// Start Table Manager
let table = $('#myTable').DataTable({
    bLengthChange: false,
    processing: true,
    serverSide: true,
    iDisplayLength: 20,
    searching: false,
    ordering: false,
    ajax: {
        url: `${rootPath}/GetDataManager`,
        "type": "POST",
        "contentType": "application/json",
        "data": function (d) {
            d.criteria = JSON.stringify(getSearchObject());
            return JSON.stringify(d);
        },
        dataSrc: 'tableData.data'
    },
    language: {
        'paginate': {
            'previous': 'Prev',
            'next': 'Next'
        },
        //"info": "Showing _START_ to _END_ of _TOTAL_ entries",
        "info": "Showing _START_ to _END_ of _TOTAL_ entries",
        "infoEmpty": "infoEmpty",
        "loadingRecords": "loadingRecords",
        "processing": "processing",
        "zeroRecords": "NoRecords",
        "emptyTable": "NoRecords",
    },
    columns:
        [
            {
                className: "text-center",
                data: function (row, type, set) {
                    try {
                        return row.id;
                    } catch {
                        return "";
                    }
                }
            },
            {
                className: "text-center",
                data: function (row, type, set) {
                    try {
                        return row.patientName;
                    } catch {
                        return "";
                    }
                }
            },
            {
                className: "text-center",
                data: function (row, type, set) {
                    try {
                        return row.doctorName;
                    } catch {
                        return "";
                    }
                }
            },
            {
                className: "text-center",
                data: function (row, type, set) {
                    try {
                        return row.workingDayName;
                    } catch {
                        return "";
                    }
                }
            },
            {
                className: "text-center",
                data: function (row, type, set) {
                    try {
                        let formatedDate = row.date != null ? formatDate(row.date.toString(), "yyyy-MM-dd") : "";
                        return formatedDate;
                    } catch {
                        return "";
                    }
                }
            },
            {
                className: "text-center",
                data: function (row, type, set) {
                    try {
                        //let formatedDate = row.startDate != null ? formatDate(row.startDate.toString(), "yyyy-MM-dd") : "";
                        return row.startDate;
                    } catch {
                        return "";
                    }
                }
            },
            {
                className: "text-center",
                data: function (row, type, set) {
                    try {
                        //let formatedDate = row.endDate != null ? formatDate(row.endDate.toString(), "yyyy-MM-dd") : "";
                        return row.endDate;
                    } catch {
                        return "";
                    }
                }
            },
            {
                className: "text-center",
                data: function (row, type, set) {
                    try {
                        let formatedDate = row.creationDate != null ? formatDate(row.creationDate.toString(), "yyyy-MM-dd") : "";
                        return formatedDate;
                    } catch {
                        return "";
                    }
                }
            },
            {
                className: "text-center",
                data: function (row, type, set) {
                    try {
                        let formatedDate = row.updatedDate != null ? formatDate(row.updatedDate.toString(), "yyyy-MM-dd") : "";
                        return formatedDate;
                    } catch {
                        return "";
                    }
                }
            },
            {
                className: "text-center",
                data: function (row, type, set) {
                    try {
                        return `
                            <a type="button" class="btn btn-light" href="/PatientAppointment/add/${row.id}">
                            <i class="fa-solid fa-pen-to-square"></i>
                            </a>
                            <button type="button" class="btn btn-danger" onclick="deleteItem(${row.id})">
                            <i class="fa-solid fa-trash"></i>
                            </button>
                        `;
                    } catch {
                        return "";
                    }
                }
            }

        ]

});

function filter() {

    //var SearchObject = new Object();
    //SearchObject = getSearchObject();
    //table.search(SearchObject).draw();
    table.search(getSearchObject()).draw();
    //table.ajax.reload();

}

function getSearchObject() {
    let obj = {};
    //main object
    let mainItems = document.querySelectorAll(".searchcriteria");
    mainItems.forEach(item => {
        obj[`${item.id}`] = item.id == "StartDate" || item.id == "EndDate" ? formatDate(item.value) : item.value;
    })

    return obj;
}

function formatDate(dateString) {
    if (dateString == "") {
        return "";
    }
    const date = new Date(dateString);
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0'); // Months are zero-based in JS
    const day = String(date.getDate()).padStart(2, '0');
    const hours = String(date.getHours()).padStart(2, '0');
    const minutes = String(date.getMinutes()).padStart(2, '0');
    const seconds = String(date.getSeconds()).padStart(2, '0');

    return ` ${year}-${month}-${day}`; // ${year}-${month}-${day}T
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


