
//File: Inv_PurchaseRequisition Anjular  Controller
emsApp.controller('PurchaseRequisitionAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.PurchaseRequisition = [];
    $scope.PurchaseRequisitionId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (PurchaseRequisitionId)
    {
       if(PurchaseRequisitionId!=null)
        $scope.PurchaseRequisitionId=PurchaseRequisitionId;
       
       $scope.loadPurchaseRequisitionDataExtra($scope.PurchaseRequisitionId);
       $scope.getPurchaseRequisitionById($scope.PurchaseRequisitionId);
    };
   $scope.getNewPurchaseRequisition= function()
    {
    	  $scope.getPurchaseRequisitionById(0);
    };
   $scope.getPurchaseRequisitionById= function(PurchaseRequisitionId)
    {
        if(PurchaseRequisitionId!=null && PurchaseRequisitionId!=='')
        $scope.PurchaseRequisitionId=PurchaseRequisitionId;
        else return;
        
        $http.get($scope.getPurchaseRequisitionByIdUrl, {
            params: { "id": $scope.PurchaseRequisitionId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.PurchaseRequisition = result.Data;
                updateUrlQuery('id' , $scope.PurchaseRequisition.Id);
                $scope.onAfterGetPurchaseRequisitionById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Purchase Requisition! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Purchase Requisition! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadPurchaseRequisitionDataExtra= function(PurchaseRequisitionId)
    {
        if(PurchaseRequisitionId!=null)
            $scope.PurchaseRequisitionId=PurchaseRequisitionId;
            
            $http.get($scope.getPurchaseRequisitionDataExtraUrl, {
                params: { "id": $scope.PurchaseRequisitionId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                        $scope.onAfterLoadPurchaseRequisitionDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Purchase Requisition! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Purchase Requisition! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.savePurchaseRequisition= function(){
    	if(!$scope.validatePurchaseRequisition())
        {
          return;
        }
        $http.post($scope.savePurchaseRequisitionUrl + "/", $scope.PurchaseRequisition).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.PurchaseRequisition = result.Data;
                    updateUrlQuery('id', $scope.PurchaseRequisition.Id);
                   }
                   alertSuccess("Successfully saved Purchase Requisition data!");
                   $scope.onAfterSavePurchaseRequisition(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Purchase Requisition! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Purchase Requisition! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deletePurchaseRequisitionById= function(){
    	
    };
   $scope.validatePurchaseRequisition= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (PurchaseRequisitionId,getPurchaseRequisitionByIdUrl,getPurchaseRequisitionDataExtraUrl, savePurchaseRequisitionUrl,deletePurchaseRequisitionByIdUrl) {
        $scope.PurchaseRequisitionId = PurchaseRequisitionId;
        $scope.getPurchaseRequisitionByIdUrl = getPurchaseRequisitionByIdUrl;
        $scope.savePurchaseRequisitionUrl = savePurchaseRequisitionUrl;
        $scope.getPurchaseRequisitionDataExtraUrl = getPurchaseRequisitionDataExtraUrl;
        $scope.deletePurchaseRequisitionByIdUrl = deletePurchaseRequisitionByIdUrl;

        $scope.loadPage(PurchaseRequisitionId);
    };
    
  $scope.onAfterSavePurchaseRequisition= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetPurchaseRequisitionById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadPurchaseRequisitionDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
  /*
  //Child Table Bindings, need to fix
             $scope.PurchaseRequisitionIdList =  result.DataExtra.PurchaseRequisitionIdList; 
     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

