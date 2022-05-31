
//File:Admin_Rank List Anjular  Controller
emsApp.controller('RankListCtrl', function ($scope, $http, $filter) {
    $scope.RankList = [];
    $scope.SelectedRank = [];
    
    $scope.ErrorMsg = "";
    $scope.HasError = false;

    //search items
    $scope.SearchText = "";
    $scope.SearchByCategoryEnumId = new Object();
    $scope.SearchByJobClassEnumId = new Object();
    $scope.SearchByAcademicLevelEnumId = new Object();
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 50;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
        $scope.getRankListExtraData();
        $scope.getPagedRankList();
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedRankList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedRankList();
    };
    $scope.searchRankList = function () {
        $scope.getPagedRankList();
    };
    $scope.getPagedRankList = function () {
        $scope.getRankList();
    };
    /*For Search on data end*/
    $scope.getRankList = function () {
        
        $http.get($scope.getPagedRankUrl, {
            params: { 
            "textkey": $scope.SearchText, 
            "categoryEnumId": $scope.SearchByCategoryEnumId,
            "jobClassEnumId": $scope.SearchByJobClassEnumId,
            "academicLevelEnumId": $scope.SearchByAcademicLevelEnumId,
            "pageSize": $scope.PageSize, 
            "pageNo": $scope.PageNo }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.RankList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Rank list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
            
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Rank list! " + "Status: " + status.toString() + ". " + result.Message.toString();
            alertError($scope.ErrorMsg);
            
        });
    };
    $scope.getRankListExtraData = function () {
        $http.get($scope.getRankListDataExtraUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.DataExtra.CategoryEnumList != null)
                    $scope.CategoryEnumList = result.DataExtra.CategoryEnumList;
                if (result.DataExtra.JobClassEnumList != null)
                    $scope.JobClassEnumList = result.DataExtra.JobClassEnumList;
                if (result.DataExtra.AcademicLevelEnumList != null)
                    $scope.AcademicLevelEnumList = result.DataExtra.AcademicLevelEnumList;
                //DropDown Option Tables
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Rank! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Rank! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllRankList = function() {
        
        $http.get($scope.getRankListUrl, {
            params: {}
        }).success(function(result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.RankList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Rank list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
            
        }).error(function(result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Rank list! " + "Status: " + status.toString() + ". " + result.Message.toString();
            alertError($scope.ErrorMsg);
            
        });
    };
    $scope.deleteRankByObj = function(obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function(yes) {
            if (yes) {
                
                $http.get($scope.deleteRankByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function(result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.RankList.indexOf(obj);
                        $scope.RankList.slice(i, 1);
                        alertSuccess("Successfully deleted Rank data!");

                    } else {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Unable to delete Rank data! " + result.Errors.toString();
                        alertError($scope.ErrorMsg);
                    }
                    
                }).error(function(result, status) {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to delete Rank data! " + "Status: " + status.toString() + ". " + result.Message.toString();
                    alertError($scope.ErrorMsg);
                    
                });
            }
        });
    };
    $scope.deleteRankById = function(obj) {

    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====
    $scope.Init = function (
         getPagedRankUrl
        , deleteRankByIdUrl
        , getRankListDataExtraUrl
        ) {
        $scope.getPagedRankUrl = getPagedRankUrl;
        $scope.deleteRankByIdUrl = deleteRankByIdUrl;
        $scope.getRankListDataExtraUrl = getRankListDataExtraUrl;
        /*bind extra url if need
        $scope.getRankByIdUrl = getRankByIdUrl;
        $scope.getRankDataExtraUrl = getRankDataExtraUrl;
        $scope.getRankListDataExtraUrl = getRankListDataExtraUrl;
        $scope.saveRankUrl = saveRankUrl;
        $scope.saveRankListUrl = saveRankListUrl;*/

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



