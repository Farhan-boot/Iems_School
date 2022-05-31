
//File: Inv_PurchaseRequisitionDetail Anjular  Controller
emsApp.controller('PurchaseRequisitionDetailAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.PurchaseRequisitionDetail = [];
    $scope.PurchaseRequisitionDetailId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (PurchaseRequisitionDetailId)
    {
       if(PurchaseRequisitionDetailId!=null)
        $scope.PurchaseRequisitionDetailId=PurchaseRequisitionDetailId;
       
       $scope.loadPurchaseRequisitionDetailDataExtra($scope.PurchaseRequisitionDetailId);
       $scope.getPurchaseRequisitionDetailById($scope.PurchaseRequisitionDetailId);
    };
   $scope.getNewPurchaseRequisitionDetail= function()
    {
    	  $scope.getPurchaseRequisitionDetailById(0);
    };
   $scope.getPurchaseRequisitionDetailById= function(PurchaseRequisitionDetailId)
    {
        if(PurchaseRequisitionDetailId!=null && PurchaseRequisitionDetailId!=='')
        $scope.PurchaseRequisitionDetailId=PurchaseRequisitionDetailId;
        else return;
        
        $http.get($scope.getPurchaseRequisitionDetailByIdUrl, {
            params: { "id": $scope.PurchaseRequisitionDetailId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.PurchaseRequisitionDetail = result.Data;
                updateUrlQuery('id' , $scope.PurchaseRequisitionDetail.Id);
                $scope.onAfterGetPurchaseRequisitionDetailById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Purchase Requisition Detail! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Purchase Requisition Detail! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadPurchaseRequisitionDetailDataExtra= function(PurchaseRequisitionDetailId)
    {
        if(PurchaseRequisitionDetailId!=null)
            $scope.PurchaseRequisitionDetailId=PurchaseRequisitionDetailId;
            
            $http.get($scope.getPurchaseRequisitionDetailDataExtraUrl, {
                params: { "id": $scope.PurchaseRequisitionDetailId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.UnitTypeEnumList!=null)
                         $scope.UnitTypeEnumList = result.DataExtra.UnitTypeEnumList;
                        $scope.onAfterLoadPurchaseRequisitionDetailDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Purchase Requisition Detail! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Purchase Requisition Detail! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.savePurchaseRequisitionDetail= function(){
    	if(!$scope.validatePurchaseRequisitionDetail())
        {
          return;
        }
        $http.post($scope.savePurchaseRequisitionDetailUrl + "/", $scope.PurchaseRequisitionDetail).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.PurchaseRequisitionDetail = result.Data;
                    updateUrlQuery('id', $scope.PurchaseRequisitionDetail.Id);
                   }
                   alertSuccess("Successfully saved Purchase Requisition Detail data!");
                   $scope.onAfterSavePurchaseRequisitionDetail(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Purchase Requisition Detail! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Purchase Requisition Detail! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deletePurchaseRequisitionDetailById= function(){
    	
    };
   $scope.validatePurchaseRequisitionDetail= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (PurchaseRequisitionDetailId,getPurchaseRequisitionDetailByIdUrl,getPurchaseRequisitionDetailDataExtraUrl, savePurchaseRequisitionDetailUrl,deletePurchaseRequisitionDetailByIdUrl) {
        $scope.PurchaseRequisitionDetailId = PurchaseRequisitionDetailId;
        $scope.getPurchaseRequisitionDetailByIdUrl = getPurchaseRequisitionDetailByIdUrl;
        $scope.savePurchaseRequisitionDetailUrl = savePurchaseRequisitionDetailUrl;
        $scope.getPurchaseRequisitionDetailDataExtraUrl = getPurchaseRequisitionDetailDataExtraUrl;
        $scope.deletePurchaseRequisitionDetailByIdUrl = deletePurchaseRequisitionDetailByIdUrl;

        $scope.loadPage(PurchaseRequisitionDetailId);
    };
    
  $scope.onAfterSavePurchaseRequisitionDetail= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetPurchaseRequisitionDetailById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadPurchaseRequisitionDetailDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
         if(result.DataExtra.ProductCategoryList!=null)
         $scope.ProductCategoryList =  result.DataExtra.ProductCategoryList; 
  /*
  //Child Table Bindings, need to fix     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

