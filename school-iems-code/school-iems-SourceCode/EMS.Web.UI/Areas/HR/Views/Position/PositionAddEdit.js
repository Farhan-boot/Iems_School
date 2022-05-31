
//File: HR_Position Anjular  Controller
emsApp.controller('PositionAddEditCtrl', function ($scope, $http, $filter) {
    $scope.Position = [];
    $scope.PositionId=0;
    
    $scope.ErrorMsg = "";
    $scope.HasError= false;

   $scope.loadPage= function (PositionId)
    {
       if(PositionId!=null)
        $scope.PositionId=PositionId;
       
       $scope.loadPositionExtraData($scope.PositionId);
       $scope.getPositionById($scope.PositionId);
    }
   $scope.getNewPosition= function()
    {
    	  $scope.getPositionById(0);
    }
   $scope.getPositionById= function(PositionId)
    {
        if(PositionId!=null)
        $scope.PositionId=PositionId;
        
        
        $http.get($scope.getPositionByIdUrl, {
            params: { "id": $scope.PositionId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.Position = result.Data;
                $scope.onAfterGetPositionById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Position! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
             
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Position! "+"Status: " +status.toString()+". "+ result.Message.toString();
             alertError($scope.ErrorMsg);
             
        });
    }
   $scope.loadPositionExtraData= function(PositionId)
    {
        if(PositionId!=null)
            $scope.PositionId=PositionId;
            
            
            $http.get($scope.getPositionExtraDataUrl, {
                params: { "id": $scope.PositionId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.JobClassEnumList!=null)
                         $scope.JobClassEnumList = result.DataExtra.JobClassEnumList;
                      if(result.DataExtra.StatusEnumList!=null)
                        $scope.StatusEnumList = result.DataExtra.StatusEnumList;
                    if (result.DataExtra.SalaryTemplateList != null)
                        $scope.SalaryTemplateList = result.DataExtra.SalaryTemplateList;
                        $scope.onAfterLoadPositionExtraData(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Position! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
                 
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Position! "+"Status: " +status.toString()+". "+ result.Message.toString();
                 alertError($scope.ErrorMsg);
                 
            });
    }
   $scope.savePosition= function(){
    	if(!$scope.validatePosition())
        {
          return;
        }
        
        $http.post($scope.savePositionUrl + "/", $scope.Position).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.Position = result.Data;}
                   alertSuccess("Successfully saved Position data!");
                   $scope.onAfterSavePosition(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Position! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
                
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Position! "+"Status: " +status.toString()+". "+ result.Message.toString();
                alertError($scope.ErrorMsg);
                
            });
    }

   $scope.deletePositionById= function(){
    	
    }
   $scope.validatePosition= function(){
        return true;
    } 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (PositionId,getPositionByIdUrl,getPositionExtraDataUrl, savePositionUrl,deletePositionByIdUrl) {
        $scope.PositionId = PositionId;
        $scope.getPositionByIdUrl = getPositionByIdUrl;
        $scope.savePositionUrl = savePositionUrl;
        $scope.getPositionExtraDataUrl = getPositionExtraDataUrl;
        $scope.deletePositionByIdUrl = deletePositionByIdUrl;

        $scope.loadPage(PositionId);
    };
    
    $scope.onAfterSavePosition= function(result){
     //var data=result.Data;
    };
    $scope.onAfterGetPositionById= function(result){
        //var data=result.Data;
        /*//Child Table Bindings
                 $scope.AppointmentHistory_PositionIdList =  result.DataExtra.AppointmentHistory_PositionIdList; 

                 $scope.DeptPosition_PositionIdList =  result.DataExtra.DeptPosition_PositionIdList; 

                 $scope.Employee_PositionIdList =  result.DataExtra.Employee_PositionIdList; 
         */
    };
    $scope.onAfterLoadPositionExtraData = function(result) {
        //var DataExtra=result.DataExtra;
        //DropDown Option Tables    };
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

