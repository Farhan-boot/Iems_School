
//File:HR_MonthlyPayslip List Anjular  Controller
emsApp.controller('MonthlyPayslipListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.MonthlyPayslipList = [];
    $scope.SelectedMonthlyPayslip = [];
    $scope.GenerateEmployeePayslipJson = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = true;
    $scope.SelectedPayroll=null;
     $scope.SelectedSalarySettingsId=0;      
    $scope.SelectedEmployeeId = 0;
    $scope.selectedEmployeeMonthlyPayslipIndex = 0;
    //search items
    $scope.SearchText = "";
    $scope.SearchUserName = "";
    $scope.SearchByApproval = 2;
    $scope.SearchByDepartmentId = new Object();
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 5;
    $scope.PageNo = 1;

    $scope.IsEnableBulkDrafting = false;

    $scope.IsGenerateAble = false;
    $scope.IsDraftUndraftAble = false;

    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedEmployeeList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedEmployeeList();
    };

    $scope.loadPage = function () {
        $scope.getMonthlyPayslipManagerExtraData();
    };
   
    $scope.getMonthlyPayslipManagerExtraData= function()
    {
        $http.get($scope.getMonthlyPayslipManagerDataExtraUrl , {
                params: { }
        }).success(function (result, status) {

            $scope.HasViewPermission = result.HasViewPermission;

            if (!result.HasError) {
                $scope.HasError = false;
                //DropDown Option Tables
                if (result.DataExtra.PayrollList != null) {
                    $scope.PayrollList = result.DataExtra.PayrollList;

                    if (result.DataExtra.SelectedPayrollId != null) {
                        var selectedPayroll = $scope.PayrollList.filter(x => x.Id === result.DataExtra.SelectedPayrollId,true);
                        $scope.SelectedPayroll = selectedPayroll[0];
                    }
                    

                    if (result.DataExtra.DepartmentList != null)
                        $scope.DepartmentList = result.DataExtra.DepartmentList;
                    if (result.DataExtra.CategoryEnumList != null)
                        $scope.CategoryEnumList = result.DataExtra.CategoryEnumList;
                    if (result.DataExtra.JobClassEnumList != null)
                        $scope.JobClassEnumList = result.DataExtra.JobClassEnumList;
                    if (result.DataExtra.AcademicLevelEnumList != null)
                        $scope.AcademicLevelEnumList = result.DataExtra.AcademicLevelEnumList;
                    if (result.DataExtra.SalaryTemplateList != null)
                        $scope.SalaryTemplateList = result.DataExtra.SalaryTemplateList;
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

                    if (result.DataExtra.CategoryTypeEnumList != null)
                        $scope.CategoryTypeEnumList = result.DataExtra.CategoryTypeEnumList;

                    if (result.DataExtra.SalaryTypeEnumList != null)
                        $scope.SalaryTypeEnumList = result.DataExtra.SalaryTypeEnumList;

                    $scope.getMonthlyPayslipByPayrollId();
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to load option data for Monthly Payslip! No Payroll Found.";
                    alertError($scope.ErrorMsg);

                }

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Monthly Payslip! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Monthly Payslip! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    $scope.getMonthlyPayslipByPayrollId = function () {
        $http.get($scope.getMonthlyPayslipByPayrollIdUrl, {
            params: {
                "payrollId": $scope.SelectedPayroll.Id
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;

                $scope.MonthlyPayslipList = result.Data;
                $scope.editEmployeeMonthlyPayslip($scope.selectedEmployeeMonthlyPayslipIndex);
            } else {
                //$scope.HasError = true;
                $scope.MonthlyPayslipList = [];
                $scope.ErrorMsg = "Unable to get Employee Monthly Payslip data! " + result.Errors.toString();
                //alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Employee Monthly Payslip data! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.getMonthlyPayslipDetailsList = function () {
        $http.get($scope.getMonthlyPayslipDetailsByMonthlyPayslipIdUrl, {
            params: {
                "monthlyPayslipId": $scope.EmployeeMonthlyPayslip.MonthlyPayslipId
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.MonthlyPayslipDetailsList = result.Data;

                if (result.DataExtra.TotalAddition) {
                    $scope.TotalAddition = result.DataExtra.TotalAddition;
                } else {
                    $scope.TotalAddition = 0;
                }
                if (result.DataExtra.TotalDeduction) {
                    $scope.TotalDeduction = result.DataExtra.TotalDeduction;
                } else {
                    $scope.TotalDeduction = 0;
                }

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Monthly Payslip Details list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Monthly Payslip Details list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.CalculateTotalAdditionDeduction = function() {
        $scope.TotalAddition = 0;
        $scope.TotalDeduction = 0;

        var length = $scope.MonthlyPayslipDetailsList.length;

        for (var i = 0; i < length;i++)
        {
            if ($scope.MonthlyPayslipDetailsList[i].IsAddition) {
                $scope.TotalAddition += $scope.MonthlyPayslipDetailsList[i].Value;
            } else {
                $scope.TotalDeduction += $scope.MonthlyPayslipDetailsList[i].Value;
            }
        }

    }

    $scope.saveMonthlyPayslipDetailList = function () {

        var monthlyPayslipDetailsListSaveJson = {
            MonthlyPayslipId: $scope.EmployeeMonthlyPayslip.MonthlyPayslipId,
            Remarks: $scope.EmployeeMonthlyPayslip.Remarks,
            MonthlyPayslipDetailList: $scope.MonthlyPayslipDetailsList
        }

        $http.post($scope.saveMonthlyPayslipDetailsListUrl + "/", monthlyPayslipDetailsListSaveJson).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    if (result.Data != null) {
                        $scope.MonthlyPayslipDetailsList = result.Data;

                        if (result.DataExtra.TotalAddition) {
                            $scope.TotalAddition = result.DataExtra.TotalAddition;
                        } else {
                            $scope.TotalAddition = 0;
                        }
                        if (result.DataExtra.TotalDeduction) {
                            $scope.TotalDeduction = result.DataExtra.TotalDeduction;
                        } else {
                            $scope.TotalDeduction = 0;
                        }
                    }
                    alertSuccess("Successfully saved Monthly Payslip data!");
                    $scope.getMonthlyPayslipByPayrollId();
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to save Monthly Payslip! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to save Monthly Payslip! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    };

    $scope.getGenerateAbleEmployeeList =function() {
        $('#MonthlyPayslipModal').modal('show');
    }

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
                "payrollId": $scope.SelectedPayroll.Id,
                "deptId": $scope.SearchByDepartmentId,
                "categoryEnumId": $scope.SearchByCategoryEnumId,
                "jobClassEnumId": $scope.SearchByJobClassEnumId,
                "employeeCategoryEnumId": $scope.SearchByEmployeeCategoryEnumId,
                "employeeTypeEnumId": $scope.SearchByEmployeeTypeEnumId,
                "workingStatusEnumId": $scope.SearchByWorkingStatusEnumId,
                "jobTypeEnumId": $scope.SearchByJobTypeEnumId,
                "academicLevelEnumId": $scope.SearchByAcademicLevelEnumId,
                "salaryTemplateId": $scope.SelectedSalaryTemplateId,
                "pageSize": $scope.PageSize,
                "pageNo": $scope.PageNo
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.GenerateEmployeePayslipJson = result.Data;
                $scope.totalItems = result.DataExtra.Count;
                $scope.totalServerItems = $scope.GenerateEmployeePayslipJson.EmployeeList.length;
            } else {
                $scope.GenerateEmployeePayslipJson = null;
                $scope.totalServerItems = 0;
                $scope.totalItems = 0;

                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Employee list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }

        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Employee list! " + "Status: " + status.toString();
            alertError($scope.ErrorMsg);

        });
    };

    $scope.generateMonthlyPayslip = function () {

        bootbox.confirm("Are you sure you want generate Payslip of the selected Employees?",
            function(yes) {
                if (yes) {
                    $http.post($scope.generateMonthlyPayslipUrl + "/", $scope.GenerateEmployeePayslipJson).
                        success(function (result, status) {
                            if (!result.HasError) {
                                $scope.HasError = false;
                                alertSuccess("All Selected Employees Salary Generated Successfully.");
                                $scope.getMonthlyPayslipByPayrollId();
                            } else {
                                $scope.HasError = true;
                                $scope.ErrorMsg = "Unable to generate Monthly Payslip! " + result.Errors.toString();
                                alertError($scope.ErrorMsg);
                            }
                            $scope.GenerateEmployeePayslipJson = null;
                            $('#MonthlyPayslipModal').modal('hide');
                        }).
                        error(function (result, status) {
                            $scope.HasError = true;
                            $scope.ErrorMsg = "Unable to generate Monthly Payslip! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                            alertError($scope.ErrorMsg);
                        });
                }
            });
    };
    $scope.getNewMonthlyPayslipDetails = function () {
        $scope.getMonthlyPayslipDetailsById(0);
    };
    $scope.getMonthlyPayslipDetailsById = function (MonthlyPayslipDetailsId) {
        if (MonthlyPayslipDetailsId != null && MonthlyPayslipDetailsId !== '')
            $scope.MonthlyPayslipDetailsId = MonthlyPayslipDetailsId;
        else return;

        log($scope.EmployeeMonthlyPayslip);
        $http.get($scope.getMonthlyPayslipDetailsByIdUrl, {
            params: {
                "id": $scope.MonthlyPayslipDetailsId
                , "monthlyPayslipId": $scope.EmployeeMonthlyPayslip.MonthlyPayslipId
                , "employeeId": $scope.EmployeeMonthlyPayslip.EmployeeId
            }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.MonthlyPayslipDetails = result.Data;
                $('#MonthlyPayslipDetailsModal').modal('show');
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Monthly Payslip Details! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Monthly Payslip Details! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.saveMonthlyPayslipDetails = function () {
        $http.post($scope.saveMonthlyPayslipDetailsUrl + "/", $scope.MonthlyPayslipDetails).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    if (result.Data != null) {
                        $scope.MonthlyPayslipDetails = result.Data;
                    }
                    $scope.getMonthlyPayslipByPayrollId();
                    alertSuccess("Successfully saved Monthly Payslip Details data!");
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to save Monthly Payslip Details! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
                $('#MonthlyPayslipDetailsModal').modal('hide');
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to save Monthly Payslip Details! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    };

    $scope.draftOrUndraftSelected = function (isDraft) {

        var draftUndraftMsg = isDraft ? "Draft" : "Un-Draft";

        var draftUnDraftJson = {
            IsDraft: isDraft,
            PayrollId: $scope.SelectedPayroll.Id,
            PayslipList: $scope.MonthlyPayslipList
        }

        $scope.GenerateEmployeePayslipJson.IsDraft = isDraft;
        bootbox.confirm("Are you sure you want set selected Payslip as " + draftUndraftMsg,
            function (yes) {
                if (yes) {
                    $http.post($scope.draftOrUndraftSelectedUrl + "/", draftUnDraftJson).
                        success(function (result, status) {
                            if (!result.HasError) {
                                $scope.HasError = false;
                                alertSuccess("All Selected Employees Salary set as " + draftUndraftMsg + " Successfully.");
                                $scope.getMonthlyPayslipByPayrollId();
                                $scope.IsEnableBulkDrafting = false;
                            } else {
                                $scope.HasError = true;
                                $scope.ErrorMsg = "Unable to update Monthly Payslip! " + result.Errors.toString();
                                alertError($scope.ErrorMsg);
                            }
                        }).
                        error(function (result, status) {
                            $scope.HasError = true;
                            $scope.ErrorMsg = "Unable to update Monthly Payslip! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                            alertError($scope.ErrorMsg);
                        });
                }
            });
    };

    $scope.deleteMonthlyPayslipDetailsById = function (obj) {
        log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }

        bootbox.confirm("Are you sure you want to delete this field Permanently data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteMonthlyPayslipDetailsByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        $('#MonthlyPayslipDetailsModal').modal('hide');
                        $scope.getMonthlyPayslipByPayrollId();
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

    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
        getMonthlyPayslipManagerDataExtraUrl
        , getMonthlyPayslipByPayrollIdUrl
        , getMonthlyPayslipDetailsByMonthlyPayslipIdUrl
        , saveMonthlyPayslipDetailsListUrl
        , getPagedEmployeeUrl
        , generateMonthlyPayslipUrl
        , getMonthlyPayslipDetailsByIdUrl
        , saveMonthlyPayslipDetailsUrl
        , draftOrUndraftSelectedUrl
        , deleteMonthlyPayslipDetailsByIdUrl
        ) {
        $scope.getMonthlyPayslipManagerDataExtraUrl = getMonthlyPayslipManagerDataExtraUrl;
        $scope.getMonthlyPayslipByPayrollIdUrl = getMonthlyPayslipByPayrollIdUrl;
        $scope.getMonthlyPayslipDetailsByMonthlyPayslipIdUrl = getMonthlyPayslipDetailsByMonthlyPayslipIdUrl;
        $scope.saveMonthlyPayslipDetailsListUrl = saveMonthlyPayslipDetailsListUrl;
        $scope.getPagedEmployeeUrl = getPagedEmployeeUrl;
        $scope.generateMonthlyPayslipUrl = generateMonthlyPayslipUrl;
        $scope.getMonthlyPayslipDetailsByIdUrl = getMonthlyPayslipDetailsByIdUrl;
        $scope.saveMonthlyPayslipDetailsUrl = saveMonthlyPayslipDetailsUrl;
        $scope.draftOrUndraftSelectedUrl = draftOrUndraftSelectedUrl;
        $scope.deleteMonthlyPayslipDetailsByIdUrl = deleteMonthlyPayslipDetailsByIdUrl;

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 
    $scope.checkIsGenerateAble = function () {
        for (var i = 0; i < $scope.GenerateEmployeePayslipJson.EmployeeList.length; i++) {
            if ($scope.GenerateEmployeePayslipJson.EmployeeList[i].IsSelected) {
                $scope.IsGenerateAble = true;
                return;
            }
        }
        $scope.IsGenerateAble = false;
        return;
    }

    $scope.checkIsDraftUndraftAble = function () {
        for (var i = 0; i < $scope.MonthlyPayslipList.length; i++) {
            if ($scope.MonthlyPayslipList[i].IsSelected) {
                $scope.IsDraftUndraftAble = true;
                return;
            }
        }
        $scope.IsDraftUndraftAble = false;
        return;
    }

    $scope.selectAllPayslip = function ($event) {
        var checkbox = $event.target;

        if (checkbox.checked) {
            $scope.IsDraftUndraftAble = true;
        } else {
            $scope.IsDraftUndraftAble = false;
        }

        for (var i = 0; i < $scope.MonthlyPayslipList.length; i++) {
            var entity = $scope.MonthlyPayslipList[i];
            entity.IsSelected = checkbox.checked;
        }
    };

    $scope.selectAll = function ($event) {
        var checkbox = $event.target;

        if (checkbox.checked) {
            $scope.IsGenerateAble = true;
        } else {
            $scope.IsGenerateAble = false;
        }

        for (var i = 0; i < $scope.GenerateEmployeePayslipJson.EmployeeList.length; i++) {
            var entity = $scope.GenerateEmployeePayslipJson.EmployeeList[i];
            entity.IsSelected = checkbox.checked;
        }
    };

    $scope.editEmployeeMonthlyPayslip = function ($index) {
        $scope.EmployeeMonthlyPayslip = $scope.MonthlyPayslipList[$index];
        $scope.selectedEmployeeMonthlyPayslipIndex = $index;

        $scope.getMonthlyPayslipDetailsList();
    };

    $scope.SuggestName = function () {
        var selectedCategory =
            $scope.CategoryTypeEnumList.find(x => x.Id === $scope.MonthlyPayslipDetails.CategoryTypeEnumId, true);
        if (selectedCategory != null) {
            $scope.MonthlyPayslipDetails.Name = selectedCategory.Name;
        }
    }

    //======Custom property and Functions End========================================================== 

});



