﻿//Taking the photo from file uploader and preview it
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
                $("#btn-sinup").html("Signing up...");
                markInputAs("signup-email", SUCCESS, "");
                var url;
                if (rec.result.nav)
                    url = "/home/Index";
                else
                    url = "/profile/index"
                window.location.href = url;
            }
        });
    }
}

$("#btn-signup").click(signUp);