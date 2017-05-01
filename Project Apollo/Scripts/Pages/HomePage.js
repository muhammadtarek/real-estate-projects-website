$(window).ready(function() {
    var selectedPost;

    $(".delete-btn").click(function () {
        var projectContainer = $(this).parent().parent().parent().parent();
        deleteProject($(projectContainer).attr("id"));
    });
});

function deleteProject(projectId) {
    $("#" + projectId).remove();
}