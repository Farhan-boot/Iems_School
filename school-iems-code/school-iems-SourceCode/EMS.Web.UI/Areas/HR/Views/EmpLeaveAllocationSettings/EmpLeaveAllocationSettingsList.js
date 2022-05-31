
//File:HR_EmpLeaveAllocationSettings List Anjular  Controller
emsApp.controller('EmpLeaveAllocationSettingsListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.EmpLeaveAllocationSettingsList = [];
    $scope.SelectedEmpLeaveAllocationSettings = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
     $scope.SelectedEmployeeId=0;      
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
      $scope.getEmpLeaveAllocationSettingsListExtraData();
      $scope.getPagedEmpLeaveAllocationSettingsList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedEmpLeaveAllocationSettingsList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedEmpLeaveAllocationSettingsList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedEmpLeaveAllocationSettingsList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedEmpLeaveAllocationSettingsList();
    };
    $scope.searchEmpLeaveAllocationSettingsList = function () {
        $scope.getPagedEmpLeaveAllocationSettingsList();
    };
    $scope.getPagedEmpLeaveAllocationSettingsList = function () {
        $scope.getEmpLeaveAllocationSettingsList();
    };
    /*For Search on data end*/
    $scope.getEmpLeaveAllocationSettingsList = function () {
        $http.get($scope.getPagedEmpLeaveAllocationSettingsUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
           ,"employeeId": $scope.SelectedEmployeeId      
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.EmpLeaveAllocationSettingsList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Emp Leave Allocation Settings list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Emp Leave Allocation Settings list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getEmpLeaveAllocationSettingsListExtraData= function()
    {
            $http.get($scope.getEmpLeaveAllocationSettingsListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                  //DropDown Option Tables
                     if(result.DataExtra.EmployeeList!=null)
                       $scope.EmployeeList =  result.DataExtra.EmployeeList; 

                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Emp Leave Allocation Settings! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Emp Leave Allocation Settings! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllEmpLeaveAllocationSettingsList = function () {
        $scope.IsLoading++;
        $http.get($scope.getEmpLeaveAllocationSettingsListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.EmpLeaveAllocationSettingsList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Emp Leave Allocation Settings list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Emp Leave Allocation Settings list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteEmpLeaveAllocationSettingsById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteEmpLeaveAllocationSettingsByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.EmpLeaveAllocationSettingsList.indexOf(obj);
                        $scope.EmpLeaveAllocationSettingsList.splice(i, 1);
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
    /*$scope.deleteEmpLeaveAllocationSettingsByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteEmpLeaveAllocationSettingsByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.EmpLeaveAllocationSettingsList.indexOf(obj);
                        $scope.EmpLeaveAllocationSettingsList.splice(i, 1);
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
    for (var i = 0; i < $scope.EmpLeaveAllocationSettingsList.length; i++) {
        var entity = $scope.EmpLeaveAllocationSettingsList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedEmpLeaveAllocationSettingsUrl
        ,deleteEmpLeaveAllocationSettingsByIdUrl
        ,getEmpLeaveAllocationSettingsListDataExtraUrl
        ,saveEmpLeaveAllocationSettingsListUrl
        ,getEmpLeaveAllocationSettingsByIdUrl
        ,getEmpLeaveAllocationSettingsDataExtraUrl
        ,saveEmpLeaveAllocationSettingsUrl
        ) {
        $scope.getPagedEmpLeaveAllocationSettingsUrl = getPagedEmpLeaveAllocationSettingsUrl;
        $scope.deleteEmpLeaveAllocationSettingsByIdUrl = deleteEmpLeaveAllocationSettingsByIdUrl;
        /*bind extra url if need*/
        $scope.getEmpLeaveAllocationSettingsListDataExtraUrl = getEmpLeaveAllocationSettingsListDataExtraUrl;
        $scope.saveEmpLeaveAllocationSettingsListUrl = saveEmpLeaveAllocationSettingsListUrl;
        $scope.getEmpLeaveAllocationSettingsByIdUrl = getEmpLeaveAllocationSettingsByIdUrl;
        $scope.getEmpLeaveAllocationSettingsDataExtraUrl = getEmpLeaveAllocationSettingsDataExtraUrl;
        $scope.saveEmpLeaveAllocationSettingsUrl = saveEmpLeaveAllocationSettingsUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



