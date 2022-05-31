
//File:HR_Payroll List Anjular  Controller
emsApp.controller('PayrollListCtrl', function ($scope, $http, $filter, cfpLoadingBar, $sce) {
    $scope.PayrollList = [];
    $scope.SelectedPayroll = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
    $scope.IsShowTrashedItems = false;
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.Payroll = null;

    $scope.selectedPayrollIndex = 0;


    $scope.loadPage = function () {
      $scope.getPayrollListExtraData();
      $scope.getPagedPayrollList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedPayrollList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedPayrollList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedPayrollList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedPayrollList();
    };
    $scope.searchPayrollList = function () {
        $scope.getPagedPayrollList();
    };
    $scope.getPagedPayrollList = function () {
        $scope.getPayrollList();
    };
    /*For Search on data end*/
    $scope.getPayrollList = function () {
        $http.get($scope.getPagedPayrollUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
                , "isShowTrashedItems": $scope.IsShowTrashedItems
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.PayrollList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;

                $scope.editPayroll($scope.selectedPayrollIndex);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Payroll list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Payroll list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getPayrollListExtraData= function()
    {
            $http.get($scope.getPayrollListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.MonthEnumList!=null)
                         $scope.MonthEnumList = result.DataExtra.MonthEnumList;
                  //DropDown Option Tables
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Payroll! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Payroll! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllPayrollList = function () {
        $scope.IsLoading++;
        $http.get($scope.getPayrollListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.PayrollList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Payroll list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Payroll list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deletePayrollById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deletePayrollByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.PayrollList.indexOf(obj);

                        $scope.selectedPayrollIndex = 0;
                        $scope.editPayroll($scope.selectedPayrollIndex);

                        $scope.PayrollList.splice(i, 1);
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

    $scope.trashUnTrashById = function (obj,isDelete) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        var deleteUnDeleteMsg = isDelete ? "Delete" : "Un-Delete";
        bootbox.confirm("Are you sure you want to " + deleteUnDeleteMsg+" this data?", function (yes) {
            if (yes) {
                $http.get($scope.trashUnTrashByIdUrl, {
                    params: {
                        "id": obj.Id,
                        "isDelete": isDelete
                    }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.PayrollList.indexOf(obj);
                        $scope.editPayroll(0);

                        $scope.PayrollList.splice(i, 1);
                        alertSuccess("Data successfully " + deleteUnDeleteMsg+"!");
                    } else {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Sorry unable to " + deleteUnDeleteMsg+"! " + result.Errors.toString();
                        alertError($scope.ErrorMsg);
                    }
                }).error(function (result, status) {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Sorry unable to " + deleteUnDeleteMsg +"! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
                    alertError($scope.ErrorMsg);
                });
            }
        });
    };

    /*$scope.deletePayrollByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deletePayrollByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.PayrollList.indexOf(obj);
                        $scope.PayrollList.splice(i, 1);
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

    $scope.getNewPayroll = function () {
        $scope.getPayrollById(0);
    };
    $scope.getPayrollById = function (PayrollId) {
        if (PayrollId != null && PayrollId !== '')
            $scope.PayrollId = PayrollId;
        else return;

        $http.get($scope.getPayrollByIdUrl, {
            params: { "id": $scope.PayrollId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.Payroll = result.Data;
                $scope.Payroll.PayrollMonthYear = new Date(result.Data.PayrollMonthYear);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Payroll! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Payroll! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.loadPayrollDataExtra = function (PayrollId) {
        if (PayrollId != null)
            $scope.PayrollId = PayrollId;

        $http.get($scope.getPayrollDataExtraUrl, {
            params: { "id": $scope.PayrollId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.DataExtra.MonthEnumList != null)
                    $scope.MonthEnumList = result.DataExtra.MonthEnumList;
                $scope.onAfterLoadPayrollDataExtra(result);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Payroll! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Payroll! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.savePayroll = function () {
        $http.post($scope.savePayrollUrl + "/", $scope.Payroll).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    if (result.Data != null) {

                        if ($scope.Payroll.Id === 0) {
                            $scope.Payroll = result.Data;
                            $scope.PayrollList.push(result.Data);
                        } else {
                            $scope.Payroll = result.Data;
                            var i = $scope.PayrollList.indexOf($scope.SelectedPayroll);

                            $scope.PayrollList[i] = result.Data;
                            $scope.SelectedPayroll = $scope.PayrollList[i];
                        }
                        $scope.Payroll.PayrollMonthYear = new Date(result.Data.PayrollMonthYear);
                    }
                    alertSuccess("Successfully saved Payroll data!");
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to save Payroll! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to save Payroll! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    };


    $scope.selectAll = function ($event) {
    var checkbox = $event.target;
    for (var i = 0; i < $scope.PayrollList.length; i++) {
        var entity = $scope.PayrollList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedPayrollUrl
        ,deletePayrollByIdUrl
        ,getPayrollListDataExtraUrl
        ,getPayrollByIdUrl
        ,getPayrollDataExtraUrl
        ,savePayrollUrl
        , trashUnTrashByIdUrl
        ) {
        $scope.getPagedPayrollUrl = getPagedPayrollUrl;
        $scope.deletePayrollByIdUrl = deletePayrollByIdUrl;
        /*bind extra url if need*/
        $scope.getPayrollListDataExtraUrl = getPayrollListDataExtraUrl;

        $scope.getPayrollByIdUrl = getPayrollByIdUrl;
        $scope.getPayrollDataExtraUrl = getPayrollDataExtraUrl;
        $scope.savePayrollUrl = savePayrollUrl;
        $scope.trashUnTrashByIdUrl = trashUnTrashByIdUrl;

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 
    $scope.editPayroll = function ($index) {
        $scope.SelectedPayroll = $scope.PayrollList[$index];
        $scope.selectedPayrollIndex = $index;
        $scope.getPayrollById($scope.SelectedPayroll.Id);
    };
    $scope.renderHtml = function (html_code) {
        if (html_code == null || html_code === '') {
            html_code = '<center style="color:red;font-weight: bold;">No History Available.</center>';
        }
        return $sce.trustAsHtml(html_code);
    };

    $scope.showHistoryModal = function () {
        $('#HistoryModal').modal('show');
    }
    //======Custom property and Functions End========================================================== 

});



