
//File: Acnt_ParticularName Anjular  Controller
emsApp.controller('ParticularNameAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.ParticularName = [];
    $scope.ParticularNameId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;

   $scope.loadPage= function (ParticularNameId)
    {
       if(ParticularNameId!=null)
        $scope.ParticularNameId=ParticularNameId;
       
       $scope.loadParticularNameExtraData($scope.ParticularNameId);
       $scope.getParticularNameById($scope.ParticularNameId);
    };
   $scope.getNewParticularName= function()
    {
    	  $scope.getParticularNameById(0);
    };
   $scope.getParticularNameById= function(ParticularNameId)
    {
        if(ParticularNameId!=null && ParticularNameId!=='')
        $scope.ParticularNameId=ParticularNameId;
        else return;
        
        $http.get($scope.getParticularNameByIdUrl, {
            params: { "id": $scope.ParticularNameId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.ParticularName = result.Data;
                updateUrlQuery('id' , $scope.ParticularName.Id);
                $scope.onAfterGetParticularNameById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Particular Name! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Particular Name! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadParticularNameExtraData= function(ParticularNameId)
    {
        if(ParticularNameId!=null)
            $scope.ParticularNameId=ParticularNameId;
            
            $http.get($scope.getParticularNameExtraDataUrl, {
                params: { "id": $scope.ParticularNameId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.StatusEnumList!=null)
                         $scope.StatusEnumList = result.DataExtra.StatusEnumList;
                      if(result.DataExtra.ParticularTypeEnumList!=null)
                          $scope.ParticularTypeEnumList = result.DataExtra.ParticularTypeEnumList;

                      if (result.DataExtra.ScholarshipCategoryEnumList != null)
                          $scope.ScholarshipCategoryEnumList = result.DataExtra.ScholarshipCategoryEnumList;

                      if (result.DataExtra.SemsterList != null)
                          $scope.SemsterList = result.DataExtra.SemsterList;

                      if(result.DataExtra.ParticularNameList != null)
                          $scope.ParticularNameList = result.DataExtra.ParticularNameList;

                      if (result.DataExtra.ScholarshipConditionTypeEnumList != null)
                          $scope.ScholarshipConditionTypeEnumList = result.DataExtra.ScholarshipConditionTypeEnumList;

                      if (result.DataExtra.ScholarshipTypeEnumList != null)
                          $scope.ScholarshipTypeEnumList = result.DataExtra.ScholarshipTypeEnumList;


                      $scope.ManualEntryAllowTypeEnumList = result.DataExtra.ManualEntryAllowTypeEnumList;
                        $scope.onAfterLoadParticularNameExtraData(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Particular Name! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Particular Name! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveParticularName= function(){
    	if(!$scope.validateParticularName())
        {
          return;
        }
        $http.post($scope.saveParticularNameUrl + "/", $scope.ParticularName).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.ParticularName = result.Data;
                    updateUrlQuery('id', $scope.ParticularName.Id);
                   }
                   alertSuccess("Successfully saved Particular Name data!");
                   $scope.onAfterSaveParticularName(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Particular Name! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Particular Name! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteParticularNameById= function(){
    	
    };
   $scope.validateParticularName= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (ParticularNameId,getParticularNameByIdUrl,getParticularNameExtraDataUrl, saveParticularNameUrl,deleteParticularNameByIdUrl) {
        $scope.ParticularNameId = ParticularNameId;
        $scope.getParticularNameByIdUrl = getParticularNameByIdUrl;
        $scope.saveParticularNameUrl = saveParticularNameUrl;
        $scope.getParticularNameExtraDataUrl = getParticularNameExtraDataUrl;
        $scope.deleteParticularNameByIdUrl = deleteParticularNameByIdUrl;

        $scope.loadPage(ParticularNameId);
    };
    
  $scope.onAfterSaveParticularName= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetParticularNameById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadParticularNameExtraData= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
  /*
  //Child Table Bindings, need to fix
             $scope.ParticularNameIdList =  result.DataExtra.ParticularNameIdList; 
     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

