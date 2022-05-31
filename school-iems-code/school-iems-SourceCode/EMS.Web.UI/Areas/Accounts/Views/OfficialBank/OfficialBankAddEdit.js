
//File: Acnt_OfficialBank Anjular  Controller
emsApp.controller('OfficialBankAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.OfficialBank = [];
    $scope.OfficialBankId = 0;
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
    $scope.loadPage = function (OfficialBankId) {
        if (OfficialBankId != null)
            $scope.OfficialBankId = OfficialBankId;

        $scope.loadOfficialBankDataExtra($scope.OfficialBankId);
        $scope.getOfficialBankById($scope.OfficialBankId);
    };
    $scope.getNewOfficialBank = function () {
        $scope.getOfficialBankById(0);
    };
    $scope.getOfficialBankById = function (OfficialBankId) {
        if (OfficialBankId != null && OfficialBankId !== '')
            $scope.OfficialBankId = OfficialBankId;
        else return;

        $http.get($scope.getOfficialBankByIdUrl, {
            params: { "id": $scope.OfficialBankId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.OfficialBank = result.Data;
                updateUrlQuery('id', $scope.OfficialBank.Id);
                $scope.onAfterGetOfficialBankById(result);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Official Bank! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Official Bank! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.loadOfficialBankDataExtra = function (OfficialBankId) {
        if (OfficialBankId != null)
            $scope.OfficialBankId = OfficialBankId;

        $http.get($scope.getOfficialBankDataExtraUrl, {
            params: { "id": $scope.OfficialBankId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.DataExtra.TypeEnumList != null)
                    $scope.TypeEnumList = result.DataExtra.TypeEnumList;
                $scope.onAfterLoadOfficialBankDataExtra(result);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Official Bank! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Official Bank! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.saveOfficialBank = function () {
        if (!$scope.validateOfficialBank()) {
            return;
        }
        $http.post($scope.saveOfficialBankUrl + "/", $scope.OfficialBank).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    if (result.Data != null) {
                        $scope.OfficialBank = result.Data;
                        updateUrlQuery('id', $scope.OfficialBank.Id);
                    }
                    alertSuccess("Successfully saved Official Bank data!");
                    $scope.onAfterSaveOfficialBank(result);
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to save Official Bank! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to save Official Bank! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    };

    $scope.deleteOfficialBankById = function () {

    };
    $scope.validateOfficialBank = function () {
        return true;
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variabales goes hare=====

    $scope.Init = function (OfficialBankId, getOfficialBankByIdUrl, getOfficialBankDataExtraUrl, saveOfficialBankUrl, deleteOfficialBankByIdUrl) {
        $scope.OfficialBankId = OfficialBankId;
        $scope.getOfficialBankByIdUrl = getOfficialBankByIdUrl;
        $scope.saveOfficialBankUrl = saveOfficialBankUrl;
        $scope.getOfficialBankDataExtraUrl = getOfficialBankDataExtraUrl;
        $scope.deleteOfficialBankByIdUrl = deleteOfficialBankByIdUrl;

        $scope.loadPage(OfficialBankId);
    };

    $scope.onAfterSaveOfficialBank = function (result) {
        //var data=result.Data;
    };
    $scope.onAfterGetOfficialBankById = function (result) {
        //var data=result.Data;

    };
    $scope.onAfterLoadOfficialBankDataExtra = function (result) {
        //var DataExtra=result.DataExtra;
        //DropDown Option Tables
        /*
        //Child Table Bindings, need to fix     */
    };
    //======Other tabale's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 
});

