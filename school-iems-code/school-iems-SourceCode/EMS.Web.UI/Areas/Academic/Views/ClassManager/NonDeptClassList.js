
//File:Academic_Class List Anjular  Controller
emsApp.controller('ClassListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.ClassList = [];
    $scope.SelectedClass = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.SelectedCurriculumCourseId = 0;
    $scope.SelectedDepartmentId = 0;
    $scope.SelectedSemesterId = 0;
    $scope.SelectedSemesterId = 0;
    $scope.SelectedStudyLevelTermId = 0;
    $scope.SelectedCampusId = 0;
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
        $scope.getClassListExtraData();

    };
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
    /*For Search on data end*/
    $scope.getClassList = function () {
        $http.get($scope.getPagedClassUrl, {
            params: {
                "textkey": $scope.SearchText
            , "pageSize": $scope.PageSize
            , "pageNo": $scope.PageNo
           , "departmentId": $scope.SelectedDepartmentId
           , "curriculumCourseId": $scope.SelectedCurriculumCourseId
           , "semesterId": $scope.SelectedSemesterId
       
           , "studyLevelTermId": $scope.SelectedStudyLevelTermId
           , "campusId": $scope.SelectedCampusId
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
            $scope.ErrorMsg = "Unable to get Class list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getClassListExtraData = function () {
        $http.get($scope.getClassListDataExtraUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.DataExtra.StatusEnumList != null)
                    $scope.StatusEnumList = result.DataExtra.StatusEnumList;
                //DropDown Option Tables
                if (result.DataExtra.DepartmentList != null) {
                    $scope.DepartmentList = result.DataExtra.DepartmentList;
                    $scope.SelectedDepartmentId = $scope.DepartmentList[0].Id;
                }

                if (result.DataExtra.SemesterLevelTermList != null) {
                    $scope.SemesterLevelTermList = result.DataExtra.SemesterLevelTermList;
                    $scope.SelectedSemesterId = $scope.SemesterLevelTermList[0].Id;
                }

                if (result.DataExtra.CurriculumCourseList != null)
                    $scope.CurriculumCourseList = result.DataExtra.CurriculumCourseList;

                if (result.DataExtra.SemesterList != null)
                    $scope.SemesterList = result.DataExtra.SemesterList;

                if (result.DataExtra.StudyLevelTermList != null)
                    $scope.StudyLevelTermList = result.DataExtra.StudyLevelTermList;

                if (result.DataExtra.CampusList != null) {
                    $scope.CampusList = result.DataExtra.CampusList;
                    $scope.SelectedCampusId = $scope.CampusList[0].Id;
                }

                $scope.getPagedClassList();

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Class! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Class! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllClassList = function () {
        $scope.IsLoading++;
        $http.get($scope.getClassListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.ClassList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Class list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Class list! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.deleteClassById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteClassByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.ClassList.indexOf(obj);
                        $scope.ClassList.splice(i, 1);
                        alertSuccess("Data successfully deleted!");
                    } else {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Sorry unable to delete! " + result.Errors.toString();
                        alertError($scope.ErrorMsg);
                    }
                }).error(function (result, status) {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Sorry unable to delete! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
                    alertError($scope.ErrorMsg);
                });
            }
        });
    };
    /*$scope.deleteClassByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteClassByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.ClassList.indexOf(obj);
                        $scope.ClassList.splice(i, 1);
                       alertSuccess("Data successfully deleted!");
                    } else {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Sorry unable to delete! " + result.Errors.toString();
                        alertError($scope.ErrorMsg);
                    }
                }).error(function (result, status) {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Sorry unable to delete! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                    alertError($scope.ErrorMsg);
                });
            }
        });
    };*/

    $scope.selectAll = function ($event) {
        var checkbox = $event.target;
        for (var i = 0; i < $scope.ClassList.length; i++) {
            var entity = $scope.ClassList[i];
            entity.IsSelected = checkbox.checked;
        }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====


    $scope.Init = function (
         getPagedClassUrl
        , deleteClassByIdUrl
        , getClassListDataExtraUrl
        , saveClassListUrl
        , getClassByIdUrl
        , getClassDataExtraUrl
        , saveClassUrl
        ) {
        $scope.getPagedClassUrl = getPagedClassUrl;
        $scope.deleteClassByIdUrl = deleteClassByIdUrl;
        /*bind extra url if need*/
        $scope.getClassListDataExtraUrl = getClassListDataExtraUrl;
        $scope.saveClassListUrl = saveClassListUrl;
        $scope.getClassByIdUrl = getClassByIdUrl;
        $scope.getClassDataExtraUrl = getClassDataExtraUrl;
        $scope.saveClassUrl = saveClassUrl;


        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



