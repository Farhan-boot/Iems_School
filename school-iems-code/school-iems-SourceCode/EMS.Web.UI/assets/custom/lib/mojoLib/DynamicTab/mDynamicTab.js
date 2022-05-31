/**
  Plugin: Bootstrap Dynamic tab loader with iframe
  Version: 2.1.0
  Author and Contributors
  ========================================
  Humayon Kabir Pavel (http://www.facebook.com/hkpavel)
*/
//this will hold currently focused tab
var currentTab;
var countTab = 0;
//initilize tabs
$(function () {
    //init
});
$(document).ready(function () {
    //disableHtmlBodyScroll();
    //register click event, when ever any tab is clicked this method will be call
    $('#dynamicTab .nav-tabs li').on("click", "a", function (e) {
        bindTabOnClick(this, e);
    });


    //*register resize event on window resize
    var lastWindowHeight = $(window).height();
    var lastWindowWidth = $(window).width();
    $(window).resize(function () {
        if ($(window).height() != lastWindowHeight || $(window).width() != lastWindowWidth) {
            //console.log('offsetHeight:' + window.document.body.offsetHeight);
            //console.log('last height:' + lastWindowHeight);
            //console.log('new height:' + $(window).height());
            lastWindowHeight = $(window).height();
            lastWindowWidth = $(window).width();
            resizeHeight();
        }
    });
    registerCloseEvent();
    resizeHeight();
});
//==================== dynamic tab by pavel==================
////open tab
function openItab(title, name, url) {
//    var tabHeader = $('#dynamicTab').find('.nav-tabs a');
//    var isExist = false;
//    tabHeader.each(function () {
//        var _name = this.hash;
//        if (('#' + name) == _name) {
//            isExist = true;
//        }
//    });

    // check unique
    var tabHeader = $('#dynamicTab .nav-tabs a[name=' + name + ']');
    //console.log(tabHeader);
    if (tabHeader.length > 0)
        showTab(name);
    else {
        tabHeader = $('#dynamicTab').find('.nav-tabs');
        var tabContent = $('#dynamicTab').find('.tab-content');

        if (tabHeader && tabContent) {
            //create tab header
            tabHeader.append('<li><a name="' + name + '" href="#' + name + '" onclick="bindTabOnClick(this)"> <span  name="tabTitle">' + title + '</span>&nbsp;<i class="fa fa-refresh" onclick="reloadItabByName(\''+ name +'\')"></i><i class="fa fa-close" onclick="closeItabByObj(this)" title="Close Tab"></i></a></li>');
            //create tab content body with inner ifreame
            var element = $('<div class="tab-pane content-Body" id="' + name + '"><iframe id="iframe' + name + '" src="' + url + '" style="margin: 0; padding: 0; border: 0; height: 100%;width: 100%;" frameborder="0"></iframe></div>');
            tabContent.append(element);
            resizeHeight();
            $(this).tab('show');
            showTab(name);
            //disableHtmlBodyScroll();
        }
    }
}
function renameItab(oldName, newName, newTitle) {
    var tabHeader = $('#dynamicTab .nav-tabs a[name=' + newName + ']');
    console.log ( tabHeader);
    if (tabHeader.length==0) {
        //select tab title
        var tabTitle = $('#dynamicTab .nav-tabs a[name=' + oldName + ']').find('span[name="tabTitle"]')[0];
        //rename tab title
        tabTitle.innerHTML = newTitle;

        //rename tab name attrib
        tabHeader = $('#dynamicTab .nav-tabs a[name=' + oldName + ']');
        tabHeader.attr({
            name: newName,
            href: '#' + newName
        });
        //change tab pane id with new name
        var tabContent = $('#dynamicTab .tab-content #' + oldName + '');
        tabContent.attr("id", newName);
    }
}
function changeItabLink(oldName, newName, newTitle,newUrl) {
    var tabHeader = $('#dynamicTab .nav-tabs a[name=' + newName + ']');
    console.log(tabHeader);
    if (tabHeader.length == 0) {
        //select tab title
        var tabTitle = $('#dynamicTab .nav-tabs a[name=' + oldName + ']').find('span[name="tabTitle"]')[0];
        //rename tab title
        tabTitle.innerHTML = newTitle;

        //rename tab name attrib
        tabHeader = $('#dynamicTab .nav-tabs a[name=' + oldName + ']');
        tabHeader.attr({
            name: newName,
            href: '#' + newName
        });
        //change tab pane id with new name
        var tabContent = $('#dynamicTab .tab-content #' + oldName + '');
        var ifreame = $(tabContent).find('iframe')[0];
        if (ifreame) {
            ifreame.src = newUrl;
        }
        tabContent.attr("id", newName);

       
    }
}
function openTabAjax(name, url) {
    name = name + (++countTab);
    $('.nav-tabs').append('<li><a href="#' + name + '" onclick="bindTabOnClick(this)"><button class="close closeTab" type="button" >x</button>' + name + '</a></li>');
    $('.tab-content').append('<div class="tab-pane" id="' + name + '" style=""></div>');

    loadUrlAjax("", url, "#" + name);
    //enableHtmlBodyScroll();
    $(this).tab('show');
    showTab(name);
    registerCloseEvent();

}
//This function will create a new tab here and it will load the url content in tab content div.
function loadUrlAjax(parms, url, loadDivSelector) {

    $("" + loadDivSelector).load(url, function (response, status, xhr) {
        if (status == "error") {
            var msg = "Sorry but there was an error getting details ! ";
            $("" + loadDivSelector).html(msg + xhr.status + " " + xhr.statusText);
            $("" + loadDivSelector).html("Load Ajax Content Here...");
        }
    });

}


