$(window).load(function () {
    
});

$(document).ready(function () {
    try {
        showLoader(false);
        //init
        resizeNGTableHeight('');
        resizeNGGridHeight();
        fullHeight();
        fullParentWidth();
        fullHeightParent();
       
    } catch (ex) {
    }
    //*register resize event on window resize
    var lastWindowHeight = $(window).height();
    var lastWindowWidth = $(window).width();
    $(window).resize(function () {
        try {
            if ($(window).height() != lastWindowHeight || $(window).width() != lastWindowWidth) {
                resizeNGTableHeight();
                resizeNGGridHeight();
                
                fullHeight();
                fullParentWidth();
                fullHeightParent();
            }
        } catch (ex) {
            showLoader(false);
        }
    });
    

});

function bindIframeClick() {
    var frames = $('iframe');
    console.log(frames);
    $('iframe').load(function () {
        alert('test');
        $(this).contents().find("body").on('click', function (event) {
            $("body").click(); alert('test');
        });
    });
}

//hide show page poader
function showLoader(bool) {
    if (bool) {
        $('#backOpacity').show();
        $('#dvLoading').show();
        //console.log('show');
    } else {
        $('#dvLoading').fadeOut(700, function () {
            $('#backOpacity').fadeOut(200);
        });
        //$('#backOpacity').fadeOut(1000);
        //console.log('hide');
    }

}
///height calculation 
/////////pavel
function getOffsetTopHeight2(el) {
    // yay readability
    var lx = 0, ly = 0;

    while (el != null) {
        //console.log(el);
        lx += el.offsetLeft;
        ly += el.offsetTop;
        el = el.offsetParent;
        //console.log('x:' + lx + ',' + 'y:' + ly);
    }
    //console.log('y:' + ly);
    return ly;
}
function resizeNGTableHeight(extraOffset) {
    var element = $('div.ng-table.ng-table-full-height');
    var height = 0;
    if (element.length > 0)
        element.each(function () {
            try {
                var ele = $(this).find("table.table")[0];
                var ofsetHeight = getOffsetTopHeight2(ele);
                var headerHeight = $(this).find("table.table>thead")[0].offsetHeight;
                
                var footerHeight = $(this).find(".ng-table-footer")[0].offsetHeight;
                console.log(footerHeight);
                if (extraOffset)
                    footerHeight = footerHeight - extraOffset;
                else {
                    footerHeight = footerHeight + 1;
                }
                height = ($(window).height() - (headerHeight + footerHeight + ofsetHeight));
                $(this).find("table.table>tbody").height(height);
            } catch (ex) {
            }

        });
}
function resizeNGGridHeight() {
    try {
        if ($('.ng-gridStyle').length > 0) {
            var extraOffsetHeight = 0;
            var footerPanel = $('.ng-gridStyle .ngFooterPanel');
            var topPanel = $('.ng-gridStyle .ngTopPanel');
            var viewPort = $('.ng-gridStyle .ngViewport');
            if (footerPanel)
                extraOffsetHeight += footerPanel[0].offsetHeight;
            if (topPanel)
                extraOffsetHeight += topPanel[0].offsetHeight;
            //console.log(extraOffsetHeight);
            var ofsetHeight = getOffsetTopHeight2($('.ng-gridStyle')[0]);

            var height = ($(window).height() - ofsetHeight);

            $('.ng-gridStyle').height(height);
            $('.ng-gridStyle').css({ 'height': height, 'width': '100%' });


            viewPort.css({ 'height': (height - extraOffsetHeight), 'width': '100%' });
            footerPanel.width('100%');
            topPanel.width('100%');

            //$('.gridStyle').parent().parent()[0].offsetHeight - $('#dynamicTab .nav-tabs')[0].offsetHeight;
            //$(window).height() - getOffsetTopHeight($('#dynamicTab .tab-content')[0]);
        }
    } catch (ex) {
    }
}

function fullParentWidth() {
    var element = $('.parent-width');

    if (element.length > 0)
        element.each(function () {
            $(this).width($(this.parentElement).width());
        });
}

function fullHeight() {
    var element = $('.full-height');
    var ofsetHeight = 0;
    var height = 0;
    if (element.length > 0)
        element.each(function () {

            ofsetHeight = getOffsetTopHeight2(this);
            height = ($(window).height() - ofsetHeight);
            $(this).height(height - 4);

        });
}
function fullHeightParent() {
    var element = $('.full-height-parent');
    if (element.length > 0)
        element.each(function () {
            $(this).height($(this.parentElement).height());
        });
}


/* Make center a element */

jQuery.fn.center = function (parent) {
    if (parent) {
        parent = this.parent();
    } else {
        parent = window;
    }
    var outerHeight = this.outerHeight();
    var outerWidthh = this.outerWidth();
    var parentHeight = $(parent).height();
    var parentWidthh = $(parent).width();
    var scrollTop = $(parent).scrollTop();
    var scrollLeft = $(parent).scrollLeft();
    var Top = ((parentHeight - outerHeight) / 2) + scrollTop;
    var Left = ((parentWidthh - outerWidthh) / 2) + scrollLeft;;

    this.css({
        "position": "fixed", //"absolute",
        "top": ((($(parent).height() - this.outerHeight()) / 2) + $(parent).scrollTop()),
        "left": ((($(parent).width() - this.outerWidth()) / 2) + $(parent).scrollLeft())
    });

    //    this.css("position", "fixed");
    //    this.css("top", ($(window).height() / 2) - (this.outerHeight() / 2));
    //    this.css("left", ($(window).width() / 2) - (this.outerWidth() / 2));
    return this;

};
