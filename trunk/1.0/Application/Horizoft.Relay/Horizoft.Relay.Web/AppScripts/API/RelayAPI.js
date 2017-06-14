function RelayAPI() {
    this.webApi = new webAPI();
    //this.url = "/API/Relay/";
    this.url = serviceUrl + "/Relay/";

    this.GetReference = function () {
        var action = "GetReference"
        var type = "GET"
        var iotRef = CRUD(iotRef, this.url + action, type);
        return iotRef;
    }

    this.GetCurrentState = function () {
        var action = "GetCurrentState"
        var type = "GET"
        var iotTrs = CRUD(iotTrs, this.url + action, type);
        return iotTrs;
    }
}