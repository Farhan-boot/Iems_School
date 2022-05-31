
//File:Inventory_Inventory List Anjular  Controller
emsApp.controller('InventoryListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.InventoryList = [];
    $scope.SelectedInventory = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
     $scope.SelectedSupplierId=0;      
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
      $scope.getInventoryListExtraData();
      $scope.getPagedInventoryList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedInventoryList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedInventoryList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedInventoryList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedInventoryList();
    };
    $scope.searchInventoryList = function () {
        $scope.getPagedInventoryList();
    };
    $scope.getPagedInventoryList = function () {
        $scope.getInventoryList();
    };
    /*For Search on data end*/
    $scope.getInventoryList = function () {
        $http.get($scope.getPagedInventoryUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
           ,"supplierId": $scope.SelectedSupplierId      
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.InventoryList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Inventory list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Inventory list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getInventoryListExtraData= function()
    {
            $http.get($scope.getInventoryListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                  //DropDown Option Tables
                     if(result.DataExtra.SupplierList!=null)
                       $scope.SupplierList =  result.DataExtra.SupplierList; 

                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Inventory! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Inventory! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllInventoryList = function () {
        $scope.IsLoading++;
        $http.get($scope.getInventoryListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.InventoryList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Inventory list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Inventory list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteInventoryById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteInventoryByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.InventoryList.indexOf(obj);
                        $scope.InventoryList.splice(i, 1);
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
    /*$scope.deleteInventoryByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteInventoryByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.InventoryList.indexOf(obj);
                        $scope.InventoryList.splice(i, 1);
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
    for (var i = 0; i < $scope.InventoryList.length; i++) {
        var entity = $scope.InventoryList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedInventoryUrl
        ,deleteInventoryByIdUrl
        ,getInventoryListDataExtraUrl
        ,saveInventoryListUrl
        ,getInventoryByIdUrl
        ,getInventoryDataExtraUrl
        ,saveInventoryUrl
        ) {
        $scope.getPagedInventoryUrl = getPagedInventoryUrl;
        $scope.deleteInventoryByIdUrl = deleteInventoryByIdUrl;
        /*bind extra url if need*/
        $scope.getInventoryListDataExtraUrl = getInventoryListDataExtraUrl;
        $scope.saveInventoryListUrl = saveInventoryListUrl;
        $scope.getInventoryByIdUrl = getInventoryByIdUrl;
        $scope.getInventoryDataExtraUrl = getInventoryDataExtraUrl;
        $scope.saveInventoryUrl = saveInventoryUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



