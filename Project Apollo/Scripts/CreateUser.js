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
                var url = "/home/Index";
                window.location.href = url;
            }
        });
    }
}

$("#btn-signup").click(signUp);