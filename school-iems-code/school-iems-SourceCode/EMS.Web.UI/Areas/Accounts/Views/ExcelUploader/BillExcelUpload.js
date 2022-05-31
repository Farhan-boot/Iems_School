
//File: Aca_Exam Anjular  Controller
emsApp.controller('BankPaymentExcelUploadCtrl', function ($scope, $http, $filter, Upload, $timeout, $sce) {

    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.BankPaymentExcelDataJsonList = [];
    $scope.WaitForCreditSaving = false;//detect when saving a credit item start
    $scope.IsSuccessfullyCreditSaving = false;
    $scope.IsSummaryShow = false;

    $scope.TotalTransactions = 0;
    $scope.TotalStudent = 0;
    $scope.TotalDebitAmount = 0;
    $scope.TransactionStartToEnd = "Start Date - End Date";


    //======Custom property and Functions Start=======================================================  

    $scope.loadPage = function () {
        $scope.getBillExcelUploadDataExtra();
    };

    $scope.getBillExcelUploadDataExtra = function () {
        $scope.IsSuccessfullyCreditSaving = false;
        $http.get($scope.getBillExcelUploadDataExtraUrl)
            .success(function(result, status ) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.DataExtra.SemesterList != null)
                    $scope.SemesterList = result.DataExtra.SemesterList;

                if (result.DataExtra.BankList != null)
                    $scope.BankList = result.DataExtra.BankList;

                if (result.DataExtra.ParticularNameList != null)
                    $scope.ParticularNameList = result.DataExtra.ParticularNameList;

                if (result.DataExtra.BillTypeEnumList != null)
                    $scope.BillTypeEnumList = result.DataExtra.BillTypeEnumList;

               

                if (result.DataExtra.BankPaymentExcelTransactionJson != null)
                    $scope.BankPaymentExcelTransactionJson = result.DataExtra.BankPaymentExcelTransactionJson;
               
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Std Transaction! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Std Transaction! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.saveBillExcelData = function () {
        if ($scope.BankPaymentExcelDataJsonList.length > 0) {
            $scope.BankPaymentExcelTransactionJson.BankPaymentExcelDataList = $scope.BankPaymentExcelDataJsonList;
        } else {
            return;
        }
        bootbox.confirm("Once you confirm, all amount will be permanently debited in the system for individual student. Are you sure?",
            function(yes) {
                if (yes) {
        $scope.WaitForCreditSaving = true;
                    $http.post($scope.saveBillExcelDataUrl + "/", $scope.BankPaymentExcelTransactionJson).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    alertSuccess("Successfully all data upload!");
                    $scope.WaitForCreditSaving = false;
                    $scope.IsSuccessfullyCreditSaving = true;

                } else {
                    $scope.HasError = true;
                    $scope.WaitForCreditSaving = false;
                    $scope.IsSuccessfullyCreditSaving = false;
                    $scope.ErrorMsg = "All data upload failed. " + result.Errors.toString();
                    $scope.BankPaymentExcelDataJsonList = [];
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.WaitForCreditSaving = false;
                $scope.IsSuccessfullyCreditSaving = false;
                $scope.ErrorMsg = "All data upload failed. " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                $scope.BankPaymentExcelDataJsonList = [];
                alertError($scope.ErrorMsg);
            });

                }
            });
    };
    //======Custom Variabales goes hare=====

    $scope.Init = function (uploadBillExcelFileUrl
                            , saveBillExcelDataUrl
                            , getBillExcelUploadDataExtraUrl) {

        $scope.uploadBillExcelFileUrl = uploadBillExcelFileUrl;
        $scope.saveBillExcelDataUrl = saveBillExcelDataUrl;
        $scope.getBillExcelUploadDataExtraUrl = getBillExcelUploadDataExtraUrl;
        $scope.loadPage();
    };

    //======Other tabale's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

    $scope.uploadBillExcelFile = function () {
        //alert($scope.ExamId);
        if ($scope.myFile == null) {
            alertError("Please select valid xls file to upload!");
            return;
        }
        if ($scope.BankPaymentExcelTransactionJson.BillTypeEnumId==null || $scope.BankPaymentExcelTransactionJson.BillTypeEnumId <= 0) {
            alertError("Please select valid bill type!");
            return;
        }

        Upload.upload({
            url: $scope.uploadBillExcelFileUrl,
            data: {
                file: $scope.myFile,
                billTypeEnumId: $scope.BankPaymentExcelTransactionJson.BillTypeEnumId
            }
                    }).then(function(response) {
                            $timeout(function() {
                                $scope.result = response.data;
                                if (!$scope.result.HasError) {
                                    $scope.HasError = false;
                                    //$scope.Obj.ApplicantPhotoUrl = $scope.result.DataExtra.UploadPath;
                                    $scope.BankPaymentExcelDataJsonList = $scope.result.Data;
                                    $scope.HasErrorData = $scope.result.DataExtra.HasErrorData;
                                    $('.nav-tabs a[href="#FileSummery"]').tab('show');
                                    $scope.IsSuccessfullyCreditSaving = false;
                                    $scope.IsSummaryShow = true;
                                    if ($scope.HasErrorData) {
                                        $scope.IsSummaryShow = false;
                                    }
                                   

                                    $scope.TotalTransactions = $scope.result.DataExtra.TotalTransactions;
                                    $scope.TotalStudent = $scope.result.DataExtra.TotalStudent;
                                    $scope.TotalDebitAmount = $scope.result.DataExtra.TotalDebitAmount;
                                    $scope.TransactionStartToEnd = $scope.result.DataExtra.TransactionStartToEnd;

                                } else {
                                    $scope.HasError = true;
                                    $scope.ErrorMsg = "Bank Payment Excel validation failed! " + $scope.result.Errors.toString();
                                    $scope.BankPaymentExcelDataJsonList = [];
                                    $('.nav-tabs a[href="#UploadInstructions"]').tab('show');
                                    $scope.IsSuccessfullyCreditSaving = false;
                                    $scope.IsSummaryShow = false;
                                    alertError($scope.ErrorMsg);
                                }
                            });
                        },
                        function(response) {
                            if (response.status > 0) {
                                $scope.errorMsg = response.status + ': ' + response.data;
                                $scope.ErrorMsg = "Bank Payment Excel validation failed! " +
                                    JSON.stringify(response.data).toString() +
                                    ". " +
                                    "Status: " +
                                    JSON.stringify(response.status).toString();
                                $scope.BankPaymentExcelDataJsonList = [];
                                $('.nav-tabs a[href="#UploadInstructions"]').tab('show');
                                $scope.IsSuccessfullyCreditSaving = false;
                                $scope.IsSummaryShow = false;
                                alertError($scope.ErrorMsg);
                            }
                        },
                        function(evt) {
                            $scope.progress = parseInt(100.0 * evt.loaded / evt.total);
                        });
    };

    $scope.cleanUIData = function () {
        $scope.HasError = false;
        $scope.HasErrorData = false;
        $scope.BankPaymentExcelDataJsonList = [];
        $('.nav-tabs a[href="#UploadInstructions"]').tab('show');
        $scope.IsSuccessfullyCreditSaving = false;
        $scope.IsSummaryShow = false;

        $scope.TotalTransactions = 0;
        $scope.TotalStudent = 0;
        $scope.TotalDebitAmount = 0;
        $scope.TransactionStartToEnd = "Start Date - End Date";
    };

    $scope.renderHtml = function (html_code) {
        return $sce.trustAsHtml(html_code);
    };


});

