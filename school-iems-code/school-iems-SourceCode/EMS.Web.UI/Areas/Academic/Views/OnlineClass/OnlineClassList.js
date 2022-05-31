
//File:Admin_OnlineClass List Anjular  Controller
emsApp.controller('OnlineClassListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.OnlineClassList = [];
    $scope.SelectedOnlineClass = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
     $scope.SelectedClassShiftSectionMapId=0;      
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
      $scope.getOnlineClassListExtraData();
      $scope.getPagedOnlineClassList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedOnlineClassList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedOnlineClassList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedOnlineClassList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedOnlineClassList();
    };
    $scope.searchOnlineClassList = function () {
        $scope.getPagedOnlineClassList();
    };
    $scope.getPagedOnlineClassList = function () {
        $scope.getOnlineClassList();
    };
    /*For Search on data end*/
    $scope.getOnlineClassList = function () {
        $http.get($scope.getPagedOnlineClassUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
           ,"classShiftSectionMapId": $scope.SelectedClassShiftSectionMapId      
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.OnlineClassList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Online Class list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Online Class list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getOnlineClassListExtraData= function()
    {
            $http.get($scope.getOnlineClassListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.LiveClassMediumEnumList!=null)
                         $scope.LiveClassMediumEnumList = result.DataExtra.LiveClassMediumEnumList;
                  //DropDown Option Tables
                     if(result.DataExtra.ClassShiftSectionMapList!=null)
                       $scope.ClassShiftSectionMapList =  result.DataExtra.ClassShiftSectionMapList; 

                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Online Class! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Online Class! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllOnlineClassList = function () {
        $scope.IsLoading++;
        $http.get($scope.getOnlineClassListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.OnlineClassList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Online Class list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Online Class list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteOnlineClassById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteOnlineClassByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.OnlineClassList.indexOf(obj);
                        $scope.OnlineClassList.splice(i, 1);
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
    /*$scope.deleteOnlineClassByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteOnlineClassByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.OnlineClassList.indexOf(obj);
                        $scope.OnlineClassList.splice(i, 1);
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
    for (var i = 0; i < $scope.OnlineClassList.length; i++) {
        var entity = $scope.OnlineClassList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedOnlineClassUrl
        ,deleteOnlineClassByIdUrl
        ,getOnlineClassListDataExtraUrl
        ,saveOnlineClassListUrl
        ,getOnlineClassByIdUrl
        ,getOnlineClassDataExtraUrl
        ,saveOnlineClassUrl
        ) {
        $scope.getPagedOnlineClassUrl = getPagedOnlineClassUrl;
        $scope.deleteOnlineClassByIdUrl = deleteOnlineClassByIdUrl;
        /*bind extra url if need*/
        $scope.getOnlineClassListDataExtraUrl = getOnlineClassListDataExtraUrl;
        $scope.saveOnlineClassListUrl = saveOnlineClassListUrl;
        $scope.getOnlineClassByIdUrl = getOnlineClassByIdUrl;
        $scope.getOnlineClassDataExtraUrl = getOnlineClassDataExtraUrl;
        $scope.saveOnlineClassUrl = saveOnlineClassUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



