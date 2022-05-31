
//File: Acnt_CollectionVoucher Anjular  Controller
emsApp.controller('CollectionVoucherAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.CollectionVoucher = [];
    $scope.CollectionVoucherId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (CollectionVoucherId)
    {
       if(CollectionVoucherId!=null)
        $scope.CollectionVoucherId=CollectionVoucherId;
       
       $scope.loadCollectionVoucherDataExtra($scope.CollectionVoucherId);
       $scope.getCollectionVoucherById($scope.CollectionVoucherId);
    };
   $scope.getNewCollectionVoucher= function()
    {
    	  $scope.getCollectionVoucherById(0);
    };
   $scope.getCollectionVoucherById= function(CollectionVoucherId)
    {
        if(CollectionVoucherId!=null && CollectionVoucherId!=='')
        $scope.CollectionVoucherId=CollectionVoucherId;
        else return;
        
        $http.get($scope.getCollectionVoucherByIdUrl, {
            params: { "id": $scope.CollectionVoucherId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.CollectionVoucher = result.Data;
                updateUrlQuery('id' , $scope.CollectionVoucher.Id);
                $scope.onAfterGetCollectionVoucherById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Collection Voucher! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Collection Voucher! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadCollectionVoucherDataExtra= function(CollectionVoucherId)
    {
        if(CollectionVoucherId!=null)
            $scope.CollectionVoucherId=CollectionVoucherId;
            
            $http.get($scope.getCollectionVoucherDataExtraUrl, {
                params: { "id": $scope.CollectionVoucherId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.CollectionTypeEnumList!=null)
                         $scope.CollectionTypeEnumList = result.DataExtra.CollectionTypeEnumList;
                      if(result.DataExtra.StatusEnumList!=null)
                         $scope.StatusEnumList = result.DataExtra.StatusEnumList;
                        $scope.onAfterLoadCollectionVoucherDataExtra(result);
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
    //Init

    //PageLoad
    //CheckEligibilityRoute() ->isValidToSubmitCheckEligibility
    //isValidToSaveXyz
    //IsValidToDelete
    //

   $scope.saveCollectionVoucher= function(){
    	if(!$scope.validateCollectionVoucher())
        {
          return;
        }
        $http.post($scope.saveCollectionVoucherUrl + "/", $scope.CollectionVoucher).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.CollectionVoucher = result.Data;
                    updateUrlQuery('id', $scope.CollectionVoucher.Id);
                   }
                   alertSuccess("Successfully saved Collection Voucher data!");
                   $scope.onAfterSaveCollectionVoucher(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Collection Voucher! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Collection Voucher! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteCollectionVoucherById= function(){
    	
    };
   $scope.validateCollectionVoucher= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (CollectionVoucherId,getCollectionVoucherByIdUrl,getCollectionVoucherDataExtraUrl, saveCollectionVoucherUrl,deleteCollectionVoucherByIdUrl) {
        $scope.CollectionVoucherId = CollectionVoucherId;
        $scope.getCollectionVoucherByIdUrl = getCollectionVoucherByIdUrl;
        $scope.saveCollectionVoucherUrl = saveCollectionVoucherUrl;
        $scope.getCollectionVoucherDataExtraUrl = getCollectionVoucherDataExtraUrl;
        $scope.deleteCollectionVoucherByIdUrl = deleteCollectionVoucherByIdUrl;

        $scope.loadPage(CollectionVoucherId);
    };
    
  $scope.onAfterSaveCollectionVoucher= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetCollectionVoucherById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadCollectionVoucherDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
         if(result.DataExtra.SemesterList!=null)
         $scope.SemesterList =  result.DataExtra.SemesterList; 
         if(result.DataExtra.OfficialBankList!=null)
         $scope.OfficialBankList =  result.DataExtra.OfficialBankList; 
  /*
  //Child Table Bindings, need to fix     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

