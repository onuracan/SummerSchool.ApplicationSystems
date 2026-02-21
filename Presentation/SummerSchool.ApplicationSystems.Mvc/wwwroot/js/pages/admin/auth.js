$(document).ready(function () {
    $('#btnLogin').on('click', login);
});


function login() {
    let userName = $('#txtUserName').val();
    let password = $('#txtPassword').val();

    var response = ajaxPostWithResponse('/Admin/Auth/Login', JSON.stringify({ 'userName': userName, 'password': password }), 'json', false, null, getDefaultAjaxHeaders());
    if (response.IsSuccessful) {
        messageBox("Giriş başarılı! Yönlendiriliyorsunuz...");

        setTimeout(() => {
            window.location.replace("/Admin/Home/Index");
        }, 500);
    }
    else {
        messageBoxError(response.Message || "Giriş yapılırken bir hata oluştu.");
    }
}