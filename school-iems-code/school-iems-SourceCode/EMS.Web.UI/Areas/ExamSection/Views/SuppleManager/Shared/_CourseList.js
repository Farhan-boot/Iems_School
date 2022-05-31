
//File:Supple Course List Collection Anjular  Controller
emsApp.controller('SuppleCourseListCtrl', function ($scope, $http, $filter, $document, cfpLoadingBar, $sce) {

    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.AllExamList = [];
    $scope.WaitForSuppleSaving = false;
    $scope.SelectedExamId = "0";
    $scope.SelectedBaseCourseId = "0";
    $scope.SelectedProgramId = 0;
    $scope.SearchExaminerCampusId = "1";
    $scope.SelectedClass = [];
    $scope.SuppleNewExaminerList = [];

    $scope.CourseList = [];
    $scope.SuppleCourse = null;

    $scope.TotalDeletedExaminer = 0;
    $scope.TotalUnDeletedExaminer = 0;

    $scope.loadPage = function () {
        $scope.getSuppleCourseListDataExtra();
    };

    $scope.loadCourseData = function () {
        $scope.getSuppleCourseInfo();
    };

    $scope.getSuppleCourseListDataExtra = function () {
        $http.get($scope.getSuppleCourseListDataExtraUrl)
            .success(function (result) {
                if (!result.HasError) {
                    //Enum List 
                    $scope.EnrollTypeEnumList = result.DataExtra.EnrollTypeEnumList;
                    $scope.RegistrationStatusEnumList = result.DataExtra.RegistrationStatusEnumList;
                    $scope.ApplicantTypeEnumList = result.DataExtra.ApplicantTypeEnumList;
                    $scope.StatusEnumList = result.DataExtra.StatusEnumList;
                    $scope.CauseOfAbsenceEnumList = result.DataExtra.CauseOfAbsenceEnumList;

                    //Table List

                   

                    if (result.DataExtra.SemesterList != null) {
                        $scope.SemesterList = result.DataExtra.SemesterList;
                    }

                    if (result.DataExtra.AllDepartmentList != null)
                        $scope.AllDepartmentList = result.DataExtra.AllDepartmentList;

                    if (result.DataExtra.ProgramList != null)
                        $scope.ProgramList = result.DataExtra.ProgramList;

                    if (result.DataExtra.SuppleEmployeesJson != null)
                        $scope.SuppleEmployeesJson = result.DataExtra.SuppleEmployeesJson;

                    if (result.DataExtra.ExamList != null) {
                        $scope.ExamList = result.DataExtra.ExamList;
                        $scope.SelectedExam = result.DataExtra.SelectedExam;
                        $scope.SelectedExamId = $scope.SelectedExam.Id;
                        // Get Supple Exam Summary
                        $scope.getSuppleExamSummaryByExamId();
                    }
                    

                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = result.Errors.toString() + " Unable to get Data Extra!";
                    alertError($scope.ErrorMsg);
                }
            })
            .error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = result.Message.toString()+" Unable to get Data Extra! " + "Status: " + status.toString() + ". ";
                alertError($scope.ErrorMsg);
            });
    };

    /*For Search on data start*/

    /*For Search on data end*/

    // Api Calling Start
    $scope.getSuppleCourseListByExamIdByProgramId = function () {
        $http.get($scope.getSuppleCourseListByExamIdByProgramIdUrl, {
            params: {
                "examId": $scope.SelectedExamId,
                "programId": $scope.SelectedProgramId
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.CourseList = result.Data;
                //log($scope.CourseList);

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = result.Errors.toString()+" Unable to get Course!";
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = JSON.stringify(result).toString()+" Unable to get Course! " + "Status: " + JSON.stringify(status).toString() + ". ";
            alertError($scope.ErrorMsg);
        });
    };

    $scope.getSuppleCourseInfo = function () {
        $http.get($scope.getSuppleCourseInfoUrl, {
            params: {
                "baseCourseId": $scope.SelectedBaseCourseId,
                "examId": $scope.SelectedExamId,
                "programId":$scope.SelectedProgramId
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.SuppleCourse = result.Data;

                $scope.examinerCount();
                //log("SuppleCourse");
                //log($scope.SuppleCourse);

            } else {
                $scope.HasError = true;
                $scope.Student = null;
                $scope.ErrorMsg =result.Errors.toString() + " Unable to get Applicant data! ";
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.Student = null;
            $scope.HasError = true;
            $scope.ErrorMsg = JSON.stringify(result).toString() + " Unable to get Applicant ! " + "Status: " + JSON.stringify(status).toString() + ". ";
            alertError($scope.ErrorMsg);
        });
    };

    $scope.searchExaminers = function () {
        $scope.ClassNewExaminerList = [];
        $scope.getExaminerList();
    };

    $scope.getExaminerList = function () {
        $http.get($scope.getExaminerListUrl, {
            params: {
                "deptId": $scope.SearchExaminerByDepartmentId
                , "baseCourseId": $scope.SelectedBaseCourseId
                , "examId": $scope.SelectedExamId
                ,"programId":$scope.SelectedProgramId
                , "campusId": $scope.SearchExaminerCampusId
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.ExaminerList = result.Data;
                //log($scope.ExaminerList);

            } else {
                $scope.HasError = true;
                $scope.Student = null;
                $scope.ErrorMsg = result.Errors.toString() + "Unable to get Examiner data! ";
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.Student = null;
            $scope.HasError = true;
            $scope.ErrorMsg = JSON.stringify(result).toString() + " Unable to get Examiner ! " + "Status: " + JSON.stringify(status).toString() + ". ";
            alertError($scope.ErrorMsg);
        });
    };

    $scope.getSuppleExamSummaryByExamId = function () {
        //alert($scope.SelectedExamId);
        $http.get($scope.getSuppleExamSummaryByExamIdUrl, {
            params: {
                "examId": $scope.SelectedExamId
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.SuppleExamSummary = result.Data;

                //log($scope.SuppleExamSummary);

                if (result.DataExtra.ProgramList != null)
                    $scope.ProgramList = result.DataExtra.ProgramList;


            } else {
                $scope.HasError = true;
                $scope.Student = null;
                $scope.ErrorMsg = result.Errors.toString() + " Unable to get Supple Exam Summary data! ";
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.Student = null;
            $scope.HasError = true;
            $scope.ErrorMsg = JSON.stringify(result).toString() + " Unable to get Supple Exam Summary ! " + "Status: " + JSON.stringify(status).toString() + ". ";
            alertError($scope.ErrorMsg);
        });
    };

    $scope.getApplicantProgramListForAdmitPrintByExamId = function () {
        $http.get($scope.getApplicantProgramListByExamIdUrl, {
            params: {
                "examId": $scope.SelectedExamIdForAdmitPrint
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;

                if (result.DataExtra.ProgramList != null) {
                    $scope.ProgramListForAdmitPrint = result.DataExtra.ProgramList;
                }
                    

            } else {
                $scope.HasError = true;
                $scope.Student = null;
                $scope.ErrorMsg = result.Errors.toString() + "Unable to get Program List! ";
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.Student = null;
            $scope.HasError = true;
            $scope.ErrorMsg = JSON.stringify(result).toString() + " Unable to get Program List ! " + "Status: " + JSON.stringify(status).toString() + ". ";
            alertError($scope.ErrorMsg);
        });
    };

    $scope.getApplicantProgramListForAtnDncPrintByExamId = function () {
        $http.get($scope.getApplicantProgramListByExamIdUrl, {
            params: {
                "examId": $scope.SelectedExamIdForAtnDncPrint
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;

                if (result.DataExtra.ProgramList != null) {
                    $scope.ProgramListForAtnDncPrint = result.DataExtra.ProgramList;
                }


            } else {
                $scope.HasError = true;
                $scope.Student = null;
                $scope.ErrorMsg = result.Errors.toString() + "Unable to get Program List! ";
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.Student = null;
            $scope.HasError = true;
            $scope.ErrorMsg = JSON.stringify(result).toString() + " Unable to get Program List ! " + "Status: " + JSON.stringify(status).toString() + ". ";
            alertError($scope.ErrorMsg);
        });
    };

    $scope.getApplicantCourseListByExamIdByProgramId = function () {
        $http.get($scope.getApplicantCourseListByExamIdByProgramIdUrl, {
            params: {
                "examId": $scope.SelectedExamIdForAtnDncPrint,
                "programId": $scope.SelectedProgramIdForAtnDncPrint
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;

                if (result.DataExtra.CourseList != null) {
                    $scope.CourseListForPrint = result.DataExtra.CourseList;
                    //log("CourseListForPrint");
                    //log($scope.CourseListForPrint);
                }


            } else {
                $scope.HasError = true;
                $scope.Student = null;
                $scope.ErrorMsg = result.Errors.toString() + "Unable to get Course List! ";
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.Student = null;
            $scope.HasError = true;
            $scope.ErrorMsg = JSON.stringify(result).toString() + " Unable to get Course List ! " + "Status: " + JSON.stringify(status).toString() + ". ";
            alertError($scope.ErrorMsg);
        });
    };

    $scope.getApplicantBatchListByExamIdByProgramId = function () {
        $http.get($scope.getApplicantBatchListByExamIdByProgramIdUrl, {
            params: {
                "examId": $scope.SelectedExamIdForAdmitPrint,
                "programId": $scope.SelectedProgramIdForAdmitPrint
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;

                if (result.DataExtra.DeptBatchList != null) {
                    $scope.DeptBatchList = result.DataExtra.DeptBatchList;
                }


            } else {
                $scope.HasError = true;
                $scope.Student = null;
                $scope.ErrorMsg = result.Errors.toString() + "Unable to get Course List! ";
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.Student = null;
            $scope.HasError = true;
            $scope.ErrorMsg = JSON.stringify(result).toString() + " Unable to get Course List ! " + "Status: " + JSON.stringify(status).toString() + ". ";
            alertError($scope.ErrorMsg);
        });
    };

    $scope.getLockUnlockSuppleExamById = function (row, isLock) {
        var message = "";
        if (isLock) {
            message = "Lock";
        } else {
            message = "Unlock";
        }
        $http.get($scope.getLockUnlockSuppleExamByIdUrl, {
            params: {
                "id": row.SuppleExamLockUnlockJson.Id,
                "isLock": isLock
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.Data != null) {
                    row.SuppleExamLockUnlockJson = result.Data;
                }


            } else {
                $scope.HasError = true;
                $scope.Student = null;
                $scope.ErrorMsg = result.Errors.toString() + " Unable to Course " + message+ "! ";
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.Student = null;
            $scope.HasError = true;
            $scope.ErrorMsg = JSON.stringify(result).toString() + " Unable to Course " + message + "! " + "Status: " + JSON.stringify(status).toString() + ". ";
            alertError($scope.ErrorMsg);
        });
    };
    


    $scope.saveSuppleExaminerList = function () {
        if ($scope.SuppleNewExaminerList == null || $scope.SuppleNewExaminerList.length == 0) {
            alertWarning("Please Add Examiner(s) in this Course.");
            return;
        }
        $scope.SuppleEmployeesJson.User_EmployeeJsonList = $scope.SuppleNewExaminerList;
        $scope.SuppleEmployeesJson.ExamId = $scope.SelectedExamId;
        $scope.SuppleEmployeesJson.BaseCourseId = $scope.SelectedBaseCourseId;
        $scope.SuppleEmployeesJson.ProgramId = $scope.SelectedProgramId;

        if ($scope.WaitForSuppleSaving) {
            alertWarning("Please wait for saving Examiner(s) in this Course.");
            return;
        }
        $scope.WaitForSuppleSaving = true;
        $http.post($scope.saveSuppleExaminerListUrl + "/", $scope.SuppleEmployeesJson).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    if (result.Data != null) {
                        $scope.WaitForSuppleSaving = false;
                        $scope.SuppleNewExaminerList = [];
                        $scope.loadCourseData();
                    }
                    alertSuccess("Successfully Saved Examiner(s) in this Course!");
                    
                } else {
                    
                    $scope.HasError = true;
                    $scope.WaitForSuppleSaving = false;
                    $scope.ErrorMsg = result.Errors.toString() + " Unable to Save Examiner(s) in this Course!";
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.WaitForSuppleSaving = false;
                $scope.mapRegistrationStatusEnum($scope.beforeRegistrationStatusEnumId);
                $scope.ErrorMsg = JSON.stringify(result).toString() + ". " + " Unable to Save Examiner(s) in this Course! " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    };


    $scope.getTrashOrUnTrashExaminerById = function (id, isDelete) {
        var msg = "";
        if (isDelete) {
            msg = "Delete";
        } else {
            msg = "Undo";
        }
        bootbox.confirm("Are you sure you want to " + msg + " this data?",
            function (yes) {
                if (yes) {
                    $http.get($scope.getTrashOrUnTrashExaminerByIdUrl, {
                        params: {
                            "id": id
                            , "isDelete": isDelete
                        }
                    }).success(function (result) {
                        if (!result.HasError) {
                            $scope.HasError = false;

                            $scope.loadCourseData();

                        } else {
                            $scope.HasError = true;
                            $scope.Student = null;
                            $scope.ErrorMsg = result.Errors.toString() + "Unable to " + msg + " Examiner! ";
                            alertError($scope.ErrorMsg);
                        }
                    }).error(function (result, status) {
                        $scope.Student = null;
                        $scope.HasError = true;
                        $scope.ErrorMsg = JSON.stringify(result).toString() + " Unable " + msg + " Examiner! " + "Status: " + JSON.stringify(status).toString() + ". ";
                        alertError($scope.ErrorMsg);
                    });
                }
            });

            
        };

    $scope.onChangeExam = function () {

        $scope.refreshUI();
        $scope.SelectedExam = $filter('filter')($scope.ExamList, { Id: $scope.SelectedExamId }, true)[0];
        $scope.getSuppleExamSummaryByExamId();
    }

    $scope.onChangeProgram = function () {

        $scope.SelectedProgram = $filter('filter')($scope.ProgramList, { Id: $scope.SelectedProgramId }, true)[0];

        $scope.refreshUI();
    }

    $scope.onChangeBaseCourse = function (obj) {
        $scope.ExaminerList = [];

        $scope.SelectedBaseCourseId = obj.BaseCourseId;
        $scope.SelectedBaseCourse = obj;
        $scope.loadCourseData();
    };

    $scope.refreshUI=function() {
        $scope.CourseList = [];
        $scope.SuppleCourse = null;
        $scope.SelectedBaseCourse = null;
        $scope.SelectedBaseCourseId = "";
        $scope.ExaminerList = [];
    }
    $scope.selectExaminer = function (obj) {
        var selectedTeacher = $filter('filter')($scope.ExaminerList, { Id: obj.Id }, true)[0];

        if (obj.IsSelected == true && selectedTeacher != null) {

            var checkAddTeacher = $filter('filter')($scope.SuppleNewExaminerList, { Id: obj.Id }, true)[0];
            if (checkAddTeacher == null) {//check already added
                $scope.SuppleNewExaminerList.push(selectedTeacher);
            }

        } else {
            var i = $scope.SuppleNewExaminerList.indexOf(obj);
            $scope.SuppleNewExaminerList.splice(i, 1);
        }
    };
    $scope.classApplicantListModalShow = function (row, regStatusId) {
        $scope.SelectedClass = row;
        $scope.ClassApplicantList = $filter('filter')($scope.SuppleCourse.ApplicantList, { ClassId: row.ClassId, RegistrationStatusEnumId: regStatusId }, true);
        $('#classApplicantListModal').modal('show');
    };

    $scope.applicantTabShow = function (row) {

        $scope.SelectedBaseCourse = row;
        $scope.SelectedBaseCourseId = row.BaseCourseId;
        $scope.loadCourseData();

        $('a[href="#manageApplicantAndExaminer"]').click();
        $('a[href="#applicant"]').click();
    };

    $scope.examinerTabShow = function (row) {
        $scope.SelectedBaseCourse = row;
        $scope.SelectedBaseCourseId = row.BaseCourseId;
        $scope.loadCourseData();

        $('a[href="#manageApplicantAndExaminer"]').click();
        $('a[href="#examiner"]').click();
    };

    $scope.detailsTabShow = function (row) {
        $scope.SelectedBaseCourse = row;
        $scope.SelectedBaseCourseId = row.BaseCourseId;
        $scope.loadCourseData();

        $('a[href="#manageApplicantAndExaminer"]').click();
        $('a[href="#details"]').click();
    };

    $scope.examinerCount = function () {
        $scope.TotalDeletedExaminer = $filter('filter')($scope.SuppleCourse.ExaminerList, { IsDeleted: true }, true).length;
        $scope.TotalUnDeletedExaminer = $filter('filter')($scope.SuppleCourse.ExaminerList, { IsDeleted: false }, true).length;
    }


    $scope.examinerDetailsModalShow = function (row) {
        $scope.SelectedExaminer = row;
        $('#examinerDetailsModal').modal('show');
    };

    $scope.onChangeCourseForPrint = function (obj) {
        $scope.SelectedCourseIdForAtnDncPrint = obj.Id;
    }


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
        getSuppleCourseListDataExtraUrl,
        getSuppleCourseListByExamIdByProgramIdUrl,
        getSuppleCourseInfoUrl,
        getExaminerListUrl,
        saveSuppleExaminerListUrl,
        getTrashOrUnTrashExaminerByIdUrl,
        getSuppleExamSummaryByExamIdUrl,
        getApplicantProgramListByExamIdUrl,
        getApplicantCourseListByExamIdByProgramIdUrl,
        getApplicantBatchListByExamIdByProgramIdUrl,
        getLockUnlockSuppleExamByIdUrl
    ) {
        $scope.getSuppleCourseListDataExtraUrl = getSuppleCourseListDataExtraUrl;
        $scope.getSuppleCourseListByExamIdByProgramIdUrl = getSuppleCourseListByExamIdByProgramIdUrl;
        $scope.getSuppleCourseInfoUrl = getSuppleCourseInfoUrl;
        $scope.getExaminerListUrl = getExaminerListUrl;
        $scope.saveSuppleExaminerListUrl = saveSuppleExaminerListUrl;
        $scope.getTrashOrUnTrashExaminerByIdUrl = getTrashOrUnTrashExaminerByIdUrl;
        $scope.getSuppleExamSummaryByExamIdUrl = getSuppleExamSummaryByExamIdUrl;
        $scope.getApplicantProgramListByExamIdUrl = getApplicantProgramListByExamIdUrl;
        $scope.getApplicantCourseListByExamIdByProgramIdUrl = getApplicantCourseListByExamIdByProgramIdUrl;
        $scope.getApplicantBatchListByExamIdByProgramIdUrl = getApplicantBatchListByExamIdByProgramIdUrl;
        $scope.getLockUnlockSuppleExamByIdUrl = getLockUnlockSuppleExamByIdUrl;
        $scope.loadPage();
    };

    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



