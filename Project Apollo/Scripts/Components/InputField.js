const HIDE = 0, DANGER = 0, SHOW = 1, SUCCESS = 1;

/* 
 Adapt text area height to the munmber of lines
*/
function auto_grow(element) {
    element.style.height = "40px";
    element.style.height = (element.scrollHeight) + "px";
}

/*
 Getting input field value
*/
function getInputValue(inputID) {
    if ($("#" + inputID).attr('type') == "radio" || $("#" + inputID).attr('type') == "checkbox") {
        var inputParent = $("#" + inputID).parent().parent();
        var inputsValues = [];
        inputParent.children("div").children(":checked").each(function (i) {
            inputsValues[i] = $(this).val();
        });
        return inputsValues;
    }

    return $("#" + inputID).val();
}

/*
 Marking input field as danger or success
 */
function markInputAs(inputID, markType, message) {
    var inputParent = $("#" + inputID).parent();
    $(inputParent).removeClass("input-field--success, input-field--danger");
    $(inputParent).addClass((markType == 1) ? "input-field--success" : "input-field--danger");
    $(inputParent).children('.error-message').text(message);
}

/*
 Showing dropdown menu
*/
$("input[type=text], input[type=password]").focus(function () {
    var inputParent = $(this).parent();

    if ($(this).hasClass("dropdown-input"))
        showDropdown(inputParent);
});

/*
 Hiding dropdown menu and validating other input fields
*/
$("input[type=text], input[type=password]").blur(function () {
    var inputParent = $(this).parent();

    var validationType = $(this).attr("validation-type");
    var inputValue = $(this).val();
    var inputId = $(this).attr("id");
    var validationResult = true;
    var errorMessage;

    //Checking if the input is empty
    if (inputValue.length === 0) {
        validationResult = false;
        errorMessage = "Input cann't be empty!";
        markInputAs(inputId, DANGER, errorMessage);
        return;
    }

    //Validating input values
    if (validationResult === true) {
        if (validationType === "text") {
            validationResult = validateText(inputValue);
            if (validationResult === false) errorMessage = "Special characters and numbers aren't allowed";
        } else if (validationType === "email") {
            validationResult = validateEmail(inputValue);
            if (validationResult === false) errorMessage = "Email isn't valid";
        } else if (validationType === "phone") {
            validationResult = validatePhoneNumber(inputValue);
            if (validationResult === false) errorMessage = "Phone number isn't valid";
        } else if (validationType === "password") {
            validationResult = validatePassword(inputValue);
            if (validationResult === false) errorMessage = "Password should have capital and small letters combined number or special characters";
        } else if (validationType === "repassword") {
            var password = getInputValue("signup-password");
            var repassword = getInputValue("signup-repassword");
            if (password != repassword) {
                validationResult = false;
                errorMessage = "Passwords doesn't match";
            }
        }
    }

    if (validationResult === false) {
        markInputAs(inputId, DANGER, errorMessage);
    } else {
        errorMessage = "";
        markInputAs(inputId, SUCCESS, errorMessage);
    }

    //Check if the mouse isn't hovering on the dropdown menu
    if ($(this).hasClass("dropdown-input"))
        if (!$(inputParent).children('.dropdown').is(':hover'))
            hideDropdown(inputParent);
});

/*
 Selecting item from the dropdown
 */
$(".dropdown li").click(function () {
    var inputParent = $(this).parent().parent();
    $(inputParent).children('input').val($(this).text());
    hideDropdown(inputParent);
});

/*
 Show/hide dropdown
 @para parent, send the parent container for the input field
 */
function showDropdown(parent) {
    $(parent).children('ul').css("display", "block");
}

function hideDropdown(parent) {
    $(parent).children('ul').css("display", "none");
}