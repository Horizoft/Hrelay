(function ($) {
    $.fn.popup = function (data) {
        var $this = $(this);

        function show(data) {
            var $background = $this;
            var $popup = $this.find("[data-section='popup']");

            if (data != null) $popup.bindView(data);

            $background.css("display", "block");
        }

        function hide() {
            var $popup = $this.find("[data-section='popup']");
            $popup.toggleClass("box-popup-success-leave");

            //console.log("popup leaving...");

            setTimeout(function () {
                var $background = $this;
                $background.css("display", "none");
                $popup.toggleClass("box-popup-success-leave");

                //console.log("popup leaving timeout...");

            }, 500);
        }

        $(this).on("click", "button", function () {
            var eventType = $(this).data("event-type");

            if ((eventType || '') != '') {
                $this.trigger({
                    type: eventType
                });
            }

            hide();
        });

        show(data);
    }

}(jQuery));