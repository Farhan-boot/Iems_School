
//File: Invt_ItemRequest Anjular  Controller
emsApp.controller('ItemRequestAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.ItemRequest = [];
    $scope.ItemRequestId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (ItemRequestId)
    {
       if(ItemRequestId!=null)
        $scope.ItemRequestId=ItemRequestId;
       
       $scope.loadItemRequestDataExtra($scope.ItemRequestId);
       $scope.getItemRequestById($scope.ItemRequestId);
    };
   $scope.getNewItemRequest= function()
    {
    	  $scope.getItemRequestById(0);
    };
   $scope.getItemRequestById= function(ItemRequestId)
    {
        if(ItemRequestId!=null && ItemRequestId!=='')
        $scope.ItemRequestId=ItemRequestId;
        else return;
        
        $http.get($scope.getItemRequestByIdUrl, {
            params: { "id": $scope.ItemRequestId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.ItemRequest = result.Data;
                updateUrlQuery('id' , $scope.ItemRequest.Id);
                $scope.onAfterGetItemRequestById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Item Request! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Item Request! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadItemRequestDataExtra= function(ItemRequestId)
    {
        if(ItemRequestId!=null)
            $scope.ItemRequestId=ItemRequestId;
            
            $http.get($scope.getItemRequestDataExtraUrl, {
                params: { "id": $scope.ItemRequestId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.RequestTypeEnumList!=null)
                         $scope.RequestTypeEnumList = result.DataExtra.RequestTypeEnumList;
                      if(result.DataExtra.UnitTypeEnumList!=null)
                         $scope.UnitTypeEnumList = result.DataExtra.UnitTypeEnumList;
                      if(result.DataExtra.StatusEnumList!=null)
                        $scope.StatusEnumList = result.DataExtra.StatusEnumList;
                    if (result.DataExtra.ItemList != null)
                        $scope.ItemList = result.DataExtra.ItemList;

                        $scope.onAfterLoadItemRequestDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Item Request! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Item Request! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveItemRequest= function(){
    	if(!$scope.validateItemRequest())
        {
          return;
        }
        $http.post($scope.saveItemRequestUrl + "/", $scope.ItemRequest).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.ItemRequest = result.Data;
                    updateUrlQuery('id', $scope.ItemRequest.Id);
                   }
                   alertSuccess("Successfully saved Item Request data!");
                   $scope.onAfterSaveItemRequest(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Item Request! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Item Request! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteItemRequestById= function(){
    	
    };
   $scope.validateItemRequest= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (ItemRequestId,getItemRequestByIdUrl,getItemRequestDataExtraUrl, saveItemRequestUrl,deleteItemRequestByIdUrl) {
        $scope.ItemRequestId = ItemRequestId;
        $scope.getItemRequestByIdUrl = getItemRequestByIdUrl;
        $scope.saveItemRequestUrl = saveItemRequestUrl;
        $scope.getItemRequestDataExtraUrl = getItemRequestDataExtraUrl;
        $scope.deleteItemRequestByIdUrl = deleteItemRequestByIdUrl;

        $scope.loadPage(ItemRequestId);
    };
    
  $scope.onAfterSaveItemRequest= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetItemRequestById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadItemRequestDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
  /*
  //Child Table Bindings, need to fix     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

