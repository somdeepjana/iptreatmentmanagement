
$(function () {
    // initiating data table
    $(".c-datatable").DataTable({
        searching: false
    });

    // starting toast notification at page load
    $(".toast").toast("show");
});
