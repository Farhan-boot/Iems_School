
//File:Admin_PassReset List Anjular  Controller
emsApp.controller('PassResetListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.PassResetList = [];
    $scope.SelectedPassReset = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;

    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
        $scope.getPagedPassResetList();
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedPassResetList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedPassResetList();
    };
    $scope.searchPassResetList = function () {
        $scope.getPagedPassResetList();
    };
    $scope.getPagedPassResetList = function () {
        $scope.getPassResetList();
    };
    /*For Search on data end*/
    $scope.getPassResetList = function () {
        $http.get($scope.getPagedPassResetUrl, {
            params: { 
            "textkey": $scope.SearchText, 
            "pageSize": $scope.PageSize, 
            "pageNo": $scope.PageNo }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.PassResetList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Pass Reset list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Pass Reset list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllPassResetList = function () {
        $scope.IsLoading++;
        $http.get($scope.getPassResetListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.PassResetList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Pass Reset list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Pass Reset list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deletePassResetByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deletePassResetByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.PassResetList.indexOf(obj);
                        $scope.PassResetList.splice(i, 1);
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
    $scope.deletePassResetById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deletePassResetByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.PassResetList.indexOf(obj);
                        $scope.PassResetList.splice(i, 1);
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
         getPagedPassResetUrl
        , deletePassResetByIdUrl
        ) {
        $scope.getPagedPassResetUrl = getPagedPassResetUrl;
        $scope.deletePassResetByIdUrl = deletePassResetByIdUrl;
        /*bind extra url if need
        $scope.getPassResetByIdUrl = getPassResetByIdUrl;
        $scope.getPassResetDataExtraUrl = getPassResetDataExtraUrl;
        $scope.getPassResetListDataExtraUrl = getPassResetListDataExtraUrl;
        $scope.savePassResetUrl = savePassResetUrl;
        $scope.getPassResetListDataExtraUrl = getPassResetListDataExtraUrl;
        $scope.savePassResetListUrl = savePassResetListUrl;*/

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



