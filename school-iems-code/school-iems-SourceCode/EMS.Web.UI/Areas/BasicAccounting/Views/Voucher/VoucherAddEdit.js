
//File: BAcnt_Voucher Anjular  Controller


emsApp.controller('VoucherAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.Voucher = [];
    $scope.VoucherId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
    //https://angular-ui.github.io/ui-select/demo-basic.html
    $scope.IsSelect2Open = false;
    $scope.IsShowDeleteUnDeleteMessage = false;
    $scope.DeleteUnDeleteMessage = "";

    $scope.loadPage= function (VoucherId)
    {
       if(VoucherId!=null)
        $scope.VoucherId=VoucherId;
       
       $scope.loadVoucherDataExtra($scope.VoucherId);

       //$scope.getVoucherById($scope.VoucherId);


    };
   $scope.getNewVoucher= function()
    {
    	  $scope.getVoucherById(0);
    };
   $scope.getVoucherById= function(VoucherId)
    {
        if(VoucherId!=null && VoucherId!=='')
        $scope.VoucherId=VoucherId;
        else return;
        
        $http.get($scope.getVoucherByIdUrl, {
            params: { "id": $scope.VoucherId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;

                $scope.IsSelect2Open = false;
                $scope.IsShowDeleteUnDeleteMessage = false; 
                $scope.Voucher = result.Data;

                updateUrlQuery('id' , $scope.Voucher.Id);
                $scope.onAfterGetVoucherById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Voucher! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Voucher! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };



   $scope.loadVoucherDataExtra= function(VoucherId)
    {
        if(VoucherId!=null)
            $scope.VoucherId=VoucherId;
            
            $http.get($scope.getVoucherDataExtraUrl, {
                params: { "id": $scope.VoucherId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.VoucherStatusEnumList!=null)
                         $scope.VoucherStatusEnumList = result.DataExtra.VoucherStatusEnumList;
                      if(result.DataExtra.JournalTypeEnumList!=null)
                         $scope.JournalTypeEnumList = result.DataExtra.JournalTypeEnumList;
                      if(result.DataExtra.JournalStatusEnumList!=null)
                          $scope.JournalStatusEnumList = result.DataExtra.JournalStatusEnumList;
                      if (result.DataExtra.VoucherTypeEnumList != null)
                          $scope.VoucherTypeEnumList = result.DataExtra.VoucherTypeEnumList;

                      if (result.DataExtra.TransactionTypeEnumList != null)
                          $scope.TransactionTypeEnumList = result.DataExtra.TransactionTypeEnumList;



                      $scope.EmptyVoucher = result.DataExtra.EmptyVoucher;
                      $scope.EmptyDrVoucherDtl = result.DataExtra.EmptyDrVoucherDtl;
                      $scope.EmptyCrVoucherDtl = result.DataExtra.EmptyCrVoucherDtl;

                      $scope.onAfterLoadVoucherDataExtra(result);

                      $scope.getVoucherById($scope.VoucherId);

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
   $scope.saveVoucher = function () {

    	if(!$scope.validateVoucher())
        {
          return;
        }
        $http.post($scope.saveVoucherUrl + "/", $scope.Voucher).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null) {
                       $scope.IsSelect2Open = false;
                       $scope.IsShowDeleteUnDeleteMessage = false;

                       $scope.Voucher = result.Data;
                       updateUrlQuery('id', $scope.Voucher.Id);
                   }
                   alertSuccess("Successfully saved Voucher data!");
                   $scope.onAfterSaveVoucher(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Voucher! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Voucher! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteVoucherById= function(){
    	
   };

   $scope.validateVoucher= function(){
        return true;
   };

    //======Custom property and Functions Start=======================================================  

   $scope.setVoucherTypeEnumId = function (id) {

       /*
        Payment = 0,
        Receipt=1,
        Contra=2,
        Journal=3
       */
       
       $scope.Voucher.VoucherTypeEnumId = id;
       $scope.Voucher.VoucherTypeId = $filter('filter')($scope.VoucherTypeList, { TypeEnumId: $scope.Voucher.VoucherTypeEnumId }, true)[0].Id;

       var length = $scope.Voucher.BAcnt_VoucherDetailListJson.length;
       if (length>0) {
           return;
       }

       if (id === 0) { // Payment = 0,
           var emptyDrVoucherDtl = angular.copy($scope.EmptyDrVoucherDtl);
           $scope.Voucher.BAcnt_VoucherDetailListJson.push(emptyDrVoucherDtl);
       }
       else if (id === 1) { // Receipt=1,
           var emptyCrVoucherDtl = angular.copy($scope.EmptyCrVoucherDtl);
           $scope.Voucher.BAcnt_VoucherDetailListJson.push(emptyCrVoucherDtl);
       }


   };

   $scope.uiSelectInit = function (select) {

       if ($scope.IsSelect2Open===false) {
           return;
       }
       select.open = true;
       $('.select2-input').focus().select();
   }

   $scope.addDtlRow = function (row) {

       $scope.IsSelect2Open = true; // use for open Select2

       var totalDebit = $scope.getTotalDebit();
       var totalCredit = $scope.getTotalCredit();


       if (totalDebit > totalCredit) {
           var emptyCrVoucherDtl = angular.copy($scope.EmptyCrVoucherDtl);
           emptyCrVoucherDtl.CreditAmount = totalDebit - totalCredit;
           $scope.Voucher.BAcnt_VoucherDetailListJson.push(emptyCrVoucherDtl);

       } else {
           var emptyDrVoucherDtl = angular.copy($scope.EmptyDrVoucherDtl);
           emptyDrVoucherDtl.DebitAmount = totalCredit - totalDebit;
           $scope.Voucher.BAcnt_VoucherDetailListJson.push(emptyDrVoucherDtl);
       }
    }


   $scope.getTotalDebit = function () {

       var voucherDtlList = $filter('filter')($scope.Voucher.BAcnt_VoucherDetailListJson,
           {
               IsDeleted: false
           }, true);

        var totalValue=0;
       
        for (var i = 0; i < voucherDtlList.length; i++) {

            if (voucherDtlList[i]['DebitAmount'] == null) {
                continue;
            }
            
            totalValue += Number(voucherDtlList[i]['DebitAmount']);
        }
        return totalValue;
    }

   $scope.getTotalCredit = function () {
       var voucherDtlList = $filter('filter')($scope.Voucher.BAcnt_VoucherDetailListJson,
           {
               IsDeleted: false
           }, true);
       
       var totalValue = 0;

        for (var i = 0; i < voucherDtlList.length; i++) {

            if (voucherDtlList[i]['CreditAmount'] == null) {
                continue;
            }
            totalValue += Number(voucherDtlList[i]['CreditAmount']);
        }
        return totalValue;
    }

   $scope.delete = function (row) {
       if (row.Id === 0) {
           var index = $scope.Voucher.BAcnt_VoucherDetailListJson.indexOf(row);
            $scope.Voucher.BAcnt_VoucherDetailListJson.splice(index, 1);
        }
       else {

           /*var msg = "Are you sure you want to delete this data?";
           if (row.IsDeleted) {
               msg = "Are you sure you want to Restore this data?";
           }*/

           $scope.DeleteUnDeleteMessage = "If you sure, you want to delete this data. Then press the 'Save' button after recheck Total Debit & Credit Equality. ";
           $scope.IsShowDeleteUnDeleteMessage = true;
           row.IsDeleted = true;
       }
        
    }
   $scope.revert = function (obj) {
       $scope.DeleteUnDeleteMessage = "If you sure, you want to UnDelete this data. Then press the 'Save' button after recheck Total Debit & Credit Equality. ";
       $scope.IsShowDeleteUnDeleteMessage = true;
        obj.IsDeleted = false;
    }

   $scope.onChangeTransactionType = function (voucherDtlRow) {
        //Dr = 1,  Cr = 2
        if (voucherDtlRow.TransactionTypeEnumId === 1) {
            //for Debit 
            voucherDtlRow.IsDebited = true;
            voucherDtlRow.DebitAmount = voucherDtlRow.CreditAmount;
            voucherDtlRow.CreditAmount = 0;

        } else {
            //for Credit 
            voucherDtlRow.IsDebited = false;
            voucherDtlRow.CreditAmount = voucherDtlRow.DebitAmount;
            voucherDtlRow.DebitAmount = 0;
        }

        
    }

   $scope.addNewVoucherDtl = function () {
        var voucherDtl = angular.copy($scope.EmptyDrVoucherDtl);
        $scope.Voucher.BAcnt_VoucherDetailListJson.push(voucherDtl);

        /*var length = $scope.Voucher.BAcnt_VoucherDetailListJson.length;
        setTimeout(function () {
            $('.transType' + length).focus().select();
        }, 100);*/

    }





//======Custom Variabales goes hare=====

    $scope.Init = function (VoucherId,getVoucherByIdUrl,getVoucherDataExtraUrl, saveVoucherUrl,deleteVoucherByIdUrl) {
        $scope.VoucherId = VoucherId;
        $scope.getVoucherByIdUrl = getVoucherByIdUrl;
        $scope.saveVoucherUrl = saveVoucherUrl;
        $scope.getVoucherDataExtraUrl = getVoucherDataExtraUrl;
        $scope.deleteVoucherByIdUrl = deleteVoucherByIdUrl;

        $scope.loadPage(VoucherId);
    };
    
  $scope.onAfterSaveVoucher= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetVoucherById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadVoucherDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
         if(result.DataExtra.OfficialBankList!=null)
         $scope.OfficialBankList =  result.DataExtra.OfficialBankList; 
         if(result.DataExtra.BranchList!=null)
         $scope.BranchList =  result.DataExtra.BranchList; 
         if(result.DataExtra.CompanyAccountList!=null)
         $scope.CompanyAccountList =  result.DataExtra.CompanyAccountList; 
         if(result.DataExtra.VoucherTypeList!=null)
             $scope.VoucherTypeList = result.DataExtra.VoucherTypeList;

         if (result.DataExtra.LedgerList != null)
             $scope.LedgerList = result.DataExtra.LedgerList;



         /*
         //Child Table Bindings, need to fix
                    $scope.VoucherIdList =  result.DataExtra.VoucherIdList; 
            */
  };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 


});

