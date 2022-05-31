
//File:Inventory_ItemDelivery List Anjular  Controller
emsApp.controller('ItemDeliveryListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.ItemDeliveryList = [];
    $scope.SelectedItemDelivery = [];
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
        $scope.getItemDeliveryListExtraData();
        $scope.getPagedItemDeliveryList();
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedItemDeliveryList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedItemDeliveryList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedItemDeliveryList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedItemDeliveryList();
    };
    $scope.searchItemDeliveryList = function () {
        $scope.getPagedItemDeliveryList();
    };
    $scope.getPagedItemDeliveryList = function () {
        $scope.getItemDeliveryList();
    };
    /*For Search on data end*/
    $scope.getItemDeliveryList = function () {
        $http.get($scope.getPagedItemDeliveryUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.ItemDeliveryList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Item Delivery list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Item Delivery list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getItemDeliveryListExtraData= function()
    {
        $http.get($scope.getItemDeliveryListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                  //DropDown Option Tables
                } else {
                 $scope.HasError= true;
                    $scope.ErrorMsg ="Unable to load option data for Item Delivery! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                $scope.ErrorMsg ="Unable to load option data for Item Delivery! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllItemDeliveryList = function () {
        $scope.IsLoading++;
        $http.get($scope.getItemDeliveryListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.ItemDeliveryList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Item Delivery list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Item Delivery list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteItemDeliveryById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteItemDeliveryByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.ItemDeliveryList.indexOf(obj);
                        $scope.ItemDeliveryList.splice(i, 1);
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
    /*$scope.deleteStoreByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteStoreByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.StoreList.indexOf(obj);
                        $scope.StoreList.splice(i, 1);
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
        for (var i = 0; i < $scope.ItemDeliveryList.length; i++) {
            var entity = $scope.ItemDeliveryList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedItemDeliveryUrl
        ,deleteItemDeliveryByIdUrl
        ,getItemDeliveryListDataExtraUrl
        ,saveItemDeliveryListUrl
        ,getItemDeliveryByIdUrl
        ,getItemDeliveryDataExtraUrl
        ,saveItemDeliveryUrl
        ) {
        $scope.getPagedItemDeliveryUrl = getPagedItemDeliveryUrl;
        $scope.deleteItemDeliveryByIdUrl = deleteItemDeliveryByIdUrl;
        /*bind extra url if need*/
        $scope.getItemDeliveryListDataExtraUrl = getItemDeliveryListDataExtraUrl;
        $scope.saveItemDeliveryListUrl = saveItemDeliveryListUrl;
        $scope.getItemDeliveryByIdUrl = getItemDeliveryByIdUrl;
        $scope.getItemDeliveryDataExtraUrl = getItemDeliveryDataExtraUrl;
        $scope.saveItemDeliveryUrl = saveItemDeliveryUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



