// Write your JavaScript code.

function go() {
    var offset = window.pageYOffset;
    var header = document.getElementById("header_octagon");
    var logo = document.getElementById("logo");
    header.style.backgroundPosition = "0px " + offset / 2 + "px";
}

$("#valid").click(function () {
    $(".betsy").addClass("up").delay(100).fadeOut(100);
    $(".login").addClass("down").delay(150).fadeOut(100);
});