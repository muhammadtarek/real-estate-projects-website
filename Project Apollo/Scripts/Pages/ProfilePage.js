$(window).ready(function () {
    $("#add-user").click(function () {
        showForm("signup-form");
    });
    //Deleting current user
    //Approve post
    $(".approve-btn").click(function () {
        var projectContainer = $(this).parent().parent().parent().parent();
        var selectedProjectId = $(projectContainer).attr("id");
        $.post("/profile/approveProject", {
            projectId: selectedProjectId,
        }, function () {
            showSnackbar("Project approved");
            $(projectContainer).remove();
        });
    });
    $(".decline-btn").click(function () {
        var projectContainer = $(this).parent().parent().parent().parent();
        var selectedProjectId = $(projectContainer).attr("id");
        $.post("/profile/declineProject", {
            projectID: selectedProjectId,
        }, function () {
            showSnackbar("Project declined");
            $(projectContainer).remove();
        });
    });
});