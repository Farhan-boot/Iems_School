//File:Academic_Semester List Anjular  Controller
emsApp.controller('StudentRegistrationCtrl', function ($scope, $http, $filter, $document, cfpLoadingBar, $sce) {
    studentRegistrationCtrl = this;// use for this controller function call from another controller.
    $scope.SemesterList = [];
    $scope.SelectedSemester = [];
    $scope.RegSemesterList = [];
    $scope.AllSemesterList = [];
    $scope.SelectedCoursesForReg = [];
    $scope.CourseListForRegistrationJson = null;

    $scope.RegStatus = "";
    $scope.SemesterId = 0;
    $scope.SelectedLevelTermId = 0;
    $scope.SelectedContinuingBatchId = 0;
    $scope.SelectedClassSectionId = 0;

    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.IsAnyCourseSelectedForReg = false;
    $scope.IsSelectAll = false;

    $scope.loadPage = function () {
        $scope.getSemesterListByStudentId();
    }

    $scope.getSemesterListByStudentId = function () {
        $http.get($scope.getSemesterListByStudentIdUrl, {
            params: {
                "studentId": $scope.studentId
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.RegSemesterList = result.DataExtra.SemesterList;
                $scope.SemesterList = angular.copy($scope.RegSemesterList);
                $scope.AllSemesterList = result.DataExtra.AllSemesterList;

                if ($scope.SemesterList.length <= 0) {

                    $scope.SemesterList.push(angular.copy($scope.AllSemesterList[0]));
                }
                //$scope.SelectedSemesterId=result.DataExtra.SelectedSemesterId
                if ($scope.SemesterList != null && $scope.SemesterList.length > 0) {

                    $scope.SelectedSemester = $scope.SemesterList[0];
                    $scope.getClassListBySemesterId();


                    //LoadPaymentDetails($scope.studentId, $scope.SelectedSemester.Id);

                    //$scope.getOfferedClassListByStudentIdSemesterId($scope.SelectedSemester);
                }

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Semester list! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }

        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Semester list! " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);

        });
    };

    $scope.deleteClassStudent = function (obj) {
        var msg = "<span  class='text-danger'><i class='fa fa-question-circle'></i> " +
            "Are you sure want to permanently delete?" +
            "<span ><br>" +
            "<span  class='text-info'><i class='fa fa-info-circle'></i> " +
            "Please be careful that you will not get those data in future!" +
            "<span >";
        bootbox.confirm(msg,
            function(ok) {
                if (ok) {
                    msg = "";

                    $http.get($scope.deleteClassStudentByIdUrl,
                        {
                            params: {
                                "stdId": $scope.studentId,
                                "classId": obj.Id,
                                "semesterId": $scope.SelectedSemester.Id
                            }
                        }).success(function(result) {
                        if (!result.HasError) {

                            $scope.HasError = false;
                            var i = $scope.ClassList.indexOf(obj);
                            $scope.ClassList.splice(i, 1);
                            alertSuccess("Successfully Removed Class from this Student!");

                        } else {
                            $scope.HasError = true;
                            $scope.ErrorMsg = "Unable to Delete Course! " + result.Errors.toString();
                            alertError($scope.ErrorMsg);
                        }

                    }).error(function(result, status) {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Unable to Delete Course! " + JSON.stringify(result).toString();
                        alertError($scope.ErrorMsg);

                    });
                }
            });

    };


    $scope.onChangeSemester = function () {

        if ($scope.SelectedSemester != null) {
            $scope.getClassListBySemesterId();
            //LoadPaymentDetails($scope.studentId, $scope.SelectedSemester.Id);

            $scope.OfferedClassList = [];
            //$scope.getOfferedClassListByStudentIdSemesterId();
        }
    };

    $scope.getClassListBySemesterId = function () {
        $http.get($scope.getClassListBySemesterIdUrl, {
            params: {
                "studentId": $scope.studentId,
                "semesterId": $scope.SelectedSemester.Id
               
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.ClassList = result.Data;
                //log(result.Data);
                $scope.RegStatus = result.DataExtra.RegStatus;
                $scope.RegistrationSummaryCalculate();


            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Class list! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }

        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Class list! " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);

        });
    };


    $scope.onChangeIsCheckedAllSemester = function () {
        if ($scope.isCheckedAllSemester === true) {

            if ($scope.AllSemesterList === null) {
                //$scope.getAllSemesterList();
            } else {
                $scope.SemesterList = angular.copy($scope.AllSemesterList);
            }

        } else {
            $scope.SemesterList = angular.copy($scope.RegSemesterList);
            if ($scope.SemesterList.length <= 0) {

                $scope.SemesterList.push(angular.copy($scope.AllSemesterList[0]));
            }

        }


        var selectedSemester = $filter('filter')($scope.SemesterList, { Id: $scope.SelectedSemester.Id }, true);
       
        if (selectedSemester.length <= 0) {
            $scope.SelectedSemester = $scope.SemesterList[0];

            $scope.getSemesterListByStudentId();

        } else {
            $scope.SelectedSemester = selectedSemester[0];
        }
        
    };

    $scope.getRecalculateDropForRetakeByStdId = function () {
        $http.get($scope.getRecalculateDropForRetakeByStdIdUrl, {
            params: {
                "studentId": $scope.studentId
            }
        }).success(function (result) {
            if (!result.HasError) {

                $scope.getClassListBySemesterId();
                alertSuccess("Successfully Recalculate DropForRetake!");

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to Recalculate DropForRetake! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }

        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to Recalculate DropForRetake! " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);

        });
    };

    // use for this controller function call from another controller.
    this.getRecalculateDropForRetakeByStdIdCallAnotherCtrl = function () {
        $scope.getRecalculateDropForRetakeByStdId();
    }

    $scope.viewClassTakenHistory = function (row) {
        //log(row);
        $scope.ClassForHistory =[];
        $scope.ClassForHistory = row;
        $('#class-taken-history-modal').modal('show');
    };
    
    $scope.renderHtml = function (html_code) {
        return $sce.trustAsHtml(html_code);
    };

    //=================Start Student Course Registration========================


    $scope.getOfferedClassListByStudentIdSemesterId = function () {

        $http.get($scope.getOfferedClassListByStudentIdSemesterIdUrl, {
            params: {
                "studentId": $scope.studentId
                ,"semesterId": $scope.SelectedSemester.Id
                , "levelTermId": $scope.SelectedLevelTermId
                , "classSectionId": $scope.SelectedClassSectionId
                , "batchId": $scope.SelectedContinuingBatchId
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.OfferedClassList = result.Data;
                if (result.DataExtra.CourseListForRegistrationJson != null) {
                    $scope.CourseListForRegistrationJson = result.DataExtra.CourseListForRegistrationJson;
                }

                $scope.IsSelectAll = false;
                $scope.selectAll();
                //OfferedClassList.Taken = 

                //var takenCourses = $scope.ClassList.find(x=>x.)

            } else {
                $scope.OfferedClassList = null;
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Offered Class List! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }

        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Offered Class List! " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);

        });
    };
    
    $scope.getAddCourse = function (classId) {

        var selectedCourse = $scope.OfferedClassList.filter(x => x.Id === classId, true);
        
        if (selectedCourse.length <= 0) {
            alertError("Please select a valid course");
            return;
        }
        selectedCourse[0].IsSelected = true;
        $scope.CourseListForRegistrationJson.ClassList = $scope.OfferedClassList;
        $scope.CourseListForRegistrationJson.StudentId = $scope.studentId;
        $scope.CourseListForRegistrationJson.SemesterId = $scope.SelectedSemester.Id;

        if (selectedCourse[0].RegistrableCourseStatusEnumId === 2 || selectedCourse[0].RegistrableCourseStatusEnumId === 3 || selectedCourse[0].RegistrableCourseStatusEnumId === 10) {

            var msg = "";

            var regType = "";
            if (selectedCourse[0].RegistrableCourseStatusEnumId === 2) {
                regType += "Retake";
            }
            else if (selectedCourse[0].RegistrableCourseStatusEnumId === 3) {
                regType += "Improvement";
            }

            if (selectedCourse[0].RegistrableCourseStatusEnumId === 10) {
                msg = "Are you sure you want to add this course. Student have already Completed this course.";
            } else {
                msg = "Are you sure you want to add this course as " +
                    regType +
                    "? Student have already taken this course in other semester.";
            }

            bootbox.confirm(msg,
                function (yes) {
                if (yes) {
                    $http.post($scope.getAddCourseListUrl + "/", $scope.CourseListForRegistrationJson)
                        .success(function(result) {
                            if (result.Data != null) {
                                $scope.OfferedClassList = result.Data;
                            }
                            //log(result.Data);
                            if (!result.HasError) {
                                $scope.getClassListBySemesterId();
                                alertSuccess("Selected Course Added Successfully!");
                                $scope.ErrorMsg = "";
                            } else {
                                $scope.HasError = true;
                                $scope.ErrorMsg = "Unable to Add Selected Course! " + result.Errors.toString();
                                alertError($scope.ErrorMsg);
                            }
                            $scope.IsSelectAll = false;
                            $scope.selectAll();
                        }).error(function(result, status) {
                            $scope.HasError = true;
                            $scope.ErrorMsg = "Unable to Selected Add Course! " + JSON.stringify(result).toString();
                            alertError($scope.ErrorMsg);

                        });
                } else {
                    selectedCourse[0].IsSelected = false;
                }
            });

        } else {
            $http.post($scope.getAddCourseListUrl + "/", $scope.CourseListForRegistrationJson)
                .success(function (result) {
                    if (result.Data != null) {
                        $scope.OfferedClassList = result.Data;
                    }
                    //log(result.Data);
                    if (!result.HasError) {
                        $scope.getClassListBySemesterId();
                        alertSuccess("Selected Course Added Successfully!");
                        $scope.ErrorMsg = "";
                    } else {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Unable to Add Selected Course! " + result.Errors.toString();
                        alertError($scope.ErrorMsg);
                    }
                    $scope.IsSelectAll = false;
                    $scope.selectAll();
                }).error(function (result, status) {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to Selected Add Course! " + JSON.stringify(result).toString();
                    alertError($scope.ErrorMsg);

                });
        }

        //$http.get($scope.getAddCourseUrl, {
        //    params: {
        //        "stdId": $scope.studentId
        //        , "classId": classId
        //        , "semesterId": $scope.SelectedSemester.Id
        //    }
        //}).success(function (result) {
        //    if (!result.HasError) {

        //        $scope.getClassListBySemesterId();
        //        alertSuccess("Course Added Successfully!");
        //        $scope.ErrorMsg = "";

        //    } else {
        //        $scope.HasError = true;
        //        $scope.ErrorMsg = "Unable to Add Course! " + result.Errors.toString();
        //        alertError($scope.ErrorMsg);
        //    }

        //}).error(function (result, status) {
        //    $scope.HasError = true;
        //    $scope.ErrorMsg = "Unable to Add Course! " + JSON.stringify(result).toString();
        //    alertError($scope.ErrorMsg);

        //});
    };

    $scope.getAddCourseList = function () {

        if ($scope.SelectedCoursesForReg.length <= 0) {
            $scope.IsAnyCourseSelectedForReg = false;
            return;
        }
        //log($scope.OfferedClassList);
        $scope.CourseListForRegistrationJson.ClassList = $scope.OfferedClassList;
        $scope.CourseListForRegistrationJson.StudentId = $scope.studentId;
        $scope.CourseListForRegistrationJson.SemesterId = $scope.SelectedSemester.Id;

        //log($scope.CourseListForRegistrationJson);

        bootbox.confirm("Are you sure you Register " + $scope.SelectedCoursesForReg.length+ " Course at once?", function (yes) {
                if (yes) {
                    $http.post($scope.getAddCourseListUrl + "/", $scope.CourseListForRegistrationJson)
                        .success(function (result) {
                            if (result.Data != null) {
                                $scope.OfferedClassList = result.Data;
                            }
                            //log(result.Data);
                            if (!result.HasError) {
                                $scope.getClassListBySemesterId();
                                alertSuccess("Selected Courses Added Successfully!");
                                $scope.ErrorMsg = "";
                            } else {
                                $scope.HasError = true;
                                $scope.ErrorMsg = "Unable to Add Selected Courses! " + result.Errors.toString();
                                alertError($scope.ErrorMsg);
                            }
                            $scope.IsSelectAll = false;
                            $scope.selectAll();
                    }).error(function (result, status) {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Unable to Selected Add Courses! " + JSON.stringify(result).toString();
                        alertError($scope.ErrorMsg);

                    });
                }
            });
    };

    $scope.getDeleteAllCourse = function () {
        if ($scope.SelectedSemester == null) {
            alertError("Please select Semester");
        }

        $http.get($scope.getDeleteAllCourseUrl, {
            params: {
                  "stdId": $scope.studentId

                , "semesterId": $scope.SelectedSemester.Id
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.getClassListBySemesterId();
                alertSuccess("All Courses Cancel Successfully!");
                $scope.ErrorMsg = "";

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to Cancel All Course! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }

        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to Cancel All Course! " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);

        });
    };
    $scope.getDropAllCourse = function (semester) {
        if ($scope.SelectedSemester == null) {
            alertError("Please select Semester");
        }

        $http.get($scope.getDropAllCourseUrl, {
            params: {
                "stdId": $scope.studentId
                , "semesterId": $scope.SelectedSemester.Id
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.getClassListBySemesterId();
                alertSuccess("All Courses Dropped Successfully!");
                $scope.ErrorMsg = "";

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to Drop All Course! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }

        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to Drop All Course! " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);

        });
    };

    $scope.getDeleteUnDeleteCourseByClassId = function (classId,isDelete) {
        if (classId == null || classId <= 0) {
            alertError("Invalid Course");
        }

        $http.get($scope.getDeleteUnDeleteCourseByClassIdUrl, {
            params: {
                "stdId": $scope.studentId
                , "classId": classId
                , "isDelete": isDelete
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.getClassListBySemesterId();
                alertSuccess("Successfully Course Cancel or UnCancelled!");
                $scope.ErrorMsg = "";

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to Cancel or UnCancelled Course! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }

        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to Cancel or UnCancelled Course! " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);

        });
    };

    $scope.getDropUnDropCourseByClassId = function (classId,isDrop) {

        $http.get($scope.getDropUnDropCourseByClassIdUrl, {
            params: {
                "stdId": $scope.studentId
                , "classId": classId
                , "isDrop": isDrop
            }
        }).success(function (result) {
            if (!result.HasError) {

                $scope.getClassListBySemesterId();
                alertSuccess("Successfully Course Drop or UnDrop!");
                $scope.ErrorMsg = "";

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to Drop or UnDrop Course! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }

        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to Drop or UnDrop Course! " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);

        });
    };

    $scope.getDropForRetakeCourseByClassId = function (semester) {
        $http.get($scope.getDropForRetakeCourseByClassIdUrl, {
            params: {
                "studentId": $scope.studentId
                ,
                //"SemesterId": semester.Id,
                "semesterId": semester.Id
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.OfferedClassList = result.Data;
                //$scope.RegStatus = result.DataExtra.RegStatus;
                //$scope.RegistrationSummaryCalculate();
                //$scope.SemesterId = semester.SemesterId;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to Drop For Retake! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }

        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to Drop For Retake! " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);

        });
    };

    //=================End Student Course Registration==========================

    $scope.selectAll = function () {
        //var checkbox = $event.target;
        for (var i = 0; i < $scope.OfferedClassList.length; i++) {
            var entity = $scope.OfferedClassList[i];
            if (entity.RegistrableCourseStatusEnumId === 1) {
                entity.IsSelected = $scope.IsSelectAll;
            }
        }
        $scope.CheckIfAnyCourseSelectedForReg();
    };

    $scope.CheckIfAnyCourseSelectedForReg = function () {
        $scope.SelectedCoursesForReg = $scope.OfferedClassList.filter(x => x.IsSelected === true && (x.RegistrableCourseStatusEnumId === 1), true);
        //log($scope.SelectedCoursesForReg);
        //for (var i = 0; i < $scope.OfferedClassList.length; i++) {
        //    if ($scope.OfferedClassList[i].IsSelected === true) {
        //        $scope.SelectedCoursesForReg.push($scope.OfferedClassList[i].Id);
        //    }
        //}
        if ($scope.SelectedCoursesForReg.length === $scope.OfferedClassList.filter(x => x.RegistrableCourseStatusEnumId === 1, true).length && $scope.SelectedCoursesForReg.length > 0) {
            $scope.IsAnyCourseSelectedForReg = true;
            $scope.IsSelectAll = true;
        }
        else if ($scope.SelectedCoursesForReg.length > 0) {
            $scope.IsAnyCourseSelectedForReg = true;
            $scope.IsSelectAll = false;
        }
        else if ($scope.SelectedCoursesForReg.length === 0 && $scope.IsSelectAll) {
            alertError("No Selectable Class Available.");
            $scope.IsAnyCourseSelectedForReg = false;
            $scope.IsSelectAll = false;
        }
        else {
            $scope.IsAnyCourseSelectedForReg = false;
            $scope.IsSelectAll = false;
        }
    }

    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====
    $scope.Init = function (
         studentId
        , getSemesterListByStudentIdUrl
        , getClassListBySemesterIdUrl
        , viewPaymentDetailsByStudentIdSemesterIdUrl
        , getOfferedClassListByStudentIdSemesterIdUrl
        , getAddCourseUrl
        , getAddCourseListUrl
        , getDeleteAllCourseUrl
        , getDropAllCourseUrl
        , getDeleteUnDeleteCourseByClassIdUrl
        , getDropUnDropCourseByClassIdUrl
        , getDropForRetakeCourseByClassIdUrl
         , getRecalculateDropForRetakeByStdIdUrl
         , deleteClassStudentByIdUrl

    ) {
        $scope.studentId = studentId;
        $scope.getSemesterListByStudentIdUrl = getSemesterListByStudentIdUrl;
        $scope.getClassListBySemesterIdUrl = getClassListBySemesterIdUrl;
        $scope.viewPaymentDetailsByStudentIdSemesterIdUrl = viewPaymentDetailsByStudentIdSemesterIdUrl;
        $scope.getOfferedClassListByStudentIdSemesterIdUrl = getOfferedClassListByStudentIdSemesterIdUrl;

        //
        $scope.getAddCourseUrl = getAddCourseUrl;
        $scope.getAddCourseListUrl = getAddCourseListUrl;
        $scope.getDeleteAllCourseUrl = getDeleteAllCourseUrl;
        $scope.getDropAllCourseUrl = getDropAllCourseUrl;
        $scope.getDeleteUnDeleteCourseByClassIdUrl = getDeleteUnDeleteCourseByClassIdUrl;
        $scope.getDropUnDropCourseByClassIdUrl = getDropUnDropCourseByClassIdUrl;
        $scope.getDropForRetakeCourseByClassIdUrl = getDropForRetakeCourseByClassIdUrl;

        $scope.getRecalculateDropForRetakeByStdIdUrl = getRecalculateDropForRetakeByStdIdUrl;

        $scope.deleteClassStudentByIdUrl = deleteClassStudentByIdUrl;

        //$scope.testurl = testurl;
        $scope.loadPage();
    };




    $scope.RegistrationSummaryCalculate = function () {
        $scope.TotalCredits = 0;
        $scope.TotalCourse = 0;
        $scope.TotalTheory = 0;
        $scope.TotalLab = 0;
        $scope.TotalCoreCredit = 0;
        $scope.TotalElectiveCredit = 0;

        var takenClass = $filter("filter")($scope.ClassList, { IsAlreadyAdded: true }, true);

        angular.forEach(takenClass, function (value, key) {

            if (value.StudentRegistrationStatusEnumId !== 4) {
                $scope.TotalCredits += value.Aca_CurriculumCourseJson.CreditLoad;
                $scope.TotalCourse += 1;

                if (value.CourseCategoryEnumId == 0) {
                    $scope.TotalTheory += 1;
                }
                if (value.CourseCategoryEnumId == 1) {
                    $scope.TotalLab += 1;
                }
                if (value.CourseTypeEnumId == 0) {
                    $scope.TotalCoreCredit += value.Aca_CurriculumCourseJson.CreditLoad;
                }
                if (value.CourseTypeEnumId == 2) {
                    $scope.TotalElectiveCredit += value.Aca_CurriculumCourseJson.CreditLoad;
                }
            }
            

            


        });
        //var component = $filter("filter")(takenClass, { Aca_CurriculumCourseJson: brakdown.ComponentId }, true);
        //for (var i = 0; i < takenClass.length; i++) {

        /*if ($scope.ClassList.IsAlreadyAdded === true) {
            //$scope.TotalCredits += $scope.ClassList.Aca_CurriculumCourseJson.CreditLoad;
        } else {
            //$scope.TotalCredits -= $scope.ClassList.Aca_CurriculumCourseJson.CreditLoad;
        }*/
        // }
    }
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

    //var LoadPaymentDetails = function (studentId,semesterId) {
    //    $.get($scope.viewPaymentDetailsByStudentIdSemesterIdUrl + "/?studentId=" + studentId + "&semesterId=" + $scope.SelectedSemester.Id, function (data) {
    //        $(".payment-per-semester").html(data);
    //        $(".assessment").addClass("table table-hover table-responsive");
    //        //$('#showAssesmentModal').modal('show');
    //    }).done(function (result) {

    //    }).fail(function (result) {
    //        $(".payment-details").html("Data loading failed!");
    //    });
    //}

});