//remove current selected tab
function closeSelectedItab() {
    if (!currentTab)
        currentTab = getCurrentItab();
    var tabContentId = currentTab.attr("href");
    if (tabContentId) {
        currentTab.parent().remove(); //remove li of tab
        //console.log(tabContentId + ' -> tab removed.');
        currentTab = $('#dynamicTab a:last'); //shifting current tab
        currentTab.tab('show'); // Select first tab
        $(tabContentId).remove(); //remove respective tab content
        resizeHeight();
    } else {
        { console.log('no tab found to remove!'); }
    }
}
//this method will register event on close icon on the tab..
function registerCloseEvent() {
    $(".closeTab").click(function () {
        closeItabByObj(this);
    });
}
function closeItabByObj(obj) {
    //there are multiple elements which has .closeTab icon so close the tab whose close icon is clicked
    var tabContentId = $(obj).parent().attr("href"); //get tab content div id
    $(obj).parent().parent().remove(); //remove li of tab
    currentTab = $('#dynamicTab .nav-tabs a:last'); //shifting current tab #dynamicTab .nav-tabs li
    //console.log(currentTab);
    currentTab.tab('show'); // Select first tab
    $(tabContentId).remove(); //remove respective tab content
    resizeHeight();
}
function reloadSelectedItab() {
    try {
        var tabContentId = getCurrentItab().attr("href");
        if (tabContentId) {
            var ifreame = $(tabContentId).find('iframe')[0];
            if (ifreame) {
                //alert(ifreame.id + ifreame.src);
                ifreame.src = ifreame.src;
                //ifreame.location.reload();
            }
        }
    }
    catch (e)
    { }
}

function reloadItabByName(name) {
    try {
        var tabContent = $('#dynamicTab .tab-content #' + name + '');

        if (tabContent) {
            var ifreame = $(tabContent).find('iframe')[0];
            if (ifreame) {
                ifreame.src = ifreame.src;
            }
        }
    }
    catch (e)
    { }
}
function reloadItab2(name, url) {
    try {
        var tabContent = $('#dynamicTab .tab-content #' + name + '');

        if (tabContent) {
            var ifreame = $(tabContent).find('iframe')[0];
            if (ifreame) {
                ifreame.src = url;
            }
        }
    }
    catch (e)
    { }
}





function bindTabOnClick(obj, e) {
    if (e)
        e.preventDefault();
    $(obj).tab('show');
    currentTab = $(obj); //shifting current tab
    //console.log('current tab -> ' + currentTab.attr("href"));
}
//shows the tab with passed content div id..paramter tabid indicates the div where the content resides
function showTab(tabId) {
    currentTab = $('#dynamicTab a[href="#' + tabId + '"]'); //shifting current tab
    //console.log(currentTab);
    currentTab.tab('show');
}
function resizeHeight() {

    var h = document.documentElement.clientHeight; //window.innerHeight; //Math.max(document.documentElement.clientHeight, window.innerHeight || 0);// window.document.body.offsetHeight
    //console.log($("#body").innerHeight());

    var height = h - getOffsetTopHeight($('#dynamicTab .tab-content')[0])-17;
    //alert( h+'-'+getOffsetTopHeight($('#dynamicTab .tab-content')[0])+'='+height);
    $('#dynamicTab .tab-pane').css({ 'height': (height), 'overflow-y': 'hidden' });
    //console.log(window.document.body.scrollHeight);
    //console.log(window.document.body.offsetHeight);
    //console.log(window.document.Height);

}





