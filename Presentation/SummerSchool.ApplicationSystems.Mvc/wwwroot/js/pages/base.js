function ajaxGetWithResponse(url, data, dataType = 'json', async = false, beforeSendFunc = null, headers = null) {
    let webResponse = createResponse();

    $.ajax({
        url: url,
        type: 'GET',
        data: data,
        dataType: dataType,
        async: async,
        headers: headers,
        beforeSend: beforeSendFunc,
        success: function (response) {
            webResponse.IsSuccessful = response.isSuccessful;
            webResponse.StatusCode = response.statusCode;
            webResponse.Message = response.message;
            webResponse.Result = response.result;
        },
        error: function (xhr, status, error) {
            webResponse.StatusCode = xhr.status;
            webResponse.Message = status + ": " + xhr.statusText + " " + error;
        }
    }).fail(function (xhr, status, error) {
        webResponse.StatusCode = xhr.status;
        webResponse.Message = status + ": " + xhr.statusText + " " + error;
    });

    return webResponse;
}

function ajaxPostWithResponse(url, data, contentType = 'json', async = false, beforeSendFunc = null, headers = null) {
    let webResponse = createResponse();

    if (contentType.toLowerCase() == 'json')
        contentType = 'application/json';

    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        async: async,
        contentType: contentType,
        headers: headers,
        beforeSend: beforeSendFunc,
        success: function (response) {
            webResponse.IsSuccessful = response.isSuccessful;
            webResponse.StatusCode = response.statusCode;
            webResponse.Message = response.message;
            webResponse.Result = response.result;
        },
        error: function (xhr, status, error) {
            webResponse.StatusCode = xhr.status;
            webResponse.Message = status + ": " + xhr.statusText + " " + error;
        }
    }).fail(function (xhr, status, error) {
        webResponse.StatusCode = xhr.status;
        webResponse.Message = status + ": " + xhr.statusText + " " + error;
    });

    return webResponse;
}

function ajaxPutWithResponse(url, data, contentType = 'json', async = false, beforeSendFunc = null, headers = null) {
    let webResponse = createResponse();

    if (contentType.toLowerCase() == 'json')
        contentType = 'application/json';

    $.ajax({
        url: url,
        type: 'PUT',
        data: data,
        async: async,
        contentType: contentType,
        headers: headers,
        beforeSend: beforeSendFunc,
        success: function (response) {
            webResponse.IsSuccessful = response.isSuccessful;
            webResponse.StatusCode = response.statusCode;
            webResponse.Message = response.message;
            webResponse.Result = response.result;
        },
        error: function (xhr, status, error) {
            webResponse.StatusCode = xhr.status;
            webResponse.Message = status + ": " + xhr.statusText + " " + error;
        }
    }).fail(function (xhr, status, error) {
        webResponse.StatusCode = xhr.status;
        webResponse.Message = status + ": " + xhr.statusText + " " + error;
    });

    return webResponse;
}

function getDefaultAjaxHeaders(isUseRvtoken = true) {
    let headers = {};

    if (isUseRvtoken) {
        var token = $('input[name="__RequestVerificationToken"]').val();
        if (token)
            headers['RequestVerificationToken'] = token;
    }

    return headers;
}

function buildDataTable(dataTableId = "#dataTableColumnFilterJS", paging = true, pageSize = 10, info = false, ordering = true, columns = null, columnDefs = null, ajax = null) {
    if ($.fn.DataTable.isDataTable(dataTableId)) {
        $(dataTableId).DataTable().destroy();
        $(dataTableId + " tbody").empty();
    }


    let table = $(dataTableId).DataTable({
        dom: 't<"pagination justify-content-end"p>',
        processing: true,
        paging: paging,
        ordering: ordering,
        pageLength: pageSize,
        pagingType: 'simple_numbers',
        info: info,
        language: {
            "sEmptyTable": "Tabloda kayıt bulunamadı",
            "sSearch": "Ara:",
            "sLengthMenu": "_MENU_ kayıt göster",
            "sInfo": "_TOTAL_ kayıttan _START_ - _END_ gösteriliyor",
            "sInfoEmpty": "0 kayıttan 0 - 0 gösteriliyor",
            "sInfoFiltered": "(toplam _TOTAL_ kayıttan filtrelenmiş)",
            "sFirst": "İlk",
            "sPrevious": "Önceki",
            "sNext": "Sonraki",
            "sLast": "Son",
            "zeroRecords": "Üzgünüm, arama kriterlerinize uygun veri bulunamadı",
            "loadingRecords": "Yükleniyor...",
            "paginate": {
                "first": "İlk",
                "previous": '<span class="la la-angle-left"></span>',
                "next": '<span class="la la-angle-right"></span>',
                "last": "Son"
            },
            "aria": {
                "sortAscending": ": Aktif sıralamayı artan sıraya göre düzenle",
                "sortDescending": ": Aktif sıralamayı azalan sıraya göre düzenle"
            }
        },
        autoWidth: false,
        columns: columns,
        columnDefs: columnDefs,
        ajax: ajax
    });

    return table;
}



function messageBox(message, title) {
    setToastrOptions('success');
    toastr.success(message, title);
}

function messageBoxInfo(message, title) {
    setToastrOptions('info');
    toastr.info(message, title);
}

function messageBoxWarning(message, title) {
    setToastrOptions('warning');
    toastr.warning(message, title);
}

function messageBoxError(message, title) {
    setToastrOptions('error');
    toastr.error(message, title);
}


function setToastrOptions(toastrType) {
    let key = 'toastrsetting_' + toastrType;

    toastr.options = localStorage.getItem(key) != null ? localStorage.getItem(key) : setDefaultToastrOptions();
}

function setDefaultToastrOptions() {
    return toastrOpt = {
        "closeButton": true,
        "debug": true,
        "newestOnTop": false,
        "progressBar": true,
        "positionClass": "toast-top-full-width",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };
}


function createResponse() {
    let webResponse = new Object();
    webResponse.IsSuccessful = false;
    webResponse.StatusCode = 0;
    webResponse.Message = null;
    webResponse.Result = null;

    return webResponse;
}