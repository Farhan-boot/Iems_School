
//File: Inv_ItemMaintainance Anjular  Controller
emsApp.controller('ItemMaintainanceAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.ItemMaintainance = [];
    $scope.ItemMaintainanceId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (ItemMaintainanceId)
    {
       if(ItemMaintainanceId!=null)
        $scope.ItemMaintainanceId=ItemMaintainanceId;
       
       $scope.loadItemMaintainanceDataExtra($scope.ItemMaintainanceId);
       $scope.getItemMaintainanceById($scope.ItemMaintainanceId);
    };
   $scope.getNewItemMaintainance= function()
    {
    	  $scope.getItemMaintainanceById(0);
    };
   $scope.getItemMaintainanceById= function(ItemMaintainanceId)
    {
        if(ItemMaintainanceId!=null && ItemMaintainanceId!=='')
        $scope.ItemMaintainanceId=ItemMaintainanceId;
        else return;
        
        $http.get($scope.getItemMaintainanceByIdUrl, {
            params: { "id": $scope.ItemMaintainanceId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.ItemMaintainance = result.Data;
                updateUrlQuery('id' , $scope.ItemMaintainance.Id);
                $scope.onAfterGetItemMaintainanceById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Item Maintainance! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Item Maintainance! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadItemMaintainanceDataExtra= function(ItemMaintainanceId)
    {
        if(ItemMaintainanceId!=null)
            $scope.ItemMaintainanceId=ItemMaintainanceId;
            
            $http.get($scope.getItemMaintainanceDataExtraUrl, {
                params: { "id": $scope.ItemMaintainanceId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                        $scope.onAfterLoadItemMaintainanceDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Item Maintainance! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Item Maintainance! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveItemMaintainance= function(){
    	if(!$scope.validateItemMaintainance())
        {
          return;
        }
        $http.post($scope.saveItemMaintainanceUrl + "/", $scope.ItemMaintainance).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.ItemMaintainance = result.Data;
                    updateUrlQuery('id', $scope.ItemMaintainance.Id);
                   }
                   alertSuccess("Successfully saved Item Maintainance data!");
                   $scope.onAfterSaveItemMaintainance(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Item Maintainance! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Item Maintainance! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteItemMaintainanceById= function(){
    	
    };
   $scope.validateItemMaintainance= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (ItemMaintainanceId,getItemMaintainanceByIdUrl,getItemMaintainanceDataExtraUrl, saveItemMaintainanceUrl,deleteItemMaintainanceByIdUrl) {
        $scope.ItemMaintainanceId = ItemMaintainanceId;
        $scope.getItemMaintainanceByIdUrl = getItemMaintainanceByIdUrl;
        $scope.saveItemMaintainanceUrl = saveItemMaintainanceUrl;
        $scope.getItemMaintainanceDataExtraUrl = getItemMaintainanceDataExtraUrl;
        $scope.deleteItemMaintainanceByIdUrl = deleteItemMaintainanceByIdUrl;

        $scope.loadPage(ItemMaintainanceId);
    };
    
  $scope.onAfterSaveItemMaintainance= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetItemMaintainanceById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadItemMaintainanceDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
  /*
  //Child Table Bindings, need to fix     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

