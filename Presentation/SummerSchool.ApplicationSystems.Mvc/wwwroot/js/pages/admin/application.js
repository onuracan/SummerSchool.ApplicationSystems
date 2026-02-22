$(document).ready(function () {
    clickgetCourseQuotaList();
    $('#btnSearch').on('click', function () {
        buildDataTable('#dtApplications', true, 10, false, true, myApplicationsGridColumns, null, getApplications);
    });
});

function acceptOrRejectApplication(id, applicationStatus) {
    const url = `/Admin/Application/UpdateApplicationStatus/${id}`;
    const requestData = {
        applicationStatus: applicationStatus,
    };

    const response = ajaxPutWithResponse(url, JSON.stringify(requestData), 'json', false, null, getDefaultAjaxHeaders());
    if (response.IsSuccessful) {
        messageBox('Başvuru durumu başarıyla güncellendi.');
        $('#btnSearch').trigger('click');
        clickgetCourseQuotaList();
    }
    else {
        messageBoxError(response.Message);
    }
}

function getCourses(data, callback, settings) {
    var response = ajaxGetWithResponse('/Admin/Course/GetCourses', null, 'json', false, null, getDefaultAjaxHeaders(false));

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
function getApplications(data, callback, settings) {
    let response = createResponse();
    let courseId = $('#drpCourse').val();

    if (courseId === "") {
        messageBoxError("Lütfen ders seçimi yapınız.");
    }
    else {
        const params = new URLSearchParams({ courseId: courseId });
        const url = `/Admin/CourseApplication/GetApplicationByCourseId?${params.toString()}`;

        response = ajaxGetWithResponse(url, null, 'json', false, null, getDefaultAjaxHeaders(false));

        if (!response.IsSuccessful) {
            if (response.StatusCode == 204)
                messageBoxWarning(response.Message);
            else if (response.StatusCode == 500)
                messageBoxError(response.Message);
        }
    }

    callback({
        data: response.IsSuccessful ? response.Result : []
    });
}

let courseGridColumns = [
    { data: 'faculty' },
    { data: 'department' },
    {
        data: null,
        render: function (data, type, row) {
            return row.code + ' - ' + row.name;
        }
    },
    { data: 'quota' },
    { data: 'applicationCount' },
];

let myApplicationsGridColumns = [
    {
        data: null,
        render: function (data, type, row) {
            let val = '-';

            if (row.applicationStatus == 1) { //1- Başvuruldu statüsü
                val = '<button class="btn btn-sm btn-primary dim" type="button" onclick="acceptOrRejectApplication(\'' + row.id + '\', 2)"><i class="fa fa-check"></i></button>' +
                    '<button class="btn btn-sm btn-danger dim" type="button" onclick="acceptOrRejectApplication(\'' + row.id + '\', 3)"><i class="fa fa-remove"></i></button>';
            }

            return val;
        }
    },
    { data: 'studentInfo' },
    { data: 'courseInfo' },
    { data: 'applicationStatusInfo' },
    { data: 'updatedUser' },
    {
        data: null,
        render: function (data, type, row) {
            return formatDate(row.updatedDate);
        }
    }
];

function clickgetCourseQuotaList() {
    buildDataTable('#dtCourseQuota', true, 10, false, true, courseGridColumns, null, getCourses);
}
