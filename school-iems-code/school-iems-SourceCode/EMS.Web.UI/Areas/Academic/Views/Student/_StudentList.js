
//File:Admin_Student List Anjular  Controller
emsApp.controller('StudentListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.StudentList = [];
    $scope.SelectedStudent = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;

    //search items
    $scope.IsGraduated = false;
    $scope.SearchClassRoll = "";
    $scope.SearchText = "";
    $scope.SearchUserName = "";
    $scope.SearchUgcUniqueId = "";
    $scope.SearchByDepartmentId = -1;
    $scope.SearchByLevelTermId = -1;
    $scope.SearchByStatusId = -1;
    $scope.SearchByAdmissionStatusEnumId = -1;
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 30;
    $scope.PageNo = 1;
    $scope.StudentQuotaId = -1;
    $scope.StudentMobileNo = "";
    $scope.StudentEmail = "";
    $scope.PresentDistrict = "";
    $scope.BirthCertificateNo = "";
    $scope.NationalIdNo = "";
    $scope.PassportNo = "";
    $scope.PermanentDistrict = "";
    $scope.FatherName = "";
    $scope.ParentsJobTypeId = -1;
    $scope.MotherName = "";
    $scope.IsNeverLoggedIn = false;
    $scope.IsShowPassword = false;
    $scope.AdmissionStartDate = "";
    $scope.AdmissionEndDate = "";
    $scope.StudentsBirthDate = "";
    $scope.SearchByGenderEnumId = -1;
    $scope.SearchByEnrolmentEnumId = -1;
    $scope.IsIncludeContactAddress = false;

    $scope.loadPage = function () {

        $scope.getStudentListDataExtra();
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedStudentList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedStudentList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedStudentList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedStudentList();
    };
    $scope.searchStudentList = function () {
        $scope.getPagedStudentList();
    };
    $scope.getPagedStudentList = function () {
        $scope.getStudentList();
    };
    /*For Search on data end*/
    $scope.getStudentList = function () {
        log($scope.SearchByAdmissionSemesterId);
        $http.get($scope.getPagedStudentUrl, {
            params: {
            "classRoll": $scope.SearchClassRoll,
            "userName": $scope.SearchUserName, 
            "ugcUniqueId": $scope.SearchUgcUniqueId,
            "textkey": $scope.SearchText, 
            "deptId": $scope.SearchByDepartmentId,
            "programId": $scope.SearchByProgramId,
            "batchId": $scope.SearchByBatchId,
            "levelTermId": $scope.SearchByLevelTermId,
            "bloodId": $scope.SearchByBloodId,
           
            "genderId": $scope.SearchByGenderId,
            "admissionStatusEnumId": $scope.SearchByAdmissionStatusEnumId,
            "statusId": $scope.SearchByStatusId,
            "pageSize": $scope.PageSize, 
            "pageNo": $scope.PageNo,
            "isShowTrashedStudent": $scope.isShowTrashedStudent,
            "religionId": $scope.SearchByReligionId,
            "admissionSemesterId": $scope.SearchByAdmissionSemesterId,

            "studentQuotaId": $scope.StudentQuotaId,
            "studentMobileNo": $scope.StudentMobileNo,
            "studentEmail": $scope.StudentEmail,
            "presentDistrict": $scope.PresentDistrict,
            "birthCertificateNo": $scope.BirthCertificateNo,
            "nationalIdNo": $scope.NationalIdNo,
            "passportNo": $scope.PassportNo,
            "permanentDistrict": $scope.PermanentDistrict,
            "fatherName": $scope.FatherName,
            "parentsJobTypeId": $scope.ParentsJobTypeId,
            "motherName": $scope.MotherName,
            "isNeverLoggedIn": $scope.IsNeverLoggedIn,
            "isShowPassword": $scope.IsShowPassword,
            "admissionStartDate": $scope.AdmissionStartDate,
            "admissionEndDate": $scope.AdmissionEndDate,
            "searchByGenderEnumId": $scope.SearchByGenderEnumId,
            "searchByEnrolmentEnumId": $scope.SearchByEnrolmentEnumId,
            "studentsBirthDate": $scope.StudentsBirthDate,
            "isIncludeContactAddress": $scope.IsIncludeContactAddress
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.StudentList = result.Data;
                log(result.Data);
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Student list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Student list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getStudentListDataExtra = function () {
        
        $http.get($scope.getStudentListDataExtraUrl)
            .success(function (result) {
                if (!result.HasError) {
                    if (result.DataExtra.DepartmentList != null){
                        $scope.DepartmentList = result.DataExtra.DepartmentList;
                        //$scope.SearchByDepartmentId = $scope.DepartmentList[0].Id;
                    }
                    if (result.DataExtra.ProgramList != null) {
                        $scope.ProgramList = result.DataExtra.ProgramList;
                        //$scope.SearchByDepartmentId = $scope.DepartmentList[0].Id;
                    }
                    if (result.DataExtra.SemesterList != null) {
                        $scope.SemesterList = result.DataExtra.SemesterList;
                    }

                    if (result.DataExtra.DeptBatchList != null) {
                        $scope.DeptBatchList = result.DataExtra.DeptBatchList;
                        //$scope.SearchByDepartmentId = $scope.DepartmentList[0].Id;
                    }
                    if (result.DataExtra.BloodGroupEnumList != null) {
                        $scope.BloodGroupEnumList = result.DataExtra.BloodGroupEnumList;
                        //$scope.SearchByDepartmentId = $scope.DepartmentList[0].Id;
                    }
                    if (result.DataExtra.LevelTermList != null) {
                        $scope.LevelTermList = result.DataExtra.LevelTermList;
                    }
                    if (result.DataExtra.QuotaList != null) {
                        $scope.QuotaList = result.DataExtra.QuotaList;
                    }
                    if (result.DataExtra.ParentsPrimaryJobTypeEnumList != null) {
                        $scope.ParentsPrimaryJobTypeEnumList = result.DataExtra.ParentsPrimaryJobTypeEnumList;
                    }
                    
                    if (result.DataExtra.AdmissionStatusEnumList != null) {
                        $scope.AdmissionStatusEnumList = result.DataExtra.AdmissionStatusEnumList;
                        //$scope.SearchByAdmissionStatusEnumId = $scope.AdmissionStatusEnumList[0].Id;
                    }
                    if (result.DataExtra.EnrolmentStatusEnumList != null) {
                        $scope.EnrolmentStatusEnumList = result.DataExtra.EnrolmentStatusEnumList;
                        $scope.SearchByStatusId = $scope.EnrolmentStatusEnumList[0].Id;
                    }
                    if (result.DataExtra.EnrolmentTypeEnumList != null) {
                        $scope.EnrolmentTypeEnumList = result.DataExtra.EnrolmentTypeEnumList;
                        $scope.SearchByTypeId = $scope.EnrolmentTypeEnumList[0].Id;
                    }

                    if (result.DataExtra.ReligionEnumList != null) {
                        $scope.ReligionEnumList = result.DataExtra.ReligionEnumList;
                    }
                    
                    if (result.DataExtra.GenderEnumList != null) {
                        $scope.GenderEnumList = result.DataExtra.GenderEnumList;
                    }
                    
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to get Data Extra! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }

                $scope.getPagedStudentList();
            })
            .error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Data Extra! " + "Status: " + status.toString() + ". " + result.Message.toString();
                alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllStudentList = function () {
        $scope.IsLoading++;
        $http.get($scope.getStudentListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.StudentList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Student list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Student list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteStudentByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteStudentByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.StudentList.indexOf(obj);
                        $scope.StudentList.splice(i, 1);
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
    };
    $scope.deleteStudentById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteStudentByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.StudentList.indexOf(obj);
                        $scope.StudentList.splice(i, 1);
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

    $scope.isShowTrashedStudent = false;

    $scope.getTrashStudentById = function (obj, isPutInTrash) {
        var msg = "";
        if (isPutInTrash) {
             msg = "delete";
        } else {
             msg = "Undelete";
        }
        if (obj == null) {
            alertError("Please select a row to " + msg + "!");
        }
        bootbox.confirm("Are you sure you want to " + msg + " this data?", function (yes) {
            if (yes) {
                $http.get($scope.getTrashStudentByIdUrl, {
                    params: {
                        "stdId": obj.Id,
                        "isPutInTrash":isPutInTrash
                    }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.StudentList.indexOf(obj);
                        $scope.StudentList.splice(i, 1);
                        alertSuccess("Data successfully " + msg + "!");
                    } else {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Sorry unable to " + msg + "! " + result.Errors.toString();
                        alertError($scope.ErrorMsg);
                    }
                }).error(function (result, status) {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Sorry unable to " + msg + "! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
                    alertError($scope.ErrorMsg);
                });
            }
        });
    };

    $scope.selectAll = function ($event) {
    var checkbox = $event.target;
    for (var i = 0; i < $scope.StudentList.length; i++) {
        var entity = $scope.StudentList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====
    $scope.Init = function (
         getPagedStudentUrl
        , deleteStudentByIdUrl
        , getStudentListDataExtraUrl
        ,getTrashStudentByIdUrl
        ) {
        $scope.getPagedStudentUrl = getPagedStudentUrl;
        $scope.deleteStudentByIdUrl = deleteStudentByIdUrl;
        $scope.getStudentListDataExtraUrl = getStudentListDataExtraUrl;
        $scope.getTrashStudentByIdUrl = getTrashStudentByIdUrl;

        /*bind extra url if need
        $scope.getStudentByIdUrl = getStudentByIdUrl;
        $scope.getStudentDataExtraUrl = getStudentDataExtraUrl;
        $scope.saveStudentUrl = saveStudentUrl;
        $scope.getStudentListDataExtraUrl = getStudentListDataExtraUrl;
        $scope.saveStudentListUrl = saveStudentListUrl;*/

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



