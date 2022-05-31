
//File:Inventory_ItemWarrentyDetails List Anjular  Controller
emsApp.controller('ItemWarrentyDetailsListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.ItemWarrentyDetailsList = [];
    $scope.SelectedItemWarrentyDetails = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
      $scope.getItemWarrentyDetailsListExtraData();
      $scope.getPagedItemWarrentyDetailsList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedItemWarrentyDetailsList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedItemWarrentyDetailsList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedItemWarrentyDetailsList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedItemWarrentyDetailsList();
    };
    $scope.searchItemWarrentyDetailsList = function () {
        $scope.getPagedItemWarrentyDetailsList();
    };
    $scope.getPagedItemWarrentyDetailsList = function () {
        $scope.getItemWarrentyDetailsList();
    };
    /*For Search on data end*/
    $scope.getItemWarrentyDetailsList = function () {
        $http.get($scope.getPagedItemWarrentyDetailsUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.ItemWarrentyDetailsList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Item Warrenty Details list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Item Warrenty Details list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getItemWarrentyDetailsListExtraData= function()
    {
            $http.get($scope.getItemWarrentyDetailsListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                  //DropDown Option Tables
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Item Warrenty Details! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Item Warrenty Details! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllItemWarrentyDetailsList = function () {
        $scope.IsLoading++;
        $http.get($scope.getItemWarrentyDetailsListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.ItemWarrentyDetailsList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Item Warrenty Details list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Item Warrenty Details list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteItemWarrentyDetailsById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteItemWarrentyDetailsByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.ItemWarrentyDetailsList.indexOf(obj);
                        $scope.ItemWarrentyDetailsList.splice(i, 1);
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
    /*$scope.deleteItemWarrentyDetailsByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteItemWarrentyDetailsByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.ItemWarrentyDetailsList.indexOf(obj);
                        $scope.ItemWarrentyDetailsList.splice(i, 1);
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
    };*/

    $scope.selectAll = function ($event) {
    var checkbox = $event.target;
    for (var i = 0; i < $scope.ItemWarrentyDetailsList.length; i++) {
        var entity = $scope.ItemWarrentyDetailsList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedItemWarrentyDetailsUrl
        ,deleteItemWarrentyDetailsByIdUrl
        ,getItemWarrentyDetailsListDataExtraUrl
        ,saveItemWarrentyDetailsListUrl
        ,getItemWarrentyDetailsByIdUrl
        ,getItemWarrentyDetailsDataExtraUrl
        ,saveItemWarrentyDetailsUrl
        ) {
        $scope.getPagedItemWarrentyDetailsUrl = getPagedItemWarrentyDetailsUrl;
        $scope.deleteItemWarrentyDetailsByIdUrl = deleteItemWarrentyDetailsByIdUrl;
        /*bind extra url if need*/
        $scope.getItemWarrentyDetailsListDataExtraUrl = getItemWarrentyDetailsListDataExtraUrl;
        $scope.saveItemWarrentyDetailsListUrl = saveItemWarrentyDetailsListUrl;
        $scope.getItemWarrentyDetailsByIdUrl = getItemWarrentyDetailsByIdUrl;
        $scope.getItemWarrentyDetailsDataExtraUrl = getItemWarrentyDetailsDataExtraUrl;
        $scope.saveItemWarrentyDetailsUrl = saveItemWarrentyDetailsUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



