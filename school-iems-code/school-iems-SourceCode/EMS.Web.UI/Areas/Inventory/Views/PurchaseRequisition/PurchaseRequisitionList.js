
//File:Inventory_PurchaseRequisition List Anjular  Controller
emsApp.controller('PurchaseRequisitionListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.PurchaseRequisitionList = [];
    $scope.SelectedPurchaseRequisition = [];
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
      $scope.getPurchaseRequisitionListExtraData();
      $scope.getPagedPurchaseRequisitionList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedPurchaseRequisitionList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedPurchaseRequisitionList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedPurchaseRequisitionList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedPurchaseRequisitionList();
    };
    $scope.searchPurchaseRequisitionList = function () {
        $scope.getPagedPurchaseRequisitionList();
    };
    $scope.getPagedPurchaseRequisitionList = function () {
        $scope.getPurchaseRequisitionList();
    };
    /*For Search on data end*/
    $scope.getPurchaseRequisitionList = function () {
        $http.get($scope.getPagedPurchaseRequisitionUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.PurchaseRequisitionList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Purchase Requisition list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Purchase Requisition list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getPurchaseRequisitionListExtraData= function()
    {
            $http.get($scope.getPurchaseRequisitionListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                  //DropDown Option Tables
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Purchase Requisition! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Purchase Requisition! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllPurchaseRequisitionList = function () {
        $scope.IsLoading++;
        $http.get($scope.getPurchaseRequisitionListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.PurchaseRequisitionList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Purchase Requisition list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Purchase Requisition list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deletePurchaseRequisitionById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deletePurchaseRequisitionByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.PurchaseRequisitionList.indexOf(obj);
                        $scope.PurchaseRequisitionList.splice(i, 1);
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
    /*$scope.deletePurchaseRequisitionByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deletePurchaseRequisitionByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.PurchaseRequisitionList.indexOf(obj);
                        $scope.PurchaseRequisitionList.splice(i, 1);
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
    for (var i = 0; i < $scope.PurchaseRequisitionList.length; i++) {
        var entity = $scope.PurchaseRequisitionList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedPurchaseRequisitionUrl
        ,deletePurchaseRequisitionByIdUrl
        ,getPurchaseRequisitionListDataExtraUrl
        ,savePurchaseRequisitionListUrl
        ,getPurchaseRequisitionByIdUrl
        ,getPurchaseRequisitionDataExtraUrl
        ,savePurchaseRequisitionUrl
        ) {
        $scope.getPagedPurchaseRequisitionUrl = getPagedPurchaseRequisitionUrl;
        $scope.deletePurchaseRequisitionByIdUrl = deletePurchaseRequisitionByIdUrl;
        /*bind extra url if need*/
        $scope.getPurchaseRequisitionListDataExtraUrl = getPurchaseRequisitionListDataExtraUrl;
        $scope.savePurchaseRequisitionListUrl = savePurchaseRequisitionListUrl;
        $scope.getPurchaseRequisitionByIdUrl = getPurchaseRequisitionByIdUrl;
        $scope.getPurchaseRequisitionDataExtraUrl = getPurchaseRequisitionDataExtraUrl;
        $scope.savePurchaseRequisitionUrl = savePurchaseRequisitionUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



