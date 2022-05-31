
//File: Product Card Anjular  Controller

emsApp.controller('ProductCardAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.Product = {}
    $scope.PurchaseInformation = {}

    $scope.loadPage = function () {
      
        $scope.getItemInformationById(0);
    };

    $scope.getItemInformationById = function (ItemInformationId) {
        if (ItemInformationId != null && ItemInformationId !== '')
            $scope.ItemInformationId = ItemInformationId;
        else return;

        $http.get($scope.getItemInformationUrl, {
            params: { "id": $scope.ItemInformationId }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.IsSelect2Open = false;
                $scope.IsShowDeleteUnDeleteMessage = false;
                $scope.PurchaseInformation = result.DataExtra.PurchaseInformation;
                $scope.Product = result.DataExtra.Product;
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
        console.log("Data :" + productDtlRow)
        var ProductDtl = angular.copy($scope.Product);
        $scope.PurchaseInformation.ProductList.push(ProductDtl);
    }

    $scope.delete = function (row) {
        var index = $scope.PurchaseInformation.ProductList.indexOf(row);
        $scope.PurchaseInformation.ProductList.splice(index, 1);
    }




    /*Save Product Information*/
    $scope.saveProductInformation = function () {
        
        $http.post($scope.saveItemInformationUrl + "/", $scope.PurchaseInformation).
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

    $scope.Init = function (getItemInformationUrl, saveItemInformationUrl) {

        //console.log("result " +getItemInformationUrl);

        $scope.getItemInformationUrl = getItemInformationUrl;
        $scope.saveItemInformationUrl = saveItemInformationUrl;
       // $scope.ItemInformationId = ItemInformationId;
        $scope.loadPage();
    };

    

});

