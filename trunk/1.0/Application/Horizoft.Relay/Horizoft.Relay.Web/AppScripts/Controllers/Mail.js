var mailApi = new MailAPI();
var monitorApi = new MonitorAPI();

$(document).ready(function () {
    $("#menu-left").loadLeftMenu("../Home/LeftMenu.html", "blue-highlight");

    $("#popup-warning").popup({ status: "init" });
    $("#popup-notification").popup({ status: "init" });

    var api = mailApi.GetLast();
    var apiMonitor = monitorApi.GetLast();

    if (api.error == undefined || apiMonitor.error == undefined) {
        var mail = api.data;
        var monitor = apiMonitor.data;
        if ((mail || '') != '')
            
            $(".form-input").bindView(mail);

        $(".form-input-monitor").val(monitor.temperatureAlert);
        $(".form-input-message").jqte();
    }
    else {
        $("#popup-notification").popup({ show: true, message: "Failed to load mail setting." });
        $("#btn-disabled").prop('disabled', true);
    }

})

$(document).on("click", "[data-action='Mail-Save']", function () {
    $("#popup-warning").popup({ show: true, message: "Do you want to change mail setting?", eventName: "onSaveMail" });
})

$(document).on("click", "[data-action='Mail-Reset']", function () {
    $("#popup-warning").popup({ show: true, message: "Do you want to reset mail setting?", eventName: "onResetMail" });
})

$(document).on("onSaveMail", "#popup-warning", function () {
    var mail = $("#mail-input").retrieve(new DTMail());
    mail.message = $(".jqte_editor").html();

    var monitor = monitorApi.GetLast();
    monitor.data.temperatureAlert = $(".form-input-monitor").val();

    if (mail.id != '') {
        var result = mailApi.Update(mail);
        monitorApi.Update(monitor.data);

        if (result.error == undefined) {
            $("#popup-notification").popup({ show: true, message: "Mail data saved successfully." });
            $(".form-input").bindView(result.data);
        }
        else
            $("#popup-notification").popup({ show: true, message: "Failed to save mail data." });

    } else {
        var result = 0;//mailApi.Add(mail);

        if (result.error == undefined) {
            $("#popup-notification").popup({ show: true, message: "Mail data saved successfully." });
            $(".form-input").bindView(result.data);
        }
        else
            $("#popup-notification").popup({ show: true, message: "Failed to save mail data." });
    }

})

$(document).on("onResetMail", "#popup-warning", function () {
    var defualt_mail = mailApi.GetFirst(); //Defualt row 
    var mail = $("#mail-input").retrieve(new DTMail());
    //mail.id = 0;
    //mail.sender = "";
    //mail.recipients = "";
    //mail.subject = "";
    //mail.message = "";

    mail.sender = defualt_mail.data.sender;
    mail.recipients = defualt_mail.data.recipients;
    mail.subject = defualt_mail.data.subject;
    mail.message = defualt_mail.data.message;

    var result = mailApi.Update(mail);

    if (result.error == undefined) {
        $("#popup-notification").popup({ show: true, message: "Mail data reset successfully." });
        $(".form-input").bindView(result.data);

        // $(".form-input-message").html();
        $(".form-input-message").parent().parent().find(".jqte_editor").html(result.data.message)
    }
    else
        $("#popup-notification").popup({ show: true, message: "Failed to reset mail data." });

})
