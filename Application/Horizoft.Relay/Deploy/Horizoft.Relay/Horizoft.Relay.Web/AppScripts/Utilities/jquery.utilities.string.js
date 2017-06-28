String.format = String.prototype.format = function() {
    var i=0;
    var string = (typeof(this) == "function" && !(i++)) ? arguments[0] : this;
 
    for (; i < arguments.length; i++)
        string = string.replace(/\{\d+?\}/, arguments[i]);
 
    return string;
}

String.prototype.lpad = function (padString, length) {
    var str = this;
    while (str.length < length)
        str = padString + str;
    return str;
}

//pads right
String.prototype.rpad = function (padString, length) {
    var str = this;
    while (str.length < length)
        str = str + padString;
    return str;
}

String.prototype.toNumberWithCommas = function () {
    var number = this;
    return number.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}

String.prototype.isNumber = function () {
    var number = this;
    return !isNaN(parseFloat(number)) && isFinite(number);
}

String.prototype.capitalizeFirstLetter = function () {
    var str = this;
    str = str.toLowerCase().replace(/\b[a-z]/g, function (letter) {
        return letter.toUpperCase();
    });

    return str;
}

String.prototype.replaceAll = function (search, replacement) {
    var target = this;
    return target.split(search).join(replacement);
};

String.prototype.toDate = function () {

    var str = this;
    var MM = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];

    if (str.match(/(\d{2})-(\d{2})-(\d{4})/)) {
        var str = str.replace(/(\d{2})-(\d{2})-(\d{4})/,
            function ($0, $1, $2, $3) {
                var newStr = $1 + " " + MM[$2 - 1] + " " + $3;
                return newStr;
            }
        );
    }

    if (str.match(/(\d{4})-(\d{2})-(\d{2})/)) {
        var isoDate = str.slice(0, 19);
        isoDate = isoDate.concat();

        MM = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];

        str = isoDate.replace(/(\d{4})-(\d{2})-(\d{2})T(\d{2}):(\d{2}):\d{2}/,
            function ($0, $1, $2, $3) {
                var newStr = $3 + " " + MM[$2 - 1] + " " + $1;
                return newStr;
            }
        );
    }

    return str;

}

String.prototype.toDateTime = function () {

    var isoDate = this.slice(0, 19);
    isoDate = isoDate.concat();
    //MM = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
    MM = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];

    str = isoDate.replace(/(\d{4})-(\d{2})-(\d{2})T(\d{2}):(\d{2}):\d{2}/,
        function ($0, $1, $2, $3, $4, $5, $6) {
            var newStr = $3 + " " + MM[$2 - 1] + " " + $1 + " " + $4 % 12 + ":" + $5 + (+$4 > 12 ? "PM" : "AM");
            return newStr;
        }
    );

    return str;

}

Date.prototype.yyyymmdd = function () {
    var mm = this.getMonth() + 1; // getMonth() is zero-based
    var dd = this.getDate();

    return [this.getFullYear(),
            (mm > 9 ? '' : '0') + mm,
            (dd > 9 ? '' : '0') + dd
    ].join('-');
};

String.prototype.getGUID = function () {
    var S4 = function () {
        return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
    };
    return (S4() + S4() + "-" + S4() + "-" + S4() + "-" + S4() + "-" + S4() + S4() + S4());
}

