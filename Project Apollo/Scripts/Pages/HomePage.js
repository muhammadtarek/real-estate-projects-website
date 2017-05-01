$(window).ready(function() {
    var selectedPostId;

    //Deleting a project post
    $(".delete-btn").click(function deleteProject() {
        var projectContainer = $(this).parent().parent().parent().parent();
        var projectId = $(projectContainer).attr("id");

        //Deleting from database
        $.post("/home/deleteProject", { id: projectId }, function (operationResults) {
            success = JSON.parse(operationResults);
            if (success.opertaion)
                $("#" + projectId).remove();
        });
    });

    //Editing a project post
    $(".edit-btn").click(function editProject() {
        var projectContainer = $(this).parent().parent().parent().parent();
        selectedPostId = $(projectContainer).attr("id");
        $("#customer-form").attr("formAction", "edit");

        //Getting project details
        $.post("/home/getProject", {projectId : selectedPostId }, function (projectData) {
            projectDetails = JSON.parse(projectData);
            $("#project-name").val(projectDetails.Name);
            $("#project-description").val(projectDetails.Description);
        });
    });
});