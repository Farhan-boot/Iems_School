
//File:Inventory_RequestedItem List Anjular  Controller
emsApp.controller('RequestedItemListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.RequestedItemList = [];
    $scope.SelectedRequestedItem = [];
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
        $scope.getRequestedItemListExtraData();
        $scope.getPagedRequestedItemList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedRequestedItemList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedRequestedItemList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedRequestedItemList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedRequestedItemList();
    };
    $scope.searchRequestedItemList = function () {
        $scope.getPagedRequestedItemList();
    };
    $scope.getPagedRequestedItemList = function () {
        $scope.getRequestedItemList();
    };
    /*For Search on data end*/
    $scope.getRequestedItemList = function () {
        $http.get($scope.getPagedRequestedItemUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.RequestedItemList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Requested Item list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Requested Item list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getRequestedItemListExtraData= function()
    {
        $http.get($scope.getRequestedItemListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                  //DropDown Option Tables
                } else {
                 $scope.HasError= true;
                    $scope.ErrorMsg ="Unable to load option data for Requested Item! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                $scope.ErrorMsg ="Unable to load option data for Requested Item! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllRequestedItemList = function () {
        $scope.IsLoading++;
        $http.get($scope.getRequestedItemListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.RequestedItemList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Requested Item list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Requested Item list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteRequestedItemById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteRequestedItemByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.RequestedItemList.indexOf(obj);
                        $scope.RequestedItemList.splice(i, 1);
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
        for (var i = 0; i < $scope.RequestedItemList.length; i++) {
            var entity = $scope.RequestedItemList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedRequestedItemUrl
        ,deleteRequestedItemByIdUrl
        ,getRequestedItemListDataExtraUrl
        ,saveRequestedItemListUrl
        ,getRequestedItemByIdUrl
        ,getRequestedItemDataExtraUrl
        ,saveRequestedItemUrl
        ) {
        $scope.getPagedRequestedItemUrl = getPagedRequestedItemUrl;
        $scope.deleteRequestedItemByIdUrl = deleteRequestedItemByIdUrl;
        /*bind extra url if need*/
        $scope.getRequestedItemListDataExtraUrl = getRequestedItemListDataExtraUrl;
        $scope.saveRequestedItemListUrl = saveRequestedItemListUrl;
        $scope.getRequestedItemByIdUrl = getRequestedItemByIdUrl;
        $scope.getRequestedItemDataExtraUrl = getRequestedItemDataExtraUrl;
        $scope.saveRequestedItemUrl = saveRequestedItemUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



