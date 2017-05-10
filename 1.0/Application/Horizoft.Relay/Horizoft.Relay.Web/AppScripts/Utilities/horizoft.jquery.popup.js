(function ($) {
    $.fn.popup = function (configurations) {
        var $popup = $(this);

        if (configurations == null) var configurations = {};

        if (!configurations.hasOwnProperty("status"))
            configurations.status = '';

        if (!configurations.hasOwnProperty("show"))
            configurations.show = false;

        if (configurations.status == "init") {
            var $overlay = $(document.createElement("div")).addClass("popup-overlay");
            var $parent = $popup.parent();

            $parent.append($overlay);
            $overlay.append($popup);
            $popup.addClass("popup-in");
        }

        if (configurations.show) {
            $popup.bindView({ popup_message: configurations.message });

            $popup.removeClass("popup-out");
            $popup.addClass("popup-in");
            var $overlay = $popup.closest("div.popup-overlay");
            $overlay.css("display", "block");
        }

        function hide() {
            // $popup.toggleClass("popup-out");
            $popup.removeClass("popup-in");
            $popup.addClass("popup-out");           
            
            setTimeout(function () {
                $overlay.css("display", "none");
                // $popup.toggleClass("popup-out");
                $popup.removeClass("popup-in");
                $popup.addClass("popup-out");

            }, 500);

        }

        $(this).on("click", "button", function () {
            //var eventType = $(this).data("event-type");
            //var eventName = configurations.eventName;

            if ($(this).data("popup-event") == true) {
                if ((configurations.eventName || '') != '') {
                    $popup.trigger({
                        type: configurations.eventName
                    });
                }
            }

            hide();
        });
    }

    // $.fn.popup = function (data) {
    //     var $this = $(this);

    //     function show(data) {
    //         var $background = $this;
    //         //var $popup = $this.find("[data-section='popup']");

    //         //if (data != null) $popup.bindView(data);

    //         $background.css("display", "block");
    //     }

    //     function hide() {
    //         var $popup = $this.find("[data-section='popup']");
    //         //$popup.toggleClass("box-popup-success-leave");
    //         $popup.toggleClass("popup-out");

    //         //console.log("popup leaving...");

    //         setTimeout(function () {
    //             var $background = $this;
    //             $background.css("display", "none");
    //             $popup.toggleClass("popup-out");

    //             //console.log("popup leaving timeout...");

    //         }, 500);
    //     }

    //     $(this).on("click", "button", function () {
    //         var eventType = $(this).data("event-type");

    //         if ((eventType || '') != '') {
    //             $this.trigger({
    //                 type: eventType
    //             });
    //         }

    //         hide();
    //     });

    //     show(data);
    // }

}(jQuery));