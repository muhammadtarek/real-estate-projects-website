$(window).ready(function () {
    //Go to home page
    $("#home-tab").click(function () {
        window.location.href = "/home/index";
    });

    //Go to profile
    $("#profile-tab").click(function () {
        window.location.href = "/profile/index";
    });

    //Sign out
    $("#signout-btn").click(function () {
        window.location.href = "/welcome/signOut";
    });
});