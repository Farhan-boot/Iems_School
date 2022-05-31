
//File: Product Category Anjular  Controller

emsApp.controller('ProductCategoryAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.Product = {}
    $scope.ProductInformation = {}
    $scope.ProductId = 0;
    $scope.HasViewPermission = true;

    $scope.loadPage = function () {
        $scope.getProductCategoryById(0);
        $scope.loadProductCategoryDataExtra(0);
    };

    $scope.getProductCategoryById = function (ItemInformationId) {
        if (ItemInformationId != null && ItemInformationId !== '')
            $scope.ItemInformationId = $scope.PurchaseId;
        else return;

        $http.get($scope.getProductCategoryByIdUrl, {
            params: { "id": $scope.ProductCategoryId }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.IsSelect2Open = false;
                $scope.IsShowDeleteUnDeleteMessage = false;
                $scope.ProductCategoryInformation = result.Data;
               
                updateUrlQuery('id', $scope.ProductCategoryInformation.Id);
                //console.log($scope.ItemInformation);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Voucher! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Voucher! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.addNewProductDtl = function (productDtlRow) {
        //console.log("Data :" + productDtlRow);
        var ProductDtl = angular.copy($scope.Product);
        $scope.ProductCategoryInformation.ProductCategoryDetailsJson.push(ProductDtl);
    }

    $scope.delete = function (row) {
        var index = $scope.ProductCategoryInformation.ProductCategoryDetailsJson.indexOf(row);
        $scope.ProductCategoryInformation.ProductCategoryDetailsJson.splice(index, 1);
    }

    // $scope.PurchaseId
    $scope.loadProductCategoryDataExtra = function (ProductCategoryId) {
        if (ProductCategoryId != null)
            $scope.ProductCategoryId = ProductCategoryId;

        $http.get($scope.getProductCategoryDataExtraUrl, {
            params: { "id": $scope.ProductCategoryId }
        }).success(function (result) {
            if (!result.HasError) {

                $scope.HasError = false;
                if (result.DataExtra.AssetTypeList != null)
                    $scope.AssetTypeList = result.DataExtra.AssetTypeList;
                if (result.DataExtra.UnitTypeEnumList != null)
                    $scope.UnitTypeEnumList = result.DataExtra.UnitTypeEnumList;
                if (result.DataExtra.StoreList != null)
                    $scope.StoreList = result.DataExtra.StoreList;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Asset Type! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Asset Type! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
      };




    ///*Save Purchase Information*/
    $scope.saveProductCategory = function () {
        
        $http.post($scope.saveProductCategoryUrl + "/", $scope.ProductCategoryInformation).
            success(function (result) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    if (result.Data != null) {
                        $scope.PurchaseInformation = result.Data; 
                    }
                    alertSuccess("Successfully saved Purchase Information data!");
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to save Purchase Information! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to save Purchase Information! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
      };


    //======Custom Variabales goes hare=====

    $scope.Init = function (ProductCategoryId, getProductCategoryByIdUrl, getProductCategoryDataExtraUrl, saveProductCategoryUrl) {
        //console.log("result " +getItemInformationUrl);
        $scope.ProductCategoryId = ProductCategoryId;
        $scope.getProductCategoryByIdUrl = getProductCategoryByIdUrl;
        $scope.getProductCategoryDataExtraUrl = getProductCategoryDataExtraUrl;
        $scope.saveProductCategoryUrl = saveProductCategoryUrl;

        //console.log($scope.getProductCategoryByIdUrl);
        //$scope.saveItemInformationUrl = saveItemInformationUrl;
        //$scope.PurchaseId = PurchaseId;
        //$scope.savePurchaseUrl = savePurchaseUrl;
       // $scope.ItemInformationId = ItemInformationId;
        $scope.loadPage();
    };

    

});

