
//File:Accounts_AdmissionSummaryReport List Anjular  Controller
emsApp.controller('BankWiseCollectionSummaryCtrl', function ($scope, $http, $filter, cfpLoadingBar) {

    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;

    $scope.StartDate = "";
    $scope.EndDate = "";
    $scope.SelectedBank = null;
    $scope.Report = [];

    $scope.loadPage = function () {
        $scope.getBankWiseCollectionSummaryDataExtra();
    };

    $scope.getBankWiseCollectionSummaryDataExtra = function () {
        $http.get($scope.getBankWiseCollectionSummaryDataExtraUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;

                //DropDown Option Tables
                if (result.DataExtra.OfficialBankList != null)
                    $scope.OfficialBankList = result.DataExtra.OfficialBankList;

                $scope.StartDate = result.DataExtra.StartDate;
                $scope.EndDate = result.DataExtra.EndDate;


            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Bank Wise Collection Summary! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Bank Wise Collection Summary! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.getBankWiseCollectionSummary = function () {
        var bankId = -1;
        if ($scope.SelectedBank !== null) {
            bankId = $scope.SelectedBank.Id;
        }
        $http.get($scope.getBankWiseCollectionSummaryUrl, {
            params: {
                "bankId": bankId,
                "startDate": $scope.StartDate,
                "endDate": $scope.EndDate
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;

                $scope.Report = result.Data;
                $scope.GrandTotal = result.DataExtra.GrandTotal;
                log(result);

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable Get Bank Wise Collection Summary Report! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
                $scope.Report = [];
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable Get Bank Wise Collection Summary Report! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };

    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    $scope.Init = function (
         getBankWiseCollectionSummaryDataExtraUrl
        , getBankWiseCollectionSummaryUrl
        ) {
        $scope.getBankWiseCollectionSummaryDataExtraUrl = getBankWiseCollectionSummaryDataExtraUrl;
        $scope.getBankWiseCollectionSummaryUrl = getBankWiseCollectionSummaryUrl;
        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



