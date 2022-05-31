
//File: General_Floor Anjular  Controller
emsApp.controller('FloorAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.Floor = [];
    $scope.FloorId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;

   $scope.loadPage= function (FloorId)
    {
       if(FloorId!=null)
        $scope.FloorId=FloorId;
       
       $scope.loadFloorExtraData($scope.FloorId);
       $scope.getFloorById($scope.FloorId);
    };
   $scope.getNewFloor= function()
    {
    	  $scope.getFloorById(0);
    };
   $scope.getFloorById= function(FloorId)
    {
        if(FloorId!=null && FloorId!=='')
        $scope.FloorId=FloorId;
        else return;
        
        $http.get($scope.getFloorByIdUrl, {
            params: { "id": $scope.FloorId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.Floor = result.Data;
                updateUrlQuery('id' , $scope.Floor.Id);
                $scope.onAfterGetFloorById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Floor! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Floor! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadFloorExtraData= function(FloorId)
    {
        if(FloorId!=null)
            $scope.FloorId=FloorId;
            
            $http.get($scope.getFloorExtraDataUrl, {
                params: { "id": $scope.FloorId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                        $scope.onAfterLoadFloorExtraData(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Floor! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Floor! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveFloor= function(){
    	if(!$scope.validateFloor())
        {
          return;
        }
        $http.post($scope.saveFloorUrl + "/", $scope.Floor).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.Floor = result.Data;
                    updateUrlQuery('id', $scope.Floor.Id);
                   }
                   alertSuccess("Successfully saved Floor data!");
                   $scope.onAfterSaveFloor(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Floor! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Floor! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteFloorById= function(){
    	
    };
   $scope.validateFloor= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (FloorId,getFloorByIdUrl,getFloorExtraDataUrl, saveFloorUrl,deleteFloorByIdUrl) {
        $scope.FloorId = FloorId;
        $scope.getFloorByIdUrl = getFloorByIdUrl;
        $scope.saveFloorUrl = saveFloorUrl;
        $scope.getFloorExtraDataUrl = getFloorExtraDataUrl;
        $scope.deleteFloorByIdUrl = deleteFloorByIdUrl;

        $scope.loadPage(FloorId);
    };
    
  $scope.onAfterSaveFloor= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetFloorById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadFloorExtraData= function(result){
    //var DataExtra=result.DataExtra;
      //DropDown Option Tables  
      /*
  //Child Table Bindings, need to fix
             $scope.FloorIdList =  result.DataExtra.FloorIdList; 
     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

