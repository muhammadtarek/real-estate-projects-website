$(window).load(function () {
	var formHeight = 0;

	var loginForm = "#login-form";
	var signUpForm = "#signup-form";

	$(loginForm).css("margin-top", "-435px");
	$(signUpForm).css("margin-top", "-639px");

    /* FUNCTIONS */
	function showForm(formId) {
		//Getting form height
		var form = "#" + formId;

		$(form + ", .popup-container").css("display", "block");
		setTimeout(function () {
			$(form).css("margin-top", "35px");

		}, 2);
	}

	function hideForm() {
		$(loginForm).css("margin-top", "-435px");
		$(signUpForm).css("margin-top", "-639px");

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
});
