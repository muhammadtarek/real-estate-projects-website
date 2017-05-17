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
    //Accept Request PM
    $(".accept-request-btn").click(function () {
        var applyContainer = $(this).parent().parent().parent();
        var pmId = $(applyContainer).attr("id");
        var projectId = $(applyContainer).attr("project-id");
        $.post("/profile/Customer_assignProjectToPM", {
            PM_ID: pmId,
            projectID: projectId
        }, function () {
            showSnackbar("Request Accepted");
            $(applyContainer).remove();
            //$(applyContainer).attr("project-id");
        });
    });
    //decline Request PM
    $(".decline-request-btn").click(function () {
        var applyContainer = $(this).parent().parent().parent();
        var pmId = $(applyContainer).attr("id");
        var projectId = $(applyContainer).attr("project-id");
        $.post("/profile/declineApplyer", {
            PM_ID: pmId,
            projectID: projectId
        }, function () {
            showSnackbar("Request Declined");
            $(applyContainer).remove();
        });
    });
    //leave project
    $(".leave-project-btn").click(function () {
        var applyContainer = $(this).parent().parent().parent().parent();
        var projectId = $(applyContainer).attr("id");
        $.post("/profile/leaveProject", {
            projectId: projectId
        }, function () {
            showSnackbar("Leaved Success");
            $(applyContainer).remove();
        });
    });
});