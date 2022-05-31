
//File: BAcnt_VoucherType Anjular  Controller
emsApp.controller('VoucherTypeAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.VoucherType = [];
    $scope.VoucherTypeId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (VoucherTypeId)
    {
       if(VoucherTypeId!=null)
        $scope.VoucherTypeId=VoucherTypeId;
       
       $scope.loadVoucherTypeDataExtra($scope.VoucherTypeId);
       $scope.getVoucherTypeById($scope.VoucherTypeId);
    };
   $scope.getNewVoucherType= function()
    {
    	  $scope.getVoucherTypeById(0);
    };
   $scope.getVoucherTypeById= function(VoucherTypeId)
    {
        if(VoucherTypeId!=null && VoucherTypeId!=='')
        $scope.VoucherTypeId=VoucherTypeId;
        else return;
        
        $http.get($scope.getVoucherTypeByIdUrl, {
            params: { "id": $scope.VoucherTypeId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.VoucherType = result.Data;
                updateUrlQuery('id' , $scope.VoucherType.Id);
                $scope.onAfterGetVoucherTypeById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Voucher Type! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Voucher Type! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadVoucherTypeDataExtra= function(VoucherTypeId)
    {
        if(VoucherTypeId!=null)
            $scope.VoucherTypeId=VoucherTypeId;
            
            $http.get($scope.getVoucherTypeDataExtraUrl, {
                params: { "id": $scope.VoucherTypeId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.TypeEnumList!=null)
                         $scope.TypeEnumList = result.DataExtra.TypeEnumList;
                      if(result.DataExtra.StatusEnumList!=null)
                         $scope.StatusEnumList = result.DataExtra.StatusEnumList;
                        $scope.onAfterLoadVoucherTypeDataExtra(result);
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
   $scope.saveVoucherType= function(){
    	if(!$scope.validateVoucherType())
        {
          return;
        }
        $http.post($scope.saveVoucherTypeUrl + "/", $scope.VoucherType).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.VoucherType = result.Data;
                    updateUrlQuery('id', $scope.VoucherType.Id);
                   }
                   alertSuccess("Successfully saved Voucher Type data!");
                   $scope.onAfterSaveVoucherType(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Voucher Type! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Voucher Type! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteVoucherTypeById= function(){
    	
    };
   $scope.validateVoucherType= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (VoucherTypeId,getVoucherTypeByIdUrl,getVoucherTypeDataExtraUrl, saveVoucherTypeUrl,deleteVoucherTypeByIdUrl) {
        $scope.VoucherTypeId = VoucherTypeId;
        $scope.getVoucherTypeByIdUrl = getVoucherTypeByIdUrl;
        $scope.saveVoucherTypeUrl = saveVoucherTypeUrl;
        $scope.getVoucherTypeDataExtraUrl = getVoucherTypeDataExtraUrl;
        $scope.deleteVoucherTypeByIdUrl = deleteVoucherTypeByIdUrl;

        $scope.loadPage(VoucherTypeId);
    };
    
  $scope.onAfterSaveVoucherType= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetVoucherTypeById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadVoucherTypeDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
         if(result.DataExtra.BranchList!=null)
         $scope.BranchList =  result.DataExtra.BranchList; 
         if(result.DataExtra.CompanyAccountList!=null)
         $scope.CompanyAccountList =  result.DataExtra.CompanyAccountList; 
  /*
  //Child Table Bindings, need to fix
             $scope.VoucherTypeIdList =  result.DataExtra.VoucherTypeIdList; 
     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

