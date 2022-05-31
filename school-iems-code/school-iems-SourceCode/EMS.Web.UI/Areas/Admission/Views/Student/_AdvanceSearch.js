
//File: User_Account Anjular  Controller
emsApp.controller('AdvanceSearchCtrl', function ($scope, $http, $filter, cfpLoadingBar) {

    $scope.searchKey = "";
    $scope.SuggestionList = [];

    $scope.getSuggestionByKey = function () {

        $http.get($scope.getSuggestionByIdUrl, {
            params: { "id": 1 }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;

                $scope.SuggestionList = result.Data;
                log(result.Data);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Voucher! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Voucher! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });

        if ($scope.searchKey.length >= 2) {
            alert("hi kaj hosse");
        }
    }

    $scope.uiSelectInit = function (select) {

        if ($scope.IsSelect2Open === false) {
            return;
        }
        select.open = true;
        $('.select2-input').focus().select();
    }
  
    $scope.Init = function (
        getSuggestionByIdUrl
    )
    {
        $scope.getSuggestionByIdUrl = getSuggestionByIdUrl;
        $scope.getSuggestionByKey();
    };

//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

