
//File: BAcnt_VoucherDetail Anjular  Controller
emsApp.controller('VoucherDetailAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.VoucherDetail = [];
    $scope.VoucherDetailId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (VoucherDetailId)
    {
       if(VoucherDetailId!=null)
        $scope.VoucherDetailId=VoucherDetailId;
       
       $scope.loadVoucherDetailDataExtra($scope.VoucherDetailId);
       $scope.getVoucherDetailById($scope.VoucherDetailId);
    };
   $scope.getNewVoucherDetail= function()
    {
    	  $scope.getVoucherDetailById(0);
    };
   $scope.getVoucherDetailById= function(VoucherDetailId)
    {
        if(VoucherDetailId!=null && VoucherDetailId!=='')
        $scope.VoucherDetailId=VoucherDetailId;
        else return;
        
        $http.get($scope.getVoucherDetailByIdUrl, {
            params: { "id": $scope.VoucherDetailId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.VoucherDetail = result.Data;
                updateUrlQuery('id' , $scope.VoucherDetail.Id);
                $scope.onAfterGetVoucherDetailById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Voucher Detail! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Voucher Detail! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadVoucherDetailDataExtra= function(VoucherDetailId)
    {
        if(VoucherDetailId!=null)
            $scope.VoucherDetailId=VoucherDetailId;
            
            $http.get($scope.getVoucherDetailDataExtraUrl, {
                params: { "id": $scope.VoucherDetailId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                        $scope.onAfterLoadVoucherDetailDataExtra(result);
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
   $scope.saveVoucherDetail= function(){
    	if(!$scope.validateVoucherDetail())
        {
          return;
        }
        $http.post($scope.saveVoucherDetailUrl + "/", $scope.VoucherDetail).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.VoucherDetail = result.Data;
                    updateUrlQuery('id', $scope.VoucherDetail.Id);
                   }
                   alertSuccess("Successfully saved Voucher Detail data!");
                   $scope.onAfterSaveVoucherDetail(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Voucher Detail! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Voucher Detail! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteVoucherDetailById= function(){
    	
    };
   $scope.validateVoucherDetail= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (VoucherDetailId,getVoucherDetailByIdUrl,getVoucherDetailDataExtraUrl, saveVoucherDetailUrl,deleteVoucherDetailByIdUrl) {
        $scope.VoucherDetailId = VoucherDetailId;
        $scope.getVoucherDetailByIdUrl = getVoucherDetailByIdUrl;
        $scope.saveVoucherDetailUrl = saveVoucherDetailUrl;
        $scope.getVoucherDetailDataExtraUrl = getVoucherDetailDataExtraUrl;
        $scope.deleteVoucherDetailByIdUrl = deleteVoucherDetailByIdUrl;

        $scope.loadPage(VoucherDetailId);
    };
    
  $scope.onAfterSaveVoucherDetail= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetVoucherDetailById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadVoucherDetailDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
         if(result.DataExtra.LedgerList!=null)
         $scope.LedgerList =  result.DataExtra.LedgerList; 
         if(result.DataExtra.VoucherList!=null)
         $scope.VoucherList =  result.DataExtra.VoucherList; 
  /*
  //Child Table Bindings, need to fix     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