//this will return element from current tab
//example : if there are two tabs having  textarea with same id or same class name then when $("#someId") whill return both the text area from both tabs
//to take care this situation we need get the element from current tab.
function getElement(selector) {
    var tabContentId = currentTab.attr("href");
    return $("" + tabContentId).find("" + selector);

}
//return current active tab
function getCurrentItab() {
    if (!currentTab)
        currentTab = $('#dynamicTab a:first');
    return currentTab;
}


function getOffsetTopHeight(el) {
    // yay readability
    var lx = 0, ly = 0;

    while (el != null) {
        //console.log(el);
        lx += el.offsetLeft;
        ly += el.offsetTop;
        el = el.offsetParent;
        //console.log('x:' + lx + ',' + 'y:' + ly);
    }
    return ly;
}
function enableHtmlBodyScroll() {
    $("body").css("overflow", "auto");
    $("body").css("overflow-x", "auto");
    $("body").css("overflow-y", "auto");
    document.documentElement.style.overflow = 'auto'; // for Non IE Browser
    document.body.scroll = "yes"; // for IE Browser only
    //document.documentElement.style.overflowX = 'auto'; // horizontal scrollbar will be hidden
    //document.documentElement.style.overflowY = 'auto'; 
}
function disableHtmlBodyScroll() {
    $("body").css("overflow", "hidden");
    $("body").css("overflow-x", "hidden");
    $("body").css("overflow-y", "hidden");
    document.documentElement.style.overflow = 'hidden'; // for Non IE Browser
    document.body.scroll = "no"; // for IE Browser only
    //document.documentElement.style.overflowX = 'hidden'; // horizontal scrollbar will be hidden
    //document.documentElement.style.overflowY = 'hidden'; 
}
//=======extra function make Uniqe Id for tab

function openTabIframeUnique(name, url) {
    var tabHeader = $('#dynamicTab').find('.nav-tabs a');
    var isExist = false;
    var tab = getCurrentItab();  //currentTab.attr("href"); ;
    //tab.attr("href");
    tabHeader.each(function () {
        var _name = this.hash;
        if (('#' + name) == _name) {
            isExist = true;
        }
    });
    if (!isExist)
        //always call makeUniqueId=false from this method
        openTabIframe(name, url, false);
    else {
        showTab(name);
    }
}

function openTabIframe(name, url, makeUniqueId) {
    var tabHeader = $('#dynamicTab').find('.nav-tabs');
    var tabContent = $('#dynamicTab').find('.tab-content');

    if (makeUniqueId)
        name = name + (++countTab); //making unique name
    if (tabHeader && tabContent) {
        //create tab header
        tabHeader.append('<li><a name="' + name + '" href="#' + name + '" onclick="bindTabOnClick(this)"><button class="close closeTab" type="button" onclick="closeItabByObj(this)">x</button>' + name + '</a></li>');
        //create tab content body with inner ifreame
        var element = $('<div class="tab-pane" id="' + name + '"><iframe id="iframe' + name + '" src="' + url + '" style="margin: 0; padding: 0; border: 0; height: 100%;width: 100%;" frameborder="0"></iframe></div>');
        tabContent.append(element);
        resizeHeight();
        $(this).tab('show');
        showTab(name);
    }
}
//this method will demonstrate how to add tab dynamically
//function registerComposeButtonEvent() {
//    /* just for this demo */
//    $('#composeButton').click(function (e) {
//        e.preventDefault();

//        var tabId = "compose" + composeCount; //this is id on tab content div where the 
//        composeCount = composeCount + 1; //increment compose count

//        $('.nav-tabs').append('<li><a href="#' + tabId + '"><button class="close closeTab" type="button" >×</button>Compose</a></li>');
//        $('.tab-content').append('<div class="tab-pane" id="' + tabId + '"></div>');

//        craeteNewTabAndLoadUrl("", "TabsAccordion.aspx", "#" + tabId);

//        $(this).tab('show');
//        showTab(tabId);
//        registerCloseEvent();
//    });
//}
