
//File: Inv_RequestedItem Anjular  Controller
emsApp.controller('RequestedItemAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.RequestedItem = [];
    $scope.RequestedItemId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
    $scope.loadPage = function (RequestedItemId)
    {
        if (RequestedItemId!=null)
            $scope.RequestedItemId =RequestedItemId;
       
        $scope.loadRequestedItemDataExtra($scope.RequestedItemId);
        $scope.getRequestedItemById($scope.RequestedItemId);
    };
    $scope.getNewRequestedItem= function()
    {
        $scope.getRequestedItemById(0);
    };
    $scope.getRequestedItemById = function (RequestedItemId)
    {
        if (RequestedItemId != null && RequestedItemId!=='')
            $scope.RequestedItemId =RequestedItemId;
        else return;
        
        $http.get($scope.getRequestedItemByIdUrl, {
            params: { "id": $scope.RequestedItemId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.RequestedItem = result.Data;
                updateUrlQuery('id', $scope.RequestedItem.Id);
                $scope.onAfterGetRequestedItemById(result);
            } else {
             $scope.HasError= true;
                $scope.ErrorMsg ="Unable to get Requested Item! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
            $scope.ErrorMsg ="Unable to get Requested Item! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
    $scope.loadRequestedItemDataExtra = function (RequestedItemId)
    {
        if (RequestedItemId!=null)
            $scope.RequestedItemId =RequestedItemId;
            
        $http.get($scope.getRequestedItemDataExtraUrl, {
            params: { "id": $scope.RequestedItemId }
            }).success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;

                    if (result.DataExtra.RequisitionList != null)
                        $scope.RequisitionList = result.DataExtra.RequisitionList;

                    if (result.DataExtra.CategoryList != null)
                        $scope.CategoryList = result.DataExtra.CategoryList;

                    $scope.onAfterLoadRequestedItemDataExtra(result);
                } else {
                 $scope.HasError= true;
                    $scope.ErrorMsg ="Unable to load option data for Requested Item! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                $scope.ErrorMsg ="Unable to load option data for Requested Item! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
    $scope.saveRequestedItem= function(){
        if (!$scope.validateRequestedItem())
        {
          return;
        }
        $http.post($scope.saveRequestedItemUrl + "/", $scope.RequestedItem).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.RequestedItem = result.Data;
                       updateUrlQuery('id', $scope.RequestedItem.Id);
                   }
                    alertSuccess("Successfully saved Requested Item data!");
                    $scope.onAfterSaveRequestedItem(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg ="Unable to save Requested Item! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg ="Unable to save Requested Item! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

    $scope.deleteRequestedItemById= function(){
    	
    };
    $scope.validateRequestedItem= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (RequestedItemId, getRequestedItemByIdUrl, getRequestedItemDataExtraUrl, saveRequestedItemUrl,deleteRequestedItemByIdUrl) {
        $scope.RequestedItemId = RequestedItemId;
        $scope.getRequestedItemByIdUrl = getRequestedItemByIdUrl;
        $scope.saveRequestedItemUrl = saveRequestedItemUrl;
        $scope.getRequestedItemDataExtraUrl = getRequestedItemDataExtraUrl;
        $scope.deleteRequestedItemByIdUrl = deleteRequestedItemByIdUrl;

        $scope.loadPage(RequestedItemId);
    };
    
    $scope.onAfterSaveRequestedItem= function(result){
     //var data=result.Data;
    };
    $scope.onAfterGetRequestedItemById= function(result){
    //var data=result.Data;
    
    };
    $scope.onAfterLoadRequestedItemDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
  /*
  //Child Table Bindings, need to fix
             $scope.RequestedItemIdList =  result.DataExtra.RequestedItemIdList;
     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

