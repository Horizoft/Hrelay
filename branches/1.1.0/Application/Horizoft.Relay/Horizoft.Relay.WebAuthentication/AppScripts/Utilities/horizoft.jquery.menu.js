(function ($) {
    $.fn.loadMenu = function (menuUrl, highlightStyle) {
        return this.each(function () {

            function getMenuSource() {
                if (($this.data("topmenuurl") || '') != '') return $this.data("topmenuurl");
                if (($this.data("menuurl") || '') != '') return $this.data("menuurl");
                return "";
            }

            function setSelectedItem() {
                if ((($cell || '') != '') && $cell.length > 0) {
                    $cell.removeClass();
                    $cell.addClass(highlightStyle);
                    //$cell.find("a").addClass(highlightStyle);
                }
            }

            function getSelectedItem() {
                selectedItem = (($this.data("selecteditem") || '') != '') ? $this.data("selecteditem") : -1;

                if (selectedItem > 0) {
                    return $this.find("li").eq(selectedItem - 1);
                }
            }

            function getMatchedUrl() {
                var hrefs = window.location.pathname.split('/');
                var href = hrefs[hrefs.length - 1];
                var $href = $this.find("a[href$='" + href + "']");
                var $cell = $href.closest("td");

                return $cell;
            }

            var $this = $(this);
            if (menuUrl == "") menuUrl = getMenuSource()
            if (menuUrl == "") return;

            var html = $.loadHtml(menuUrl);
            $(this).html(html);

            var selectedItem = -1;
            var $cell = getMatchedUrl();
            if ($cell.length == 0) $cell = getSelectedItem();
            setSelectedItem();

        });
    }

    //$.fn.loadLeftMenu = function (menuUrl, selectedItem) {
    //    return this.each(function () {

    //        function setSelectedItem() {
    //            if (($li || '') != '')
    //                $li.find("a").css({ "color": "#329ED3" });
    //        }

    //        function getSelectedItem() {
    //            selectedItem = (($this.data("selecteditem") || '') != '') ? $this.data("selecteditem") : -1;

    //            if (selectedItem > 0) {
    //                return $this.find("li").eq(selectedItem - 1);
    //            }
    //        }

    //        function getMatchedUrl() {
    //            //var hrefs = window.location.pathname.split('/');
    //            var href = window.location.pathname;
    //            //var href = hrefs[hrefs.length - 1];
    //            var $href = $this.find("a[href='" + href + "']");
    //            var $li = $href.closest("li");

    //            return $li;
    //        }

    //        var html = $.loadHtml(menuUrl);
    //        var $this = $(this);
    //        var selectedItem = -1;
    //        $(this).html(html);

    //        var $li = getMatchedUrl();
    //        if ($li.length == 0) $li = getSelectedItem();
    //        setSelectedItem();

    //    });
    //}

    $.fn.loadLeftMenu = function (menuUrl, selectedItem) {
        return this.each(function () {

            function setSelectedItem($li) {
                if (($li || '') != '') {
                    $li.css({"background": "rgba(238, 238, 238, 1)", "border-left-color": "rgba(255, 140, 0, 1)" ,"border-left-style": "solid"});
                    $li.find("a").css({ "color": "rgba(33, 33, 33, 1)" });
                }


            }

            function getSelectedItem() {
                selectedItem = (($this.data("selecteditem") || '') != '') ? $this.data("selecteditem") : -1;

                if (selectedItem > 0) {
                    return $this.find("li").eq(selectedItem - 1);
                }
            }

            function getMatchedUrl() {
                var hrefs = window.location.pathname.split('/');
                var href = hrefs[hrefs.length - 1];
                var $href = $this.find("a[href$='" + href + "']");
                var $li = $href.closest("li[data-menu-item]");
                //if (($li || '') == '')
                $li = $href.closest("td[data-menu-item]");

                if ($href.length === 0)
                    $li = $('tr:first td[data-menu-item]'); 
                return $li;
            }

            var html = $.loadHtml(menuUrl);
            var $this = $(this);
            var selectedItem = -1;
            $(this).html(html);

            var $li = getMatchedUrl();
            //if ($li.length == 0) $li = getSelectedItem();
            setSelectedItem($li);

        });
    }

    $.fn.setSelectedLeftMenu = function () {
        return this.each(function () {

            function setSelectedItem($li) {
                if (($li || '') != '') {
                    $li.css({ "background": "rgba(238, 238, 238, 1)", "border-left-color": "rgba(255, 140, 0, 1)", "border-left-style": "solid" });
                    $li.find("a").css({ "color": "rgba(33, 33, 33, 1)" });
                }

            }

            function getSelectedItem() {
                selectedItem = (($this.data("selecteditem") || '') != '') ? $this.data("selecteditem") : -1;

                if (selectedItem > 0) {
                    return $this.find("li").eq(selectedItem - 1);
                }
            }

            function getMatchedUrl() {
                var hrefs = window.location.pathname.split('/');
                var href = hrefs[hrefs.length - 1];
                var $href = $this.find("a[href$='" + href + "']");
                var $li = $href.closest("li[data-menu-item]");
                //if (($li || '') == '')
                $li = $href.closest("td[data-menu-item]");

                if ($href.length === 0)
                    $li = $('tr:first td[data-menu-item]');
                return $li;
            }

            //var html = $.loadHtml(menuUrl);
            var $this = $(this);
            var selectedItem = -1;
            //$(this).html(html);

            var $li = getMatchedUrl();
            //if ($li.length == 0) $li = getSelectedItem();
            setSelectedItem($li);

        });
    }

    $.fn.loadTopMenu = function (menuUrl) {
        return this.each(function () {

            function setSelectedMenu($this) {
                var hrefs = window.location.pathname.split('/');
                var href = hrefs[hrefs.length - 1];
                var $href = $this.find("a[href='" + href + "']");
                var $li = $href.closest("li");

                $li.addClass("selectedTopMenu");
            }

            var html = $.loadHtml(menuUrl);
            $(this).html(html);

            setSelectedMenu($(this));
        });
    }

}(jQuery));