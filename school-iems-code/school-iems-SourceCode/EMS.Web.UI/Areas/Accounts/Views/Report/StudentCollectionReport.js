
//File:Accounts_StdTransaction List Anjular  Controller
emsApp.controller('StudentCollectionReport', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.StdTransactionList = [];
    $scope.SelectedStdTransaction = [];
    $scope.StudentId = "";
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;

    //search items


    $scope.loadPage = function () {
        $scope.getPrintStudentCollectionReportByStdId();

    };


    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getPrintStudentCollectionReportByStdId = function () {

        $http.get($scope.getPrintStudentCollectionReportByStdIdUrl, {
            params: {
                "stdId": $scope.StudentId,
                "feeCodeId":$scope.FeeCodeId
            }
        }).success(function (result, status) {
            if (!result.HasError) {

                $scope.HasError = false;
                $scope.SemesterDebitList = result.DataExtra.SemesterDebitList;
                $scope.SemesterCreditList = result.DataExtra.SemesterCreditList;
                $scope.TotalPackageAmount = result.DataExtra.TotalPackageAmount;
                //new
                $scope.PackageDetails = result.DataExtra.PackageDetails;
                log(result);
                setTimeout(function () { window.print(); }, 500);
              

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Std Transaction list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Std Transaction list! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };


    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====


    $scope.Init = function (
         StudentId
        , getPrintStudentCollectionReportByStdIdUrl
        , FeeCodeId
        ) {
        $scope.StudentId = StudentId;
        $scope.getPrintStudentCollectionReportByStdIdUrl = getPrintStudentCollectionReportByStdIdUrl;
        $scope.FeeCodeId = FeeCodeId;
        log($scope.FeeCodeId);
        $scope.loadPage();
        };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



