
//File:Academic_GradingPolicy List Anjular  Controller
emsApp.controller('GradingPolicyListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.GradingPolicyList = [];
    $scope.SelectedGradingPolicy = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
      $scope.getGradingPolicyListExtraData();
      $scope.getPagedGradingPolicyList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedGradingPolicyList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedGradingPolicyList();
    };
    $scope.searchGradingPolicyList = function () {
        $scope.getPagedGradingPolicyList();
    };
    $scope.getPagedGradingPolicyList = function () {
        $scope.getGradingPolicyList();
    };
    /*For Search on data end*/
    $scope.getGradingPolicyList = function () {
        $http.get($scope.getPagedGradingPolicyUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.GradingPolicyList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Grading Policy list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Grading Policy list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getGradingPolicyListExtraData= function()
    {
            $http.get($scope.getGradingPolicyListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                  //DropDown Option Tables
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Grading Policy! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Grading Policy! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllGradingPolicyList = function () {
        $scope.IsLoading++;
        $http.get($scope.getGradingPolicyListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.GradingPolicyList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Grading Policy list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Grading Policy list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };

    $scope.deleteGradingPolicyByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteGradingPolicyByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.GradingPolicyList.indexOf(obj);
                        $scope.GradingPolicyList.splice(i, 1);
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
    $scope.deleteGradingPolicyById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteGradingPolicyByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.GradingPolicyList.indexOf(obj);
                        $scope.GradingPolicyList.splice(i, 1);
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
    $scope.selectAll = function ($event) {
    var checkbox = $event.target;
    for (var i = 0; i < $scope.GradingPolicyList.length; i++) {
        var entity = $scope.GradingPolicyList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedGradingPolicyUrl
        ,deleteGradingPolicyByIdUrl
        ,getGradingPolicyListDataExtraUrl
        ,saveGradingPolicyListUrl
        ,getGradingPolicyByIdUrl
        ,getGradingPolicyDataExtraUrl
        ,saveGradingPolicyUrl
        ) {
        $scope.getPagedGradingPolicyUrl = getPagedGradingPolicyUrl;
        $scope.deleteGradingPolicyByIdUrl = deleteGradingPolicyByIdUrl;
        /*bind extra url if need*/
        $scope.getGradingPolicyListDataExtraUrl = getGradingPolicyListDataExtraUrl;
        $scope.saveGradingPolicyListUrl = saveGradingPolicyListUrl;
        $scope.getGradingPolicyByIdUrl = getGradingPolicyByIdUrl;
        $scope.getGradingPolicyDataExtraUrl = getGradingPolicyDataExtraUrl;
        $scope.saveGradingPolicyUrl = saveGradingPolicyUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



