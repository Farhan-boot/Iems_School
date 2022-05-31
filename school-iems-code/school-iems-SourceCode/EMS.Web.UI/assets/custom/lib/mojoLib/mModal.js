
/*
Bootstrap 3 Modal extention by pavel.
ltr rtlm supported
this Js method is only for Bootstrap v3.0
*/

//IFrame modal with full screen support 
function OpenModalPopup(heading, url, width, height,closeCallBack) {

    var frameModal =
		$(' <div class="modal fade">' +
              '<div class="modal-dialog">' +
                '<div class="modal-content" style="border-radius: 6px;">' +
                    '<div class="modal-header alert alert-info" style=" margin-bottom: 0 !important;border-radius: 6px;border-bottom-left-radius: 0px;border-bottom-right-radius: 0px;">' +
		    		  '<span  class="modal-title" style="font-size: 18px !important">' + heading + '</span>' +
		              '<div class="pull-right"> <i class="glyphicon glyphicon-remove" id="closeButton" data-dismiss="modal" aria-hidden="true" style="cursor: pointer;font-size:12px !important" onclick="" data-toggle="tooltip" title="Close Modal" data-placement="top"></i></div>' +
                     '</div>' +
                     '<div id="iframeContainer"   style="height: 200px;padding: 10px;">' +
                        '<iframe id="iframe" src="' + url + '" style="width: 100%;height: 100%;border: none;display: block;"  frameborder="0"></iframe>' +
                     '</div>' +
                '</div>' +
              '</div>' +
            '</div>');
    frameModal.modal({
        keyboard: false,
        backdrop: 'static',
        show: true
    });

    //setting minimum width & height
    if (width != null && width < 600)
        width = 600;
    if (height != null && height < 400)
        height = 400;

    //setting width & height not bigger then window size
    if (width == null || width < 100 || (width + 40) > ($(window).width())) {
        width = $(document).width() - 40;
    }
    if (height == null || height < 100 || (height + 120) > ($(window).height())) {
        height = $(window).height() - 120;
    }

    frameModal.find(".modal-dialog").width(width);
    frameModal.find("#iframeContainer").height(height);
    
    frameModal.find('#closeButton').click(function (event) {
        frameModal.modal('hide');
        if (closeCallBack)
        closeCallBack();

    });
    //load iframe  
    //frameModal.find('#iframe').prop('src', url);

    //setting dynamic position
    //frameModal.css({
    //    "position": "absolute",
    //    "top": (($(window).height() - (height + 51)) / 2),
    //    "left": (($(window).width() - width) / 2)
    //});


}

//IFrame modal with full screen support 
function iframeModal(heading, url, width, height) {
    var frameModal =
		$(' <div class="modal fade">' +
              '<div class="modal-dialog">' +
                '<div class="modal-content" style="border-radius: 6px;">' +
                    '<div class="modal-header alert alert-info" style=" margin-bottom: 0 !important;border-radius: 6px;border-bottom-left-radius: 0px;border-bottom-right-radius: 0px;">' +
                       
                        '<span  class="modal-title" style="font-size: 18px !important">' + heading + '</span>' +
		                '<div class="pull-right"> <i class="glyphicon glyphicon-remove " data-dismiss="modal" aria-hidden="true" style="cursor: pointer;font-size:12px !important" onclick="" data-toggle="tooltip" title="Close Modal" data-placement="top"></i></div>' +
                     '</div>' +
                     '<div id="iframeContainer"   style="height: 200px;padding: 10px;">' +
                        '<iframe id="iframe" src="' + url + '" style="width: 100%;height: 100%;border: none;display: block;"  frameborder="0"></iframe>' +
                     '</div>' +
                '</div>' +
              '</div>' +
            '</div>');
    frameModal.modal({
        keyboard: false,
        backdrop: 'static',
        show: true
    });


    //setting width & height not bigger then window size
    if (width == null || width < 100 || (width + 40) > ($(window).width())) {
        width = $(document).width() - 40;
    }
    if (height == null || height < 100 || (height + 120) > ($(window).height())) {
        height = $(window).height() - 120;
    }

    frameModal.find(".modal-dialog").width(width);
    frameModal.find("#iframeContainer").height(height);
    //frameModal.find('#iframe').prop('src', url);
}

