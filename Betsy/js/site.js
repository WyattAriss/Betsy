// Write your JavaScript code.

function go() {
    var offset = window.pageYOffset;
    var header = document.getElementById("header_octagon");
    var logo = document.getElementById("logo");
    header.style.backgroundPosition = "0px " + offset / 2 + "px";
}