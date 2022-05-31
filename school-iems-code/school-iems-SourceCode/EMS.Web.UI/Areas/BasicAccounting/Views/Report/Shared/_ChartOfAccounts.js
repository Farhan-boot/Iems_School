
//File:ChartOfAccounts List Anjular  Controller
emsApp.controller('ChartOfAccountsCtrl', function ($scope, $http, $filter, cfpLoadingBar) {

    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;

    $scope.ChartOfAccountsList = [];

    $scope.StartDate = "";
    $scope.EndDate = "";

    $scope.SelectedBranchId = 0;

    $scope.SelectedBranch = null;


    $scope.loadPage = function () {
        $scope.getChartOfAccountsDataExtra();
    };

    $scope.onChangeFilter = function () {
        $scope.getChartOfAccounts();
    };

    $scope.getChartOfAccountsDataExtra = function () {
        $http.get($scope.getChartOfAccountsDataExtraUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;

                if (result.DataExtra.BranchList != null) {
                    $scope.BranchList = result.DataExtra.BranchList;
                }

                $scope.StartDate = result.DataExtra.StartDate;
                $scope.EndDate = result.DataExtra.EndDate;


            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Chart Of Accounts! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Chart Of Accounts! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.getChartOfAccounts = function () {
        $scope.SelectedBranchId = 0;

        if ($scope.SelectedBranch != null)
            $scope.SelectedBranchId = $scope.SelectedBranch.Id;

        $http.get($scope.getChartOfAccountsUrl, {
            params: {
                "branchId": $scope.SelectedBranchId,
                "startDate": $scope.StartDate,
                "endDate": $scope.EndDate
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.ChartOfAccountsList = result.Data;
                log($scope.ChartOfAccountsList);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable Get Chart Of Accounts Report! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
                $scope.ChartOfAccountsList = [];
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ChartOfAccountsList = [];
            $scope.ErrorMsg = "Unable Get Chart Of Accounts Report! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };

    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====


    $scope.Init = function (
        getChartOfAccountsDataExtraUrl
        , getChartOfAccountsUrl
        ) {
        $scope.getChartOfAccountsDataExtraUrl = getChartOfAccountsDataExtraUrl;
        $scope.getChartOfAccountsUrl = getChartOfAccountsUrl;
        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



