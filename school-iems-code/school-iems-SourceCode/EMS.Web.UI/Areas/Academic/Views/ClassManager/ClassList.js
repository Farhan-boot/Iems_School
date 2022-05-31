
//File:Academic_Class List Anjular  Controller
emsApp.controller('ClassListCtrl', function ($scope, $http, $filter, cfpLoadingBar, $sce) {
    $scope.ClassList = [];
    $scope.OthersDeptClassList = [];
    $scope.SelectedClass = [];
    $scope.ClassNewTeacherList = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.SelectedCurriculumCourseId = 0;
    $scope.SearchTeachersByDepartmentId = 0;
    $scope.SearchTeacherCampusId = '1';
    $scope.SelectedDepartmentId = 0;
    $scope.SelectedSemesterId = 0;
    $scope.SelectedSemesterId = 0;
    $scope.SelectedStudyLevelTermId = 0;
    $scope.SelectedCampusId = 0;
    $scope.SelectedDeptBatchId = -1;
    //$scope.SelectedDurationEnumId = null;
    $scope.selectedProgram = null;
    $scope.ClassEmployeesJson = null;
    $scope.isShowTrashedClass = false;

    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    //search Others items
    $scope.totalOthersItems = 0;
    $scope.totalServerOthersItems = 0;
    $scope.OthersPageSize = 100;
    $scope.OthersPageNo = 1;

    $scope.loadPage = function () {
        $scope.getClassListExtraData();
    };

    /*For Search on data start*/

    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedClassList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedClassList();
    };

    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedClassList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedClassList();
    };


    $scope.changeOthersPageSize = function () {
        $scope.OthersPageNo = 1;
        //$scope.getPagedOthersDeptClassList();
    };
    $scope.changeOthersPageNo = function () {
        //$scope.getPagedOthersDeptClassList();
    };

    $scope.prevOthers = function () {
        $scope.OthersPageNo = $scope.OthersPageNo - 1;
        //$scope.getPagedOthersDeptClassList();
    };
    $scope.nextOthers = function () {
        $scope.OthersPageNo = $scope.OthersPageNo + 1;
        //$scope.getPagedOthersDeptClassList();
    };

    $scope.searchClassList = function () {
        $scope.getPagedClassList();
        //$scope.getPagedOthersDeptClassList(); 
    };
    $scope.getPagedClassList = function () {
        $scope.getClassList();
    };

    //$scope.getPagedOthersDeptClassList = function () {
    //    $scope.getOthersDeptClassList();
    //};

    $scope.SelectedProgramName = "";
    $scope.SelectedSemesterName = "";
    $scope.CourseListCategory = "All Courses";
    $scope.onChangeCourseListCategory=function(courseListCategory) {
        $scope.CourseListCategory = courseListCategory;
    }

    $scope.semesterFilter = function () {
        updateUrlQuery('semesterId', $scope.SelectedSemesterId);
        var semesterObj = $filter('filter')($scope.SemesterList, { Id: $scope.SelectedSemesterId }, true)[0];
        $scope.SelectedSemesterName = semesterObj.Name;
    }
    $scope.levelTermChange = function () {
        updateUrlQuery('levelTermId', $scope.SelectedStudyLevelTermId);
    }

    $scope.deptBatchChange = function () {
        updateUrlQuery('studentBatchId', $scope.SelectedDeptBatchId);
    }

    $scope.programFilter = function () {
        updateUrlQuery('programId', $scope.SelectedProgramId);

        var programObj = $filter('filter')($scope.ProgramList, { Id: $scope.SelectedProgramId }, true)[0];


        var currSemesterObj = $filter('filter')($scope.SemesterList, { Id: $scope.SelectedSemesterId }, true)[0];

        //selected Program will not change if the currently selected program is already of the same semester duration.
        if (currSemesterObj == null ||
            currSemesterObj.SemesterDurationEnumId !== programObj.SemesterDurationEnumId) {

            var semesterObj = $filter('filter')($scope.SemesterList,
                { SemesterDurationEnumId: programObj.SemesterDurationEnumId },
                true);
            if (semesterObj != null && semesterObj.length > 0) {
                $scope.SelectedSemesterId = semesterObj[0].Id;
                $scope.SelectedSemesterName = semesterObj[0].Name;
            }
        }

        //var currStudyLevelTermObj = $filter('filter')($scope.StudyLevelTermList, { Id: $scope.SelectedStudyLevelTermId }, true)[0];

        ////selected Program will not change if the currently selected program is already of the same semester duration.
        //if (currStudyLevelTermObj == null ||
        //    currStudyLevelTermObj.SemesterDurationEnumId !== programObj.SemesterDurationEnumId) {

        //    var studyLevelTermObj = $filter('filter')($scope.StudyLevelTermList,
        //        { SemesterDurationEnumId: programObj.SemesterDurationEnumId },
        //        true);
        //    if (studyLevelTermObj != null && studyLevelTermObj.length > 0) {
        //        $scope.SelectedStudyLevelTermId = studyLevelTermObj[0].Id;
        //    }
        //}

        $scope.selectedProgram = programObj;
        $scope.SelectedProgramName = programObj.ShortName;
    }

    /*For Search on data end*/
    $scope.getClassList = function () {
        $http.get($scope.getPagedClassUrl, {
            params: {
                "textkey": $scope.SearchText
            , "pageSize": $scope.PageSize
            , "pageNo": $scope.PageNo
                 , "programId": $scope.SelectedProgramId
           , "departmentId": $scope.SelectedDepartmentId
           , "curriculumCourseId": $scope.SelectedCurriculumCourseId
           , "semesterId": $scope.SelectedSemesterId
            , "studentBatchId": $scope.SelectedDeptBatchId  //this is use for studyLevelTermId
           , "studyLevelTermId": $scope.SelectedStudyLevelTermId
           , "campusId": $scope.SelectedCampusId
           , "isOfferedByThisDept": true
            , "isShowTrashedClass": $scope.isShowTrashedClass
            }
        }).success(function (result) {
            if (!result.HasError) {
               
                $scope.ClassList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Class list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Class list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    //$scope.getOthersDeptClassList = function () {
    //    $http.get($scope.getPagedClassUrl, {
    //        params: {
    //            "textkey": $scope.SearchText
    //        , "pageSize": $scope.OthersPageSize
    //        , "pageNo": $scope.OthersPageNo
    //       , "programId": $scope.SelectedProgramId
    //       , "departmentId": $scope.SelectedDepartmentId
    //       , "curriculumCourseId": $scope.SelectedCurriculumCourseId
    //       , "semesterId": $scope.SelectedSemesterId
          
    //       , "studyLevelTermId": $scope.SelectedStudyLevelTermId
    //       , "campusId": $scope.SelectedCampusId
    //       , "isOfferedByThisDept": false
    //        }
    //    }).success(function (result) {
    //        if (!result.HasError) {
               
    //            $scope.OthersDeptClassList = result.Data;
    //            $scope.totalOthersItems = result.Count;
    //            $scope.totalServerOthersItems = result.Data.length;
    //        } else {
    //            $scope.HasError = true;
    //            $scope.ErrorMsg = "Unable to get Others Dept Class list data! " + result.Errors.toString();
    //            alertError($scope.ErrorMsg);
    //        }
    //    }).error(function (result, status) {
    //        $scope.HasError = true;
    //        $scope.ErrorMsg = "Unable to get Others Dept Class list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
    //        alertError($scope.ErrorMsg);
    //    });
    //};

    //$scope.SemeserDurationFilter = function (item) {
    //    if ($scope.selectedProgram == null) {
    //        return true;
    //    } else {
    //        return item.SemesterDurationEnumId === $scope.selectedProgram.SemesterDurationEnumId;

    //    }
    //}

    $scope.getClassListExtraData = function () {
        $http.get($scope.getClassListDataExtraUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.DataExtra.StatusEnumList != null)
                    $scope.StatusEnumList = result.DataExtra.StatusEnumList;
                //DropDown Option Tables

                if (result.DataExtra.ProgramList != null) {
                    $scope.ProgramList = result.DataExtra.ProgramList;
                   
                    if ($scope.programId != null && $scope.programId != 0) {

                        $scope.SelectedProgramId =
                            $filter('filter')($scope.ProgramList, { Id: $scope.programId }, true)[0].Id;
                    } else {
                        $scope.SelectedProgramId = $scope.ProgramList[0].Id;
                    }
                }
                if (result.DataExtra.SemesterList != null) {
                    $scope.SemesterList = result.DataExtra.SemesterList;

                    if ($scope.semesterId != null && $scope.semesterId != 0) {

                        $scope.SelectedSemesterId = $filter('filter')($scope.SemesterList, { Id: $scope.semesterId }, true)[0].Id;
                    }
                }

                if (result.DataExtra.AllDepartmentList != null) {
                    $scope.AllDepartmentList = result.DataExtra.AllDepartmentList;
                    $scope.SearchTeachersByDepartmentId = result.DataExtra.AllDepartmentList[0].Id;
                }

                if (result.DataExtra.ClassEmployeesJson != null)
                    $scope.ClassEmployeesJson = result.DataExtra.ClassEmployeesJson;

                if (result.DataExtra.DeptBatchList != null) {
                    $scope.DeptBatchList = result.DataExtra.DeptBatchList;

                    if ($scope.studentBatchId != null && $scope.studentBatchId != -1 && $scope.studentBatchId != 0) {
                        $scope.SelectedDeptBatchId = $filter('filter')($scope.DeptBatchList, { Id: $scope.studentBatchId }, true)[0].Id;
                    }
                }

                if (result.DataExtra.StudyLevelTermList != null) {
                    $scope.StudyLevelTermList = result.DataExtra.StudyLevelTermList;

                    if ($scope.levelTermId != "0" && $scope.levelTermId != "-1") {

                        $scope.SelectedStudyLevelTermId = $filter('filter')($scope.StudyLevelTermList, { Id: $scope.levelTermId }, true)[0].Id;
                    }
                }

                if ($scope.semesterId != "0" && $scope.programId != "0" && $scope.levelTermId !="-1") {

                    updateUrlQuery('msg', null);

                    if ($scope.msg == 'true') {
                        alertSuccess("Bulk Course Offering is Successful.");
                    }
                    $scope.getPagedClassList();
                    //update url
                }
                $scope.programFilter();
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Class! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Class! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllClassList = function () {
        $scope.IsLoading++;
        $http.get($scope.getClassListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.ClassList = result.Data;
                //$scope.DepartmentalClassList = ;
                //    $scope.NonDepartmentalClassList = ;


            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Class list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Class list! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.deleteClassById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteClassByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.ClassList.indexOf(obj);
                        $scope.ClassList.splice(i, 1);
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

    $scope.getTrashUnTrashClassById = function (obj, isPutInTrash) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }

        var massage = "";
        if (isPutInTrash) {
            massage = "Trash";
        } else {
            massage = "UnTrash";
        }

        bootbox.confirm("Are you sure you want to " + massage + " this data?", function (yes) {
            if (yes) {
                $http.get($scope.getTrashUnTrashClassByIdUrl, {
                    params: {
                        "id": obj.Id,
                        "isPutInTrash": isPutInTrash
                    }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.ClassList.indexOf(obj);
                        $scope.ClassList.splice(i, 1);
                        alertSuccess("Data successfully " + massage + "!");
                    } else {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Sorry unable to " + massage + "! " + result.Errors.toString();
                        alertError($scope.ErrorMsg);
                    }
                }).error(function (result, status) {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Sorry unable to " + massage + "! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
                    alertError($scope.ErrorMsg);
                });
            }
        });
    };



    $scope.deleteForceClassById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete Forcefully!");
        }
        bootbox.confirm("Everything will be deleted on this class including Attendance, Marks, Students and Teachers" +
            "<br/>Are you sure you want to delete forcefully this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteForceClassByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.ClassList.indexOf(obj);
                        $scope.ClassList.splice(i, 1);
                        alertSuccess("Data successfully deleted forcefully!");
                    } else {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Sorry unable to delete forcefully! " + result.Errors.toString();
                        alertError($scope.ErrorMsg);
                    }
                }).error(function (result, status) {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Sorry unable to delete forcefully! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
                    alertError($scope.ErrorMsg);
                });
            }
        });
    };
    /*$scope.deleteClassByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteClassByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.ClassList.indexOf(obj);
                        $scope.ClassList.splice(i, 1);
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

    $scope.ClassHistoryMsg = "";
    $scope.openClassHistory = function (history) {

        log("Call history: " + history);
        if (history == null || history === '') {
            $scope.ClassHistoryMsg = '<center style="color:red;font-weight: bold;">No History Available.</center>';
        } else {
            $scope.ClassHistoryMsg = history;
        }

        $('#classHistoryModal').modal('show');
    }

    $scope.renderHtml = function (html_code) {
        return $sce.trustAsHtml(html_code);
    };


    $scope.selectAll = function ($event) {
        var checkbox = $event.target;
        for (var i = 0; i < $scope.ClassList.length; i++) {
            var entity = $scope.ClassList[i];
            entity.IsSelected = checkbox.checked;
        }
    };
    //======Custom property and Functions Start=======================================================  
    $scope.getOfferAnotherSectionModal = function (id) {
        $scope.ClassToDuplicateId = id;
        $('#OfferAnotherSectionModal').modal('show');
    }

    $scope.NumberOfSection = 0;
    $scope.ClassToDuplicateId = 0;

    $scope.getOfferAnotherSectionByClassId = function () {
        $http.get($scope.getOfferAnotherSectionByClassIdUrl, {
            params: {
                "id": $scope.ClassToDuplicateId,
                "numberOfSection": $scope.NumberOfSection
            }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.getPagedClassList();
                $('#OfferAnotherSectionModal').modal('hide');
                alertSuccess("Successfully Offer those Sections!");
                $scope.NumberOfSection = 0;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to Offer those Sections! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to Offer those Sections! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };



    $scope.getOfferedFromPreviousSemesterModal = function () {
        $('#OfferedFromPreviousSemesterModal').modal('show');
    }

    $scope.SourceSemesterId = 0;
    $scope.DestinationSemesterId = 0;

    $scope.getOfferedFromPreviousSemester = function () {
        $http.get($scope.getOfferedFromPreviousSemesterUrl, {
            params: {
                "sourceSemesterId": $scope.SourceSemesterId,
                "destinationSemesterId": $scope.DestinationSemesterId,
                "programId": $scope.SelectedProgramId
            }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.SelectedSemesterId = $scope.DestinationSemesterId; // for show Destination Semester classes in class list
                $scope.getPagedClassList();
                $('#OfferedFromPreviousSemesterModal').modal('hide');
                alertSuccess("Successfully Offer From Previous Semester!");

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to Offer From Previous Semester! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to Offer From Previous Semester! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    //======Custom Variables goes hare=====


    $scope.Init = function (
            semesterId
            , programId
            , levelTermId
        , studentBatchId
            , msg
         ,getPagedClassUrl
        , deleteClassByIdUrl
        , deleteForceClassByIdUrl
        , getClassListDataExtraUrl
        , saveClassListUrl
        , getClassByIdUrl
        , getClassDataExtraUrl
        , saveClassUrl
        , getOfferAnotherSectionByClassIdUrl
        , getOfferedFromPreviousSemesterUrl
        , getTeacherUrl
        , saveClassTeacherUrl
        , getTrashUnTrashClassByIdUrl
        ) {

        $scope.semesterId = semesterId;
        $scope.programId = parseInt(programId);
        $scope.levelTermId = parseInt(levelTermId);
        $scope.studentBatchId = parseInt(studentBatchId);

        $scope.msg = msg;

        $scope.getPagedClassUrl = getPagedClassUrl;
        $scope.deleteClassByIdUrl = deleteClassByIdUrl;
        $scope.deleteForceClassByIdUrl = deleteForceClassByIdUrl;
        /*bind extra url if need*/
        $scope.getClassListDataExtraUrl = getClassListDataExtraUrl;
        $scope.saveClassListUrl = saveClassListUrl;
        $scope.getClassByIdUrl = getClassByIdUrl;
        $scope.getClassDataExtraUrl = getClassDataExtraUrl;
        $scope.saveClassUrl = saveClassUrl;
        $scope.getOfferAnotherSectionByClassIdUrl = getOfferAnotherSectionByClassIdUrl;
        $scope.getOfferedFromPreviousSemesterUrl = getOfferedFromPreviousSemesterUrl;
        $scope.getTeacherUrl = getTeacherUrl;
        $scope.saveClassTeacherUrl = saveClassTeacherUrl;
        $scope.getTrashUnTrashClassByIdUrl = getTrashUnTrashClassByIdUrl;

        $scope.loadPage();
    };
   /*
    $scope.ClassTypeList = [
    {
        Id: 0,
        Name: 'Departmental & Non-Departmental'
    },
    {
        Id: 1,
        Name: 'Departmental'
    }
    ,
    {
        Id: 2,
        Name: 'Non-Departmental'
    }
    ];
    $scope.SelectedClassType = 0;
    $scope.DepartmentClassList = [];
    $scope.NonDepartmentClassList = [];
    $scope.filterClassType = function () {
        //Aca_CurriculumCourseJson.@Aca_CurriculumCourse.Property_IsDepartmental=true
      
        if (SelectedClassType == 1) {
            $scope.ClassList = $filter("filter")($scope.ClassList, { IsDepartmental: true }, true);
        }
      
    }*/
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    $scope.searchTeachers = function (roleEnumId,classId) {
        $scope.ClassNewTeacherList = [];
        $scope.SelectedClassId = classId;
        $scope.getTeacherList(roleEnumId, classId);
    };
    $scope.getTeacherList = function (roleEnumId) {
        if ($scope.SelectedClassId != null) {
            $http.get($scope.getTeacherUrl, {
                params: {
                    "deptId": $scope.SearchTeachersByDepartmentId
                    , "classId": $scope.SelectedClassId //roll type need to attach
                    , "roleEnumId": roleEnumId
                    , "campusId": $scope.SearchTeacherCampusId
                }
            }).success(function (result) {
                if (!result.HasError) {
                    $scope.TeacherList = result.Data;

                } else {

                    toastr.error("Please try again later to get Teacher Data! " + result.Errors.toString(), "Attention! ", { positionClass: "toast-top-center", timeOut: 10000, closeButton: true });
                }
            }).error(function (result) {

                toastr.error("Please try again later to get Teacher Data!", "Attention! ", { positionClass: "toast-top-center", timeOut: 10000, closeButton: true });
            });
        } else {
            toastr.error("Please Select a Valid Class First!", "Attention! ", { positionClass: "toast-top-center", timeOut: 10000, closeButton: true });
        }
    };

    $scope.addClassTeachers = function (roleEnumId, sectionEnumId, statusEnumId) {
        var msg = "";
        if ($scope.ClassNewTeacherList == null || $scope.ClassNewTeacherList.length == 0) {
            msg += "Please Add Teachers in Class. ";
        }

        if (msg === "") {
            $scope.ClassEmployeesJson.User_EmployeeJsonList = $scope.ClassNewTeacherList;
            $scope.ClassEmployeesJson.ClassId = $scope.SelectedClassId;
            $scope.ClassEmployeesJson.RoleEnumId = roleEnumId;
            $scope.ClassEmployeesJson.SectionEnumId = sectionEnumId;
            $scope.ClassEmployeesJson.statusEnumId = statusEnumId;

            $http.post($scope.saveClassTeacherUrl + "/", $scope.ClassEmployeesJson
            ).success(function (result) {
                if (result.HasError) {
                    $scope.error = true;
                    msg += result.Errors.toString();
                    toastr.error(msg, 'Attention! ', { positionClass: 'toast-top-center', timeOut: 10000, closeButton: true });
                } else {
                    msg += "Successfully Saved!";
                    toastr.success(msg, 'Success', { positionClass: 'toast-top-center', timeOut: 10000, closeButton: true });

                    $scope.getTeacherList(roleEnumId);

                    $scope.searchClassList();

                    $scope.ClassNewTeacherList = [];
                }

            }).error(function (result) {
                $scope.error = true;
                msg += "Please try again later to save!!!" + result.Errors.toString();
                toastr.error(msg, 'Attention! ', { positionClass: 'toast-top-center', timeOut: 10000, closeButton: true });

            });
        } else {
            msg += "Please provide all the required information! ";
            toastr.warning(msg, 'Warning', { positionClass: 'toast-top-center', timeOut: 10000, closeButton: true });
        }
    };

    $scope.selectTeacher = function (obj) {
        var selectedTeacher = $filter('filter')($scope.TeacherList, { Id: obj.Id }, true)[0];

        if (obj.IsSelected == true && selectedTeacher != null) {

            var checkAddTeacher = $filter('filter')($scope.ClassNewTeacherList, { Id: obj.Id }, true)[0];
            if (checkAddTeacher == null) {//check already added
                $scope.ClassNewTeacherList.push(selectedTeacher);
            }

        } else {
            var i = $scope.ClassNewTeacherList.indexOf(obj);
            $scope.ClassNewTeacherList.splice(i, 1);
            //$scope.ClassNewTeacherList.splice($index, 1);
        }
    };

    //======Custom property and Functions End========================================================== 

});



