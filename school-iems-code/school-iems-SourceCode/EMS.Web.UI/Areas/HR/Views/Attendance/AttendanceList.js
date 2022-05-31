
//File:HR_Attendance List Anjular  Controller
emsApp.controller('AttendanceListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.AttendanceList = [];
    $scope.SelectedAttendance = [];
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
      $scope.getAttendanceListExtraData();
      $scope.getPagedAttendanceList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedAttendanceList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedAttendanceList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedAttendanceList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedAttendanceList();
    };
    $scope.searchAttendanceList = function () {
        $scope.getPagedAttendanceList();
    };
    $scope.getPagedAttendanceList = function () {
        $scope.getAttendanceList();
    };
    /*For Search on data end*/
    $scope.getAttendanceList = function () {
        $http.get($scope.getPagedAttendanceUrl, {
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
                $scope.AttendanceList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Attendance list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Attendance list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getAttendanceListExtraData= function()
    {
            $http.get($scope.getAttendanceListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.PunchTypeEnumList!=null)
                         $scope.PunchTypeEnumList = result.DataExtra.PunchTypeEnumList;
                      if(result.DataExtra.PunchModeEnumList!=null)
                         $scope.PunchModeEnumList = result.DataExtra.PunchModeEnumList;
                  //DropDown Option Tables
                     if(result.DataExtra.EmployeeList!=null)
                       $scope.EmployeeList =  result.DataExtra.EmployeeList; 

                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Attendance! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Attendance! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllAttendanceList = function () {
        $scope.IsLoading++;
        $http.get($scope.getAttendanceListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.AttendanceList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Attendance list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Attendance list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteAttendanceById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteAttendanceByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.AttendanceList.indexOf(obj);
                        $scope.AttendanceList.splice(i, 1);
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
    /*$scope.deleteAttendanceByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteAttendanceByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.AttendanceList.indexOf(obj);
                        $scope.AttendanceList.splice(i, 1);
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
    for (var i = 0; i < $scope.AttendanceList.length; i++) {
        var entity = $scope.AttendanceList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedAttendanceUrl
        ,deleteAttendanceByIdUrl
        ,getAttendanceListDataExtraUrl
        ,saveAttendanceListUrl
        ,getAttendanceByIdUrl
        ,getAttendanceDataExtraUrl
        ,saveAttendanceUrl
        ) {
        $scope.getPagedAttendanceUrl = getPagedAttendanceUrl;
        $scope.deleteAttendanceByIdUrl = deleteAttendanceByIdUrl;
        /*bind extra url if need*/
        $scope.getAttendanceListDataExtraUrl = getAttendanceListDataExtraUrl;
        $scope.saveAttendanceListUrl = saveAttendanceListUrl;
        $scope.getAttendanceByIdUrl = getAttendanceByIdUrl;
        $scope.getAttendanceDataExtraUrl = getAttendanceDataExtraUrl;
        $scope.saveAttendanceUrl = saveAttendanceUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



