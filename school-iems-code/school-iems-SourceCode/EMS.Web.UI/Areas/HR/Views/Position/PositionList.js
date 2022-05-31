
//File:HR_Position List Anjular  Controller
emsApp.controller('PositionListCtrl', function ($scope, $http, $filter) {
    $scope.PositionList = [];
    $scope.SelectedPosition = [];
    
    $scope.ErrorMsg = "";
    $scope.HasError = false;

    //search items
    $scope.SearchText = "";
    $scope.SearchByJobClassEnumId = new Object();
    $scope.SelectedSalaryTemplateGroupId = -1;
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 50;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
        $scope.getPositionListExtraData();
        $scope.getPagedPositionList();
    }
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedPositionList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedPositionList();
    };
    $scope.searchPositionList = function () {
        $scope.getPagedPositionList();
    };
    $scope.getPagedPositionList = function () {
        $scope.getPositionList();
    };
    /*For Search on data end*/
    $scope.getPositionList = function () {
        
        $http.get($scope.getPagedPositionUrl, {
            params: { 
            "textkey": $scope.SearchText,
            "pageSize": $scope.PageSize, 
                "pageNo": $scope.PageNo,
            "jobClassEnumId": $scope.SearchByJobClassEnumId,
                "salaryTemplateGroupId": $scope.SelectedSalaryTemplateGroupId,
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.PositionList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Position list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
            
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Position list! " + "Status: " + status.toString() + ". " + result.Message.toString();
            alertError($scope.ErrorMsg);
            
        });
    };

    $scope.getPositionListExtraData = function () {
        $http.get($scope.getPositionListDataExtraUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.DataExtra.JobClassEnumList != null)
                    $scope.JobClassEnumList = result.DataExtra.JobClassEnumList;
                if (result.DataExtra.StatusEnumList != null)
                    $scope.StatusEnumList = result.DataExtra.StatusEnumList;

                if (result.DataExtra.SalaryTemplateGroupList != null)
                    $scope.SalaryTemplateGroupList = result.DataExtra.SalaryTemplateGroupList;
                //DropDown Option Tables
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Position! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Position! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllPositionList = function () {
        
        $http.get($scope.getPositionListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.PositionList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Position list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
            
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Position list! " + "Status: " + status.toString() + ". " + result.Message.toString();
            alertError( $scope.ErrorMsg);
            
        });
    }
    $scope.deletePositionByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                
                $http.get($scope.deletePositionByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.PositionList.indexOf(obj);
                        $scope.PositionList.slice(i, 1);
                        alertSuccess("Successfully deleted Position data!");

                    } else {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Unable to delete Position data! " + result.Errors.toString();
                        alertError($scope.ErrorMsg);
                    }
                    
                }).error(function (result, status) {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to delete Position data! " + "Status: " + status.toString() + ". " + result.Message.toString();
                    alertError($scope.ErrorMsg);
                    
                });
            }
        });
    }
    $scope.deletePositionById = function (obj) {

    }
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====
    $scope.Init = function (
         getPagedPositionUrl
        , deletePositionByIdUrl
        , getPositionListDataExtraUrl
        ) {
        $scope.getPagedPositionUrl = getPagedPositionUrl;
        $scope.deletePositionByIdUrl = deletePositionByIdUrl;
        $scope.getPositionListDataExtraUrl = getPositionListDataExtraUrl;
        /*bind extra url if need
        $scope.getPositionByIdUrl = getPositionByIdUrl;
        $scope.getPositionDataExtraUrl = getPositionDataExtraUrl;
        $scope.getPositionListDataExtraUrl = getPositionListDataExtraUrl;
        $scope.savePositionUrl = savePositionUrl;
        $scope.getPositionListDataExtraUrl = getPositionListDataExtraUrl;
        $scope.savePositionListUrl = savePositionListUrl;*/

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



