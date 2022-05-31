
//File:Inventory_ProductCategoryDetails List Anjular  Controller
emsApp.controller('ProductCategoryDetailsListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.ProductCategoryDetailsList = [];
    $scope.SelectedProductCategoryDetails = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
     $scope.SelectedProductCategoryId=0;      
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
      $scope.getProductCategoryDetailsListExtraData();
      $scope.getPagedProductCategoryDetailsList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedProductCategoryDetailsList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedProductCategoryDetailsList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedProductCategoryDetailsList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedProductCategoryDetailsList();
    };
    $scope.searchProductCategoryDetailsList = function () {
        $scope.getPagedProductCategoryDetailsList();
    };
    $scope.getPagedProductCategoryDetailsList = function () {
        $scope.getProductCategoryDetailsList();
    };
    /*For Search on data end*/
    $scope.getProductCategoryDetailsList = function () {
        $http.get($scope.getPagedProductCategoryDetailsUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
           ,"productCategoryId": $scope.SelectedProductCategoryId      
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.ProductCategoryDetailsList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Product Category Details list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Product Category Details list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getProductCategoryDetailsListExtraData= function()
    {
            $http.get($scope.getProductCategoryDetailsListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                  //DropDown Option Tables
                     if(result.DataExtra.ProductCategoryList!=null)
                       $scope.ProductCategoryList =  result.DataExtra.ProductCategoryList; 

                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Product Category Details! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Product Category Details! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllProductCategoryDetailsList = function () {
        $scope.IsLoading++;
        $http.get($scope.getProductCategoryDetailsListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.ProductCategoryDetailsList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Product Category Details list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Product Category Details list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteProductCategoryDetailsById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteProductCategoryDetailsByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.ProductCategoryDetailsList.indexOf(obj);
                        $scope.ProductCategoryDetailsList.splice(i, 1);
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
    /*$scope.deleteProductCategoryDetailsByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteProductCategoryDetailsByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.ProductCategoryDetailsList.indexOf(obj);
                        $scope.ProductCategoryDetailsList.splice(i, 1);
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
    for (var i = 0; i < $scope.ProductCategoryDetailsList.length; i++) {
        var entity = $scope.ProductCategoryDetailsList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedProductCategoryDetailsUrl
        ,deleteProductCategoryDetailsByIdUrl
        ,getProductCategoryDetailsListDataExtraUrl
        ,saveProductCategoryDetailsListUrl
        ,getProductCategoryDetailsByIdUrl
        ,getProductCategoryDetailsDataExtraUrl
        ,saveProductCategoryDetailsUrl
        ) {
        $scope.getPagedProductCategoryDetailsUrl = getPagedProductCategoryDetailsUrl;
        $scope.deleteProductCategoryDetailsByIdUrl = deleteProductCategoryDetailsByIdUrl;
        /*bind extra url if need*/
        $scope.getProductCategoryDetailsListDataExtraUrl = getProductCategoryDetailsListDataExtraUrl;
        $scope.saveProductCategoryDetailsListUrl = saveProductCategoryDetailsListUrl;
        $scope.getProductCategoryDetailsByIdUrl = getProductCategoryDetailsByIdUrl;
        $scope.getProductCategoryDetailsDataExtraUrl = getProductCategoryDetailsDataExtraUrl;
        $scope.saveProductCategoryDetailsUrl = saveProductCategoryDetailsUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



