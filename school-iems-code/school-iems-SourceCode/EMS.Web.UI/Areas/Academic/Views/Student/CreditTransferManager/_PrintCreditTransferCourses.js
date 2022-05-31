
//File:Accounts_AdmissionSummaryReport List Anjular  Controller
emsApp.controller('PrintCreditTransferCoursesCtrl', function ($scope, $http, $filter, cfpLoadingBar) {

    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;

    $scope.CreditTransferJsonList = [];

    $scope.CreditTransferId = 0;
    $scope.StudentId = 0;


    $scope.loadPage = function () {
        $scope.getPrintCreditTransferCourses();
    };


    $scope.getPrintCreditTransferCourses = function () {
         $http.get($scope.getPrintCreditTransferCoursesUrl, {
            params: {
                "studentId": $scope.StudentId
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.CreditTransferJsonList = result.Data;
                $scope.CreditTransferSummary = result.DataExtra.CreditTransferSummary;
                log($scope.CreditTransferJsonList);
                log($scope.CreditTransferSummary);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable Print Credit Transfer Courses! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable Print Credit Transfer Courses! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };

    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====


     $scope.Init = function (
         studentId,
        getPrintCreditTransferCoursesUrl
    ) {
         $scope.StudentId = studentId;
        $scope.getPrintCreditTransferCoursesUrl = getPrintCreditTransferCoursesUrl;
        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



