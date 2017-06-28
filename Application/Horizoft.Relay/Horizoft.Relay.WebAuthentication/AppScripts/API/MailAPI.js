function MailAPI() {

    this.webApi = new webAPI();
    //this.url = "/API/Mail/";
    this.url = serviceUrl + "/API/Mail/";

    this.GetFirst = function () {
        var action = "GetFirst"
        var type = "GET"
        var mail = CRUD(mail, this.url + action, type);
        return mail;
    }

    this.GetLast = function () {
        var action = "GetLast"
        var type = "GET"
        var monitor = CRUD(monitor, this.url + action, type);
        return monitor;
    }

    this.Add = function (mail) {
        var action = "Add"
        var type = "POST"
        var mail = CRUD(mail, this.url + action, type);
        return mail;
    }

    this.Update = function (mail) {
        var action = "Update"
        var type = "POST"
        var mail = CRUD(mail, this.url + action, type);
        return mail;
    }

    this.Delete = function (mail) {
        var action = "Delete"
        var type = "POST"
        var mail = CRUD(mail, this.url + action, type);
        return mail;
    }

    //this.GetFirst = function () {
    //    var action = "GetFirst"
    //    var mail = this.webApi.getResponseText(this.url + action);
    //    return mail;
    //}

    //this.Add = function (mail) {
    //    var action = "Add"
    //    var mail = this.webApi.postDataAndGetStatus(this.url + action, mail);
    //    return mail;
    //}

    //this.Update = function (mail) {
    //    var action = "Update"
    //    var mail = this.webApi.postDataAndGetStatus(this.url + action, mail);
    //    return mail;
    //}

    //this.Delete = function (mail) {
    //    var action = "Delete"
    //    var mail = this.webApi.postDataAndGetStatus(this.url + action, mail);
    //    return mail;
    //}

}