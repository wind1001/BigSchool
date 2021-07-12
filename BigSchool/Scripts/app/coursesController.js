var CousesController = function () {
    var button;
    var init = function () {
        $(".js-toggle-attendance").click(toggleAttendance);
    };
};
var toggleAttendance = Function(e){
    button = $(e.target);
if (button.hasClass("btn-default")) {
    createAttendance();
}
else {
    deleteAttendance();
}
};
var createAttendance = function () {
    $.post("/ api / attedances", { courseId: button.attr("data-course-id") }).done(done).fail(fail);
};
var deleteAttendance = function () {
    $.ajax({
        url: "/api/attedances" + button.attr("data-course-id", method: "Delete")
    })
        .done(done).fail(fail);
};
var done