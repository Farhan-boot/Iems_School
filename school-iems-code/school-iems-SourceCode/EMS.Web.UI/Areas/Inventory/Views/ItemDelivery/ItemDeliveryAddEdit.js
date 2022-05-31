
//File: Inv_ItemDelivery Anjular  Controller
emsApp.controller('ItemDeliveryAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.ItemDelivery = [];
    $scope.ItemDeliveryId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
    $scope.loadPage = function (ItemDeliveryId)
    {
        if (ItemDeliveryId!=null)
            $scope.ItemDeliveryId =ItemDeliveryId;
       
        $scope.loadItemDeliveryDataExtra($scope.ItemDeliveryId);
        $scope.getItemDeliveryById($scope.ItemDeliveryId);
    };
    $scope.getNewItemDelivery= function()
    {
        $scope.getItemDeliveryById(0);
    };
    $scope.getItemDeliveryById = function (ItemDeliveryId)
    {
        if (ItemDeliveryId != null && ItemDeliveryId!=='')
            $scope.ItemDeliveryId =ItemDeliveryId;
        else return;
        
        $http.get($scope.getItemDeliveryByIdUrl, {
            params: { "id": $scope.ItemDeliveryId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.ItemDelivery = result.Data;
                updateUrlQuery('id', $scope.ItemDelivery.Id);
                $scope.onAfterGetItemDeliveryById(result);
            } else {
             $scope.HasError= true;
                $scope.ErrorMsg ="Unable to get Item Delivery! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
            $scope.ErrorMsg ="Unable to get Item Delivery! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
    $scope.loadItemDeliveryDataExtra = function (ItemDeliveryId)
    {
        if (ItemDeliveryId!=null)
            $scope.ItemDeliveryId =ItemDeliveryId;
            
        $http.get($scope.getItemDeliveryDataExtraUrl, {
            params: { "id": $scope.ItemDeliveryId }
            }).success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;

                    if (result.DataExtra.DeliveredList != null)
                        $scope.DeliveredList = result.DataExtra.DeliveredList;

                    if (result.DataExtra.DepartmentList != null)
                        $scope.DepartmentList = result.DataExtra.DepartmentList;

                    $scope.onAfterLoadItemDeliveryDataExtra(result);
                } else {
                 $scope.HasError= true;
                    $scope.ErrorMsg ="Unable to load option data for Item Delivery! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                $scope.ErrorMsg ="Unable to load option data for Item Delivery! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
    $scope.saveItemDelivery= function(){
        if (!$scope.validateItemDelivery())
        {
          return;
        }
        $http.post($scope.saveItemDeliveryUrl + "/", $scope.ItemDelivery).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.ItemDelivery = result.Data;
                       updateUrlQuery('id', $scope.ItemDelivery.Id);
                   }
                    alertSuccess("Successfully saved Item Delivery data!");
                    $scope.onAfterSaveItemDelivery(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg ="Unable to save Item Delivery! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg ="Unable to save Item Delivery! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

    $scope.deleteItemDeliveryById= function(){
    	
    };
    $scope.validateItemDelivery= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (ItemDeliveryId, getItemDeliveryByIdUrl, getItemDeliveryDataExtraUrl, saveItemDeliveryUrl,deleteItemDeliveryByIdUrl) {
        $scope.ItemDeliveryId = ItemDeliveryId;
        $scope.getItemDeliveryByIdUrl = getItemDeliveryByIdUrl;
        $scope.saveItemDeliveryUrl = saveItemDeliveryUrl;
        $scope.getItemDeliveryDataExtraUrl = getItemDeliveryDataExtraUrl;
        $scope.deleteItemDeliveryByIdUrl = deleteItemDeliveryByIdUrl;

        $scope.loadPage(ItemDeliveryId);
    };
    
    $scope.onAfterSaveItemDelivery= function(result){
     //var data=result.Data;
    };
    $scope.onAfterGetItemDeliveryById= function(result){
    //var data=result.Data;
    
    };
    $scope.onAfterLoadItemDeliveryDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
  /*
  //Child Table Bindings, need to fix
             $scope.ItemDeliveryIdList =  result.DataExtra.ItemDeliveryIdList;
     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

