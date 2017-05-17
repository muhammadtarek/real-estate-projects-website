$(window).ready(function () {
    $("#add-user").click(function () {
        showForm("signup-form");
    });
    //Deleting current user
    //Approve post
    $(".approve-project-btn").click(function () {
        var projectContainer = $(this).parent().parent().parent().parent();
        var selectedProjectId = $(projectContainer).attr("id");
        $.post("/profile/approveProject", {
            projectId: selectedProjectId,
        }, function () {
            showSnackbar("Project approved");
            $(projectContainer).remove();
        });
    });
    //Decline Post
    $(".decline-project-btn").click(function () {
        var projectContainer = $(this).parent().parent().parent().parent();
        var selectedProjectId = $(projectContainer).attr("id");
        $.post("/profile/declineProject", {
            projectID: selectedProjectId,
        }, function () {
            showSnackbar("Project declined");
            $(projectContainer).remove();
        });
    });
    //Accept Request
    $(".accept-invitation-btn").click(function () {
        var requestContainer = $(this).parent().parent();
        var selectedRequestId = $(requestContainer).attr("id");
        $.post("/profile/acceptRequest", {
            requestID: selectedRequestId,
        }, function () {
            showSnackbar("Request Accepted");
            $(requestContainer).remove();
        });
    });
    //Decline Request
    $(".decline-invitation-btn").click(function () {
        var requestContainer = $(this).parent().parent();
        var selectedRequestId = $(requestContainer).attr("id");
        $.post("/profile/deleteRequest", {
            requestID: selectedRequestId,
        }, function () {
            showSnackbar("Request Decline");
            $(requestContainer).remove();
        });
    });
});