
//File:Accounts_OfficialBank List Anjular  Controller
emsApp.controller('OfficialBankListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.OfficialBankList = [];
    $scope.SelectedOfficialBank = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
      $scope.getOfficialBankListExtraData();
      $scope.getPagedOfficialBankList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedOfficialBankList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedOfficialBankList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedOfficialBankList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedOfficialBankList();
    };
    $scope.searchOfficialBankList = function () {
        $scope.getPagedOfficialBankList();
    };
    $scope.getPagedOfficialBankList = function () {
        $scope.getOfficialBankList();
    };
    /*For Search on data end*/
    $scope.getOfficialBankList = function () {
        $http.get($scope.getPagedOfficialBankUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.OfficialBankList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Official Bank list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Official Bank list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getOfficialBankListExtraData= function()
    {
            $http.get($scope.getOfficialBankListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.StatusEnumList!=null)
                         $scope.StatusEnumList = result.DataExtra.StatusEnumList;
                      if(result.DataExtra.TypeEnumList!=null)
                         $scope.TypeEnumList = result.DataExtra.TypeEnumList;
                  //DropDown Option Tables
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Official Bank! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Official Bank! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllOfficialBankList = function () {
        $scope.IsLoading++;
        $http.get($scope.getOfficialBankListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.OfficialBankList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Official Bank list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Official Bank list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteOfficialBankById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteOfficialBankByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.OfficialBankList.indexOf(obj);
                        $scope.OfficialBankList.splice(i, 1);
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
    /*$scope.deleteOfficialBankByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteOfficialBankByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.OfficialBankList.indexOf(obj);
                        $scope.OfficialBankList.splice(i, 1);
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
    for (var i = 0; i < $scope.OfficialBankList.length; i++) {
        var entity = $scope.OfficialBankList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedOfficialBankUrl
        ,deleteOfficialBankByIdUrl
        ,getOfficialBankListDataExtraUrl
        ,saveOfficialBankListUrl
        ,getOfficialBankByIdUrl
        ,getOfficialBankDataExtraUrl
        ,saveOfficialBankUrl
        ) {
        $scope.getPagedOfficialBankUrl = getPagedOfficialBankUrl;
        $scope.deleteOfficialBankByIdUrl = deleteOfficialBankByIdUrl;
        /*bind extra url if need*/
        $scope.getOfficialBankListDataExtraUrl = getOfficialBankListDataExtraUrl;
        $scope.saveOfficialBankListUrl = saveOfficialBankListUrl;
        $scope.getOfficialBankByIdUrl = getOfficialBankByIdUrl;
        $scope.getOfficialBankDataExtraUrl = getOfficialBankDataExtraUrl;
        $scope.saveOfficialBankUrl = saveOfficialBankUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



