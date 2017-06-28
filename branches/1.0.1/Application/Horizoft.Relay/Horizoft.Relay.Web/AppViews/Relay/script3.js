var i;
var n = ["extVar0", "extVar1", "extVar2", "extVar3"];
var eb = ["ON", "ON", "ON", "ON"];
var eb2 = ["OFF", "OFF", "OFF", "OFF"];
var d = ["", "RelayDamage2", "RelayDamage1", "18000BTU 2", "Fan", "Exhaust Fan", "Air Vent", "14000BTU", "RelayDamage3", "RelayDamage4", "18000BTU 1"];
var o = ["", "ON", "ON", "ON", "ON", "ON", "ON", "ON", "ON", "ON", "ON"];
var f = ["", "OFF", "OFF", "OFF", "OFF", "OFF", "OFF", "OFF", "OFF", "OFF", "OFF"];
var p = ["", "PULSE", "PULSE", "PULSE", "PULSE", "PULSE", "PULSE", "PULSE", "PULSE", "PULSE", "PULSE"];
var s = ["", "Rack 2 Sensor", "Rack 3 Sensor", "Sensor 3"];
for (i = 1; i <= 10; i++) {
    document.writeln("<tr id=\"r" + i + "\"><td>" + d[i] + "</td><td id=\"rs" + i + "\" class=\"center\">ON</td><td id=\"bc1\" class=\"center\"><span id=\"oob" + i + "\"><input type=\"button\" value=\"" + o[i] + "\" onclick=\"javascript:setState(" + i + ",1)\"><input type=\"button\" value=\"" + f[i] + "\" onclick=\"javascript:setState(" + i + ",0)\"></span><span id=\"pb" + i + "\"><input type=\"button\" value=\"" + p[i] + "\" onclick=\"javascript:setState(" + i + ",2)\"></span></td></tr>");
}
for (i = 1; i <= 3; i++) {
    document.writeln("<tr id=\"s" + i + "\"><td>" + s[i] + "</td><td id=\"st" + i + "\" colspan=\"2\"></td></tr>");
}
for (i = 0; i <= 3; i++) {
    document.writeln("<tr id=\"e" + i + "\"><td>" + n[i] + "</td><td id=\"es" + i + "\" class=\"center\">ON</td><td id=\"ebc" + i + "\" class=\"center\"><span id=\"eb1" + i + "\"><input type=\"button\" value=\"" + eb[i] + "\" onclick=\"javascript:setV(" + i + ",1)\"></span><span id=\"eb2" + i + "\"><input type=\"button\" value=\"" + eb2[i] + "\" onclick=\"javascript:setV(" + i + ",0)\"></span></td></tr>");
}