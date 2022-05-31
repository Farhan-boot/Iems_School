/* [ ---- Crs Admin Panel - widgets ---- ] */

$(document).ready(function () {
    //* autosize textarea
    //$('.auto_expand').autosize();
    dashboard_sortable.init();
});

var sortablePanel = $('#sortable_panels div[class*="col"]').not('.not_sortable');
dashboard_sortable = {
   
    init: function () {

        // loadWidgetOrderFromCookie();
        sortablePanel.sortable({
            connectWith: '#sortable_panels div[class*="col"]',
            helper: 'original',
            handle: '.w-box-header',
            cancel: ".sort-disabled",
            forceHelperSize: true,
            forcePlaceholderSize: true,
            tolerance: 'pointer',

            activate: function (event, ui) {
                $(".ui-sortable").addClass('sort_ph');
            },
            stop: function (event, ui) {
                $(".ui-sortable").removeClass('sort_ph');
                //update orentation to server
                dashboard_sortable.updateWidgetOrderAjax();
            },
            update: function (e, ui) {
                //updateWidgetOrderCookie();
                //console.log("update");
            },
            
        });

        $('.reset_layout').click(function () {
            $.cookie('sortOrder', null);
            location.reload();
        });
    },

    updateWidgetOrderAjax: function () {
        showLoader(true);
        try {
            $.ajax({
                url: rootUrl + "WebApi/Erp/Widget/UpdateWidgetOrder/",
                type: "Post",
                dataType: 'json',
                data: JSON.stringify(dashboard_sortable.getWidgetDataList()),
                contentType: "application/json;charset=utf-8",
                success: function (rslt) {
                    if (rslt.IsSuccess) {
                        //successModal("Success", "Widget Saved.", 'Ok', null);
                        //errorModal("success");
                        showLoader(false);
                    } else {
                        errorModal("Error", "Orentation update failed, " + rslt.ErrorMessage, 'Ok', null);
                        sortablePanel.sortable("cancel");
                        showLoader(false);
                    }
                },
                error: function (msg) {
                    errorModal("Error", "Ajax Url call failed! ", 'Ok', null);
                    sortablePanel.sortable("cancel");
                    showLoader(false);
                }
            });
        } catch (ex) {
            showLoader(false);
        }
       
    },

    MyWidget: function (widgetNo, columnNo, orderNo) {
        this.WidgetNo = widgetNo;
        this.ColumnNo = columnNo;
        this.OrderNo = orderNo;
    },
    getWidgetDataList: function () {

        var columns = [];
        var widgetsElement = [];
        var widgetDataList = [];
        sortablePanel.each(function () {
            columns.push($(this));
        });
        if (columns.length > 0) {
            var orderNo = 0;
            for (var col = 0; col < columns.length; col++) {
                orderNo = 0;
                widgetsElement = columns[col].find('.w-box').toArray();

                for (var x = 0; x < widgetsElement.length; x++) {
                    orderNo++;
                    var widgetNo = $(widgetsElement[x]).attr('id');
                    widgetDataList.push(new dashboard_sortable.MyWidget(widgetNo, col + 1, orderNo));
                }
            }
        }
        
        return widgetDataList;
    },
    /*
    loadWidgetOrderFromCookie: function () {
        var thisCookie = $.cookie('sortOrder');
        if (thisCookie != null) {
            $.each(thisCookie.split(';'), function (i, id) {
                thisSortable = $('#sortable_panels div[class*="col"]').not('.not_sortable').get(i);
                if (id != 'null') {
                    $.each(id.split(','), function (i, id) {
                        $("#" + id).appendTo(thisSortable);
                    });
                }
            })
        }
    },
    updateWidgetOrderCookie: function () {
        var elem = [];
        $('#sortable_panels div[class*="col"]').not('.not_sortable').each(function () {
            elem.push($(this).sortable("toArray"));
        });
        var str = '';
        var m_len = elem.length;
        jQuery.each(elem, function (index, value) {
            var s_len = value.length;
            if (value == '') {
                str += 'null';
            } else {
                jQuery.each(value, function (index, value) {
                    str += value;
                    if (index != s_len - 1) {
                        str += ","
                    }
                });
            }
            if (index != m_len - 1) {
                str += ";"
            }
        });
        $.cookie('sortOrder', str, { expires: 20 });
    },*/
};
function removeWidgetAjax(widgetNo) {
   
    try {
        var confirmCall = function () {
            showLoader(true);
            $.ajax({
                url: rootUrl + 'WebApi/Erp/Widget/UpdateDeleteWidgetByNo/',
                type: "POST",
                dataType: 'json',
                data: { '': widgetNo },
                //contentType: "application/json;charset=utf-8",
                success: function (rslt) {
                    if (rslt.IsSuccess) {
                        //successModal("Success", "Widget Saved.", 'Ok', null);
                        $('#' + widgetNo).hide('slow', function () { $('#' + widgetNo).remove(); });
                        showLoader(false);
                        
                    } else {
                        errorModal("Error", rslt.ErrorMessage, 'Ok', null);
                        sortablePanel.sortable("cancel");
                        showLoader(false);
                    }
                },
                error: function (msg) {
                    errorModal("Error", "Ajax Url call failed! ", 'Ok', null);
                    sortablePanel.sortable("cancel");
                    showLoader(false);
                }
            });
            
        };

        confirmModal(" Confirm?", "Are you sure, remove this widget from your dashboard?", "Cancel", "Ok", confirmCall, null);
    } catch (ex) {

    }

   
};

