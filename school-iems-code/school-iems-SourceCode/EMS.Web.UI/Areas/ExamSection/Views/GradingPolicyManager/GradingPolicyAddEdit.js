
//File: Aca_GradingPolicy Anjular  Controller
emsApp.controller('GradingPolicyAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.GradingPolicy = [];
    $scope.GradingPolicyId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;

   $scope.loadPage= function (GradingPolicyId)
    {
       if(GradingPolicyId!=null)
        $scope.GradingPolicyId=GradingPolicyId;
       
       $scope.loadGradingPolicyExtraData($scope.GradingPolicyId);
       $scope.getGradingPolicyById($scope.GradingPolicyId);
    };
   $scope.getNewGradingPolicy= function()
    {
    	  $scope.getGradingPolicyById(0);
    };
   $scope.getGradingPolicyById= function(GradingPolicyId)
    {
        if(GradingPolicyId!=null && GradingPolicyId!=='')
        $scope.GradingPolicyId=GradingPolicyId;
        else return;
        
        $http.get($scope.getGradingPolicyByIdUrl, {
            params: { "id": $scope.GradingPolicyId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.GradingPolicy = result.Data;
                updateUrlQuery('id' , $scope.GradingPolicy.Id);
                $scope.onAfterGetGradingPolicyById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Grading Policy! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Grading Policy! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadGradingPolicyExtraData= function(GradingPolicyId)
    {
        if(GradingPolicyId!=null)
            $scope.GradingPolicyId=GradingPolicyId;
            
            $http.get($scope.getGradingPolicyExtraDataUrl, {
                params: { "id": $scope.GradingPolicyId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                        $scope.onAfterLoadGradingPolicyExtraData(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Grading Policy! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Grading Policy! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveGradingPolicy= function(){
    	if(!$scope.validateGradingPolicy())
        {
          return;
        }
        $http.post($scope.saveGradingPolicyUrl + "/", $scope.GradingPolicy).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.GradingPolicy = result.Data;
                    updateUrlQuery('id', $scope.GradingPolicy.Id);
                   }
                   alertSuccess("Successfully saved Grading Policy data!");
                   $scope.onAfterSaveGradingPolicy(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Grading Policy! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Grading Policy! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteGradingPolicyById= function(){
    	
    };
   $scope.validateGradingPolicy= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (GradingPolicyId,getGradingPolicyByIdUrl,getGradingPolicyExtraDataUrl, saveGradingPolicyUrl,deleteGradingPolicyByIdUrl) {
        $scope.GradingPolicyId = GradingPolicyId;
        $scope.getGradingPolicyByIdUrl = getGradingPolicyByIdUrl;
        $scope.saveGradingPolicyUrl = saveGradingPolicyUrl;
        $scope.getGradingPolicyExtraDataUrl = getGradingPolicyExtraDataUrl;
        $scope.deleteGradingPolicyByIdUrl = deleteGradingPolicyByIdUrl;

        $scope.loadPage(GradingPolicyId);
    };
    
  $scope.onAfterSaveGradingPolicy= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetGradingPolicyById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadGradingPolicyExtraData= function(result){
    //var DataExtra=result.DataExtra;
      //DropDown Option Tables  
      /*//Child Table Bindings
             $scope.GradingPolicyIdList =  result.DataExtra.GradingPolicyIdList; 

             $scope.GradingPolicyIdList =  result.DataExtra.GradingPolicyIdList; 
     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

