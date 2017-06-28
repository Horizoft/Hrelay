function CRUD(viewModel, serviceUrl, type) {
    var resultData;
    var resultError;
    var returnObject = {};
    var params = {};
    var urlService = serviceUrl;
    var data = JSON.stringify(viewModel);
    $.ajax({
        url: urlService,
        type: type,  //type = 'GET','POST','DELETE'
        //data: (type.toUpperCase() == 'GET') ? JSON.stringify(params) : viewModel,
        data: (type.toUpperCase() == 'GET') ? "" : data,
        //data: data,
        dataType: 'json',
        cache: false,
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            resultData = result;
        },
        error: function (jqXHR, textStatus) {
            resultError = jqXHR.statusText;
        }
    });

    returnObject = {
        data: resultData,
        error: resultError
    };

    return returnObject;
}

function getQueryStringByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

function getQueryStringFromHrefByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.href);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

function sortResults(JSONData, prop, asc) { //xyz2  //prop: Property
    JSONData = JSONData.sort(function (a, b) {
        if (asc) return (a[prop] > b[prop]) ? 1 : ((a[prop] < b[prop]) ? -1 : 0);
        else return (b[prop] > a[prop]) ? 1 : ((b[prop] < a[prop]) ? -1 : 0);
    });

}