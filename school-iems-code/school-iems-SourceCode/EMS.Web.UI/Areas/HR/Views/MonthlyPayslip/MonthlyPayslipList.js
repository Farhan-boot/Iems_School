
//File:HR_MonthlyPayslip List Anjular  Controller
emsApp.controller('MonthlyPayslipListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.MonthlyPayslipList = [];
    $scope.SelectedMonthlyPayslip = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
     $scope.SelectedPayrollId=0;      
     $scope.SelectedSalarySettingsId=0;      
     $scope.SelectedEmployeeId=0;      
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
      $scope.getMonthlyPayslipListExtraData();
      $scope.getPagedMonthlyPayslipList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedMonthlyPayslipList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedMonthlyPayslipList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedMonthlyPayslipList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedMonthlyPayslipList();
    };
    $scope.searchMonthlyPayslipList = function () {
        $scope.getPagedMonthlyPayslipList();
    };
    $scope.getPagedMonthlyPayslipList = function () {
        $scope.getMonthlyPayslipList();
    };
    /*For Search on data end*/
    $scope.getMonthlyPayslipList = function () {
        $http.get($scope.getPagedMonthlyPayslipUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
           ,"payrollId": $scope.SelectedPayrollId      
           ,"salarySettingsId": $scope.SelectedSalarySettingsId      
           ,"employeeId": $scope.SelectedEmployeeId      
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.MonthlyPayslipList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Monthly Payslip list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Monthly Payslip list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getMonthlyPayslipListExtraData= function()
    {
            $http.get($scope.getMonthlyPayslipListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                  //DropDown Option Tables
                     if(result.DataExtra.PayrollList!=null)
                       $scope.PayrollList =  result.DataExtra.PayrollList; 

                     if(result.DataExtra.SalarySettingsList!=null)
                       $scope.SalarySettingsList =  result.DataExtra.SalarySettingsList; 

                     if(result.DataExtra.EmployeeList!=null)
                       $scope.EmployeeList =  result.DataExtra.EmployeeList; 

                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Monthly Payslip! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Monthly Payslip! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllMonthlyPayslipList = function () {
        $scope.IsLoading++;
        $http.get($scope.getMonthlyPayslipListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.MonthlyPayslipList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Monthly Payslip list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Monthly Payslip list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
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
    /*$scope.deleteMonthlyPayslipByObj = function (obj) {
        console.log(obj);
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
                    $scope.ErrorMsg = "Sorry unable to delete! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                    alertError($scope.ErrorMsg);
                });
            }
        });
    };*/

    $scope.selectAll = function ($event) {
    var checkbox = $event.target;
    for (var i = 0; i < $scope.MonthlyPayslipList.length; i++) {
        var entity = $scope.MonthlyPayslipList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedMonthlyPayslipUrl
        ,deleteMonthlyPayslipByIdUrl
        ,getMonthlyPayslipListDataExtraUrl
        ,saveMonthlyPayslipListUrl
        ,getMonthlyPayslipByIdUrl
        ,getMonthlyPayslipDataExtraUrl
        ,saveMonthlyPayslipUrl
        ) {
        $scope.getPagedMonthlyPayslipUrl = getPagedMonthlyPayslipUrl;
        $scope.deleteMonthlyPayslipByIdUrl = deleteMonthlyPayslipByIdUrl;
        /*bind extra url if need*/
        $scope.getMonthlyPayslipListDataExtraUrl = getMonthlyPayslipListDataExtraUrl;
        $scope.saveMonthlyPayslipListUrl = saveMonthlyPayslipListUrl;
        $scope.getMonthlyPayslipByIdUrl = getMonthlyPayslipByIdUrl;
        $scope.getMonthlyPayslipDataExtraUrl = getMonthlyPayslipDataExtraUrl;
        $scope.saveMonthlyPayslipUrl = saveMonthlyPayslipUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



