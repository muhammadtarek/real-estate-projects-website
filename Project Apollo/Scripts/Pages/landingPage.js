$(window).load(function () {
    var formHeight = 0;

    var loginForm = "#login-form";
    var signUpForm = "#signup-form";

    $(loginForm).css("top", "-435px");
    $(signUpForm).css("top", "-639px");

    /* FUNCTIONS */
    function showForm(formId) {
        //Getting form height
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
    $("#signup-photo").change(function () {
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
});