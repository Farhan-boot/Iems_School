
//File:HR_Department List Anjular  Controller
emsApp.controller('DepartmentListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.DepartmentList = [];
    $scope.SelectedDepartment = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
     $scope.SelectedOrgId=0;      
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
      $scope.getDepartmentListExtraData();
      $scope.getPagedDepartmentList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedDepartmentList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedDepartmentList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedDepartmentList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedDepartmentList();
    };
    $scope.searchDepartmentList = function () {
        $scope.getPagedDepartmentList();
    };
    $scope.getPagedDepartmentList = function () {
        $scope.getDepartmentList();
    };
    /*For Search on data end*/
    $scope.getDepartmentList = function () {
        $http.get($scope.getPagedDepartmentUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
           ,"orgId": $scope.SelectedOrgId      
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.DepartmentList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Department list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Department list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getDepartmentListExtraData= function()
    {
            $http.get($scope.getDepartmentListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.TypeEnumList!=null)
                         $scope.TypeEnumList = result.DataExtra.TypeEnumList;
                      if(result.DataExtra.StatusEnumList!=null)
                         $scope.StatusEnumList = result.DataExtra.StatusEnumList;
                  //DropDown Option Tables
                     if(result.DataExtra.OrganizationList!=null)
                       $scope.OrganizationList =  result.DataExtra.OrganizationList; 

                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Department! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Department! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllDepartmentList = function () {
        $scope.IsLoading++;
        $http.get($scope.getDepartmentListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.DepartmentList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Department list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Department list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteDepartmentById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteDepartmentByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.DepartmentList.indexOf(obj);
                        $scope.DepartmentList.splice(i, 1);
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
    /*$scope.deleteDepartmentByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteDepartmentByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.DepartmentList.indexOf(obj);
                        $scope.DepartmentList.splice(i, 1);
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
    for (var i = 0; i < $scope.DepartmentList.length; i++) {
        var entity = $scope.DepartmentList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedDepartmentUrl
        ,deleteDepartmentByIdUrl
        ,getDepartmentListDataExtraUrl
        ,saveDepartmentListUrl
        ,getDepartmentByIdUrl
        ,getDepartmentDataExtraUrl
        ,saveDepartmentUrl
        ) {
        $scope.getPagedDepartmentUrl = getPagedDepartmentUrl;
        $scope.deleteDepartmentByIdUrl = deleteDepartmentByIdUrl;
        /*bind extra url if need*/
        $scope.getDepartmentListDataExtraUrl = getDepartmentListDataExtraUrl;
        $scope.saveDepartmentListUrl = saveDepartmentListUrl;
        $scope.getDepartmentByIdUrl = getDepartmentByIdUrl;
        $scope.getDepartmentDataExtraUrl = getDepartmentDataExtraUrl;
        $scope.saveDepartmentUrl = saveDepartmentUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



