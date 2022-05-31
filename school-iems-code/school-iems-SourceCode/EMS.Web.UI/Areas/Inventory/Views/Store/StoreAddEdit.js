
//File: Inv_Store Anjular  Controller
emsApp.controller('StoreAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.Store = [];
    $scope.StoreId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (StoreId)
    {
       if(StoreId!=null)
        $scope.StoreId=StoreId;
       
       $scope.loadStoreDataExtra($scope.StoreId);
       $scope.getStoreById($scope.StoreId);
    };
   $scope.getNewStore= function()
    {
    	  $scope.getStoreById(0);
    };
   $scope.getStoreById= function(StoreId)
    {
        if(StoreId!=null && StoreId!=='')
        $scope.StoreId=StoreId;
        else return;
        
        $http.get($scope.getStoreByIdUrl, {
            params: { "id": $scope.StoreId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.Store = result.Data;
                updateUrlQuery('id' , $scope.Store.Id);
                $scope.onAfterGetStoreById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Store! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Store! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadStoreDataExtra= function(StoreId)
    {
        if(StoreId!=null)
            $scope.StoreId=StoreId;
            
            $http.get($scope.getStoreDataExtraUrl, {
                params: { "id": $scope.StoreId }
            }).success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    if (result.DataExtra.RoomList != null)
                        $scope.RoomList = result.DataExtra.RoomList;
                    $scope.onAfterLoadStoreDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Store! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Store! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveStore= function(){
    	if(!$scope.validateStore())
        {
          return;
        }
        $http.post($scope.saveStoreUrl + "/", $scope.Store).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.Store = result.Data;
                    updateUrlQuery('id', $scope.Store.Id);
                   }
                   alertSuccess("Successfully saved Store data!");
                   $scope.onAfterSaveStore(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Store! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Store! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteStoreById= function(){
    	
    };
   $scope.validateStore= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (StoreId,getStoreByIdUrl,getStoreDataExtraUrl, saveStoreUrl,deleteStoreByIdUrl) {
        $scope.StoreId = StoreId;
        $scope.getStoreByIdUrl = getStoreByIdUrl;
        $scope.saveStoreUrl = saveStoreUrl;
        $scope.getStoreDataExtraUrl = getStoreDataExtraUrl;
        $scope.deleteStoreByIdUrl = deleteStoreByIdUrl;

        $scope.loadPage(StoreId);
    };
    
  $scope.onAfterSaveStore= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetStoreById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadStoreDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
  /*
  //Child Table Bindings, need to fix
             $scope.StoreIdList =  result.DataExtra.StoreIdList; 
     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

