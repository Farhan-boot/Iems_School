
//File:Student Payment Collection Anjular  Controller
emsApp.controller('FinancialReportCollection', function ($scope, $http, $filter, $document, cfpLoadingBar, $sce) {

    $scope.Student = null;
    $scope.ErrorMsg = "";
    $scope.HasError = false;

    $scope.loadPage = function () {
        $scope.getFinancialReportByStudentId();
    };

    $scope.getFinancialReportByStudentId = function () {
        $http.get($scope.getFinancialReportByStudentIdUrl).success(function (result) {
            if (!result.HasError) {

                $scope.SemesterDebitList = result.DataExtra.SemesterDebitList;
                log($scope.SemesterDebitList);
                $scope.SemesterCreditList = result.DataExtra.SemesterCreditList;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Financial Report! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {

            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Financial Report! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };



    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */

    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====
    $scope.Init = function (getFinancialReportByStudentIdUrl) {
        $scope.getFinancialReportByStudentIdUrl = getFinancialReportByStudentIdUrl;
        $scope.loadPage();
    };

    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



