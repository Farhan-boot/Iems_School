
//File: Product Card Anjular  Controller

emsApp.controller('ProductCategoryDetailCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.PurchaseDetailList = [];
    $scope.SelectedPurchaseDetail = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
    $scope.IsShowTrashedItems = false;
    $scope.PurchaseHistoryOf = [];
    $scope.ProductDetail = {}
    $scope.PurchaseDetailInformation = {}

    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.PurchaseId = 0;

    $scope.loadPage = function () {
        $scope.getProductDetail();
        $scope.loadPurchaseDetailDataExtra(0);
    };

    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedPurchaseDetailList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedPurchaseDetailList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedPurchaseDetailList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedPurchaseDetailList();
    };

    $scope.searchPurchaseDetailList = function () {
        $scope.getPagedPurchaseDetailList();
    };
    $scope.getPagedPurchaseDetailList = function () {
        $scope.getProductDetail();
    };

    /*For Search on data end*/
    $scope.getProductDetail = function () {
        $http.get($scope.getProductCardDetailUrl, {
            params: {
                "textkey": $scope.SearchText
                , "pageSize": $scope.PageSize
                , "pageNo": $scope.PageNo
                , "PurchaseId": 0//$scope.SelectedPurchaseId
                , "ItemId": $scope.SelectedItemId
                , "WarhouseId": $scope.SelectedWarhouseId
                , "formDate": $scope.SelectedFormDate
                , "toDate": $scope.SelectedToDate
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.ProductDetail = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
               
                console.log($scope.ProductDetail);

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






    $scope.loadPurchaseDetailDataExtra = function (PurchaseId) {
        if (PurchaseId != null)
            $scope.PurchaseId = PurchaseId;

        $http.get($scope.getPurchaseDetailDataExtraUrl, {
            params: { "id": $scope.PurchaseId }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.DataExtra.ItemList != null)
                    $scope.ItemList = result.DataExtra.ItemList;
                if (result.DataExtra.WarhouseList != null)
                    $scope.WarhouseList = result.DataExtra.WarhouseList;
                if (result.DataExtra.SupplierList != null)
                    $scope.SupplierList = result.DataExtra.SupplierList;
                //difolt value
                //if (result.DataExtra.FormDate != null)
                //    $scope.SelectedFormDate = result.DataExtra.FormDate;
                //if (result.DataExtra.ToDate != null)
                //    $scope.SelectedToDate = result.DataExtra.ToDate;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Purchase! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Purchase! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };






    




    //======Custom Variabales goes hare=====

    $scope.Init = function (getProductCardDetailUrl, getPurchaseDetailDataExtraUrl) {
        $scope.getProductCardDetailUrl = getProductCardDetailUrl;
        $scope.getPurchaseDetailDataExtraUrl = getPurchaseDetailDataExtraUrl;

        $scope.loadPage();
    };


});

