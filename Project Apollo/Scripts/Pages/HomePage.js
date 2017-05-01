$(window).ready(function() {
    var selectedPostId;

    //Deleting a project post
    $(".delete-btn").click(function () {
        var projectContainer = $(this).parent().parent().parent().parent();
        deleteProject($(projectContainer).attr("id"));
    });

    //Editing a project post
    $(".edit-btn").click(function () {
        var projectContainer = $(this).parent().parent().parent().parent();
        selectedPostId = $(projectContainer).attr("id");
        $("#customer-form").attr("formAction", "edit");

        //Getting project details
        $.post("/home/getProject", {projectId : selectedPostId}, function (projectData) {
            projectDetails = JSON.parse(projectData);
            $("#project-name").val(projectDetails.Name);
            $("#project-description").val(projectDetails.Description);
        });
    });
});

function deleteProject(projectId) {
    $("#" + projectId).remove();
}
