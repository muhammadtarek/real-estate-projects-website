const HIDE = 0, DANGER = 0, SHOW = 1, SUCCESS = 1;

/* Adapt text area height to the munmber of lines */
function auto_grow(element) {
    element.style.height = "40px";
    element.style.height = (element.scrollHeight) + "px";
}

//Getting input field value
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
 Marking input field as success
 @para inputID
 @para markType, SUCCESS or dangers-color
 @para message
 */
function markInputAs(inputID, markType, message) {
  var inputParent = $("#" + inputID).parent();
  $(inputParent).removeClass("input-field--success, input-field--danger");
  $(inputParent).addClass((markType == 1) ? "input-field--success" : "input-field--danger");
  $(inputParent).children('.error-tooltip').text(message);
}

/*
 Input fields
 */
//When gaining focus
$("input[type=text], input[type=password]").focus(function () {
  //Selecting the label for this input field and changes it's color
  var inputParent = $(this).parent();

  if ($(this).hasClass("dropdown-input"))
    showDropdown(inputParent);
});

//When losing focus
$("input[type=text], input[type=password]").blur(function () {
  //Selecting the label for this input field and changes it's color
  var inputParent = $(this).parent();

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