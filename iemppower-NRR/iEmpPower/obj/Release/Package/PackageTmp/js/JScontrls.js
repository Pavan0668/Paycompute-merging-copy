       jQuery(document).ready(function ($) {
           var alterClass = function () {
               var ww = document.body.clientWidth;
               /// alert(ww);
               if (ww < 600) {
                   $("#divbrdr").removeClass('divfr');
                   $('#canvas').hide();
                   $('#headspace').hide();
                   $('#footspace').hide();

               } else if (ww >= 401) {
                   $('#divbrdr').addClass('divfr');
                   $('#canvas').show();
                   $('#headspace').show();
                   $('#footspace').show();
               };
           };
           $(window).resize(function () {
               alterClass();
           });
           //Fire it when the page first loads:
           alterClass();
       });


$(function () {
    $(".txtDropDownwidth").focus(function () {
        $(this).attr("rel", $(this).attr("placeholder"));
        $(this).removeAttr("placeholder");
    });
    $(".txtDropDownwidth").blur(function () {
        $(this).attr("placeholder", $(this).attr("rel"));
        $(this).removeAttr("rel");
    });
});
