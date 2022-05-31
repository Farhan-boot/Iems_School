/**
Bootstrap Dynamic tab loader with ifreame
Humayon Kabir Pavel

*/
//this will hold currently focused tab
var currentTab;//selected tab header li
var countTab = 0;

$(document).ready(function () {

    $("#tab-container > .nav-tabs").resize(function () {
        $("#tab-container > .tab-content").css({ "top": $("#tab-container > .nav-tabs").height() });
    });

    $("#sidebar").resize(function () {
        console.log("Sidebar width:" + $("#sidebar").width());
        /*Change brand size and container left position if side bar size change*/       
        $(".erpbar-brand").css({ "width": $("#sidebar").width() + 1 });
        $("#tab-container").css({ "left": $("#sidebar").width() + 1 });
    });

    ////register click event, when ever any tab is clicked this method will be call
    //$('#dynamicTab .nav-tabs li').on("click", "a", function (e) {
    //    bindTabOnClick(this, e);
    //});


    ////*register resize event on window resize
    //var lastWindowHeight = $(window).height();
    //var lastWindowWidth = $(window).width();
    //$(window).resize(function () {
    //    if ($(window).height() != lastWindowHeight || $(window).width() != lastWindowWidth) {

    //        lastWindowHeight = $(window).height();
    //        lastWindowWidth = $(window).width();
    //        resizeHeight();
    //        //resize height for hardcoaded written tab
    //        resizeHeight2();
    //    }
    //});
    ////registerCloseEvent();
    //resizeHeight();
    ////resize height for hardcoaded written tab
    //resizeHeight2();
});
//==================== dynamic tab by pavel==================

//shows the tab with passed content div id..paramter tabid indicates the div where the content resides
function ShowTab(tabId) {
    var currentTab = $('#tab-container > .nav-tabs a[href="#' + tabId + '"]'); //shifting current tab
    currentTab.tab('show');
}

function OpenDocument(title, name, url) {
    var tabHeaderContainer = $("#tab-container > .nav-tabs");
    tabHeaderContainer.append('<li><a name="' + name + '" href="#' + name + '" onclick="bindTabOnClick(this)"> <span  name="tabTitle">' + title + '</span>&nbsp;<i class="glyphicon glyphicon-remove close-tab" style="cursor: pointer;font-size:10px" onclick="closeTab(this)" data-toggle="tooltip" title="Close Tab" data-placement="top"></i></a></li>');

    var tabContentContainer = $("#tab-container > .tab-content");
    var tabContentElement = $('<div class="tab-pane" id="' + name + '"><iframe id="iframe' + name + '" src="' + url + '" frameborder="0"></iframe></div>');
    tabContentContainer.append(tabContentElement);

    ShowTab(name);
}

function renameTab(oldName, newName, newTitle) {

    var tabHeader = $('#dynamicTab .nav-tabs a[name="' + newName + '"]');
    //console.log($(window));
    if (tabHeader.length == 0) {
        tabHeader = $('#dynamicTab .nav-tabs a[name="' + oldName + '"]');
        //select tab title
        var tabTitle = tabHeader.find('span[name="tabTitle"]')[0];
        //rename tab title
        tabTitle.innerHTML = newTitle;

        //rename tab name attrib

        tabHeader.attr({
            name: newName,
            href: '#' + newName
        });
        //change tab pane id with new name
        var tabContent = $('#dynamicTab .tab-content #' + oldName + '');
        tabContent.attr("id", newName);
    }
}
//rename and change url
function renameTab2(oldName, newName, newTitle, newUrl) {
    var tabHeader = $('#dynamicTab .nav-tabs a[name=' + newName + ']');

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
    $('.nav-tabs').append('<li><a href="#' + name + '" onclick="bindTabOnClick(this)"><span  class="close closeTab" type="button" >x</span>' + name + '</a></li>');
    $('.tab-content').append('<div class="tab-pane" id="' + name + '" style=""></div>');

    LoadUrlAjax("", url, "#" + name);
    //enableHtmlBodyScroll();
    $(this).tab('show');
    showTab(name);
    registerCloseEvent();
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
    try {
        var height = $('#dynamicTab .tab-content:first').parent().parent()[0].offsetHeight - $('#dynamicTab .nav-tabs')[0].offsetHeight; //$(window).height() - getOffsetTopHeight($('#dynamicTab .tab-content')[0]);
        $('#dynamicTab .tab-pane').css({ 'height': (height), 'overflow-y': 'hidden' });
    } catch (ex) { }
}
//resize height for hardcoaded written tab
function resizeHeight2() {
    try {
        var height = $(window).height() - getOffsetTopHeight($('#dynamicTab .tab-content')[0]);
        $('#dynamicTab .tab-pane').css({ 'height': (height), 'overflow-y': 'hidden' });
    } catch (ex) { }
}
function closeTab(obj) {
    $('.close-tab').tooltip('hide');
    //there are multiple elements which has .closeTab icon so close the tab whose close icon is clicked
    var tabContentId = $(obj).parent().attr("href"); //get tab content div id
    $(obj).parent().parent().remove(); //remove li of tab
    currentTab = $('#dynamicTab .nav-tabs a:last'); //shifting current tab #dynamicTab .nav-tabs li
    //console.log(currentTab);
    currentTab.tab('show'); // Select first tab
    $(tabContentId).remove(); //remove respective tab content
    resizeHeight();
}
//this method will register event on close icon on the tab..
function registerCloseEvent() {
    $(".closeTab").click(function () {
        closeTab(this);
    });
}

//return current active tab
function getCurrentTab() {
    if (!currentTab)
        currentTab = $('#dynamicTab a:first');
    return currentTab;
}
function reloadIframeTab(name) {
    try {
        console.log($(document).body);
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
function reloadIframeTab2(name, url) {
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
function reloadCurrentIframeTab() {
    try {
        var tabContentId = getCurrentTab().attr("href");
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
//This function will create a new tab here and it will load the url content in tab content div.
function LoadUrlAjax(parms, url, loadDivSelector) {

    $("" + loadDivSelector).load(url, function (response, status, xhr) {
        if (status == "error") {
            var msg = "Sorry but there was an error getting details ! ";
            $("" + loadDivSelector).html(msg + xhr.status + " " + xhr.statusText);
            $("" + loadDivSelector).html("Load Ajax Content Here...");
        }
    });

}

//this will return element from current tab
//example : if there are two tabs having  textarea with same id or same class name then when $("#someId") whill return both the text area from both tabs
//to take care this situation we need get the element from current tab.
function getElement(selector) {
    var tabContentId = currentTab.attr("href");
    return $("" + tabContentId).find("" + selector);

}
//remove current selectet tab
function removeCurrentTab() {
    if (!currentTab)
        currentTab = getCurrentTab();
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
    //console.log('y:' + ly);
    return ly;
}


