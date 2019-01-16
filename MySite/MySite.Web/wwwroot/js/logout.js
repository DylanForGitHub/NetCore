

//Used to logout from the website
function logout(){
    $.ajax({
        type: "get",
        url: "/Account/LogoutCookie",
        data: null,
        success: function (data, status) {
            if (status == "success") {
                var returndata = data.status;
                if(returndata){
                    //window.location.href = "/Home/Index";
                    location.reload();
                }
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