/* Ajax Remote modal */
function remoteModal(heading, url, width, height) {
    var remoteModal =

			$('<div id="model-remote" class="modal fade" tabindex="-1" role="dialog">' +
		    '<div class="modal-dialog" style="min-width:300px;overflow: hidden !important;">' +
                '<div class="modal-content" style="">' +
			        '<div class="modal-header alert alert-info" style=" margin-bottom: 0 !important;border-radius: 6px;border-bottom-left-radius: 0px;border-bottom-right-radius: 0px;">' +
			           
			            '<span  class="modal-title" style="font-size: 18px !important">' + heading + '</span>' +
			            '<div class="pull-right"> <i class="glyphicon glyphicon-remove " data-dismiss="modal" aria-hidden="true" style="cursor: pointer;font-size:12px !important" onclick="" data-toggle="tooltip" title="Close Modal" data-placement="top"></i></div>' +
			        '</div>' +
			        '<div class="modal-body" style="min-height:400px;overflow: hidden !important;">' +
			            '<p>My modal content here…</p>' +
			        '</div>' +
			        //'<div class="modal-footer" style="padding: 10px !important;">' +
			        //    '<a href="#" id="okButton" class="btn btn-info btn-sm"><i class="glyphicon glyphicon-ok"/> ' +
			        //    okButtonTxt +
			        //    '</a>' +
			        //'</div>' +
		        '</div>' +
		    '</div>' +
		'</div>');

    //remoteModal.find('#okButton').click(function (event) {
    //    remoteModal.modal('hide');
    //    okcall();
    //});
    remoteModal.find('.modal-body').load(url, function (result) {
        remoteModal.modal({
            keyboard: false,
            backdrop: 'static',
            show: true
        });
    });

    //setting minimum width & height
    if (width != null && width < 300)
        width = 300;
    if (height != null && height < 400)
        height = 400;

    //setting width & height not bigger then window size
    if (width == null || width < 100 || (width + 40) > ($(window).width())) {
        width = $(document).width() - 40;
    }
    if (height == null || height < 100 || (height + 120) > ($(window).height())) {
        height = $(window).height() - 120;
    }

    remoteModal.find(".modal-dialog").width(width);
    remoteModal.find(".modal-body").height(height);

};
function callRemoteModal(heading, url) {
    var okButtonTxt = 'Ok';
    var okcall = function () {
        //alert('you Press OK + ' + url);
    };
    remoteModal(heading, url, null, null);
}

/* confirmation modal */
function confirmModal(heading, question, cancelButtonTxt, okButtonTxt, yescall, nocall) {
    var confirmModal =
		$('<div id="model-confirm" class="modal fade">' +
		    '<div class="modal-dialog">' +
                '<div class="modal-content" style="">' +
			        '<div class="modal-header alert alert-warning" style=" margin-bottom: 0 !important;border-radius: 6px;border-bottom-left-radius: 0px;border-bottom-right-radius: 0px;">' +
			            '<span  class="modal-title" style="font-size: 18px !important"><i class="glyphicon glyphicon-question-sign"/>&nbsp;&nbsp;' + heading + '</span>' +
		                '<div class="pull-right"> <i class="glyphicon glyphicon-remove " data-dismiss="modal" aria-hidden="true" style="cursor: pointer;font-size:12px !important" onclick="" data-toggle="tooltip" title="Close Modal" data-placement="top"></i></div>' +
			        '</div>' +
			        '<div class="modal-body">' +
			        '<p>' + question + '</p>' +
			        '</div>' +
			        '<div class="modal-footer" style="padding: 10px !important;">' +
			            '<a href="#" id="okButton" class="btn btn-info btn-sm"><i class="glyphicon glyphicon-ok"/> ' +
			            okButtonTxt +
			            '</a>' +
			            '<a href="#" class="btn btn-warning btn-sm" data-dismiss="modal" id="noButton"><i class="glyphicon glyphicon-ban-circle"/>  ' +
			            cancelButtonTxt +
			            '</a>' +
			        '</div>' +
		        '</div>' +
		    '</div>' +
		'</div>');
    confirmModal.find('#okButton').click(function (event) {
        confirmModal.modal('hide');
        yescall();

    });
    confirmModal.find('#noButton').click(function (event) {
        confirmModal.modal('hide');
        nocall();

    });
    confirmModal.modal({
        keyboard: false,
        backdrop: 'static',
        show: true
    });
};
function callConfirmModal(heading, question) {
    var cancelButtonTxt = 'No';
    var okButtonTxt = 'Yes';
    var yescall = function () {
        //alert('you Press OK');
    };
    var nocall = function () {
        //('you Press No');
    };
    confirmModal(heading, question, cancelButtonTxt, okButtonTxt, yescall, nocall);
}

