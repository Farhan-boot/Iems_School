
//File: Inv_InventoryDetails Anjular  Controller
emsApp.controller('InventoryDetailsAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.InventoryDetails = [];
    $scope.InventoryDetailsId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (InventoryDetailsId)
    {
       if(InventoryDetailsId!=null)
        $scope.InventoryDetailsId=InventoryDetailsId;
       
       $scope.loadInventoryDetailsDataExtra($scope.InventoryDetailsId);
       $scope.getInventoryDetailsById($scope.InventoryDetailsId);
    };
   $scope.getNewInventoryDetails= function()
    {
    	  $scope.getInventoryDetailsById(0);
    };
   $scope.getInventoryDetailsById= function(InventoryDetailsId)
    {
        if(InventoryDetailsId!=null && InventoryDetailsId!=='')
        $scope.InventoryDetailsId=InventoryDetailsId;
        else return;
        
        $http.get($scope.getInventoryDetailsByIdUrl, {
            params: { "id": $scope.InventoryDetailsId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.InventoryDetails = result.Data;
                updateUrlQuery('id' , $scope.InventoryDetails.Id);
                $scope.onAfterGetInventoryDetailsById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Inventory Details! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Inventory Details! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadInventoryDetailsDataExtra= function(InventoryDetailsId)
    {
        if(InventoryDetailsId!=null)
            $scope.InventoryDetailsId=InventoryDetailsId;
            
            $http.get($scope.getInventoryDetailsDataExtraUrl, {
                params: { "id": $scope.InventoryDetailsId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.StatusEnumList!=null)
                         $scope.StatusEnumList = result.DataExtra.StatusEnumList;
                        $scope.onAfterLoadInventoryDetailsDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Inventory Details! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Inventory Details! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveInventoryDetails= function(){
    	if(!$scope.validateInventoryDetails())
        {
          return;
        }
        $http.post($scope.saveInventoryDetailsUrl + "/", $scope.InventoryDetails).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.InventoryDetails = result.Data;
                    updateUrlQuery('id', $scope.InventoryDetails.Id);
                   }
                   alertSuccess("Successfully saved Inventory Details data!");
                   $scope.onAfterSaveInventoryDetails(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Inventory Details! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Inventory Details! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteInventoryDetailsById= function(){
    	
    };
   $scope.validateInventoryDetails= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (InventoryDetailsId,getInventoryDetailsByIdUrl,getInventoryDetailsDataExtraUrl, saveInventoryDetailsUrl,deleteInventoryDetailsByIdUrl) {
        $scope.InventoryDetailsId = InventoryDetailsId;
        $scope.getInventoryDetailsByIdUrl = getInventoryDetailsByIdUrl;
        $scope.saveInventoryDetailsUrl = saveInventoryDetailsUrl;
        $scope.getInventoryDetailsDataExtraUrl = getInventoryDetailsDataExtraUrl;
        $scope.deleteInventoryDetailsByIdUrl = deleteInventoryDetailsByIdUrl;

        $scope.loadPage(InventoryDetailsId);
    };
    
  $scope.onAfterSaveInventoryDetails= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetInventoryDetailsById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadInventoryDetailsDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
         if(result.DataExtra.InventoryList!=null)
         $scope.InventoryList =  result.DataExtra.InventoryList; 
  /*
  //Child Table Bindings, need to fix     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

