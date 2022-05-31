
//File:BasicAccounting_VoucherType List Anjular  Controller
emsApp.controller('VoucherTypeListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.VoucherTypeList = [];
    $scope.SelectedVoucherType = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
     $scope.SelectedBranchId=0;      
     $scope.SelectedCompanyId=0;      
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
      $scope.getVoucherTypeListExtraData();
      $scope.getPagedVoucherTypeList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedVoucherTypeList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedVoucherTypeList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedVoucherTypeList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedVoucherTypeList();
    };
    $scope.searchVoucherTypeList = function () {
        $scope.getPagedVoucherTypeList();
    };
    $scope.getPagedVoucherTypeList = function () {
        $scope.getVoucherTypeList();
    };
    /*For Search on data end*/
    $scope.getVoucherTypeList = function () {
        $http.get($scope.getPagedVoucherTypeUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
           ,"branchId": $scope.SelectedBranchId      
           ,"companyId": $scope.SelectedCompanyId      
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.VoucherTypeList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Voucher Type list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Voucher Type list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getVoucherTypeListExtraData= function()
    {
            $http.get($scope.getVoucherTypeListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.TypeEnumList!=null)
                         $scope.TypeEnumList = result.DataExtra.TypeEnumList;
                      if(result.DataExtra.StatusEnumList!=null)
                         $scope.StatusEnumList = result.DataExtra.StatusEnumList;
                  //DropDown Option Tables
                     if(result.DataExtra.BranchList!=null)
                       $scope.BranchList =  result.DataExtra.BranchList; 

                     if(result.DataExtra.CompanyAccountList!=null)
                       $scope.CompanyAccountList =  result.DataExtra.CompanyAccountList; 

                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Voucher Type! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Voucher Type! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllVoucherTypeList = function () {
        $scope.IsLoading++;
        $http.get($scope.getVoucherTypeListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.VoucherTypeList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Voucher Type list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Voucher Type list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteVoucherTypeById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteVoucherTypeByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.VoucherTypeList.indexOf(obj);
                        $scope.VoucherTypeList.splice(i, 1);
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
    /*$scope.deleteVoucherTypeByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteVoucherTypeByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.VoucherTypeList.indexOf(obj);
                        $scope.VoucherTypeList.splice(i, 1);
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
    for (var i = 0; i < $scope.VoucherTypeList.length; i++) {
        var entity = $scope.VoucherTypeList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedVoucherTypeUrl
        ,deleteVoucherTypeByIdUrl
        ,getVoucherTypeListDataExtraUrl
        ,saveVoucherTypeListUrl
        ,getVoucherTypeByIdUrl
        ,getVoucherTypeDataExtraUrl
        ,saveVoucherTypeUrl
        ) {
        $scope.getPagedVoucherTypeUrl = getPagedVoucherTypeUrl;
        $scope.deleteVoucherTypeByIdUrl = deleteVoucherTypeByIdUrl;
        /*bind extra url if need*/
        $scope.getVoucherTypeListDataExtraUrl = getVoucherTypeListDataExtraUrl;
        $scope.saveVoucherTypeListUrl = saveVoucherTypeListUrl;
        $scope.getVoucherTypeByIdUrl = getVoucherTypeByIdUrl;
        $scope.getVoucherTypeDataExtraUrl = getVoucherTypeDataExtraUrl;
        $scope.saveVoucherTypeUrl = saveVoucherTypeUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



