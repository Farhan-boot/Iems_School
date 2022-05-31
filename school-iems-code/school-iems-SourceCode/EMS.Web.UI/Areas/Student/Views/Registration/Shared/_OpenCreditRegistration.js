
//File:Academic_Semester List Anjular  Controller
emsApp.controller('OnlineCourseRegistrationCtrl', function ($scope, $http, $filter, $document, cfpLoadingBar, $sce) {
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.ApplyCourseList = [];
    $scope.SelectedApplyCourse = null;
    $scope.IsCourseRegistration = false;
    $scope.IsAutoSelectLevelTerm = false;
    $scope.RegFromNo = 0;

    $scope.MaxCreditToBeTaken = 0;
    $scope.MinimumCreditToBeTaken = 0;

    $scope.TotalTakenCredit = 0;
    $scope.TotalNewAddedCredit = 0;

    $scope.IsEnableOpenCreditRegistration = false;

    $scope.HasRegConfirmError = false;
    $scope.RegConfirmErrorMsg = "";
    $scope.RegSemesterName = null;

    $scope.SelectedStudyLevelTermId = 3;

    //======Custom property and Functions Start=======================================================  

    $scope.getOnlineRegistrationDataExtra = function () {
        $http.get($scope.getOnlineRegistrationDataExtraUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;

                $scope.StudentsInstructionsForRegistration = result.DataExtra.StudentsInstructionsForRegistration;
                $scope.RegSemesterName = result.DataExtra.RegSemesterName;

                $scope.MaxCreditToBeTaken = result.DataExtra.MaxCredit;
                $scope.MinimumCreditToBeTaken = result.DataExtra.MinCredit;

                $scope.getRegisteredClassList();
                $scope.getEligibleCourseListForOpenCreditRegistration();

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

    $scope.getTakenCreditCount = function () {
        $scope.TotalTakenCredit = 0;
        $scope.TotalNewAddedCredit = 0;
        for (var i = 0; i < $scope.RegisteredClass.RegisteredClassList.length; i++) {
            $scope.TotalTakenCredit += $scope.RegisteredClass.RegisteredClassList[i].Aca_CurriculumCourseJson.CreditLoad;
        }
        for (var i = 0; i < $scope.ApplyCourseList.length; i++) {
            $scope.TotalNewAddedCredit += $scope.ApplyCourseList[i].CreditHours;
        }
    }

    $scope.getEligibleCourseListForOpenCreditRegistration = function () {

        $http.get($scope.getEligibleCourseListForOpenCreditRegistrationUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.Data != null) {
                    $scope.Registration = result.Data;
                    $scope.ApplyCourseList = [];
                    $scope.getTakenCreditCount();
                    if ($scope.Registration.IsShowRegistrationForm) {
                        $scope.RegFromNo = 2;
                    }
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

    $scope.addApplyCourse = function (obj) {
        var takenCredit = $scope.TotalNewAddedCredit + $scope.TotalTakenCredit + obj.CreditHours;
        if (takenCredit > $scope.MaxCreditToBeTaken) {
            var msg = "You can not take more than " + $scope.MaxCreditToBeTaken + " Credit.";
            alertError(msg);
            $scope.HasRegConfirmError = true;
            $scope.RegConfirmErrorMsg = msg;
            return;
        }

        $scope.AppliedCourse = $filter('filter')($scope.ApplyCourseList, { CurriculumCourseId: obj.CurriculumCourseId }, true)[0];
        if ($scope.AppliedCourse == null || $scope.AppliedCourse==undefined) {
            $scope.ApplyCourseList.push(obj);
        }
        $scope.HasRegConfirmError = false;
        $scope.RegConfirmErrorMsg = "";
        $scope.getTakenCreditCount();
    };

    $scope.saveRegistrationCourseForCloseCredit = function () {
        var takenCredit = $scope.TotalNewAddedCredit + $scope.TotalTakenCredit;
        if (takenCredit > $scope.MaxCreditToBeTaken) {
            var msg = "You can not take more than " + $scope.MaxCreditToBeTaken + " Credit.";
            //var msg = "You can't Confirm Registration .";
            //if ($scope.TotalTakenCredit > 0) {
            //    msg += "You have already taken " + $scope.TotalTakenCredit + " Credit! And ";
            //}
            //msg += "You are trying to add " + $scope.TotalNewAddedCredit + " New Credit. ! You can not take more than " + $scope.MaxCreditToBeTaken + " Credit.";
            alertError(msg);
            $scope.HasRegConfirmError = true;
            $scope.RegConfirmErrorMsg = msg;
            return;
        }
        else if (takenCredit < $scope.MinimumCreditToBeTaken) {
            var msg = "You can't register less than " + $scope.MinimumCreditToBeTaken + " credits. Please contact your registration department for help.";

            alertError(msg);
            $scope.HasRegConfirmError = true;
            $scope.RegConfirmErrorMsg = msg;
            return;
        }
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
                    alertSuccess("Successfully saved Course Registration data!");
                    $scope.HasRegConfirmError = false;
                    $scope.RegConfirmErrorMsg = "";
                    $scope.getEligibleCourseListForOpenCreditRegistration();
                    $scope.SelectedApplyCourse = null;
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
        $scope.getEligibleCourseListForOpenCreditRegistration();

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
        $scope.getTakenCreditCount();
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
        , getEligibleCourseListForOpenCreditRegistrationUrl
        , saveRegistrationCourseForCloseCreditUrl
    ) {
        $scope.getOnlineRegistrationDataExtraUrl = getOnlineRegistrationDataExtraUrl;
        $scope.getRegisteredClassListUrl = getRegisteredClassListUrl;
        $scope.getEligibleCourseListForOpenCreditRegistrationUrl = getEligibleCourseListForOpenCreditRegistrationUrl;
        $scope.saveRegistrationCourseForCloseCreditUrl = saveRegistrationCourseForCloseCreditUrl;

        $scope.getOnlineRegistrationDataExtra();

        
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 





});



