
//File:Accounts_SemWiseScholarship List Anjular  Controller
emsApp.controller('SalarySettingsCtrl', function ($scope, $http, $filter, $document, cfpLoadingBar, $sce) {
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
    $scope.SalarySettingsList = [];
    $scope.IsLoadEmployeeSalarySettings = false;
    $scope.IsShowTrashedItems = false;
    $scope.selectedSalarySettingsIndex = 0;
    $scope.SalarySettings = null;

    $scope.TotalDeduction = 0;
    $scope.TotalAddition = 0;

    $scope.loadPage = function () {
        $scope.loadSalarySettingsDataExtra();
        $scope.getSalarySettingsDetailsByEmployeeId();
        collapseSidebar();
        $scope.loadSalarySettingsDetailsDataExtra();
    };

    /*For Search on data end*/
    $scope.getSalarySettingsDetailsByEmployeeId = function () {
        $http.get($scope.getSalarySettingsByEmployeeIdUrl, {
            params: {
                "employeeId": $scope.EmployeeId
                ,"isShowTrashedItems": $scope.IsShowTrashedItems
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.IsLoadEmployeeSalarySettings = true;

                $scope.SalarySettingsList = result.Data;
                $scope.editSalarySettings($scope.selectedSalarySettingsIndex);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Employee Salary Settings data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Employee Salary Settings data! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.saveSalarySettingsDetailList = function () {
        $http.post($scope.saveSalarySettingsDetailsListUrl + "/", $scope.SalarySettingsDetailsList).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    if (result.Data != null) {
                        $scope.SalarySettingsDetailsList = result.Data;
                    }
                    alertSuccess("Successfully saved Monthly Payslip data!");
                    $scope.getSalarySettingsDetailsByEmployeeId();
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
        , getSalarySettingsByEmployeeIdUrl
        , deleteSalarySettingsByIdUrl
        , getSalarySettingsDataExtraUrl
        , getSalarySettingsByIdUrl
        , saveSalarySettingsUrl
        , deleteSalarySettingsDetailsByIdUrl
        , getSalarySettingsDetailsBySalarySettingsIdUrl
        , getSalarySettingsDetailsByIdUrl
        , getSalarySettingsDetailsDataExtraUrl
        , saveSalarySettingsDetailsUrl
        , trashUnTrashByIdUrl
        , saveSalarySettingsDetailsListUrl
    ) {
        $scope.EmployeeId = EmployeeId;

        $scope.getSalarySettingsByEmployeeIdUrl = getSalarySettingsByEmployeeIdUrl;
        $scope.deleteSalarySettingsByIdUrl = deleteSalarySettingsByIdUrl;
        $scope.getSalarySettingsDataExtraUrl = getSalarySettingsDataExtraUrl;
        $scope.getSalarySettingsByIdUrl = getSalarySettingsByIdUrl;
        $scope.saveSalarySettingsUrl = saveSalarySettingsUrl;
        $scope.deleteSalarySettingsDetailsByIdUrl = deleteSalarySettingsDetailsByIdUrl;
        $scope.getSalarySettingsDetailsBySalarySettingsIdUrl = getSalarySettingsDetailsBySalarySettingsIdUrl;

        $scope.getSalarySettingsDetailsByIdUrl = getSalarySettingsDetailsByIdUrl;
        $scope.getSalarySettingsDetailsDataExtraUrl = getSalarySettingsDetailsDataExtraUrl;
        $scope.saveSalarySettingsDetailsUrl = saveSalarySettingsDetailsUrl;
        $scope.trashUnTrashByIdUrl = trashUnTrashByIdUrl;
        $scope.saveSalarySettingsDetailsListUrl = saveSalarySettingsDetailsListUrl;
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

    $scope.editSalarySettings = function ($index) {
        if ($scope.SalarySettingsList == null || $scope.SalarySettingsList.length <= 0) {
            $scope.SalarySettingsDetailsList = [];
        }
        $scope.SalarySettings = $scope.SalarySettingsList[$index];
        $scope.selectedSalarySettingsIndex = $index;
        $scope.searchSalarySettingsDetailsList();
    };
    $scope.SuggestName = function () {
        var selectedCategory =
            $scope.CategoryTypeEnumList.find(x => x.Id === $scope.SalarySettingsDetails.CategoryTypeEnumId, true);
        if (selectedCategory != null) {
            $scope.SalarySettingsDetails.Name = selectedCategory.Name;
        }
    }

    $scope.showHistoryModal = function () {
        $('#SalarySettingsHistoryModal').modal('show');
    }

    //==========Salary Settings Functions Start================================================

    $scope.deleteSalarySettingsById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteSalarySettingsByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.SalarySettingsList.indexOf(obj);
                        $scope.SalarySettingsList.splice(i, 1);
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
                        var i = $scope.SalarySettingsList.indexOf(obj);

                        $scope.SalarySettingsList.splice(i, 1);
                        alertSuccess("Data successfully " + deleteUnDeleteMsg + "!");

                        $scope.selectedSalarySettingsIndex = 0;
                        $scope.editSalarySettings($scope.selectedSalarySettingsIndex);
                        
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

    $scope.getNewSalarySettings = function () {
        $scope.getSalarySettingsById(0);
    };
    $scope.getSalarySettingsById = function (SalarySettingsId) {
        if (SalarySettingsId != null && SalarySettingsId !== '')
            $scope.SalarySettingsId = SalarySettingsId;
        else return;

        $http.get($scope.getSalarySettingsByIdUrl, {
            params: {
                 "id": $scope.SalarySettingsId
                 , "employeeId": $scope.EmployeeId
            }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.SalarySettings = result.Data;

                $('#SalarySettingsModal').modal('show');
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Salary Settings! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Salary Settings! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.loadSalarySettingsDataExtra = function (SalarySettingsId) {
        if (SalarySettingsId != null)
            $scope.SalarySettingsId = SalarySettingsId;

        $http.get($scope.getSalarySettingsDataExtraUrl, {
            params: {
                 "id": $scope.SalarySettingsId
            }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.DataExtra.SalaryTypeEnumList != null)
                    $scope.SalaryTypeEnumList = result.DataExtra.SalaryTypeEnumList;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Salary Settings! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Salary Settings! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.saveSalarySettings = function () {
       
        $http.post($scope.saveSalarySettingsUrl + "/", $scope.SalarySettings).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    if (result.Data != null) {
                        $scope.SalarySettings = result.Data;
                    }
                    alertSuccess("Successfully saved Salary Settings data!");
                    $scope.getSalarySettingsDetailsByEmployeeId();
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to save Salary Settings! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
                $('#SalarySettingsModal').modal('hide');
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to save Salary Settings! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    };

    //==========Salary Settings Function End======================

    //==========Salary Settings Details Function Start=============

    $scope.searchSalarySettingsDetailsList = function () {
        $scope.getPagedSalarySettingsDetailsList();
    };
    $scope.getPagedSalarySettingsDetailsList = function () {
        $scope.getSalarySettingsDetailsList();
    };
    /*For Search on data end*/
    $scope.getSalarySettingsDetailsList = function () {
        $http.get($scope.getSalarySettingsDetailsBySalarySettingsIdUrl, {
            params: {
                "salarySettingsId": $scope.SalarySettings.Id
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.SalarySettingsDetailsList = result.Data;

                if (result.DataExtra.TotalAddition!=null) {
                    $scope.TotalAddition = result.DataExtra.TotalAddition;
                }
                if (result.DataExtra.TotalDeduction != null) {
                    $scope.TotalDeduction = result.DataExtra.TotalDeduction;
                }

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Salary Settings Details list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Salary Settings Details list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.deleteSalarySettingsDetailsById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteSalarySettingsDetailsByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.SalarySettingsDetailsList.indexOf(obj);
                        $scope.SalarySettingsDetailsList.splice(i, 1);
                        alertSuccess("Data successfully deleted!");

                        $('#SalarySettingsDetailsModal').modal('hide');
                        $scope.getSalarySettingsDetailsByEmployeeId();

                        if (result.DataExtra.History != null) {
                            $scope.SalarySettings.History = result.DataExtra.History;
                        }
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

    $scope.getNewSalarySettingsDetails = function () {
        $scope.getSalarySettingsDetailsById(0);
    };
    $scope.getSalarySettingsDetailsById = function (SalarySettingsDetailsId) {
        if (SalarySettingsDetailsId != null && SalarySettingsDetailsId !== '')
            $scope.SalarySettingsDetailsId = SalarySettingsDetailsId;
        else return;

        $http.get($scope.getSalarySettingsDetailsByIdUrl, {
            params: {
                "id": $scope.SalarySettingsDetailsId
                , "salarySettingsId": $scope.SalarySettings.Id
            }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.SalarySettingsDetails = result.Data;
                $('#SalarySettingsDetailsModal').modal('show');
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Salary Settings Details! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Salary Settings Details! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.loadSalarySettingsDetailsDataExtra = function (SalarySettingsDetailsId) {
        if (SalarySettingsDetailsId != null)
            $scope.SalarySettingsDetailsId = SalarySettingsDetailsId;

        $http.get($scope.getSalarySettingsDetailsDataExtraUrl, {
            params: { "id": $scope.SalarySettingsDetailsId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.DataExtra.SalaryTypeEnumList != null)
                    $scope.SalaryTypeEnumList = result.DataExtra.SalaryTypeEnumList;
                if (result.DataExtra.CategoryTypeEnumList != null)
                    $scope.CategoryTypeEnumList = result.DataExtra.CategoryTypeEnumList;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Salary Settings Details! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Salary Settings Details! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.saveSalarySettingsDetails = function () {
        $http.post($scope.saveSalarySettingsDetailsUrl + "/", $scope.SalarySettingsDetails).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    if (result.Data != null) {
                        $scope.SalarySettingsDetails = result.Data;
                    }
                    $scope.getSalarySettingsDetailsList();
                    alertSuccess("Successfully saved Salary Settings Details data!");
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to save Salary Settings Details! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
                $('#SalarySettingsDetailsModal').modal('hide');
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to save Salary Settings Details! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    };
    //==========Salary Settings Details Function End===============

});



