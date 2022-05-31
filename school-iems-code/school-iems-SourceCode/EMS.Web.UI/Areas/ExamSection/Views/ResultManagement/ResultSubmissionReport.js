
//File:Academic_Semester List Anjular  Controller
emsApp.controller('SubmissionReportCtrl', function ($scope, $http, $filter) {
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.SelectedSemesterDurationEnumId = 0;
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====
    $scope.Init = function (
         getGetResultSubmissionReporDataExtraUrl
         , getExamListUrl
         , getPagedClassUrl
         //, getCurriculumListUrl
         //, getCalculateCgpaUrl
        ) {
        $scope.getTabulationFilterUrl = getGetResultSubmissionReporDataExtraUrl;
        $scope.getPagedClassUrl = getPagedClassUrl;
      
        $scope.getExamListUrl = getExamListUrl;
        //$scope.getCalculateCgpaUrl = getCalculateCgpaUrl;
        //$scope.getCurriculumListUrl = getCurriculumListUrl;
        // $scope.loadPage();
        $scope.getTabulationFilter();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

    //$scope.SelectedCurriculumCourseId = 0;
    //$scope.SelectedDepartmentId = 0;
    //$scope.SelectedSemesterId = 0;

    //$scope.SelectedCampusId = 0;
    //$scope.SelectedProgramTypeEnumId = 0;
    $scope.SelectedSemesterId = 0;
    $scope.SelectedProgramId = 0;
    $scope.SelectedExamId = 0;
    $scope.SelectedLevelTermId = 0;
    $scope.SelectedLockUnlockTypeId = 0;
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
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
        $scope.getClassList();
    };
    $scope.getClassList = function () {

        //if ($scope.SelectedCurriculumId === null) {
        //    $scope.ClassList = [];
        //    return;
        //}

        $http.get($scope.getPagedClassUrl, {
            params: {
             "pageSize": $scope.PageSize
           , "pageNo": $scope.PageNo
           , "semesterId": $scope.SelectedSemesterId
           , "programId": $scope.SelectedProgramId
           //, "departmentId": $scope.SelectedDepartmentId
           //, "campusId": $scope.SelectedCampusId
           //, "programTypeEnumId": $scope.SelectedProgramTypeEnumId
           //, "curriculumId": $scope.SelectedCurriculumId
           , "examId": $scope.SelectedExamId
                , "levelTermId": $scope.SelectedLevelTermId
                , "lockUnlockTypeId": $scope.SelectedLockUnlockTypeId
            }
        }).success(function (result) {
            if (!result.HasError) {
                log(result.Data);
                
                $scope.ClassList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
                if (result.Data.length<=0) {
                    $scope.ErrorMsg = "No Class Found using your search! ";
                    alertError($scope.ErrorMsg);
                }
               

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Class list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Class list! " + "Status: " + JSON.stringify(status) + ". " + JSON.stringify(result);
            alertError($scope.ErrorMsg);
        });
    };

    //$scope.SelectedSemesterId = 0;
    //$scope.SelectedProgramId = 0;
    //$scope.SelectedExamId = 0;
    $scope.getTabulationFilter = function () {
        $http.get($scope.getTabulationFilterUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                //if (result.DataExtra.StatusEnumList != null)
                //    $scope.StatusEnumList = result.DataExtra.StatusEnumList;
                //DropDown Option Tables
                //if (result.DataExtra.DepartmentList != null) {
                //    $scope.DepartmentList = result.DataExtra.DepartmentList;
                //    $scope.SelectedDepartmentId = $scope.DepartmentList[0].Id;
                //}

                if (result.DataExtra.ProgramList != null) {
                    $scope.ProgramList = result.DataExtra.ProgramList;
                    $scope.SelectedProgramId = $scope.ProgramList[0].Id;
                }
                if (result.DataExtra.SemesterList != null) {
                    $scope.SemesterList = result.DataExtra.SemesterList;
                    $scope.SelectedSemesterId = $scope.SemesterList[0].Id;
                }
                if (result.DataExtra.StudyLevelTermList != null) {
                    $scope.StudyLevelTermList = result.DataExtra.StudyLevelTermList;
                    $scope.SelectedLevelTermId = $scope.StudyLevelTermList[0].Id;
                }

                if (result.DataExtra.LockUnlockTypeEnumList != null) {
                    $scope.LockUnlockTypeEnumList = result.DataExtra.LockUnlockTypeEnumList;
                    $scope.SelectedLockUnlockTypeId = $scope.LockUnlockTypeEnumList[0].Id;
                }

                $scope.semesterFilter();
                //if (result.DataExtra.CurriculumCourseList != null)
                //    $scope.CurriculumCourseList = result.DataExtra.CurriculumCourseList;

                //if (result.DataExtra.SemesterList != null)
                //    $scope.SemesterList = result.DataExtra.SemesterList;

                //if (result.DataExtra.ProgramTypeEnumList != null) {
                //    $scope.ProgramTypeEnumList = result.DataExtra.ProgramTypeEnumList;
                //    $scope.SelectedProgramTypeEnumId = $scope.ProgramTypeEnumList[0].Id;
                //}

                //if (result.DataExtra.CampusList != null) {
                //    $scope.CampusList = result.DataExtra.CampusList;
                //    $scope.SelectedCampusId = $scope.CampusList[0].Id;
                //}

                $scope.getExamList();

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

    $scope.getExamList = function () {
        //log($scope.getCurriculumListUrl);

        $http.get($scope.getExamListUrl, {
            params: {
                "semesterId": $scope.SelectedSemesterId
                //, "programTypeEnumId": $scope.SelectedProgramTypeEnumId
            }
        }).success(function (result) {
            if (!result.HasError) {
                //log(result.DataExtra.CurriculumList);
                $scope.ExamList = result.DataExtra.ExamList;
                if (result.DataExtra.ExamList != null) {
                    //$scope.CurriculumList = result.DataExtra.CurriculumList;
                    $scope.SelectedExamId = $scope.ExamList[0].Id;
                    //$scope.getCurriculumList();
                } else {
                    $scope.CurriculumList = [];
                    $scope.ClassList = [];
                }
                //alertError( JSON.stringify(result.DataExtra));
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
    //$scope.getCurriculumList = function () {
    //    $scope.ClassList = [];
    //    if ($scope.SelectedExamId === null) {
    //        $scope.CurriculumList = [];
           
    //        return;
    //    }

    //    $http.get($scope.getCurriculumListUrl, {
    //        params: {
    //             "SemesterId": $scope.SelectedSemesterId
    //            ,"departmentId": $scope.SelectedDepartmentId
    //            ,"campusId": $scope.SelectedCampusId
    //            , "programTypeEnumId": $scope.SelectedProgramTypeEnumId
          
    //        }
    //    }).success(function (result) {
    //        if (!result.HasError) {
    //            //log(result.DataExtra.CurriculumList);
    //            $scope.CurriculumList = result.DataExtra.CurriculumList;
    //            if (result.DataExtra.CurriculumList != null) {
    //                //$scope.CurriculumList = result.DataExtra.CurriculumList;
    //                $scope.SelectedCurriculumId = $scope.CurriculumList[0].Id;
    //            }
    //            //alertError( JSON.stringify(result.DataExtra));
    //        } else {
    //            $scope.HasError = true;
    //            $scope.ErrorMsg = "Unable to get Curriculum! " + result.Errors.toString();
    //            alertError($scope.ErrorMsg);
    //        }
    //    }).error(function (result, status) {
    //        $scope.HasError = true;
    //        $scope.ErrorMsg = "Unable to get Curriculum! " + "Status: " + JSON.stringify(status) + ". " + JSON.stringify(result);
    //        alertError($scope.ErrorMsg);
    //    });
    //};

    //$scope.getCalculateCgpa = function () {
    //   // 
    //    //if ($scope.SelectedCurriculumId === null) {
    //    //    $scope.ClassList = [];
    //    //    return;
    //    //}
    //    $http.get($scope.getCalculateCgpaUrl, {
    //        params: {
    //        "SemesterId": $scope.SelectedSemesterId
    //       , "departmentId": $scope.SelectedDepartmentId
    //       , "campusId": $scope.SelectedCampusId
    //       , "programTypeEnumId": $scope.SelectedProgramTypeEnumId
    //       , "curriculumId": $scope.SelectedCurriculumId
    //       , "examId": $scope.SelectedExamId
    //        }
    //    }).success(function (result) {
    //        if (!result.HasError) {
    //            log(result.Data);
    //            alertSuccess("All selected Student's GPA updated successfully.");
    //        } else {
    //            $scope.HasError = true;
    //            $scope.ErrorMsg = "Unable to update Student's GPA! " + result.Errors.toString();
    //            alertError($scope.ErrorMsg);
    //        }
    //    }).error(function (result, status) {
    //        $scope.HasError = true;
    //        $scope.ErrorMsg = "Unable to update Student's GPA! " + "Status: " + JSON.stringify(status) + ". " + JSON.stringify(result);
    //        alertError($scope.ErrorMsg);
    //    });
    //};
    /////

    $scope.semesterFilter = function () {
        $scope.SelectedSemester = $filter('filter')($scope.SemesterList, { Id: $scope.SelectedSemesterId }, true)[0];

        if ($scope.SelectedSemester != null) {
            $scope.SelectedSemesterDurationEnumId = $scope.SelectedSemester.SemesterDurationEnumId;
        }
    }

});



