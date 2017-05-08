$(window).ready(function () {
    var formHeight = 0;

    var loginForm = "#login-form";
    var signUpForm = "#signup-form";

    $(loginForm).css("top", "-435px");
    $(signUpForm).css("top", "-639px");

    function showForm(formId) {
        var form = "#" + formId;

        $(form + ", .popup-container").css("display", "block");
        setTimeout(function () {
            $(form).css("top", "35px");

        }, 2);
    }

    function hideForm() {
        $(loginForm).css("top", "-435px");
        $(signUpForm).css("top", "-639px");

        setTimeout(function () {
            $(".form, .popup-container").css("display", "none");
        }, 260);
    }

    /* ACTION LISENTERS */
    $("#login-btn").click(function () {
        showForm("login-form");
    });

    $("#signup-btn").click(function () {
        showForm("signup-form");
    });

    $(".close").click(function () {
        hideForm();
    })

    //Taking the photo from file uploader and preview it
    $("#user-photo").change(function () {
      var file = document.querySelector('input[type=file]').files[0];
      var reader = new FileReader();
      console.log("Triggered");

      reader.addEventListener("load", function () {
        $("#photo-preview").attr('src', reader.result);
      }, false);

      if (file) {
        reader.readAsDataURL(file);
      }
    });

    /*Action lisenters*/
    $("#btn-signup").click(signUp);
    $("#btn-login").click(login);
});

/* FUNCTIONS */
function signUp() {
    if (checkForEmptyFields("signup") && checkForDangerFields()) {
        $("#btn-signup").prop("disabled", true);
        var email = getInputValue("signup-email");
        var name = getInputValue("signup-name");
        var phone = getInputValue("signup-phone");
        var password = getInputValue("signup-password");
        var userrolestring = getInputValue("signup-userrole");
        var userrole = 0;
        if (userrolestring === "Admin")
            userrole = 0;
        else if (userrolestring === "Customer")
            userrole = 1;
        else if (userrolestring === "Project Manager")
            userrole = 2;
        else if (userrolestring === "Team Leader")
            userrole = 3;
        else
            userrole = 4;
        var bio = getInputValue("signup-bio");
        var filesSelected = $("#photo-preview").attr('src');

        var datasend = {
            userPicture: filesSelected,
            name: name,
            email: email,
            password: password,
            phoneNumber: phone,
            Desciption: bio,
            userType: userrole
        }

        $.post("/welcome/signUp", datasend, function (data) {
            rec = JSON.parse(data);
            console.log(rec);
            if (rec.result.email !== true) {
                markInputAs("signup-email", DANGER, rec.result.email);
                $("#btn-signup").prop("disabled", false);
            }
            else {
                markInputAs("signup-email", SUCCESS, "");
                var url = "/home/Index";
                window.location.href = url;
            }
        });
    }
}

function login() {
    if (checkForEmptyFields("login") && checkForDangerFields()) {
        $("#btn-login").prop("disabled", true);
        var email = getInputValue("login-email");
        var password = getInputValue("login-password");
        var datasend = {
            email: email,
            password: password,
        }
        $.post("/welcome/login", datasend, function (data) {
            rec = JSON.parse(data);
            if (rec.Result.Email !== true) {
                markInputAs("login-email", DANGER, rec.Result.Email);
                $("#btn-login").prop("disabled", false);
            }
            else if (rec.Result.password !== true) {
                markInputAs("login-password", DANGER, rec.Result.password);
                $("#btn-login").prop("disabled", false);
            }
            else {
                markInputAs("login-email", SUCCESS);
                markInputAs("login-password", SUCCESS);
                var url = "/home/Index";
                window.location.href = url;
            }
        });
    }
}

function checkForEmptyFields(form) {
    var inputs = $("input[id^='" + form + "-']")
        .map(function () {
            if ($(this).val().length === 0)
                return $(this);
        }).get();

    //Returning true if all inputs have value
    if (inputs.length == 0)
        return true;

    //Focusing on first empty field
    var firstDangerInput = inputs[0][0].id;
    markInputAs(firstDangerInput, DANGER, "Input is required");
    $("#" + firstDangerInput).focus();
}

function checkForDangerFields() {
    var inputs = $(".input-field--danger")
        .map(function () {
            return $(this); 
        }).get();

    //Returning true if all inputs are valid
    if (inputs.length == 0)
        return true;

    //Focusing on first empty field
    var firstDangerInput = inputs[0][0].id;
    $("#" + firstDangerInput).focus();
}