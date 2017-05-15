$(".nav-tab").click(function switchTabs() {
    $(".tab-active").removeClass("tab-active");
    $(this).addClass("tab-active");

    var selectedTab = $(this).attr("tab");
    $(".tab-view--active").removeClass("tab-view--active");
    $(".tab-view[tab-view='" + selectedTab + "']").addClass("tab-view--active");
});