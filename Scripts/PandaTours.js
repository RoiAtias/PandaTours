$(window).scroll(function () {
    if ($(".navbar").offset().top > 50) {
        $(".navbar-fixed-top").addClass("top-nav-collapse");
    } else {
        $(".navbar-fixed-top").removeClass("top-nav-collapse");
    }
});

// jQuery for page scrolling feature - requires jQuery Easing plugin
$(function () {
    $('a.page-scroll').bind('click', function (event) {
        var $anchor = $(this);
        $('html, body').stop().animate({
            scrollTop: $($anchor.attr('href')).offset().top
        }, 1500, 'easeInOutExpo');
        event.preventDefault();
    });
});

// Closes the Responsive Menu on Menu Item Click
$('.navbar-collapse ul li a').click(function () {
    $('.navbar-toggle:visible').click();
});



// When the document is loaded...
$(document).ready(function () {

    // Hide the toTop button when the page loads.
    $("#toTop").css("display", "none");

    // This function runs every time the user scrolls the page.
    $(window).scroll(function () {

        // Check weather the user has scrolled down (if "scrollTop()"" is more than 0)
        if ($(window).scrollTop() > 0) {

            // If it's more than or equal to 0, show the toTop button.
            console.log("is more");
            $("#toTop").fadeIn("slow");
        }
        else {
            // If it's less than 0 (at the top), hide the toTop button.
            console.log("is less");
            $("#toTop").fadeOut("slow");

        }
    });

    // When the user clicks the toTop button, we want the page to scroll to the top.
    $("#toTop").click(function () {

        // Disable the default behaviour when a user clicks an empty anchor link.
        // (The page jumps to the top instead of // animating)
        event.preventDefault();

        // Animate the scrolling motion.
        $("html, body").animate({
            scrollTop: 0
        }, "slow");

    });

});


