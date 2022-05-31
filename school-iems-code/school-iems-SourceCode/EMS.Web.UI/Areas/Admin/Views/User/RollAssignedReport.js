
//File:Admin_Country List Anjular  Controller
emsApp.controller('RollAssignedReportCtrl', function ($scope, $http, $filter, cfpLoadingBar, $sce) {
    $scope.RollAssignedUserList = [];

    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;

    $scope.SelectedDepartmentId = 0;
    $scope.SelectedUserStatusEnumId = -1;
    $scope.SelectedRollId = 0;

    //===================================

    $scope.loadPage = function () {
        $scope.getRollAssignedExtraData();
        //$scope.getPagedAssigned Roll ReportList();
    };
    $scope.getRollAssignedExtraData = function () {
        $http.get($scope.getRollAssignedReportExtraUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;

                if (result.DataExtra.UserStatusEnumList != null)
                    $scope.UserStatusEnumList = result.DataExtra.UserStatusEnumList;

                if (result.DataExtra.DepartmentList != null)
                    $scope.DepartmentList = result.DataExtra.DepartmentList;

                if (result.DataExtra.RollList != null)
                    $scope.RollList = result.DataExtra.RollList;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Assigned Roll Report! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Assigned Roll Report! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getRollAssignedList = function () {

        $http.get($scope.getRollAssignedListUrl, {
                params: {
                    "rollId": $scope.SelectedRollId
                    , "userStatusEnumId": $scope.SelectedUserStatusEnumId
                    , "departmentId": $scope.SelectedDepartmentId
                }
            })
            .success(function (result) {
                if (!result.HasError) {
                    $scope.RollAssignedUserList = result.Data;
                    $scope.HasError = false;
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to get Assigned Roll Report data! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Assigned Roll Report! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
                alertError($scope.ErrorMsg);
            });
    };

    //======Custom Variables goes hare=====


    $scope.Init = function (
        getRollAssignedReportExtraUrl
, getRollAssignedListUrl
       ) {
        $scope.getRollAssignedReportExtraUrl = getRollAssignedReportExtraUrl;
        /*bind extra url if need*/
        $scope.getRollAssignedListUrl = getRollAssignedListUrl;

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



