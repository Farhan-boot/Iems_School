
//File:Academic_Semester List Anjular  Controller
emsApp.controller('ManageTabulationSheetCtrl', function ($scope, $http, $filter) {
    $scope.ErrorMsg = "";
    $scope.HasError = false;

    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====
    $scope.Init = function (
         getTabulationFilterUrl
         , getPagedClassUrl
         , getCurriculumListUrl
         , getExamListUrl
         , getCalculateCgpaUrl
        , getUpdateLevelTermUrl
        ) {
        $scope.getTabulationFilterUrl = getTabulationFilterUrl;
        $scope.getPagedClassUrl = getPagedClassUrl;
        $scope.getCurriculumListUrl = getCurriculumListUrl;
        $scope.getExamListUrl = getExamListUrl;
        $scope.getCalculateCgpaUrl = getCalculateCgpaUrl;
        $scope.getUpdateLevelTermUrl = getUpdateLevelTermUrl;
        // $scope.loadPage();
        $scope.getTabulationFilter();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

    $scope.SelectedCurriculumCourseId = 0;
    $scope.SelectedProgramId = 0;
    $scope.SelectedSemesterId = 0;
    $scope.SelectedCampusId = 0;
    $scope.SelectedLevelTermId = 0;
    $scope.SelectedProgramTypeEnumId = 0;
    $scope.SelectedExamId = 0;
    $scope.Remark = "";

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
           , "SemesterId": $scope.SelectedSemesterId
           , "programId": $scope.SelectedProgramId
           //, "campusId": $scope.SelectedCampusId
           //, "programTypeEnumId": $scope.SelectedProgramTypeEnumId
           , "curriculumId": $scope.SelectedCurriculumId
           , "examId": $scope.SelectedExamId
             , "levelTermId" : $scope.SelectedLevelTermId
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.ClassList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
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
    $scope.getTabulationFilter = function () {
        $http.get($scope.getTabulationFilterUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                //if (result.DataExtra.StatusEnumList != null)
                //    $scope.StatusEnumList = result.DataExtra.StatusEnumList;
                //DropDown Option Tables
                if (result.DataExtra.ProgramList != null) {
                    $scope.ProgramList = result.DataExtra.ProgramList;
                    $scope.SelectedProgramId = $scope.ProgramList[0].Id;
                }
                if (result.DataExtra.SemesterList != null) {
                    $scope.SemesterList = result.DataExtra.SemesterList;
                    $scope.SelectedSemesterId = $scope.SemesterList[0].Id;
                }

                if (result.DataExtra.CurriculumCourseList != null)
                    $scope.CurriculumCourseList = result.DataExtra.CurriculumCourseList;

                //if (result.DataExtra.SemesterList != null)
                //    $scope.SemesterList = result.DataExtra.SemesterList;

                if (result.DataExtra.ProgramTypeEnumList != null) {
                    $scope.ProgramTypeEnumList = result.DataExtra.ProgramTypeEnumList;
                    $scope.SelectedProgramTypeEnumId = $scope.ProgramTypeEnumList[0].Id;
                }

                if (result.DataExtra.CampusList != null) {
                    $scope.CampusList = result.DataExtra.CampusList;
                    $scope.SelectedCampusId = $scope.CampusList[0].Id;
                }
                if (result.DataExtra.LevelTermList != null) {
                    $scope.LevelTermList = result.DataExtra.LevelTermList;
                    $scope.SelectedLevelTermId = $scope.LevelTermList[0].Id;
                }
                

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
                "SemesterId": $scope.SelectedSemesterId
                //, "programTypeEnumId": $scope.SelectedProgramTypeEnumId
            }
        }).success(function (result) {
            if (!result.HasError) {
                //log(result.DataExtra.CurriculumList);
                $scope.ExamList = result.DataExtra.ExamList;
                if (result.DataExtra.ExamList != null) {
                    //$scope.CurriculumList = result.DataExtra.CurriculumList;
                    $scope.SelectedExamId = $scope.ExamList[0].Id;
                    $scope.getCurriculumList();
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
    $scope.getCurriculumList = function () {
        $scope.ClassList = [];
        if ($scope.SelectedExamId === null) {
            $scope.CurriculumList = [];
           
            return;
        }

        $http.get($scope.getCurriculumListUrl, {
            params: {
                 "SemesterId": $scope.SelectedSemesterId
                ,"programId": $scope.SelectedProgramId
                //,"campusId": $scope.SelectedCampusId
                //, "programTypeEnumId": $scope.SelectedProgramTypeEnumId
          
            }
        }).success(function (result) {
            if (!result.HasError) {
                //log(result.DataExtra.CurriculumList);
                $scope.CurriculumList = result.DataExtra.CurriculumList;
                if (result.DataExtra.CurriculumList != null) {
                    //$scope.CurriculumList = result.DataExtra.CurriculumList;
                    $scope.SelectedCurriculumId = $scope.CurriculumList[0].Id;
                }
                //alertError( JSON.stringify(result.DataExtra));
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Curriculum! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Curriculum! " + "Status: " + JSON.stringify(status) + ". " + JSON.stringify(result);
            alertError($scope.ErrorMsg);
        });
    };

    $scope.NextLevelTerm = [];
    $scope.ngChangeLevelterm = function (id) {
        id = id + 1;
        $scope.NextLevelTerm = $filter('filter')($scope.LevelTermList, { Id: id }, true)[0];
    }


    $scope.getCalculateCgpa = function () {
       // 
        //if ($scope.SelectedCurriculumId === null) {
        //    $scope.ClassList = [];
        //    return;
        //}
        $http.get($scope.getCalculateCgpaUrl, {
            params: {
            "SemesterId": $scope.SelectedSemesterId
           , "programId": $scope.SelectedProgramId
           //, "campusId": $scope.SelectedCampusId
           //, "programTypeEnumId": $scope.SelectedProgramTypeEnumId
           , "curriculumId": $scope.SelectedCurriculumId
           , "examId": $scope.SelectedExamId
            , "levelTermId": $scope.SelectedLevelTermId
            }
        }).success(function (result) {
            if (!result.HasError) {
                alertSuccess("All selected Student's GPA updated successfully.");
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to update Student's GPA! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to update Student's GPA! " + "Status: " + JSON.stringify(status) + ". " + JSON.stringify(result);
            alertError($scope.ErrorMsg);
        });
    };

    $scope.getUpdateLevelTerm = function () {
        bootbox.confirm("Promote All Passed Students to next Level Term (" + $scope.NextLevelTerm.Name + "), are you sure?", function (yes) {
            if (yes) {

        $http.get($scope.getUpdateLevelTermUrl, {
            params: {
                "semesterId": $scope.SelectedSemesterId
                , "programId": $scope.SelectedProgramId
                , "levelTermId": $scope.SelectedLevelTermId
            }
        }).success(function (result) {
            if (!result.HasError) {
                alertSuccess("All selected Student's Level Term updated successfully.");
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to update Student's Level Term! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to update Student's Level Term ! " + "Status: " + JSON.stringify(status) + ". " + JSON.stringify(result);
            alertError($scope.ErrorMsg);
        });

    }
});//

    };

   






});



