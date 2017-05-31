var ax = null;
var refreshID = null;
var r = new Array(4);
var bc = new Array(4);
var w = new Array(4);
var e = new Array(4);
var ebc = new Array(4);
var cc = new Array(4);
var ir = new Array(4);
var rowWidth = 1;
var is = ["", "0", "0"];
var cnt = ["", "0", "0"];
var cRst = ["", "0", "0"];
var rs = ["", "0", "0", "1", "1", "1", "1", "1", "0", "0", "1"];
var oob = ["", "0", "0", "1", "1", "1", "1", "1", "0", "0", "1"];
var pb = ["", "0", "0", "1", "1", "1", "1", "1", "0", "0", "1"];
var es = ["0", "0", "0", "0"];
var b1 = ["0", "0", "0", "0"];
var b2 = ["0", "0", "0", "0"];
var sr = ["1", "1", "1"];

function gId(n) {
    return document.getElementById(n);
}

function hide(e) {
    gId(e).style.display = "none";
}

function ajaxRequestFinished() {
    var e;
    switch (ax.readyState) {
        case 4:
            updateStatus();
            initclk();
            break;
        default:
            break;
    }
}

function ajaxRequest(filename) {
    if (navigator.appName == "Microsoft Internet Explorer") {
        ax = new ActiveXObject("Microsoft.XMLHTTP");
    } else {
        ax = new XMLHttpRequest();
    }
    if (ax) {
        ax.onreadystatechange = ajaxRequestFinished;
        try {
            ax.open("GET", filename, true);
            ax.send(null);
        } catch (err) {}
    }
}

function DisplayRelayText(whichRelay, state) {
    var onlbl = ["ON", "ON", "ON", "ON", "ON", "ON", "ON", "ON", "ON", "ON"];
    var offlbl = ["OFF", "OFF", "OFF", "OFF", "OFF", "OFF", "OFF", "OFF", "OFF", "OFF"];
    whichRelay--;
    switch (state) {
        case "1":
            return onlbl[whichRelay];
        case "0":
            return offlbl[whichRelay];
        default:
            return ("Undefined State");
    }
}

function DisplayInputText(whichRelay, state) {
    var onlbl = ["ON", "ON"];
    var offlbl = ["OFF", "OFF"];
    whichRelay--;
    switch (state) {
        case "1":
            return onlbl[whichRelay];
        case "0":
            return offlbl[whichRelay];
        default:
            return ("Undefined State");
    }
}

function DisplayEventText(whichVar, val) {
    var onlbl = ["ON", "ON", "ON", "ON"];
    var offlbl = ["0000000", "OFF", "OFF", "OFF"];
    val = parseFloat(val);
    switch (val) {
        case 0:
            return offlbl[whichVar];
        case 1:
            return onlbl[whichVar];
        default:
            return (val);
    }
}

function DisplayExtVarCol(val) {
    switch (val) {
        case "0.00":
            return ("#8E8E8E");
        case "1.00":
            return ("#FFFF00");
        default:
            return ("E8EAEB");
    }
}

function DisplayColor(state) {
    switch (state) {
        case "0":
            return ("#8E8E8E");
        case "1":
            return ("#FFFF00");
        case "2":
            return ("#FFFF00");
        default:
            return ("red");
    }
}

function fV(tag) {
    var opentag = "<" + tag + ">";
    var closetag = "<\/" + tag + ">";
    index = ax.responseText.indexOf(opentag);
    startindex = index + opentag.length;
    endindex = ax.responseText.indexOf(closetag);
    return ax.responseText.slice(startindex, endindex);
}

function refresh(params) {
    var date = new Date();
    var timestamp = date.getTime();
    var request = "state.xml";
    if (params) {
        request = request + "?" + params + "&time=" + timestamp;
    } else {
        request = request + "?time=" + timestamp;
    }
    ajaxRequest(request);
}

function setState(relayNumber, state) {
    var params;
    if (relayNumber >= 1 && relayNumber <= 10 && state >= 0 && state <= 2) {
        params = "relay" + relayNumber + "State=" + state;
        refresh(params);
    }
}

function setV(varNum, value) {
    var params;
    if (varNum >= 0 && varNum <= 4 && value >= 0 && value <= 1) {
        params = "extvar" + varNum + "=" + value;
        refresh(params);
    }
}

function rstCnt(inpNum) {
    refresh("count" + inpNum + "=0");
}

function updateStatus() {
    for (i = 1; i <= 2; i++) {
        if (is[i] == 1) {
            var ival = fV("input" + i + "state");
            var ie = gId("it" + i);
            ie.innerHTML = DisplayInputText(i, ival);
            ie.style.backgroundColor = DisplayColor(ival);
            ie.style.color = "#000000";
        }
        if (cnt[i] == 1) {
            ival = fV("count" + i);
            ie = gId("cval" + i);
            ie.innerHTML = "Count = " + ival;
        }
    }
    for (i = 1; i <= 10; i++) {
        if (rs[i] == 1) {
            var val = fV("relay" + i + "state");
            var e = gId("rs" + i);
            e.innerHTML = DisplayRelayText(i, val);
            e.style.backgroundColor = DisplayColor(val);
            e.style.color = "#000000";
        }
    }
    var units = fV("units");
    for (i = 1; i <= 3; i++) {
        if (sr[i - 1] == 1) {
            var ie = gId("st" + i);
            var ival = fV("sensor" + i + "temp");
            if (ival[0] == 'H') {
                ie.innerHTML = ival.substring(1) + " %RH";
            } else {
                ie.innerHTML = ival + " &deg;" + units;
            }
        } else {
            hide("s" + i);
        }
    }
    for (i = 0; i <= 3; i++) {
        var val = fV("extvar" + i);
        var e = gId("es" + i);
        e.innerHTML = DisplayEventText(i, val);
        e.style.backgroundColor = DisplayExtVarCol(val);
        e.style.color = "#000000";
    }
    var refOn = gId("refOn").value;
    var refTime = gId("refTime").value;
    if (refTime > 0 && refTime < 33 && refOn == 1) {
        if (refreshID) {
            clearTimeout(refreshID);
        }
        refreshID = setTimeout("refresh()", refTime * 1000);
    }
}
