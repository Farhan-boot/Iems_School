
//File:Academic_Semester List Anjular  Controller
emsApp.controller('SuppleExamCtrl', function ($scope, $http, $filter, $document, cfpLoadingBar, $sce) {
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.ApplyCourseList = [];
    $scope.SelectedApplyCourse = null;
    $scope.IsApplyForNewCourse = false;

    //======Custom property and Functions Start=======================================================  

    $scope.getSuppleExamEligibleCourseList = function () {
        $http.get($scope.getSuppleExamEligibleCourseListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.Data != null) {
                    $scope.SuppleExam = result.Data;
                    log("Call");
                    log($scope.SuppleExam);
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

    $scope.addApplyCourse=function (obj)
    {
        $scope.AppliedCourse = $filter('filter')($scope.ApplyCourseList, { ClassId: obj.ClassId }, true)[0];
        if ($scope.AppliedCourse == null || $scope.AppliedCourse==undefined) {
            $scope.ApplyCourseList.push(obj);
        }
    };

    $scope.saveSuppleTakenCourseListByStudent = function () {
        if ($scope.WaitForSuppleSaving) {
            alertWarning("Please wait for saving the supple application.");
            return;
        }
        $scope.WaitForSuppleSaving = true;
        $http.post($scope.saveSuppleTakenCourseListByStudentUrl + "/", $scope.ApplyCourseList).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    $scope.ApplyCourseList = [];
                    $scope.getSuppleExamEligibleCourseList();
                    $scope.WaitForSuppleSaving = false;
                    $scope.IsApplyForNewCourse = false;
                    alertSuccess("Successfully saved Supple Taken By Student data!");
                    //$scope.onAfterSaveSuppleTakenByStudent(result);
                } else {
                    //$scope.SuppleCourse = result.Data;
                    $scope.HasError = true;
                    $scope.WaitForSuppleSaving = false;
                    $scope.ErrorMsg = result.Errors.toString() + " Unable to save Supple Taken By Student!";
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.WaitForSuppleSaving = false;
                $scope.mapRegistrationStatusEnum($scope.beforeRegistrationStatusEnumId);
                $scope.ErrorMsg = JSON.stringify(result).toString() + ". " + " Unable to save Supple Taken By Student! " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    };

    $scope.onApplyCourse=function(obj) {
        $scope.SelectedApplyCourse = obj;
    }
    $scope.showApplyFrom=function() {
        $scope.IsApplyForNewCourse = true;
    }

    $scope.removeCourseFromApplyList = function (index) {
        $scope.ApplyCourseList.splice(index, 1);
    }
    $scope.removeAllCourseFromApplyList = function (index) {
        $scope.ApplyCourseList = [];
        $scope.IsApplyForNewCourse = false;
    }
    $scope.renderHtml = function (html_code) {
        return $sce.trustAsHtml(html_code);
    };

    //======Custom Variables goes hare=====
    $scope.Init = function (
        getSuppleExamEligibleCourseListUrl
        , saveSuppleTakenCourseListByStudentUrl
    ) {
        $scope.getSuppleExamEligibleCourseListUrl = getSuppleExamEligibleCourseListUrl;
        $scope.saveSuppleTakenCourseListByStudentUrl = saveSuppleTakenCourseListByStudentUrl;
        $scope.getSuppleExamEligibleCourseList();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 





});



