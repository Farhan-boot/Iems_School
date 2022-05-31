
//File: Inv_ItemAllocation Anjular  Controller
emsApp.controller('ItemAllocationAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.ItemAllocation = [];
    $scope.ItemAllocationId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (ItemAllocationId)
    {
       if(ItemAllocationId!=null)
        $scope.ItemAllocationId=ItemAllocationId;
       
       $scope.loadItemAllocationDataExtra($scope.ItemAllocationId);
       $scope.getItemAllocationById($scope.ItemAllocationId);
    };
   $scope.getNewItemAllocation= function()
    {
    	  $scope.getItemAllocationById(0);
    };
   $scope.getItemAllocationById= function(ItemAllocationId)
    {
        if(ItemAllocationId!=null && ItemAllocationId!=='')
        $scope.ItemAllocationId=ItemAllocationId;
        else return;
        
        $http.get($scope.getItemAllocationByIdUrl, {
            params: { "id": $scope.ItemAllocationId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.ItemAllocation = result.Data;
                updateUrlQuery('id' , $scope.ItemAllocation.Id);
                $scope.onAfterGetItemAllocationById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Item Allocation! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Item Allocation! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadItemAllocationDataExtra= function(ItemAllocationId)
    {
        if(ItemAllocationId!=null)
            $scope.ItemAllocationId=ItemAllocationId;
            
            $http.get($scope.getItemAllocationDataExtraUrl, {
                params: { "id": $scope.ItemAllocationId }
            }).success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    //New Data Extra
                    if (result.DataExtra.ItemList != null)
                        $scope.ItemList = result.DataExtra.ItemList;
                    if (result.DataExtra.StoreList != null)
                        $scope.StoreList = result.DataExtra.StoreList;
                    if (result.DataExtra.UserList != null)
                        $scope.UserList = result.DataExtra.UserList;

                        $scope.onAfterLoadItemAllocationDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Item Allocation! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Item Allocation! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveItemAllocation= function(){
    	if(!$scope.validateItemAllocation())
        {
          return;
        }
        $http.post($scope.saveItemAllocationUrl + "/", $scope.ItemAllocation).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.ItemAllocation = result.Data;
                    updateUrlQuery('id', $scope.ItemAllocation.Id);
                   }
                   alertSuccess("Successfully saved Item Allocation data!");
                   $scope.onAfterSaveItemAllocation(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Item Allocation! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Item Allocation! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteItemAllocationById= function(){
    	
    };
   $scope.validateItemAllocation= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (ItemAllocationId,getItemAllocationByIdUrl,getItemAllocationDataExtraUrl, saveItemAllocationUrl,deleteItemAllocationByIdUrl) {
        $scope.ItemAllocationId = ItemAllocationId;
        $scope.getItemAllocationByIdUrl = getItemAllocationByIdUrl;
        $scope.saveItemAllocationUrl = saveItemAllocationUrl;
        $scope.getItemAllocationDataExtraUrl = getItemAllocationDataExtraUrl;
        $scope.deleteItemAllocationByIdUrl = deleteItemAllocationByIdUrl;

        $scope.loadPage(ItemAllocationId);
    };
    
  $scope.onAfterSaveItemAllocation= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetItemAllocationById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadItemAllocationDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
         if(result.DataExtra.ItemAllocationList!=null)
         $scope.ItemAllocationList =  result.DataExtra.ItemAllocationList; 
  /*
  //Child Table Bindings, need to fix
             $scope.IdList =  result.DataExtra.IdList; 

             $scope.ItemAllocationIdList =  result.DataExtra.ItemAllocationIdList; 

             $scope.ItemAllocationIdList =  result.DataExtra.ItemAllocationIdList; 
     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

