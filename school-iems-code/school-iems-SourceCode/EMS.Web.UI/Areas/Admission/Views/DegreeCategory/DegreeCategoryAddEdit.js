
//File: Adm_DegreeCategory Anjular  Controller
emsApp.controller('DegreeCategoryAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.DegreeCategory = [];
    $scope.DegreeCategoryId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;

   $scope.loadPage= function (DegreeCategoryId)
    {
       if(DegreeCategoryId!=null)
        $scope.DegreeCategoryId=DegreeCategoryId;
       
       $scope.loadDegreeCategoryExtraData($scope.DegreeCategoryId);
       $scope.getDegreeCategoryById($scope.DegreeCategoryId);
    };
   $scope.getNewDegreeCategory= function()
    {
    	  $scope.getDegreeCategoryById(0);
    };
   $scope.getDegreeCategoryById= function(DegreeCategoryId)
    {
        if(DegreeCategoryId!=null && DegreeCategoryId!=='')
        $scope.DegreeCategoryId=DegreeCategoryId;
        else return;
        
        $http.get($scope.getDegreeCategoryByIdUrl, {
            params: { "id": $scope.DegreeCategoryId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.DegreeCategory = result.Data;
                updateUrlQuery('id' , $scope.DegreeCategory.Id);
                $scope.onAfterGetDegreeCategoryById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Degree Category! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Degree Category! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadDegreeCategoryExtraData= function(DegreeCategoryId)
    {
        if(DegreeCategoryId!=null)
            $scope.DegreeCategoryId=DegreeCategoryId;
            
            $http.get($scope.getDegreeCategoryExtraDataUrl, {
                params: { "id": $scope.DegreeCategoryId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.DegreeEquivalentEnumList!=null)
                         $scope.DegreeEquivalentEnumList = result.DataExtra.DegreeEquivalentEnumList;
                      if(result.DataExtra.BoardTypeEnumList!=null)
                         $scope.BoardTypeEnumList = result.DataExtra.BoardTypeEnumList;
                        $scope.onAfterLoadDegreeCategoryExtraData(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Degree Category! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Degree Category! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveDegreeCategory= function(){
    	if(!$scope.validateDegreeCategory())
        {
          return;
        }
        $http.post($scope.saveDegreeCategoryUrl + "/", $scope.DegreeCategory).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.DegreeCategory = result.Data;
                    updateUrlQuery('id', $scope.DegreeCategory.Id);
                   }
                   alertSuccess("Successfully saved Degree Category data!");
                   $scope.onAfterSaveDegreeCategory(result);

                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Degree Category! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Degree Category! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteDegreeCategoryById= function(){
    	
    };
   $scope.validateDegreeCategory= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (DegreeCategoryId,getDegreeCategoryByIdUrl,getDegreeCategoryExtraDataUrl, saveDegreeCategoryUrl,deleteDegreeCategoryByIdUrl) {
        $scope.DegreeCategoryId = DegreeCategoryId;
        $scope.getDegreeCategoryByIdUrl = getDegreeCategoryByIdUrl;
        $scope.saveDegreeCategoryUrl = saveDegreeCategoryUrl;
        $scope.getDegreeCategoryExtraDataUrl = getDegreeCategoryExtraDataUrl;
        $scope.deleteDegreeCategoryByIdUrl = deleteDegreeCategoryByIdUrl;

        $scope.loadPage(DegreeCategoryId);
    };
    
  $scope.onAfterSaveDegreeCategory= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetDegreeCategoryById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadDegreeCategoryExtraData= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
  /*
  //Child Table Bindings, need to fix
             $scope.DegreeCategoryIdList =  result.DataExtra.DegreeCategoryIdList; 
     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

