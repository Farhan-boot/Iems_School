
//File: Inv_Item Anjular  Controller
emsApp.controller('ItemAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.Item = [];
    $scope.ItemId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (ItemId)
    {
       if(ItemId!=null)
        $scope.ItemId=ItemId;
       
       $scope.loadItemDataExtra($scope.ItemId);
       $scope.getItemById($scope.ItemId);
    };
   $scope.getNewItem= function()
    {
    	  $scope.getItemById(0);
    };
   $scope.getItemById= function(ItemId)
    {
        if(ItemId!=null && ItemId!=='')
        $scope.ItemId=ItemId;
        else return;
        
        $http.get($scope.getItemByIdUrl, {
            params: { "id": $scope.ItemId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.Item = result.Data;
                updateUrlQuery('id' , $scope.Item.Id);
                $scope.onAfterGetItemById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Item! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Item! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadItemDataExtra= function(ItemId)
    {
        if(ItemId!=null)
            $scope.ItemId=ItemId;
            
            $http.get($scope.getItemDataExtraUrl, {
                params: { "id": $scope.ItemId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.AssetTypeEnumList!=null)
                         $scope.AssetTypeEnumList = result.DataExtra.AssetTypeEnumList;
                        $scope.onAfterLoadItemDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Item! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Item! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveItem= function(){
    	if(!$scope.validateItem())
        {
          return;
        }
        $http.post($scope.saveItemUrl + "/", $scope.Item).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.Item = result.Data;
                    updateUrlQuery('id', $scope.Item.Id);
                   }
                   alertSuccess("Successfully saved Item data!");
                   $scope.onAfterSaveItem(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Item! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Item! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteItemById= function(){
    	
    };
   $scope.validateItem= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (ItemId,getItemByIdUrl,getItemDataExtraUrl, saveItemUrl,deleteItemByIdUrl) {
        $scope.ItemId = ItemId;
        $scope.getItemByIdUrl = getItemByIdUrl;
        $scope.saveItemUrl = saveItemUrl;
        $scope.getItemDataExtraUrl = getItemDataExtraUrl;
        $scope.deleteItemByIdUrl = deleteItemByIdUrl;

        $scope.loadPage(ItemId);
    };
    
  $scope.onAfterSaveItem= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetItemById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadItemDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
         if(result.DataExtra.ProductCategoryList!=null)
         $scope.ProductCategoryList =  result.DataExtra.ProductCategoryList; 
         if(result.DataExtra.StoreList!=null)
         $scope.StoreList =  result.DataExtra.StoreList; 
  /*
  //Child Table Bindings, need to fix     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

