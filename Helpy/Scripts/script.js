$(document).ready(function() {
    $(".donate_child_click").click(function () {
        $("#donate_to_name").text("to " + $(this).children(".list-group-item-heading").html());
        var mi = $(this).attr("childId");
        $("#ContentPlaceHolder1_lblDonateToId").text(mi);
        activaTab(2);
    });

    $(".btn_title_donation_item").click(function() {
        $("#btn_title_donation").html($(this).text() + " <span class=\"caret\"></span>");
    });

    $(".btn_card_donation_item").click(function () {
        $("#btn_card_donation").html($(this).text() + " <span class=\"caret\"></span>");
    });

    $(".datepicker").datetimepicker({
        format: 'mm/dd/yyyy',
        weekStart: 1
    });
});
function activaTab(tab) {
    $('.nav-tabs a[href="#tab-' + tab + '"]').tab('show');
};