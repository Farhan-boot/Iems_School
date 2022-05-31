
//File: HR_Designation Anjular  Controller
emsApp.controller('DesignationAddEditCtrl', function ($scope, $http, $filter) {
    $scope.Designation = [];
    $scope.DesignationId=0;
    
    $scope.ErrorMsg = "";
    $scope.HasError= false;

   $scope.loadPage= function (DesignationId)
    {
       if(DesignationId!=null)
        $scope.DesignationId=DesignationId;
       
       $scope.loadDesignationExtraData($scope.DesignationId);
       $scope.getDesignationById($scope.DesignationId);
    }
   $scope.getNewDesignation= function()
    {
    	  $scope.getDesignationById(0);
    }
   $scope.getDesignationById= function(DesignationId)
    {
        if(DesignationId!=null)
        $scope.DesignationId=DesignationId;
        
        
        $http.get($scope.getDesignationByIdUrl, {
            params: { "id": $scope.DesignationId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.Designation = result.Data;
                $scope.onAfterGetDesignationById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Designation! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
             
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Designation! "+"Status: " +status.toString()+". "+ result.Message.toString();
             alertError($scope.ErrorMsg);
             
        });
    }
   $scope.loadDesignationExtraData= function(DesignationId)
    {
        if(DesignationId!=null)
            $scope.DesignationId=DesignationId;
            
            
            $http.get($scope.getDesignationExtraDataUrl, {
                params: { "id": $scope.DesignationId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.StatusEnumList!=null)
                         $scope.StatusEnumList = result.DataExtra.StatusEnumList;
                        $scope.onAfterLoadDesignationExtraData(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Designation! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
                 
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Designation! "+"Status: " +status.toString()+". "+ result.Message.toString();
                 alertError($scope.ErrorMsg);
                 
            });
    }
   $scope.saveDesignation= function(){
    	if(!$scope.validateDesignation())
        {
          return;
        }
        
        $http.post($scope.saveDesignationUrl + "/", $scope.Designation).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.Designation = result.Data;}
                   alertSuccess("Successfully saved Designation data!");
                   $scope.onAfterSaveDesignation(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Designation! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
                
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Designation! "+"Status: " +status.toString()+". "+ result.Message.toString();
                alertError($scope.ErrorMsg);
                
            });
    }

   $scope.deleteDesignationById= function(){
    	
    }
   $scope.validateDesignation= function(){
        return true;
    } 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (DesignationId,getDesignationByIdUrl,getDesignationExtraDataUrl, saveDesignationUrl,deleteDesignationByIdUrl) {
        $scope.DesignationId = DesignationId;
        $scope.getDesignationByIdUrl = getDesignationByIdUrl;
        $scope.saveDesignationUrl = saveDesignationUrl;
        $scope.getDesignationExtraDataUrl = getDesignationExtraDataUrl;
        $scope.deleteDesignationByIdUrl = deleteDesignationByIdUrl;

        $scope.loadPage(DesignationId);
    };
    
  $scope.onAfterSaveDesignation= function(result){
     //var data=result.Data;
    } 
  $scope.onAfterGetDesignationById= function(result){
    //var data=result.Data;
    /*//Child Table Bindings     */
    }   
  $scope.onAfterLoadDesignationExtraData= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
         if(result.DataExtra.JobClassList!=null)
         $scope.JobClassList =  result.DataExtra.JobClassList; 
    }
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

