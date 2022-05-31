
//File: Inv_ItemWarrentyDetails Anjular  Controller
emsApp.controller('ItemWarrentyDetailsAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.ItemWarrentyDetails = [];
    $scope.ItemWarrentyDetailsId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (ItemWarrentyDetailsId)
    {
       if(ItemWarrentyDetailsId!=null)
        $scope.ItemWarrentyDetailsId=ItemWarrentyDetailsId;
       
       $scope.loadItemWarrentyDetailsDataExtra($scope.ItemWarrentyDetailsId);
       $scope.getItemWarrentyDetailsById($scope.ItemWarrentyDetailsId);
    };
   $scope.getNewItemWarrentyDetails= function()
    {
    	  $scope.getItemWarrentyDetailsById(0);
    };
   $scope.getItemWarrentyDetailsById= function(ItemWarrentyDetailsId)
    {
        if(ItemWarrentyDetailsId!=null && ItemWarrentyDetailsId!=='')
        $scope.ItemWarrentyDetailsId=ItemWarrentyDetailsId;
        else return;
        
        $http.get($scope.getItemWarrentyDetailsByIdUrl, {
            params: { "id": $scope.ItemWarrentyDetailsId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.ItemWarrentyDetails = result.Data;
                updateUrlQuery('id' , $scope.ItemWarrentyDetails.Id);
                $scope.onAfterGetItemWarrentyDetailsById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Item Warrenty Details! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Item Warrenty Details! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadItemWarrentyDetailsDataExtra= function(ItemWarrentyDetailsId)
    {
        if(ItemWarrentyDetailsId!=null)
            $scope.ItemWarrentyDetailsId=ItemWarrentyDetailsId;
            
            $http.get($scope.getItemWarrentyDetailsDataExtraUrl, {
                params: { "id": $scope.ItemWarrentyDetailsId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                        $scope.onAfterLoadItemWarrentyDetailsDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Item Warrenty Details! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Item Warrenty Details! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveItemWarrentyDetails= function(){
    	if(!$scope.validateItemWarrentyDetails())
        {
          return;
        }
        $http.post($scope.saveItemWarrentyDetailsUrl + "/", $scope.ItemWarrentyDetails).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.ItemWarrentyDetails = result.Data;
                    updateUrlQuery('id', $scope.ItemWarrentyDetails.Id);
                   }
                   alertSuccess("Successfully saved Item Warrenty Details data!");
                   $scope.onAfterSaveItemWarrentyDetails(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Item Warrenty Details! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Item Warrenty Details! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteItemWarrentyDetailsById= function(){
    	
    };
   $scope.validateItemWarrentyDetails= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (ItemWarrentyDetailsId,getItemWarrentyDetailsByIdUrl,getItemWarrentyDetailsDataExtraUrl, saveItemWarrentyDetailsUrl,deleteItemWarrentyDetailsByIdUrl) {
        $scope.ItemWarrentyDetailsId = ItemWarrentyDetailsId;
        $scope.getItemWarrentyDetailsByIdUrl = getItemWarrentyDetailsByIdUrl;
        $scope.saveItemWarrentyDetailsUrl = saveItemWarrentyDetailsUrl;
        $scope.getItemWarrentyDetailsDataExtraUrl = getItemWarrentyDetailsDataExtraUrl;
        $scope.deleteItemWarrentyDetailsByIdUrl = deleteItemWarrentyDetailsByIdUrl;

        $scope.loadPage(ItemWarrentyDetailsId);
    };
    
  $scope.onAfterSaveItemWarrentyDetails= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetItemWarrentyDetailsById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadItemWarrentyDetailsDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
  /*
  //Child Table Bindings, need to fix     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

