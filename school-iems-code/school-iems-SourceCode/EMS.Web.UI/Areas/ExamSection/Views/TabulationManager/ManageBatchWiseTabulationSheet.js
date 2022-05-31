
//File:Academic_Semester List Anjular  Controller
emsApp.controller('ManageTabulationSheetCtrl', function ($scope, $http, $filter) {
    $scope.ErrorMsg = "";
    $scope.HasError = false;

    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====
    $scope.Init = function (
         getTabulationFilterUrl
         , getCurriculumAndBatchListForTabulationBatchWiseUrl
         
        ) {
        $scope.getTabulationFilterUrl = getTabulationFilterUrl;
        $scope.getCurriculumAndBatchListForTabulationBatchWiseUrl = getCurriculumAndBatchListForTabulationBatchWiseUrl;

        // $scope.loadPage();
        $scope.getTabulationFilter();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

    $scope.SelectedCurriculumCourseId = 0;
    $scope.SelectedProgramId = 0;
    $scope.SelectedSemester = null;
    $scope.SelectedSemesterId = 0;
    $scope.SelectedBatchId = 0;


    $scope.getTabulationFilter = function () {
        $http.get($scope.getTabulationFilterUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                

                //DropDown Option Tables
                if (result.DataExtra.SemesterList != null) {
                    $scope.SemesterList = result.DataExtra.SemesterList;
                    $scope.SelectedSemester = $scope.SemesterList[0];
                    $scope.SelectedSemesterId = $scope.SelectedSemester.Id;
                }
                if (result.DataExtra.ProgramList != null) {
                    $scope.ProgramList = result.DataExtra.ProgramList;
                    $scope.SelectedProgram = $scope.ProgramList.filter(x=>x.SemesterDurationEnumId === $scope.SelectedSemester.SemesterDurationEnumId, true)[0];
                    $scope.SelectedProgramId = $scope.SelectedProgram.Id;
                    //log($scope.ProgramList);
                }

                $scope.getCurriculumAndBatchListForTabulationBatchWise();


            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Tabulation! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Tabulation! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getCurriculumAndBatchListForTabulationBatchWise = function () {

        if ($scope.SelectedSemester != null) {
            $scope.SelectedSemesterId = $scope.SelectedSemester.Id;
        }

        if ($scope.SelectedProgram.SemesterDurationEnumId != $scope.SelectedSemester.SemesterDurationEnumId) {
            $scope.SelectedProgram = $scope.ProgramList.filter(x=>x.SemesterDurationEnumId === $scope.SelectedSemester.SemesterDurationEnumId, true)[0];
            $scope.SelectedProgramId = $scope.SelectedProgram.Id;
        }

        if ($scope.SelectedProgram != null) {
            $scope.SelectedProgramId = $scope.SelectedProgram.Id;
        }

        if ($scope.SelectedSemesterId <= 0 || $scope.SelectedProgramId<=0) {
            return;
        }

        $http.get($scope.getCurriculumAndBatchListForTabulationBatchWiseUrl, {
            params: {
                 "SemesterId": $scope.SelectedSemesterId
                ,"programId": $scope.SelectedProgramId
            }
        }).success(function (result) {
            if (!result.HasError) {

                $scope.CurriculumList = result.DataExtra.CurriculumList;
                if (result.DataExtra.CurriculumList != null) {
                    $scope.SelectedCurriculumId = $scope.CurriculumList[0].Id;
                }

                $scope.DeptBatchList = result.DataExtra.DeptBatchList;
                if (result.DataExtra.DeptBatchList != null) {
                    $scope.SelectedBatchId = $scope.DeptBatchList[0].Id;
                }

            } else {
                $scope.CurriculumList = [];
                $scope.DeptBatchList = [];
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Data! " + "Status: " + JSON.stringify(status) + ". " + JSON.stringify(result);
            alertError($scope.ErrorMsg);
        });
    };


});



