
//File: Inv_ProductCategory Anjular  Controller
emsApp.controller('ProductCategoryAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.ProductCategory = [];
    $scope.ProductCategoryId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (ProductCategoryId)
    {
       if(ProductCategoryId!=null)
        $scope.ProductCategoryId=ProductCategoryId;
       
       $scope.loadProductCategoryDataExtra($scope.ProductCategoryId);
       $scope.getProductCategoryById($scope.ProductCategoryId);
    };
   $scope.getNewProductCategory= function()
    {
    	  $scope.getProductCategoryById(0);
    };
   $scope.getProductCategoryById= function(ProductCategoryId)
    {
        if(ProductCategoryId!=null && ProductCategoryId!=='')
        $scope.ProductCategoryId=ProductCategoryId;
        else return;
        
        $http.get($scope.getProductCategoryByIdUrl, {
            params: { "id": $scope.ProductCategoryId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.ProductCategory = result.Data;
                updateUrlQuery('id' , $scope.ProductCategory.Id);
                $scope.onAfterGetProductCategoryById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Product Category! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Product Category! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadProductCategoryDataExtra= function(ProductCategoryId)
    {
        if(ProductCategoryId!=null)
            $scope.ProductCategoryId=ProductCategoryId;
            
            $http.get($scope.getProductCategoryDataExtraUrl, {
                params: { "id": $scope.ProductCategoryId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.AssetTypeEnumList!=null)
                         $scope.AssetTypeEnumList = result.DataExtra.AssetTypeEnumList;
                      if(result.DataExtra.UnitTypeEnumList!=null)
                        $scope.UnitTypeEnumList = result.DataExtra.UnitTypeEnumList;
                      if (result.DataExtra.StoreList != null)
                        $scope.StoreList = result.DataExtra.StoreList;

                        $scope.onAfterLoadProductCategoryDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Product Category! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Product Category! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveProductCategory= function(){
    	if(!$scope.validateProductCategory())
        {
          return;
        }
        $http.post($scope.saveProductCategoryUrl + "/", $scope.ProductCategory).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.ProductCategory = result.Data;
                    updateUrlQuery('id', $scope.ProductCategory.Id);
                   }
                   alertSuccess("Successfully saved Product Category data!");
                   $scope.onAfterSaveProductCategory(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Product Category! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Product Category! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteProductCategoryById= function(){
    	
    };
   $scope.validateProductCategory= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (ProductCategoryId,getProductCategoryByIdUrl,getProductCategoryDataExtraUrl, saveProductCategoryUrl,deleteProductCategoryByIdUrl) {
        $scope.ProductCategoryId = ProductCategoryId;
        $scope.getProductCategoryByIdUrl = getProductCategoryByIdUrl;
        $scope.saveProductCategoryUrl = saveProductCategoryUrl;
        $scope.getProductCategoryDataExtraUrl = getProductCategoryDataExtraUrl;
        $scope.deleteProductCategoryByIdUrl = deleteProductCategoryByIdUrl;

        $scope.loadPage(ProductCategoryId);
    };
    
  $scope.onAfterSaveProductCategory= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetProductCategoryById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadProductCategoryDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
  /*
  //Child Table Bindings, need to fix
             $scope.CategoryIdList =  result.DataExtra.CategoryIdList; 

             $scope.ProductCategoryIdList =  result.DataExtra.ProductCategoryIdList; 

             $scope.CatagoryIdList =  result.DataExtra.CatagoryIdList; 

             $scope.ProductCatagoryIdList =  result.DataExtra.ProductCatagoryIdList; 
     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

