
//File:Accounts_AdmissionSummaryReport List Anjular  Controller
emsApp.controller('VaucherWiseCollectionReportCtrl', function ($scope, $http, $filter, $document, cfpLoadingBar, $sce) {

    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;

    $scope.StartDate = "";
    $scope.EndDate = "";

    $scope.SelectedProgramId = 0;
    $scope.SelectedBankId = 0;
    $scope.SelectedParticularNameId = 0;
    $scope.VoucherNo = "";
    $scope.Report = [];
    $scope.WarningMessages = "";



    $scope.loadPage = function () {
        if ($scope.VoucherNo != 0 || $scope.VoucherNo !== "") {
            log($scope.VoucherNo);
            $scope.getVaucherWiseCollectionReport();
        }
        
    };


    $scope.getVaucherWiseCollectionReport = function () {
        $http.get($scope.getVaucherWiseCollectionReportUrl, {
            params: {
                "vaucherNo": $scope.VoucherNo
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;

                $scope.Report = result.Data;
                $scope.GrandTotal = result.DataExtra.GrandTotal;
                $scope.BankName = result.DataExtra.BankName;
                $scope.VoucherDate = result.DataExtra.VoucherDate;
                $scope.WarningMessages = result.DataExtra.WarningMessages;
               
                updateUrlQuery('vid', $scope.VoucherNo);

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable Get Vaucher Wise Collection Report! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
                $scope.Report = [];
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable Get Vaucher Wise Collection Report! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.renderHtml = function (html_code) {
        return $sce.trustAsHtml(html_code);
    };

    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====


    $scope.Init = function (
        voucherNo
        ,getVaucherWiseCollectionReportUrl
        ) {
        $scope.VoucherNo = voucherNo;
        $scope.getVaucherWiseCollectionReportUrl = getVaucherWiseCollectionReportUrl;
        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



