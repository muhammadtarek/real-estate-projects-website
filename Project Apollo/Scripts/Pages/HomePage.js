$(window).ready(function () {
    var selectedPostId;

    //Deleting a project post
    $(".delete-btn").click(function deleteProject() {
        var projectContainer = $(this).parent().parent().parent().parent();
        var projectId = $(projectContainer).attr("id");

        //Deleting from database
        $.post("/home/deleteProject", { id: projectId }, function (operationResults) {
            success = JSON.parse(operationResults);
            if (success.opertaion) {
                $("#" + projectId).remove();
                showSnackbar("Project deleted successfully");
            }
                
        });
    });

    //Editing a project post
    $(".edit-btn").click(function editProject() {
        var projectContainer = $(this).parent().parent().parent().parent();
        selectedPostId = $(projectContainer).attr("id");
        $("#customer-form").attr("formAction", "edit");
        $("#call-to-action").html("Update Project");
        $("#customer-form").attr("formaction", "edit");

        var projectName = $(projectContainer).find("h4.name")[0].innerHTML;
        var projectDescription = $(projectContainer).find("p.description")[0].innerHTML;

        $("#project-name").val(projectName);
        $("#project-description").val(projectDescription);
    });

    //Creating or updating project
    $("#call-to-action").click(function () {
        var projectName = getInputValue("project-name");
        var projectDescription = getInputValue("project-description");

        //Creating new project
        if ($("#customer-form").attr("formaction") === "create") {
            if (projectName === "" && projectDescription === "") {
                showSnackbar("Couldn't add empty project");
            } else {
                $.post("/home/createProject", {
                    name: projectName,
                    description: projectDescription
                }, function () {
                    $("#project-name").val("");
                    $("#project-description").val("");
                    showSnackbar("Project created successfully");
                });
            }
        } //Updating project
        else {
            $.post("/home/updateProject", {
                projectName: projectName,
                projectDescription: projectDescription,
                projectId: selectedPostId
            },
                function () {
                    var projectName = $("#project-name").val();
                    var projectDescription = $("#project-description").val();
                    $("#project-name").val("");
                    $("#project-description").val("");
                    $("#call-to-action").html("Create Project");
                    $("#customer-form").attr("formaction", "create");

                    $("#" + selectedPostId).find("h4.name")[0].innerHTML = projectName;
                    $("#" + selectedPostId).find("p.description")[0].innerHTML = projectDescription;
                    showSnackbar("Project updated successfully");
                });
        }
    });

    //Writing comment on posh
    // TODO

    //Apply to project
    // TODO
});

function createComment(comment) {
    // TODO
}