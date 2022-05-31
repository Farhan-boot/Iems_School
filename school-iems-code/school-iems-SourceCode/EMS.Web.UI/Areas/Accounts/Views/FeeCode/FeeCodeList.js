
//File:Accounts_FeeCode List Anjular  Controller
emsApp.controller('FeeCodeListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.FeeCodeList = [];
    $scope.SelectedFeeCode = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
     $scope.SelectedProgramId=0;      
     $scope.SelectedStartSemesterId=0;      
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
      $scope.getFeeCodeListExtraData();
      $scope.getPagedFeeCodeList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedFeeCodeList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedFeeCodeList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedFeeCodeList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedFeeCodeList();
    };
    $scope.searchFeeCodeList = function () {
        $scope.getPagedFeeCodeList();
    };
    $scope.getPagedFeeCodeList = function () {
        $scope.getFeeCodeList();
    };
    /*For Search on data end*/
    $scope.getFeeCodeList = function () {
        $http.get($scope.getPagedFeeCodeUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
           ,"programId": $scope.SelectedProgramId      
           ,"startSemesterId": $scope.SelectedStartSemesterId      
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.FeeCodeList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;

                $scope.HasViewPermission = result.HasViewPermission;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Fee Code list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Fee Code list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getFeeCodeListExtraData= function()
    {
            $http.get($scope.getFeeCodeListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.StatusEnumList!=null)
                         $scope.StatusEnumList = result.DataExtra.StatusEnumList;
                     //DropDown Option Tables
                     if(result.DataExtra.ProgramList!=null)
                       $scope.ProgramList =  result.DataExtra.ProgramList; 

                     if(result.DataExtra.SemesterList!=null)
                       $scope.SemesterList =  result.DataExtra.SemesterList; 

                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Fee Code! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Fee Code! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllFeeCodeList = function () {
        $scope.IsLoading++;
        $http.get($scope.getFeeCodeListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.FeeCodeList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Fee Code list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Fee Code list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteFeeCodeById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteFeeCodeByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.FeeCodeList.indexOf(obj);
                        $scope.FeeCodeList.splice(i, 1);
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
    /*$scope.deleteFeeCodeByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteFeeCodeByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.FeeCodeList.indexOf(obj);
                        $scope.FeeCodeList.splice(i, 1);
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
    for (var i = 0; i < $scope.FeeCodeList.length; i++) {
        var entity = $scope.FeeCodeList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedFeeCodeUrl
        ,deleteFeeCodeByIdUrl
        ,getFeeCodeListDataExtraUrl
        ,saveFeeCodeListUrl
        ,getFeeCodeByIdUrl
        ,getFeeCodeDataExtraUrl
        ,saveFeeCodeUrl
        ) {
        $scope.getPagedFeeCodeUrl = getPagedFeeCodeUrl;
        $scope.deleteFeeCodeByIdUrl = deleteFeeCodeByIdUrl;
        /*bind extra url if need*/
        $scope.getFeeCodeListDataExtraUrl = getFeeCodeListDataExtraUrl;
        $scope.saveFeeCodeListUrl = saveFeeCodeListUrl;
        $scope.getFeeCodeByIdUrl = getFeeCodeByIdUrl;
        $scope.getFeeCodeDataExtraUrl = getFeeCodeDataExtraUrl;
        $scope.saveFeeCodeUrl = saveFeeCodeUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



