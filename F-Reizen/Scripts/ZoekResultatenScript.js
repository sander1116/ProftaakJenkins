
$(window).ready(function () {
    if ($('#Id').val() == 'Zomer') {
        $("#zomer").addClass("active");
    } else if ($('#Id').val() == 'Winter') {
        $("#winter").addClass("active");
    }
});
