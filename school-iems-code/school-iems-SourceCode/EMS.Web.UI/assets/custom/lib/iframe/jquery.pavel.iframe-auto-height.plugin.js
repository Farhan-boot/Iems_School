
/*
  Plugin: iframe autoheight jQuery Plugin
  Version: 1.1.0
  Author and Contributors
  ========================================
  Humayon Kabir Pavel (http://www.facebook.com/hkpavel)
*/
$(document).ready(function () {
    // Set specific variable to represent all iframe tags.
    //var iFrames = document.getElementsByTagName('iframe');
    //resize all iframe height every after 1 sec
    setInterval(function () {
        iResize();
    }, 1000);

    // Check if browser is Safari or Opera.
    if ($.browser.safari || $.browser.opera) {
        // Start timer when loaded.
        $('iframe').load(function () {
            setTimeout(iResize, 0);
        });

        // Safari and Opera need a kick-start.
        $("iframe").each(function () {
            var iSource = this.src;
            this.src = '';
            this.src = iSource;

        });
    }
    else {
        // For other good browsers.
        $('iframe').load(function () {
            // Set inline style to equal the body height of the iframed content.
            this.style.height = this.contentWindow.document.body.offsetHeight + 'px';
        }
			);
    }
}
);
// Resize heights.
function iResize() {
    // Iterate through all iframes in the page.
    $("iframe").each(function () {
        setIframeHeight(this);

    });
}
function setIframeHeight(iframe) {
    if (iframe) {
        var iframeWin = iframe.contentWindow || iframe.contentDocument.parentWindow;
        if (iframeWin.document.body) {
            var height = iframeWin.document.documentElement.scrollHeight || iframeWin.document.body.scrollHeight;
            //height=iframeWin.contentWindow.document.body.offsetHeight;
            if (height > 148) {
                iframe.height = height + "px";
                iframe.style.height = height + "px";
            }
            else {
                iframe.height = 200 + "px";
            }
            //alert(iframe.height);
        }
    }
};