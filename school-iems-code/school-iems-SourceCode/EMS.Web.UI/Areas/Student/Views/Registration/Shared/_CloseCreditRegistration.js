
//File:Academic_Semester List Anjular  Controller
emsApp.controller('OnlineCourseRegistrationCtrl', function ($scope, $http, $filter, $document, cfpLoadingBar, $sce) {
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.ApplyCourseList = [];
    $scope.SelectedApplyCourse = null;
    $scope.IsCourseRegistration = false;
    $scope.IsAutoSelectLevelTerm = false;
    $scope.RegFromNo = 0;

    $scope.IsEnableOpenCreditRegistration = false;

    $scope.HasRegConfirmError = false;
    $scope.RegConfirmErrorMsg = "";
    $scope.StudentBatch = "";

    $scope.SelectedStudyLevelTermId = "";

    //======Custom property and Functions Start=======================================================  

    $scope.getOnlineRegistrationDataExtra = function () {
        $http.get($scope.getOnlineRegistrationDataExtraUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;

                if (result.DataExtra.StudyLevelTermList!=null) {
                    $scope.StudyLevelTermList = result.DataExtra.StudyLevelTermList;
                }
                if (result.DataExtra.SelectedLevelTermId != null) {
                    $scope.SelectedStudyLevelTermId = result.DataExtra.SelectedLevelTermId;
                    $scope.IsAutoSelectLevelTerm = true;
                }


                $scope.StudentsInstructionsForRegistration = result.DataExtra.StudentsInstructionsForRegistration;
                $scope.RegSemesterName = result.DataExtra.RegSemesterName;

                $scope.StudentBatch = result.DataExtra.StudentBatch;

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

    $scope.getRegisteredClassList = function () {
        $http.get($scope.getRegisteredClassListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;

                $scope.RegisteredClass = result.Data;
                log($scope.RegisteredClass);


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


    $scope.getEligibleCourseListForCloseCreditRegistrationByLevelTermId = function () {

        if ($scope.SelectedStudyLevelTermId<=0) {
            return;
        }

        $http.get($scope.getEligibleCourseListForCloseCreditRegistrationByLevelTermIdUrl, {
            params: { "levelTermId": $scope.SelectedStudyLevelTermId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.Data != null) {
                    $scope.Registration = result.Data;
                    $scope.ApplyCourseList = $scope.Registration.MustRegistrationCourseList;

                    if ($scope.Registration.IsShowRegistrationForm) {
                        $scope.RegFromNo = 2;
                    }

                    
                    log($scope.Registration);
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
        $scope.AppliedCourse = $filter('filter')($scope.ApplyCourseList, { CurriculumCourseId: obj.CurriculumCourseId }, true)[0];
        if ($scope.AppliedCourse == null || $scope.AppliedCourse==undefined) {
            $scope.ApplyCourseList.push(obj);
        }
    };

    $scope.saveRegistrationCourseForCloseCredit = function () {
        if ($scope.WaitForSuppleSaving) {
            alertWarning("Please wait for saving the Course Registration.");
            return;
        }
        $scope.WaitForSuppleSaving = true;
        $http.post($scope.saveRegistrationCourseForCloseCreditUrl + "/", $scope.ApplyCourseList).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    $scope.ApplyCourseList = [];
                    $scope.getRegisteredClassList();
                    $scope.WaitForSuppleSaving = false;
                    $scope.RegFromNo = 0;
                    alertSuccess("Successfully saved Course Registration data!");
                  
                } else {
                    //$scope.SuppleCourse = result.Data;
                    //$scope.HasError = true;
                    $scope.WaitForSuppleSaving = false;

                    $scope.HasRegConfirmError = true;
                    $scope.RegConfirmErrorMsg = result.Errors.toString();
                    //$scope.ErrorMsg = result.Errors.toString() + " Unable to save Course Registration Data!";
                    //alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.WaitForSuppleSaving = false;
                $scope.mapRegistrationStatusEnum($scope.beforeRegistrationStatusEnumId);
                $scope.ErrorMsg = JSON.stringify(result).toString() + ". " + " Unable to save Course Registration Data! " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    };

    $scope.onApplyCourse=function(obj) {
        $scope.SelectedApplyCourse = obj;
    }

    
    $scope.showLevelTermForm = function () {
        if ($scope.IsAutoSelectLevelTerm) {
            $scope.showRegistrationFrom();
        }
        $scope.RegFromNo = 1;

    }

    $scope.showRegistrationFrom = function () {
        $scope.getEligibleCourseListForCloseCreditRegistrationByLevelTermId();

    }
    $scope.backRegFromNo = function () {
        $scope.RegFromNo = $scope.RegFromNo - 1;
        $scope.messageClear();
    }
    
    $scope.messageClear = function () {
        $scope.HasRegConfirmError = false;
        $scope.RegConfirmErrorMsg = "";
        $scope.ErrorMsg = "";
        $scope.HasError = false;
        $scope.Registration = null;
    }

    
    $scope.removeCourseFromApplyList = function (index) {
        $scope.ApplyCourseList.splice(index, 1);
    }
    $scope.cancelRegistration = function () {
        $scope.RegFromNo = 0;
        $scope.messageClear();
    }
    $scope.renderHtml = function (html_code) {
        return $sce.trustAsHtml(html_code);
    };

    //======Custom Variables goes hare=====
    $scope.Init = function (
        getOnlineRegistrationDataExtraUrl
        , getRegisteredClassListUrl
        , getEligibleCourseListForCloseCreditRegistrationByLevelTermIdUrl
        , saveRegistrationCourseForCloseCreditUrl
    ) {
        $scope.getOnlineRegistrationDataExtraUrl = getOnlineRegistrationDataExtraUrl;
        $scope.getRegisteredClassListUrl = getRegisteredClassListUrl;
        $scope.getEligibleCourseListForCloseCreditRegistrationByLevelTermIdUrl = getEligibleCourseListForCloseCreditRegistrationByLevelTermIdUrl;
        $scope.saveRegistrationCourseForCloseCreditUrl = saveRegistrationCourseForCloseCreditUrl;

        $scope.getRegisteredClassList();
        $scope.getOnlineRegistrationDataExtra();

        
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 





});



