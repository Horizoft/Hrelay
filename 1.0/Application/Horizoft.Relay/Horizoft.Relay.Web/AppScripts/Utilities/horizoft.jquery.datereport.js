(function ($) {
	$.fn.dateReport = function (options) {
		var _dateReport = $(this);
		var dateOnFocus = "";
		var dateOnChange = "";

		var configurations = {
			chart: {
				renderTo: 'chart',
				type: 'line',
				borderRadius: 0,
				plotBorderWidth: 3,
				plotBorderColor: '#999'
			},
			title: {
				text: '10 Minutes Interval Temperature',
				align: 'left',
				x: 50 //center
			},
			subtitle: {
				text: 'Source: Server Room',
				align: 'left',
				x: 50
			},
			xAxis: {
				categories: ['0:00', '0:10', '0:20', '0:30', '0:40', '0:50', '1:00', '1:10', '1:20', '1:30', '1:40', '1:50', '2:00', '2:10', '2:20', '2:30', '2:40', '2:50', '3:00', '3:10', '3:20', '3:30', '3:40', '3:50', '4:00', '4:10', '4:20', '4:30', '4:40', '4:50', '5:00', '5:10', '5:20', '5:30', '5:40', '5:50', '6:00', '6:10', '6:20', '6:30', '6:40', '6:50', '7:00', '7:10', '7:20', '7:30', '7:40', '7:50', '8:00', '8:10', '8:20', '8:30', '8:40', '8:50', '9:00', '9:10', '9:20', '9:30', '9:40', '9:50', '10:00', '10:10', '10:20', '10:30', '10:40', '10:50', '11:00', '11:10', '11:20', '11:30', '11:40', '11:50', '12:00', '12:10', '12:20', '12:30', '12:40', '12:50', '13:00', '13:10', '13:20', '13:30', '13:40', '13:50', '14:00', '14:10', '14:20', '14:30', '14:40', '14:50', '15:00', '15:10', '15:20', '15:30', '15:40', '15:50', '16:00', '16:10', '16:20', '16:30', '16:40', '16:50', '17:00', '17:10', '17:20', '17:30', '17:40', '17:50', '18:00', '18:10', '18:20', '18:30', '18:40', '18:50', '19:00', '19:10', '19:20', '19:30', '19:40', '19:50', '20:00', '20:10', '20:20', '20:30', '20:40', '20:50', '21:00', '21:10', '21:20', '21:30', '21:40', '21:50', '22:00', '22:10', '22:20', '22:30', '22:40', '22:50', '23:00', '23:10', '23:20', '23:30', '23:40', '23:50'],
				labels: {
					rotation: -45,
					align: 'right',
					style: {
						fontSize: '12px',
						fontFamily: 'Verdana, sans-serif'
					}
				},
				tickInterval: 6
			},
			yAxis: {
				title: {
					text: 'Temperature (°C)'
				},
				plotLines: [{
					value: 0,
					width: 1,
					color: '#808080'
				}]
			},
			tooltip: {
				formatter: function () {
					return '<b>' + this.series.name + '</b><br/>' +
					this.x + ': ' + this.y + '°C';
				}
			},
			legend: {
				layout: 'vertical',
				align: 'left',
				verticalAlign: 'top',
				x: 100,
				y: 200,
				borderWidth: 1,
				floating: true
			},
			plotOptions: {
				series: {
					lineWidth: 1,
					marker: {
						radius: 0
					}
				}
			},
			series: []
		};

		var chartData = configurations.series;

		var addChartData = function (data) {
			chartData.push(data);
		}

		var updateChartData = function (index, data) {
			chartData[index] = data;
		}

		var deleteChartData = function (index) {
			chartData.splice(index, 1);
		}

		var addNewDate = function () {
			var lastDate = _dateReport.find("ul li:last");
			var newDate = lastDate.clone();

			lastDate.hide();
			lastDate.before(newDate);
			newDate.show();

			var textBox = newDate.find("input[type='text']");
			textBox.datepicker({
				dateFormat: "yy-mm-dd",
				showOn: "button",
				buttonImage: "../../Images/calendar.gif",
				buttonImageOnly: true
			});

		}

		var deleteDate = function (index) {
			_dateReport.find("ul").children().eq(index).remove();
			//$("ul").children().eq(index).remove();
		}

		var isDateLastItem = function ($thisLi) {
			var $ul = $thisLi.closest("ul");
			var index = $ul.children().index($thisLi);

			return (index < $ul.children().length - 2) ? false: true;
		}

		var isDateDuplicated = function (date) {
			var n = 0;

			while (n < chartData.length) {
				if (date == chartData[n].name) return true;
				n++;
			}

			return false;
		}

		var show = function (configurations) {
			var chart = new Highcharts.Chart(configurations);
		}

		var refresh = function (configurations) {
			show(configurations);
		}

		$(this).on("focus", "[data-field=date]", function () {
			dateOnFocus = $(this).val();
			dateOnChange = "";
		});

		$(this).on("change", "[data-field=date]", function () {
			dateOnChange = $(this).val();

			if (dateOnFocus == dateOnChange) return;

			//var date = $(this).val();
			if (isDateDuplicated(dateOnChange)) {
				$(this).val(dateOnFocus);
				return;
			}

			var temperatureReport = temperatureReportApi.getAverageTemperatureData(dateOnChange);
			var $li = $(this).closest("li");

			if (isDateLastItem($li)) {
				
				addChartData(temperatureReport);
				show(configurations);
				addNewDate();
			}
			else {
				var $ul = $li.closest("ul");
				var index = $ul.children().index($li);
				updateChartData(index, temperatureReport);
				show(configurations);
			}

		});

		$(this).on("click", "[data-action=date-delete]", function () {
			var $ul = $(this).closest("ul");
			var $li = $(this).closest("li");
			var index = $ul.children().index($li);

			if ($ul.children().length == 2) return;
			if ($li.find("input[type='text']").val() == '') return;

			deleteDate(index);
			deleteChartData(index);
			refresh(configurations);

		});

		addNewDate();

		if (((options || '') != '') && options.hasOwnProperty("date")) {
		    var date = options.date;

		    //var temperatureReportApi = new TemperatureReportAPI();
		    var data = temperatureReportApi.getAverageTemperatureData(date);

		    configurations.series.push({});
		    configurations.series[0] = data;
		    var chart = new Highcharts.Chart(configurations);
		}

	}
}(jQuery));