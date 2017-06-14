function MonitorAPI() {

    this.webApi = new webAPI();
    //this.url = "/API/Monitor/";
    this.url = serviceUrl + "/Monitor/";

    this.GetFirst = function () {
        var action = "GetFirst"
        var type = "GET"
        var monitor = CRUD(monitor, this.url + action, type);
        return monitor;
    }

    this.GetLast = function () {
        var action = "GetLast"
        var type = "GET"
        var monitor = CRUD(monitor, this.url + action, type);
        return monitor;
    }

    this.Add = function (monitor) {
        var action = "Add"
        var type = "POST"
        var monitor = CRUD(monitor, this.url + action, type);
        return monitor;
    }

    this.Update = function (monitor) {
        var action = "Update"
        var type = "POST"
        var monitor = CRUD(monitor, this.url + action, type);
        return monitor;
    }

    this.Delete = function (monitor) {
        var action = "Delete"
        var type = "POST"
        var monitor = CRUD(monitor, this.url + action, type);
        return monitor;
    }

    //this.GetFirst = function () {
    //    var action = "GetFirst"
    //    var monitor = this.webApi.getData(this.url + action);
    //    return monitor;
    //}

    //this.Add = function (monitor) {
    //    var action = "Add"
    //    var monitor = this.webApi.postDataAndGetStatus(this.url + action, monitor);
    //    return monitor;
    //}

    //this.Update = function (monitor) {
    //    var action = "Update"
    //    var monitor = this.webApi.postDataAndGetStatus(this.url + action, monitor);
    //    return monitor;
    //}

    //this.Delete = function (monitor) {
    //    var action = "Delete"
    //    var monitor = this.webApi.postDataAndGetStatus(this.url + action, monitor);
    //    return monitor;
    //}

}