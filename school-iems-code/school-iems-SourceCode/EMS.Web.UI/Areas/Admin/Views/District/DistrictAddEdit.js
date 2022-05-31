
//File: General_District Anjular  Controller
emsApp.controller('DistrictAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.District = [];
    $scope.DistrictId = 0;
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
    $scope.loadPage = function (DistrictId) {
        if (DistrictId != null)
            $scope.DistrictId = DistrictId;

        $scope.loadDistrictDataExtra($scope.DistrictId);
        $scope.getDistrictById($scope.DistrictId);
    };
    $scope.getNewDistrict = function () {
        $scope.getDistrictById(0);
    };
    $scope.getDistrictById = function (DistrictId) {
        if (DistrictId != null && DistrictId !== '')
            $scope.DistrictId = DistrictId;
        else return;

        $http.get($scope.getDistrictByIdUrl, {
            params: { "id": $scope.DistrictId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.District = result.Data;
                updateUrlQuery('id', $scope.District.Id);
                $scope.onAfterGetDistrictById(result);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get District! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get District! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.loadDistrictDataExtra = function (DistrictId) {
        if (DistrictId != null)
            $scope.DistrictId = DistrictId;

        $http.get($scope.getDistrictDataExtraUrl, {
            params: { "id": $scope.DistrictId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.DataExtra.StatusEnumList != null)
                    $scope.StatusEnumList = result.DataExtra.StatusEnumList;
                $scope.onAfterLoadDistrictDataExtra(result);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for District! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for District! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.saveDistrict = function () {
        if (!$scope.validateDistrict()) {
            return;
        }
        $http.post($scope.saveDistrictUrl + "/", $scope.District).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    if (result.Data != null) {
                        $scope.District = result.Data;
                        updateUrlQuery('id', $scope.District.Id);
                    }
                    alertSuccess("Successfully saved District data!");
                    $scope.onAfterSaveDistrict(result);
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to save District! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to save District! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    };

    $scope.deleteDistrictById = function () {

    };
    $scope.validateDistrict = function () {
        return true;
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variabales goes hare=====

    $scope.Init = function (DistrictId, getDistrictByIdUrl, getDistrictDataExtraUrl, saveDistrictUrl, deleteDistrictByIdUrl) {
        $scope.DistrictId = DistrictId;
        $scope.getDistrictByIdUrl = getDistrictByIdUrl;
        $scope.saveDistrictUrl = saveDistrictUrl;
        $scope.getDistrictDataExtraUrl = getDistrictDataExtraUrl;
        $scope.deleteDistrictByIdUrl = deleteDistrictByIdUrl;

        $scope.loadPage(DistrictId);
    };

    $scope.onAfterSaveDistrict = function (result) {
        //var data=result.Data;
    };
    $scope.onAfterGetDistrictById = function (result) {
        //var data=result.Data;

    };
    $scope.onAfterLoadDistrictDataExtra = function (result) {
        //var DataExtra=result.DataExtra;
        //DropDown Option Tables
        if (result.DataExtra.CountryList != null)
            $scope.CountryList = result.DataExtra.CountryList;
        if (result.DataExtra.DivisionList != null)
            $scope.DivisionList = result.DataExtra.DivisionList;
        /*
        //Child Table Bindings, need to fix
                   $scope.DistrictIdList =  result.DataExtra.DistrictIdList; 
      
                   $scope.DistrictIdList =  result.DataExtra.DistrictIdList; 
           */
    };
    //======Other tabale's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 
});

