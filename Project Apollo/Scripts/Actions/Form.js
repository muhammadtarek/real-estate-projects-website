$(window).ready(function () {
    $("#login-form").css("top", "-435px");
    $("#signup-form").css("top", "-739px");

    $(".close").click(function () {
        hideForm();
    })
});

function showForm(formId) {
    var form = "#" + formId;

    $(form + ", .popup-container").css("display", "block");
    setTimeout(function () {
        $(form).css("top", "35px");

    }, 2);
}

function hideForm() {
    $("#signup-form").css("top", "-639px");
    $("#login-form").css("top", "-435px");

    setTimeout(function () {
        $(".form, .popup-container").css("display", "none");
    }, 260);
}