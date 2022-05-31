
//File:Academic_Semester List Anjular  Controller
emsApp.controller('ClassResultManagerCtrl', function ($scope, $http, $filter, Upload, $timeout) {
    $scope.SemesterList = [];
    $scope.SelectedSemester = [];
    $scope.SelectedClass = [];

    $scope.ClassId = '0';

    $scope.ResultComponentSettingsList = [];
    $scope.ResultSettingSectionAB = [];

    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.Init = function (
            classId
           , getSemesterListByFacultyUrl
            , getResultSettingsByClassIdUrl
            , getGenerateClassResultByClassIdUrl
            , getDefaultMarkDistributionByClassIdUrl
            , uploadClassMaterialUrl
            , getPagedClassAttendanceSettingByClassIdUrl
            , getPagedClassNoticeUrl
            , getClassNoticeExtraDataUrl
            , saveClassNoticeUrl
            , getClassNoticeByIdUrl
            , deleteClassNoticeByIdUrl
            , getPagedClassMaterialUrl
            , deleteClassMaterialFileMapByIdUrl
            , deleteClassAttendanceSettingByIdUrl
            , getClassStudentListByClassIdUrl
        ) {
        $scope.ClassId = classId;
        $scope.getSemesterListByFacultyUrl = getSemesterListByFacultyUrl;
        $scope.getResultSettingsByClassIdUrl = getResultSettingsByClassIdUrl;
        $scope.getGenerateClassResultByClassIdUrl = getGenerateClassResultByClassIdUrl;
        $scope.getDefaultMarkDistributionByClassIdUrl = getDefaultMarkDistributionByClassIdUrl;
        $scope.uploadClassMaterialUrl = uploadClassMaterialUrl;
        $scope.getPagedClassAttendanceSettingByClassIdUrl = getPagedClassAttendanceSettingByClassIdUrl;
        //ClassNotice
        $scope.getPagedClassNoticeUrl = RootApiUrl + getPagedClassNoticeUrl;
        $scope.getClassNoticeExtraDataUrl = RootApiUrl + getClassNoticeExtraDataUrl;
        $scope.saveClassNoticeUrl = RootApiUrl + saveClassNoticeUrl;
        $scope.getClassNoticeByIdUrl = RootApiUrl + getClassNoticeByIdUrl;
        $scope.deleteClassNoticeByIdUrl = RootApiUrl + deleteClassNoticeByIdUrl;
        $scope.getPagedClassMaterialUrl = getPagedClassMaterialUrl;
        $scope.deleteClassMaterialFileMapByIdUrl = deleteClassMaterialFileMapByIdUrl;
        $scope.deleteClassAttendanceSettingByIdUrl = deleteClassAttendanceSettingByIdUrl;

        $scope.getClassStudentListByClassIdUrl = getClassStudentListByClassIdUrl;

        $scope.getSemesterListByFaculty();
    };

    $scope.loadPage = function () {
        $scope.getResultSettingsByClassId();
        $scope.loadAttendanceSetting();
        $scope.getClassMaterialList();
        $scope.loadClassNotices();
    };
    $scope.RefreshClass = function () {
        $scope.loadPage();

    };
    //#region Semester Class List
    $scope.getSemesterListByFaculty = function () {

        $http.get($scope.getSemesterListByFacultyUrl, {
            params: {}
        }).success(function (result) {
            if (!result.HasError) {
                $scope.SemesterList = result.Data;
                if ($scope.SemesterList != null && $scope.SemesterList.length > 0) {
                    if ($scope.ClassId === '0')
                    {
                        $scope.SelectedSemester = $scope.SemesterList[0];
                        updateUrlQuery('semesterId', $scope.SelectedSemester.Id);
                    }
                    //$scope.SelectedClass = [];
                    //updateUrlQuery('semesterId', $scope.SelectedSemester.Id);
                    if ($scope.SelectedSemester.Aca_ClassListJson != null && $scope.SelectedSemester.Aca_ClassListJson.length > 0) {
                        //selecting class on dropdown
                        //$scope.SelectedClass = $filter("filter")($scope.SelectedSemester.Aca_ClassListJson, { Id: $scope.ClassId }, true);
                        //if ($scope.SelectedClass != null)
                        //{ $scope.SelectedClass = $scope.SelectedClass[0]; }
                        //$scope.SelectedClass = $scope.SemesterList[0].Aca_ClassListJson[0];
                        //$scope.ClassId = $scope.SelectedClass.Id;
                        //updateUrlQuery('classId', $scope.SelectedClass.Id);
                        //$scope.loadPage();
                    }
                    //now load other data
                    $scope.loadPage();
                }

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Semester list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }

        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Semester list! " + "Status: " + status.toString() + ". " + JSON.stringify(result).toString();
            //console.log(result);
            alertError($scope.ErrorMsg);

        });
    };
    $scope.onChangeSemester = function () {
        if ($scope.SelectedSemester.Aca_ClassListJson != null && $scope.SelectedSemester.Aca_ClassListJson.length > 0) {
            updateUrlQuery('semesterId', $scope.SelectedSemester.Id);
            $scope.SelectedClass = [];
            $scope.ClassId = '0';
            //$scope.SelectedSemester.Aca_ClassListJson[0];
            //updateUrlQuery('semesterId', $scope.SelectedSemester.Id);
            //$scope.onChangeClass();
            //loadClass()
        }
    };
    $scope.onChangeClass = function (index) {
        //alertInfo($('.main-tabs>.nav-tabs>.active').text());
        // console.log(index);
        //console.log($scope.SelectedClass);
        if ($scope.SelectedClass != null) {
            $scope.ClassId = $scope.SelectedClass.Id;
            updateUrlQuery('classId', $scope.ClassId);
            $scope.loadPage();
           // alertInfo('');
        }
    };
    //#endregion Semester Class List

    //======Custom property and Functions Start=======================================================

    //#region Result Manager
    $scope.getResultSettingsByClassId = function () {
        // console.log($scope.SelectedClass);

        if ($scope.ClassId === '0')
        { return; }

        //for Faculty
        $http.get($scope.getResultSettingsByClassIdUrl, {
            params: { "id": $scope.ClassId }
        }).success(function (result) {
            if (!result.HasError) {

                $scope.IsUgTheory = result.DataExtra.IsUgTheory;
                $scope.Data = result;
                //auto select class/semester from dropdown
                if ($scope.SemesterList != null && $scope.SemesterList.length > 0) {
                    $scope.SelectedSemester = $filter("filter")($scope.SemesterList, { Id: $scope.Data.DataExtra.SemesterId }, true);;
                    if ($scope.SelectedSemester != null) {
                        $scope.SelectedSemester = $scope.SelectedSemester[0];
                        updateUrlQuery('semesterId', $scope.SelectedSemester.Id);
                        if ($scope.SelectedSemester.Aca_ClassListJson != null && $scope.SelectedSemester.Aca_ClassListJson.length > 0) {
                            $scope.SelectedClass = $filter("filter")($scope.SelectedSemester.Aca_ClassListJson, { Id: $scope.ClassId }, true);

                            if ($scope.SelectedClass != null) {
                                $scope.SelectedClass = $scope.SelectedClass[0];
                            }
                        }
                    }
                }

                $scope.OpenMidTermMarkInput = result.DataExtra.OpenMidTermMarkInput;
                $scope.OpenFinalTermMarkInput = result.DataExtra.OpenFinalTermMarkInput;


                //$scope.ResultComponentSettingsList = result.DataExtra.RegularFacultyComponent;
                //$scope.ResultComponentSettingSectionAB = result.DataExtra.RegularSectionAB;
                $scope.RegularClassSetting = result.DataExtra.RegularClassSetting;
                $scope.RegularFacultyComponent = result.DataExtra.RegularFacultyComponent;
                $scope.RegularSectionAB = result.DataExtra.RegularSectionAB;

                $scope.ReferredClassSetting = result.DataExtra.ReferredClassSetting;
                $scope.ReferredFacultyComponent = result.DataExtra.ReferredFacultyComponent;
                $scope.ReferredSectionAB = result.DataExtra.ReferredSectionAB;

                $scope.BacklogClassSetting = result.DataExtra.BacklogClassSetting;
                $scope.BacklogFacultyComponent = result.DataExtra.BacklogFacultyComponent;
                $scope.BacklogSectionAB = result.DataExtra.BacklogSectionAB;
                //temp
                $scope.getClassStudentListByClassId();
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Semester list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }

        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Semester list! " + "Status: " + status.toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);

        });

    };


    $scope.getDefaultMarkDistributionByClassId = function () {
        if ($scope.ClassId === '0')
        { return; }
        $http.get($scope.getDefaultMarkDistributionByClassIdUrl, {
            params: { "id": $scope.ClassId }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.getResultSettingsByClassId();
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "" + result.Errors.toString();
                alertWarning($scope.ErrorMsg);
            }

        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to Default Mark Distribution! " + "Status: " + status.toString() + ". " + JSON.stringify(result).toString();
            //console.log(result);
            alertError($scope.ErrorMsg);
        });

    };
    //#endregion Result Manager

    //#region Student List Manager

    $scope.getClassStudentListByClassId = function () {
        if ($scope.ClassId === '0')
        { return; }
        $http.get($scope.getClassStudentListByClassIdUrl, {
            params: { "classId": $scope.ClassId }//, "examType": $scope.ExamType
        }).success(function (result) {
            if (!result.HasError) {
                $scope.StudentList = result.Data;
                log($scope.StudentList);
                // alertSuccess("All Marks reloaded from server.");
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get student list! " + result.Errors.toString();
                //alertError($scope.ErrorMsg);
            }

        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get student list! " + "Status: " + status.toString() + ". " + result.Message.toString();
            //alertError($scope.ErrorMsg);

        });
    };
    //#endregion Student List Manager

    //#region Class Material
    $scope.ClassMaterialList = [];
    $scope.SelectedClassMaterialFileMap = [];
    $scope.ClassMaterialErrorMsg = "";
    $scope.ClassMaterialHasError = false;
    //$scope.SelectedClassId = 0;
    //$scope.SelectedFileId = 0;
    //$scope.SelectedUserId = 0;
    //search items
    $scope.ClassMaterialSearchText = "";
    $scope.ClassMaterialTotalItems = 0;
    $scope.ClassMaterialTotalServerItems = 0;
    $scope.ClassMaterialPageSize = 100;
    $scope.ClassMaterialPageNo = 1;
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.ClassMaterialPageNo = 1;
        $scope.getPagedClassMaterialFileMapList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedClassMaterialFileMapList();
    };
    $scope.prev = function () {
        $scope.ClassMaterialPageNo = $scope.ClassMaterialPageNo - 1;
        $scope.getPagedClassMaterialFileMapList();
    };
    $scope.next = function () {
        $scope.ClassMaterialPageNo = $scope.ClassMaterialPageNo + 1;
        $scope.getPagedClassMaterialFileMapList();
    };

    $scope.getPagedClassMaterialFileMapList = function () {
        $scope.getClassMaterialList();
    };
    /*For Search on data end*/
    $scope.getClassMaterialList = function () {
        if ($scope.ClassId === '0')
        { return; }
        $http.get($scope.getPagedClassMaterialUrl, {
            params: {
                "textkey": $scope.ClassMaterialSearchText
                , "pageSize": $scope.ClassMaterialPageSize
                , "pageNo": $scope.ClassMaterialPageNo
                , "classId": $scope.ClassId
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.ClassMaterialList = result.Data;
                $scope.ClassMaterialTotalItems = result.Count;
                $scope.ClassMaterialTotalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Class Material! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Class Material! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.uploadClassMaterial = function () {
        if ($scope.ClassId === '0')
        { return; }
        //alert($scope.ExamId);
        if ($scope.myFiles == null || $scope.myFiles.length <= 0) {
            alertError("Please select valid files to upload!");
            return;
        }
        bootbox.confirm("Are you sure, you want to upload Class Materials for this class?", function (yes) {
            if (yes) {
                Upload.upload({
                    url: $scope.uploadClassMaterialUrl,
                    data: { file: $scope.myFiles, classId: $scope.ClassId }
                }).then(function (response) {
                    $timeout(function () {
                        $scope.result = response.data;
                        if (!$scope.result.HasError) {
                            //$scope.Obj.ApplicantPhotoUrl = $scope.result.DataExtra.UploadPath;
                            $scope.myFiles = [];
                            alertSuccess("Class Materials upload successfully! ");//+ $scope.result.DataExtra.Message.replace("\n", "<br>")
                            $scope.getClassMaterialList();
                        } else {
                            $scope.HasError = true;
                            $scope.ErrorMsg = "Class Materials upload failed! " + $scope.result.Errors.toString();
                            alertError($scope.ErrorMsg);
                            //$scope.myFiles = [];
                        }
                    });
                }, function (response) {
                    if (response.status > 0) {
                        $scope.errorMsg = response.status + ': ' + response.data;
                        $scope.ErrorMsg = "Class Materials upload failed! " + JSON.stringify(response.data).toString() + ". " + "Status: " + JSON.stringify(response.status).toString();
                        alertError($scope.ErrorMsg);
                    }
                }, function (evt) {
                    $scope.progress = parseInt(100.0 * evt.loaded / evt.total);
                });
            }
        });
    }
    $scope.deleteClassMaterialById = function (obj) {
        if (obj == null) {
            alertError("Please select a Course Material to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this Course Material?", function (yes) {
            if (yes) {
                $http.get($scope.deleteClassMaterialFileMapByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.ClassMaterialHasError = false;
                        var i = $scope.ClassMaterialList.indexOf(obj);
                        $scope.ClassMaterialList.splice(i, 1);
                        alertSuccess("Course Material successfully deleted!");
                    } else {
                        $scope.ClassMaterialHasError = true;
                        $scope.ClassMaterialErrorMsg = "Sorry unable to delete Course Material! " + result.Errors.toString();
                        alertError($scope.ClassMaterialErrorMsg);
                    }
                }).error(function (result, status) {
                    $scope.ClassMaterialHasError = true;
                    $scope.ClassMaterialErrorMsg = "Sorry unable to delete Course Material! " + "Status: " + JSON.stringify(status) + ". " + JSON.stringify(result);
                    alertError($scope.ClassMaterialErrorMsg);
                });
            }
        });
    };
    $scope.removeFromWaitList = function (file) {
        if (file == null) {
            return;
        }
        var i = $scope.myFiles.indexOf(file);
        $scope.myFiles.splice(i, 1);
    };

    //#endregion Class Material

    //#region Notice Manager
    $scope.ClassNotice = [];
    $scope.ClassNoticeList = [];
    $scope.totalClassNoticeItems = 0;
    $scope.totalClassNoticeServerItems = 0;
    $scope.PageSizeClassNotice = 20;
    $scope.PageNoClassNotice = 1;
    $scope.SearchTextClassNotice = "";
    $scope.loadClassNotices = function () {
        $scope.getClassNoticeList();
        $scope.getNewClassNotice();
    };
    $scope.getNewClassNotice = function () {
        $scope.getClassNoticeById(0);
    };
    $scope.getClassNoticeById = function (classNoticeId) {
        if ($scope.ClassId === '0')
        { return; }

        if (classNoticeId != null && classNoticeId !== '')
            $scope.ClassNoticeId = classNoticeId;
        else return;

        $http.get($scope.getClassNoticeByIdUrl, {
            params: { "id": $scope.ClassNoticeId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.ClassNotice = result.Data;
                $scope.ClassNotice.ClassId = $scope.ClassId;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Class Notice! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Class Notice! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    /*Paging Option*/
    $scope.changePageSizeClassNotice = function () {
        $scope.PageNo = 1;
        $scope.searchClassNoticeList();
    };
    $scope.changePageNoClassNotice = function () {
        $scope.searchClassNoticeList();
    };
    /*Search*/
    $scope.searchClassNoticeList = function () {
        $scope.getPagedClassNoticeList();
    };
    $scope.getPagedClassNoticeList = function () {
        $scope.getClassNoticeList();
    };
    $scope.refreshClassNotice = function () {
        $scope.getClassNoticeList();
    };
    /*Get ClassNotice*/
    /*For Search on data end*/
    $scope.getClassNoticeList = function () {
        if ($scope.ClassId === '0')
        { return; }
        $http.get($scope.getPagedClassNoticeUrl, {
            params: {
                "textkey": $scope.SearchText
                , "pageSize": $scope.PageSize
                , "pageNo": $scope.PageNo
                , "classId": $scope.ClassId
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.ClassNoticeList = result.Data;
                $scope.editClassNotice($scope.selectedClassNoticeIndex);
                $scope.totalClassNoticeItems = result.Count;
                $scope.totalClassNoticeServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Notices! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Notices! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    /*Edit ClassNotice*/
    $scope.editClassNotice = function ($index) {
        $scope.ClassNotice = $scope.ClassNoticeList[$index];
        $scope.selectedClassNoticeIndex = $index;
    };
    /*Save ClassNotice*/
    $scope.saveClassNotice = function () {
        if ($scope.ClassId === '0')
        { return; }
        if (!$scope.validateClassNotice()) {
            return;
        }
        $scope.ClassNotice.ClassId = $scope.ClassId;
        $http.post($scope.saveClassNoticeUrl + "/", $scope.ClassNotice).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    if (result.Data != null) {
                        $scope.ClassNotice = result.Data;
                        $scope.refreshClassNotice();
                    }
                    alertSuccess("Successfully saved Notice!");
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to save Notice! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to save Notice! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    };
    $scope.validateClassNotice = function () {
        if ($scope.ClassNotice.Subject == null || $scope.ClassNotice.Subject === "") {
            alertError("Subject can't be blank. ");
            return false;
        }
        return true;
    };
    /*Delete ClassNotice*/
    $scope.deleteClassNoticeById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteClassNoticeByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.ClassNoticeList.indexOf(obj);
                        $scope.ClassNoticeList.splice(i, 1);
                        $scope.editClassNotice($scope.selectedClassNoticeIndex);
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
    //#endregion Notice Manager

    //#region Attendance Manager
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;
    $scope.loadAttendanceSetting = function () {
        //$scope.getClassAttendanceSettingListExtraData();
        $scope.getClassAttendanceSettingList();

    };
    $scope.getClassAttendanceSettingList = function () {
        if ($scope.ClassId === '0')
        { return; }
        $http.get($scope.getPagedClassAttendanceSettingByClassIdUrl, {
            params: {
                "textkey": $scope.SearchText
            , "pageSize": $scope.PageSize
            , "pageNo": $scope.PageNo
           , "classId": $scope.ClassId
           , "routineId": $scope.SelectedRoutineId
           , "employeeId": $scope.SelectedEmployeeId
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.ClassAttendanceSettingList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;

                if (result.DataExtra.ClassTypeEnumList != null)
                    $scope.ClassTypeEnumList = result.DataExtra.ClassTypeEnumList;
                //DropDown Option Tables
                if (result.DataExtra.ClassList != null)
                    $scope.ClassList = result.DataExtra.ClassList;

                if (result.DataExtra.ClassRoutineList != null)
                    $scope.ClassRoutineList = result.DataExtra.ClassRoutineList;

                if (result.DataExtra.EmployeeList != null)
                    $scope.EmployeeList = result.DataExtra.EmployeeList;

                $scope.SmsLogList = result.DataExtra.SmsLogList;


            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Class Attendance Setting list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Class Attendance Setting list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.deleteClassAttendanceSettingById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete all Attendance of this Period? This will delete all Attendance and SMS log of this Period.", function (yes) {
            if (yes) {
                $http.get($scope.deleteClassAttendanceSettingByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.ClassAttendanceSettingList.indexOf(obj);
                        $scope.ClassAttendanceSettingList.splice(i, 1);
                        alertSuccess("Attendance successfully deleted!");
                    } else {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Sorry unable to delete! " + result.Errors.toString();
                        alertError($scope.ErrorMsg);
                    }
                }).error(function (result, status) {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Sorry unable to delete! " + JSON.stringify(result).toString();
                    alertError($scope.ErrorMsg);
                });
            }
        });
    };
    //#endregion Attendance Manager

    //======Supporting Functions start================================================================ 

});