/* error modal */
function errorModal(heading, message, okButtonTxt, okcall) {
    var errorModal =
$('<div id="model-error" class="modal fade">' +
		    '<div class="modal-dialog">' +
                '<div class="modal-content" style="">' +
			        '<div class="modal-header alert alert-danger" style=" margin-bottom: 0 !important;border-radius: 6px;border-bottom-left-radius: 0px;border-bottom-right-radius: 0px;">' +
			            '<span  class="modal-title" style="font-size: 18px !important"><i class="glyphicon glyphicon-ban-circle"/>&nbsp;&nbsp;' + heading + '</span>' +
                        '<div class="pull-right"> <i class="glyphicon glyphicon-remove " data-dismiss="modal" aria-hidden="true" style="cursor: pointer;font-size:12px !important" onclick="" data-toggle="tooltip" title="Close Modal" data-placement="top"></i></div>' +
			        '</div>' +
			        '<div class="modal-body">' +
			        '<p>' + message + '</p>' +
			        '</div>' +
			        '<div class="modal-footer" style="padding: 10px !important;">' +
			            '<a href="#" id="okButton" class="btn btn-danger btn-sm"><i class="glyphicon glyphicon-ok"/> ' +
			            okButtonTxt +
			            '</a>' +
			        '</div>' +
		        '</div>' +
		    '</div>' +
		'</div>');
    errorModal.find('#okButton').click(function (event) {

        errorModal.modal('hide');
        okcall();
    });
    errorModal.modal({
        keyboard: false,
        backdrop: 'static',
        show: true
    });
};
function callErrorModal(heading, message) {
    var okButtonTxt = 'Ok';
    var okcall = function () {
        // alert('you Press OK');
    };
    errorModal(heading, message, okButtonTxt, okcall);
}

/* warning modal */
function warningModal(heading, message, okButtonTxt, okcall) {
    var warningModal =

	$('<div id="model-warning" class="modal fade">' +
		    '<div class="modal-dialog">' +
                '<div class="modal-content" style="">' +
			        '<div class="modal-header alert alert-warning" style=" margin-bottom: 0 !important;border-radius: 6px;border-bottom-left-radius: 0px;border-bottom-right-radius: 0px;">' +
			            '<span  class="modal-title" style="font-size: 18px !important"><i class="glyphicon glyphicon-warning-sign"/>&nbsp;&nbsp;' + heading + '</span>' +
	                    '<div class="pull-right"> <i class="glyphicon glyphicon-remove " data-dismiss="modal" aria-hidden="true" style="cursor: pointer;font-size:12px !important" onclick="" data-toggle="tooltip" title="Close Modal" data-placement="top"></i></div>' +
			        '</div>' +
			        '<div class="modal-body">' +
			        '<p>' + message + '</p>' +
			        '</div>' +
			        '<div class="modal-footer" style="padding: 10px !important;">' +
			            '<a href="#" id="okButton" class="btn btn-warning btn-sm"><i class="glyphicon glyphicon-ok"/> ' +
			            okButtonTxt +
			            '</a>' +
			        '</div>' +
		        '</div>' +
		    '</div>' +
		'</div>');
    warningModal.find('#okButton').click(function (event) {

        warningModal.modal('hide');
        okcall();
    });
    warningModal.modal({
        keyboard: false,
        backdrop: 'static',
        show: true
    });
};
function callWarningModal(heading, message) {
    var okButtonTxt = 'Ok';
    var okcall = function () {
        //alert('you Press OK');
    };
    warningModal(heading, message, okButtonTxt, okcall);
}

