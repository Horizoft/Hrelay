(function ($) {
	$.loadHtml = function (menuUrl) {
		return $.ajax({
			type: 'GET',
			async: false,
			url: menuUrl
		}).responseText;
	}

	$.getJsonData = function (url) {
		var request = $.ajax({
			type: "GET",
			async: false,
			url: url,
			data: [],
			contentType: "application/json; charset=utf-8",
			dataType: "json"
		});

		var response = request.complete();
		var responseText = response.responseText.replace('{"d":null}', '');
		var data = JSON.parse(responseText);

		return data;
	}

	$.bind = function ($elements, object) {
	    if ($elements.length == 0) return;

	    for (var n = 0; n < $elements.length; n++) {
	        var value = object[$elements.eq(n).data("field")];

	        var dataType = $elements.eq(n).data("type");
	        //if (((value || '') != '') && value.toString().isDate()) value = value.toString().toDate();
	        if (($elements.eq(n).data("type") == "number") || ($elements.eq(n).data("type") == "money"))
	            if (((value || '') != '') && value.toString().isNumber()) value = value.toString().toNumberWithCommas();

            if ((value || '') == '') value = "";

	        switch ($elements.eq(n)[0].tagName.toLowerCase()) {
	            case "label":
	            case "span":
	            case "div":
	            case "td":
	                if (value === "") $elements.eq(n).html("&nbsp;")
	                else
	                    $elements.eq(n).text(value);
	                break;
	            case "input":
	            case "select":
	            case "textarea":
	                $elements.eq(n).val(value);
	                break;
	            case "a":
	            	var href = $elements.eq(n).attr("href");
	            	$elements.eq(n).attr("href", href + value);
	            	break;
	        }

	    }
	}

	$.fn.bindView = function (obect) {
	    var $elements = $(this).find("[data-field]");

	    $.bind($elements, obect);
	}

	//$.fn.bind = function(id, object) {
	//	var $elements = $(id + " [data-field]");

	//	$.bind($elements, object);

    //}

	$.fn.resetView = function () {
	    $(this).find("input[type=text], textarea").val("");
	}

	$.fn.bindTable = function (rows) {
	    function bindRows($rowTemplate, rows) {
	        for (var n = 0; n < rows.length; n++) {
	            var $newRow = createNewRow();

	            bindRow($newRow, rows[n]);
	        }
	    }

	    function createNewRow() {
	        var $newRow = $rowTemplate.clone().show();
	        $rowTemplate.first().before($newRow);

	        return $newRow;
	    }

	    function bindRow($row, row) {
	        var $elements = $row.find("[data-field]");

	        $.bind($elements, row);
	    }

	    function getRowTemplate() {
	        return $this.find("tbody tr:last");
	    }

	    var $this = $(this);
	    var $rowTemplate = getRowTemplate();
	    $rowTemplate.hide();

	    bindRows($rowTemplate, rows);
	}

	$.fn.retrieve = function (object) {
	    //var $elements = $(id + " [data-field]");
	    var $elements = $(this).find("[data-field]");

	    return $.retrieve($elements, object);
	}

	$.retrieve = function ($elements, object) {
	    if ($elements.length == 0) return;

	    for (var n = 0; n < $elements.length; n++) {
	        var name = $elements.eq(n).data("field");

	        switch ($elements.eq(n)[0].tagName.toLowerCase()) {
	            case "div":
	            case "label":
	            case "td":
	                object[name] = $elements.eq(n).text();
	                break;
	            case "input":
	            case "select":
	                object[name] = $elements.eq(n).val();
	                break;
	        }

	    }

	    return object;
	}

	$.getUrlQueryString = function (name, url) {
	    if (!url) url = window.location.href;

	    name = name.replace(/[\[\]]/g, "\\$&");

	    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
	        results = regex.exec(url);

	    if (!results) return null;
	    if (!results[2]) return '';

	    return decodeURIComponent(results[2].replace(/\+/g, " "));
	}

	$.fn.outerHTML = function () {

	    // IE, Chrome & Safari will comply with the non-standard outerHTML, all others (FF) will have a fall-back for cloning
	    return (!this.length) ? this : (this[0].outerHTML || (
          function (el) {
              var div = document.createElement('div');
              div.appendChild(el.cloneNode(true));
              var contents = div.innerHTML;
              div = null;
              return contents;
          })(this[0]));
	}

	Array.prototype.inArray = function (searchFor, property) {
	    var retVal = -1;
	    var self = this;
	    for (var n = 0; n < self.length; n++) {
	        var item = self[n];
	        if (item.hasOwnProperty(property)) {
	            if (item[property].toLowerCase() === searchFor.toLowerCase()) {
	                retVal = n;
	                return retVal;
	            }
	        }
	    };
	    return retVal;
	}

	Array.prototype.remove = function (removingList, property) {
	    var array = this;
	    var n_this = 0;

	    for (var n = 0; n < removingList.length; n++) {
	        var index = array.inArray(removingList[n], property);
	        if (index >= 0) array.splice(index, 1);
	    }

	    return array;
	}

}(jQuery));