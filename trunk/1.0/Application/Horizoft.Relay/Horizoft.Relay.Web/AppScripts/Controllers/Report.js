var temperatureReportApi = new TemperatureReportAPI();

$(document).ready(function () {
    $("#menu-left").loadLeftMenu("../Home/LeftMenu.html", "blue-highlight");
    $("#report").dateReport();

})