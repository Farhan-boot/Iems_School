
//File:Inventory_ItemMaintainance List Anjular  Controller
emsApp.controller('ItemMaintainanceListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.ItemMaintainanceList = [];
    $scope.SelectedItemMaintainance = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
      $scope.getItemMaintainanceListExtraData();
      $scope.getPagedItemMaintainanceList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedItemMaintainanceList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedItemMaintainanceList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedItemMaintainanceList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedItemMaintainanceList();
    };
    $scope.searchItemMaintainanceList = function () {
        $scope.getPagedItemMaintainanceList();
    };
    $scope.getPagedItemMaintainanceList = function () {
        $scope.getItemMaintainanceList();
    };
    /*For Search on data end*/
    $scope.getItemMaintainanceList = function () {
        $http.get($scope.getPagedItemMaintainanceUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.ItemMaintainanceList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Item Maintainance list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Item Maintainance list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getItemMaintainanceListExtraData= function()
    {
            $http.get($scope.getItemMaintainanceListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                  //DropDown Option Tables
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Item Maintainance! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Item Maintainance! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllItemMaintainanceList = function () {
        $scope.IsLoading++;
        $http.get($scope.getItemMaintainanceListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.ItemMaintainanceList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Item Maintainance list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Item Maintainance list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteItemMaintainanceById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteItemMaintainanceByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.ItemMaintainanceList.indexOf(obj);
                        $scope.ItemMaintainanceList.splice(i, 1);
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
    /*$scope.deleteItemMaintainanceByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteItemMaintainanceByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.ItemMaintainanceList.indexOf(obj);
                        $scope.ItemMaintainanceList.splice(i, 1);
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
    for (var i = 0; i < $scope.ItemMaintainanceList.length; i++) {
        var entity = $scope.ItemMaintainanceList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedItemMaintainanceUrl
        ,deleteItemMaintainanceByIdUrl
        ,getItemMaintainanceListDataExtraUrl
        ,saveItemMaintainanceListUrl
        ,getItemMaintainanceByIdUrl
        ,getItemMaintainanceDataExtraUrl
        ,saveItemMaintainanceUrl
        ) {
        $scope.getPagedItemMaintainanceUrl = getPagedItemMaintainanceUrl;
        $scope.deleteItemMaintainanceByIdUrl = deleteItemMaintainanceByIdUrl;
        /*bind extra url if need*/
        $scope.getItemMaintainanceListDataExtraUrl = getItemMaintainanceListDataExtraUrl;
        $scope.saveItemMaintainanceListUrl = saveItemMaintainanceListUrl;
        $scope.getItemMaintainanceByIdUrl = getItemMaintainanceByIdUrl;
        $scope.getItemMaintainanceDataExtraUrl = getItemMaintainanceDataExtraUrl;
        $scope.saveItemMaintainanceUrl = saveItemMaintainanceUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



