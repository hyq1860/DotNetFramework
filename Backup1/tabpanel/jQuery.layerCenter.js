(function ($) {
    $.fn.extend({
        layerCenter: function () {
            var obj = $(this);
            obj.css({
                position: "absolute",
                top: "50%",
                left: "50%",
                marginLeft: -obj.width() / 2,
                marginTop: -obj.height() / 2
            });
        }
    });
})(jQuery);