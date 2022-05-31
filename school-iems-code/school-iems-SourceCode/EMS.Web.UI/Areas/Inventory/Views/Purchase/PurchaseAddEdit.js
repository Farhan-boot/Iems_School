
//File: Invt_Purchase Anjular  Controller
emsApp.controller('PurchaseAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.Purchase = [];
    $scope.PurchaseId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (PurchaseId)
    {
       if(PurchaseId!=null)
        $scope.PurchaseId=PurchaseId;
       
       $scope.loadPurchaseDataExtra($scope.PurchaseId);
       $scope.getPurchaseById($scope.PurchaseId);
    };
   $scope.getNewPurchase= function()
    {
    	  $scope.getPurchaseById(0);
    };
   $scope.getPurchaseById= function(PurchaseId)
    {
        if(PurchaseId!=null && PurchaseId!=='')
        $scope.PurchaseId=PurchaseId;
        else return;
        
        $http.get($scope.getPurchaseByIdUrl, {
            params: { "id": $scope.PurchaseId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.Purchase = result.Data;
                updateUrlQuery('id' , $scope.Purchase.Id);
                $scope.onAfterGetPurchaseById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Purchase! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Purchase! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadPurchaseDataExtra= function(PurchaseId)
    {
        if(PurchaseId!=null)
            $scope.PurchaseId=PurchaseId;
            
            $http.get($scope.getPurchaseDataExtraUrl, {
                params: { "id": $scope.PurchaseId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                        $scope.onAfterLoadPurchaseDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Purchase! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Purchase! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.savePurchase= function(){
    	if(!$scope.validatePurchase())
        {
          return;
        }
        $http.post($scope.savePurchaseUrl + "/", $scope.Purchase).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.Purchase = result.Data;
                    updateUrlQuery('id', $scope.Purchase.Id);
                   }
                   alertSuccess("Successfully saved Purchase data!");
                   $scope.onAfterSavePurchase(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Purchase! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Purchase! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deletePurchaseById= function(){
    	
    };
   $scope.validatePurchase= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (PurchaseId,getPurchaseByIdUrl,getPurchaseDataExtraUrl, savePurchaseUrl,deletePurchaseByIdUrl) {
        $scope.PurchaseId = PurchaseId;
        $scope.getPurchaseByIdUrl = getPurchaseByIdUrl;
        $scope.savePurchaseUrl = savePurchaseUrl;
        $scope.getPurchaseDataExtraUrl = getPurchaseDataExtraUrl;
        $scope.deletePurchaseByIdUrl = deletePurchaseByIdUrl;

        $scope.loadPage(PurchaseId);
    };
    
  $scope.onAfterSavePurchase= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetPurchaseById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadPurchaseDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
         if(result.DataExtra.SupplierList!=null)
         $scope.SupplierList =  result.DataExtra.SupplierList; 
  /*
  //Child Table Bindings, need to fix
             $scope.PurchaseIdList =  result.DataExtra.PurchaseIdList; 
     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

