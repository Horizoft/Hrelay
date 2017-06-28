function TemperatureReportAPI() {
    this.webApi = new webAPI();
    //this.url = "/API/TemperatureReport/";
    this.url = serviceUrl + "/API/TemperatureReport/";

    this.getReports = function (date) {
        var action = "GetReports"
        var list = this.webApi.getData(this.url + action + "/" + date);

        return list;
    }

    this.getAverageTemperatureData = function (date) {
        var temperatureData = { name: date, data: [] };
        var data = this.getReports(date);

        data = data[0].temperatureReports;

        for (var n = 0; n < data.length; n++) {
            temperatureData.data.push(data[n].averageTemperature);
        }

        return temperatureData;
    }
}