
//File:HR_MonthlyPayslipDetails List Anjular  Controller
emsApp.controller('MonthlyPayslipDetailsListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.MonthlyPayslipDetailsList = [];
    $scope.SelectedMonthlyPayslipDetails = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
     $scope.SelectedMonthlyPayslipId=0;      
     $scope.SelectedEmployeeId=0;      
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
      $scope.getMonthlyPayslipDetailsListExtraData();
      $scope.getPagedMonthlyPayslipDetailsList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedMonthlyPayslipDetailsList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedMonthlyPayslipDetailsList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedMonthlyPayslipDetailsList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedMonthlyPayslipDetailsList();
    };
    $scope.searchMonthlyPayslipDetailsList = function () {
        $scope.getPagedMonthlyPayslipDetailsList();
    };
    $scope.getPagedMonthlyPayslipDetailsList = function () {
        $scope.getMonthlyPayslipDetailsList();
    };
    /*For Search on data end*/
    $scope.getMonthlyPayslipDetailsList = function () {
        $http.get($scope.getPagedMonthlyPayslipDetailsUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
           ,"monthlyPayslipId": $scope.SelectedMonthlyPayslipId      
           ,"employeeId": $scope.SelectedEmployeeId      
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.MonthlyPayslipDetailsList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
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
    $scope.getMonthlyPayslipDetailsListExtraData= function()
    {
            $http.get($scope.getMonthlyPayslipDetailsListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.SalaryTypeEnumList!=null)
                         $scope.SalaryTypeEnumList = result.DataExtra.SalaryTypeEnumList;
                      if(result.DataExtra.CategoryTypeEnumList!=null)
                         $scope.CategoryTypeEnumList = result.DataExtra.CategoryTypeEnumList;
                  //DropDown Option Tables
                     if(result.DataExtra.MonthlyPayslipList!=null)
                       $scope.MonthlyPayslipList =  result.DataExtra.MonthlyPayslipList; 

                     if(result.DataExtra.EmployeeList!=null)
                       $scope.EmployeeList =  result.DataExtra.EmployeeList; 

                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Monthly Payslip Details! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Monthly Payslip Details! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllMonthlyPayslipDetailsList = function () {
        $scope.IsLoading++;
        $http.get($scope.getMonthlyPayslipDetailsListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.MonthlyPayslipDetailsList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Monthly Payslip Details list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Monthly Payslip Details list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
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
                        var i = $scope.MonthlyPayslipDetailsList.indexOf(obj);
                        $scope.MonthlyPayslipDetailsList.splice(i, 1);
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
    /*$scope.deleteMonthlyPayslipDetailsByObj = function (obj) {
        console.log(obj);
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
                        var i = $scope.MonthlyPayslipDetailsList.indexOf(obj);
                        $scope.MonthlyPayslipDetailsList.splice(i, 1);
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
    for (var i = 0; i < $scope.MonthlyPayslipDetailsList.length; i++) {
        var entity = $scope.MonthlyPayslipDetailsList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedMonthlyPayslipDetailsUrl
        ,deleteMonthlyPayslipDetailsByIdUrl
        ,getMonthlyPayslipDetailsListDataExtraUrl
        ,saveMonthlyPayslipDetailsListUrl
        ,getMonthlyPayslipDetailsByIdUrl
        ,getMonthlyPayslipDetailsDataExtraUrl
        ,saveMonthlyPayslipDetailsUrl
        ) {
        $scope.getPagedMonthlyPayslipDetailsUrl = getPagedMonthlyPayslipDetailsUrl;
        $scope.deleteMonthlyPayslipDetailsByIdUrl = deleteMonthlyPayslipDetailsByIdUrl;
        /*bind extra url if need*/
        $scope.getMonthlyPayslipDetailsListDataExtraUrl = getMonthlyPayslipDetailsListDataExtraUrl;
        $scope.saveMonthlyPayslipDetailsListUrl = saveMonthlyPayslipDetailsListUrl;
        $scope.getMonthlyPayslipDetailsByIdUrl = getMonthlyPayslipDetailsByIdUrl;
        $scope.getMonthlyPayslipDetailsDataExtraUrl = getMonthlyPayslipDetailsDataExtraUrl;
        $scope.saveMonthlyPayslipDetailsUrl = saveMonthlyPayslipDetailsUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



