function showSnackbar(message) {
    $(".snackbar label").text(message);
    $(".snackbar").css("display", "block");

    setTimeout(function () {
        $(".snackbar").css("top", "86%");
    }, 10);

    setTimeout(function () {
        hideSnackbar();
    }, 7000);
}

function hideSnackbar() {
    $(".snackbar").css("top", "110%");
}

$(".snackbar button").attr('onclick', 'hideSnackbar();');