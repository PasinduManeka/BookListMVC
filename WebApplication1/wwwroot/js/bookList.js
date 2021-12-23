var dataTable;

$(document).ready(function () {
    loadDataTable();
})

//load the table
function loadDataTable() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            //define the url to be called 
            "url": "/books/getall/",
            "type": "GET",
            "datatype": "json"
        },
        //columns that we have to display
        "columns": [
            { "data": "name", "width": "30%" },
            { "data": "author", "width": "30%" },
            { "data": "isbn", "width": "30%" },
            {
                //in last columns we display two columns
                //and need to pass the id
                "data": "id",
                "render": function (data) {
                    return `
                        <div>
                            <a href="/Books/upsert?id=${data}" class='btn btn-outline-info' style='cursor:pointer; width:100px;' >
                                Edit    
                            </a>
                            <a class='btn btn-outline-danger' style='cursor:pointer; width:100px; margin-top:5px;' 
                                onClick=Delete('/books/Delete?id=+${data}')>
                                Delete  
                            </a>
                        </div> `;
                }, "width": "30%"
            }

        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width": "100%"

    })
}

function Delete(url) {
    //Display sweet alert
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover..",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    }).then((willDelete) => {
        //user said yes
        if (willDelete) {
            //call api using ajax
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        //if success tru display the message
                        toastr.success(data.message);
                        //reload the page
                        dataTable.ajax.reload();
                    } else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });

}