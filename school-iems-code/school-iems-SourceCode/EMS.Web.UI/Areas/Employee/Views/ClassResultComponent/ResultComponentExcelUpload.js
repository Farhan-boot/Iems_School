
//File: Aca_Exam Anjular  Controller
emsApp.controller('ResultComponentExcelUploadCtrl', function ($scope, $http, $filter, Upload, $timeout, $sce) {

    $scope.ErrorMsg = "";
    $scope.HasError = false;

    $scope.WaitForCreditSaving = false;
    $scope.TotalStudent = 0;



    //======Custom property and Functions Start=======================================================  

    $scope.loadPage = function () {
        $scope.getResultComponentExcelUploadDataExtra();
    };


    $scope.getResultComponentExcelUploadDataExtra = function () {
        $http.get($scope.getResultComponentExcelUploadDataExtraUrl, {
                    params: {
                        "classId": $scope.classId
                        , "componentSettingsId": $scope.componentSettingsId
                        , "breakdownSettingsId": $scope.breakdownSettingsId
                        , "markTypeEnumId" : $scope.markTypeEnumId
                    }
                })
            .success(function(result, status ) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.ResultExcelUploadJson = result.Data;
                log($scope.ResultExcelUploadJson);

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Component Wise Result Excel Upload! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Component Wise Result Excel Upload! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };



    $scope.saveClassResultExcelMarkList = function () {

        if ($scope.WaitForCreditSaving) {
            return true;
        }

        $scope.ResultExcelUploadJson.MarkTypeEnumId = $scope.markTypeEnumId;

        bootbox.confirm("Once you confirm, all mark will be permanently replace in the system for individual student. Are you sure?",
            function(yes) {
                if (yes) {
        $scope.WaitForCreditSaving = true;
                    $http.post($scope.saveClassResultExcelMarkListUrl + "/", $scope.ResultExcelUploadJson).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    alertSuccess("Successfully all Mark upload!");
                    $scope.WaitForCreditSaving = false;
                    
                    window.location = $scope.routeUrl;

                } else {
                    $scope.HasError = true;
                    $scope.WaitForCreditSaving = false;
                   
                    $scope.ErrorMsg = "All Mark upload failed. " + result.Errors.toString();
                    $scope.BankPaymentExcelDataJsonList = [];
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.WaitForCreditSaving = false;
                $scope.ErrorMsg = "All Mark upload failed. " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                $scope.BankPaymentExcelDataJsonList = [];
                alertError($scope.ErrorMsg);
            });

                }
            });
    };
    //======Custom Variabales goes hare=====

    $scope.Init = function (
        classId
        , componentSettingsId
        , breakdownSettingsId
        , markTypeEnumId
        ,getResultComponentExcelUploadDataExtraUrl
        , uploadResultComponentExcelFileByClassIdUrl
        , saveClassResultExcelMarkListUrl
    , routeUrl

    ) {

        $scope.classId = classId;
        $scope.componentSettingsId = componentSettingsId;
        $scope.breakdownSettingsId = breakdownSettingsId;
        $scope.markTypeEnumId = markTypeEnumId;
        $scope.getResultComponentExcelUploadDataExtraUrl = getResultComponentExcelUploadDataExtraUrl;
        $scope.uploadResultComponentExcelFileByClassIdUrl = uploadResultComponentExcelFileByClassIdUrl;
        $scope.saveClassResultExcelMarkListUrl = saveClassResultExcelMarkListUrl;
        $scope.routeUrl = routeUrl;

        //$scope.getBillExcelUploadDataExtraUrl = getBillExcelUploadDataExtraUrl;
        $scope.loadPage();
    };

    //======Other tabale's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

    $scope.uploadResultComponentExcelFileByClassId = function () {
        if ($scope.myFile == null) {
            alertError("Please select valid xls file to upload!");
            return;
        }
        Upload.upload({
            url: $scope.uploadResultComponentExcelFileByClassIdUrl,
            data: {
                file: $scope.myFile,
                classId: $scope.ResultExcelUploadJson.ClassId,
                componentSettingsId: $scope.componentSettingsId,
                breakdownSettingsId: $scope.breakdownSettingsId,
                markTypeEnumId: $scope.markTypeEnumId
            }
                    }).then(function(response) {
                            $timeout(function() {
                                $scope.result = response.data;
                                if (!$scope.result.HasError) {
                                    $scope.HasError = false;

                                    $scope.ResultExcelUploadJson.ClassStudentMarkJsonList = $scope.result.Data;
                                    log($scope.ResultExcelUploadJson.ClassStudentMarkJsonList);

                                    $scope.HasErrorData = $scope.result.DataExtra.HasErrorData;
                                    $scope.TotalStudent = $scope.result.DataExtra.TotalStudent;



                                } else {
                                    $scope.HasError = true;
                                    $scope.ErrorMsg = "Result Excel validation failed! " + $scope.result.Errors.toString();
                                    $scope.ResultExcelUploadJson.ClassStudentMarkJsonList = [];
                                    alertError($scope.ErrorMsg);
                                }
                            });
                        },
                        function(response) {
                            if (response.status > 0) {
                                $scope.errorMsg = response.status + ': ' + response.data;
                                $scope.ErrorMsg = "Result Excel validation failed! " +
                                    JSON.stringify(response.data).toString() +
                                    ". " +
                                    "Status: " +
                                    JSON.stringify(response.status).toString();
                                $scope.ResultExcelUploadJson.ClassStudentMarkJsonList = [];
                                alertError($scope.ErrorMsg);
                            }
                        },
                        function(evt) {
                            $scope.progress = parseInt(100.0 * evt.loaded / evt.total);
                        });
    };

    $scope.renderHtml = function (html_code) {
        return $sce.trustAsHtml(html_code);
    };


});

