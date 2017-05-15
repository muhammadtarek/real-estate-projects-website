/* VALIDATION METHODS */
//Text
function validateText(text) {
  var re = /^[a-z A-Z]*$/
  return re.test(text);
  
}

//Email
function validateEmail(email) {
  var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
  return re.test(email);
}

//Phone number
function validatePhoneNumber(phoneNumber) {
  if (phoneNumber.length != 11 || isNaN(phoneNumber)) {
    return false;
  }
}

//Password
function validatePassword(password) {
  var re = /^(?:(?=.*[a-z])(?:(?=.*[A-Z])(?=.*[\d\W])|(?=.*\W)(?=.*\d))|(?=.*\W)(?=.*[A-Z])(?=.*\d)).{8,}$/;
  return re.test(password);
}

// TODO date and time validation

function checkForEmptyFields(form) {
    if (form === "") {
        var inputs = $("input")
            .map(function () {
                if ($(this).val().length === 0)
                    return $(this);
            }).get();
    } else {
        var inputs = $("input[id^='" + form + "-']")
            .map(function () {
                if ($(this).val().length === 0)
                    return $(this);
            }).get();
    }
    
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