
//File: General_PostOffice Anjular  Controller
emsApp.controller('PostOfficeAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.PostOffice = [];
    $scope.PostOfficeId = 0;
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
    $scope.loadPage = function (PostOfficeId) {
        if (PostOfficeId != null)
            $scope.PostOfficeId = PostOfficeId;

        $scope.loadPostOfficeDataExtra($scope.PostOfficeId);
        $scope.getPostOfficeById($scope.PostOfficeId);
    };
    $scope.getNewPostOffice = function () {
        $scope.getPostOfficeById(0);
    };
    $scope.getPostOfficeById = function (PostOfficeId) {
        if (PostOfficeId != null && PostOfficeId !== '')
            $scope.PostOfficeId = PostOfficeId;
        else return;

        $http.get($scope.getPostOfficeByIdUrl, {
            params: { "id": $scope.PostOfficeId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.PostOffice = result.Data;
                updateUrlQuery('id', $scope.PostOffice.Id);
                $scope.onAfterGetPostOfficeById(result);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Post Office! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Post Office! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.loadPostOfficeDataExtra = function (PostOfficeId) {
        if (PostOfficeId != null)
            $scope.PostOfficeId = PostOfficeId;

        $http.get($scope.getPostOfficeDataExtraUrl, {
            params: { "id": $scope.PostOfficeId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.onAfterLoadPostOfficeDataExtra(result);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Post Office! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Post Office! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.savePostOffice = function () {
        if (!$scope.validatePostOffice()) {
            return;
        }
        $http.post($scope.savePostOfficeUrl + "/", $scope.PostOffice).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    if (result.Data != null) {
                        $scope.PostOffice = result.Data;
                        updateUrlQuery('id', $scope.PostOffice.Id);
                    }
                    alertSuccess("Successfully saved Post Office data!");
                    $scope.onAfterSavePostOffice(result);
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to save Post Office! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to save Post Office! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    };

    $scope.deletePostOfficeById = function () {

    };
    $scope.validatePostOffice = function () {
        return true;
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variabales goes hare=====

    $scope.Init = function (PostOfficeId, getPostOfficeByIdUrl, getPostOfficeDataExtraUrl, savePostOfficeUrl, deletePostOfficeByIdUrl) {
        $scope.PostOfficeId = PostOfficeId;
        $scope.getPostOfficeByIdUrl = getPostOfficeByIdUrl;
        $scope.savePostOfficeUrl = savePostOfficeUrl;
        $scope.getPostOfficeDataExtraUrl = getPostOfficeDataExtraUrl;
        $scope.deletePostOfficeByIdUrl = deletePostOfficeByIdUrl;

        $scope.loadPage(PostOfficeId);
    };

    $scope.onAfterSavePostOffice = function (result) {
        //var data=result.Data;
    };
    $scope.onAfterGetPostOfficeById = function (result) {
        //var data=result.Data;

    };
    $scope.onAfterLoadPostOfficeDataExtra = function (result) {
        //var DataExtra=result.DataExtra;
        //DropDown Option Tables
        if (result.DataExtra.DistrictList != null)
            $scope.DistrictList = result.DataExtra.DistrictList;
        if (result.DataExtra.PoliceStationList != null)
            $scope.PoliceStationList = result.DataExtra.PoliceStationList;
        /*
        //Child Table Bindings, need to fix     */
    };
    //======Other tabale's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 
});

