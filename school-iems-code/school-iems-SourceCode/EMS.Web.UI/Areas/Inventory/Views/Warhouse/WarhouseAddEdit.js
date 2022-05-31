
//File: Invt_Warhouse Anjular  Controller
emsApp.controller('WarhouseAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.Warhouse = [];
    $scope.WarhouseId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (WarhouseId)
    {
       if(WarhouseId!=null)
        $scope.WarhouseId=WarhouseId;
       
       $scope.loadWarhouseDataExtra($scope.WarhouseId);
       $scope.getWarhouseById($scope.WarhouseId);
    };
   $scope.getNewWarhouse= function()
    {
    	  $scope.getWarhouseById(0);
    };
   $scope.getWarhouseById= function(WarhouseId)
    {
        if(WarhouseId!=null && WarhouseId!=='')
        $scope.WarhouseId=WarhouseId;
        else return;
        
        $http.get($scope.getWarhouseByIdUrl, {
            params: { "id": $scope.WarhouseId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.Warhouse = result.Data;
                updateUrlQuery('id' , $scope.Warhouse.Id);
                $scope.onAfterGetWarhouseById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Warhouse! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Warhouse! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadWarhouseDataExtra= function(WarhouseId)
    {
        if(WarhouseId!=null)
            $scope.WarhouseId=WarhouseId;
            
            $http.get($scope.getWarhouseDataExtraUrl, {
                params: { "id": $scope.WarhouseId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                        $scope.onAfterLoadWarhouseDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Warhouse! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Warhouse! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveWarhouse= function(){
    	if(!$scope.validateWarhouse())
        {
          return;
        }
        $http.post($scope.saveWarhouseUrl + "/", $scope.Warhouse).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.Warhouse = result.Data;
                    updateUrlQuery('id', $scope.Warhouse.Id);
                   }
                   alertSuccess("Successfully saved Warhouse data!");
                   $scope.onAfterSaveWarhouse(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Warhouse! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Warhouse! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteWarhouseById= function(){
    	
    };
   $scope.validateWarhouse= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (WarhouseId,getWarhouseByIdUrl,getWarhouseDataExtraUrl, saveWarhouseUrl,deleteWarhouseByIdUrl) {
        $scope.WarhouseId = WarhouseId;
        $scope.getWarhouseByIdUrl = getWarhouseByIdUrl;
        $scope.saveWarhouseUrl = saveWarhouseUrl;
        $scope.getWarhouseDataExtraUrl = getWarhouseDataExtraUrl;
        $scope.deleteWarhouseByIdUrl = deleteWarhouseByIdUrl;

        $scope.loadPage(WarhouseId);
    };
    
  $scope.onAfterSaveWarhouse= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetWarhouseById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadWarhouseDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
  /*
  //Child Table Bindings, need to fix
             $scope.WarhouseIdList =  result.DataExtra.WarhouseIdList; 
     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

