
//File: General_Room Anjular  Controller
emsApp.controller('RoomAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.Room = [];
    $scope.RoomId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;

   $scope.loadPage= function (RoomId)
    {
       if(RoomId!=null)
        $scope.RoomId=RoomId;
       
       $scope.loadRoomExtraData($scope.RoomId);
       $scope.getRoomById($scope.RoomId);
    };
   $scope.getNewRoom= function()
    {
    	  $scope.getRoomById(0);
    };
   $scope.getRoomById= function(RoomId)
    {
        if(RoomId!=null && RoomId!=='')
        $scope.RoomId=RoomId;
        else return;
        
        $http.get($scope.getRoomByIdUrl, {
            params: { "id": $scope.RoomId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.Room = result.Data;
                updateUrlQuery('id' , $scope.Room.Id);
                $scope.onAfterGetRoomById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Room! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Room! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadRoomExtraData= function(RoomId)
    {
        if(RoomId!=null)
            $scope.RoomId=RoomId;
            
            $http.get($scope.getRoomExtraDataUrl, {
                params: { "id": $scope.RoomId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.TypeEnumList!=null)
                         $scope.TypeEnumList = result.DataExtra.TypeEnumList;
                      if(result.DataExtra.StatusEnumList!=null)
                         $scope.StatusEnumList = result.DataExtra.StatusEnumList;
                        $scope.onAfterLoadRoomExtraData(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Room! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Room! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveRoom= function(){
    	if(!$scope.validateRoom())
        {
          return;
        }
        $http.post($scope.saveRoomUrl + "/", $scope.Room).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.Room = result.Data;
                    updateUrlQuery('id', $scope.Room.Id);
                   }
                   alertSuccess("Successfully saved Room data!");
                   $scope.onAfterSaveRoom(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Room! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Room! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteRoomById= function(){
    	
    };
   $scope.validateRoom= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (RoomId,getRoomByIdUrl,getRoomExtraDataUrl, saveRoomUrl,deleteRoomByIdUrl) {
        $scope.RoomId = RoomId;
        $scope.getRoomByIdUrl = getRoomByIdUrl;
        $scope.saveRoomUrl = saveRoomUrl;
        $scope.getRoomExtraDataUrl = getRoomExtraDataUrl;
        $scope.deleteRoomByIdUrl = deleteRoomByIdUrl;

        $scope.loadPage(RoomId);
    };
    
  $scope.onAfterSaveRoom= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetRoomById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadRoomExtraData= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
         if(result.DataExtra.BuildingList!=null)
         $scope.BuildingList =  result.DataExtra.BuildingList; 

         if(result.DataExtra.FloorList!=null)
         $scope.FloorList =  result.DataExtra.FloorList; 

         if(result.DataExtra.DepartmentList!=null)
         $scope.DepartmentList =  result.DataExtra.DepartmentList; 
  /*
  //Child Table Bindings, need to fix
             $scope.RoomIdList =  result.DataExtra.RoomIdList; 
     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

