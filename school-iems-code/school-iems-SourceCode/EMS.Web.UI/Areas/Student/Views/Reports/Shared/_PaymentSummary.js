
//File: Aca_SemesterResult Anjular  Controller
emsApp.controller('PaymentSummaryCtrl', function ($scope, $http, $filter, cfpLoadingBar) {

    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;



    $scope.loadPage = function () {
        $scope.getPaymentSummaryDataExtra();
    }

    $scope.refreshResult = function () {
        $scope.getPaymentSummaryBySemesterId();
    }

    $scope.getPaymentSummaryDataExtra = function () {
        $http.get($scope.getPaymentSummaryDataExtraUrl, {
            params: {}
        }).success(function (result) {
            if (!result.HasError) {
                $scope.SemesterList = result.Data;
                if ($scope.SemesterList != null && $scope.SemesterList.length > 0) {
                    $scope.SelectedSemester = $scope.SemesterList[0];
                    $scope.refreshResult();
                }

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Semester list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }

        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Semester list! " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);

        });
    };

    $scope.getPaymentSummaryBySemesterId = function () {
        $http.get($scope.getPaymentSummaryBySemesterIdUrl, {
            params: {
                "semesterId": $scope.SelectedSemester.Id
            }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.SemesterBill = result.Data;
             

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg =result.Errors.toString();
                $scope.SemesterBill = null;

            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Please try again later!";

            log(JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString());
        });
    };


    //======Custom property and Functions Start=======================================================  
    $scope.onChangeSemester = function () {
        if ($scope.SelectedSemester != null) {
            $scope.refreshResult();
        }
    };

    //======Custom Variabales goes hare=====

    $scope.Init = function (
        getPaymentSummaryDataExtraUrl
    , getPaymentSummaryBySemesterIdUrl
    ) {
        $scope.getPaymentSummaryDataExtraUrl = getPaymentSummaryDataExtraUrl;
        $scope.getPaymentSummaryBySemesterIdUrl = getPaymentSummaryBySemesterIdUrl;
        $scope.loadPage();
        
    };

});

