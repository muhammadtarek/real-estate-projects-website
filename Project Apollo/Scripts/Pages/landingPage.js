$(window).ready(function () {
    /* ACTION LISENTERS */
    $("#login-btn").click(function () {
        showForm("login-form");
    });

    $("#signup-btn").click(function () {
        showForm("signup-form");
    });

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
    $("#btn-login").click(login);
});

/* FUNCTIONS */
function login() {
    if (checkForEmptyFields("login") && checkForDangerFields()) {
        $("#btn-login").prop("disabled", true);
        $("#btn-login").html("Logging in...");

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