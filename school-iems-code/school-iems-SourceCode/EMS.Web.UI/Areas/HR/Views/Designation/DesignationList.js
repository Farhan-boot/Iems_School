
//File:HR_Designation List Anjular  Controller
emsApp.controller('DesignationListCtrl', function ($scope, $http, $filter) {
    $scope.DesignationList = [];
    $scope.SelectedDesignation = [];
    
    $scope.ErrorMsg = "";
    $scope.HasError = false;

    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 20;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
        $scope.getPagedDesignationList();
    }
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedDesignationList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedDesignationList();
    };
    $scope.searchDesignationList = function () {
        if ($scope.SearchText == null || $scope.SearchText === "") {
            alertError("Please enter text to search!");
        } else {
            $scope.getPagedDesignationList();
        }
    };
    $scope.getPagedDesignationList = function () {
        $scope.getDesignationList($scope.SearchText, $scope.PageSize, $scope.PageNo);
    };
    /*For Search on data end*/
    $scope.getDesignationList = function (textkey, pageSize, pageNo) {
        
        $http.get($scope.getPagedDesignationUrl, {
            params: { "textkey": textkey, "pageSize": pageSize, "pageNo": pageNo }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.DesignationList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Designation list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
            
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Designation list! " + "Status: " + status.toString() + ". " + result.Message.toString();
            alertError($scope.ErrorMsg);
            
        });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllDesignationList = function () {
        
        $http.get($scope.getDesignationListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.DesignationList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Designation list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
            
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Designation list! " + "Status: " + status.toString() + ". " + result.Message.toString();
            alertError( $scope.ErrorMsg);
            
        });
    }
    $scope.deleteDesignationByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                
                $http.get($scope.deleteDesignationByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.DesignationList.indexOf(obj);
                        $scope.DesignationList.slice(i, 1);
                        alertSuccess("Successfully deleted Designation data!");

                    } else {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Unable to delete Designation data! " + result.Errors.toString();
                        alertError($scope.ErrorMsg);
                    }
                    
                }).error(function (result, status) {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to delete Designation data! " + "Status: " + status.toString() + ". " + result.Message.toString();
                    alertError($scope.ErrorMsg);
                    
                });
            }
        });
    }
    $scope.deleteDesignationById = function (obj) {

    }
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====
    $scope.Init = function (
         getPagedDesignationUrl
        , deleteDesignationByIdUrl
        ) {
        $scope.getPagedDesignationUrl = getPagedDesignationUrl;
        $scope.deleteDesignationByIdUrl = deleteDesignationByIdUrl;
        /*bind extra url if need
        $scope.getDesignationByIdUrl = getDesignationByIdUrl;
        $scope.getDesignationDataExtraUrl = getDesignationDataExtraUrl;
        $scope.getDesignationListDataExtraUrl = getDesignationListDataExtraUrl;
        $scope.saveDesignationUrl = saveDesignationUrl;
        $scope.getDesignationListDataExtraUrl = getDesignationListDataExtraUrl;
        $scope.saveDesignationListUrl = saveDesignationListUrl;*/

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



