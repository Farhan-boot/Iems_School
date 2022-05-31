
//File:Accounts_AdmissionSummaryReport List Anjular  Controller
emsApp.controller('MarkChangeHistoryCtrl', function ($scope, $http, $filter, cfpLoadingBar, $sce) {

    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;

    $scope.StdUserName = "";
    $scope.Report = [];



    $scope.loadPage = function () {
        if ($scope.StdUserName != 0 || $scope.StdUserName !== "") {
            $scope.getMarkChangeHistory();
        }
    };

    $scope.getMarkChangeHistory = function () {
        $http.get($scope.getMarkChangeHistoryUrl, {
            params: {
                "stdUserName": $scope.StdUserName
            }
        }).success(function (result) {
            if (!result.HasError) {

                $scope.HasError = false;
                $scope.Report = result.Data;
                log(result);
                updateUrlQuery('un', $scope.StdUserName);

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable Get Mark Change History! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
                $scope.Report = [];
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable Get Mark Change History! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.renderHtml = function (html_code) {
        return $sce.trustAsHtml(html_code);
    };

    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    $scope.Init = function (
          stdUserName
        , getMarkChangeHistoryUrl
        ) {
        $scope.StdUserName = stdUserName;
        $scope.getMarkChangeHistoryUrl = getMarkChangeHistoryUrl;
        $scope.loadPage();
    };


    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



