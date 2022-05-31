
//File: Inventory Anjular  Controller

emsApp.controller('RequisitionAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.Requisition = {}
    $scope.RequisitionInformation = {}
    $scope.RequisitionId = 0;
    $scope.HasViewPermission = true;

    $scope.loadPage = function () {
        $scope.getRequisitionById(0);
        $scope.loadRequisitionDataExtra(0);
    };

    $scope.getRequisitionById = function (RequisitionId) {
        if (RequisitionId != null && RequisitionId !== '')
            $scope.RequisitionId = $scope.RequisitionId;
        else return;

        $http.get($scope.getRequisitionByIdUrl, {
            params: { "id": $scope.RequisitionId }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.IsSelect2Open = false;
                $scope.IsShowDeleteUnDeleteMessage = false;
                $scope.RequisitionInformation = result.Data;

                console.log($scope.RequisitionInformation);
                updateUrlQuery('id', $scope.RequisitionInformation.Id);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Requisition! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Requisition! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.addNewRequisitionDtl = function (inventoryDtlRow) {
        //console.log("Data :" + productDtlRow);
        var RequisitionDtl = angular.copy($scope.Requisition);
        $scope.RequisitionInformation.RequisitionDetailsJson.push(RequisitionDtl);
    }

    $scope.delete = function (row) {
        var index = $scope.RequisitionInformation.RequisitionDetailsJson.indexOf(row);
        $scope.RequisitionInformation.RequisitionDetailsJson.splice(index, 1);
    }

    // $scope.PurchaseId
    $scope.loadRequisitionDataExtra = function (RequisitionId) {
        if (RequisitionId != null)
            $scope.RequisitionId = RequisitionId;

        $http.get($scope.getRequisitionDataExtraUrl, {
            params: { "id": $scope.RequisitionId }
        }).success(function (result) {
            if (!result.HasError) {

                $scope.HasError = false;
                if (result.DataExtra.ItemList != null)
                    $scope.ItemList = result.DataExtra.ItemList;
                if (result.DataExtra.UserList != null)
                    $scope.UserList = result.DataExtra.UserList;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Item List! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Item List! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
      };




    ///*Save Requisition Information*/
    $scope.saveRequisition = function () {
        
        $http.post($scope.saveRequisitionUrl + "/", $scope.RequisitionInformation).
            success(function (result) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    if (result.Data != null) {
                        $scope.RequisitionInformation = result.Data;
                    }
                    alertSuccess("Successfully saved Requisition Information data!");
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to save Requisition Information! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to save Requisition Information! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
      };


    //======Custom Variabales goes hare=====

    $scope.Init = function (RequisitionId, getRequisitionByIdUrl, getRequisitionDataExtraUrl, saveRequisitionUrl) {
        //console.log("result " +getItemInformationUrl);
        $scope.RequisitionId = RequisitionId;
        $scope.getRequisitionByIdUrl = getRequisitionByIdUrl;
        $scope.getRequisitionDataExtraUrl = getRequisitionDataExtraUrl;
        $scope.saveRequisitionUrl = saveRequisitionUrl;

        //console.log($scope.getProductCategoryByIdUrl);
        //$scope.saveItemInformationUrl = saveItemInformationUrl;
        //$scope.PurchaseId = PurchaseId;
        //$scope.savePurchaseUrl = savePurchaseUrl;
       // $scope.ItemInformationId = ItemInformationId;
        $scope.loadPage();
    };

    

});

