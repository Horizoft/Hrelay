function MailAPI() {

    this.webApi = new webAPI();
    this.url = "/API/Mail/";

    this.GetFirst = function () {
        var action = "GetFirst"
        var mail = this.webApi.getData(this.url + action);
        return mail;
    }

    this.Add = function (mail) {
        var action = "Add"
        var mail = this.webApi.postDataAndGetStatus(this.url + action, mail);
        return mail;
    }

    this.Update = function (mail) {
        var action = "Update"
        var mail = this.webApi.postDataAndGetStatus(this.url + action, mail);
        return mail;
    }

    this.Delete = function (mail) {
        var action = "Delete"
        var mail = this.webApi.postDataAndGetStatus(this.url + action, mail);
        return mail;
    }

}