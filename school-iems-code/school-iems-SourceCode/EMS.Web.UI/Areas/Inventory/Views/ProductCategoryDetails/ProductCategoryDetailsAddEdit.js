
//File: Inv_ProductCategoryDetails Anjular  Controller
emsApp.controller('ProductCategoryDetailsAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.ProductCategoryDetails = [];
    $scope.ProductCategoryDetailsId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (ProductCategoryDetailsId)
    {
       if(ProductCategoryDetailsId!=null)
        $scope.ProductCategoryDetailsId=ProductCategoryDetailsId;
       
       $scope.loadProductCategoryDetailsDataExtra($scope.ProductCategoryDetailsId);
       $scope.getProductCategoryDetailsById($scope.ProductCategoryDetailsId);
    };
   $scope.getNewProductCategoryDetails= function()
    {
    	  $scope.getProductCategoryDetailsById(0);
    };
   $scope.getProductCategoryDetailsById= function(ProductCategoryDetailsId)
    {
        if(ProductCategoryDetailsId!=null && ProductCategoryDetailsId!=='')
        $scope.ProductCategoryDetailsId=ProductCategoryDetailsId;
        else return;
        
        $http.get($scope.getProductCategoryDetailsByIdUrl, {
            params: { "id": $scope.ProductCategoryDetailsId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.ProductCategoryDetails = result.Data;
                updateUrlQuery('id' , $scope.ProductCategoryDetails.Id);
                $scope.onAfterGetProductCategoryDetailsById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Product Category Details! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Product Category Details! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadProductCategoryDetailsDataExtra= function(ProductCategoryDetailsId)
    {
        if(ProductCategoryDetailsId!=null)
            $scope.ProductCategoryDetailsId=ProductCategoryDetailsId;
            
            $http.get($scope.getProductCategoryDetailsDataExtraUrl, {
                params: { "id": $scope.ProductCategoryDetailsId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                        $scope.onAfterLoadProductCategoryDetailsDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Product Category Details! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Product Category Details! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveProductCategoryDetails= function(){
    	if(!$scope.validateProductCategoryDetails())
        {
          return;
        }
        $http.post($scope.saveProductCategoryDetailsUrl + "/", $scope.ProductCategoryDetails).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.ProductCategoryDetails = result.Data;
                    updateUrlQuery('id', $scope.ProductCategoryDetails.Id);
                   }
                   alertSuccess("Successfully saved Product Category Details data!");
                   $scope.onAfterSaveProductCategoryDetails(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Product Category Details! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Product Category Details! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteProductCategoryDetailsById= function(){
    	
    };
   $scope.validateProductCategoryDetails= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (ProductCategoryDetailsId,getProductCategoryDetailsByIdUrl,getProductCategoryDetailsDataExtraUrl, saveProductCategoryDetailsUrl,deleteProductCategoryDetailsByIdUrl) {
        $scope.ProductCategoryDetailsId = ProductCategoryDetailsId;
        $scope.getProductCategoryDetailsByIdUrl = getProductCategoryDetailsByIdUrl;
        $scope.saveProductCategoryDetailsUrl = saveProductCategoryDetailsUrl;
        $scope.getProductCategoryDetailsDataExtraUrl = getProductCategoryDetailsDataExtraUrl;
        $scope.deleteProductCategoryDetailsByIdUrl = deleteProductCategoryDetailsByIdUrl;

        $scope.loadPage(ProductCategoryDetailsId);
    };
    
  $scope.onAfterSaveProductCategoryDetails= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetProductCategoryDetailsById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadProductCategoryDetailsDataExtra= function(result){
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

