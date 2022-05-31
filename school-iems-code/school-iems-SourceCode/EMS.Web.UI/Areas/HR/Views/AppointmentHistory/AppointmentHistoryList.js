
//File:HR_AppointmentHistory List Anjular  Controller
emsApp.controller('AppointmentHistoryListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.AppointmentHistoryList = [];
    $scope.SelectedAppointmentHistory = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
     $scope.SelectedDepartmentId=0;      
     $scope.SelectedPositionId=0;      
     $scope.SelectedEmployeeId=0;      
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
      $scope.getAppointmentHistoryListExtraData();
      $scope.getPagedAppointmentHistoryList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedAppointmentHistoryList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedAppointmentHistoryList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedAppointmentHistoryList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedAppointmentHistoryList();
    };
    $scope.searchAppointmentHistoryList = function () {
        $scope.getPagedAppointmentHistoryList();
    };
    $scope.getPagedAppointmentHistoryList = function () {
        $scope.getAppointmentHistoryList();
    };
    /*For Search on data end*/
    $scope.getAppointmentHistoryList = function () {
        $http.get($scope.getPagedAppointmentHistoryUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
           ,"departmentId": $scope.SelectedDepartmentId      
           ,"positionId": $scope.SelectedPositionId      
           ,"employeeId": $scope.SelectedEmployeeId      
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.AppointmentHistoryList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Appointment History list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Appointment History list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getAppointmentHistoryListExtraData= function()
    {
            $http.get($scope.getAppointmentHistoryListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.HistoryTypeEnumList!=null)
                         $scope.HistoryTypeEnumList = result.DataExtra.HistoryTypeEnumList;
                  //DropDown Option Tables
                     if(result.DataExtra.DepartmentList!=null)
                       $scope.DepartmentList =  result.DataExtra.DepartmentList; 

                     if(result.DataExtra.PositionList!=null)
                       $scope.PositionList =  result.DataExtra.PositionList; 

                     if(result.DataExtra.EmployeeList!=null)
                       $scope.EmployeeList =  result.DataExtra.EmployeeList; 

                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Appointment History! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Appointment History! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllAppointmentHistoryList = function () {
        $scope.IsLoading++;
        $http.get($scope.getAppointmentHistoryListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.AppointmentHistoryList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Appointment History list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Appointment History list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteAppointmentHistoryById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteAppointmentHistoryByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.AppointmentHistoryList.indexOf(obj);
                        $scope.AppointmentHistoryList.splice(i, 1);
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
    /*$scope.deleteAppointmentHistoryByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteAppointmentHistoryByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.AppointmentHistoryList.indexOf(obj);
                        $scope.AppointmentHistoryList.splice(i, 1);
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
    for (var i = 0; i < $scope.AppointmentHistoryList.length; i++) {
        var entity = $scope.AppointmentHistoryList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedAppointmentHistoryUrl
        ,deleteAppointmentHistoryByIdUrl
        ,getAppointmentHistoryListDataExtraUrl
        ,saveAppointmentHistoryListUrl
        ,getAppointmentHistoryByIdUrl
        ,getAppointmentHistoryDataExtraUrl
        ,saveAppointmentHistoryUrl
        ) {
        $scope.getPagedAppointmentHistoryUrl = getPagedAppointmentHistoryUrl;
        $scope.deleteAppointmentHistoryByIdUrl = deleteAppointmentHistoryByIdUrl;
        /*bind extra url if need*/
        $scope.getAppointmentHistoryListDataExtraUrl = getAppointmentHistoryListDataExtraUrl;
        $scope.saveAppointmentHistoryListUrl = saveAppointmentHistoryListUrl;
        $scope.getAppointmentHistoryByIdUrl = getAppointmentHistoryByIdUrl;
        $scope.getAppointmentHistoryDataExtraUrl = getAppointmentHistoryDataExtraUrl;
        $scope.saveAppointmentHistoryUrl = saveAppointmentHistoryUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



