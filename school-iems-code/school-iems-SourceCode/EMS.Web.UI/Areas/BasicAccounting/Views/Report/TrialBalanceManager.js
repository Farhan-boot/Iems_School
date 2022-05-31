
//File: Day Book Anjular  Controller
emsApp.controller('TrialBalanceCtrl', function ($scope, $http, $filter, cfpLoadingBar) {

    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
    $scope.SelectedBranch = [];

    // Report Parameters
    $scope.BranchId = 0;
    $scope.IsUgcReport = false;

    $scope.loadPage = function () {
        $scope.loadTrialBalanceDataExtra();
    };

    $scope.loadTrialBalanceDataExtra = function () {
        $http.get($scope.getTrialBalanceDataExtraUrl)
            .success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;

                    if (result.DataExtra.BranchList != null)
                        $scope.BranchList = result.DataExtra.BranchList;

                    $scope.StartDate = result.DataExtra.StartDate;
                    $scope.EndDate = result.DataExtra.EndDate;

                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to load option data for Day Book! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Day Book! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    };

    //======Custom property and Functions Start=======================================================  
    $scope.onChangeReportType = function (ugc) {
        if (ugc) {
            updateUrlQuery('ugc', "1");
        } else {
            updateUrlQuery('ugc', "0");
        }
    }

    //======Custom Variabales goes hare=====

    $scope.Init = function (
        getTrialBalanceDataExtraUrl
        ,isUgcReport
    ) {
        $scope.getTrialBalanceDataExtraUrl = getTrialBalanceDataExtraUrl;
        $scope.IsUgcReport = isUgcReport;
        $scope.loadPage();
    };


    //======Other tabale's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 
});

