
//File:Admission_DegreeCategory List Anjular  Controller
emsApp.controller('DegreeCategoryListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.DegreeCategoryList = [];
    $scope.SelectedDegreeCategory = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
      $scope.getDegreeCategoryListExtraData();
      $scope.getPagedDegreeCategoryList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedDegreeCategoryList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedDegreeCategoryList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedDegreeCategoryList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedDegreeCategoryList();
    };
    $scope.searchDegreeCategoryList = function () {
        $scope.getPagedDegreeCategoryList();
    };
    $scope.getPagedDegreeCategoryList = function () {
        $scope.getDegreeCategoryList();
    };
    /*For Search on data end*/
    $scope.getDegreeCategoryList = function () {
        $http.get($scope.getPagedDegreeCategoryUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.DegreeCategoryList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Degree Category list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Degree Category list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getDegreeCategoryListExtraData= function()
    {
            $http.get($scope.getDegreeCategoryListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.DegreeEquivalentEnumList!=null)
                         $scope.DegreeEquivalentEnumList = result.DataExtra.DegreeEquivalentEnumList;
                      if(result.DataExtra.BoardTypeEnumList!=null)
                         $scope.BoardTypeEnumList = result.DataExtra.BoardTypeEnumList;
                  //DropDown Option Tables
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Degree Category! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Degree Category! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllDegreeCategoryList = function () {
        $scope.IsLoading++;
        $http.get($scope.getDegreeCategoryListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.DegreeCategoryList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Degree Category list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Degree Category list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteDegreeCategoryById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteDegreeCategoryByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.DegreeCategoryList.indexOf(obj);
                        $scope.DegreeCategoryList.splice(i, 1);
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
    /*$scope.deleteDegreeCategoryByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteDegreeCategoryByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.DegreeCategoryList.indexOf(obj);
                        $scope.DegreeCategoryList.splice(i, 1);
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
    for (var i = 0; i < $scope.DegreeCategoryList.length; i++) {
        var entity = $scope.DegreeCategoryList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedDegreeCategoryUrl
        ,deleteDegreeCategoryByIdUrl
        ,getDegreeCategoryListDataExtraUrl
        ,saveDegreeCategoryListUrl
        ,getDegreeCategoryByIdUrl
        ,getDegreeCategoryDataExtraUrl
        ,saveDegreeCategoryUrl
        ) {
        $scope.getPagedDegreeCategoryUrl = getPagedDegreeCategoryUrl;
        $scope.deleteDegreeCategoryByIdUrl = deleteDegreeCategoryByIdUrl;
        /*bind extra url if need*/
        $scope.getDegreeCategoryListDataExtraUrl = getDegreeCategoryListDataExtraUrl;
        $scope.saveDegreeCategoryListUrl = saveDegreeCategoryListUrl;
        $scope.getDegreeCategoryByIdUrl = getDegreeCategoryByIdUrl;
        $scope.getDegreeCategoryDataExtraUrl = getDegreeCategoryDataExtraUrl;
        $scope.saveDegreeCategoryUrl = saveDegreeCategoryUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



