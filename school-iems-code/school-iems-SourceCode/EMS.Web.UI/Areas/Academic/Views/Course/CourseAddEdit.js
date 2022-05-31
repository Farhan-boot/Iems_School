
//File: Aca_Course Anjular  Controller
emsApp.controller('CourseAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.Course = [];
    $scope.CourseId = 0;
    $scope.ErrorMsg = "";
    $scope.HasError = false;

    $scope.loadPage = function (CourseId) {
        if (CourseId != null)
            $scope.CourseId = CourseId;

        $scope.loadCourseExtraData($scope.CourseId);
    };
    $scope.getNewCourse = function () {
        $scope.getCourseById(0);
    };
    $scope.getCourseById = function (CourseId) {
        if (CourseId != null && CourseId !== '')
            $scope.CourseId = CourseId;
        else return;

        $http.get($scope.getCourseByIdUrl, {
            params: { "id": $scope.CourseId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.Course = result.Data;
                updateUrlQuery('id', $scope.Course.Id);
                $scope.onAfterGetCourseById(result);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Course! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Course! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.loadCourseExtraData = function (CourseId) {
        if (CourseId != null)
            $scope.CourseId = CourseId;

        $http.get($scope.getCourseExtraDataUrl, {
            params: { "id": $scope.CourseId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.DataExtra.CourseCategoryEnumList != null)
                    $scope.CourseCategoryEnumList = result.DataExtra.CourseCategoryEnumList;
                $scope.onAfterLoadCourseExtraData(result);
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
    $scope.saveCourse = function () {
        if (!$scope.validateCourse()) {
            return;
        }
        $http.post($scope.saveCourseUrl + "/", $scope.Course).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    if (result.Data != null) {
                        $scope.Course = result.Data;
                        updateUrlQuery('id', $scope.Course.Id);
                    }
                    alertSuccess("Successfully saved Course data!");
                    $scope.onAfterSaveCourse(result);
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to save Course! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to save Course! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    };

    $scope.deleteCourseById = function () {

    };
    $scope.validateCourse = function () {
        return true;
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variabales goes hare=====

    $scope.Init = function (CourseId, getCourseByIdUrl, getCourseExtraDataUrl, saveCourseUrl, deleteCourseByIdUrl) {
        $scope.CourseId = CourseId;
        $scope.getCourseByIdUrl = getCourseByIdUrl;
        $scope.saveCourseUrl = saveCourseUrl;
        $scope.getCourseExtraDataUrl = getCourseExtraDataUrl;
        $scope.deleteCourseByIdUrl = deleteCourseByIdUrl;

        $scope.loadPage(CourseId);
    };

    $scope.onAfterSaveCourse = function (result) {
        //var data=result.Data;
    };
    $scope.onAfterGetCourseById = function (result) {
        //if ($scope.Course != null && $scope.Course.Id <=0) {
        //    $scope.Course.StudyLevelTermId = $scope.StudyLevelTermList[0].Id;
        //    $scope.Course.DepartmentId = $scope.DepartmentList[0].Id;
        //}

    };
    $scope.onAfterLoadCourseExtraData = function (result) {
        //var DataExtra=result.DataExtra;
        //DropDown Option Tables
        if (result.DataExtra.StudyLevelTermList != null){
            $scope.StudyLevelTermList = result.DataExtra.StudyLevelTermList;
        }

        if (result.DataExtra.DepartmentList != null){
            $scope.DepartmentList = result.DataExtra.DepartmentList;
        }

        $scope.getCourseById($scope.CourseId);
    };
    //======Other tabale's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 
});

