
//File:Accounts_SemWiseScholarship List Anjular  Controller
emsApp.controller('UserLogCtrl', function ($scope, $http, $filter, $document, cfpLoadingBar, $sce) {
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;

    $scope.IsLoadEmployeeLog = false;

    $scope.LoginHistoryPageSize = 5;
    $scope.LoginHistoryPageNo = 1;
    $scope.LoginHistoryTotalItems = 0;
    $scope.LoginHistoryTotalServerItems = 0;
    $scope.LoginAuditList = [];

    $scope.PasswordResetHistoryPageSize = 5;
    $scope.PasswordResetHistoryPageNo = 1;
    $scope.PasswordResetHistoryTotalItems = 0;
    $scope.PasswordResetHistoryTotalServerItems = 0;
    $scope.PasswordResetHistoryList = [];

    $scope.ChangeHistoryPageSize = 3;
    $scope.ChangeHistoryPageNo = 1;
    $scope.ChangeHistoryTotalItems = 0;
    $scope.ChangeHistoryTotalServerItems = 0;
    $scope.ChangeHistoryList = [];


    


    $scope.loadPage = function () {
        $scope.getPagedLoginAuditById();
        $scope.getPagedChangeHistoryById();
        $scope.getPagedPasswordResetHistoryById();
    };

    /*For Search on data end*/
    $scope.getPagedLoginAuditById = function () {
        $http.get($scope.getPagedLoginAuditByIdUrl, {
            params: {
                "userId": $scope.AccountId
                , "pageSize": $scope.LoginHistoryPageSize
                , "pageNo": $scope.LoginHistoryPageNo
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.IsLoadEmployeeLog = true;
                $scope.LoginAuditList = result.Data;
                $scope.LoginHistoryTotalItems = result.Count;
                $scope.LoginHistoryTotalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Employee Login list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Employee Login list data! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };


    //Login history functions
    $scope.changeLoginHistoryPageSize = function () {
        $scope.LoginHistoryPageNo = 1;
        $scope.getPagedLoginAuditById();
    };
    $scope.changeLoginHistoryPageNo = function () {
        $scope.getPagedLoginAuditById();
    };
    $scope.prevLoginHistory = function () {
        $scope.LoginHistoryPageNo = $scope.LoginHistoryPageNo - 1;
        $scope.getPagedLoginAuditById();
    };
    $scope.nextLoginHistory = function () {
        $scope.LoginHistoryPageNo = $scope.LoginHistoryPageNo + 1;
        $scope.getPagedLoginAuditById();
    };


    //Password Email/Mobile change History Functions
    $scope.getPagedChangeHistoryById = function () {
        $http.get($scope.getPagedChangeHistoryByIdUrl, {
            params: {
                "userId": $scope.AccountId
                , "pageSize": $scope.ChangeHistoryPageSize
                , "pageNo": $scope.ChangeHistoryPageNo
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.IsLoadEmployeeLog = true;
                $scope.ChangeHistoryList = result.Data;

                //log(result.Data);
                $scope.ChangeHistoryTotalItems = result.Count;
                $scope.ChangeHistoryTotalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Password/Email change list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Password/Email change list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.changeChangeHistoryPageSize = function () {
        $scope.ChangeHistoryPageNo = 1;
        $scope.getPagedChangeHistoryById();
    };
    $scope.changeChangeHistoryPageNo = function () {
        $scope.getPagedChangeHistoryById();
    };
    $scope.prevChangeHistory = function () {
        $scope.ChangeHistoryPageNo = $scope.ChangeHistoryPageNo - 1;
        $scope.getPagedChangeHistoryById();
    };
    $scope.nextChangeHistory = function () {
        $scope.ChangeHistoryPageNo = $scope.ChangeHistoryPageNo + 1;
        $scope.getPagedChangeHistoryById();
    };


    //Password Reset History Functions
    $scope.getPagedPasswordResetHistoryById = function () {
        $http.get($scope.getPagedPasswordResetHistoryByIdUrl, {
            params: {
                "userId": $scope.AccountId
                , "pageSize": $scope.PasswordResetHistoryPageSize
                , "pageNo": $scope.PasswordResetHistoryPageNo
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.IsLoadEmployeeLog = true;
                $scope.PasswordResetHistoryList = result.Data;

                log(result.Data);
                $scope.PasswordResetHistoryTotalItems = result.Count;
                $scope.PasswordResetHistoryTotalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Password/Email change list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Password/Email change list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.changePasswordResetHistoryPageSize = function () {
        $scope.PasswordResetHistoryPageNo = 1;
        $scope.getPagedPasswordResetHistoryById();
    };
    $scope.changePasswordResetHistoryPageNo = function () {
        $scope.getPagedPasswordResetHistoryById();
    };
    $scope.prevPasswordResetHistory = function () {
        $scope.PasswordResetHistoryPageNo = $scope.PasswordResetHistoryPageNo - 1;
        $scope.getPagedPasswordResetHistoryById();
    };
    $scope.nextPasswordResetHistory = function () {
        $scope.PasswordResetHistoryPageNo = $scope.PasswordResetHistoryPageNo + 1;
        $scope.getPagedPasswordResetHistoryById();
    };



    $scope.renderHtml = function (html_code) {
        return $sce.trustAsHtml(html_code);
    };

    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====


    $scope.Init = function (
            AccountId
    , getPagedLoginAuditByIdUrl
    , getPagedChangeHistoryByIdUrl
    , getPagedPasswordResetHistoryByIdUrl
    ) {
        $scope.AccountId = AccountId;
        $scope.getPagedLoginAuditByIdUrl = getPagedLoginAuditByIdUrl;
        $scope.getPagedChangeHistoryByIdUrl = getPagedChangeHistoryByIdUrl;
        $scope.getPagedPasswordResetHistoryByIdUrl = getPagedPasswordResetHistoryByIdUrl;
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



