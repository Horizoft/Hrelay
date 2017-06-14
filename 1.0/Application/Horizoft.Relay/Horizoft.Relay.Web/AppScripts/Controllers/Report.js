var temperatureReportApi = new TemperatureReportAPI();

$(document).ready(function () {
    $("#menu-left").loadLeftMenu("/AppViews/Home/LeftMenu2.html", "blue-highlight");
    $("#report").dateReport();

})