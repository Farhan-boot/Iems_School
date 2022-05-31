
//File:Supple Course List Collection Anjular  Controller
emsApp.controller('FacultySuppleManagerCtrl', function ($scope, $http, $filter, $document, cfpLoadingBar, $sce) {

    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.WaitForSuppleSaving = false;
    $scope.SelectedExamId = "0";
    $scope.CourseResult = null;

    $scope.examId = "0";
    $scope.courseId = "0";

    $scope.loadPage = function () {
        $scope.getFacultySuppleManagerDataExtra();
    };



    $scope.getFacultySuppleManagerDataExtra = function () {
        $http.get($scope.getFacultySuppleManagerDataExtraUrl)
            .success(function (result) {
                if (!result.HasError) {
                    //Enum List 
                    $scope.EnrollTypeEnumList = result.DataExtra.EnrollTypeEnumList;
                    $scope.RegistrationStatusEnumList = result.DataExtra.RegistrationStatusEnumList;
                    $scope.ApplicantTypeEnumList = result.DataExtra.ApplicantTypeEnumList;
                    $scope.StatusEnumList = result.DataExtra.StatusEnumList;
                    $scope.CauseOfAbsenceEnumList = result.DataExtra.CauseOfAbsenceEnumList;
                    $scope.AttendanceTypeEnumList = result.DataExtra.AttendanceTypeEnumList;
                    //Table List

                    if (result.DataExtra.ExamList != null) {
                        $scope.ExamList = result.DataExtra.ExamList;
                        $scope.SelectedExam = result.DataExtra.SelectedExam;
                        $scope.SelectedExamId = $scope.SelectedExam.Id;
                    }

                    // Get Supple Course List
                    $scope.getEnrolledSuppleCourseListByExamId();


                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = result.Errors.toString() + " Unable to get Data Extra!";
                    alertError($scope.ErrorMsg);
                }
            })
            .error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = result.Message.toString() + " Unable to get Data Extra! " + "Status: " + status.toString() + ". ";
                alertError($scope.ErrorMsg);
            });
    };

    /*For Search on data start*/

    /*For Search on data end*/

    // Api Calling Start


    $scope.getEnrolledSuppleCourseListByExamId = function () {
        $http.get($scope.getEnrolledSuppleCourseListByExamIdUrl, {
            params: {
                "examId": $scope.SelectedExamId
                , "baseCourseId": $scope.courseId
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.CourseList = result.Data;
                log($scope.CourseList);

                if ($scope.examId != "0" && $scope.courseId != '0') {

                    $scope.SelectedBaseCourse = $filter('filter')($scope.CourseList, { BaseCourseId: $scope.courseId }, true)[0];
                    $scope.SelectedBaseCourseId = $scope.SelectedBaseCourse.BaseCourseId;
                    log($scope.SelectedBaseCourseId);
                    $scope.SelectedExamId = $scope.examId;

                    $scope.getSuppleApplicantListByCourseId();
                }
            } else {
                $scope.HasError = true;
                $scope.Student = null;
                $scope.ErrorMsg = result.Errors.toString() + " Unable to get Supple Course List data! ";
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.Student = null;
            $scope.HasError = true;
            $scope.ErrorMsg = JSON.stringify(result).toString() + " Unable to get Supple Course List ! " + "Status: " + JSON.stringify(status).toString() + ". ";
            alertError($scope.ErrorMsg);
        });
    };

    $scope.getSuppleApplicantListByCourseId = function () {
        $http.get($scope.getSuppleApplicantListByCourseIdUrl, {
            params: {
                "baseCourseId": $scope.SelectedBaseCourse.BaseCourseId,
                "examId": $scope.SelectedExamId,
                "programId": $scope.SelectedBaseCourse.ProgramId
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.CourseResult = result.Data;
                $scope.SuppleCourse = $scope.CourseResult.SuppleCourseInfoJson;
                //log(result.Data);
            } else {
                $scope.HasError = true;
                $scope.Student = null;
                $scope.CourseResult = null;
                $scope.ErrorMsg = result.Errors.toString() + " Unable to get Supple Course List data! ";
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.Student = null;
            $scope.CourseResult = null;
            $scope.HasError = true;
            $scope.ErrorMsg = JSON.stringify(result).toString() + " Unable to get Supple Course List ! " + "Status: " + JSON.stringify(status).toString() + ". ";
            alertError($scope.ErrorMsg);
        });
    };

    $scope.saveSuppleResultComponent = function () {

        if ($scope.WaitForSuppleSaving) {
            alertWarning("Please wait for saving this Course Results.");
            return;
        }

        $scope.WaitForSuppleSaving = true;
        $http.post($scope.saveSuppleResultComponentUrl + "/", $scope.CourseResult).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    $scope.WaitForSuppleSaving = false;
                    alertSuccess("Successfully Saved this Course Results!");
                    $scope.getSuppleApplicantListByCourseId();

                } else {

                    $scope.HasError = true;
                    $scope.WaitForSuppleSaving = false;
                    $scope.ErrorMsg = result.Errors.toString() + " Unable to Save this Course Results!";
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.WaitForSuppleSaving = false;
                $scope.ErrorMsg = JSON.stringify(result).toString() + ". " + " Unable to Save this Course Results! " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    };

    $scope.saveAndLoclSuppleResult = function (row) {

        bootbox.confirm("Are you sure you want to confirm & lock this course?", function (yes) {
            if (yes) {
                $scope.saveAndLockSuppleResultComponent();
            }
        });


    };

    $scope.saveAndLockSuppleResultComponent = function () {

        if ($scope.WaitForSuppleSaving) {
            alertWarning("Please wait for saving and Lock this Course Results.");
            return;
        }

        $scope.WaitForSuppleSaving = true;
        $http.post($scope.saveAndLockSuppleResultComponentUrl + "/", $scope.CourseResult).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    $scope.WaitForSuppleSaving = false;

                    //alertSuccess("Successfully Saved and Lock this Course Results!");
                    //$scope.getSuppleApplicantListByCourseId();

                    $scope.getGenerateSuppleResultAfterByCourseId();

                } else {

                    $scope.HasError = true;
                    $scope.WaitForSuppleSaving = false;
                    $scope.ErrorMsg = result.Errors.toString() + " Unable to Save and Lock this Course Results!";
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.WaitForSuppleSaving = false;
                $scope.ErrorMsg = JSON.stringify(result).toString() + ". " + " Unable to Save and Lock this Course Results! " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    };

    $scope.getGenerateSuppleResultAfterByCourseId = function () {
        $http.get($scope.getGenerateSuppleResultByCourseIdUrl, {
            params: {
                "baseCourseId": $scope.SelectedBaseCourse.BaseCourseId,
                "examId": $scope.SelectedExamId,
                "programId": $scope.SelectedBaseCourse.ProgramId
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;
                alertSuccess("Successfully Saved, Generated Grade & Lock this Course Results!");
                $scope.getSuppleApplicantListByCourseId();

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = result.Errors.toString() + " Successfully Saved and Lock but Unable to generated Supple Final Grade! ";
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = JSON.stringify(result).toString() + " Successfully Saved and Lock but Unable to generated Supple Final Grade ! " + "Status: " + JSON.stringify(status).toString() + ". ";
            alertError($scope.ErrorMsg);
        });
    };


    $scope.onChangeExam = function () {

        //$scope.refreshUI();
        $scope.CourseResult = null;
        $scope.SelectedExam = $filter('filter')($scope.ExamList, { Id: $scope.SelectedExamId }, true)[0];
        $scope.getEnrolledSuppleCourseListByExamId();
    }

    $scope.onChangeBaseCourse = function (obj) {

        $scope.SelectedBaseCourseId = obj.BaseCourseId;
        $scope.SelectedBaseCourse = obj;
        $scope.getSuppleApplicantListByCourseId();
    };

    $scope.markHistoryModalShow = function (applicant, semester, applicantCourse) {
        $scope.SelectedApplicant = applicant;
        $scope.SelectedApplicantSemester = semester;
        $scope.SelectedApplicantCourse = applicantCourse;
        $('#markHistoryModal').modal('show');
    };

    $scope.getGenerateSuppleResultByCourseId = function () {
        $http.get($scope.getGenerateSuppleResultByCourseIdUrl, {
            params: {
                "baseCourseId": $scope.SelectedBaseCourse.BaseCourseId,
                "examId": $scope.SelectedExamId,
                "programId": $scope.SelectedBaseCourse.ProgramId
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;
                alertSuccess("Final Grade Sheet generated successfully!");
                $scope.getSuppleApplicantListByCourseId();

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = result.Errors.toString() + " Unable to generated Supple Final Grade! ";
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = JSON.stringify(result).toString() + " Unable to generated Supple Final Grade ! " + "Status: " + JSON.stringify(status).toString() + ". ";
            alertError($scope.ErrorMsg);
        });
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
        examId,
        courseId,
        getFacultySuppleManagerDataExtraUrl,
        getEnrolledSuppleCourseListByExamIdUrl,
        getSuppleApplicantListByCourseIdUrl,
        saveSuppleResultComponentUrl,
        saveAndLockSuppleResultComponentUrl,
        getGenerateSuppleResultByCourseIdUrl

    ) {
        $scope.examId = examId;
        $scope.courseId = courseId;

        $scope.getFacultySuppleManagerDataExtraUrl = getFacultySuppleManagerDataExtraUrl;
        $scope.getEnrolledSuppleCourseListByExamIdUrl = getEnrolledSuppleCourseListByExamIdUrl;
        $scope.getSuppleApplicantListByCourseIdUrl = getSuppleApplicantListByCourseIdUrl;
        $scope.saveSuppleResultComponentUrl = saveSuppleResultComponentUrl;
        $scope.saveAndLockSuppleResultComponentUrl = saveAndLockSuppleResultComponentUrl;
        $scope.getGenerateSuppleResultByCourseIdUrl = getGenerateSuppleResultByCourseIdUrl;

        $scope.loadPage();
    };

    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



