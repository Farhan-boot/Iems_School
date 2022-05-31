
//File:Supple Apply Collection Anjular  Controller
emsApp.controller('SuppleApplyCollection', function ($scope, $http, $filter, $document, cfpLoadingBar, $sce) {

    $scope.Student = null;
    $scope.ErrorMsg = "";
    $scope.HasError = false;

    $scope.SearchUserName = "";
    $scope.StudentId = "";
    $scope.SelectedExamId = "";
    $scope.SelectedExam = [];
    $scope.SuppleCourseToSave = [];
    $scope.AllExamList = [];
    $scope.ValidExamList = [];
    $scope.ExamList = [];
    $scope.UrlExamId = "";
    $scope.HasWarning = false;
    $scope.WarningMessage = "";

    

    $scope.isCheckedAllExam = false;
    $scope.WaitForSuppleSaving = false;

    $scope.loadPage = function () {
        $scope.getSuppleApplyDataExtra();
    };

    $scope.checkUrlExamId = function () {
        if ($scope.UrlExamId !== "") {

            $scope.UrlExam = $filter('filter')($scope.ValidExamList, { Id: $scope.UrlExamId }, true)[0];

            if ($scope.UrlExam != null) {
                $scope.ExamList = $scope.ValidExamList;
            } else {
                $scope.UrlExam = $filter('filter')($scope.AllExamList, { Id: $scope.UrlExamId }, true)[0];
                $scope.ExamList = $scope.AllExamList;
                $scope.isCheckedAllExam = true;
            }

            $scope.SelectedExamId = $scope.UrlExamId;
            $scope.SelectedExam = $scope.UrlExam;
        }

        if ($scope.SearchUserName.length > 5) {
            $scope.getStudentByUserName();
        }
    };

    $scope.getSuppleApplyDataExtra = function () {
        $http.get($scope.getSuppleApplyDataExtraUrl)
            .success(function (result) {
                if (!result.HasError) {
                    //Enum List 
                    $scope.EnrollTypeEnumList = result.DataExtra.EnrollTypeEnumList;
                    $scope.RegistrationStatusEnumList = result.DataExtra.RegistrationStatusEnumList;
                    $scope.ApplicantTypeEnumList = result.DataExtra.ApplicantTypeEnumList;
                    $scope.StatusEnumList = result.DataExtra.StatusEnumList;
                    $scope.CauseOfAbsenceEnumList = result.DataExtra.CauseOfAbsenceEnumList;

                    //Table List

                    if (result.DataExtra.ExamList != null) {
                        $scope.ValidExamList = result.DataExtra.ExamList;
                        $scope.ExamList = $scope.ValidExamList;
                    }

                    if (result.DataExtra.ExamList != null) {
                        $scope.AllExamList = result.DataExtra.AllExamList;
                        
                    }
                    $scope.onChangeIsCheckedAllExam();

                    $scope.SelectedExam = result.DataExtra.SelectedExam;
                    $scope.SelectedExamId = $scope.SelectedExam.Id;

                    if (result.DataExtra.SemesterList != null) {
                        $scope.SemesterList = result.DataExtra.SemesterList;
                    }
                   
                    $scope.checkUrlExamId();
                    

                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to get Data Extra! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            })
            .error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Data Extra! " + "Status: " + status.toString() + ". " + result.Message.toString();
                alertError($scope.ErrorMsg);
            });
    };

    /*For Search on data start*/

    /*For Search on data end*/

    // Api Calling Start
    $scope.getStudentByUserName = function () {

        $scope.clearMessage();
        if ($scope.SearchUserName == null || $scope.SearchUserName.length <= 5) {
            alertWarning("Invalid Student Id to search!");
            return;
        }
        $http.get($scope.getStudentByUserNameUrl, {
            params: {
                "un": $scope.SearchUserName
            }

        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.Student = result.Data;
                $scope.StudentId = $scope.Student.Id;

                updateUrlQuery('un', $scope.SearchUserName);
                $scope.getSuppleCourseListByStudentId();

            } else {
                $scope.HasError = true;
                $scope.Student = null;
                updateUrlQuery('un', $scope.SearchUserName);
                $scope.ErrorMsg = result.Errors.toString() + " Unable to get Student data!";
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.Student = null;
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Student ! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.getSuppleCourseListByStudentId = function () {
        $scope.clearMessage();
        $scope.CourseList = [];
        if ($scope.SelectedExamId == null)
            $scope.SelectedExamId = 0;
        $http.get($scope.getSuppleCourseListByStudentIdUrl, {
            params: {
                "stdId": $scope.StudentId,
                "examId": $scope.SelectedExamId
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HasWarning = result.DataExtra.HasWarning;
                $scope.WarningMessage = result.DataExtra.WarningMessage;
                $scope.CourseList = result.Data;

                log($scope.CourseList);
                
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = result.Errors.toString() + " Unable to get Course!";
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Course! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.saveSuppleTakenByStudent = function () {
        /*if (!$scope.validateSuppleTakenByStudent()) {
            return;
        }*/
       
        if ($scope.WaitForSuppleSaving) {
            alertWarning("Please wait for saving the supple application.");
            return;
        }
        $scope.WaitForSuppleSaving = true;
        $http.post($scope.saveSuppleTakenByStudentUrl + "/", $scope.SuppleCourseToSave).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    if (result.Data != null ) {
                        //$scope.SuppleCourse = result.Data;
                        $scope.editSuppleCourseModalHide();
                        $scope.getSuppleCourseListByStudentId();
                        alertSuccess("Successfully saved Supple Taken By Student data!");
                        //updateUrlQuery('id', $scope.SuppleTakenByStudent.Id);
                    }

                    $scope.WaitForSuppleSaving = false;
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


    $scope.clearMessage = function() {
        $scope.HasWarning = false;
        $scope.WarningMessage = "";
    }
    $scope.onChangeExam = function () {
        $scope.SelectedExam = $filter('filter')($scope.ExamList, { Id: $scope.SelectedExamId }, true)[0];
        $scope.getSuppleCourseListByStudentId();
        updateUrlQuery('examId', $scope.SelectedExamId);
    }

    $scope.onChangeCauseOfAbsence = function (obj) {

        // OverlappingOfExam=2, Enum Value
        if (obj.CauseOfAbsenceEnumId === 2) {
            obj.ExamFeeAmount = 0;
        } else {
            if (obj.ExamFeeSettingsAmount !== 0) {
                obj.ExamFeeAmount = obj.ExamFeeSettingsAmount;
            }
        }
    };

    $scope.editSuppleCourse = function (registrationStatusEnumId) {

        $scope.SuppleCourseToSave = angular.copy($scope.SuppleCourse);

        $scope.SuppleCourseToSave.RegistrationStatusEnumId = registrationStatusEnumId;

        var registrationStatus = $filter('filter')($scope.RegistrationStatusEnumList, { Id: registrationStatusEnumId }, true)[0];
        $scope.SuppleCourseToSave.RegistrationStatus = registrationStatus.Name;

        // Call Save API
        $scope.saveSuppleTakenByStudent();
    };

    // This function remove after testing
    $scope.editSuppleCourseModalShow = function (row) {

        $scope.ErrorMsg = "";
        $scope.HasError = false;
       

        $scope.SuppleCourse = angular.copy(row);
        $('#editSuppleCourseModal').modal('show');
    };

    $scope.editSuppleCourseModalHide = function () {
        $('#editSuppleCourseModal').modal('hide');
    };

    $scope.onChangeIsCheckedAllExam = function () {
        if ($scope.isCheckedAllExam === true) {
            $scope.ExamList = $scope.AllExamList;
        } else {
            $scope.ExamList = $scope.ValidExamList;
        } 
    };

    // Local Function End 


    $scope.renderHtml = function (html_code) {
        return $sce.trustAsHtml(html_code);
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */

    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====
    $scope.Init = function (
            searchUserName
            , urlExamId
         , getStudentByUserNameUrl
        , getSuppleApplyDataExtraUrl
        , getSuppleCourseListByStudentIdUrl
        , saveSuppleTakenByStudentUrl

    ) {
        $scope.SearchUserName = searchUserName;
        $scope.UrlExamId = urlExamId;
        $scope.getStudentByUserNameUrl = getStudentByUserNameUrl;
        $scope.getSuppleApplyDataExtraUrl = getSuppleApplyDataExtraUrl;
        $scope.getSuppleCourseListByStudentIdUrl = getSuppleCourseListByStudentIdUrl;
        $scope.saveSuppleTakenByStudentUrl = saveSuppleTakenByStudentUrl;
        $scope.loadPage();
    };

    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



