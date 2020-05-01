jQuery(function ($) {
    
    if (window.location.href.includes('/Offers/Dashboard')) {
        $('body').addClass('is-front');
    }

    /** Wrap second word of product tabs **/
    if ($('.product-tabs-wrapper').length) {
        $('.product-tabs-wrapper nav a').each(function () {

            let text = $(this).text().split(' ');
            let n = 1; // Second word is target.
            let t = 'span'; // HTML wrapper element.

            if (n >= text.length) { return; }

            text[n] = '<' + t + '>' + text[n] + ' </' + t + '>';

            $(this).html(text.join(' '));

        });
    }

    /** Sidebar configuration **/
    let isMobile = window.matchMedia("only screen and (max-width: 760px)").matches;
    $(".sidebar-dropdown > a").click(function () {
        $(".sidebar-submenu").slideUp(200);
        if (
            $(this)
                .parent()
                .hasClass("active")
        ) {
            $(".sidebar-dropdown").removeClass("active");
            $(this)
                .parent()
                .removeClass("active");
        } else {
            $(".sidebar-dropdown").removeClass("active");
            $(this)
                .next(".sidebar-submenu")
                .slideDown(200);
            $(this)
                .parent()
                .addClass("active");
        }
    });

    $("#close-sidebar").click(function () {
        $(".page-wrapper").removeClass("toggled");
    });

    $("#show-sidebar").click(function () {
        $(".page-wrapper").addClass("toggled");
    });

    // If we use mobile device, we will initially hide the menu.
    if (isMobile) {
        $(".page-wrapper").removeClass("toggled");
    }
});