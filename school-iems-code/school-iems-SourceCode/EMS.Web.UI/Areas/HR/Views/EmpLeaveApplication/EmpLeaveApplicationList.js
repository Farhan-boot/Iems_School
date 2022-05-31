
//File:HR_EmpLeaveApplication List Anjular  Controller
emsApp.controller('EmpLeaveApplicationListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.EmpLeaveApplicationList = [];
    $scope.SelectedEmpLeaveApplication = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
     $scope.SelectedLeaveAllocationSettingsId=0;      
     $scope.SelectedEmployeeId=0;      
     $scope.SelectedRecommendByEmployeeId=0;      
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
      $scope.getEmpLeaveApplicationListExtraData();
      $scope.getPagedEmpLeaveApplicationList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedEmpLeaveApplicationList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedEmpLeaveApplicationList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedEmpLeaveApplicationList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedEmpLeaveApplicationList();
    };
    $scope.searchEmpLeaveApplicationList = function () {
        $scope.getPagedEmpLeaveApplicationList();
    };
    $scope.getPagedEmpLeaveApplicationList = function () {
        $scope.getEmpLeaveApplicationList();
    };
    /*For Search on data end*/
    $scope.getEmpLeaveApplicationList = function () {
        $http.get($scope.getPagedEmpLeaveApplicationUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
           ,"leaveAllocationSettingsId": $scope.SelectedLeaveAllocationSettingsId      
           ,"employeeId": $scope.SelectedEmployeeId      
           ,"recommendByEmployeeId": $scope.SelectedRecommendByEmployeeId      
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.EmpLeaveApplicationList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Emp Leave Application list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Emp Leave Application list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getEmpLeaveApplicationListExtraData= function()
    {
            $http.get($scope.getEmpLeaveApplicationListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.LeaveTypeEnumList!=null)
                         $scope.LeaveTypeEnumList = result.DataExtra.LeaveTypeEnumList;
                      if(result.DataExtra.StatusEnumList!=null)
                         $scope.StatusEnumList = result.DataExtra.StatusEnumList;
                  //DropDown Option Tables
                     if(result.DataExtra.EmpLeaveAllocationSettingsList!=null)
                       $scope.EmpLeaveAllocationSettingsList =  result.DataExtra.EmpLeaveAllocationSettingsList; 

                     if(result.DataExtra.EmployeeList!=null)
                       $scope.EmployeeList =  result.DataExtra.EmployeeList; 

                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Emp Leave Application! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Emp Leave Application! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllEmpLeaveApplicationList = function () {
        $scope.IsLoading++;
        $http.get($scope.getEmpLeaveApplicationListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.EmpLeaveApplicationList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Emp Leave Application list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Emp Leave Application list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteEmpLeaveApplicationById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteEmpLeaveApplicationByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.EmpLeaveApplicationList.indexOf(obj);
                        $scope.EmpLeaveApplicationList.splice(i, 1);
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
    /*$scope.deleteEmpLeaveApplicationByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteEmpLeaveApplicationByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.EmpLeaveApplicationList.indexOf(obj);
                        $scope.EmpLeaveApplicationList.splice(i, 1);
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
    for (var i = 0; i < $scope.EmpLeaveApplicationList.length; i++) {
        var entity = $scope.EmpLeaveApplicationList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedEmpLeaveApplicationUrl
        ,deleteEmpLeaveApplicationByIdUrl
        ,getEmpLeaveApplicationListDataExtraUrl
        ,saveEmpLeaveApplicationListUrl
        ,getEmpLeaveApplicationByIdUrl
        ,getEmpLeaveApplicationDataExtraUrl
        ,saveEmpLeaveApplicationUrl
        ) {
        $scope.getPagedEmpLeaveApplicationUrl = getPagedEmpLeaveApplicationUrl;
        $scope.deleteEmpLeaveApplicationByIdUrl = deleteEmpLeaveApplicationByIdUrl;
        /*bind extra url if need*/
        $scope.getEmpLeaveApplicationListDataExtraUrl = getEmpLeaveApplicationListDataExtraUrl;
        $scope.saveEmpLeaveApplicationListUrl = saveEmpLeaveApplicationListUrl;
        $scope.getEmpLeaveApplicationByIdUrl = getEmpLeaveApplicationByIdUrl;
        $scope.getEmpLeaveApplicationDataExtraUrl = getEmpLeaveApplicationDataExtraUrl;
        $scope.saveEmpLeaveApplicationUrl = saveEmpLeaveApplicationUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



