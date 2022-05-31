
//File: General_Country Anjular  Controller
emsApp.controller('CountryAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.Country = [];
    $scope.CountryId = 0;
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
    $scope.loadPage = function (CountryId) {
        if (CountryId != null)
            $scope.CountryId = CountryId;

        $scope.loadCountryDataExtra($scope.CountryId);
        $scope.getCountryById($scope.CountryId);
    };
    $scope.getNewCountry = function () {
        $scope.getCountryById(0);
    };
    $scope.getCountryById = function (CountryId) {
        if (CountryId != null && CountryId !== '')
            $scope.CountryId = CountryId;
        else return;

        $http.get($scope.getCountryByIdUrl, {
            params: { "id": $scope.CountryId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.Country = result.Data;
                updateUrlQuery('id', $scope.Country.Id);
                $scope.onAfterGetCountryById(result);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Country! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Country! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.loadCountryDataExtra = function (CountryId) {
        if (CountryId != null)
            $scope.CountryId = CountryId;

        $http.get($scope.getCountryDataExtraUrl, {
            params: { "id": $scope.CountryId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.onAfterLoadCountryDataExtra(result);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Country! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Country! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.saveCountry = function () {
        if (!$scope.validateCountry()) {
            return;
        }
        $http.post($scope.saveCountryUrl + "/", $scope.Country).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    if (result.Data != null) {
                        $scope.Country = result.Data;
                        updateUrlQuery('id', $scope.Country.Id);
                    }
                    alertSuccess("Successfully saved Country data!");
                    $scope.onAfterSaveCountry(result);
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to save Country! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to save Country! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    };

    $scope.deleteCountryById = function () {

    };
    $scope.validateCountry = function () {
        return true;
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variabales goes hare=====

    $scope.Init = function (CountryId, getCountryByIdUrl, getCountryDataExtraUrl, saveCountryUrl, deleteCountryByIdUrl) {
        $scope.CountryId = CountryId;
        $scope.getCountryByIdUrl = getCountryByIdUrl;
        $scope.saveCountryUrl = saveCountryUrl;
        $scope.getCountryDataExtraUrl = getCountryDataExtraUrl;
        $scope.deleteCountryByIdUrl = deleteCountryByIdUrl;

        $scope.loadPage(CountryId);
    };

    $scope.onAfterSaveCountry = function (result) {
        //var data=result.Data;
    };
    $scope.onAfterGetCountryById = function (result) {
        //var data=result.Data;

    };
    $scope.onAfterLoadCountryDataExtra = function (result) {
        //var DataExtra=result.DataExtra;
        //DropDown Option Tables
        /*
        //Child Table Bindings, need to fix
                   $scope.CountryIdList =  result.DataExtra.CountryIdList; 
      
                   $scope.CountryIdList =  result.DataExtra.CountryIdList; 
      
                   $scope.PlaceOfBirthCountryIdList =  result.DataExtra.PlaceOfBirthCountryIdList; 
      
                   $scope.CountryIdList =  result.DataExtra.CountryIdList; 
      
                   $scope.CountryIdList =  result.DataExtra.CountryIdList; 
           */
    };
    //======Other tabale's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 
});

