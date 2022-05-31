
//File:Accounts_SemWiseScholarship List Anjular  Controller
emsApp.controller('MonthlyPayslipCtrl', function ($scope, $http, $filter, $document, cfpLoadingBar, $sce) {
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
    $scope.MonthlyPayslipList = [];
    $scope.IsLoadEmployeeMonthlyPayslip = false;
    $scope.IsShowTrashedItems = false;
    $scope.selectedMonthlyPayslipIndex = 0;
    $scope.EmployeeMonthlyPayslip = null;

    $scope.TotalDeduction = 0;
    $scope.TotalAddition = 0;

    $scope.loadPage = function () {
        $scope.loadMonthlyPayslipDataExtra();
        $scope.getMonthlyPayslipDetailsByEmployeeId();
        collapseSidebar();
        $scope.loadMonthlyPayslipDetailsDataExtra();
    };

    /*For Search on data end*/
    $scope.getMonthlyPayslipDetailsByEmployeeId = function () {
        $http.get($scope.getMonthlyPayslipByEmployeeIdUrl, {
            params: {
                "employeeId": $scope.EmployeeId,
                "isShowTrashedItems": $scope.IsShowTrashedItems
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.IsLoadEmployeeMonthlyPayslip = true;

                $scope.MonthlyPayslipList = result.Data;
                $scope.editMonthlyPayslip($scope.selectedMonthlyPayslipIndex);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Employee Monthly Payslip data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Employee Monthly Payslip data! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.saveMonthlyPayslipDetailList = function () {

        var monthlyPayslipDetailsListSaveJson = {
            MonthlyPayslipId: $scope.EmployeeMonthlyPayslip.Id,
            Remarks: $scope.EmployeeMonthlyPayslip.Remarks,
            MonthlyPayslipDetailList: $scope.MonthlyPayslipDetailsList
        }

        $http.post($scope.saveMonthlyPayslipDetailsListUrl + "/", monthlyPayslipDetailsListSaveJson).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    if (result.Data != null) {
                        $scope.MonthlyPayslipDetailsList = result.Data;

                        if (result.DataExtra.TotalAddition != null) {
                            $scope.TotalAddition = result.DataExtra.TotalAddition;
                        } else {
                            $scope.TotalAddition = 0;
                        }
                        if (result.DataExtra.TotalDeduction != null) {
                            $scope.TotalDeduction = result.DataExtra.TotalDeduction;
                        } else {
                            $scope.TotalDeduction = 0;
                        }
                    }
                    alertSuccess("Successfully saved Monthly Payslip data!");
                    $scope.getMonthlyPayslipDetailsByEmployeeId();
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

    $scope.renderHtml = function (html_code) {
        if (html_code == null || html_code === '') {
            html_code = '<center style="color:red;font-weight: bold;">No History Available.</center>';
        }
        return $sce.trustAsHtml(html_code);
    };

    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====


    $scope.Init = function (
        EmployeeId
        , getMonthlyPayslipByEmployeeIdUrl
        , deleteMonthlyPayslipByIdUrl
        , getMonthlyPayslipDataExtraUrl
        , getMonthlyPayslipByIdUrl
        , saveMonthlyPayslipUrl
        , deleteMonthlyPayslipDetailsByIdUrl
        , getMonthlyPayslipDetailsByMonthlyPayslipIdUrl
        , getMonthlyPayslipDetailsByIdUrl
        , getMonthlyPayslipDetailsDataExtraUrl
        , saveMonthlyPayslipDetailsUrl
        , saveMonthlyPayslipDetailsListUrl
        , trashUnTrashByIdUrl
    ) {
        $scope.EmployeeId = EmployeeId;

        $scope.getMonthlyPayslipByEmployeeIdUrl = getMonthlyPayslipByEmployeeIdUrl;
        $scope.deleteMonthlyPayslipByIdUrl = deleteMonthlyPayslipByIdUrl;
        $scope.getMonthlyPayslipDataExtraUrl = getMonthlyPayslipDataExtraUrl;
        $scope.getMonthlyPayslipByIdUrl = getMonthlyPayslipByIdUrl;
        $scope.saveMonthlyPayslipUrl = saveMonthlyPayslipUrl;
        $scope.deleteMonthlyPayslipDetailsByIdUrl = deleteMonthlyPayslipDetailsByIdUrl;
        $scope.getMonthlyPayslipDetailsByMonthlyPayslipIdUrl = getMonthlyPayslipDetailsByMonthlyPayslipIdUrl;

        $scope.getMonthlyPayslipDetailsByIdUrl = getMonthlyPayslipDetailsByIdUrl;
        $scope.getMonthlyPayslipDetailsDataExtraUrl = getMonthlyPayslipDetailsDataExtraUrl;
        $scope.saveMonthlyPayslipDetailsUrl = saveMonthlyPayslipDetailsUrl;
        $scope.saveMonthlyPayslipDetailsListUrl = saveMonthlyPayslipDetailsListUrl;
        $scope.trashUnTrashByIdUrl = trashUnTrashByIdUrl;
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

    $scope.showHistoryModal = function() {
        $('#PayslipHistoryModal').modal('show');
    }

    $scope.editMonthlyPayslip = function ($index) {
        if ($scope.MonthlyPayslipList == null || $scope.MonthlyPayslipList.length <= 0) {
            $scope.MonthlyPayslipDetailsList = [];
        }
        $scope.EmployeeMonthlyPayslip = $scope.MonthlyPayslipList[$index];
        $scope.selectedMonthlyPayslipIndex = $index;
        $scope.searchMonthlyPayslipDetailsList();

        $scope.SelectedPayroll = $scope.EmployeeMonthlyPayslip;
    };
    $scope.SuggestName = function () {
        var selectedCategory =
            $scope.CategoryTypeEnumList.find(x => x.Id === $scope.MonthlyPayslipDetails.CategoryTypeEnumId, true);
        if (selectedCategory != null) {
            $scope.MonthlyPayslipDetails.Name = selectedCategory.Name;
        }
    }

    $scope.CalculateTotalAdditionDeduction = function () {
        $scope.TotalAddition = 0;
        $scope.TotalDeduction = 0;

        var length = $scope.MonthlyPayslipDetailsList.length;

        for (var i = 0; i < length; i++) {
            if ($scope.MonthlyPayslipDetailsList[i].IsAddition) {
                $scope.TotalAddition += $scope.MonthlyPayslipDetailsList[i].Value;
            } else {
                $scope.TotalDeduction += $scope.MonthlyPayslipDetailsList[i].Value;
            }
        }

    }
    //==========Monthly Payslip Functions Start================================================

    $scope.deleteMonthlyPayslipById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteMonthlyPayslipByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.MonthlyPayslipList.indexOf(obj);
                        $scope.MonthlyPayslipList.splice(i, 1);
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

    $scope.trashUnTrashById = function (obj, isDelete) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        var deleteUnDeleteMsg = isDelete ? "Delete" : "Un-Delete";
        bootbox.confirm("Are you sure you want to " + deleteUnDeleteMsg + " this data?", function (yes) {
            if (yes) {
                $http.get($scope.trashUnTrashByIdUrl, {
                    params: {
                        "id": obj.Id,
                        "isDelete": isDelete
                    }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.MonthlyPayslipList.indexOf(obj);
                        $scope.MonthlyPayslipList.splice(i, 1);

                        $scope.selectedMonthlyPayslipIndex = 0;
                        $scope.editMonthlyPayslip($scope.selectedMonthlyPayslipIndex);
                        alertSuccess("Data successfully " + deleteUnDeleteMsg + "!");
                    } else {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Sorry unable to " + deleteUnDeleteMsg + "! " + result.Errors.toString();
                        alertError($scope.ErrorMsg);
                    }
                }).error(function (result, status) {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Sorry unable to " + deleteUnDeleteMsg + "! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
                    alertError($scope.ErrorMsg);
                });
            }
        });
    };


    $scope.getNewMonthlyPayslip = function () {
        $scope.getMonthlyPayslipById(0);
    };
    $scope.getMonthlyPayslipById = function (MonthlyPayslipId) {
        if (MonthlyPayslipId != null && MonthlyPayslipId !== '')
            $scope.MonthlyPayslipId = MonthlyPayslipId;
        else return;

        $http.get($scope.getMonthlyPayslipByIdUrl, {
            params: {
                 "id": $scope.MonthlyPayslipId
                 , "employeeId": $scope.EmployeeId
            }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.EmployeeMonthlyPayslip = result.Data;

                $('#MonthlyPayslipModal').modal('show');
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Monthly Payslip! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Monthly Payslip! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.loadMonthlyPayslipDataExtra = function (MonthlyPayslipId) {
        if (MonthlyPayslipId != null)
            $scope.MonthlyPayslipId = MonthlyPayslipId;

        $http.get($scope.getMonthlyPayslipDataExtraUrl, {
            params: {
                 "id": $scope.MonthlyPayslipId
            }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.DataExtra.SalaryTypeEnumList != null)
                    $scope.SalaryTypeEnumList = result.DataExtra.SalaryTypeEnumList;

                if (result.DataExtra.PayrollList != null)
                    $scope.PayrollList = result.DataExtra.PayrollList;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Monthly Payslip! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Monthly Payslip! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.saveMonthlyPayslip = function () {
       
        $http.post($scope.saveMonthlyPayslipUrl + "/", $scope.EmployeeMonthlyPayslip).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    if (result.Data != null) {
                        $scope.EmployeeMonthlyPayslip = result.Data;
                    }
                    alertSuccess("Successfully saved Monthly Payslip data!");
                    $scope.getMonthlyPayslipDetailsByEmployeeId();
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to save Monthly Payslip! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
                $('#MonthlyPayslipModal').modal('hide');
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to save Monthly Payslip! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    };

    //==========Monthly Payslip Function End======================

    //==========Monthly Payslip Details Function Start=============

    $scope.searchMonthlyPayslipDetailsList = function () {
        $scope.getPagedMonthlyPayslipDetailsList();
    };
    $scope.getPagedMonthlyPayslipDetailsList = function () {
        $scope.getMonthlyPayslipDetailsList();
    };
    /*For Search on data end*/
    $scope.getMonthlyPayslipDetailsList = function () {
        $http.get($scope.getMonthlyPayslipDetailsByMonthlyPayslipIdUrl, {
            params: {
                "MonthlyPayslipId": $scope.EmployeeMonthlyPayslip.Id
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
    $scope.deleteMonthlyPayslipDetailsById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteMonthlyPayslipDetailsByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        //var i = $scope.MonthlyPayslipDetailsList.indexOf(obj);
                        //$scope.MonthlyPayslipDetailsList.splice(i, 1);
                        //alertSuccess("Data successfully deleted!");

                        //if (result.DataExtra.History != null) {
                        //    $scope.MonthlyPayslip.History = result.DataExtra.History;
                        //}
                        $('#MonthlyPayslipDetailsModal').modal('hide');
                        $scope.getMonthlyPayslipDetailsByEmployeeId();
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

    $scope.getNewMonthlyPayslipDetails = function () {
        $scope.getMonthlyPayslipDetailsById(0);
    };
    $scope.getMonthlyPayslipDetailsById = function (MonthlyPayslipDetailsId) {
        if (MonthlyPayslipDetailsId != null && MonthlyPayslipDetailsId !== '')
            $scope.MonthlyPayslipDetailsId = MonthlyPayslipDetailsId;
        else return;

        $http.get($scope.getMonthlyPayslipDetailsByIdUrl, {
            params: {
                "id": $scope.MonthlyPayslipDetailsId
                , "monthlyPayslipId": $scope.EmployeeMonthlyPayslip.Id
                , "employeeId": $scope.EmployeeId
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
    $scope.loadMonthlyPayslipDetailsDataExtra = function (MonthlyPayslipDetailsId) {
        if (MonthlyPayslipDetailsId != null)
            $scope.MonthlyPayslipDetailsId = MonthlyPayslipDetailsId;

        $http.get($scope.getMonthlyPayslipDetailsDataExtraUrl, {
            params: { "id": $scope.MonthlyPayslipDetailsId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.DataExtra.SalaryTypeEnumList != null)
                    $scope.SalaryTypeEnumList = result.DataExtra.SalaryTypeEnumList;
                if (result.DataExtra.CategoryTypeEnumList != null)
                    $scope.CategoryTypeEnumList = result.DataExtra.CategoryTypeEnumList;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Monthly Payslip Details! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Monthly Payslip Details! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
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
                    $scope.getMonthlyPayslipDetailsByEmployeeId();
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
    //==========Monthly Payslip Details Function End===============

});



