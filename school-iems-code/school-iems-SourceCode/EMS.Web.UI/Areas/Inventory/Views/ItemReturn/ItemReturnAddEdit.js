
//File: Inv_ItemReturn Anjular  Controller
emsApp.controller('ItemReturnAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.ItemReturn = [];
    $scope.ItemReturnId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (ItemReturnId)
    {
       if(ItemReturnId!=null)
        $scope.ItemReturnId=ItemReturnId;
       
       $scope.loadItemReturnDataExtra($scope.ItemReturnId);
       $scope.getItemReturnById($scope.ItemReturnId);
    };
   $scope.getNewItemReturn= function()
    {
    	  $scope.getItemReturnById(0);
    };
   $scope.getItemReturnById= function(ItemReturnId)
    {
        if(ItemReturnId!=null && ItemReturnId!=='')
        $scope.ItemReturnId=ItemReturnId;
        else return;
        
        $http.get($scope.getItemReturnByIdUrl, {
            params: { "id": $scope.ItemReturnId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.ItemReturn = result.Data;
                updateUrlQuery('id' , $scope.ItemReturn.Id);
                $scope.onAfterGetItemReturnById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Item Return! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Item Return! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadItemReturnDataExtra= function(ItemReturnId)
    {
        if(ItemReturnId!=null)
            $scope.ItemReturnId=ItemReturnId;
            
            $http.get($scope.getItemReturnDataExtraUrl, {
                params: { "id": $scope.ItemReturnId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                        $scope.onAfterLoadItemReturnDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Item Return! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Item Return! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveItemReturn= function(){
    	if(!$scope.validateItemReturn())
        {
          return;
        }
        $http.post($scope.saveItemReturnUrl + "/", $scope.ItemReturn).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.ItemReturn = result.Data;
                    updateUrlQuery('id', $scope.ItemReturn.Id);
                   }
                   alertSuccess("Successfully saved Item Return data!");
                   $scope.onAfterSaveItemReturn(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Item Return! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Item Return! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteItemReturnById= function(){
    	
    };
   $scope.validateItemReturn= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (ItemReturnId,getItemReturnByIdUrl,getItemReturnDataExtraUrl, saveItemReturnUrl,deleteItemReturnByIdUrl) {
        $scope.ItemReturnId = ItemReturnId;
        $scope.getItemReturnByIdUrl = getItemReturnByIdUrl;
        $scope.saveItemReturnUrl = saveItemReturnUrl;
        $scope.getItemReturnDataExtraUrl = getItemReturnDataExtraUrl;
        $scope.deleteItemReturnByIdUrl = deleteItemReturnByIdUrl;

        $scope.loadPage(ItemReturnId);
    };
    
  $scope.onAfterSaveItemReturn= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetItemReturnById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadItemReturnDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
  /*
  //Child Table Bindings, need to fix
             $scope.ItemReturnIdList =  result.DataExtra.ItemReturnIdList; 
     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

