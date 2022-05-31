
//File:HR_EmpLeaveAllocationSettingsDetails List Anjular  Controller
emsApp.controller('EmpLeaveAllocationSettingsDetailsListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.EmpLeaveAllocationSettingsDetailsList = [];
    $scope.SelectedEmpLeaveAllocationSettingsDetails = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
     $scope.SelectedEmpLeaveAllocationSettingsId=0;      
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
      $scope.getEmpLeaveAllocationSettingsDetailsListExtraData();
      $scope.getPagedEmpLeaveAllocationSettingsDetailsList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedEmpLeaveAllocationSettingsDetailsList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedEmpLeaveAllocationSettingsDetailsList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedEmpLeaveAllocationSettingsDetailsList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedEmpLeaveAllocationSettingsDetailsList();
    };
    $scope.searchEmpLeaveAllocationSettingsDetailsList = function () {
        $scope.getPagedEmpLeaveAllocationSettingsDetailsList();
    };
    $scope.getPagedEmpLeaveAllocationSettingsDetailsList = function () {
        $scope.getEmpLeaveAllocationSettingsDetailsList();
    };
    /*For Search on data end*/
    $scope.getEmpLeaveAllocationSettingsDetailsList = function () {
        $http.get($scope.getPagedEmpLeaveAllocationSettingsDetailsUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
           ,"empLeaveAllocationSettingsId": $scope.SelectedEmpLeaveAllocationSettingsId      
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.EmpLeaveAllocationSettingsDetailsList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Emp Leave Allocation Settings Details list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Emp Leave Allocation Settings Details list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getEmpLeaveAllocationSettingsDetailsListExtraData= function()
    {
            $http.get($scope.getEmpLeaveAllocationSettingsDetailsListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.LeaveTypeEnumList!=null)
                         $scope.LeaveTypeEnumList = result.DataExtra.LeaveTypeEnumList;
                  //DropDown Option Tables
                     if(result.DataExtra.EmpLeaveAllocationSettingsList!=null)
                       $scope.EmpLeaveAllocationSettingsList =  result.DataExtra.EmpLeaveAllocationSettingsList; 

                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Emp Leave Allocation Settings Details! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Emp Leave Allocation Settings Details! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllEmpLeaveAllocationSettingsDetailsList = function () {
        $scope.IsLoading++;
        $http.get($scope.getEmpLeaveAllocationSettingsDetailsListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.EmpLeaveAllocationSettingsDetailsList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Emp Leave Allocation Settings Details list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Emp Leave Allocation Settings Details list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteEmpLeaveAllocationSettingsDetailsById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteEmpLeaveAllocationSettingsDetailsByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.EmpLeaveAllocationSettingsDetailsList.indexOf(obj);
                        $scope.EmpLeaveAllocationSettingsDetailsList.splice(i, 1);
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
    /*$scope.deleteEmpLeaveAllocationSettingsDetailsByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteEmpLeaveAllocationSettingsDetailsByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.EmpLeaveAllocationSettingsDetailsList.indexOf(obj);
                        $scope.EmpLeaveAllocationSettingsDetailsList.splice(i, 1);
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
    for (var i = 0; i < $scope.EmpLeaveAllocationSettingsDetailsList.length; i++) {
        var entity = $scope.EmpLeaveAllocationSettingsDetailsList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedEmpLeaveAllocationSettingsDetailsUrl
        ,deleteEmpLeaveAllocationSettingsDetailsByIdUrl
        ,getEmpLeaveAllocationSettingsDetailsListDataExtraUrl
        ,saveEmpLeaveAllocationSettingsDetailsListUrl
        ,getEmpLeaveAllocationSettingsDetailsByIdUrl
        ,getEmpLeaveAllocationSettingsDetailsDataExtraUrl
        ,saveEmpLeaveAllocationSettingsDetailsUrl
        ) {
        $scope.getPagedEmpLeaveAllocationSettingsDetailsUrl = getPagedEmpLeaveAllocationSettingsDetailsUrl;
        $scope.deleteEmpLeaveAllocationSettingsDetailsByIdUrl = deleteEmpLeaveAllocationSettingsDetailsByIdUrl;
        /*bind extra url if need*/
        $scope.getEmpLeaveAllocationSettingsDetailsListDataExtraUrl = getEmpLeaveAllocationSettingsDetailsListDataExtraUrl;
        $scope.saveEmpLeaveAllocationSettingsDetailsListUrl = saveEmpLeaveAllocationSettingsDetailsListUrl;
        $scope.getEmpLeaveAllocationSettingsDetailsByIdUrl = getEmpLeaveAllocationSettingsDetailsByIdUrl;
        $scope.getEmpLeaveAllocationSettingsDetailsDataExtraUrl = getEmpLeaveAllocationSettingsDetailsDataExtraUrl;
        $scope.saveEmpLeaveAllocationSettingsDetailsUrl = saveEmpLeaveAllocationSettingsDetailsUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



