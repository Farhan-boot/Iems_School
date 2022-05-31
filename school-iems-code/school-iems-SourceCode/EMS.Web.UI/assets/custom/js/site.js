$(document).ready(function () {
    //$('.nav-pills, .nav-tabs').tabdrop();
    /*for SIDEBAR MENU*/
    var hash = window.location.hash;
    var url = window.location.href.replace(hash, "");
  

    $(".sidebar-menu li").removeClass("open");
    // Will only work if string in href matches with location
    $('.sidebar-menu li a[href="' + url + '"]').parent().addClass('active');
    // Will also work for relative and absolute hrefs
    $('.sidebar-menu li a').filter(function () {
        return this.href == url;
    }).parent().addClass('active');

    /*for TAB*/
    $('ul.nav-tabs > li > a').click(function (e) {
        e.preventDefault();
        $(this).tab('show');
    });
    // store the currently selected tab in the hash value
    // for bootstrap 3 use 'shown.bs.tab', for bootstrap 2 use 'shown' in the next line
    $("ul.nav-tabs > li > a").on("shown.bs.tab", function (e) {
        var id = "#" + $(e.target).attr("href").substr(1);
        if (history.pushState) {
            history.pushState(null, null, id);
            //alertInfo(id)
        }
        else {
            window.location.hash = id;
        }
        //var scrollmem = $('html,body').scrollTop();
        //window.location.hash = id;
        //autoscroll = false;
        //$('html,body').scrollTop(scrollmem);
    });
    //bootstrap tab active onload
    url = window.location.href;
    var activeTab = url.substring(url.indexOf("#") + 1);
    activeTab = activeTab.replace('/','');
    //$(".tab-pane").removeClass("active in");
    //$("#" + activeTab).addClass("active in");
    $('a[href="#' + activeTab + '"]').tab('show');
   
    bindMoveFocusOnEnter();//focus next on enter, not work with ng-repeated 
    /**
     * http://xdsoft.net/jqplugins/datetimepicker/
     * This will work with eight04/angular-datetime
     * https://github.com/eight04/angular-datetime
     */
    //added to maincontroller.js
    //datetime="dd/MM/yyyy HH:mm"
    //$('#datepicker').datetimepicker({
    //    format: 'd/m/Y',
    //    timepicker: false
    //});
    //$('#timepicker').datetimepicker({
    //    //formatTime: 'H:i',
    //    format:'H:i',
    //    timepicker: true,
    //    step: 10,
    //    datepicker: false
    //});
    //$("#datetimepicker").datetimepicker({
    //    //value:dateFormat(this.val, "dd/mm/yyyy"),//'2015/04/15 05:03',
    //    //formatTime: 'H:i',
    //    //formatDate: 'd.m.Y',
    //    //formatDate: 'Y/m/d',
    //    format: 'd/m/Y H:i',
    //    timepicker: true,
    //    step: 15,
    //    //dayOfWeekStart: 1,
    //    //lang: 'en'
    //    //disabledDates:['1986/01/08','1986/01/09','1986/01/10'],
    //    //startDate:	'1986/01/05'
    //});



    //get on bs tab chage
    //$('.nav-tabs a').on('shown.bs.tab', function (event) {
    //    var x = $(event.target).text();         // active tab
    //    var y = $(event.relatedTarget).text();  // previous tab
    //    alertInfo(x);
    //});

});
var log = function (obj)
{
    console.log(obj);
};

//$(document).ready(function(notify) {

//--Bootstrap Date Picker--
//$('.date-picker').datepicker();
//--Bootstrap Time Picker--
//$('.time-picker').timepicker();
//--Bootstrap Date Range Picker--
//$('.date-picker-range').daterangepicker();

//Notification toaster
var notify = function (message, position, timeout, theme, icon) {
    toastr.options.positionClass = 'toast-' + position;
    toastr.options.extendedTimeOut = 0; //1000;
    toastr.options.timeOut = timeout;
    toastr.options.closeButton = true;
    toastr.options.progressBar = progressBar;
    toastr.options.iconClass = icon + ' toast-' + theme;
    toastr[theme](message);

};
var alertSuccess = function (message) {
    toastr.success(message.toString(), 'Success', { positionClass: 'toast-top-center', timeOut: 10000, closeButton: true });
}
var alertWarning = function (message) {
    toastr.warning(message.toString(), 'Warning', { positionClass: 'toast-top-center', timeOut: 10000, closeButton: true });
}
var alertError = function (message) {
    toastr.error(message.toString(), "Attention Please !", { positionClass: "toast-top-center", timeOut: 1000, closeButton: true });
}
var alertInfo = function (message) {
    toastr.info(message.toString(), "Info.", { positionClass: "toast-top-center", timeOut: 10000, closeButton: true });
}
//replace whole Url Query String without reload 
//http://diveintohtml5.info/history.html
var changeUrlQuery = function(queryString) {
    if (history.pushState) {
        var newurl = window.location.protocol + "//" + window.location.host + window.location.pathname + '?' + queryString;
        window.history.pushState({ path: newurl }, '', newurl);
    }
}/**
  * add/update Url Query String without reload
  * http://stackoverflow.com/questions/5999118/add-or-update-query-string-parameter
  * @param {parameter} key 
  * @param {parameter} value 
  * @param {url to update, if not set for current url will used} 
  * @returns {new url} 
  */
