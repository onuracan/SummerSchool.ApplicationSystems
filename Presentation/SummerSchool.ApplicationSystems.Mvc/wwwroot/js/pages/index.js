$(document).ready(function () {
    $('#btnSave').on('click', savePersonelInformation);
    $('#btnNewApplication').on('click', function () {
        buildDataTable('#dtCourse', true, 10, false, true, courseGridColumns, null, getCourses);
    });
    $('#btnMyApplications').on('click', function () {
        buildDataTable('#dtMyApplications', true, 10, false, true, myApplicationsGridColumns, null, getMyApplications);
    });
});

function savePersonelInformation() {
    let studentId = $('#hdnStudentId').val();
    let firstName = $('#txtFirstName').val();
    let lastName = $('#txtLastName').val();
    let identityNumber = $('#txtIdentityNumber').val();
    let schoolNumber = $('#txtSchoolNumber').val();
    let department = $('#txtDepartment').val();
    let faculty = $('#txtFaculty').val();
    let phoneNumber = $('#txtPhoneNumber').val();
    let eMail = $('#txtEMail').val();
    let countryCode = $('#drpCountryCode').val();

    if (!firstName || !lastName || !identityNumber || !schoolNumber || !department ||
        !faculty || !phoneNumber || !eMail || !countryCode) {
        messageBoxWarning("Lütfen tüm alanları doldurunuz.");
        return;
    }

    let isNewStudent = !studentId || studentId === '00000000-0000-0000-0000-000000000000';

    let requestData;
    let apiUrl;
    let response = createResponse;

    if (isNewStudent) {
        requestData = {
            firstName: firstName,
            lastName: lastName,
            identityNumber: identityNumber,
            schoolNumber: schoolNumber,
            department: department,
            faculty: faculty,
            phoneNumber: phoneNumber,
            eMail: eMail,
            countryCode: countryCode
        };

        apiUrl = '/Student/Create';

        response = ajaxPostWithResponse(apiUrl, JSON.stringify(requestData), 'json', false, null, getDefaultAjaxHeaders());

        if (response.IsSuccessful) {
            messageBox("Kişisel bilgileriniz başarıyla kaydedildi!");
        } else {
            messageBoxError(response.Message || "Bilgiler kaydedilirken bir hata oluştu.");
        }
    } else {
        requestData = {
            id: studentId,
            firstName: firstName,
            lastName: lastName,
            identityNumber: identityNumber,
            schoolNumber: schoolNumber,
            department: department,
            faculty: faculty,
            phoneNumber: phoneNumber,
            eMail: eMail,
            countryCode: countryCode
        };

        apiUrl = '/Student/Update/' + studentId;

        response = ajaxPutWithResponse(apiUrl, JSON.stringify(requestData), 'json', false, null, getDefaultAjaxHeaders());

        if (response.IsSuccessful) {
            messageBox("Kişisel bilgileriniz başarıyla güncellendi!");
        } else {
            messageBoxError(response.Message || "Bilgiler güncellenirken bir hata oluştu.");
        }
    }
}

function applyCourse(courseId) {
    apiUrl = '/CourseApplication/Apply';

    response = ajaxPostWithResponse(apiUrl, JSON.stringify(courseId), 'json', false, null, getDefaultAjaxHeaders());

    if (response.IsSuccessful) {
        messageBox("Başvurunuz onay sürecine gönderildi.");
        buildDataTable('#dtCourse', true, 10, false, true, courseGridColumns, null, getCourses);
    }
    else {
        messageBoxError(response.Message || "Başvuru işleminde bir hata oluştu.");
    }
}

function getCourses(data, callback, settings) {
    var response = ajaxGetWithResponse('/Course/GetCourses', null, 'json', false, null, getDefaultAjaxHeaders(false));

    if (!response.IsSuccessful) {
        if (response.StatusCode == 204)
            messageBoxWarning(response.Message);
        else
            messageBoxError(response.Message);
    }

    callback({
        data: response.IsSuccessful ? response.Result : []
    });
}

function getMyApplications(data, callback, settings) {
    var response = ajaxGetWithResponse('/CourseApplication/GetMeApplications', null, 'json', false, null, getDefaultAjaxHeaders(false));

    if (!response.IsSuccessful) {
        if (response.StatusCode == 204)
            messageBoxWarning(response.Message);
        else
            messageBoxError(response.Message);
    }

    callback({
        data: response.IsSuccessful ? response.Result : []
    });
}

let courseGridColumns = [
    {
        data: null,
        render: function (data, type, row) {
            return row.code + ' - ' + row.name;
        }
    },
    { data: 'quota' },
    { data: 'applicationCount' },
    {
        data: null,
        defaultContent: '-',
        render: function (data, type, row) {
            let val = '';
            if (row.quota == row.applicationCount)
                val = '<label class="text-danger">Kontenjan Dolu</label>';
            else if (row.canBeApply)
                val = '<label class="text-danger">Başvurdunuz</label>';
            else
                val = '<button class="btn btn-primary" onclick="applyCourse(\'' + row.id + '\')">Başvur</button>';

            return val;
        }
    },
];

let myApplicationsGridColumns = [
    { data: 'courseInfo'},
    { data: 'applicationStatusInfo' },
    { data: 'applicationStatusDescription' }
];
