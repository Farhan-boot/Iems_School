
//File:Academic_Course List Anjular  Controller
emsApp.controller('CourseListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.CourseList = [];
    $scope.SelectedCourse = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.SelectedStudyLevelTermId = 0;
    $scope.SelectedDepartmentId = 0;
    $scope.SearchByTypeId = 0;
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
        $scope.getCourseListExtraData();
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedCourseList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedCourseList();
    };
    $scope.searchCourseList = function () {
        $scope.getPagedCourseList();
    };
    $scope.getPagedCourseList = function () {
        $scope.getCourseList();
    };
    /*For Search on data end*/
    $scope.getCourseList = function () {
        $http.get($scope.getPagedCourseUrl, {
            params: {
                "textkey": $scope.SearchText
            , "pageSize": $scope.PageSize
            , "pageNo": $scope.PageNo
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.CourseList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Course list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Course list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getCourseListExtraData = function () {
        $http.get($scope.getCourseListDataExtraUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.DataExtra.CourseCategoryEnumList != null)
                    $scope.CourseCategoryEnumList = result.DataExtra.CourseCategoryEnumList;
                //DropDown Option Tables
                if (result.DataExtra.StudyLevelTermList != null)
                    $scope.StudyLevelTermList = result.DataExtra.StudyLevelTermList;
                if ($scope.SelectedStudyLevelTermId <= 0) {
                    $scope.SelectedStudyLevelTermId = $scope.StudyLevelTermList[0].Id;
                }

                if (result.DataExtra.DepartmentList != null) {
                    $scope.DepartmentList = result.DataExtra.DepartmentList;
                    if ($scope.SelectedDepartmentId<=0){
                        $scope.SelectedDepartmentId = $scope.DepartmentList[0].Id;
                    }
                }
                //Get Course List After Extra Data
                $scope.getPagedCourseList();

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Course! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Course! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllCourseList = function () {
        $scope.IsLoading++;
        $http.get($scope.getCourseListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.CourseList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Course list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Course list! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.deleteCourseById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteCourseByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.CourseList.indexOf(obj);
                        $scope.CourseList.splice(i, 1);
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
    /*$scope.deleteCourseByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteCourseByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.CourseList.indexOf(obj);
                        $scope.CourseList.splice(i, 1);
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
        for (var i = 0; i < $scope.CourseList.length; i++) {
            var entity = $scope.CourseList[i];
            entity.IsSelected = checkbox.checked;
        }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====


    $scope.Init = function (
         getPagedCourseUrl
        , deleteCourseByIdUrl
        , getCourseListDataExtraUrl
        , saveCourseListUrl
        , getCourseByIdUrl
        , getCourseDataExtraUrl
        , saveCourseUrl
        ) {
        $scope.getPagedCourseUrl = getPagedCourseUrl;
        $scope.deleteCourseByIdUrl = deleteCourseByIdUrl;
        /*bind extra url if need*/
        $scope.getCourseListDataExtraUrl = getCourseListDataExtraUrl;
        $scope.saveCourseListUrl = saveCourseListUrl;
        $scope.getCourseByIdUrl = getCourseByIdUrl;
        $scope.getCourseDataExtraUrl = getCourseDataExtraUrl;
        $scope.saveCourseUrl = saveCourseUrl;


        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



