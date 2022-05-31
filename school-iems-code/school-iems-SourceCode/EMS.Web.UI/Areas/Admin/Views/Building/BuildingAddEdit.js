
//File: General_Building Anjular  Controller
emsApp.controller('BuildingAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.Building = [];
    $scope.BuildingId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;

   $scope.loadPage= function (BuildingId)
    {
       if(BuildingId!=null)
        $scope.BuildingId=BuildingId;
       
       $scope.loadBuildingExtraData($scope.BuildingId);
       $scope.getBuildingById($scope.BuildingId);
    };
   $scope.getNewBuilding= function()
    {
    	  $scope.getBuildingById(0);
    };
   $scope.getBuildingById= function(BuildingId)
    {
        if(BuildingId!=null && BuildingId!=='')
        $scope.BuildingId=BuildingId;
        else return;
        
        $http.get($scope.getBuildingByIdUrl, {
            params: { "id": $scope.BuildingId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.Building = result.Data;
                updateUrlQuery('id' , $scope.Building.Id);
                $scope.onAfterGetBuildingById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Building! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Building! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadBuildingExtraData= function(BuildingId)
    {
        if(BuildingId!=null)
            $scope.BuildingId=BuildingId;
            
            $http.get($scope.getBuildingExtraDataUrl, {
                params: { "id": $scope.BuildingId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.StatusEnumList!=null)
                         $scope.StatusEnumList = result.DataExtra.StatusEnumList;
                        $scope.onAfterLoadBuildingExtraData(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Building! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Building! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveBuilding= function(){
    	if(!$scope.validateBuilding())
        {
          return;
        }
        $http.post($scope.saveBuildingUrl + "/", $scope.Building).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.Building = result.Data;
                    updateUrlQuery('id', $scope.Building.Id);
                   }
                   alertSuccess("Successfully saved Building data!");
                   $scope.onAfterSaveBuilding(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Building! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Building! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteBuildingById= function(){
    	
    };
   $scope.validateBuilding= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (BuildingId,getBuildingByIdUrl,getBuildingExtraDataUrl, saveBuildingUrl,deleteBuildingByIdUrl) {
        $scope.BuildingId = BuildingId;
        $scope.getBuildingByIdUrl = getBuildingByIdUrl;
        $scope.saveBuildingUrl = saveBuildingUrl;
        $scope.getBuildingExtraDataUrl = getBuildingExtraDataUrl;
        $scope.deleteBuildingByIdUrl = deleteBuildingByIdUrl;

        $scope.loadPage(BuildingId);
    };
    
  $scope.onAfterSaveBuilding= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetBuildingById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadBuildingExtraData= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
         if(result.DataExtra.CampusList!=null)
         $scope.CampusList =  result.DataExtra.CampusList; 
  /*
  //Child Table Bindings, need to fix
             $scope.BuildingIdList =  result.DataExtra.BuildingIdList; 
     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

