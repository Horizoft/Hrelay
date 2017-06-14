var hostApi = new HostAPI();

$(document).ready(function () {
    $("#menu-left").loadLeftMenu("/AppViews/Home/LeftMenu2.html", "blue-highlight");

    $("#popup-warning").popup({ status: "init" });
    $("#popup-notification").popup({ status: "init" });

    //var hostList = [
    //    { id: 1, host: "smtp.live.com", protocol: "SMTP", userName: "horizoft@hotmail.com", password: "p_12345" },
    //    { id: 2, host: "ews.horizoft.com", protocol: "EWS", userName: "ews@horizoft.com", password: "p_12345" }
    //];

    //var host = hostList[0];
    var api = hostApi.GetLast();

    if (api.error == undefined) {
        var host = api.data;
        if ((host || '') != '')
            $(".form-input").bindView(host);
    }
    else {
        $("#popup-notification").popup({ show: true, message: "Failed to load host setting." });
        $("#btn-disabled").prop('disabled', true);
    }
})

$(document).on("click", "[data-action='Host-Save']", function () {
    $("#popup-warning").popup({ show: true, message: "Do you want to change host setting?", eventName: "onSaveHost" });
})

$(document).on("onSaveHost", "#popup-warning", function () {
    var host = $("#host-item").retrieve(new DTHost());

    if (host.id != '') {
        var result = hostApi.Update(host);

        if (result.error == undefined) {
            $("#popup-notification").popup({ show: true, message: "Host data saved successfully." });
            $(".form-input").bindView(result.data);
        }
        else
            $("#popup-notification").popup({ show: true, message: "Failed to save Host data." });
    } else {
        var result = hostApi.Add(host);

        if (result.error == undefined) {
            $("#popup-notification").popup({ show: true, message: "Host data saved successfully." });
            $(".form-input").bindView(result.data);
        }
        else
            $("#popup-notification").popup({ show: true, message: "Failed to save Host data." });
    }

})

$(document).on("click", "[data-action='Host-Reset']", function () {
    $("#popup-warning").popup({ show: true, message: "Do you want to reset host setting?", eventName: "onResetHost" });
})

$(document).on("onResetHost", "#popup-warning", function () {
    var defualt_host = hostApi.GetFirst(); //Defualt row 
    var host = $("#host-item").retrieve(new DTHost());
    //host.uRL = "";
    //host.protocal = "";
    //host.port = 0;
    //host.username = "";
    //host.password = "";
    host.url = defualt_host.data.url;
    host.protocal = defualt_host.data.protocal;
    host.port = defualt_host.data.port;
    host.userName = defualt_host.data.userName;
    host.password = defualt_host.data.password;

    var result = hostApi.Update(host);

    if (result.error == undefined) {
        $("#popup-notification").popup({ show: true, message: "Host data reset successfully." });
        $(".form-input").bindView(result.data);
    }
    else
        $("#popup-notification").popup({ show: true, message: "Failed to reset host data." });

})
