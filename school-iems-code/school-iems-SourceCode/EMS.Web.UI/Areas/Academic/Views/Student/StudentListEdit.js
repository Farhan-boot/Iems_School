
//File:Admin_Student List Anjular  Controller
emsApp.controller('StudentListEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.StudentList = [];
    $scope.SelectedStudent = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;

    //search items
    $scope.IsGraduated = false;
    $scope.SearchClassRoll = "";
    $scope.SearchText = "";
    $scope.SearchUserName = "";
    $scope.SearchByDepartmentId = -1;
    $scope.SearchByLevelId = -1;
    $scope.SearchByTermId = -1;
    $scope.SearchByStatusId = -1;
    $scope.SearchByCampusId = -1;
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 0;
    $scope.PageNo = 0;

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
        $http.get($scope.getPagedStudentUrl, {
            params: {
            "classRoll": $scope.SearchClassRoll,
            "userName": $scope.SearchUserName, 
            "textkey": $scope.SearchText, 
            "deptId": $scope.SearchByDepartmentId,
            "levelTermId": $scope.SearchByLevelTermId,
            "campusId": $scope.SearchByCampusId,
            "statusId": $scope.SearchByStatusId,
            "pageSize": $scope.PageSize, 
            "pageNo": $scope.PageNo }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.StudentList = result.Data;
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
                        $scope.SearchByDepartmentId = $scope.DepartmentList[0].Id;
                    }
                    /*if (result.DataExtra.LevelList != null){
                        $scope.LevelList = result.DataExtra.LevelList;
                        $scope.SearchByLevelId = $scope.LevelList[0].Id;
                    }
                    if (result.DataExtra.TermList != null) {
                        $scope.TermList = result.DataExtra.TermList;
                        //$scope.SearchByTermId = $scope.TermList[0].Id;
                    }*/
                    if (result.DataExtra.LevelTermList != null) {
                        $scope.LevelTermList = result.DataExtra.LevelTermList;
                        //$scope.SearchByLevelTermId = $scope.LevelTermList[0].Id;
                    }
                    
                    if (result.DataExtra.CampusList != null) {
                        $scope.CampusList = result.DataExtra.CampusList;
                        $scope.SearchByCampusId = $scope.CampusList[0].Id;
                    }
                    if (result.DataExtra.EnrolmentStatusEnumList != null) {
                        $scope.EnrolmentStatusEnumList = result.DataExtra.EnrolmentStatusEnumList;
                        $scope.SearchByStatusId = $scope.EnrolmentStatusEnumList[0].Id;
                    }
                    if (result.DataExtra.EnrolmentTypeEnumList != null) {
                        $scope.EnrolmentTypeEnumList = result.DataExtra.EnrolmentTypeEnumList;
                        $scope.SearchByTypeId = $scope.EnrolmentTypeEnumList[0].Id;
                    }
                    if (result.DataExtra.ClassSectionList != null) {
                        $scope.ClassSectionList = result.DataExtra.ClassSectionList;
                    }
                    if (result.DataExtra.DeptBatchList != null) {
                        $scope.DeptBatchList = result.DataExtra.DeptBatchList;
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

    $scope.saveStudentList = function () {
        if (!$scope.validateStudent()) {
            return;
        }
        $http.post($scope.saveStudentListUrl + "/", $scope.StudentList).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    if (result.Data != null) {
                        $scope.StudentList = result.Data;
                    }
                    alertSuccess("Successfully saved all Student data!");
                    $scope.onAfterSaveStudent(result);
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to save all Student! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to save Student! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    };
    $scope.validateStudent = function () {
        return true;
    };

    $scope.setLevelId = function () {
        for (var i = 0; i < $scope.StudentList.length; i++) {
            var entity = $scope.StudentList[i];
            entity.LevelId = $scope.SelectedLevelId;
         }
    };
    $scope.setTermId = function () {
        for (var i = 0; i < $scope.StudentList.length; i++) {
            var entity = $scope.StudentList[i];
            entity.TermId = $scope.SelectedTermId;
         }
    };
    $scope.setClassSectionId = function () {
        for (var i = 0; i < $scope.StudentList.length; i++) {
            var entity = $scope.StudentList[i];
            entity.ClassSectionId = $scope.SelectedClassSectionId;
         }
    };
    $scope.setContinuingBatchId = function () {
        for (var i = 0; i < $scope.StudentList.length; i++) {
            var entity = $scope.StudentList[i];
            entity.ContinuingBatchId = $scope.SelectedContinuingBatchId;
         }
    };
    $scope.setEnrollmentStatusEnumId = function () {
        for (var i = 0; i < $scope.StudentList.length; i++) {
            var entity = $scope.StudentList[i];
            entity.EnrollmentStatusEnumId = $scope.SelectedEnrollmentStatusEnumId;
         }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====
    $scope.Init = function (
         getPagedStudentUrl
        ,saveStudentListUrl
        ,getStudentListDataExtraUrl
        ) {
        $scope.getPagedStudentUrl = getPagedStudentUrl;
        $scope.saveStudentListUrl = saveStudentListUrl;
        $scope.getStudentListDataExtraUrl = getStudentListDataExtraUrl;

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



