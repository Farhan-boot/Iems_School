
//File:Inventory_InventoryDetails List Anjular  Controller
emsApp.controller('InventoryDetailsListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.InventoryDetailsList = [];
    $scope.SelectedInventoryDetails = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
     $scope.SelectedInventoryId=0;      
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
      $scope.getInventoryDetailsListExtraData();
      $scope.getPagedInventoryDetailsList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedInventoryDetailsList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedInventoryDetailsList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedInventoryDetailsList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedInventoryDetailsList();
    };
    $scope.searchInventoryDetailsList = function () {
        $scope.getPagedInventoryDetailsList();
    };
    $scope.getPagedInventoryDetailsList = function () {
        $scope.getInventoryDetailsList();
    };
    /*For Search on data end*/
    $scope.getInventoryDetailsList = function () {
        $http.get($scope.getPagedInventoryDetailsUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
           ,"inventoryId": $scope.SelectedInventoryId      
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.InventoryDetailsList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Inventory Details list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Inventory Details list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getInventoryDetailsListExtraData= function()
    {
            $http.get($scope.getInventoryDetailsListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.StatusEnumList!=null)
                         $scope.StatusEnumList = result.DataExtra.StatusEnumList;
                  //DropDown Option Tables
                     if(result.DataExtra.InventoryList!=null)
                       $scope.InventoryList =  result.DataExtra.InventoryList; 

                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Inventory Details! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Inventory Details! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllInventoryDetailsList = function () {
        $scope.IsLoading++;
        $http.get($scope.getInventoryDetailsListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.InventoryDetailsList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Inventory Details list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Inventory Details list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteInventoryDetailsById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteInventoryDetailsByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.InventoryDetailsList.indexOf(obj);
                        $scope.InventoryDetailsList.splice(i, 1);
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
    /*$scope.deleteInventoryDetailsByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteInventoryDetailsByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.InventoryDetailsList.indexOf(obj);
                        $scope.InventoryDetailsList.splice(i, 1);
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
    for (var i = 0; i < $scope.InventoryDetailsList.length; i++) {
        var entity = $scope.InventoryDetailsList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedInventoryDetailsUrl
        ,deleteInventoryDetailsByIdUrl
        ,getInventoryDetailsListDataExtraUrl
        ,saveInventoryDetailsListUrl
        ,getInventoryDetailsByIdUrl
        ,getInventoryDetailsDataExtraUrl
        ,saveInventoryDetailsUrl
        ) {
        $scope.getPagedInventoryDetailsUrl = getPagedInventoryDetailsUrl;
        $scope.deleteInventoryDetailsByIdUrl = deleteInventoryDetailsByIdUrl;
        /*bind extra url if need*/
        $scope.getInventoryDetailsListDataExtraUrl = getInventoryDetailsListDataExtraUrl;
        $scope.saveInventoryDetailsListUrl = saveInventoryDetailsListUrl;
        $scope.getInventoryDetailsByIdUrl = getInventoryDetailsByIdUrl;
        $scope.getInventoryDetailsDataExtraUrl = getInventoryDetailsDataExtraUrl;
        $scope.saveInventoryDetailsUrl = saveInventoryDetailsUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



