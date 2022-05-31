
//File:Admin_Purchase List Anjular  Controller
emsApp.controller('PurchaseListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.PurchaseList = [];
    $scope.SelectedPurchase = [];
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
      $scope.getPurchaseListExtraData();
      $scope.getPagedPurchaseList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedPurchaseList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedPurchaseList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedPurchaseList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedPurchaseList();
    };
    $scope.searchPurchaseList = function () {
        $scope.getPagedPurchaseList();
    };
    $scope.getPagedPurchaseList = function () {
        $scope.getPurchaseList();
    };
    /*For Search on data end*/
    $scope.getPurchaseList = function () {
        $http.get($scope.getPagedPurchaseUrl, {
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
                $scope.PurchaseList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Purchase list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Purchase list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getPurchaseListExtraData= function()
    {
            $http.get($scope.getPurchaseListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                  //DropDown Option Tables
                     if(result.DataExtra.SupplierList!=null)
                       $scope.SupplierList =  result.DataExtra.SupplierList; 

                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Purchase! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Purchase! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllPurchaseList = function () {
        $scope.IsLoading++;
        $http.get($scope.getPurchaseListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.PurchaseList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Purchase list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Purchase list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deletePurchaseById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deletePurchaseByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.PurchaseList.indexOf(obj);
                        $scope.PurchaseList.splice(i, 1);
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
    /*$scope.deletePurchaseByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deletePurchaseByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.PurchaseList.indexOf(obj);
                        $scope.PurchaseList.splice(i, 1);
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
    for (var i = 0; i < $scope.PurchaseList.length; i++) {
        var entity = $scope.PurchaseList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedPurchaseUrl
        ,deletePurchaseByIdUrl
        ,getPurchaseListDataExtraUrl
        ,savePurchaseListUrl
        ,getPurchaseByIdUrl
        ,getPurchaseDataExtraUrl
        ,savePurchaseUrl
        ) {
        $scope.getPagedPurchaseUrl = getPagedPurchaseUrl;
        $scope.deletePurchaseByIdUrl = deletePurchaseByIdUrl;
        /*bind extra url if need*/
        $scope.getPurchaseListDataExtraUrl = getPurchaseListDataExtraUrl;
        $scope.savePurchaseListUrl = savePurchaseListUrl;
        $scope.getPurchaseByIdUrl = getPurchaseByIdUrl;
        $scope.getPurchaseDataExtraUrl = getPurchaseDataExtraUrl;
        $scope.savePurchaseUrl = savePurchaseUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



