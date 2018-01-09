
$('.btn.btn-toggle').on('click', function () {
    $('.btn.btn-toggle.active').removeClass('active');
    $(this).addClass('active');
});

$('button').on('click',
    function () {
        if ($('#zomer').hasClass('active')) {
            $("#Id").val("Zomer");
        } else if ($('#winter').hasClass('active')) {
            $("#Id").val("Winter");
        }
    });
