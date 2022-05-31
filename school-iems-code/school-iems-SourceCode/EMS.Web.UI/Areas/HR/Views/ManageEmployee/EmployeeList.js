//File:Admin_Employee List Anjular  Controller
emsApp.controller('EmployeeListCtrl', function ($scope, $http, $filter) {
    $scope.EmployeeList = [];
    $scope.SelectedEmployee = [];
    
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.IsShowPassword = false;

    //search items
    $scope.SearchText = "";
    $scope.SearchUserName = "";
    $scope.SearchByApproval = 2;
    $scope.SelectedSalaryTemplateGroupId = -1;
    $scope.SalarySettingsStatusEnumId = -1;
    $scope.SearchByDepartmentId = new Object();
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 50;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
        $scope.getEmployeeListDataExtra();
        $scope.getPagedEmployeeList();
    }
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedEmployeeList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedEmployeeList();
    };
    $scope.searchEmployeeList = function () {
            $scope.getPagedEmployeeList();
    };
    $scope.getPagedEmployeeList = function () {
        $scope.getEmployeeList();
    };
    /*For Search on data end*/
    $scope.getEmployeeList = function () {
        
        $http.get($scope.getPagedEmployeeUrl, {
            params: {
                "textkey": $scope.SearchText,
                "userName": $scope.SearchUserName,
                "approval": $scope.SearchByApproval,
                "deptId": $scope.SearchByDepartmentId,
                "categoryEnumId": $scope.SearchByCategoryEnumId,
                "jobClassEnumId": $scope.SearchByJobClassEnumId,
                "employeeCategoryEnumId": $scope.SearchByEmployeeCategoryEnumId,
                "employeeTypeEnumId": $scope.SearchByEmployeeTypeEnumId,
                "workingStatusEnumId": $scope.SearchByWorkingStatusEnumId,
                "jobTypeEnumId": $scope.SearchByJobTypeEnumId,
                "academicLevelEnumId": $scope.SearchByAcademicLevelEnumId,
                "campusId": $scope.SearchByCampusId,
                "salaryTemplateGroupId": $scope.SelectedSalaryTemplateGroupId,
                "salarySettingsStatusEnumId": $scope.SalarySettingsStatusEnumId,
                "pageSize": $scope.PageSize,
                "pageNo": $scope.PageNo
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.EmployeeList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Employee list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
            
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Employee list! " + "Status: " + status.toString() ;
            alertError($scope.ErrorMsg);
            
        });
    };
    $scope.getEmployeeListDataExtra = function () {
        
        $http.get($scope.getEmployeeListDataExtraUrl)
            .success(function (result) {
                if (!result.HasError) {
                    if (result.DataExtra.DepartmentList != null)
                        $scope.DepartmentList = result.DataExtra.DepartmentList;
                    if (result.DataExtra.CategoryEnumList != null)
                        $scope.CategoryEnumList = result.DataExtra.CategoryEnumList;
                    if (result.DataExtra.JobClassEnumList != null)
                        $scope.JobClassEnumList = result.DataExtra.JobClassEnumList;
                    if (result.DataExtra.AcademicLevelEnumList != null)
                        $scope.AcademicLevelEnumList = result.DataExtra.AcademicLevelEnumList;
                    if (result.DataExtra.CampusList != null)
                        $scope.CampusList = result.DataExtra.CampusList;
                    if (result.DataExtra.EmployeeCategoryEnumList != null)
                        $scope.EmployeeCategoryEnumList = result.DataExtra.EmployeeCategoryEnumList;
                    if (result.DataExtra.EmployeeTypeEnumList != null)
                        $scope.EmployeeTypeEnumList = result.DataExtra.EmployeeTypeEnumList;
                    if (result.DataExtra.WorkingStatusEnumList != null) {
                        $scope.WorkingStatusEnumList = result.DataExtra.WorkingStatusEnumList;
                        $scope.SearchByWorkingStatusEnumId = $scope.WorkingStatusEnumList[0].Id;
                    }
                    if (result.DataExtra.EmployeeTypeEnumList != null)
                        $scope.JobTypeEnumList = result.DataExtra.JobTypeEnumList;

                    if (result.DataExtra.SalaryTemplateGroupList != null)
                        $scope.SalaryTemplateGroupList = result.DataExtra.SalaryTemplateGroupList;

                    if (result.DataExtra.SalarySettingsStatusEnumList != null)
                        $scope.SalarySettingsStatusEnumList = result.DataExtra.SalarySettingsStatusEnumList;
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to get Data Extra! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            })
            .error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Data Extra! " + "Status: " + status.toString();
                alertError($scope.ErrorMsg);
            });
    };
    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllEmployeeList = function () {
        
        $http.get($scope.getEmployeeListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.EmployeeList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Employee list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
            
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Employee list! " + "Status: " + status.toString();
            alertError( $scope.ErrorMsg);
            
        });
    }
    $scope.deleteEmployeeByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                
                $http.get($scope.deleteEmployeeByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.EmployeeList.indexOf(obj);
                        $scope.EmployeeList.slice(i, 1);
                        alertSuccess("Successfully deleted Employee data!");

                    } else {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Unable to delete Employee data! " + result.Errors.toString();
                        alertError($scope.ErrorMsg);
                    }
                    
                }).error(function (result, status) {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to delete Employee data! " + "Status: " + status.toString();
                    alertError($scope.ErrorMsg);
                    
                });
            }
        });
    }
    $scope.deleteEmployeeById = function (obj) {

    }
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====
    $scope.Init = function (
         getPagedEmployeeUrl
        , deleteEmployeeByIdUrl
        , getEmployeeListDataExtraUrl
        ) {
        $scope.getPagedEmployeeUrl = getPagedEmployeeUrl;
        $scope.deleteEmployeeByIdUrl = deleteEmployeeByIdUrl;
        $scope.getEmployeeListDataExtraUrl = getEmployeeListDataExtraUrl;
        /*bind extra url if need
        $scope.getEmployeeByIdUrl = getEmployeeByIdUrl;
        $scope.getEmployeeDataExtraUrl = getEmployeeDataExtraUrl;
        $scope.getEmployeeListDataExtraUrl = getEmployeeListDataExtraUrl;
        $scope.saveEmployeeUrl = saveEmployeeUrl;
        $scope.getEmployeeListDataExtraUrl = getEmployeeListDataExtraUrl;
        $scope.saveEmployeeListUrl = saveEmployeeListUrl;*/

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



