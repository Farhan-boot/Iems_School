
//File:BasicAccounting_Voucher List Anjular  Controller
emsApp.controller('VoucherListCtrl', function ($scope, $http, $filter, $document, cfpLoadingBar, $sce) {
    $scope.VoucherList = [];
    $scope.SelectedVoucher = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
     $scope.SelectedBankId=0;      
     $scope.SelectedBranchId=0;      
     $scope.SelectedCompanyId=0;      
     $scope.SelectedVoucherTypeId = 0;
     $scope.SearchChequeNo = "";
     $scope.IsShowTrashedItems = false;
     $scope.VoucherHistoryOf = [];

    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
      $scope.getVoucherListExtraData();
      $scope.getPagedVoucherList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedVoucherList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedVoucherList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedVoucherList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedVoucherList();
    };
    $scope.searchVoucherList = function () {
        $scope.getPagedVoucherList();
    };
    $scope.getPagedVoucherList = function () {
        $scope.getVoucherList();
    };
    /*For Search on data end*/
    $scope.getVoucherList = function () {
        $http.get($scope.getPagedVoucherUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
           ,"bankId": $scope.SelectedBankId      
           ,"branchId": $scope.SelectedBranchId      
           ,"companyId": $scope.SelectedCompanyId      
           , "voucherTypeId": $scope.SelectedVoucherTypeId
            , "chequeNo": $scope.SearchChequeNo
            , "startDate": $scope.StartDate
            , "endDate": $scope.EndDate
            , "isShowTrashedItems": $scope.IsShowTrashedItems
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.VoucherList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
                $scope.TotalAmount = result.DataExtra.TotalAmount;


            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Voucher list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Voucher list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };




    $scope.getVoucherListExtraData= function()
    {
            $http.get($scope.getVoucherListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.VoucherStatusEnumList!=null)
                         $scope.VoucherStatusEnumList = result.DataExtra.VoucherStatusEnumList;
                      if(result.DataExtra.JournalTypeEnumList!=null)
                         $scope.JournalTypeEnumList = result.DataExtra.JournalTypeEnumList;
                      if(result.DataExtra.JournalStatusEnumList!=null)
                         $scope.JournalStatusEnumList = result.DataExtra.JournalStatusEnumList;
                  //DropDown Option Tables
                     if(result.DataExtra.OfficialBankList!=null)
                       $scope.OfficialBankList =  result.DataExtra.OfficialBankList; 

                     if(result.DataExtra.BranchList!=null)
                       $scope.BranchList =  result.DataExtra.BranchList; 

                     if(result.DataExtra.CompanyAccountList!=null)
                       $scope.CompanyAccountList =  result.DataExtra.CompanyAccountList; 

                     if(result.DataExtra.VoucherTypeList!=null)
                       $scope.VoucherTypeList =  result.DataExtra.VoucherTypeList; 

                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Voucher! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Voucher! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllVoucherList = function () {
        $scope.IsLoading++;
        $http.get($scope.getVoucherListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.VoucherList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Voucher list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Voucher list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteVoucherById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteVoucherByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.VoucherList.indexOf(obj);
                        $scope.VoucherList.splice(i, 1);
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

    $scope.getTrashUnTrashVoucherById = function (obj) {

        var msg = "Are you sure you want to Trash this data?";
        if (obj.IsDeleted) {
            msg = "Are you sure you want to Restore this data?";
        }

        bootbox.confirm(msg, function (yes) {
            if (yes) {
                $http.get($scope.getTrashUnTrashVoucherByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.VoucherList.indexOf(obj);
                        $scope.VoucherList.splice(i, 1);
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

    $scope.getVoucherHistoryById = function(id){
        $http.get($scope.getVoucherHistoryByIdUrl, {
        params: { "id": id }
    }).success(function (result, status) {
        if (!result.HasError) {
            $scope.HasError= false;
            $scope.HasViewPermission = result.HasViewPermission;

            $scope.VoucherHistoryOf = result.Data;

            $scope.showHistoryModal();

        } else {
            $scope.HasError= true;
            $scope.ErrorMsg="Unable to get Voucher History! "+result.Errors.toString();
            alertError($scope.ErrorMsg);
        }
    }).error(function (result, status) {
        $scope.HasError= true;
        $scope.ErrorMsg = "Unable to get Voucher History! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
        alertError($scope.ErrorMsg);
    });
};

    /*$scope.deleteVoucherByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteVoucherByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.VoucherList.indexOf(obj);
                        $scope.VoucherList.splice(i, 1);
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
    for (var i = 0; i < $scope.VoucherList.length; i++) {
        var entity = $scope.VoucherList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  

    $scope.showHistoryModal = function () {
        $('#voucherHistoryModal').modal('show');
    };

    $scope.renderHtml = function (html_code) {
        return $sce.trustAsHtml(html_code);
    };
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedVoucherUrl
        ,deleteVoucherByIdUrl
        ,getVoucherListDataExtraUrl
        ,saveVoucherListUrl
        ,getVoucherByIdUrl
        ,getVoucherDataExtraUrl
        , saveVoucherUrl
        , getTrashUnTrashVoucherByIdUrl
        , getVoucherHistoryByIdUrl
        ) {
        $scope.getPagedVoucherUrl = getPagedVoucherUrl;
        $scope.deleteVoucherByIdUrl = deleteVoucherByIdUrl;
        /*bind extra url if need*/
        $scope.getVoucherListDataExtraUrl = getVoucherListDataExtraUrl;
        $scope.saveVoucherListUrl = saveVoucherListUrl;
        $scope.getVoucherByIdUrl = getVoucherByIdUrl;
        $scope.getVoucherDataExtraUrl = getVoucherDataExtraUrl;
        $scope.saveVoucherUrl = saveVoucherUrl;
        $scope.getTrashUnTrashVoucherByIdUrl = getTrashUnTrashVoucherByIdUrl;
        $scope.getVoucherHistoryByIdUrl = getVoucherHistoryByIdUrl;

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



