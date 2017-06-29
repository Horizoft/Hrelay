function RelayAPI() {
    this.webApi = new webAPI();
    //this.url = "/API/Relay/";
    this.url = serviceUrl + "/API/Relay/";

    this.GetReference = function () {
        var action = "GetReference"
        var type = "GET"
        var iotRef = CRUD(iotRef, this.url + action, type);
        return iotRef;
    }

    //this.GetCurrentState = function () {
    //    var action = "GetCurrentState"
    //    var type = "GET"
    //    var iotTrs = CRUD(iotTrs, this.url + action, type);
    //    return iotTrs;
    //}

    this.GetCurrentState = function (hostUrl) {
        var action = "GetCurrentState?hostUrl=" + hostUrl
        var type = "GET"
        var iotTrs = CRUD(iotTrs, this.url + action, type);
        return iotTrs;
    }
}