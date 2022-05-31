
//File:Inventory_PurchaseRequisitionDetail List Anjular  Controller
emsApp.controller('PurchaseRequisitionDetailListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.PurchaseRequisitionDetailList = [];
    $scope.SelectedPurchaseRequisitionDetail = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
     $scope.SelectedProductCategoryId=0;      
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
      $scope.getPurchaseRequisitionDetailListExtraData();
      $scope.getPagedPurchaseRequisitionDetailList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedPurchaseRequisitionDetailList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedPurchaseRequisitionDetailList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedPurchaseRequisitionDetailList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedPurchaseRequisitionDetailList();
    };
    $scope.searchPurchaseRequisitionDetailList = function () {
        $scope.getPagedPurchaseRequisitionDetailList();
    };
    $scope.getPagedPurchaseRequisitionDetailList = function () {
        $scope.getPurchaseRequisitionDetailList();
    };
    /*For Search on data end*/
    $scope.getPurchaseRequisitionDetailList = function () {
        $http.get($scope.getPagedPurchaseRequisitionDetailUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
           ,"productCategoryId": $scope.SelectedProductCategoryId      
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.PurchaseRequisitionDetailList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Purchase Requisition Detail list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Purchase Requisition Detail list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getPurchaseRequisitionDetailListExtraData= function()
    {
            $http.get($scope.getPurchaseRequisitionDetailListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.UnitTypeEnumList!=null)
                         $scope.UnitTypeEnumList = result.DataExtra.UnitTypeEnumList;
                  //DropDown Option Tables
                     if(result.DataExtra.ProductCategoryList!=null)
                       $scope.ProductCategoryList =  result.DataExtra.ProductCategoryList; 

                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Purchase Requisition Detail! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Purchase Requisition Detail! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllPurchaseRequisitionDetailList = function () {
        $scope.IsLoading++;
        $http.get($scope.getPurchaseRequisitionDetailListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.PurchaseRequisitionDetailList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Purchase Requisition Detail list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Purchase Requisition Detail list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deletePurchaseRequisitionDetailById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deletePurchaseRequisitionDetailByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.PurchaseRequisitionDetailList.indexOf(obj);
                        $scope.PurchaseRequisitionDetailList.splice(i, 1);
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
    /*$scope.deletePurchaseRequisitionDetailByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deletePurchaseRequisitionDetailByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.PurchaseRequisitionDetailList.indexOf(obj);
                        $scope.PurchaseRequisitionDetailList.splice(i, 1);
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
    for (var i = 0; i < $scope.PurchaseRequisitionDetailList.length; i++) {
        var entity = $scope.PurchaseRequisitionDetailList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedPurchaseRequisitionDetailUrl
        ,deletePurchaseRequisitionDetailByIdUrl
        ,getPurchaseRequisitionDetailListDataExtraUrl
        ,savePurchaseRequisitionDetailListUrl
        ,getPurchaseRequisitionDetailByIdUrl
        ,getPurchaseRequisitionDetailDataExtraUrl
        ,savePurchaseRequisitionDetailUrl
        ) {
        $scope.getPagedPurchaseRequisitionDetailUrl = getPagedPurchaseRequisitionDetailUrl;
        $scope.deletePurchaseRequisitionDetailByIdUrl = deletePurchaseRequisitionDetailByIdUrl;
        /*bind extra url if need*/
        $scope.getPurchaseRequisitionDetailListDataExtraUrl = getPurchaseRequisitionDetailListDataExtraUrl;
        $scope.savePurchaseRequisitionDetailListUrl = savePurchaseRequisitionDetailListUrl;
        $scope.getPurchaseRequisitionDetailByIdUrl = getPurchaseRequisitionDetailByIdUrl;
        $scope.getPurchaseRequisitionDetailDataExtraUrl = getPurchaseRequisitionDetailDataExtraUrl;
        $scope.savePurchaseRequisitionDetailUrl = savePurchaseRequisitionDetailUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



