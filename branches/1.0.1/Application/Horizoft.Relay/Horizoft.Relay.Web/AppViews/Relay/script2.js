var tID = null;
var tRun = false;
var t;

function stpclk() {
    if (tRun) {
        clearTimeout(tID);
    }
    tRun = false;
}

function initclk() {
    t = fV("time") * 1000;
    stpclk();
    showt();
}

function showt() {
    var n = new Date();
    n.setTime(t += 1000);
    gId("rtc-disp").innerHTML = n.toUTCString().slice(0, -3);
    tID = setTimeout("showt()", 1000);
    tRun = true;
}

function init() {
    rowWidth = 1;
    for (i = 1; i <= 2; i++) {
        if (cnt[i] == 0 && cRst[i] == 0) {
            cc[i] = 0;
        } else {
            cc[i] = 1;
        }
        if (cc[i] == 0 && is[i] == 0) {
            ir[i] = 0;
        } else {
            ir[i] = 1;
        }
        w[i] = 1;
        if (is[i] == 1) {
            w[i]++;
        }
        if (cc[i] == 1) {
            w[i]++;
        }
        if (w[i] > rowWidth) {
            rowWidth = w[i];
        }
    }
    for (i = 1; i <= 10; i++) {
        if (oob[i] == 0 && pb[i] == 0) {
            bc[i] = 0;
        } else {
            bc[i] = 1;
        }
        if (bc[i] == 0 & rs[i] == 0) {
            r[i] = 0;
        } else {
            r[i] = 1;
        }
        w[i] = 1;
        if (rs[i] == 1) {
            w[i]++;
        }
        if (bc[i] == 1) {
            w[i]++;
        }
        if (w[i] > rowWidth) {
            rowWidth = w[i];
        }
    }
    for (i = 0; i <= 3; i++) {
        if (b1[i] == 0 && b2[i] == 0) {
            ebc[i] = 0;
        } else {
            ebc[i] = 1;
        }
        if (ebc[i] == 0 & es[i] == 0) {
            e[i] = 0;
        } else {
            e[i] = 1;
        }
        w[i] = 1;
        if (es[i] == 1) {
            w[i]++;
        }
        if (ebc[i] == 1) {
            w[i]++;
        }
        if (w[i] > rowWidth) {
            rowWidth = w[i];
        }
    }
    for (i = 1; i <= 2; i++) {
        if (is[i] == 0) {
            gId("it" + i).innerHTML = "&nbsp;"
        }
        if (cnt[i] == 0) {
            gId("cval" + i).innerHTML = "&nbsp;"
        }
        if (cRst[i] == 0) {
            gId("crb" + i).innerHTML = "&nbsp;"
        }
        if (rowWidth >= 2) {
            if (is[i] == 1 && cc[i] == 0) {
                gId("ib" + i).style.display = "none";
                gId("i" + i).cells[1].colSpan = rowWidth - 1;
            } else if (is[i] == 0 && cc[i] == 1) {
                gId("it" + i).style.display = "none";
                gId("i" + i).cells[2].colSpan = rowWidth - 1;
            }
        }
    }
    for (i = 1; i <= 10; i++) {
        if (rs[i] == 0) {
            gId("rs" + i).innerHTML = "&nbsp;"
        }
        if (oob[i] == 0) {
            gId("oob" + i).innerHTML = "&nbsp;"
        }
        if (pb[i] == 0) {
            gId("pb" + i).innerHTML = "&nbsp;"
        }
        if (rowWidth >= 2) {
            if (rs[i] == 1 && bc[i] == 0) {
                gId("bc" + i).style.display = "none";
                gId("r" + i).cells[1].colSpan = rowWidth - 1;
            } else if (rs[i] == 0 && bc[i] == 1) {
                gId("rs" + i).style.display = "none";
                gId("r" + i).cells[2].colSpan = rowWidth - 1;
            }
        }
        if (r[i] == 0) {
            gId("r" + i).style.display = "none";
        }
        if (ir[i] == 0) {
            gId("i" + i).style.display = "none";
        }
    }
    for (i = 0; i <= 3; i++) {
        if (es[i] == 0) {
            gId("es" + i).innerHTML = "&nbsp;"
        }
        if (b1[i] == 0) {
            gId("eb1" + i).style.display = "none";
        }
        if (b2[i] == 0) {
            gId("eb2" + i).style.display = "none";
        }
        if (ebc[i] == 0) {
            gId("ebc" + i).innerHTML = "&nbsp;"
        }
        if (rowWidth >= 2) {
            if (es[i] == 1 && ebc[i] == 0) {
                gId("ebc" + i).style.display = "none";
                gId("e" + i).cells[1].colSpan = rowWidth - 1;
            } else if (es[i] == 0 && ebc[i] == 1) {
                gId("es" + i).style.display = "none";
                gId("e" + i).cells[2].colSpan = rowWidth - 1;
            }
        }
        if (e[i] == 0) {
            gId("e" + i).style.display = "none";
        };
    }
    refresh();
}