function updateUrlQuery(key, value, url) {
    if (!url) url = window.location.href;
    var re = new RegExp("([?&])" + key + "=.*?(&|#|$)(.*)", "gi"),
        hash;
    if (re.test(url)) {
        if (typeof value !== 'undefined' && value !== null)
            url=url.replace(re, '$1' + key + "=" + value + '$2$3');
            //return url;
        else {
            hash = url.split('#');
            url = hash[0].replace(re, '$1$3').replace(/(&|\?)$/, '');
            if (typeof hash[1] !== 'undefined' && hash[1] !== null)
                url += '#' + hash[1];
            //return url;
        }
    }
    else {
        if (typeof value !== 'undefined' && value !== null) {
            var separator = url.indexOf('?') !== -1 ? '&' : '?';
            hash = url.split('#');
            url = hash[0] + separator + key + '=' + value;
            if (typeof hash[1] !== 'undefined' && hash[1] !== null)
                url += '#' + hash[1];
            //return url;
        }
        //else
        //    return url;
    }
 
    if (history.pushState) {
        window.history.pushState({ path: url }, '', url);
    }
}

/**
 * For example, if you have the URL:
http://www.example.com/?me=myValue&name2=SomeOtherValue
This then, vars=
{
    "me"    : "myValue",
    "name2" : "SomeOtherValue"
}
and you can do:
var me = getQueryString("me");
var name2 = getQueryString("name2");
 */
var getQueryString = function (key) {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars[key];
}
//use next-focus for angularJs
var bindMoveFocusOnEnter = function () {
    //$('input').keydown(function (e) {
    //    if (e.which === 13) {
    //        var index = $('input').index(this) + 1;
    //        $('input').eq(index).focus();
    //    }
    //});
    var select = $("input[type='text'], " +
            "input[type='number'], " +
            "input[type='email'], " +
            "input[type='password'], " +
            "input[type='checkbox']," +
            "input[type='radio'], " +
            "input[datetime], " +
            "textarea, " +
            ".next-focus, " +
            "select")
        .not("input[readonly]," +
        "textarea[readonly], " +
        "input[no-focus], " +
        ".no-focus, " +
        "select[disabled],input[disabled]"); //:first
    //log(select)
    select.eq(0).focus();

    select.bind("keydown", function (e) {
        var n = select.length;
        if (e.which == 13) { //Enter key

            if (this.tagName.toLowerCase() == "textarea") {
                //if textarea then retun
                //console.log("return");
                return;
            }
            e.preventDefault(); //Skip default behavior of the enter key

            var nextIndex = select.index(this) + 1; //select next element index
            if (nextIndex < n)
                select[nextIndex].focus(); //focus next element index
            else {
                //end of selection click on submit
                /*var submit = $("input[type='submit'], button[type='submit']"); //se
                if (submit.length > 0) {

                    submit.eq(0).click();
                } else {
                    //end of selection go to first element
                    var firstEle = $('input,button,textarea,select').not("input[readonly],textarea[readonly], select[disabled]").first();
                    firstEle[0].focus();
                }*/
                var firstEle = $('input,button,textarea,select').not("input[readonly],textarea[readonly], select[disabled]").first();
                firstEle[0].focus();
                //select[nextIndex - 1].blur();
            }
        }
    });
};

// disable mousewheel on a input number field when in focus
// (to prevent Cromium browsers change the value when scrolling)
$('form').on('focus', 'input[type=number]', function (e) {
    $(this).on('mousewheel.disableScroll', function (e) {
        e.preventDefault()
    })
})
$('form').on('blur', 'input[type=number]', function (e) {
    $(this).off('mousewheel.disableScroll')
})
/* here's the code if u want to use plain javascript
        function setActive() {
          aObj = document.getElementById('nav').getElementsByTagName('a');
          for(i=0;i<aObj.length;i++) { 
            if(document.location.href.indexOf(aObj[i].href)>=0) {
              aObj[i].className='active';
            }
          }
        }
        window.onload = setActive;
*/

$(window).bind("load", function () {
    /*Sets Themed Colors Based on Themes*/
    themeprimary = getThemeColorFromCss('themeprimary');
    themesecondary = getThemeColorFromCss('themesecondary');
    themethirdcolor = getThemeColorFromCss('themethirdcolor');
    themefourthcolor = getThemeColorFromCss('themefourthcolor');
    themefifthcolor = getThemeColorFromCss('themefifthcolor');
});


var collapseSidebar = function () {
    if (!$('#sidebar').is(':visible'))
        $("#sidebar").toggleClass("hide");
    $("#sidebar").toggleClass("menu-compact");
    $(".sidebar-collapse").toggleClass("active");
    b = $("#sidebar").hasClass("menu-compact");

    if ($(".sidebar-menu").closest("div").hasClass("slimScrollDiv")) {
        $(".sidebar-menu").slimScroll({ destroy: true });
        $(".sidebar-menu").attr('style', '');
    }
    if (b) {
        $(".open > .submenu")
            .removeClass("open");
    } else {
        if ($('.page-sidebar').hasClass('sidebar-fixed')) {
            var position = (readCookie("rtl-support") || location.pathname == "/index-rtl-fa.html" || location.pathname == "/index-rtl-ar.html") ? 'right' : 'left';
            $('.sidebar-menu').slimscroll({
                height: 'auto',
                position: position,
                size: '3px',
                color: themeprimary
            });
        }
    }
}