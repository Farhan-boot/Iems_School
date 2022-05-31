
//File:Admin_LoginAudit List Anjular  Controller
emsApp.controller('LoginAuditListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.LoginAuditList = [];
    $scope.SelectedLoginAudit = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;

    //search items
    $scope.SearchText = "";
    $scope.SearchName = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
        $scope.getPagedLoginAuditList();
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedLoginAuditList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedLoginAuditList();
    };
    $scope.searchLoginAuditList = function () {
        $scope.getPagedLoginAuditList();
    };
    $scope.getPagedLoginAuditList = function () {
        $scope.getLoginAuditList();
    };
    /*For Search on data end*/
    $scope.getLoginAuditList = function () {
        $http.get($scope.getPagedLoginAuditUrl, {
            params: { 
            "textkey": $scope.SearchText, 
            "name": $scope.SearchName,
            "pageSize": $scope.PageSize, 
            "pageNo": $scope.PageNo }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.LoginAuditList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Login Audit list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Login Audit list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllLoginAuditList = function () {
        $scope.IsLoading++;
        $http.get($scope.getLoginAuditListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.LoginAuditList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Login Audit list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Login Audit list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteLoginAuditByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteLoginAuditByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.LoginAuditList.indexOf(obj);
                        $scope.LoginAuditList.splice(i, 1);
                       alertSuccess("Data successfully deleted!");
                    } else {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Sorry unable to delete! " + result.Errors.toString();
                        alertError($scope.ErrorMsg);
                    }
                }).error(function (result, status) {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Sorry unable to delete! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                    alertError($scope.ErrorMsg);
                });
            }
        });
    };
    $scope.deleteLoginAuditById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteLoginAuditByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.LoginAuditList.indexOf(obj);
                        $scope.LoginAuditList.splice(i, 1);
                        alertSuccess("Data successfully deleted!");
                    } else {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Sorry unable to delete! " + result.Errors.toString();
                        alertError($scope.ErrorMsg);
                    }
                }).error(function (result, status) {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Sorry unable to delete! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
                    alertError($scope.ErrorMsg);
                });
            }
        });
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====
    $scope.Init = function (
         getPagedLoginAuditUrl
        , deleteLoginAuditByIdUrl
        ) {
        $scope.getPagedLoginAuditUrl = getPagedLoginAuditUrl;
        $scope.deleteLoginAuditByIdUrl = deleteLoginAuditByIdUrl;
        /*bind extra url if need
        $scope.getLoginAuditByIdUrl = getLoginAuditByIdUrl;
        $scope.getLoginAuditDataExtraUrl = getLoginAuditDataExtraUrl;
        $scope.getLoginAuditListDataExtraUrl = getLoginAuditListDataExtraUrl;
        $scope.saveLoginAuditUrl = saveLoginAuditUrl;
        $scope.getLoginAuditListDataExtraUrl = getLoginAuditListDataExtraUrl;
        $scope.saveLoginAuditListUrl = saveLoginAuditListUrl;*/

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



