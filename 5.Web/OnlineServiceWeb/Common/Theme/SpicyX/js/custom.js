/** 
  * Template Name: SpicyX
  * Version: 1.0  
  * Template Scripts
  * Author: MarkUps
  * Author URI: http://www.markups.io/

  Custom JS
  

  1. FIXED NAVBAR 
  2. TOP SLIDER
  3. ABOUT US (SLICK SLIDER) 
  4. DATEPICKER
  5. SHEF SLIDER (SLICK SLIDER)
  6. TESTIMONIAL SLIDER (SLICK SLIDER)
  7. COUNTER
  8. MIXIT FILTER (FOR GALLERY)
  9. FANCYBOX (FOR PORTFOLIO POPUP VIEW) 
  10. MENU SMOOTH SCROLLING
  11. HOVER DROPDOWN MENU
  12. SCROLL TOP BUTTON
  13. PRELOADER  

  
**/



jQuery(function ($) {

    /* ----------------------------------------------------------- */
    /*  7. COUNTER
    /* ----------------------------------------------------------- */

    //jQuery('.counter').counterUp({
    //    delay: 10,
    //    time: 1000
    //});

    /* ----------------------------------------------------------- */
    /*  8. MIXIT FILTER (FOR GALLERY) 
    /* ----------------------------------------------------------- */

    jQuery(function () {
        jQuery('#mixit-container').mixItUp();
    });

    /* ----------------------------------------------------------- */
    /*  9. FANCYBOX (FOR PORTFOLIO POPUP VIEW) 
    /* ----------------------------------------------------------- */

    jQuery(document).ready(function () {
        jQuery(".fancybox").fancybox();
    });

    /* ----------------------------------------------------------- */
    /*  10. MENU SMOOTH SCROLLING
    /* ----------------------------------------------------------- */

    //MENU SCROLLING WITH ACTIVE ITEM SELECTED

    // Cache selectors
    var lastId,
    topMenu = $(".mu-main-nav"),
    topMenuHeight = topMenu.outerHeight() + 13,
    // All list items
    menuItems = topMenu.find("a"),
    // Anchors corresponding to menu items
    scrollItems = menuItems.map(function () {
        try {
            var item = $($(this).attr("href"));
            if (item.length) { return item; }
        } catch (e) { }
    });

    // Bind click handler to menu items
    // so we can get a fancy scroll animation
    menuItems.click(function (e) {
        var href = $(this).attr("href"),
            offsetTop = href === "#" ? 0 : $(href).offset().top - topMenuHeight + 32;
        jQuery('html, body').stop().animate({
            scrollTop: offsetTop
        }, 1500);
        jQuery('.navbar-collapse').removeClass('in');
        e.preventDefault();
    });

    // Bind to scroll
    jQuery(window).scroll(function () {
        // Get container scroll position
        var fromTop = $(this).scrollTop() + topMenuHeight;

        // Get id of current scroll item
        var cur = scrollItems.map(function () {
            if ($(this).offset().top < fromTop)
                return this;
        });
        // Get the id of the current element
        cur = cur[cur.length - 1];
        var id = cur && cur.length ? cur[0].id : "";

        if (lastId !== id) {
            lastId = id;
            // Set/remove active class
            menuItems
              .parent().removeClass("active")
              .end().filter("[href=#" + id + "]").parent().addClass("active");
        }
    })

    /* ----------------------------------------------------------- */
    /*  11. HOVER DROPDOWN MENU
    /* ----------------------------------------------------------- */

    // for hover dropdown menu
    jQuery('ul.nav li.dropdown').hover(function () {
        
        jQuery(this).find('.dropdown-menu').stop(true, true).delay(200).fadeIn(200);
    }, function () {
        
        jQuery(this).find('.dropdown-menu').stop(true, true).delay(200).fadeOut(200);
    });


    /* ----------------------------------------------------------- */
    /*  12. SCROLL TOP BUTTON
    /* ----------------------------------------------------------- */

    //Check to see if the window is top if not then display button

    jQuery(window).scroll(function () {
        if (jQuery(this).scrollTop() > 300) {
            jQuery('.scrollToTop').fadeIn();
        } else {
            jQuery('.scrollToTop').fadeOut();
        }
    });

    //Click event to scroll to top

    jQuery('.scrollToTop').click(function () {
        jQuery('html, body').animate({ scrollTop: 0 }, 800);
        return false;
    });

    /* ----------------------------------------------------------- */
    /*  13. PRELOADER
    /* ----------------------------------------------------------- */

    jQuery(window).load(function () { // makes sure the whole site is loaded      
        jQuery('#aa-preloader-area').delay(300).fadeOut('slow'); // will fade out      
    })


});

