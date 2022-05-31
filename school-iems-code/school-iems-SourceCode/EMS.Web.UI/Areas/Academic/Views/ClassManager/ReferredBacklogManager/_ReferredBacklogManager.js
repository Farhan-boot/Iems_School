
//File: Aca_Exam Anjular  Controller
emsApp.controller('ReferredBacklogStudentManagerCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
    $scope.SearchExaminerCampusId = "1";
    $scope.TotalDeletedExaminer = 0;
    $scope.TotalUnDeletedExaminer = 0;
    $scope.SuppleNewExaminerList = [];
    $scope.SearchExaminerByDepartmentId = 0;
    $scope.IsAlreadyLoadReferredBacklogData = false;



    $scope.loadReferredBacklogPage = function () {
        $scope.getReferredBacklogStudentManagerDataExtra($scope.ExamId);

    };


    
    $scope.onChangeCategory = function (id) {
        $scope.SelectedCategoryEnumId = id;
        $scope.IsAllSelectedAll = false;
        $scope.allUnSelected();
    }
    


    $scope.getReferredBacklogStudentManagerDataExtra = function () {
        $http.get($scope.getReferredBacklogStudentManagerDataExtraUrl, {
            params: { "classId": $scope.ClassId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HasViewPermission = result.HasViewPermission;

                if (result.DataExtra.CategoryEnumList != null) {
                    $scope.CategoryEnumList = result.DataExtra.CategoryEnumList;
                    $scope.SelectedCategoryEnumId = $scope.CategoryEnumList[0].Id;
                }

                if (result.DataExtra.ExamList != null) {
                    $scope.ExamList = result.DataExtra.ExamList;
                    $scope.SelectedExamId = result.DataExtra.SelectedExamId;
                }

                if (result.DataExtra.SuppleEmployeesJson != null)
                    $scope.SuppleEmployeesJson = result.DataExtra.SuppleEmployeesJson;

                if (result.DataExtra.AllDepartmentListForReferredBacklog != null) {
                    $scope.AllDepartmentListForReferredBacklog = result.DataExtra.AllDepartmentListForReferredBacklog;
                    $scope.SearchExaminerByDepartmentId = $scope.AllDepartmentListForReferredBacklog[0].Id;
                }
                


                $scope.getReferredBacklogStudentListByClassId();
                


            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Data Extra! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Data Extra! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };


    $scope.getReferredBacklogStudentListByClassId = function () {
        $http.get($scope.getReferredBacklogStudentListByClassIdUrl, {
            params: {
                "classId": $scope.ClassId,
                "examIdForSupple": $scope.SelectedExamId
            }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HasViewPermission = result.HasViewPermission;

                $scope.ReferredBacklogStudentList = result.Data;
                $scope.getThisCourseExaminerList();

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Data Extra! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Data Extra! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };


    $scope.SaveToReferredBacklogStudentList = [];

    $scope.saveReferredBacklogStudentSingleStudentApply = function (row)
    {
        $scope.SaveToReferredBacklogStudentList = [];
        $scope.pushApply(row);
        $scope.saveReferredBacklogStudentList();
    }

    $scope.saveReferredBacklogStudentSingleStudentCancele = function (row) {
        $scope.SaveToReferredBacklogStudentList = [];
        $scope.pushCancele(row);
        $scope.saveReferredBacklogStudentList();
    }

    $scope.pushApply = function (row) {
        //Approved = 3,
        row.RegistrationStatusEnumId = 3;
        $scope.SaveToReferredBacklogStudentList.push(row);
    }

    $scope.pushCancele = function (row) {
        // Canceled = 5
        row.RegistrationStatusEnumId = 5;
        $scope.SaveToReferredBacklogStudentList.push(row);
    }

    $scope.allUnSelected = function () {
        for (var i = 0; i < $scope.ReferredBacklogStudentList.length; i++) {
            $scope.ReferredBacklogStudentList[i].IsSelected = false;
        }
    }

    $scope.selectAllReferredBacklogStudent = function ($event) {
        var checkbox = $event.target;
        for (var i = 0; i < $scope.ReferredBacklogStudentList.length; i++) {
            var entity = $scope.ReferredBacklogStudentList[i];
            entity.IsSelected = checkbox.checked;
        }
    };

    $scope.categoryWiseAllSelected = function () {
        $scope.CategoryWiseList = $filter('filter')($scope.ReferredBacklogStudentList, { CategoryEnumId: $scope.SelectedCategoryEnumId }, true);
        for (var i = 0; i < $scope.CategoryWiseList.length ; i++) {
            $scope.CategoryWiseList[i].IsSelected = $scope.IsAllSelectedAll;
        }
    }



    $scope.saveReferredBacklogStudentAllStudent = function () {
        $scope.SaveToReferredBacklogStudentList = [];
        $scope.SaveToReferredBacklogStudentList = angular.copy($scope.ReferredBacklogStudentList);
        $scope.saveReferredBacklogStudentList();
    }

    $scope.markAsAdd = function() {
        $scope.SaveToReferredBacklogStudentList = [];

        $scope.CategoryWiseList = $filter('filter')($scope.ReferredBacklogStudentList, { CategoryEnumId: $scope.SelectedCategoryEnumId }, true);

        for (var i = 0; i < $scope.CategoryWiseList.length ; i++) {
            if ($scope.CategoryWiseList[i].IsSelected===true) {
                $scope.pushApply($scope.CategoryWiseList[i]);
            }//end if
        }// end for

        $scope.saveReferredBacklogStudentList();

    }// end function

    $scope.markAsCancel = function () {
        $scope.SaveToReferredBacklogStudentList = [];

        $scope.CategoryWiseList = $filter('filter')($scope.ReferredBacklogStudentList, { CategoryEnumId: $scope.SelectedCategoryEnumId }, true);

        for (var i = 0; i < $scope.CategoryWiseList.length ; i++) {
            if ($scope.CategoryWiseList[i].IsSelected === true) {
                $scope.pushCancele($scope.CategoryWiseList[i]);
            }//end if
        }// end for

        $scope.saveReferredBacklogStudentList();

    }// end function

    $scope.saveReferredBacklogStudentList = function () {
        $http.post($scope.saveReferredBacklogStudentListUrl + "/", $scope.SaveToReferredBacklogStudentList).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    if (result.Data != null) {
                    }
                    $scope.getReferredBacklogStudentListByClassId();
                    alertSuccess("Successfully saved data!");
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to save! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to save! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    };


    //=================== Start Examiner==============
    $scope.ClassNewExaminerList = [];

    $scope.searchExaminers = function (examinerId) {
        $scope.SearchExaminerByDepartmentId = examinerId;
        $scope.ClassNewExaminerList = [];
        $scope.getExaminerList();
    };

    $scope.getExaminerList = function () {
        $http.get($scope.getExaminerListUrl, {
            params: {
                "deptId": $scope.SearchExaminerByDepartmentId
                , "baseCourseId": $scope.ReferredBacklogStudentList[0].BaseCourseId
                , "examId": $scope.SelectedExamId
                , "programId": $scope.ReferredBacklogStudentList[0].ProgramId
                , "campusId": $scope.SearchExaminerCampusId
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.ExaminerList = result.Data;

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

    $scope.getThisCourseExaminerList = function () {
        if ($scope.ReferredBacklogStudentList.length <= 0) {
            return;
        }
        $http.get($scope.getThisCourseExaminerListUrl, {
            params: {
                "examId": $scope.SelectedExamId
                , "programId": $scope.ReferredBacklogStudentList[0].ProgramId
                , "baseCourseId": $scope.ReferredBacklogStudentList[0].BaseCourseId
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.ThisCourseExaminerList = result.Data;
                $scope.examinerCount();

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = result.Errors.toString() + "Unable to get Examiner data! ";
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = JSON.stringify(result).toString() + " Unable to get Examiner ! " + "Status: " + JSON.stringify(status).toString() + ". ";
            alertError($scope.ErrorMsg);
        });
    };

    $scope.examinerCount = function() {
        $scope.TotalDeletedExaminer =
            $filter('filter')($scope.ThisCourseExaminerList, { IsDeleted: true }, true).length;
        $scope.TotalUnDeletedExaminer =
            $filter('filter')($scope.ThisCourseExaminerList, { IsDeleted: false }, true).length;
    };

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

    $scope.saveSuppleExaminerList = function () {
        if ($scope.SuppleNewExaminerList == null || $scope.SuppleNewExaminerList.length == 0) {
            alertWarning("Please Add Examiner(s) in this Course.");
            return;
        }
        $scope.SuppleEmployeesJson.User_EmployeeJsonList = $scope.SuppleNewExaminerList;
        $scope.SuppleEmployeesJson.ExamId = $scope.SelectedExamId;
        $scope.SuppleEmployeesJson.BaseCourseId = $scope.ReferredBacklogStudentList[0].BaseCourseId;
        $scope.SuppleEmployeesJson.ProgramId = $scope.ReferredBacklogStudentList[0].ProgramId;

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
                        $scope.getThisCourseExaminerList();
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

    $scope.examinerDetailsModalShow = function (row) {
        $scope.SelectedExaminer = row;
        $('#examinerDetailsModal').modal('show');
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
                            $scope.getThisCourseExaminerList();
                           

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

    //=================== End Examiner==============
  

    //======Custom property and Functions Start=======================================================  
    //======Custom Variabales goes hare=====

    $scope.Init = function (
        classId,
        getReferredBacklogStudentManagerDataExtraUrl,
        getReferredBacklogStudentListByClassIdUrl,
        saveReferredBacklogStudentListUrl,
        getExaminerListUrl,
        getThisCourseExaminerListUrl,
        saveSuppleExaminerListUrl,
        getTrashOrUnTrashExaminerByIdUrl
    )
    {
        $scope.ClassId = classId;
        $scope.getReferredBacklogStudentManagerDataExtraUrl = getReferredBacklogStudentManagerDataExtraUrl;
        $scope.getReferredBacklogStudentListByClassIdUrl = getReferredBacklogStudentListByClassIdUrl;
        $scope.saveReferredBacklogStudentListUrl = saveReferredBacklogStudentListUrl;
        $scope.getExaminerListUrl = getExaminerListUrl;
        $scope.getThisCourseExaminerListUrl = getThisCourseExaminerListUrl;
        $scope.saveSuppleExaminerListUrl = saveSuppleExaminerListUrl;
        $scope.getTrashOrUnTrashExaminerByIdUrl = getTrashOrUnTrashExaminerByIdUrl;

        
    };


   
    $scope.LoadReferredBacklogData = function() {
        $scope.loadReferredBacklogPage();
        $scope.IsAlreadyLoadReferredBacklogData = true;
    }
 


    $scope.onAfterLoadExamDataExtra = function (result) {
        //var DataExtra=result.DataExtra;
        //DropDown Option Tables
        if (result.DataExtra.GradingPolicyOptionList != null)
            $scope.GradingPolicyOptionList = result.DataExtra.GradingPolicyOptionList;
        if (result.DataExtra.SemesterList != null)
            $scope.SemesterList = result.DataExtra.SemesterList;
       
    };

});

