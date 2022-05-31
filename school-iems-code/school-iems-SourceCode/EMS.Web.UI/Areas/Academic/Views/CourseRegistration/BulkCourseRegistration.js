//File:Admin_Country List Anjular  Controller
emsApp.controller('BulkCourseRegistrationCtrl', function ($scope, $http, $filter, cfpLoadingBar, $sce) {
    $scope.RegistrationJson = [];
    $scope.CourseList = [];
    $scope.StudentList = [];

    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;

    $scope.TotalCredit = 0;

    $scope.SelectedProgram = [];
    $scope.SelectedContinuingBatchId = 0;
    $scope.SelectedStudyLevelTermId = 0;
    $scope.SelectedSemesterId = 0;
    $scope.SelectedCampusId = 0;
    $scope.SelectedClassSectionId = 0;
    $scope.SelectedSemesterDurationEnumId = 0;

    $scope.StudentLoadOnEnumId = 0;
    $scope.SelectedStudyLevelTermIdForStdLoad = 0;
    $scope.SelectedStudentBatchIdForStdLoad = 0;
    $scope.IsIncludeInvalidStudent = false;
    $scope.IsRetakeAbleCourseAvailable = false;
    $scope.IsIgnoreableCourseAvailable = false;
    $scope.IsBulkCourseDelete = false;
    $scope.IsBulkStudentDelete = false;
    $scope.AdmissionSemesterId = 0;
    //===================================


    $scope.loadPage = function () {
        $scope.getClassListExtraData();
        //$scope.getPagedClassList();

    };
    //search items
    //$scope.SearchText = "";
    //$scope.totalItems = 0;
    //$scope.totalServerItems = 0;
    //$scope.PageSize = 100;
    //$scope.PageNo = 1;
    /*For Search on data start*/
    //$scope.changePageSize = function () {
    //    $scope.PageNo = 1;
    //    $scope.getPagedClassList();
    //};
    //$scope.changePageNo = function () {
    //    $scope.getPagedClassList();
    //};
    //$scope.prev = function () {
    //    $scope.PageNo = $scope.PageNo - 1;
    //    $scope.getPagedClassList();
    //};
    //$scope.next = function () {
    //    $scope.PageNo = $scope.PageNo + 1;
    //    $scope.getPagedClassList();
    //};
    //$scope.searchClassList = function () {
    //    $scope.getPagedClassList();
    //};
    //$scope.getPagedClassList = function () {
    //    $scope.getClassList();
    //};
    /*For Search on data end*/

    $scope.clearRegistration = function ()
    {
        $scope.RegistrationJson = [];
        $scope.CourseList = [];
        $scope.StudentList = [];
        $scope.AddNewStdErrorMsg = "";
        $scope.AddNewStdHasError = false;
    }


   
    $scope.getClassListExtraData = function () {
        $http.get($scope.getBulkRegClassDataExtraUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;

                //$scope.BlankStudent = result.DataExtra.BlankStudent;
                //$scope.StudentList.push(angular.copy($scope.BlankStudent));
                //angular.copy
                //if (result.DataExtra.ProgramTypeEnumList != null)
                //    $scope.ProgramTypeEnumList = result.DataExtra.ProgramTypeEnumList;

                //if (result.DataExtra.StatusEnumList != null)
                //    $scope.StatusEnumList = result.DataExtra.StatusEnumList;
                //DropDown Option Tables
                if (result.DataExtra.SemesterList != null)
                    $scope.SemesterList = result.DataExtra.SemesterList;

                $scope.SelectedSemesterId= result.DataExtra.CurrentSemesterId;
                log($scope.SelectedSemesterId);

                if (result.DataExtra.ProgramList != null)
                    $scope.ProgramList = result.DataExtra.ProgramList;

                if (result.DataExtra.ClassSectionList != null)
                    $scope.ClassSectionList = result.DataExtra.ClassSectionList;

                //if (result.DataExtra.CurriculumCourseList != null)
                //    $scope.CurriculumCourseList = result.DataExtra.CurriculumCourseList;

                if (result.DataExtra.DeptBatchList != null)
                    $scope.DeptBatchList = result.DataExtra.DeptBatchList;

               

                //if (result.DataExtra.SemesterLevelTermList != null)
                //    $scope.SemesterLevelTermList = result.DataExtra.SemesterLevelTermList;

                if (result.DataExtra.StudyLevelTermList != null)
                    $scope.StudyLevelTermList = result.DataExtra.StudyLevelTermList;

                if (result.DataExtra.StudentLoadOnEnumList != null) {
                    $scope.StudentLoadOnEnumList = result.DataExtra.StudentLoadOnEnumList;
                }

                $scope.semesterFilter();
                //if (result.DataExtra.CampusList != null)
                //    $scope.CampusList = result.DataExtra.CampusList;

                //if (result.DataExtra.DepartmentList != null)
                //    $scope.DepartmentList = result.DataExtra.DepartmentList;

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

    $scope.getClassList = function () {
        $http.get($scope.getBulkRegClassListUrl, {
            params: {
                "semesterId": $scope.SelectedSemesterId
            , "programId": $scope.SelectedProgram.Id
            , "studyLevelTermId": $scope.SelectedStudyLevelTermId
            , "batchId": $scope.SelectedContinuingBatchId
           , "campusId": $scope.SelectedCampusId
           , "classSectionId": $scope.SelectedClassSectionId
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HasViewPermission = result.HasViewPermission;

                $scope.RegistrationJson = result.Data;
                $scope.CourseList = result.Data.CourseList;

                log($scope.CourseList);

                $scope.TotalCredit = result.DataExtra.TotalCredit ;
                //$scope.totalItems = result.Count;
                //$scope.totalServerItems = result.Data.length;
            } else {
                $scope.TotalCredit = 0;
                $scope.CourseList = [];
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Class list! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.TotalCredit = 0;
            $scope.CourseList = [];
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Class list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.removeClassById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to remove this course from registration?", function (yes) {
            if (yes) {
                var i = $scope.CourseList.indexOf(obj);
                $scope.$apply(function () {
                    $scope.CourseList.splice(i, 1);
                    //alertSuccess("Data successfully deleted!");
                });
            }
        });
    };
    $scope.removeStudent = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to remove this course from registration?", function (yes) {
            if (yes) {
                var i = $scope.StudentList.indexOf(obj);
                $scope.$apply(function () {
                    $scope.StudentList.splice(i, 1);
                    //alertSuccess("Data successfully deleted!");
                });
            }
        });
    };

    //======Student Panel Start=======================================================  
    // Api Calling Start
    $scope.AddNewStdErrorMsg = "";
    $scope.AddNewStdHasError = false;

    $scope.getStudentByUserName = function ($index, row) {
       
        if ($scope.UserName == null || $scope.UserName.length <= 3) {
            //$scope.UserName = '150305012';
            alertWarning("Invalid Student Id to search!");
            return;
        }
        $scope.RegistrationJson.TmpStudentId = $scope.UserName;
        $scope.RegistrationJson.CourseList = $scope.CourseList;

        $http.post($scope.getGetStudentByIdUrl + "/", $scope.RegistrationJson)
        //$http.get($scope.getGetStudentByIdUrl, {
        //    params: {
        //        //"studentId": $scope.UserName
        //        "registrationJson": $scope.RegistrationJson
        //    }})
            .success(function (result) {
            if (!result.HasError) {
                $scope.AddNewStdHasError = false;
                $scope.AddNewStdErrorMsg = "";

                var newStudent = result.Data;
                $scope.UserName = "";//newStudent.UserName;

                var existStudent = $filter('filter')($scope.StudentList, { StudentId: newStudent.StudentId }, true);

                if (existStudent.length<=0) {
                    $scope.StudentList.push(result.Data);
                    window.scrollTo(0, document.body.scrollHeight);
                } else {
                    $scope.AddNewStdHasError = true;
                    $scope.AddNewStdErrorMsg = newStudent.UserName + " Student Already Added In The List!";
                    alertWarning($scope.AddNewStdErrorMsg);
                }
          
            } else {
                $scope.AddNewStdHasError = true;
                $scope.Student = [];
                //updateUrlQuery('sid', $scope.SearchUserName);
                $scope.AddNewStdErrorMsg = result.Errors.toString() + " Unable to get Student!";
                alertError($scope.AddNewStdErrorMsg);
            }
        }).error(function (result, status) {
            $scope.Student = [];
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Student ! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.getStudentListByProgramIdAndLevelTermId = function () {
        $scope.StudentList = [];

        $http.get($scope.getStudentListByProgramIdAndLevelTermIdUrl, {
                params: {
                    "programId": $scope.SelectedProgram.Id
                    , "levelTermId": $scope.SelectedStudyLevelTermIdForStdLoad
                    , "batchId": $scope.SelectedStudentBatchIdForStdLoad
                    , "studentLoadOnEnumId": $scope.StudentLoadOnEnumId
                    , "isIncludeInvalidStudent":$scope.IsIncludeInvalidStudent
                    , "admissionSemesterId": $scope.AdmissionSemesterId
                }
            })
            .success(function (result) {
                if (!result.HasError) {
                    $scope.AddNewStdHasError = false;
                    $scope.AddNewStdErrorMsg = "";
                    var newStudentList = result.Data;

                    for (var i = 0; i < newStudentList.length; i++) {
                        var newStudent = newStudentList[i];
                        var existStudent = $filter('filter')($scope.StudentList, { StudentId: newStudent.StudentId }, true);

                        if (existStudent.length <= 0) {
                            $scope.StudentList.push(newStudent);
                            //window.scrollTo(0, document.body.scrollHeight);
                        }

                    }
                    $scope.IsRetakeAbleCourseAvailable = false;
                    $scope.IsIgnoreableCourseAvailable = false;

                } else {
                    $scope.AddNewStdHasError = true;
                    //$scope.Student = [];
                    //updateUrlQuery('sid', $scope.SearchUserName);
                    $scope.AddNewStdErrorMsg = result.Errors.toString() + " Unable to get Student!";
                    alertError($scope.AddNewStdErrorMsg);
                }
            }).error(function (result, status) {
                $scope.Student = [];
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Student ! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
                alertError($scope.ErrorMsg);
            });
    };

    $scope.RegisterStdErrorMsg = "";
    $scope.RegisterStdHasError = false;
    $scope.registerStudentList = function () {

        //if ($scope.UserName == null || $scope.UserName.length <= 5) {
        //    $scope.UserName = '150305012';
        //    //alertWarning("Invalid Student Id to search!");
        //    //return;
        //}
        //$scope.RegistrationJson.TmpStudentId = $scope.UserName;
        $scope.RegistrationJson.CourseList = $scope.CourseList;
        $scope.RegistrationJson.StudentList = $scope.StudentList;
        log($scope.StudentList);

        $http.post($scope.saveRegisterStudentListUrl + "/", $scope.RegistrationJson)
                .success(function (result) {
                if (!result.HasError) {
                    $scope.RegisterStdHasError = false;
                    $scope.RegisterStdErrorMsg = "";
                    $scope.RegistrationJson = result.Data;
                    //$scope.RegistrationJson.CourseList = $scope.CourseList;
                    $scope.StudentList = $scope.RegistrationJson.StudentList;
                    alertSuccess("All Registration Success!");


                } else {
                    //$scope.$apply(function () {
                    //    $scope.StudentList.splice(i, 1);
                    //    //alertSuccess("Data successfully deleted!");
                    //});
                    $scope.RegistrationJson = result.Data;
                    $scope.StudentList = $scope.RegistrationJson.StudentList;

                    //checking if any ignorable course available.
                    $scope.IsAnyRetakeableCourseAvailable();
                    $scope.IsAnyIgnorableCourseAvailable();

                    $scope.RegisterStdHasError = true;
                    $scope.RegisterStdErrorMsg = result.Errors.toString() + "All Course Registration Failed!";
                    alertError($scope.RegisterStdErrorMsg);
                }
                }).error(function (result, status) {
                    $scope.RegistrationJson = result.Data;
                $scope.StudentList = $scope.RegistrationJson.StudentList;
                $scope.RegisterStdHasError = true;
                $scope.RegisterStdErrorMsg = "All Course Registration Failed! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
                alertError($scope.RegisterStdErrorMsg);
            });
    };

    $scope.showLoadStudentFilterModal = function () {
        $('#StudentLoadFilterModal').modal('show');

    }


    $scope.validateStudentList = function () {

        //if ($scope.UserName == null || $scope.UserName.length <= 5) {
        //    $scope.UserName = '150305012';
        //    //alertWarning("Invalid Student Id to search!");
        //    //return;
        //}
        $scope.RegistrationJson.CourseList = $scope.CourseList;
        $scope.RegistrationJson.StudentList = $scope.StudentList;

        $http.post($scope.getGetValidateBeforeBulkRegisterStudentListUrl + "/", $scope.RegistrationJson)
                .success(function (result) {
                    if (!result.HasError) {

                        $scope.RegistrationJson = result.Data;


                    } else {

                        $scope.RegisterStdHasError = true;
                        $scope.RegisterStdErrorMsg = result.Errors.toString() + " Unable to get Student data!";
                        alertError($scope.RegisterStdErrorMsg);
                    }
                }).error(function (result, status) {

                    $scope.RegisterStdHasError = true;
                    $scope.RegisterStdErrorMsg = "Unable to get Student ! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
                    alertError($scope.RegisterStdErrorMsg);
                });
    };
    $scope.renderHtml = function (html_code) {
        return $sce.trustAsHtml(html_code);
    };

    $scope.semesterFilter = function () {
        $scope.SelectedSemester = $filter('filter')($scope.SemesterList, { Id: $scope.SelectedSemesterId }, true)[0];

        if ($scope.SelectedSemester != null) {
            $scope.SelectedSemesterDurationEnumId = $scope.SelectedSemester.SemesterDurationEnumId;
        }
    }

    //======Custom Variables goes hare=====


    $scope.Init = function (

        getBulkRegClassDataExtraUrl
, getBulkRegClassListUrl
, getGetStudentByIdUrl
, saveRegisterStudentListUrl
, getGetValidateBeforeBulkRegisterStudentListUrl
    , getStudentListByProgramIdAndLevelTermIdUrl
       ) {
        $scope.getBulkRegClassDataExtraUrl = getBulkRegClassDataExtraUrl;
        $scope.getBulkRegClassListUrl = getBulkRegClassListUrl;
        /*bind extra url if need*/
        $scope.getGetStudentByIdUrl = getGetStudentByIdUrl;
        $scope.saveRegisterStudentListUrl = saveRegisterStudentListUrl;
        $scope.getGetValidateBeforeBulkRegisterStudentListUrl = getGetValidateBeforeBulkRegisterStudentListUrl;
        $scope.getStudentListByProgramIdAndLevelTermIdUrl = getStudentListByProgramIdAndLevelTermIdUrl;


        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    $scope.IgnoreAll = function () {
        var ignored = 0;
        //student list.
        for (var i = 0; i < $scope.StudentList.length; i++) {
            //student's course list.
            for (var j = 0; j < $scope.StudentList[i].CourseMessageList.length; j++) {
                $scope.StudentList[i].CourseMessageList[j].IsIgnored = true;
                $scope.StudentList[i].CourseMessageList[j].IsSelectedAsRetakeAble = false;
                $scope.StudentList[i].CourseMessageList[j].IsSelectedAsImprovementAble = false;
                ignored++;
            }
        }
    }

    $scope.RetakeAll = function () {
        var retakeAble = 0;
        //student list.
        for (var i = 0; i < $scope.StudentList.length; i++) {
            //student's course list.
            for (var j = 0; j < $scope.StudentList[i].CourseMessageList.length; j++) {
                if ($scope.StudentList[i].CourseMessageList[j].IsRetakeAble) {
                    $scope.StudentList[i].CourseMessageList[j].IsIgnored = false;
                    $scope.StudentList[i].CourseMessageList[j].IsSelectedAsRetakeAble = true;
                    $scope.StudentList[i].CourseMessageList[j].IsSelectedAsImprovementAble = false;
                    retakeAble++;
                }
            }
        }
    }

    $scope.SelectAsIgnored = function (obj) {
        if (obj.IsIgnored) {
            obj.IsSelectedAsImprovementAble = false;
            obj.IsSelectedAsRetakeAble = false;
        }
    }
    $scope.SelectAsRetake = function (obj) {
        if (obj.IsSelectedAsRetakeAble) {
            obj.IsSelectedAsImprovementAble = false;
            obj.IsIgnored = false;
        }
    }
    $scope.SelectAsImprovement = function (obj) {
        if (obj.IsSelectedAsImprovementAble) {
            obj.IsIgnored = false;
            obj.IsSelectedAsRetakeAble = false;
        }
    }

    $scope.ImprovementAll = function () {
        var improvementAble = 0;
        //student list.
        for (var i = 0; i < $scope.StudentList.length; i++) {
            //student's course list.
            for (var j = 0; j < $scope.StudentList[i].CourseMessageList.length; j++) {
                if ($scope.StudentList[i].CourseMessageList[j].IsRetakeAble) {
                    $scope.StudentList[i].CourseMessageList[j].IsIgnored = false;
                    $scope.StudentList[i].CourseMessageList[j].IsSelectedAsRetakeAble = false;
                    $scope.StudentList[i].CourseMessageList[j].IsSelectedAsImprovementAble = true;
                    improvementAble++;
                }
            }
        }
    }

    $scope.IsAnyIgnorableCourseAvailable = function() {
        for (var i = 0; i < $scope.StudentList.length; i++) {
            //student's course list.
            for (var j = 0; j < $scope.StudentList[i].CourseMessageList.length; j++) {
                $scope.IsIgnoreableCourseAvailable = true;
                break;
            }
        }
    }

    $scope.IsAnyRetakeableCourseAvailable = function() {
        for (var i = 0; i < $scope.StudentList.length; i++) {
            //student's course list.
            for (var j = 0; j < $scope.StudentList[i].CourseMessageList.length; j++) {
                if ($scope.StudentList[i].CourseMessageList[j].IsRetakeAble) {
                    $scope.IsRetakeAbleCourseAvailable = true;
                    break;
                }
            }
        }
    }

    $scope.selectAllStudent = function ($event) {
        var checkbox = $event.target;
        for (var i = 0; i < $scope.StudentList.length; i++) {
            var entity = $scope.StudentList[i];
            entity.IsSelected = checkbox.checked;
        }
    };
    $scope.selectAllCourse = function ($event) {
        var checkbox = $event.target;
        for (var i = 0; i < $scope.CourseList.length; i++) {
            var entity = $scope.CourseList[i];
            entity.IsSelected = checkbox.checked;
        }
    };

    $scope.clearSelectedStudents = function () {
        var selectedStudents = $filter('filter')($scope.StudentList, { IsSelected: true }, true);
        for (var i = 0; i < selectedStudents.length; i++) {
            var index = $scope.StudentList.indexOf(selectedStudents[i]);
            $scope.StudentList.splice(index, 1);
        }
    }

    $scope.clearSelectedCourses = function () {
        var selectedCourses = $filter('filter')($scope.CourseList, { IsSelected: true }, true);
        for (var i = 0; i < selectedCourses.length; i++) {
            var index = $scope.CourseList.indexOf(selectedCourses[i]);
            $scope.CourseList.splice(index, 1);
        }
    }

    //======Custom property and Functions End========================================================== 

});



