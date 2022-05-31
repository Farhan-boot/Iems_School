
//File:Academic_Semester List Anjular  Controller
emsApp.controller('ClassWiseAttendancePrintManagerCtrl', function ($scope, $http, $filter) {
    $scope.ErrorMsg = "";
    $scope.HasError = false;

    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====
    $scope.Init = function (
         getClassWiseAttendancePrintDataExtraUrl
         , getClassSectionListByCourseIdUrl
         , getOfferedCourseListBySemesterIdProgramIdUrl
        ) {
        $scope.getClassWiseAttendancePrintDataExtraUrl = getClassWiseAttendancePrintDataExtraUrl;
        $scope.getClassSectionListByCourseIdUrl = getClassSectionListByCourseIdUrl;
        $scope.getOfferedCourseListBySemesterIdProgramIdUrl = getOfferedCourseListBySemesterIdProgramIdUrl;
        $scope.getGeneralDataExtra();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

    $scope.SelectedSemesterId = 0;
    $scope.SelectedSemester = [];
    $scope.SelectedProgramId = 0;
    $scope.SelectedContinuingBatchId = 0;

    //search items


    $scope.getGeneralDataExtra = function () {
        $http.get($scope.getClassWiseAttendancePrintDataExtraUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.DataExtra.SemesterList != null) {
                    $scope.SemesterList = result.DataExtra.SemesterList;
                    $scope.SelectedSemesterId = $scope.SemesterList[0].Id;
                    $scope.SelectedSemester = $scope.SemesterList[0];
                }
                if (result.DataExtra.ProgramList != null) {
                    $scope.ProgramList = result.DataExtra.ProgramList;
                    //$scope.SelectedProgramId = $scope.ProgramList[0].Id;
                }
                if (result.DataExtra.StudyLevelTermList != null) {
                    $scope.StudyLevelTermList = result.DataExtra.StudyLevelTermList;
                    //$scope.SelectedProgramId = $scope.ProgramList[0].Id;
                }

                if (result.DataExtra.AdmitCardDownloadTypeEnumList != null) {
                    $scope.AdmitCardDownloadTypeEnumList = result.DataExtra.AdmitCardDownloadTypeEnumList;
                    $scope.SelectedAdmitCardDownloadTypeEnum = $scope.AdmitCardDownloadTypeEnumList[0];
                    $scope.updateExamName();
                }
          
                if (result.DataExtra.DeptBatchList != null) {
                    $scope.DeptBatchList = result.DataExtra.DeptBatchList;
                    //$scope.SelectedContinuingBatchId = $scope.DeptBatchList[0].Id;
                }

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg =  result.Errors.toString()+" Unable to load Data!";
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Student Due Report! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.getOfferedCourseList = function () {
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

        $http.get($scope.getOfferedCourseListBySemesterIdProgramIdUrl, {
            params: {
                "semesterId": $scope.SelectedSemesterId
                , "programId": $scope.SelectedProgramId
                , "levelTermId": $scope.SelectedLevelTermId

            }
        }).success(function (result) {
            if (!result.HasError) {
                //log(result.DataExtra.CurriculumList);
                $scope.OfferedCourseList = result.Data;
                //if ($scope.OfferedCourseList != null) {
                //    //$scope.CurriculumList = result.DataExtra.CurriculumList;
                //    $scope.SelectedCurriculumId = $scope.OfferedCourseList[0].Id;
                //}
                //alertError( JSON.stringify(result.DataExtra));
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Course List! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Course List! " + "Status: " + JSON.stringify(status) + ". " + JSON.stringify(result);
            alertError($scope.ErrorMsg);
        });
    };

    $scope.updateExamName = function () {
        log($scope.SelectedAdmitCardDownloadTypeEnum);
        $scope.ExamName = $scope.SelectedAdmitCardDownloadTypeEnum.Name + " " + $scope.SelectedSemester.Name;
    }

    $scope.getClassSectionByCourseIdList = function () {
        //$scope.OfferedCourseList = [];
        $scope.OfferedClassSectionList = [];
        $scope.SelectedClassSectionId = 0;
        //if ($scope.SelectedExamId === null) {
        //    $scope.CurriculumList = [];
        //    return;
        //}
        if ($scope.SelectedSemester != null) {
            $scope.SelectedSemesterId = $scope.SelectedSemester.Id;
        }

        $http.get($scope.getClassSectionListByCourseIdUrl, {
            params: {
                "semesterId": $scope.SelectedSemesterId
                , "programId": $scope.SelectedProgramId
                , "courseId": $scope.SelectedCourseId

            }
        }).success(function (result) {
            if (!result.HasError) {
                //log(result.DataExtra.CurriculumList);
                $scope.OfferedClassSectionList = result.Data;
                log(result.Data);
                if ($scope.OfferedClassSectionList != null) {
                    //$scope.CurriculumList = result.DataExtra.CurriculumList;
                    $scope.SelectedClassSectionId = $scope.OfferedClassSectionList[0].Id;
                }
                //alertError( JSON.stringify(result.DataExtra));
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Class Section! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Class Section! " + "Status: " + JSON.stringify(status) + ". " + JSON.stringify(result);
            alertError($scope.ErrorMsg);
        });
    };

});



