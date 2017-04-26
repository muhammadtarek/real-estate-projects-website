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
  console.log(phoneNumber.length != 11);
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