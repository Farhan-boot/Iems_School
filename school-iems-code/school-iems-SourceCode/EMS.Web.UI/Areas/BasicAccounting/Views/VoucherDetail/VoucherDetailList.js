
//File:BasicAccounting_VoucherDetail List Anjular  Controller
emsApp.controller('VoucherDetailListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.VoucherDetailList = [];
    $scope.SelectedVoucherDetail = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
     $scope.SelectedLedgerId=0;      
     $scope.SelectedVoucherId=0;      
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
      $scope.getVoucherDetailListExtraData();
      $scope.getPagedVoucherDetailList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedVoucherDetailList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedVoucherDetailList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedVoucherDetailList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedVoucherDetailList();
    };
    $scope.searchVoucherDetailList = function () {
        $scope.getPagedVoucherDetailList();
    };
    $scope.getPagedVoucherDetailList = function () {
        $scope.getVoucherDetailList();
    };
    /*For Search on data end*/
    $scope.getVoucherDetailList = function () {
        $http.get($scope.getPagedVoucherDetailUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
           ,"ledgerId": $scope.SelectedLedgerId      
           ,"voucherId": $scope.SelectedVoucherId      
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.VoucherDetailList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Voucher Detail list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Voucher Detail list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getVoucherDetailListExtraData= function()
    {
            $http.get($scope.getVoucherDetailListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                  //DropDown Option Tables
                     if(result.DataExtra.LedgerList!=null)
                       $scope.LedgerList =  result.DataExtra.LedgerList; 

                     if(result.DataExtra.VoucherList!=null)
                       $scope.VoucherList =  result.DataExtra.VoucherList; 

                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Voucher Detail! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Voucher Detail! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllVoucherDetailList = function () {
        $scope.IsLoading++;
        $http.get($scope.getVoucherDetailListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.VoucherDetailList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Voucher Detail list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Voucher Detail list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteVoucherDetailById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteVoucherDetailByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.VoucherDetailList.indexOf(obj);
                        $scope.VoucherDetailList.splice(i, 1);
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
    /*$scope.deleteVoucherDetailByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteVoucherDetailByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.VoucherDetailList.indexOf(obj);
                        $scope.VoucherDetailList.splice(i, 1);
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
    for (var i = 0; i < $scope.VoucherDetailList.length; i++) {
        var entity = $scope.VoucherDetailList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedVoucherDetailUrl
        ,deleteVoucherDetailByIdUrl
        ,getVoucherDetailListDataExtraUrl
        ,saveVoucherDetailListUrl
        ,getVoucherDetailByIdUrl
        ,getVoucherDetailDataExtraUrl
        ,saveVoucherDetailUrl
        ) {
        $scope.getPagedVoucherDetailUrl = getPagedVoucherDetailUrl;
        $scope.deleteVoucherDetailByIdUrl = deleteVoucherDetailByIdUrl;
        /*bind extra url if need*/
        $scope.getVoucherDetailListDataExtraUrl = getVoucherDetailListDataExtraUrl;
        $scope.saveVoucherDetailListUrl = saveVoucherDetailListUrl;
        $scope.getVoucherDetailByIdUrl = getVoucherDetailByIdUrl;
        $scope.getVoucherDetailDataExtraUrl = getVoucherDetailDataExtraUrl;
        $scope.saveVoucherDetailUrl = saveVoucherDetailUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



