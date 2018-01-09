
$(".radiobuttons").change(function () {
    if ($(".rButton1").is(':checked')) {
        $(".rButton4").attr('disabled', false);
        $(".rButton5").attr('disabled', true);
        $(".rButton6").attr('disabled', true);
        $(".rButton4").prop('checked', true);
    }
    if ($(".rButton2").is(':checked')) {
        $(".rButton4").attr('disabled', true);
        $(".rButton5").attr('disabled', false);
        $(".rButton6").attr('disabled', true);
        $(".rButton5").prop('checked', true);
    }
    if ($(".rButton3").is(':checked')) {
        $(".rButton4").attr('disabled', true);
        $(".rButton5").attr('disabled', false);
        $(".rButton6").attr('disabled', false);
        $(".rButton5").prop('checked', true);
    }
}
);


$(window).ready(function () {
    if ($('#Id').val() == 'Zomer') {
        $("#zomer").addClass("active");
    } else if ($('#Id').val() == 'Winter') {
        $("#winter").addClass("active");
    }
})
