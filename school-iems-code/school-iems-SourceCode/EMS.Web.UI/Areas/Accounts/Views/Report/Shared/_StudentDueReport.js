
//File:Academic_Semester List Anjular  Controller
emsApp.controller('StudentDueReportCtrl', function ($scope, $http, $filter) {
    $scope.ErrorMsg = "";
    $scope.HasError = false;

    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====
    $scope.Init = function (
         getStudentDueReportDataExtraUrl
         , getPagedStudentDueReportUrl
        , getBatchListForStudentDueReportUrl
        ) {
        $scope.getStudentDueReportDataExtraUrl = getStudentDueReportDataExtraUrl;
        $scope.getPagedStudentDueReportUrl = getPagedStudentDueReportUrl;
        $scope.getBatchListForStudentDueReportUrl = getBatchListForStudentDueReportUrl;
        $scope.getStudentDueReportDataExtra();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

    $scope.SelectedSemesterId = 0;
    $scope.SelectedProgramId = 0;
    $scope.SelectedContinuingBatchId = 0;
    $scope.SelectedRegistrationStatusEnumId = 0;
    $scope.StudentList = [];
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 250;
    $scope.PageNo = 1;


    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedClassList();
    };

    $scope.changePageNo = function () {
        $scope.getPagedClassList();
    };

    $scope.searchClassList = function () {
        $scope.getPagedClassList();
    };

    $scope.getPagedClassList = function () {
        $scope.getStdTransactionList();
    };

    $scope.getStdTransactionList = function () {
        $http.get($scope.getPagedStudentDueReportUrl, {
            params: {
             "pageSize": $scope.PageSize
           , "pageNo": $scope.PageNo
           , "semesterId": $scope.SelectedSemesterId
           , "programId": $scope.SelectedProgramId
           , "ContinuingBatchId": $scope.SelectedContinuingBatchId
             , "registrationStatusEnumId": $scope.SelectedRegistrationStatusEnumId
            }
        }).success(function (result) {
            if (!result.HasError) {
                
                $scope.StudentList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
                if (result.Data.length <= 0) {
                    $scope.ErrorMsg = "No Data Found using your search! ";
                    $scope.StudentList = [];
                    $scope.totalItems = 0;
                    alertError($scope.ErrorMsg);
                }
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Student Due Report! " + result.Errors.toString();
                $scope.StudentList = [];
                $scope.totalItems = 0;
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Student Due Report! " + "Status: " + JSON.stringify(status) + ". " + JSON.stringify(result);
            alertError($scope.ErrorMsg);
        });
    };

    $scope.getBatchListForStudentDueReport = function () {
        $scope.cleanUi();
        if ($scope.SelectedSemesterId <= 0 || $scope.SelectedRegistrationStatusEnumId<=0) {
            return;
        }
        $http.get($scope.getBatchListForStudentDueReportUrl, {
            params: {
                 "semesterId": $scope.SelectedSemesterId
                , "programId": $scope.SelectedProgramId
                , "registrationStatusEnumId": $scope.SelectedRegistrationStatusEnumId
            }
        }).success(function (result) {
            if (!result.HasError) {

                if (result.DataExtra.DeptBatchList != null) {
                    $scope.DeptBatchList = result.DataExtra.DeptBatchList;
                    $scope.SelectedContinuingBatchId = 0;
                }
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Batch List! " + result.Errors.toString();
                $scope.StudentList = [];
                $scope.totalItems = 0;
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Batch List! " + "Status: " + JSON.stringify(status) + ". " + JSON.stringify(result);
            alertError($scope.ErrorMsg);
        });
    };

    $scope.getStudentDueReportDataExtra = function () {
        $http.get($scope.getStudentDueReportDataExtraUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.DataExtra.ProgramList != null) {
                    $scope.ProgramList = result.DataExtra.ProgramList;
                    //$scope.SelectedProgramId = $scope.ProgramList[0].Id;
                }
                if (result.DataExtra.SemesterList != null) {
                    $scope.SemesterList = result.DataExtra.SemesterList;
                    //$scope.SelectedSemesterId = $scope.SemesterList[0].Id;
                }

                if (result.DataExtra.CurrentSemesterId != null) {
                    $scope.SelectedSemesterId = result.DataExtra.CurrentSemesterId;
                }
               

                if (result.DataExtra.RegistrationStatusEnumList != null) {
                    $scope.RegistrationStatusEnumList = result.DataExtra.RegistrationStatusEnumList;
                }

                

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Student Due Report! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Student Due Report! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.cleanUi=function() {
        $scope.StudentList = [];
    }

});



