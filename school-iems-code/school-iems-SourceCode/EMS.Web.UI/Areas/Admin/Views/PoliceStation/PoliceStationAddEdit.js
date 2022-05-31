
//File: General_PoliceStation Anjular  Controller
emsApp.controller('PoliceStationAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.PoliceStation = [];
    $scope.PoliceStationId = 0;
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
    $scope.loadPage = function (PoliceStationId) {
        if (PoliceStationId != null)
            $scope.PoliceStationId = PoliceStationId;

        $scope.loadPoliceStationDataExtra($scope.PoliceStationId);
        $scope.getPoliceStationById($scope.PoliceStationId);
    };
    $scope.getNewPoliceStation = function () {
        $scope.getPoliceStationById(0);
    };
    $scope.getPoliceStationById = function (PoliceStationId) {
        if (PoliceStationId != null && PoliceStationId !== '')
            $scope.PoliceStationId = PoliceStationId;
        else return;

        $http.get($scope.getPoliceStationByIdUrl, {
            params: { "id": $scope.PoliceStationId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.PoliceStation = result.Data;
                updateUrlQuery('id', $scope.PoliceStation.Id);
                $scope.onAfterGetPoliceStationById(result);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Police Station! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Police Station! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.loadPoliceStationDataExtra = function (PoliceStationId) {
        if (PoliceStationId != null)
            $scope.PoliceStationId = PoliceStationId;

        $http.get($scope.getPoliceStationDataExtraUrl, {
            params: { "id": $scope.PoliceStationId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.onAfterLoadPoliceStationDataExtra(result);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Police Station! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Police Station! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.savePoliceStation = function () {
        if (!$scope.validatePoliceStation()) {
            return;
        }
        $http.post($scope.savePoliceStationUrl + "/", $scope.PoliceStation).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    if (result.Data != null) {
                        $scope.PoliceStation = result.Data;
                        updateUrlQuery('id', $scope.PoliceStation.Id);
                    }
                    alertSuccess("Successfully saved Police Station data!");
                    $scope.onAfterSavePoliceStation(result);
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to save Police Station! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to save Police Station! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    };

    $scope.deletePoliceStationById = function () {

    };
    $scope.validatePoliceStation = function () {
        return true;
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variabales goes hare=====

    $scope.Init = function (PoliceStationId, getPoliceStationByIdUrl, getPoliceStationDataExtraUrl, savePoliceStationUrl, deletePoliceStationByIdUrl) {
        $scope.PoliceStationId = PoliceStationId;
        $scope.getPoliceStationByIdUrl = getPoliceStationByIdUrl;
        $scope.savePoliceStationUrl = savePoliceStationUrl;
        $scope.getPoliceStationDataExtraUrl = getPoliceStationDataExtraUrl;
        $scope.deletePoliceStationByIdUrl = deletePoliceStationByIdUrl;

        $scope.loadPage(PoliceStationId);
    };

    $scope.onAfterSavePoliceStation = function (result) {
        //var data=result.Data;
    };
    $scope.onAfterGetPoliceStationById = function (result) {
        //var data=result.Data;

    };
    $scope.onAfterLoadPoliceStationDataExtra = function (result) {
        //var DataExtra=result.DataExtra;
        //DropDown Option Tables
        if (result.DataExtra.DistrictList != null)
            $scope.DistrictList = result.DataExtra.DistrictList;
        /*
        //Child Table Bindings, need to fix
                   $scope.PoliceStationIdList =  result.DataExtra.PoliceStationIdList; 
           */
    };
    //======Other tabale's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 
});

