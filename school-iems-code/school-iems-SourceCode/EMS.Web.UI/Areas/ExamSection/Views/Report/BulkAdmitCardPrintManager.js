
//File:Academic_Semester List Anjular  Controller
emsApp.controller('BulkAdmitCardPrintManagerCtrl', function ($scope, $http, $filter) {
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.SelectedAdmitCardDownloadTypeEnum = [];
    $scope.SelectedSemester = [];
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====
    $scope.Init = function (
         getGeneralDataExtraUrl
         , getBatchListByProgramIdSemesterId

        ) {
        $scope.getGeneralDataExtraUrl = getGeneralDataExtraUrl;
        $scope.getBatchListByProgramIdSemesterId = getBatchListByProgramIdSemesterId;
        $scope.getGeneralDataExtra();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

    $scope.SelectedSemesterId = 0;
    $scope.SelectedProgramId = 0;
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
                    $scope.SelectedSemester = $scope.SemesterList[0];
                }
                if (result.DataExtra.ProgramList != null) {
                    $scope.ProgramList = result.DataExtra.ProgramList;
                    //$scope.SelectedProgramId = $scope.ProgramList[0].Id;
                }
                if (result.DataExtra.AdmitCardDownloadTypeEnumList != null) {
                    $scope.AdmitCardDownloadTypeEnumList = result.DataExtra.AdmitCardDownloadTypeEnumList;
                    $scope.SelectedAdmitCardDownloadTypeEnum = $scope.AdmitCardDownloadTypeEnumList[0];
                    $scope.updateExamName();
                }
          
                //if (result.DataExtra.DeptBatchList != null) {
                //    $scope.DeptBatchList = result.DataExtra.DeptBatchList;
                //    //$scope.SelectedContinuingBatchId = $scope.DeptBatchList[0].Id;
                //}

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg =  result.Errors.toString()+" Unable to load Data!";
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load data! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.updateExamName = function() {
        log($scope.SelectedAdmitCardDownloadTypeEnum);
        $scope.ExamName = $scope.SelectedAdmitCardDownloadTypeEnum.Name + " " + $scope.SelectedSemester.Name;
    }

    $scope.getBatchList = function () {
        $scope.OfferedCourseList = [];
        $scope.OfferedClassSectionList = [];
        $scope.SelectedCourseId = 0;
        $scope.SelectedClassSectionId = 0;
        if ($scope.SelectedSemester != null) {
            $scope.SelectedSemesterId = $scope.SelectedSemester.Id;
        }
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
                //log(result.DataExtra.CurriculumList);
                $scope.DeptBatchList = result.DataExtra.DeptBatchList;
                //if ($scope.OfferedCourseList != null) {
                //    //$scope.CurriculumList = result.DataExtra.CurriculumList;
                //    $scope.SelectedCurriculumId = $scope.OfferedCourseList[0].Id;
                //}
                //alertError( JSON.stringify(result.DataExtra));
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



