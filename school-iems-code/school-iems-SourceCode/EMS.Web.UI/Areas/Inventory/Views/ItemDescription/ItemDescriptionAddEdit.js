
//File: Inv_ItemDescription Anjular  Controller
emsApp.controller('ItemDescriptionAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.ItemDescription = [];
    $scope.ItemDescriptionId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (ItemDescriptionId)
    {
       if(ItemDescriptionId!=null)
        $scope.ItemDescriptionId=ItemDescriptionId;
       
       $scope.loadItemDescriptionDataExtra($scope.ItemDescriptionId);
       $scope.getItemDescriptionById($scope.ItemDescriptionId);
    };
   $scope.getNewItemDescription= function()
    {
    	  $scope.getItemDescriptionById(0);
    };
   $scope.getItemDescriptionById= function(ItemDescriptionId)
    {
        if(ItemDescriptionId!=null && ItemDescriptionId!=='')
        $scope.ItemDescriptionId=ItemDescriptionId;
        else return;
        
        $http.get($scope.getItemDescriptionByIdUrl, {
            params: { "id": $scope.ItemDescriptionId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.ItemDescription = result.Data;
                updateUrlQuery('id' , $scope.ItemDescription.Id);
                $scope.onAfterGetItemDescriptionById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Item Description! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Item Description! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadItemDescriptionDataExtra= function(ItemDescriptionId)
    {
        if(ItemDescriptionId!=null)
            $scope.ItemDescriptionId=ItemDescriptionId;
            
            $http.get($scope.getItemDescriptionDataExtraUrl, {
                params: { "id": $scope.ItemDescriptionId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                        $scope.onAfterLoadItemDescriptionDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Item Description! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Item Description! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveItemDescription= function(){
    	if(!$scope.validateItemDescription())
        {
          return;
        }
        $http.post($scope.saveItemDescriptionUrl + "/", $scope.ItemDescription).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.ItemDescription = result.Data;
                    updateUrlQuery('id', $scope.ItemDescription.Id);
                   }
                   alertSuccess("Successfully saved Item Description data!");
                   $scope.onAfterSaveItemDescription(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Item Description! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Item Description! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteItemDescriptionById= function(){
    	
    };
   $scope.validateItemDescription= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (ItemDescriptionId,getItemDescriptionByIdUrl,getItemDescriptionDataExtraUrl, saveItemDescriptionUrl,deleteItemDescriptionByIdUrl) {
        $scope.ItemDescriptionId = ItemDescriptionId;
        $scope.getItemDescriptionByIdUrl = getItemDescriptionByIdUrl;
        $scope.saveItemDescriptionUrl = saveItemDescriptionUrl;
        $scope.getItemDescriptionDataExtraUrl = getItemDescriptionDataExtraUrl;
        $scope.deleteItemDescriptionByIdUrl = deleteItemDescriptionByIdUrl;

        $scope.loadPage(ItemDescriptionId);
    };
    
  $scope.onAfterSaveItemDescription= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetItemDescriptionById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadItemDescriptionDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
  /*
  //Child Table Bindings, need to fix     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