/* info modal */
function infoModal(heading, message, okButtonTxt, okcall) {
    var infogModal =
			$('<div id="model-info" class="modal fade">' +
		    '<div class="modal-dialog">' +
                '<div class="modal-content" style="">' +
			        '<div class="modal-header alert alert-info" style=" margin-bottom: 0 !important;border-radius: 6px;border-bottom-left-radius: 0px;border-bottom-right-radius: 0px;">' +
			            '<span  class="modal-title" style="font-size: 18px !important"><i class="glyphicon glyphicon-info-sign"/>&nbsp;&nbsp;' + heading + '</span>' +
			            '<div class="pull-right"> <i class="glyphicon glyphicon-remove " data-dismiss="modal" aria-hidden="true" style="cursor: pointer;font-size:12px !important" onclick="" data-toggle="tooltip" title="Close Modal" data-placement="top"></i></div>' +
			        '</div>' +
			        '<div class="modal-body">' +
			        '<p>' + message + '</p>' +
			        '</div>' +
			        '<div class="modal-footer" style="padding: 10px !important;">' +
			            '<a href="#" id="okButton" class="btn btn-info btn-sm"><i class="glyphicon glyphicon-ok"/> ' +
			            okButtonTxt +
			            '</a>' +
			        '</div>' +
		        '</div>' +
		    '</div>' +
		'</div>');
    infogModal.find('#okButton').click(function (event) {

        infogModal.modal('hide');
        okcall();
    });
    infogModal.modal({
        keyboard: false,
        backdrop: 'static',
        show: true
    });
};
function callInfoModal(heading, message) {
    var okButtonTxt = 'Ok';
    var okcall = function () {
        //alert('you Press OK');
    };
    infoModal(heading, message, okButtonTxt, okcall);
}

/* success modal */
function successModal(heading, message, okButtonTxt, okcall) {
    var infogModal =
			$('<div id="model-info" class="modal fade">' +
		    '<div class="modal-dialog">' +
                '<div class="modal-content" style="">' +
			        '<div class="modal-header alert alert-success" style=" margin-bottom: 0 !important;border-radius: 6px;border-bottom-left-radius: 0px;border-bottom-right-radius: 0px;">' +
			            '<span  class="modal-title" style="font-size: 18px !important"><i class="glyphicon glyphicon-ok"/>&nbsp;&nbsp;' + heading + '</span>' +
			            '<div class="pull-right"> <i class="glyphicon glyphicon-remove " data-dismiss="modal" aria-hidden="true" style="cursor: pointer;font-size:12px !important" onclick="" data-toggle="tooltip" title="Close Modal" data-placement="top"></i></div>' +
			        '</div>' +
			        '<div class="modal-body">' +
			        '<p>' + message + '</p>' +
			        '</div>' +
			        '<div class="modal-footer" style="padding: 10px !important;">' +
			            '<a href="#" id="okButton" class="btn btn-success btn-sm"><i class="glyphicon glyphicon-ok"/> ' +
			            okButtonTxt +
			            '</a>' +
			        '</div>' +
		        '</div>' +
		    '</div>' +
		'</div>');
    infogModal.find('#okButton').click(function (event) {

        infogModal.modal('hide');
        okcall();
    });
    infogModal.modal({
        keyboard: false,
        backdrop: 'static',
        show: true
    });
};
function callSuccessModal(heading, message) {
    var okButtonTxt = 'Ok';
    var okcall = function () {
        //alert('you Press OK');
    };
    successModal(heading, message, okButtonTxt, okcall);
}


