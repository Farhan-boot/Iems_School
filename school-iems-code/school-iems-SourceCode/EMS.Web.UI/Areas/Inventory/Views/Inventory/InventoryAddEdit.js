
//File: Inv_Inventory Anjular  Controller
emsApp.controller('InventoryAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.Inventory = [];
    $scope.InventoryId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (InventoryId)
    {
       if(InventoryId!=null)
        $scope.InventoryId=InventoryId;
       
       $scope.loadInventoryDataExtra($scope.InventoryId);
       $scope.getInventoryById($scope.InventoryId);
    };
   $scope.getNewInventory= function()
    {
    	  $scope.getInventoryById(0);
    };
   $scope.getInventoryById= function(InventoryId)
    {
        if(InventoryId!=null && InventoryId!=='')
        $scope.InventoryId=InventoryId;
        else return;
        
        $http.get($scope.getInventoryByIdUrl, {
            params: { "id": $scope.InventoryId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.Inventory = result.Data;
                updateUrlQuery('id' , $scope.Inventory.Id);
                $scope.onAfterGetInventoryById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Inventory! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Inventory! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadInventoryDataExtra= function(InventoryId)
    {
        if(InventoryId!=null)
            $scope.InventoryId=InventoryId;
            
            $http.get($scope.getInventoryDataExtraUrl, {
                params: { "id": $scope.InventoryId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                        $scope.onAfterLoadInventoryDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Inventory! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Inventory! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveInventory= function(){
    	if(!$scope.validateInventory())
        {
          return;
        }
        $http.post($scope.saveInventoryUrl + "/", $scope.Inventory).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.Inventory = result.Data;
                    updateUrlQuery('id', $scope.Inventory.Id);
                   }
                   alertSuccess("Successfully saved Inventory data!");
                   $scope.onAfterSaveInventory(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Inventory! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Inventory! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteInventoryById= function(){
    	
    };
   $scope.validateInventory= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (InventoryId,getInventoryByIdUrl,getInventoryDataExtraUrl, saveInventoryUrl,deleteInventoryByIdUrl) {
        $scope.InventoryId = InventoryId;
        $scope.getInventoryByIdUrl = getInventoryByIdUrl;
        $scope.saveInventoryUrl = saveInventoryUrl;
        $scope.getInventoryDataExtraUrl = getInventoryDataExtraUrl;
        $scope.deleteInventoryByIdUrl = deleteInventoryByIdUrl;

        $scope.loadPage(InventoryId);
    };
    
  $scope.onAfterSaveInventory= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetInventoryById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadInventoryDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
         if(result.DataExtra.SupplierList!=null)
         $scope.SupplierList =  result.DataExtra.SupplierList; 
  /*
  //Child Table Bindings, need to fix
             $scope.InventoryIdList =  result.DataExtra.InventoryIdList; 

             $scope.InventoryIdList =  result.DataExtra.InventoryIdList; 

             $scope.InventoryIdList =  result.DataExtra.InventoryIdList; 

             $scope.InventoryIdList =  result.DataExtra.InventoryIdList; 
     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

