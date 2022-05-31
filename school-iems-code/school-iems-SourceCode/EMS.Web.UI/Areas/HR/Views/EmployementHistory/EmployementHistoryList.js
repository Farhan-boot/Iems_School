
//File:HR_EmployementHistory List Anjular  Controller
emsApp.controller('EmployementHistoryListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.EmployementHistoryList = [];
    $scope.SelectedEmployementHistory = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
     $scope.SelectedDepartmentId=0;      
     $scope.SelectedEmployeeId=0;      
     $scope.SelectedRankId=0;      
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
      $scope.getEmployementHistoryListExtraData();
      $scope.getPagedEmployementHistoryList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedEmployementHistoryList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedEmployementHistoryList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedEmployementHistoryList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedEmployementHistoryList();
    };
    $scope.searchEmployementHistoryList = function () {
        $scope.getPagedEmployementHistoryList();
    };
    $scope.getPagedEmployementHistoryList = function () {
        $scope.getEmployementHistoryList();
    };
    /*For Search on data end*/
    $scope.getEmployementHistoryList = function () {
        $http.get($scope.getPagedEmployementHistoryUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
           ,"departmentId": $scope.SelectedDepartmentId      
           ,"employeeId": $scope.SelectedEmployeeId      
           ,"rankId": $scope.SelectedRankId      
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.EmployementHistoryList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Employement History list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Employement History list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getEmployementHistoryListExtraData= function()
    {
            $http.get($scope.getEmployementHistoryListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.EmployementTypeEnumList!=null)
                         $scope.EmployementTypeEnumList = result.DataExtra.EmployementTypeEnumList;
                      if(result.DataExtra.JobTypeEnumList!=null)
                         $scope.JobTypeEnumList = result.DataExtra.JobTypeEnumList;
                      if(result.DataExtra.HistoryTypeEnumList!=null)
                         $scope.HistoryTypeEnumList = result.DataExtra.HistoryTypeEnumList;
                  //DropDown Option Tables
                     if(result.DataExtra.DepartmentList!=null)
                       $scope.DepartmentList =  result.DataExtra.DepartmentList; 

                     if(result.DataExtra.EmployeeList!=null)
                       $scope.EmployeeList =  result.DataExtra.EmployeeList; 

                     if(result.DataExtra.RankList!=null)
                       $scope.RankList =  result.DataExtra.RankList; 

                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Employement History! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Employement History! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllEmployementHistoryList = function () {
        $scope.IsLoading++;
        $http.get($scope.getEmployementHistoryListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.EmployementHistoryList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Employement History list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Employement History list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteEmployementHistoryById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteEmployementHistoryByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.EmployementHistoryList.indexOf(obj);
                        $scope.EmployementHistoryList.splice(i, 1);
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
    /*$scope.deleteEmployementHistoryByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteEmployementHistoryByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.EmployementHistoryList.indexOf(obj);
                        $scope.EmployementHistoryList.splice(i, 1);
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
    for (var i = 0; i < $scope.EmployementHistoryList.length; i++) {
        var entity = $scope.EmployementHistoryList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedEmployementHistoryUrl
        ,deleteEmployementHistoryByIdUrl
        ,getEmployementHistoryListDataExtraUrl
        ,saveEmployementHistoryListUrl
        ,getEmployementHistoryByIdUrl
        ,getEmployementHistoryDataExtraUrl
        ,saveEmployementHistoryUrl
        ) {
        $scope.getPagedEmployementHistoryUrl = getPagedEmployementHistoryUrl;
        $scope.deleteEmployementHistoryByIdUrl = deleteEmployementHistoryByIdUrl;
        /*bind extra url if need*/
        $scope.getEmployementHistoryListDataExtraUrl = getEmployementHistoryListDataExtraUrl;
        $scope.saveEmployementHistoryListUrl = saveEmployementHistoryListUrl;
        $scope.getEmployementHistoryByIdUrl = getEmployementHistoryByIdUrl;
        $scope.getEmployementHistoryDataExtraUrl = getEmployementHistoryDataExtraUrl;
        $scope.saveEmployementHistoryUrl = saveEmployementHistoryUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



