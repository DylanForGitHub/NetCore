
function showDetail(id){
    //alert(id);
    $.ajax({
                type: "post",
                url: "/Web/API/GetDetails",
                data: { "ID": id },
                success: function (data, status) {
                    if (status == "success") {
                        //$("#myModalLabel").text("新增");
                        //alert(data.id);
                        $('#txt_id').val(data.id);
                        $('#txt_name').val(data.name);
                        $('#txt_department').val(data.department);
                        $('#myModal').modal();
                    }
                },
                error: function () {
                    //toastr.error('Error');
                    alert("failed...")
                },
                complete: function () {

                }

        });
}