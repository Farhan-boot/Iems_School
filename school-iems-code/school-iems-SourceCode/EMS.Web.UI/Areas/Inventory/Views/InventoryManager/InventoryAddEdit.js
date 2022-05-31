
//File: Inventory Anjular  Controller

emsApp.controller('InventoryAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.Inventory = {}
    $scope.InventoryInformation = {}
    $scope.InventoryId = 0;
    $scope.HasViewPermission = true;

    $scope.loadPage = function () {
        $scope.getInventoryById(0);
        $scope.loadInventoryDataExtra(0);
    };

    $scope.getInventoryById = function (InventoryId) {
        if (InventoryId != null && InventoryId !== '')
            $scope.InventoryId = $scope.InventoryId;
        else return;

        $http.get($scope.getInventoryByIdUrl, {
            params: { "id": $scope.InventoryId }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.IsSelect2Open = false;
                $scope.IsShowDeleteUnDeleteMessage = false;
                $scope.InventoryInformation = result.Data;
               
                updateUrlQuery('id', $scope.InventoryInformation.Id);
                console.log($scope.InventoryInformation);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Inventory! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Inventory! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.addNewInventoryDtl = function (inventoryDtlRow) {
        //console.log("Data :" + productDtlRow);
        var InventoryDtl = angular.copy($scope.Inventory);
        $scope.InventoryInformation.InventoryDetailsJson.push(InventoryDtl);
    }

    $scope.delete = function (row) {
        var index = $scope.InventoryInformation.InventoryDetailsJson.indexOf(row);
        $scope.InventoryInformation.InventoryDetailsJson.splice(index, 1);
    }

    // $scope.PurchaseId
    $scope.loadInventoryDataExtra = function (InventoryId) {
        if (InventoryId != null)
            $scope.InventoryId = InventoryId;

        $http.get($scope.getInventoryDataExtraUrl, {
            params: { "id": $scope.InventoryId }
        }).success(function (result) {
            if (!result.HasError) {

                $scope.HasError = false;
                if (result.DataExtra.SupplierList != null)
                    $scope.SupplierList = result.DataExtra.SupplierList;
                if (result.DataExtra.UserList != null)
                    $scope.UserList = result.DataExtra.UserList;
                //New Data Extra
                if (result.DataExtra.StoreList != null)
                    $scope.StoreList = result.DataExtra.StoreList;
                if (result.DataExtra.ItemList != null)
                    $scope.ItemList = result.DataExtra.ItemList;
                if (result.DataExtra.CategoryList != null)
                    $scope.CategoryList = result.DataExtra.CategoryList;
                if (result.DataExtra.StatusList != null)
                    $scope.StatusList = result.DataExtra.StatusList;

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
    $scope.saveInventory = function () {
        
        $http.post($scope.saveInventoryUrl + "/", $scope.InventoryInformation).
            success(function (result) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    if (result.Data != null) {
                        $scope.InventoryInformation = result.Data;
                        updateUrlQuery('id', $scope.InventoryInformation.Id);
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

    $scope.Init = function (InventoryId, getInventoryByIdUrl, getInventoryDataExtraUrl, saveInventoryUrl) {
        //console.log("result " +getItemInformationUrl);
        $scope.InventoryId = InventoryId;
        $scope.getInventoryByIdUrl = getInventoryByIdUrl;
        $scope.getInventoryDataExtraUrl = getInventoryDataExtraUrl;
        $scope.saveInventoryUrl = saveInventoryUrl;

        //console.log($scope.getProductCategoryByIdUrl);
        //$scope.saveItemInformationUrl = saveItemInformationUrl;
        //$scope.PurchaseId = PurchaseId;
        //$scope.savePurchaseUrl = savePurchaseUrl;
       // $scope.ItemInformationId = ItemInformationId;
        $scope.loadPage();
    };

    

});

