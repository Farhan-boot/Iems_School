
//File:Admission_ParentsPrimaryJobType List Anjular  Controller
emsApp.controller('ParentsPrimaryJobTypeListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.ParentsPrimaryJobTypeList = [];
    $scope.SelectedParentsPrimaryJobType = [];
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
      $scope.getParentsPrimaryJobTypeListExtraData();
      $scope.getPagedParentsPrimaryJobTypeList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedParentsPrimaryJobTypeList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedParentsPrimaryJobTypeList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedParentsPrimaryJobTypeList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedParentsPrimaryJobTypeList();
    };
    $scope.searchParentsPrimaryJobTypeList = function () {
        $scope.getPagedParentsPrimaryJobTypeList();
    };
    $scope.getPagedParentsPrimaryJobTypeList = function () {
        $scope.getParentsPrimaryJobTypeList();
    };
    /*For Search on data end*/
    $scope.getParentsPrimaryJobTypeList = function () {
        $http.get($scope.getPagedParentsPrimaryJobTypeUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.ParentsPrimaryJobTypeList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Parents Primary Job Type list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Parents Primary Job Type list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getParentsPrimaryJobTypeListExtraData= function()
    {
            $http.get($scope.getParentsPrimaryJobTypeListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                  //DropDown Option Tables
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Parents Primary Job Type! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Parents Primary Job Type! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllParentsPrimaryJobTypeList = function () {
        $scope.IsLoading++;
        $http.get($scope.getParentsPrimaryJobTypeListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.ParentsPrimaryJobTypeList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Parents Primary Job Type list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Parents Primary Job Type list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteParentsPrimaryJobTypeById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteParentsPrimaryJobTypeByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.ParentsPrimaryJobTypeList.indexOf(obj);
                        $scope.ParentsPrimaryJobTypeList.splice(i, 1);
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
    /*$scope.deleteParentsPrimaryJobTypeByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteParentsPrimaryJobTypeByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.ParentsPrimaryJobTypeList.indexOf(obj);
                        $scope.ParentsPrimaryJobTypeList.splice(i, 1);
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
    for (var i = 0; i < $scope.ParentsPrimaryJobTypeList.length; i++) {
        var entity = $scope.ParentsPrimaryJobTypeList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedParentsPrimaryJobTypeUrl
        ,deleteParentsPrimaryJobTypeByIdUrl
        ,getParentsPrimaryJobTypeListDataExtraUrl
        ,saveParentsPrimaryJobTypeListUrl
        ,getParentsPrimaryJobTypeByIdUrl
        ,getParentsPrimaryJobTypeDataExtraUrl
        ,saveParentsPrimaryJobTypeUrl
        ) {
        $scope.getPagedParentsPrimaryJobTypeUrl = getPagedParentsPrimaryJobTypeUrl;
        $scope.deleteParentsPrimaryJobTypeByIdUrl = deleteParentsPrimaryJobTypeByIdUrl;
        /*bind extra url if need*/
        $scope.getParentsPrimaryJobTypeListDataExtraUrl = getParentsPrimaryJobTypeListDataExtraUrl;
        $scope.saveParentsPrimaryJobTypeListUrl = saveParentsPrimaryJobTypeListUrl;
        $scope.getParentsPrimaryJobTypeByIdUrl = getParentsPrimaryJobTypeByIdUrl;
        $scope.getParentsPrimaryJobTypeDataExtraUrl = getParentsPrimaryJobTypeDataExtraUrl;
        $scope.saveParentsPrimaryJobTypeUrl = saveParentsPrimaryJobTypeUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



