$(document).ready(function () {
    $(".update01").slice(0, 4).fadeIn();
    $(".loadMore").click(function () {
        $(".update01").slice().fadeIn();
        $(this).fadeOut();
    });
});