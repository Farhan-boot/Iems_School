
//File:Academic_Semester List Anjular  Controller
emsApp.controller('StudentDueReportSummaryCtrl', function ($scope, $http, $filter) {
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.eftnAndbKashRecived = 0;
    $scope.eftn = 0;
    $scope.bKash= 0;

    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====
    $scope.Init = function (
        getStudentDueReportSummaryDataExtraUrl
         , getStudentDueReportSummaryUrl
        ) {
        $scope.getStudentDueReportSummaryDataExtraUrl = getStudentDueReportSummaryDataExtraUrl;
        $scope.getStudentDueReportSummaryUrl = getStudentDueReportSummaryUrl;
        $scope.getStudentDueReportSummaryDataExtra();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

    $scope.SelectedSemesterId = 0;
    $scope.SelectedProgramId = 0;
    $scope.StartDate = null;
    $scope.EndDate = null;

    /*For Search on data start*/
    $scope.loadPage=function() {
        $scope.getStudentDueReportSummary();
    }

    $scope.getStudentDueReportSummaryDataExtra = function () {
        $http.get($scope.getStudentDueReportSummaryDataExtraUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.ErrorMsg = '';
                if (result.DataExtra.ProgramList != null) {
                    $scope.ProgramList = result.DataExtra.ProgramList;
                    //$scope.SelectedProgramId = $scope.ProgramList[0].Id;
                }
                if (result.DataExtra.SemesterList != null) {
                    $scope.SemesterList = result.DataExtra.SemesterList;
                    //$scope.SelectedSemesterId = $scope.SemesterList[0].Id;
                }
                if (result.DataExtra.OngoingSemesterId != null) {
                    //$scope.SemesterList = result.DataExtra.SemesterList;
                    $scope.SelectedSemesterId = result.DataExtra.OngoingSemesterId;
                }

                $scope.StartDate = null;
                $scope.EndDate = null;

                //$scope.getStudentDueReportSummary();

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Student Due Report Summary! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Student Due Report Summary! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.getStudentDueReportSummary = function () {
        $http.get($scope.getStudentDueReportSummaryUrl, {
            params: {
                "semesterId": $scope.SelectedSemesterId
                , "programId": $scope.SelectedProgramId
                , "startDate": $scope.StartDate
                , "endDate": $scope.EndDate
            }
        }).success(function (result) {
            if (!result.HasError) {

                $scope.ReportSummaryList = result.Data;
                $scope.GrantTotal = result.DataExtra.GrantTotal;
               
                if (result.Data.length <= 0) {
                    $scope.ErrorMsg = "No Data Found using your search! ";
                    $scope.ReportSummaryList = [];
                    alertError($scope.ErrorMsg);
                } else {
                    $scope.HasError = false;
                    $scope.ErrorMsg = '';
                }
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Student Due Report Summary! " + result.Errors.toString();
                $scope.ReportSummaryList = [];
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Student Due Report Summary! " + "Status: " + JSON.stringify(status) + ". " + JSON.stringify(result);
            alertError($scope.ErrorMsg);
        });
    };

    $scope.formatDate = function (date) {
        var dateOut = new Date(date);
        return dateOut;
    };

});



