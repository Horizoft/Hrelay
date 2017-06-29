function webAPI() {
    this.getResponseText = function (url) {
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

        return responseText;
    }

    this.getData = function (url) {
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

    this.postDataAndGetResponse = function (url, data) {
        var json = JSON.stringify(data);

        console.log("JSON serialized object");
        console.log(json);

        var request = $.ajax({
            type: "POST",
            async: false,
            url: url,
            data: json,
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });

        var response = request.complete();
        var responseText = response.responseText.replace('{"d":null}', '');
        var data = JSON.parse(responseText);

        return data;
    }

    this.postDataAndGetStatus = function (url, data) {
        var json = JSON.stringify(data);

        var request = $.ajax({
            type: "POST",
            async: false,
            url: url,
            data: json,
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });

        var response = request.complete();
        //var responseText = response.responseText.replace('{"d":null}', '');
        //var data = JSON.parse(responseText);

        //return data;
    }

}