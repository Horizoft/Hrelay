﻿function HostAPI() {

    this.webApi = new webAPI();
    this.url = "/API/Host/";

    this.GetFirst = function () {
        var action = "GetFirst"
        var host = this.webApi.getData(this.url + action);
        return host;
    }

    this.Add = function (host) {
        var action = "Add"
        var host = this.webApi.postDataAndGetStatus(this.url + action, host)
        return host;
    }

    this.Update = function (host) {
        var action = "Update"
        var host = this.webApi.postDataAndGetStatus(this.url + action, host);
        return host;
    }

    this.Delete = function (host) {
        var action = "Delete"
        var host = this.webApi.postDataAndGetStatus(this.url + action, host);
        return host;
    }

}