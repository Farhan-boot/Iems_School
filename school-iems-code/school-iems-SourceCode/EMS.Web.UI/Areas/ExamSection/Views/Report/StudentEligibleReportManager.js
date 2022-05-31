
//File:Academic_Semester List Anjular  Controller
emsApp.controller('StudentEligibleReportManagerCtrl', function ($scope, $http, $filter) {
    $scope.ErrorMsg = "";
    $scope.HasError = false;

    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====
    $scope.Init = function (
         getGeneralDataExtraUrl
         , getExamListBySemesterIdForStudentEligibleReportUrl
        , getBatchListByProgramIdSemesterId

        ) {
        $scope.getGeneralDataExtraUrl = getGeneralDataExtraUrl;
        $scope.getExamListBySemesterIdForStudentEligibleReportUrl = getExamListBySemesterIdForStudentEligibleReportUrl;
        $scope.getBatchListByProgramIdSemesterId = getBatchListByProgramIdSemesterId;
        $scope.getGeneralDataExtra();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

    $scope.SelectedSemesterId = 0;
    $scope.SelectedProgramId = 0;
    $scope.SelectedExamId = 0;
    $scope.SelectedReportTypeEnumId = 0;

    $scope.SelectedContinuingBatchId = 0;

    //search items

    $scope.getGeneralDataExtra = function () {
        $http.get($scope.getGeneralDataExtraUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.DataExtra.SemesterList != null) {
                    $scope.SemesterList = result.DataExtra.SemesterList;
                    $scope.SelectedSemesterId = $scope.SemesterList[0].Id;
                }
                if (result.DataExtra.ProgramList != null) {
                    $scope.ProgramList = result.DataExtra.ProgramList;
                    $scope.SelectedProgramId = $scope.ProgramList[0].Id;
                }
                /*if (result.DataExtra.LevelTermList != null) {
                    $scope.LevelTermList = result.DataExtra.LevelTermList;
                    $scope.SelectedLevelTermId = $scope.LevelTermList[0].Id;
                }*/

                if (result.DataExtra.ReportTypeEnumList != null) {
                    $scope.ReportTypeEnumList = result.DataExtra.ReportTypeEnumList;
                   
                }
                

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = result.Errors.toString() + " Unable to load Data!";
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load data! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.onChangeSemester = function () {
        //$scope.getExamListBySemesterIdForStudentEligibleReport();
        $scope.getBatchList();
    }
    $scope.onChangeProgram = function() {
        $scope.getBatchList();
    }

    $scope.getExamListBySemesterIdForStudentEligibleReport = function () {
        if ($scope.SelectedSemesterId <= 0) {

            return;
        }
        $http.get($scope.getExamListBySemesterIdForStudentEligibleReportUrl, {
            params: {
                "semesterId": $scope.SelectedSemesterId
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.ExamList = result.DataExtra.ExamList;
                
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Exam List! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Exam List! " + "Status: " + JSON.stringify(status) + ". " + JSON.stringify(result);
            alertError($scope.ErrorMsg);
        });
    };

    $scope.getBatchList = function () {
        if ($scope.SelectedSemesterId <= 0 || $scope.SelectedProgramId <= 0) {
            return;
        }
        $http.get($scope.getBatchListByProgramIdSemesterId, {
            params: {
                "semesterId": $scope.SelectedSemesterId
                , "programId": $scope.SelectedProgramId
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.DeptBatchList = result.DataExtra.DeptBatchList;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Batch List! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Batch List! " + "Status: " + JSON.stringify(status) + ". " + JSON.stringify(result);
            alertError($scope.ErrorMsg);
        });
    };

});



