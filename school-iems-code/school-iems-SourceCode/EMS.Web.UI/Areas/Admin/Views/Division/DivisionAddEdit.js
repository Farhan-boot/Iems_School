
//File: General_Division Anjular  Controller
emsApp.controller('DivisionAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.Division = [];
    $scope.DivisionId = 0;
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
    $scope.loadPage = function (DivisionId) {
        if (DivisionId != null)
            $scope.DivisionId = DivisionId;

        $scope.loadDivisionDataExtra($scope.DivisionId);
        $scope.getDivisionById($scope.DivisionId);
    };
    $scope.getNewDivision = function () {
        $scope.getDivisionById(0);
    };
    $scope.getDivisionById = function (DivisionId) {
        if (DivisionId != null && DivisionId !== '')
            $scope.DivisionId = DivisionId;
        else return;

        $http.get($scope.getDivisionByIdUrl, {
            params: { "id": $scope.DivisionId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.Division = result.Data;
                updateUrlQuery('id', $scope.Division.Id);
                $scope.onAfterGetDivisionById(result);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Division! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Division! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.loadDivisionDataExtra = function (DivisionId) {
        if (DivisionId != null)
            $scope.DivisionId = DivisionId;

        $http.get($scope.getDivisionDataExtraUrl, {
            params: { "id": $scope.DivisionId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.onAfterLoadDivisionDataExtra(result);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Division! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Division! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.saveDivision = function () {
        if (!$scope.validateDivision()) {
            return;
        }
        $http.post($scope.saveDivisionUrl + "/", $scope.Division).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    if (result.Data != null) {
                        $scope.Division = result.Data;
                        updateUrlQuery('id', $scope.Division.Id);
                    }
                    alertSuccess("Successfully saved Division data!");
                    $scope.onAfterSaveDivision(result);
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to save Division! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to save Division! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    };

    $scope.deleteDivisionById = function () {

    };
    $scope.validateDivision = function () {
        return true;
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variabales goes hare=====

    $scope.Init = function (DivisionId, getDivisionByIdUrl, getDivisionDataExtraUrl, saveDivisionUrl, deleteDivisionByIdUrl) {
        $scope.DivisionId = DivisionId;
        $scope.getDivisionByIdUrl = getDivisionByIdUrl;
        $scope.saveDivisionUrl = saveDivisionUrl;
        $scope.getDivisionDataExtraUrl = getDivisionDataExtraUrl;
        $scope.deleteDivisionByIdUrl = deleteDivisionByIdUrl;

        $scope.loadPage(DivisionId);
    };

    $scope.onAfterSaveDivision = function (result) {
        //var data=result.Data;
    };
    $scope.onAfterGetDivisionById = function (result) {
        //var data=result.Data;

    };
    $scope.onAfterLoadDivisionDataExtra = function (result) {
        //var DataExtra=result.DataExtra;
        //DropDown Option Tables
        if (result.DataExtra.CountryList != null)
            $scope.CountryList = result.DataExtra.CountryList;
        /*
        //Child Table Bindings, need to fix
                   $scope.DivisionIdList =  result.DataExtra.DivisionIdList; 
           */
    };
    //======Other tabale's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 
});

