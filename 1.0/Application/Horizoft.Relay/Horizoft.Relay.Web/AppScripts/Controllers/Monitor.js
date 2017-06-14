var monitorApi = new MonitorAPI();

$(document).ready(function () {
    //$("#menu-left").loadLeftMenu("/AppViews/Home/LeftMenu2.html", "blue-highlight");
    $("#menu-left").loadLeftMenu(menuUrl + "/LeftMenu2.html", "blue-highlight");

    $("#popup-warning").popup({ status: "init" });
    $("#popup-notification").popup({ status: "init" });

    var api = monitorApi.GetLast();

    if (api.error == undefined) {
        var monitor = api.data;
        if ((monitor || '') != '')
            $(".form-input").bindView(monitor);
    }
    else {
        $("#popup-notification").popup({ show: true, message: "Failed to load host setting." });
        $("#btn-disabled").prop('disabled', true);
    }

    //Textbox Number Only
    $("#numberOnly").keydown(function (e) {
        // Allow: backspace, delete, tab, escape, enter and .
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
            // Allow: Ctrl+A, Command+A
            (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            // Allow: home, end, left, right, down, up
            (e.keyCode >= 35 && e.keyCode <= 40)) {
            // let it happen, don't do anything
            return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });

})

$(document).on("click", "[data-action='Monitor-Save']", function () {
    $("#popup-warning").popup({ show: true, message: "Do you want to change monitor setting?", eventName: "onSaveMonitor" });
})

$(document).on("onSaveMonitor", "#popup-warning", function () {
    var monitor = $("#monitor-item").retrieve(new DTMonitor());

    if (monitor.id != '') {
        var result = monitorApi.Update(monitor);

        if (result.error == undefined) {
            $("#popup-notification").popup({ show: true, message: "Monitor data saved successfully." });
            $(".form-input").bindView(result.data);
        }
        else
            $("#popup-notification").popup({ show: true, message: "Failed to save monitor data." });
    }
    else {
        var result = monitorApi.Add(monitor);

        if (result.error == undefined) {
            $("#popup-notification").popup({ show: true, message: "Monitor data saved successfully." });
            $(".form-input").bindView(result.data);
        }
        else
            $("#popup-notification").popup({ show: true, message: "Failed to save monitor data." });

    }

})

$(document).on("click", "[data-action='Monitor-Reset']", function () {
    $("#popup-warning").popup({ show: true, message: "Do you want to Reset monitor setting?", eventName: "onResetMonitor" });
})

$(document).on("onResetMonitor", "#popup-warning", function () {
    var defualt_monitor = monitorApi.GetFirst(); //Defualt row 
    var monitor = $("#monitor-item").retrieve(new DTMonitor());
    monitor.logURL = defualt_monitor.data.logURL;
    monitor.temperatureAlert = defualt_monitor.data.temperatureAlert;

    var result = monitorApi.Update(monitor);

    if (result.error == undefined) {
        $("#popup-notification").popup({ show: true, message: "Monitor data reset successfully." });
        $(".form-input").bindView(result.data);
    }
    else
        $("#popup-notification").popup({ show: true, message: "Failed to reset monitor data." });
})

function IsNumeric(e) {
    //Textbox Number Only
    $("#numberOnly").keydown(function (e) {
        // Allow: backspace, delete, tab, escape, enter and .
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
            // Allow: Ctrl+A, Command+A
            (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            // Allow: home, end, left, right, down, up
            (e.keyCode >= 35 && e.keyCode <= 40)) {
            // let it happen, don't do anything
            return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });

}