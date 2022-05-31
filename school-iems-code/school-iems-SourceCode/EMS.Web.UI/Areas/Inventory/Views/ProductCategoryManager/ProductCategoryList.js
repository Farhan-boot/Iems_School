
//File:Inventory_ProductCategory List Anjular  Controller
emsApp.controller('ProductCategoryListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.ProductCategoryList = [];
    $scope.SelectedProductCategory = [];
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
        $scope.getProductCategoryListExtraData();
        $scope.getPagedProductCategoryList();

    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedProductCategoryList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedProductCategoryList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedProductCategoryList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedProductCategoryList();
    };
    $scope.searchProductCategoryList = function () {
        $scope.getPagedProductCategoryList();
    };
    $scope.getPagedProductCategoryList = function () {
        $scope.getProductCategoryList();
    };
    /*For Search on data end*/
    $scope.getProductCategoryList = function () {
        $http.get($scope.getPagedProductCategoryUrl, {
            params: {
                "textkey": $scope.SearchText
                , "pageSize": $scope.PageSize
                , "pageNo": $scope.PageNo
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.ProductCategoryList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Product Category list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Product Category list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getProductCategoryListExtraData = function () {
        $http.get($scope.getProductCategoryListDataExtraUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.DataExtra.AssetTypeEnumList != null)
                    $scope.AssetTypeEnumList = result.DataExtra.AssetTypeEnumList;
                //DropDown Option Tables
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Product Category! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Product Category! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllProductCategoryList = function () {
        $scope.IsLoading++;
        $http.get($scope.getProductCategoryListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.ProductCategoryList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Product Category list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Product Category list! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.deleteProductCategoryById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteProductCategoryByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.ProductCategoryList.indexOf(obj);
                        $scope.ProductCategoryList.splice(i, 1);
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
    /*$scope.deleteProductCategoryByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteProductCategoryByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.ProductCategoryList.indexOf(obj);
                        $scope.ProductCategoryList.splice(i, 1);
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
        for (var i = 0; i < $scope.ProductCategoryList.length; i++) {
            var entity = $scope.ProductCategoryList[i];
            entity.IsSelected = checkbox.checked;
        }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====


    $scope.Init = function (
        getPagedProductCategoryUrl
        , deleteProductCategoryByIdUrl
        , getProductCategoryListDataExtraUrl
        , saveProductCategoryListUrl
        , getProductCategoryByIdUrl
        , getProductCategoryDataExtraUrl
        , saveProductCategoryUrl
    ) {
        $scope.getPagedProductCategoryUrl = getPagedProductCategoryUrl;
        $scope.deleteProductCategoryByIdUrl = deleteProductCategoryByIdUrl;
        /*bind extra url if need*/
        $scope.getProductCategoryListDataExtraUrl = getProductCategoryListDataExtraUrl;
        $scope.saveProductCategoryListUrl = saveProductCategoryListUrl;
        $scope.getProductCategoryByIdUrl = getProductCategoryByIdUrl;
        $scope.getProductCategoryDataExtraUrl = getProductCategoryDataExtraUrl;
        $scope.saveProductCategoryUrl = saveProductCategoryUrl;


        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



