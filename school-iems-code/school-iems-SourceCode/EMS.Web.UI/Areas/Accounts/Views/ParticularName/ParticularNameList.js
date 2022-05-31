
//File:Accounts_ParticularName List Anjular  Controller
emsApp.controller('ParticularNameListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.ParticularNameList = [];
    $scope.SelectedParticularName = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    //search items
    $scope.SearchText = "";
    $scope.SelectedParticularEnumId = -1;
    $scope.SelectedStatusEnumId = -1;
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
      $scope.getParticularNameListExtraData();
      $scope.getPagedParticularNameList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedParticularNameList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedParticularNameList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedParticularNameList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedParticularNameList();
    };
    $scope.searchParticularNameList = function () {
        $scope.getPagedParticularNameList();
    };
    $scope.getPagedParticularNameList = function () {
        $scope.getParticularNameList();
    };
    /*For Search on data end*/
    $scope.getParticularNameList = function () {
        $http.get($scope.getPagedParticularNameUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
            , "selectedParticularEnumId": $scope.SelectedParticularEnumId
            , "selectedStatusEnumId": $scope.SelectedStatusEnumId
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.ParticularNameList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Particular Name list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Particular Name list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getParticularNameListExtraData= function()
    {
            $http.get($scope.getParticularNameListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.StatusEnumList!=null)
                         $scope.StatusEnumList = result.DataExtra.StatusEnumList;
                      if(result.DataExtra.ParticularTypeEnumList!=null)
                         $scope.ParticularTypeEnumList = result.DataExtra.ParticularTypeEnumList;
                  //DropDown Option Tables
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Particular Name! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Particular Name! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllParticularNameList = function () {
        $scope.IsLoading++;
        $http.get($scope.getParticularNameListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.ParticularNameList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Particular Name list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Particular Name list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteParticularNameById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteParticularNameByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.ParticularNameList.indexOf(obj);
                        $scope.ParticularNameList.splice(i, 1);
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
    /*$scope.deleteParticularNameByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteParticularNameByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.ParticularNameList.indexOf(obj);
                        $scope.ParticularNameList.splice(i, 1);
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
    for (var i = 0; i < $scope.ParticularNameList.length; i++) {
        var entity = $scope.ParticularNameList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedParticularNameUrl
        ,deleteParticularNameByIdUrl
        ,getParticularNameListDataExtraUrl
        ,saveParticularNameListUrl
        ,getParticularNameByIdUrl
        ,getParticularNameDataExtraUrl
        ,saveParticularNameUrl
        ) {
        $scope.getPagedParticularNameUrl = getPagedParticularNameUrl;
        $scope.deleteParticularNameByIdUrl = deleteParticularNameByIdUrl;
        /*bind extra url if need*/
        $scope.getParticularNameListDataExtraUrl = getParticularNameListDataExtraUrl;
        $scope.saveParticularNameListUrl = saveParticularNameListUrl;
        $scope.getParticularNameByIdUrl = getParticularNameByIdUrl;
        $scope.getParticularNameDataExtraUrl = getParticularNameDataExtraUrl;
        $scope.saveParticularNameUrl = saveParticularNameUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



