function RelayAPI() {
    this.webApi = new webAPI();
    this.url = "/API/Relay/";

    this.GetCurrentState = function () {
        var action = "GetCurrentState"
        var type = "GET"
        var iotTrs = CRUD(iotTrs, this.url + action, type);
        return iotTrs;
    }
}