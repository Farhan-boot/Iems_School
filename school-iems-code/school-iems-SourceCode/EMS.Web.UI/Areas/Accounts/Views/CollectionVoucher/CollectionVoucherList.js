
//File:Accounts_CollectionVoucher List Anjular  Controller
emsApp.controller('CollectionVoucherListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.CollectionVoucherList = [];
    $scope.SelectedCollectionVoucher = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
     $scope.SelectedSemesterId=0;      
     $scope.SelectedBankId=0;      
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 50;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
      $scope.getCollectionVoucherListExtraData();
      $scope.getPagedCollectionVoucherList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedCollectionVoucherList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedCollectionVoucherList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedCollectionVoucherList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedCollectionVoucherList();
    };
    $scope.searchCollectionVoucherList = function () {
        $scope.getPagedCollectionVoucherList();
    };
    $scope.getPagedCollectionVoucherList = function () {
        $scope.getCollectionVoucherList();
    };
    /*For Search on data end*/
    $scope.getCollectionVoucherList = function () {
        $http.get($scope.getPagedCollectionVoucherUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
           ,"semesterId": $scope.SelectedSemesterId      
           , "bankId": $scope.SelectedBankId
           , "userId": $scope.UserId
           , "startDate": $scope.StartDate
            , "endDate": $scope.EndDate
              
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.CollectionVoucherList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
                $scope.NetTotal = result.DataExtra.NetTotal;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Collection Voucher list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);

                $scope.CollectionVoucherList = [];
                $scope.totalItems = 0;
                $scope.totalServerItems = 0;
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Collection Voucher list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);

            $scope.CollectionVoucherList = [];
            $scope.totalItems = 0;
            $scope.totalServerItems = 0;
        });
    };
    $scope.getCollectionVoucherListExtraData= function()
    {
            $http.get($scope.getCollectionVoucherListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.CollectionTypeEnumList!=null)
                         $scope.CollectionTypeEnumList = result.DataExtra.CollectionTypeEnumList;
                      if(result.DataExtra.StatusEnumList!=null)
                         $scope.StatusEnumList = result.DataExtra.StatusEnumList;
                  //DropDown Option Tables
                     if(result.DataExtra.SemesterList!=null)
                       $scope.SemesterList =  result.DataExtra.SemesterList; 

                     if(result.DataExtra.OfficialBankList!=null)
                       $scope.OfficialBankList =  result.DataExtra.OfficialBankList; 

                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Collection Voucher! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Collection Voucher! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllCollectionVoucherList = function () {
        $scope.IsLoading++;
        $http.get($scope.getCollectionVoucherListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.CollectionVoucherList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Collection Voucher list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Collection Voucher list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteCollectionVoucherById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteCollectionVoucherByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.CollectionVoucherList.indexOf(obj);
                        $scope.CollectionVoucherList.splice(i, 1);
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
    /*$scope.deleteCollectionVoucherByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteCollectionVoucherByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.CollectionVoucherList.indexOf(obj);
                        $scope.CollectionVoucherList.splice(i, 1);
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
    for (var i = 0; i < $scope.CollectionVoucherList.length; i++) {
        var entity = $scope.CollectionVoucherList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedCollectionVoucherUrl
        ,deleteCollectionVoucherByIdUrl
        ,getCollectionVoucherListDataExtraUrl
        ,saveCollectionVoucherListUrl
        ,getCollectionVoucherByIdUrl
        ,getCollectionVoucherDataExtraUrl
        ,saveCollectionVoucherUrl
        ) {
        $scope.getPagedCollectionVoucherUrl = getPagedCollectionVoucherUrl;
        $scope.deleteCollectionVoucherByIdUrl = deleteCollectionVoucherByIdUrl;
        /*bind extra url if need*/
        $scope.getCollectionVoucherListDataExtraUrl = getCollectionVoucherListDataExtraUrl;
        $scope.saveCollectionVoucherListUrl = saveCollectionVoucherListUrl;
        $scope.getCollectionVoucherByIdUrl = getCollectionVoucherByIdUrl;
        $scope.getCollectionVoucherDataExtraUrl = getCollectionVoucherDataExtraUrl;
        $scope.saveCollectionVoucherUrl = saveCollectionVoucherUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



