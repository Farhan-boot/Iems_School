emsApp.controller('ClassManagerCtrl', function ($scope, $http, $filter, $sce) {
    $scope.ClassList = [];
    $scope.ClassId = 0;
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.isShowTrashedFaculty = false;
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 20;
    $scope.PageNo = 1;
    $scope.SearchClassByName = "";
    $scope.SearchClassBySemesterId = 0;
    $scope.SearchClassByStudyLevelTermId = 0;
    $scope.SearchClassByDepartmentId = 0;
    $scope.SearchStudentCampusId = '1';
    $scope.SearchTeacherCampusId = '1';
    $scope.Class = [];
    $scope.ClassHistory = [];
    $scope.ClassStudentList = [];
    $scope.ClassNewStudentList = [];
    $scope.ClassWaiverStudentList = [];
    $scope.ClassNewWaiverStudentList = [];
    $scope.StudentList = [];
    $scope.ClassStudentsJson = [];
    $scope.SearchStudentsByDepartmentId = 0;
    $scope.ClassTeacherList = [];
    $scope.ClassNewTeacherList = [];
    $scope.TeacherList = [];
    $scope.ClassTeachersJson = [];
    $scope.SearchTeachersByDepartmentId = 0;
    $scope.ClassStatusEnumList = [];
    $scope.SelectTypeId = 0;
    $scope.SearchStudentsByClassId = 0;
    $scope.SearchClassRoll = null;

    $scope.selectedClassIndex = 0;
    $scope.SelectAllStudents = false;
    $scope.SelectAllTeachers = false;
    $scope.SelectAllWaiverStudents = false;
    $scope.SelectAllWaiverTeachers = false;
    $scope.SelectedSemester = null;
    $scope.ClassRoutineList = [];
    $scope.SelectedClassRoutine = [];
    $scope.ClassTakenByEmployeeHistoryMsg = "";

    /*Initialize*/
    $scope.Init = function (
        ClassId
        , getClassByIdUrl
        , getClassUrl
        , getClassDataExtraUrl
        , saveClassUrl
        , getPagedCurriculumCourseUrl
        , getCurriculumCourseByIdUrl
        , getClassStudentUrl
        , getClassWaiverStudentUrl
        , getStudentUrl
        , saveClassStudentUrl
        , saveClassWaiverStudentUrl
        , saveBulkClassWaiverStudentUrl
        , deleteClassStudentUrl
        , deleteClassAllStudentUrl
        , deleteClassWaiverStudentUrl
        , getClassTeacherUrl
        , getTeacherUrl
        , saveClassTeacherUrl
        , deleteClassTeacherUrl
        , saveClassTakenByStudentListUrl
        , saveClassTakenByEmployeeListUrl
        , getPagedClassRoutineUrl
        , getClassRoutineByIdUrl
        , getClassRoutineDataExtraUrl
        , saveClassRoutineUrl
        , deleteClassRoutineByIdUrl
        , getMarkDistributionPolicyListUrl
        , getClassHistoryByIdUrl
        , trashOrUnTrashTeacherByIdUrl
        , getDropUnDropCourseByClassIdUrl
         , getTrashUntrashCourseByClassIdUrl
        ) {
        $scope.ClassId = ClassId;
        $scope.getClassByIdUrl = getClassByIdUrl;
        $scope.getClassUrl = RootApiUrl + getClassUrl;
        $scope.getClassDataExtraUrl = RootApiUrl + getClassDataExtraUrl;
        $scope.saveClassUrl = RootApiUrl + saveClassUrl;
        $scope.getPagedCurriculumCourseUrl = getPagedCurriculumCourseUrl;
        $scope.getCurriculumCourseByIdUrl = getCurriculumCourseByIdUrl;
        $scope.getClassStudentUrl = RootApiUrl + getClassStudentUrl;
        $scope.getClassWaiverStudentUrl = RootApiUrl + getClassWaiverStudentUrl;
        $scope.getStudentUrl = RootApiUrl + getStudentUrl;
        $scope.saveClassStudentUrl = RootApiUrl + saveClassStudentUrl;
        $scope.saveClassWaiverStudentUrl = RootApiUrl + saveClassWaiverStudentUrl;
        $scope.saveBulkClassWaiverStudentUrl = RootApiUrl + saveBulkClassWaiverStudentUrl;
        $scope.deleteClassStudentUrl = RootApiUrl + deleteClassStudentUrl;
        $scope.deleteClassAllStudentUrl = RootApiUrl + deleteClassAllStudentUrl;
        $scope.deleteClassWaiverStudentUrl = RootApiUrl + deleteClassWaiverStudentUrl;
        $scope.getClassTeacherUrl = RootApiUrl + getClassTeacherUrl;
        $scope.getTeacherUrl = RootApiUrl + getTeacherUrl;
        $scope.saveClassTeacherUrl = RootApiUrl + saveClassTeacherUrl;
        $scope.deleteClassTeacherUrl = RootApiUrl + deleteClassTeacherUrl;
        $scope.saveClassTakenByStudentListUrl = RootApiUrl + saveClassTakenByStudentListUrl;
        $scope.saveClassTakenByEmployeeListUrl = RootApiUrl + saveClassTakenByEmployeeListUrl;

        $scope.getPagedClassRoutineUrl = RootApiUrl + getPagedClassRoutineUrl;
        $scope.getClassRoutineByIdUrl = RootApiUrl + getClassRoutineByIdUrl;
        $scope.getClassRoutineDataExtraUrl = RootApiUrl + getClassRoutineDataExtraUrl;
        $scope.saveClassRoutineUrl = RootApiUrl + saveClassRoutineUrl;
        $scope.deleteClassRoutineByIdUrl = RootApiUrl + deleteClassRoutineByIdUrl;
        $scope.getMarkDistributionPolicyListUrl = getMarkDistributionPolicyListUrl;
        $scope.getClassHistoryByIdUrl = getClassHistoryByIdUrl;
        $scope.trashOrUnTrashTeacherByIdUrl = trashOrUnTrashTeacherByIdUrl;
        $scope.getDropUnDropCourseByClassIdUrl = getDropUnDropCourseByClassIdUrl;

        $scope.getTrashUntrashCourseByClassIdUrl = getTrashUntrashCourseByClassIdUrl;

        $scope.getClassDataExtra();
    };
    $scope.getNewClass = function () {
        $scope.getClassById(0);
        $scope.SelectedCurriculumCourse = null;
    };
    $scope.getClassById = function (ClassId) {
        if (ClassId != null && ClassId !== '')
            $scope.ClassId = ClassId;
        else return;

        $http.get($scope.getClassByIdUrl, {
            params: { "id": $scope.ClassId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.Class = result.Data;

                updateUrlQuery('id', $scope.Class.SemesterId);

                //$scope.apply(function () {
                //    $scope.changeSemester();
                //});
                
                updateUrlQuery('id', $scope.Class.Id);
                $scope.onAfterGetClassById(result);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Class! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Class! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.getClassHistoryById = function () {
       
        $http.get($scope.getClassHistoryByIdUrl, {
            params: { "id": $scope.ClassId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.ClassHistory = result.Data;
                $('#HistoryModal').modal('show');
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Class History! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Class History! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.setDataFromUrl = function () {
        $scope.Class.SemesterId = getQueryString("SemesterId");
        $scope.Class.ProgramId = parseInt(getQueryString("ProgramId"));
      
        $scope.SelectedProgramId = $scope.Class.ProgramId;
        $scope.Class.StudyLevelTermId = parseInt(getQueryString("LevelTermId"));
        $scope.Class.StudentBatchId = parseInt(getQueryString("BatchId"));
        log($scope.Class.StudentBatchId);
        if ($scope.Class.StudentBatchId <= 0) {
            $scope.Class.StudentBatchId = null;
        }
        $scope.SelectedCurriculumId = getQueryString("CurriculumId");
    };
    $scope.setDataToUrl = function () {
        updateUrlQuery("SemesterId", $scope.Class.SemesterId);
        updateUrlQuery("ProgramId", $scope.Class.ProgramId);
        updateUrlQuery("LevelTermId", $scope.Class.StudyLevelTermId);
        updateUrlQuery("BatchId", $scope.Class.StudentBatchId);
        updateUrlQuery("CurriculumId", $scope.SelectedCurriculumId);
    };
    $scope.onAfterGetClassById = function (result) {
        if ($scope.Class != null && $scope.Class.Id > 0) {
            $scope.getCurriculumCourseById($scope.Class.CurriculumCourseId);
            $scope.setDataToUrl();
        } else {
            //$scope.Class.DepartmentId = $scope.DepartmentList[0].Id;
            //$scope.changeDepartment();
            $scope.setDataFromUrl();
            if ($scope.Class.ProgramId == null || $scope.Class.ProgramId < 0)
             {
                $scope.Class.ProgramId = $scope.ProgramList[0].Id;
                
            }
           
            $scope.changeProgram();
        }

        $scope.getPagedClassRoutineList();

        $scope.getClassStudents();
        $scope.getClassTeachers();
    };
    /*Paging Option*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.search();
    };
    $scope.changePageNo = function () {
        $scope.search();
    };
    /*Search*/
    $scope.search = function () {

        $scope.getClassList();

    };
    $scope.searchByClassId = function () {

        if ($scope.SearchByClassId == null || $scope.SearchByClassId < 0) {
            toastr.error("Enter ClassId! ", "Attention! ", { positionClass: "toast-top-center", timeOut: 10000, closeButton: true });
        } else {
            $scope.getClassById($scope.SearchByClassId);
        }

    };
    /*Control*/
    $scope.addNewClass = function () {
        $scope.Class = [];
    };
    $scope.refreshClass = function () {
        $scope.getClassList();
    };
    /*Get Class*/
    $scope.getClassList = function () {

        $http.get($scope.getClassUrl, {
            params: {
                "name": $scope.SearchClassByName,
                "semesterId": $scope.SearchClassBySemesterId,
                "studyLevelTermId": $scope.SearchClassByStudyLevelTermId,
                "departmentId": $scope.SearchClassByDepartmentId,
                "pageSize": $scope.PageSize,
                "pageNo": $scope.PageNo
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.ClassList = result.Data;
                $scope.editClass($scope.selectedClassIndex);
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {

                toastr.error("Please try again later to get Class Data! " + result.Errors.toString(), "Attention! ", { positionClass: "toast-top-center", timeOut: 10000, closeButton: true });
            }
        }).error(function (result) {

            toastr.error("Please try again later to get Class Data!", "Attention! ", { positionClass: "toast-top-center", timeOut: 10000, closeButton: true });
        });
    };
    $scope.getClassDataExtra = function () {
        $http.get($scope.getClassDataExtraUrl)
            .success(function (result) {
                if (!result.HasError) {


                    if (result.DataExtra.SemesterList != null) {
                        $scope.SemesterList = result.DataExtra.SemesterList;

                        $scope.SelectedSemesterId = $scope.SemesterList[0].Id;
                        $scope.Class.SemesterId = $scope.SelectedSemesterId;
                        //$scope.SelectedSemester = $filter('filter')($scope.SemesterList, { Id: $scope.Class.SemesterId }, true)[0];
                        //updateUrlQuery('SemesterId', $scope.SelectedSemesterId);
                    }
                    if (result.DataExtra.DeptBatchList != null)
                        $scope.DeptBatchList = result.DataExtra.DeptBatchList;
                    if (result.DataExtra.StatusEnumList != null)
                        $scope.StatusEnumList = result.DataExtra.StatusEnumList;

                    if (result.DataExtra.ProgramTypeEnumList != null)
                        $scope.ProgramTypeEnumList = result.DataExtra.ProgramTypeEnumList;

                    if (result.DataExtra.AllDepartmentList != null)
                        $scope.AllDepartmentList = result.DataExtra.AllDepartmentList;

                    if (result.DataExtra.DepartmentList != null) {
                        $scope.DepartmentList = result.DataExtra.DepartmentList;
                        $scope.SelectedDepartmentId = $scope.DepartmentList[0].Id;
                    }

                    if (result.DataExtra.ProgramList != null) {
                        $scope.ProgramList = result.DataExtra.ProgramList;
                        $scope.SelectedProgramId = $scope.ProgramList[0].Id;
                        
                    }

                    if (result.DataExtra.CurriculumList != null) {
                        $scope.CurriculumList = result.DataExtra.CurriculumList;
                        $scope.SelectedCurriculumId = $scope.CurriculumList[0].Id;
                    }

                    if (result.DataExtra.StudyLevelTermList != null) {
                        $scope.StudyLevelTermList = result.DataExtra.StudyLevelTermList;
                        $scope.SelectedStudyLevelTermId = $scope.StudyLevelTermList[0].Id;
                       
                    }

                    if (result.DataExtra.ClassSectionList != null) {
                        $scope.ClassSectionList = result.DataExtra.ClassSectionList;
                    }

                    if (result.DataExtra.ClassStudentsJson != null)
                        $scope.ClassStudentsJson = result.DataExtra.ClassStudentsJson;

                    if (result.DataExtra.ClassEmployeesJson != null)
                        $scope.ClassEmployeesJson = result.DataExtra.ClassEmployeesJson;



                    if (result.DataExtra.SemesterLevelTermList != null) {
                        $scope.SemesterLevelTermList = result.DataExtra.SemesterLevelTermList;
                        $scope.SearchStudentsBySemesterId = $scope.SemesterLevelTermList[0].Id;
                    }

                    if (result.DataExtra.CampusList != null)
                        $scope.CampusList = result.DataExtra.CampusList;

                    /*if (result.DataExtra.MarkDistributionPolicyList != null)
                        $scope.MarkDistributionPolicyList = result.DataExtra.MarkDistributionPolicyList;*/

                    //get Class
                    $scope.getClassById($scope.ClassId);

                    $scope.SearchClassBySemesterId = $scope.SemesterList[0].Id;
                    $scope.SearchClassByStudyLevelTermId = $scope.StudyLevelTermList[0].Id;
                    $scope.SearchClassByDepartmentId = $scope.DepartmentList[0].Id;

                    $scope.SearchStudentsByDepartmentId = $scope.DepartmentList[0].Id;
                    $scope.SearchStudentsByStudyLevelTermId = $scope.StudyLevelTermList[0].Id;

                    $scope.SearchTeachersByDepartmentId = $scope.AllDepartmentList[0].Id;
                    //$scope.SearchByTermId = $scope.TermList[0].Id;

                    if (result.DataExtra.EmployeeRoleEnumList != null)
                        $scope.EmployeeRoleEnumList = result.DataExtra.EmployeeRoleEnumList;
                    if (result.DataExtra.EmployeeExaminerRoleEnumList != null)
                        $scope.EmployeeExaminerRoleEnumList = result.DataExtra.EmployeeExaminerRoleEnumList;
                    if (result.DataExtra.EmployeeScrutinizerRoleEnumList != null)
                        $scope.EmployeeScrutinizerRoleEnumList = result.DataExtra.EmployeeScrutinizerRoleEnumList;
                    if (result.DataExtra.EmployeeSectionEnumList != null)
                        $scope.EmployeeSectionEnumList = result.DataExtra.EmployeeSectionEnumList;
                    if (result.DataExtra.EmployeeStatusEnumList != null)
                        $scope.EmployeeStatusEnumList = result.DataExtra.EmployeeStatusEnumList;

                    if (result.DataExtra.StudentEnrollTypeEnumList != null)
                        $scope.StudentEnrollTypeEnumList = result.DataExtra.StudentEnrollTypeEnumList;
                    if (result.DataExtra.StudentRegistrationStatusEnumList != null)
                        $scope.StudentRegistrationStatusEnumList = result.DataExtra.StudentRegistrationStatusEnumList;
                    if (result.DataExtra.StudentStatusEnumList != null)
                        $scope.StudentStatusEnumList = result.DataExtra.StudentStatusEnumList;
                    //For Class Routine
                    if (result.DataExtra.DayEnumList != null)
                        $scope.DayEnumList = result.DataExtra.DayEnumList;
                    if (result.DataExtra.RoomList != null)
                        $scope.RoomList = result.DataExtra.RoomList;
                } else {
                    toastr.error("Please try again later to get Extra Data! " + result.Errors.toString(), "Attention! ", { positionClass: "toast-top-center", timeOut: 10000, closeButton: true });
                }
            })
            .error(function (result) {

                toastr.error("Please try again later to get Extra Data!", "Attention! ", { positionClass: "toast-top-center", timeOut: 10000, closeButton: true });
            });
    };

    $scope.getMarkDistributionPolicyList = function () {
        var isNewCreateMode = false;

        if ($scope.Class.Id === 0) {
            isNewCreateMode = true;
        } else {
            isNewCreateMode = false;

        }

        $http.get($scope.getMarkDistributionPolicyListUrl, {
            params: {
                "creditHour": $scope.SelectedCurriculumCourse.CreditLoad,
                "courseCategoryEnumId": $scope.SelectedCurriculumCourse.CourseCategoryEnumId,
                "semesterId": $scope.Class.SemesterId,
                "programId": $scope.Class.ProgramId,
                "isNewCreateMode": isNewCreateMode
            }

        }).success(function (result) {
            if (!result.HasError) {

                $scope.MarkDistributionPolicyList = result.Data;
                if ($scope.Class.Id == 0) {
                    $scope.Class.RegularExamMarkDistributionPolicyId = result.DataExtra.SelectedMarkDistributionPolicyId;
                }

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Mark Distribution Policy! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {

            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Mark Distribution Policy! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };

    /*Edit Class*/
    $scope.editClass = function ($index) {
        $scope.Class = $scope.ClassList[$index];
        $scope.selectedClassIndex = $index;
    };
    /*Save Class*/
    $scope.saveClass = function () {

        if ($scope.Class.ProgramId != null || $scope.Class.ProgramId > 0) {
            $scope.FilterProgram = $filter('filter')($scope.ProgramList, { Id: $scope.Class.ProgramId }, true)[0];

            $scope.SelectedProgramId = $scope.FilterProgram.Id;
            $scope.Class.DepartmentId = $scope.FilterProgram.DepartmentId;
            updateUrlQuery('ProgramId', $scope.SelectedProgramId);
        }

        $http.post($scope.saveClassUrl + "/", $scope.Class).
            success(function (result, status) {
                if (!result.HasError) {
                    alertSuccess("Successfully Saved Class!");
                    $scope.Class = result.Data;
                    updateUrlQuery('id', $scope.Class.Id);
                    $scope.HasError = false;
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to save Class! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to save Class! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    };
    /**/
    /****Class Students****/
    $scope.getClassStudents = function () {
        $scope.getClassStudentList();
        $scope.getClassWaiverStudentList();
    };
    $scope.refreshClassStudents = function () {
        $scope.getClassStudentList();
        $scope.getClassWaiverStudentList();
    };
    $scope.getClassStudentList = function () {
        if ($scope.Class != null || $scope.Class.Id != "0") {

            $http.get($scope.getClassStudentUrl, {
                params: { "classId": $scope.Class.Id }
            }).success(function (result) {
                if (!result.HasError) {
                    $scope.ClassStudentList = result.Data;
                } else {

                    toastr.error("Please try again later to get Class Student Data! " + result.Errors.toString(), "Attention! ", { positionClass: "toast-top-center", timeOut: 10000, closeButton: true });
                }
            }).error(function (result) {

                toastr.error("Please try again later to get Class Student Data!", "Attention! ", { positionClass: "toast-top-center", timeOut: 10000, closeButton: true });
            });
        } else {
            toastr.error("Please Select or Create Class First!", "Attention! ", { positionClass: "toast-top-center", timeOut: 10000, closeButton: true });
        }
    };
    $scope.getClassWaiverStudentList = function () {
        if ($scope.Class != null || $scope.Class.Id != "0") {

            $http.get($scope.getClassWaiverStudentUrl, {
                params: { "classId": $scope.Class.Id }
            }).success(function (result) {
                if (!result.HasError) {
                    $scope.ClassWaiverStudentList = result.Data;

                } else {

                    toastr.error("Please try again later to get Class Waiver Student Data! " + result.Errors.toString(), "Attention! ", { positionClass: "toast-top-center", timeOut: 10000, closeButton: true });
                }
            }).error(function (result) {

                toastr.error("Please try again later to get Class Waiver Student Data!", "Attention! ", { positionClass: "toast-top-center", timeOut: 10000, closeButton: true });
            });
        } else {
            toastr.error("Please Select or Create Class First!", "Attention! ", { positionClass: "toast-top-center", timeOut: 10000, closeButton: true });
        }
    };
    $scope.changeSearchStudentsBySemesterLevelTerm = function () {
        $scope.ClasstList = [];
        $http.get($scope.getClassUrl, {
            params: {
                "departmentId": $scope.Class.DepartmentId
                , "semesterId": $scope.SearchStudentsBySemesterId
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.ClassList = result.Data;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Class list data! " + result.Errors.toString();
                //alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Class list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.searchStudents = function (enrollTypeEnumId) {
        $scope.ClassNewStudentList = [];
        $scope.getStudentList(enrollTypeEnumId);
    };
    //todo This is Search Student Modal
    //search items
    //$scope.SearchText = "";
    $scope.SearchStudentTotalItems = 0;
    $scope.SearchStudentTotalServerItems = 0;
    $scope.SearchStudentPageSize = 100;
    $scope.SearchStudentPageNo = 1;
    /*For Search on data start*/
    $scope.searchStudentChangePageSize = function () {
        $scope.SearchStudentPageNo = 1;
        $scope.getStudentList();
    };
    $scope.searchStudentChangePageNo = function () {
        $scope.getStudentList();
    };
    $scope.searchStudentPrev = function () {
        $scope.SearchStudentPageNo = $scope.SearchStudentPageNo - 1;
        $scope.getStudentList();
    };
    $scope.searchStudentNext = function () {
        $scope.SearchStudentPageNo = $scope.SearchStudentPageNo + 1;
        $scope.getStudentList();
    };

    $scope.getTrashUntrashCourseByClassId = function (obj, isDelete) {
        $http.get($scope.getTrashUntrashCourseByClassIdUrl, {
            params: {
                "stdId": obj.Student.Id
                , "classId": $scope.Class.Id
                , "isDelete": isDelete
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.getClassStudentList();
                alertSuccess("Successfully Student's Registration Cancel or UnCancelled!");
                $scope.ErrorMsg = "";

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to Cancel or UnCancelled Student's Registration! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }

        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to Cancel or UnCancelled Student's Registration! " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);

        });
    };


    $scope.getStudentList = function (enrollTypeEnumId) {

        if ($scope.Class != null || $scope.Class.Id != "0") {
            var classRoll = -1;
            if ($scope.SelectTypeId == 1) {
                if ($scope.SearchStudentsByClassId == null
                    || $scope.SearchStudentsByClassId <= 0) {
                    alertWarning("Please select Class to copy students.");
                }
            }
            if ($scope.SelectTypeId == 2) {
                if ($scope.SearchClassRoll != null) {
                    classRoll = $scope.SearchClassRoll;
                }
            }
            $http.get($scope.getStudentUrl, {
                params: {
                    "pageSize": $scope.SearchStudentPageSize
                    , "pageNo": $scope.SearchStudentPageNo
                    , "deptId": $scope.Class.DepartmentId
                    , "studyLevelTermId": $scope.SearchStudentsByStudyLevelTermId
                    , "classId": $scope.Class.Id
                    , "selectTypeId": $scope.SelectTypeId
                    , "searchClassId": $scope.SearchStudentsByClassId
                    , "enrollTypeEnumId": enrollTypeEnumId
                    , "searchClassRoll": classRoll
                    , "campusId": $scope.SearchStudentCampusId
                }
            }).success(function (result) {
                if (!result.HasError) {
                    $scope.StudentList = result.Data;


                    $scope.SearchStudentTotalItems = result.Count;
                    $scope.SearchStudentTotalServerItems = result.Data.length;

                } else {

                    toastr.error("Please try again later to get Student Data! " + result.Errors.toString(), "Attention! ", { positionClass: "toast-top-center", timeOut: 10000, closeButton: true });
                }
            }).error(function (result) {

                toastr.error("Please try again later to get Student Data!", "Attention! ", { positionClass: "toast-top-center", timeOut: 10000, closeButton: true });
            });
        } else {
            toastr.error("Please Select or Create Class First!", "Attention! ", { positionClass: "toast-top-center", timeOut: 10000, closeButton: true });
        }
    };
    $scope.searchWaiverStudents = function () {
        $scope.ClassNewWaiverStudentList = [];
        $scope.getWaiverStudentList();
    };
    $scope.getWaiverStudentList = function () {
        if ($scope.Class != null || $scope.Class.Id != "0") {

            $http.get($scope.getStudentUrl, {
                params: {
                    "deptId": $scope.Class.DepartmentId
                    , "studyLevelTermId": $scope.SearchStudentsByStudyLevelTermId
                    , "classId": $scope.Class.Id
                    , "campusId": $scope.SearchStudentCampusId
                }
            }).success(function (result) {
                if (!result.HasError) {
                    $scope.StudentList = result.Data;

                } else {

                    toastr.error("Please try again later to get Waiver Students Data! " + result.Errors.toString(), "Attention! ", { positionClass: "toast-top-center", timeOut: 10000, closeButton: true });
                }
            }).error(function (result) {

                toastr.error("Please try again later to get Waiver Students Data!", "Attention! ", { positionClass: "toast-top-center", timeOut: 10000, closeButton: true });
            });
        } else {
            toastr.error("Please Select or Create Class First!", "Attention! ", { positionClass: "toast-top-center", timeOut: 10000, closeButton: true });
        }
    };
    $scope.selectAllStudents = function () {
        $scope.ClassNewStudentList = [];
        for (var i = 0; i < $scope.StudentList.length; i++) {
            if ($scope.SelectAllStudents == true && $scope.StudentList[i].IsAlreadyAdded == false) {
                $scope.StudentList[i].IsSelected = true;
                $scope.ClassNewStudentList.push($scope.StudentList[i]);
            } else {
                $scope.StudentList[i].IsSelected = false;
                $scope.ClassNewStudentList.splice(i, 1);
            }
        }
    };
    $scope.selectStudent = function (id) {
        var obj = $filter('filter')($scope.StudentList, { Id: id }, true)[0];
        var index = $scope.StudentList.indexOf(obj);
        if ($scope.StudentList[index].IsSelected) {
            $scope.ClassNewStudentList.push($scope.StudentList[index]);
        } else {
            for (var i = 0; i < $scope.ClassNewStudentList.length; i++) {
                if ($scope.ClassNewStudentList[i].Id == $scope.StudentList[index].Id) {
                    $scope.ClassNewStudentList.splice(i, 1);
                }
            }
        }
    };
    $scope.selectAllWaiverStudents = function () {
        $scope.ClassNewWaiverStudentList = [];
        for (var i = 0; i < $scope.StudentList.length; i++) {
            if ($scope.SelectAllWaiverStudents == true && $scope.StudentList[i].IsAlreadyAdded == false) {
                $scope.StudentList[i].IsSelected = true;
                $scope.ClassNewWaiverStudentList.push($scope.StudentList[i]);
            } else {
                $scope.StudentList[i].IsSelected = false;
                $scope.ClassNewWaiverStudentList.splice(i, 1);
            }
        }
    };
    $scope.selectWaiverStudent = function (id) {
        var obj = $filter('filter')($scope.StudentList, { Id: id }, true)[0];
        var index = $scope.StudentList.indexOf(obj);
        if ($scope.StudentList[index].IsSelected) {
            $scope.ClassNewWaiverStudentList.push($scope.StudentList[index]);
        } else {
            for (var i = 0; i < $scope.ClassNewWaiverStudentList.length; i++) {
                if ($scope.ClassNewWaiverStudentList[i].Id == $scope.StudentList[index].Id) {
                    $scope.ClassNewWaiverStudentList.splice(i, 1);
                }
            }
        }
    };
    $scope.addClassStudents = function (enrollTypeEnumId, registrationstatusEnumId, statusEnumId) {
        var msg = "";
        if ($scope.ClassNewStudentList == null || $scope.ClassNewStudentList.length == 0) {
            msg += "Please Add Students in Class. ";
        }
        if ($scope.Class == null || $scope.ClassNewStudentList.length > $scope.Class.RegularCapacity) {
            msg += "Invalid Class or Regular Capacity Overflowed.";
        }
        if (msg === "") {
            $scope.ClassStudentsJson.User_StudentJsonList = $scope.ClassNewStudentList;
            $scope.ClassStudentsJson.ClassId = $scope.Class.Id;
            $scope.ClassStudentsJson.enrollTypeEnumId = enrollTypeEnumId;
            $scope.ClassStudentsJson.registrationstatusEnumId = registrationstatusEnumId;
            $scope.ClassStudentsJson.statusEnumId = statusEnumId;
            $http.post($scope.saveClassStudentUrl + "/", $scope.ClassStudentsJson
            ).success(function (result, status) {
                if (result.HasError) {
                    $scope.error = true;
                    msg += result.Errors.toString();
                    toastr.error(msg, 'Attention! ', { positionClass: 'toast-top-center', timeOut: 10000, closeButton: true });
                } else {
                    msg += "Successfully Saved!";
                    toastr.success(msg, 'Success', { positionClass: 'toast-top-center', timeOut: 10000, closeButton: true });
                    $scope.refreshClassStudents();
                    $scope.ClassNewStudentList = [];
                    $scope.getStudentList(enrollTypeEnumId);
                    //$scope.ClassNewStudentList = result.Data;
                }

            }).error(function (result, status) {
                $scope.error = true;
                msg += "Please try again later to save!!!" + result.Errors;
                toastr.error(msg, 'Attention! ', { positionClass: 'toast-top-center', timeOut: 10000, closeButton: true });

            });
        } else {
            msg += "Please provide all the required information! ";
            toastr.warning(msg, 'Warning', { positionClass: 'toast-top-center', timeOut: 10000, closeButton: true });
        }
    };
    $scope.addClassWaiverStudents = function () {
        var msg = "";
        if ($scope.ClassNewWaiverStudentList == null || $scope.ClassNewWaiverStudentList.length == 0) {
            msg += "Please Add Students in Class. ";
        }
        if ($scope.Class == null || $scope.ClassNewWaiverStudentList.length > $scope.Class.RegularCapacity) {
            msg += "Invalid Class or Regular Capacity Overflowed.";
        }
        if (msg === "") {
            $scope.ClassStudentsJson.User_StudentJsonList = $scope.ClassNewWaiverStudentList;
            $scope.ClassStudentsJson.ClassId = $scope.Class.Id;
            $http.post($scope.saveClassWaiverStudentUrl + "/", $scope.ClassStudentsJson
            ).success(function (result, status) {
                if (result.HasError) {
                    $scope.error = true;
                    msg += result.Errors.toString();
                    toastr.error(msg, 'Attention! ', { positionClass: 'toast-top-center', timeOut: 10000, closeButton: true });
                } else {
                    msg += "Successfully Saved!";
                    toastr.success(msg, 'Success', { positionClass: 'toast-top-center', timeOut: 10000, closeButton: true });
                    $scope.refreshClassStudents();
                    $scope.ClassNewStudentList = [];
                    $scope.getStudentList();
                    //$scope.ClassNewStudentList = result.Data;
                }

            }).error(function (result, status) {
                $scope.error = true;
                msg += "Please try again later to save!!!" + result.Errors;
                toastr.error(msg, 'Attention! ', { positionClass: 'toast-top-center', timeOut: 10000, closeButton: true });

            });
        } else {
            msg += "Please provide all the required information! ";
            toastr.warning(msg, 'Warning', { positionClass: 'toast-top-center', timeOut: 10000, closeButton: true });
        }
    };
    $scope.saveBulkClassWaiverStudents = function () {
        var msg = "";
        if ($scope.ClassWaiverStudentList == null || $scope.ClassWaiverStudentList.length == 0) {
            msg += "Please Add Students in Class. ";
        }
        if (msg === "") {
            $http.post($scope.saveBulkClassWaiverStudentUrl + "/", $scope.ClassWaiverStudentList
            ).success(function (result, status) {
                if (result.HasError) {
                    $scope.error = true;
                    msg += result.Errors.toString();
                    toastr.error(msg, 'Attention! ', { positionClass: 'toast-top-center', timeOut: 10000, closeButton: true });
                } else {
                    msg += "Successfully Saved Waiver Students!";
                    toastr.success(msg, 'Success', { positionClass: 'toast-top-center', timeOut: 10000, closeButton: true });
                    $scope.getClassWaiverStudentList();
                }

            }).error(function (result, status) {
                $scope.error = true;
                msg += "Please try again later to save!!!" + result.Errors;
                toastr.error(msg, 'Attention! ', { positionClass: 'toast-top-center', timeOut: 10000, closeButton: true });

            });
        } else {
            msg += "Please provide all the required information! ";
            toastr.warning(msg, 'Warning', { positionClass: 'toast-top-center', timeOut: 10000, closeButton: true });
        }
    };
    $scope.deleteClassStudent = function (obj) {
        var msg = "<span  class='text-danger'><i class='fa fa-question-circle'></i> " +
            "Are you sure want to permanently delete?" +
            "<span ><br>" +
            "<span  class='text-info'><i class='fa fa-info-circle'></i> " +
            "Please be careful that you will not get those data in future!" +
            "<span >";
        bootbox.confirm(msg, function (ok) {
            if (ok) {
                msg = "";

                obj.UserId = $scope.Class.Id;
                $http.post($scope.deleteClassStudentUrl + "/", obj
                    ).success(function (result) {
                        if (!result.HasError) {
                            $scope.HasError = false;
                            var i = $scope.ClassStudentList.indexOf(obj);
                            $scope.ClassStudentList.splice(i, 1);
                            alertSuccess("Successfully Removed Student from this Class!");

                        } else {
                            alertWarning("Student can't remove from this Class !! ");
                        }
                    }).error(function (result) {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Unable to Delete! " + result.Errors.toString();
                        alertError($scope.ErrorMsg);
                    });
            } else {
                alertInfo("Thank You for your decision.");
            }
        });
    };

    $scope.getDropUnDropCourseByClassId = function (obj,isDrop) {

        $http.get($scope.getDropUnDropCourseByClassIdUrl, {
            params: {
                "stdId": obj.Student.Id
                , "classId": $scope.Class.Id
                , "isDrop": isDrop
            }
        }).success(function (result) {
            if (!result.HasError) {
                
                $scope.getClassStudentList();
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


    $scope.deleteClassAllStudent = function (enrollTypeEnumId) {
        var msg = "<span  class='text-danger'><i class='fa fa-question-circle'></i> " +
            "Are you sure want to permanently delete?" +
            "<span ><br>" +
            "<span  class='text-info'><i class='fa fa-info-circle'></i> " +
            "Please be careful that you will not get those data in future!" +
            "<span >";
        bootbox.confirm(msg, function (ok) {
            if (ok) {
                msg = "";

                $scope.List = $filter('filter')($scope.ClassStudentList, { EnrollTypeEnumId: enrollTypeEnumId }, true);
                $http.post($scope.deleteClassAllStudentUrl + "/", $scope.List
                    ).success(function (result) {
                        if (!result.HasError) {
                            $scope.HasError = false;
                            for (x = 0; x < $scope.List.length; x++) {
                                var i = $scope.ClassStudentList.indexOf($scope.List[x]);
                                $scope.ClassStudentList.splice(i, 1);
                            }
                            alertSuccess("Successfully Removed Students from this Class!");
                        } else {
                            alertWarning("Students can't remove from this Class !! ");
                        }
                    }).error(function (result) {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Unable to Delete! " + result.Errors.toString();
                        alertError($scope.ErrorMsg);
                    });
            } else {
                alertInfo("Thank You for your decision.");
            }
        });
    };
    $scope.deleteClassWaiverStudent = function (obj) {
        var msg = "<span  class='text-danger'><i class='fa fa-question-circle'></i> " +
            "Are you sure want to permanently delete?" +
            "<span ><br>" +
            "<span  class='text-info'><i class='fa fa-info-circle'></i> " +
            "Please be careful that you will not get those data in future!" +
            "<span >";
        bootbox.confirm(msg, function (ok) {
            if (ok) {
                msg = "";

                obj.UserId = $scope.Class.Id;
                $http.post($scope.deleteClassWaiverStudentUrl + "/", obj
                    ).success(function (result) {
                        if (!result.HasError) {
                            $scope.HasError = false;
                            var i = $scope.ClassWaiverStudentList.indexOf(obj);
                            $scope.ClassWaiverStudentList.splice(i, 1);
                            alertSuccess("Successfully Removed Student from this Class!");

                        } else {
                            alertWarning("Student can't remove from this Class !! ");
                        }
                    }).error(function (result) {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Unable to Delete! " + result.Errors.toString();
                        alertError($scope.ErrorMsg);
                    });
            } else {
                alertInfo("Thank You for your decision.");
            }
        });
    };
    /****Class Teachers****/
    $scope.getClassTeachers = function () {
        $scope.getClassTeacherList();
    };
    $scope.refreshClassTeachers = function () {
        $scope.getClassTeacherList();
    };
    $scope.getClassTeacherList = function () {
        if ($scope.Class != null || $scope.Class.Id != "0") {

            $http.get($scope.getClassTeacherUrl, {
                params: {
                    "classId": $scope.Class.Id
                    ,"isShowTrashedFaculty": $scope.isShowTrashedFaculty
                }
            }).success(function (result) {
                if (!result.HasError) {
                    $scope.ClassTeacherList = result.Data;

                } else {

                    toastr.error("Please try again later to get Class Teacher Data! " + result.Errors.toString(), "Attention! ", { positionClass: "toast-top-center", timeOut: 10000, closeButton: true });
                }
            }).error(function (result) {

                toastr.error("Please try again later to get Class Teacher Data!", "Attention! ", { positionClass: "toast-top-center", timeOut: 10000, closeButton: true });
            });
        } else {
            toastr.error("Please Select or Create Class First!", "Attention! ", { positionClass: "toast-top-center", timeOut: 10000, closeButton: true });
        }
    };
    $scope.searchTeachers = function (roleEnumId) {
        $scope.ClassNewTeacherList = [];
        $scope.getTeacherList(roleEnumId);
    };
    $scope.getTeacherList = function (roleEnumId) {
        if ($scope.Class != null || $scope.Class.Id != "0") {
            $http.get($scope.getTeacherUrl, {
                params: {
                    "deptId": $scope.SearchTeachersByDepartmentId
                    , "classId": $scope.Class.Id //roll type need to attach
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
            toastr.error("Please Select or Create Class First!", "Attention! ", { positionClass: "toast-top-center", timeOut: 10000, closeButton: true });
        }
    };
    //for problem its cant use
    //$scope.selectAllTeachers = function () {
    //    $scope.ClassNewTeacherList = [];
    //    for (var i = 0; i < $scope.TeacherList.length; i++) {
    //        if ($scope.SelectAllTeachers == true && $scope.TeacherList[i].IsAlreadyAdded == false) {
    //            $scope.TeacherList[i].IsSelected = true;
    //            $scope.ClassNewTeacherList.push($scope.TeacherList[i]);
    //        } else {
    //            $scope.TeacherList[i].IsSelected = false;
    //            $scope.ClassNewTeacherList.splice(i, 1);
    //        }
    //    }
    //};
    $scope.selectTeachertmp = function ($index) {
        if ($scope.TeacherList[$index].IsSelected == true) {
            $scope.ClassNewTeacherList.push($scope.TeacherList[$index]);
        } else {
            $scope.ClassNewTeacherList.splice($index, 1);
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
    $scope.addClassTeachers = function (roleEnumId, sectionEnumId, statusEnumId) {
        var msg = "";
        if ($scope.ClassNewTeacherList == null || $scope.ClassNewTeacherList.length == 0) {
            msg += "Please Add Teachers in Class. ";
        }
        if (msg === "") {
            $scope.ClassEmployeesJson.User_EmployeeJsonList = $scope.ClassNewTeacherList;
            $scope.ClassEmployeesJson.ClassId = $scope.Class.Id;
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
                    $scope.refreshClassTeachers();
                    $scope.getTeacherList(roleEnumId);
                    //$scope.ClassNewTeacherList = result.Data;
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
    $scope.deleteClassTeacher = function (obj) {
        var msg = "<span  class='text-danger'><i class='fa fa-question-circle'></i> " +
            "Are you sure want to permanently delete?" +
            "<span ><br>" +
            "<span  class='text-info'><i class='fa fa-info-circle'></i> " +
            "Please be careful that you will not get those data in future!" +
            "<span >";
        bootbox.confirm(msg, function (ok) {
            if (ok) {
                msg = "";

                obj.UserId = $scope.Class.Id;
                $http.post($scope.deleteClassTeacherUrl + "/", obj
                    ).success(function (result) {
                        if (!result.HasError) {
                            var i = $scope.ClassTeacherList.indexOf(obj);
                            $scope.ClassTeacherList.splice(i, 1);
                            msg += "Successfully Removed Teacher from this Class!";
                            toastr.success(msg, 'Success', { positionClass: 'toast-top-center', timeOut: 10000, closeButton: true });

                        } else {

                            toastr.error("Teacher can't remove from this Class !! " + result.Errors.toString(), "Attention! ", { positionClass: "toast-top-center", timeOut: 10000, closeButton: true });
                        }
                    }).error(function (result) {

                        toastr.error("Please try again later to get delete Class Teacher Data!", "Attention! ", { positionClass: "toast-top-center", timeOut: 10000, closeButton: true });
                    });
            } else {
                msg = "Thank You for your decision.";
                toastr.success(msg, 'Operation Recovered!', { positionClass: 'toast-top-center', timeOut: 10000, closeButton: true });
            }
        });
    };

    $scope.TrashClassTeacher = function (classTakenByEmployee) {
        var msg = "<span  class='text-danger'><i class='fa fa-question-circle'></i> " +
            "Are you sure want to delete?" +
            "<span >";
        bootbox.confirm(msg, function (ok) {
            if (ok) {
                msg = "";
                $http.get($scope.trashOrUnTrashTeacherByIdUrl, {
                    params: {
                        "classTakenByEmployeeId": classTakenByEmployee.Id
                        , "isTrash": true
                    }
                }).success(function (result) {
                        if (!result.HasError) {
                            var i = $scope.ClassTeacherList.indexOf(classTakenByEmployee);
                            $scope.ClassTeacherList.splice(i, 1);
                            msg += "Successfully Removed Teacher from this Class!";
                            toastr.success(msg, 'Success', { positionClass: 'toast-top-center', timeOut: 10000, closeButton: true });

                        } else {

                            toastr.error("Teacher can't remove from this Class !! " + result.Errors.toString(), "Attention! ", { positionClass: "toast-top-center", timeOut: 10000, closeButton: true });
                        }
                    }).error(function (result) {

                        toastr.error("Please try again later to get delete Class Teacher Data!", "Attention! ", { positionClass: "toast-top-center", timeOut: 10000, closeButton: true });
                    });
            } else {
                msg = "Thank You for your decision.";
                toastr.success(msg, 'Operation Recovered!', { positionClass: 'toast-top-center', timeOut: 10000, closeButton: true });
            }
        });
    };
    $scope.UnTrashClassTeacher = function (classTakenByEmployee) {
        var msg = "<span  class='text-success'><i class='fa fa-question-circle'></i> " +
            "Are you sure want to Un-Delete?" +
            "<span >";
        bootbox.confirm(msg, function (ok) {
            if (ok) {
                msg = "";

                $http.get($scope.trashOrUnTrashTeacherByIdUrl, {
                    params: {
                        "classTakenByEmployeeId": classTakenByEmployee.Id
                        , "isTrash": false
                    }
                }).success(function (result) {
                    if (!result.HasError) {
                        var i = $scope.ClassTeacherList.indexOf(classTakenByEmployee);
                        $scope.ClassTeacherList.splice(i, 1);
                        msg += "Successfully Removed Teacher from this Class!";
                        toastr.success(msg, 'Success', { positionClass: 'toast-top-center', timeOut: 10000, closeButton: true });

                    } else {

                        toastr.error("Teacher can't remove from this Class !! " + result.Errors.toString(), "Attention! ", { positionClass: "toast-top-center", timeOut: 10000, closeButton: true });
                    }
                }).error(function (result) {

                    toastr.error("Please try again later to get delete Class Teacher Data!", "Attention! ", { positionClass: "toast-top-center", timeOut: 10000, closeButton: true });
                });
            } else {
                msg = "Thank You for your decision.";
                toastr.success(msg, 'Operation Recovered!', { positionClass: 'toast-top-center', timeOut: 10000, closeButton: true });
            }
        });
    };
    /*Save list*/
    $scope.saveAllClassStudents = function () {
        $http.post($scope.saveClassTakenByStudentListUrl + "/", $scope.ClassStudentList).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    if (result.Data != null) {
                        $scope.ClassStudentList = result.Data;
                        //updateUrlQuery('id', $scope.Class.Id);
                    }
                    alertSuccess("Successfully saved all class students!");
                    $scope.onAfterSaveClass(result);
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to save all class students! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to save all class students! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    }
    $scope.saveAllClassTeachers = function () {
        $http.post($scope.saveClassTakenByEmployeeListUrl + "/", $scope.ClassTeacherList).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    if (result.Data != null) {
                        $scope.ClassTeacherList = result.Data;
                        //updateUrlQuery('id', $scope.Class.Id);
                    }
                    alertSuccess("Successfully saved all class teachers!");
                    $scope.onAfterSaveClass(result);
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to save all class teachers! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to save all class teachers! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    }
    /***Class Routine***/
    $scope.searchClassRoutineList = function () {
        $scope.getPagedClassRoutineList();
    };
    $scope.getPagedClassRoutineList = function () {
        $scope.getClassRoutineList();
    };
    $scope.getClassRoutineList = function () {
        if ($scope.ClassId > 0) {
            $http.get($scope.getPagedClassRoutineUrl, {
                params: {
                    "textkey": $scope.SearchText
                , "pageSize": $scope.PageSize
                , "pageNo": $scope.PageNo
               , "classId": $scope.Class.Id
               , "roomId": $scope.SelectedRoomId
               , "employeeId": $scope.SelectedEmployeeId
                }
            }).success(function (result) {
                if (!result.HasError) {
                    $scope.ClassRoutineList = result.Data;
                    $scope.totalItems = result.Count;
                    $scope.totalServerItems = result.Data.length;
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to get Class Routine list data! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Class Routine list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
                alertError($scope.ErrorMsg);
            });
        }
    };
    $scope.getNewClassRoutine = function () {
        $scope.getClassRoutineById(0);
    };
    $scope.getClassRoutineById = function (ClassRoutineId) {
        if (ClassRoutineId != null && ClassRoutineId !== '')
            $scope.ClassRoutineId = ClassRoutineId;
        else return;

        $http.get($scope.getClassRoutineByIdUrl, {
            params: { "id": $scope.ClassRoutineId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.ClassRoutine = result.Data;
                $scope.ClassRoutine.ClassId = $scope.Class.Id;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Class Routine! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Class Routine! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.saveClassRoutine = function () {
        if (!$scope.validateClassRoutine()) {
            return;
        }
        $http.post($scope.saveClassRoutineUrl + "/", $scope.ClassRoutine).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    if (result.Data != null) {
                        $scope.ClassRoutine = result.Data;
                        $scope.getClassRoutineList();
                    }
                    alertSuccess("Successfully saved Class Routine data!");
                    $scope.onAfterSaveClassRoutine(result);
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to save Class Routine! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to save Class Routine! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    };

    $scope.deleteClassRoutineById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteClassRoutineByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.ClassRoutineList.indexOf(obj);
                        $scope.ClassRoutineList.splice(i, 1);
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
    $scope.validateClassRoutine = function () {
        return true;
    };

    $scope.addNewClassRoutine = function () {
        $scope.getClassRoutineById(0);
    };
    $scope.editClassRoutine = function (ClassRoutineId) {
        $scope.getClassRoutineById(ClassRoutineId);
    };

    //======Supporting Functions start================================================================ 
    $scope.SearchByTypeId = 0;
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 1000;
    $scope.PageNo = 1;
    $scope.searchCurriculumCourseList = function () {

        if ($scope.SelectedCurriculumId != null || $scope.SelectedCurriculumId > 0) {
            updateUrlQuery('CurriculumId', $scope.SelectedCurriculumId);
        }

        $http.get($scope.getPagedCurriculumCourseUrl, {
            params: {
                "textkey": $scope.SearchText
                , "pageSize": $scope.PageSize
                , "pageNo": $scope.PageNo
                , "curriculumId": $scope.SelectedCurriculumId
                , "studyLevelTermId": $scope.SelectedStudyLevelTermId
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.CurriculumCourseList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Base/Master Course list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Base/Master Course list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getCurriculumCourseById = function (curriculumCourseId) {
        if (curriculumCourseId != null && curriculumCourseId !== '')
            $scope.CurriculumCourseId = curriculumCourseId;
        else return;

        $http.get($scope.getCurriculumCourseByIdUrl, {
            params: { "id": $scope.CurriculumCourseId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.SelectedCurriculumCourse = result.Data;

                var curriculum = $filter('filter')($scope.CurriculumList, { Id: $scope.SelectedCurriculumCourse.CurriculumId }, true)[0];
                if (curriculum != null) {
                    $scope.FilterProgramList = $filter('filter')(
                        $scope.ProgramList, { DepartmentId: $scope.Class.DepartmentId }, true);
                    var program = $filter('filter')($scope.FilterProgramList, { Id: curriculum.ProgramId }, true)[0];
                    if (program != null) {
                        var j = $scope.FilterProgramList.indexOf(program);
                        $scope.SelectedProgramId = $scope.FilterProgramList[j].Id;
                    }

                    $scope.FilterCurriculumList = $filter('filter')(
                        $scope.CurriculumList
                        , { ProgramId: curriculum.ProgramId }, true
                    );
                    var k = $scope.FilterCurriculumList.indexOf(curriculum);
                    $scope.SelectedCurriculumId = $scope.FilterCurriculumList[k].Id;
                }
                var levelterm = $filter('filter')(
                    $scope.StudyLevelTermList,
                    { Id: $scope.SelectedCurriculumCourse.StudyLevelTermId }, true)[0];
                var lv = $scope.StudyLevelTermList.indexOf(levelterm);
                $scope.SelectedStudyLevelTermId = $scope.StudyLevelTermList[lv].Id;

                $scope.getMarkDistributionPolicyList();

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Curriculum Course! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Curriculum Course! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.SemeserDurationFilter = function (item) {
        if ($scope.SelectedSemester == null) {
            return true;
        } else {
            return item.SemesterDurationEnumId === $scope.SelectedSemester.SemesterDurationEnumId;

        }
    }

    $scope.changeSemester = function () {
        if ($scope.Class.SemesterId != null || $scope.Class.SemesterId > 0) {
            updateUrlQuery('SemesterId', $scope.Class.SemesterId);

            var semesterObj = $filter('filter')($scope.SemesterList, { Id: $scope.Class.SemesterId }, true)[0];

            if (semesterObj != null) {
                $scope.SelectedSemester = semesterObj;

                //var currProgramObj = $filter('filter')($scope.ProgramList, { Id: $scope.Class.ProgramId }, true)[0];

                ////selected Program will not change if the currently selected program is already of the same semester duration.
                //if (currProgramObj == null || currProgramObj.SemesterDurationEnumId !== $scope.SelectedSemester.SemesterDurationEnumId) {
                //    //selectng the first of that semester duration if currently selected durtion id is not matched.
                //    var programObj = $filter('filter')($scope.ProgramList, { SemesterDurationEnumId: semesterObj.SemesterDurationEnumId }, true);
                //    if (programObj != null && programObj.length > 0) {
                //        $scope.Class.ProgramId = programObj[0].Id;
                //    }
                //}

                //var currStudyLevelTermObj = $filter('filter')($scope.StudyLevelTermList, { Id: $scope.Class.StudyLevelTermId }, true)[0];

                ////selected Study term will not change if the currently selected program is already of the same semester duration.
                //if (currStudyLevelTermObj == null || currStudyLevelTermObj.SemesterDurationEnumId !== $scope.SelectedSemester.SemesterDurationEnumId) {

                //    var studyLevelTermObj = $filter('filter')($scope.StudyLevelTermList, { SemesterDurationEnumId: semesterObj.SemesterDurationEnumId }, true);
                //    if (studyLevelTermObj != null && studyLevelTermObj.length > 0) {
                //        $scope.Class.StudyLevelTermId = studyLevelTermObj[0].Id;
                //    }
                //}
            }


        }
    };
    $scope.changeLevelTerm = function () {
        if ($scope.Class.StudyLevelTermId != null || $scope.Class.StudyLevelTermId > 0) {
            updateUrlQuery('LevelTermId', $scope.Class.StudyLevelTermId);
        }
    };
    $scope.changeBatch = function () {
        if ($scope.Class.StudentBatchId != null || $scope.Class.StudentBatchId > 0) {
            updateUrlQuery('BatchId', $scope.Class.StudentBatchId);
        }
    };
    $scope.changeDepartment = function () {
        if ($scope.Class.DepartmentId != null || $scope.Class.DepartmentId > 0) {
            $scope.FilterProgramList = $filter('filter')(
                $scope.ProgramList, { DepartmentId: $scope.Class.DepartmentId }, true
            );
            if ($scope.Class.Id <= 0) {
                $scope.SelectedProgramId = $scope.FilterProgramList[0].Id;
                $scope.changeProgram();
            }
        }
    };
    $scope.changeProgram = function () {
        // DepartmentId Mapping
        //$scope.Class.Id>0 &&(

        if ($scope.Class.ProgramId != null || $scope.Class.ProgramId > 0) {
            $scope.FilterProgram = $filter('filter')($scope.ProgramList, { Id: $scope.Class.ProgramId }, true)[0];

            $scope.SelectedProgramId = $scope.FilterProgram.Id;
            $scope.Class.DepartmentId = $scope.FilterProgram.DepartmentId;

            updateUrlQuery('ProgramId', $scope.SelectedProgramId);
        }

        if ($scope.SelectedProgramId != null || $scope.SelectedProgramId > 0) {
            $scope.FilterCurriculumList = $filter('filter')($scope.CurriculumList, { ProgramId: $scope.SelectedProgramId }, true);
            //&& $scope.SelectedProgramId < 0
            if ($scope.FilterCurriculumList.length > 0) {
                var offeredCurriculum = $filter('filter')($scope.FilterCurriculumList, { IsOffering: true }, true);
                if (offeredCurriculum.length > 0) {
                    $scope.SelectedCurriculumId = offeredCurriculum[0].Id;
                }
                else {
                    $scope.SelectedCurriculumId = $scope.FilterCurriculumList[0].Id;
                }
            
            }
            
            $scope.searchCurriculumCourseList();
        }
    };
    $scope.changeSelectedCurriculumCourse = function (obj) {
        $scope.SelectedCurriculumCourse = obj;
        $scope.Class.CurriculumCourseId = obj.Id;
        $scope.Class.Code = obj.Code;
        $scope.Class.Name = obj.Name;
        $scope.generateName();

        $scope.getMarkDistributionPolicyList();

        $("#selectCurriculumCourseModal").modal('hide');
    };
    $scope.generateName = function () {
        if ($scope.SelectedCurriculumCourse != null) {
            var obj = $filter('filter')($scope.ClassSectionList, { Id: $scope.Class.ClassSectionId }, true)[0];
            $scope.Class.Name = $scope.SelectedCurriculumCourse.Name + " [" + obj.Name + "]";
        }
    };
    $scope.renderHtml = function (html_code) {
        return $sce.trustAsHtml(html_code);
    };

    $scope.openHistoryModal = function () {
        $scope.getClassHistoryById();
    }
    $scope.openClassTakenByEmployeeHistory = function (history) {
        if (history == null || history === '') {
            $scope.ClassTakenByEmployeeHistoryMsg = '<center style="color:red;font-weight: bold;">No History Available.</center>';
        } else {
            $scope.ClassTakenByEmployeeHistoryMsg = history;
        }

        $('#classTakenByTeacherHistoryModal').modal('show');
    }

    //======Custom property and Functions End========================================================== 
});