
//File: Aca_MarkDistributionPolicy Anjular  Controller
emsApp.controller('MarkDistributionPolicyListCtrl', function ($scope, $http, $filter) {
    $scope.MarkDistributionPolicyList = [];
    $scope.SelectedMarkDistributionPolicy = [];
    
    $scope.ErrorMsg = "";
    $scope.HasError = false;

    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
        $scope.getPagedMarkDistributionPolicyList();
    }
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedMarkDistributionPolicyList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedMarkDistributionPolicyList();
    };
    $scope.searchMarkDistributionPolicyList = function () {
        if ($scope.SearchText == null || $scope.SearchText === "") {
            alertError("Please enter text to search!");
        } else {
            $scope.getPagedMarkDistributionPolicyList();
        }
    };
    $scope.getPagedMarkDistributionPolicyList = function () {
        $scope.getMarkDistributionPolicyList($scope.SearchText, $scope.PageSize, $scope.PageNo);
    };
    /*For Search on data end*/
    $scope.getMarkDistributionPolicyList = function (textkey, pageSize, pageNo) {
        
        $http.get($scope.getPagedMarkDistributionPolicyUrl, {
            params: { "textkey": textkey, "pageSize": pageSize, "pageNo": pageNo }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.MarkDistributionPolicyList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Mark Distribution Policy list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
            
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Mark Distribution Policy list! " + "Status: " + status.toString() + ". " + result.toString();
            alertError($scope.ErrorMsg);
            
        });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllMarkDistributionPolicyList = function () {
        
        $http.get($scope.getMarkDistributionPolicyListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.MarkDistributionPolicyList = result.Data;

            } else {
                $scope.HasError = true;
                alertError($scope.ErrorMsg + "Unable to get Mark Distribution Policy list data! " + result.Errors.toString());
            }
            
        }).error(function (result, status) {
            console.log(result);
            console.log(status);
            $scope.HasError = true;
            alertError($scope.ErrorMsg + "Unable to get Mark Distribution Policy list! " + "Status: " + status.toString() + ". " + result.toString());
            
        });
    }
    $scope.deleteMarkDistributionPolicyByObj = function (obj) {
        console.log($scope.deleteMarkDistributionPolicyByIdUrl);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                
                $http.get($scope.deleteMarkDistributionPolicyByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.MarkDistributionPolicyList.indexOf(obj);
                        $scope.MarkDistributionPolicyList.splice(i, 1);
                        alertSuccess($scope.ErrorMsg + "Mark Distribution Policy deleted successfully! ");

                    } else {
                        $scope.HasError = true;
                        alertError($scope.ErrorMsg + "Sorry unable to delete! " + result.Errors.toString());
                    }
                    
                }).error(function (result, status) {
                    $scope.HasError = true;
                    alertError($scope.ErrorMsg + "Sorry unable to delete! " + "Status: " + status.toString() + ". " + result.toString());
                    
                });
            }
        });
    }
    $scope.deleteMarkDistributionPolicyById = function (obj) {

    }
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====
    $scope.Init = function (
         getPagedMarkDistributionPolicyUrl
        , deleteMarkDistributionPolicyByIdUrl
        ) {
        $scope.getPagedMarkDistributionPolicyUrl = getPagedMarkDistributionPolicyUrl;
        $scope.deleteMarkDistributionPolicyByIdUrl = deleteMarkDistributionPolicyByIdUrl;
        /*bind extra url if need
        $scope.getMarkDistributionPolicyByIdUrl = getMarkDistributionPolicyByIdUrl;
        $scope.getMarkDistributionPolicyDataExtraUrl = getMarkDistributionPolicyDataExtraUrl;
        $scope.getMarkDistributionPolicyListDataExtraUrl = getMarkDistributionPolicyListDataExtraUrl;
        $scope.saveMarkDistributionPolicyUrl = saveMarkDistributionPolicyUrl;
        $scope.getMarkDistributionPolicyListDataExtraUrl = getMarkDistributionPolicyListDataExtraUrl;
        $scope.saveMarkDistributionPolicyListUrl = saveMarkDistributionPolicyListUrl;*/

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});

