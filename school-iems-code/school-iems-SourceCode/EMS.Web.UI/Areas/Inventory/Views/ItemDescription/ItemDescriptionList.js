
//File:Inventory_ItemDescription List Anjular  Controller
emsApp.controller('ItemDescriptionListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.ItemDescriptionList = [];
    $scope.SelectedItemDescription = [];
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
      $scope.getItemDescriptionListExtraData();
      $scope.getPagedItemDescriptionList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedItemDescriptionList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedItemDescriptionList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedItemDescriptionList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedItemDescriptionList();
    };
    $scope.searchItemDescriptionList = function () {
        $scope.getPagedItemDescriptionList();
    };
    $scope.getPagedItemDescriptionList = function () {
        $scope.getItemDescriptionList();
    };
    /*For Search on data end*/
    $scope.getItemDescriptionList = function () {
        $http.get($scope.getPagedItemDescriptionUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.ItemDescriptionList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Item Description list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Item Description list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getItemDescriptionListExtraData= function()
    {
            $http.get($scope.getItemDescriptionListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                  //DropDown Option Tables
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Item Description! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Item Description! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllItemDescriptionList = function () {
        $scope.IsLoading++;
        $http.get($scope.getItemDescriptionListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.ItemDescriptionList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Item Description list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Item Description list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteItemDescriptionById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteItemDescriptionByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.ItemDescriptionList.indexOf(obj);
                        $scope.ItemDescriptionList.splice(i, 1);
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
    /*$scope.deleteItemDescriptionByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteItemDescriptionByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.ItemDescriptionList.indexOf(obj);
                        $scope.ItemDescriptionList.splice(i, 1);
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
    for (var i = 0; i < $scope.ItemDescriptionList.length; i++) {
        var entity = $scope.ItemDescriptionList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedItemDescriptionUrl
        ,deleteItemDescriptionByIdUrl
        ,getItemDescriptionListDataExtraUrl
        ,saveItemDescriptionListUrl
        ,getItemDescriptionByIdUrl
        ,getItemDescriptionDataExtraUrl
        ,saveItemDescriptionUrl
        ) {
        $scope.getPagedItemDescriptionUrl = getPagedItemDescriptionUrl;
        $scope.deleteItemDescriptionByIdUrl = deleteItemDescriptionByIdUrl;
        /*bind extra url if need*/
        $scope.getItemDescriptionListDataExtraUrl = getItemDescriptionListDataExtraUrl;
        $scope.saveItemDescriptionListUrl = saveItemDescriptionListUrl;
        $scope.getItemDescriptionByIdUrl = getItemDescriptionByIdUrl;
        $scope.getItemDescriptionDataExtraUrl = getItemDescriptionDataExtraUrl;
        $scope.saveItemDescriptionUrl = saveItemDescriptionUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



