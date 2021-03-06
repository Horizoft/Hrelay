﻿var monitorApi = new MonitorAPI();
var hostApi = monitorApi.GetLast();
var hostUrl = hostApi.data.logURL;

function getIoTReference() {
    var api = relayApi.GetReference();

    if (api.error == undefined) {
        var iotRef = api.data;
        if ((iotRef || '') != '')
            $(".form-output").bindView(iotRef);

        $(".dashboard-appliance").each(function () {
            $('.dashboard-hidden[value!="true"]').parent().addClass('operate-hidden');
        });
    }
}

function updateIoTTransaction() {
    //$("#report").dateReport({ date: "2017-04-07" });
    var api = relayApi.GetCurrentState(hostUrl);

    if (api.error == undefined) {
        var iotTrs = api.data;
        if ((iotTrs || '') != '')
            $(".form-output").bindView(iotTrs);
    }

    $(".dashboard-appliance-icon input").each(function () {
        $(this).parent().attr("data-status", ($(this).val() == "") ? 0 : 1);
    });

}