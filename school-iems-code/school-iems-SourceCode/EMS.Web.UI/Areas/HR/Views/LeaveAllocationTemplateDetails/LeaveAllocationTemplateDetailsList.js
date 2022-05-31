
//File:HR_LeaveAllocationTemplateDetails List Anjular  Controller
emsApp.controller('LeaveAllocationTemplateDetailsListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.LeaveAllocationTemplateDetailsList = [];
    $scope.SelectedLeaveAllocationTemplateDetails = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
     $scope.SelectedSalaryTemplateId=0;      
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
      $scope.getLeaveAllocationTemplateDetailsListExtraData();
      $scope.getPagedLeaveAllocationTemplateDetailsList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedLeaveAllocationTemplateDetailsList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedLeaveAllocationTemplateDetailsList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedLeaveAllocationTemplateDetailsList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedLeaveAllocationTemplateDetailsList();
    };
    $scope.searchLeaveAllocationTemplateDetailsList = function () {
        $scope.getPagedLeaveAllocationTemplateDetailsList();
    };
    $scope.getPagedLeaveAllocationTemplateDetailsList = function () {
        $scope.getLeaveAllocationTemplateDetailsList();
    };
    /*For Search on data end*/
    $scope.getLeaveAllocationTemplateDetailsList = function () {
        $http.get($scope.getPagedLeaveAllocationTemplateDetailsUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
           ,"salaryTemplateId": $scope.SelectedSalaryTemplateId      
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.LeaveAllocationTemplateDetailsList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Leave Allocation Template Details list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Leave Allocation Template Details list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getLeaveAllocationTemplateDetailsListExtraData= function()
    {
            $http.get($scope.getLeaveAllocationTemplateDetailsListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.LeaveTypeEnumList!=null)
                         $scope.LeaveTypeEnumList = result.DataExtra.LeaveTypeEnumList;
                  //DropDown Option Tables
                     if(result.DataExtra.SalaryTemplateList!=null)
                       $scope.SalaryTemplateList =  result.DataExtra.SalaryTemplateList; 

                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Leave Allocation Template Details! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Leave Allocation Template Details! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllLeaveAllocationTemplateDetailsList = function () {
        $scope.IsLoading++;
        $http.get($scope.getLeaveAllocationTemplateDetailsListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.LeaveAllocationTemplateDetailsList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Leave Allocation Template Details list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Leave Allocation Template Details list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteLeaveAllocationTemplateDetailsById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteLeaveAllocationTemplateDetailsByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.LeaveAllocationTemplateDetailsList.indexOf(obj);
                        $scope.LeaveAllocationTemplateDetailsList.splice(i, 1);
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
    /*$scope.deleteLeaveAllocationTemplateDetailsByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteLeaveAllocationTemplateDetailsByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.LeaveAllocationTemplateDetailsList.indexOf(obj);
                        $scope.LeaveAllocationTemplateDetailsList.splice(i, 1);
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
    for (var i = 0; i < $scope.LeaveAllocationTemplateDetailsList.length; i++) {
        var entity = $scope.LeaveAllocationTemplateDetailsList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedLeaveAllocationTemplateDetailsUrl
        ,deleteLeaveAllocationTemplateDetailsByIdUrl
        ,getLeaveAllocationTemplateDetailsListDataExtraUrl
        ,saveLeaveAllocationTemplateDetailsListUrl
        ,getLeaveAllocationTemplateDetailsByIdUrl
        ,getLeaveAllocationTemplateDetailsDataExtraUrl
        ,saveLeaveAllocationTemplateDetailsUrl
        ) {
        $scope.getPagedLeaveAllocationTemplateDetailsUrl = getPagedLeaveAllocationTemplateDetailsUrl;
        $scope.deleteLeaveAllocationTemplateDetailsByIdUrl = deleteLeaveAllocationTemplateDetailsByIdUrl;
        /*bind extra url if need*/
        $scope.getLeaveAllocationTemplateDetailsListDataExtraUrl = getLeaveAllocationTemplateDetailsListDataExtraUrl;
        $scope.saveLeaveAllocationTemplateDetailsListUrl = saveLeaveAllocationTemplateDetailsListUrl;
        $scope.getLeaveAllocationTemplateDetailsByIdUrl = getLeaveAllocationTemplateDetailsByIdUrl;
        $scope.getLeaveAllocationTemplateDetailsDataExtraUrl = getLeaveAllocationTemplateDetailsDataExtraUrl;
        $scope.saveLeaveAllocationTemplateDetailsUrl = saveLeaveAllocationTemplateDetailsUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



