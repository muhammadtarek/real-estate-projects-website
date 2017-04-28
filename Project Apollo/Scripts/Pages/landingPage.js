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

    /*Click button ajax part*/
    $("#btn-signup").click(signUp);
    $("#btn-login").click(login);
});

/* Ajax signUp */
function signUp() {
    var email = getInputValue("signup-email");
    var name = getInputValue("signup-name");
    var phone = getInputValue("signup-phone");
    var password = getInputValue("signup-password");
    var userrole = getInputValue("signup-userrole");
    var bio = getInputValue("signup-bio");
    var filesSelected = $("#photo-preview").attr('src');
   //console.log(atob(filesSelected.replace(/^data:image\/[a-z]+;base64,/, "")));
    var datasend = {
        userPicture: filesSelected,
        name: name,
        email: email,
        password: password,
        phoneNumber: phone,
        Desciption: bio,
        userType : userrole
    }
    $.post("/welcome/signUp", datasend, function (data) {
        rec = JSON.parse(data);
        console.log(rec);
        if (rec.result.email !== true) {
            markInputAs("signup-email", DANGER, rec.result.email);
        }
        else {
            markInputAs("signup-email", SUCCESS, "");
            var url = "/home/Index?id=" + rec.user.id;
            window.location.href = url;
        }
    });
}

/* Ajax login */
function login() {
    var email = getInputValue("login-email");
    var password = getInputValue("login-password");
    var datasend = {
        email: email,
        password: password,
    }
    $.post("/welcome/login", datasend, function (data) {
        rec = JSON.parse(data);
        if(rec.Result.Email !== true){
            markInputAs("login-email", DANGER, rec.Result.Email);
        }
        else if (rec.Result.password !== true) {
            markInputAs("login-password", DANGER, rec.Result.password);
        }
        else{
            markInputAs("login-email", SUCCESS);
            markInputAs("login-password", SUCCESS);
            var url = "/home/Index?id=" + rec.user.id;
            window.location.href = url;
        }
    });
}