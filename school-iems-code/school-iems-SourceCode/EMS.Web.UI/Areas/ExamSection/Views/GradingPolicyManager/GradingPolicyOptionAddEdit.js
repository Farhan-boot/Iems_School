
//File: Aca_GradingPolicyOption Anjular  Controller
emsApp.controller('GradingPolicyOptionAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.GradingPolicyOption = [];
    $scope.GradingPolicyOptionId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;

   $scope.loadPage= function (GradingPolicyOptionId)
    {
       if(GradingPolicyOptionId!=null)
        $scope.GradingPolicyOptionId=GradingPolicyOptionId;
       
       $scope.loadGradingPolicyOptionExtraData($scope.GradingPolicyOptionId);
       $scope.getGradingPolicyOptionById($scope.GradingPolicyOptionId);
    };
   $scope.getNewGradingPolicyOption= function()
    {
    	  $scope.getGradingPolicyOptionById(0);
    };
   $scope.getGradingPolicyOptionById= function(GradingPolicyOptionId)
    {
        if(GradingPolicyOptionId!=null && GradingPolicyOptionId!=='')
        $scope.GradingPolicyOptionId=GradingPolicyOptionId;
        else return;
        
        $http.get($scope.getGradingPolicyOptionByIdUrl, {
            params: { "id": $scope.GradingPolicyOptionId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.GradingPolicyOption = result.Data;
                updateUrlQuery('id' , $scope.GradingPolicyOption.Id);
                $scope.onAfterGetGradingPolicyOptionById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Grading Policy Option! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Grading Policy Option! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadGradingPolicyOptionExtraData= function(GradingPolicyOptionId)
    {
        if(GradingPolicyOptionId!=null)
            $scope.GradingPolicyOptionId=GradingPolicyOptionId;
            
            $http.get($scope.getGradingPolicyOptionExtraDataUrl, {
                params: { "id": $scope.GradingPolicyOptionId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.LimitOperatorEnumList!=null)
                         $scope.LimitOperatorEnumList = result.DataExtra.LimitOperatorEnumList;
                        $scope.onAfterLoadGradingPolicyOptionExtraData(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Grading Policy Option! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Grading Policy Option! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveGradingPolicyOption= function(){
    	if(!$scope.validateGradingPolicyOption())
        {
          return;
        }
        $http.post($scope.saveGradingPolicyOptionUrl + "/", $scope.GradingPolicyOption).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.GradingPolicyOption = result.Data;
                    updateUrlQuery('id', $scope.GradingPolicyOption.Id);
                   }
                   alertSuccess("Successfully saved Grading Policy Option data!");
                   $scope.onAfterSaveGradingPolicyOption(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Grading Policy Option! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Grading Policy Option! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteGradingPolicyOptionById= function(){
    	
    };
   $scope.validateGradingPolicyOption= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (GradingPolicyOptionId,getGradingPolicyOptionByIdUrl,getGradingPolicyOptionExtraDataUrl, saveGradingPolicyOptionUrl,deleteGradingPolicyOptionByIdUrl) {
        $scope.GradingPolicyOptionId = GradingPolicyOptionId;
        $scope.getGradingPolicyOptionByIdUrl = getGradingPolicyOptionByIdUrl;
        $scope.saveGradingPolicyOptionUrl = saveGradingPolicyOptionUrl;
        $scope.getGradingPolicyOptionExtraDataUrl = getGradingPolicyOptionExtraDataUrl;
        $scope.deleteGradingPolicyOptionByIdUrl = deleteGradingPolicyOptionByIdUrl;

        $scope.loadPage(GradingPolicyOptionId);
    };
    
  $scope.onAfterSaveGradingPolicyOption= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetGradingPolicyOptionById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadGradingPolicyOptionExtraData= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
         if(result.DataExtra.GradingPolicyList!=null)
         $scope.GradingPolicyList =  result.DataExtra.GradingPolicyList; 
  /*//Child Table Bindings     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

