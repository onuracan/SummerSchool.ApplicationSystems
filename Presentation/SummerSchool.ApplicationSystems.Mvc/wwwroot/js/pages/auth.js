$(document).ready(function () {
    $('#btnSendCode').on('click', sendVerificationCode);
    $('#btnLogin').on('click', login);
});

function sendVerificationCode() {
    let phoneNumber = $('#txtPhoneNumber').val();

    var response = ajaxPostWithResponse('/Auth/RequestOtp', JSON.stringify(phoneNumber), 'json', false, null, getDefaultAjaxHeaders());
    if (response.IsSuccessful) {
        messageBoxInfo("Lütfen doğrulama kodunu giriniz.");

        $('#txtVerificationCode').parent().removeClass('d-none');
        $('#btnSendCode').parent().addClass('d-none');
        $('#btnLogin').parent().removeClass('d-none');
    }
    else {
        messageBoxError(response.Message);
    }
}

function login() {
    let phoneNumber = $('#txtPhoneNumber').val();
    let verificationCode = $('#txtVerificationCode').val();

    var responseVerification = ajaxPostWithResponse('/Auth/VerifyOtp', JSON.stringify(verificationCode), 'json', false, null, getDefaultAjaxHeaders());
    if (!responseVerification.IsSuccessful) {
        messageBoxError(responseVerification.Message);
        return;
    }

    var response = ajaxPostWithResponse('/Auth/Login', JSON.stringify(phoneNumber), 'json', false, null, getDefaultAjaxHeaders());
    if (response.IsSuccessful) {
        if (!phoneNumberParam) {
            messageBox("Giriş başarılı! Yönlendiriliyorsunuz...");

            setTimeout(() => {
                window.location.replace("/Home/Index");
            }, 500);
        }
    }
    else {
        messageBoxError(response.Message || "Giriş yapılırken bir hata oluştu.");
    }
}