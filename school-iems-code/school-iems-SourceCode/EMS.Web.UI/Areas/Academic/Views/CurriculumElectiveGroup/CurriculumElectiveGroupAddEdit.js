
//File: Aca_CurriculumElectiveGroup Anjular  Controller
emsApp.controller('CurriculumElectiveGroupAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.CurriculumElectiveGroup = [];
    $scope.CurriculumElectiveGroupId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;

   $scope.loadPage= function (CurriculumElectiveGroupId)
    {
       if(CurriculumElectiveGroupId!=null)
        $scope.CurriculumElectiveGroupId=CurriculumElectiveGroupId;
       
       $scope.loadCurriculumElectiveGroupExtraData($scope.CurriculumElectiveGroupId);
       $scope.getCurriculumElectiveGroupById($scope.CurriculumElectiveGroupId);
    };
   $scope.getNewCurriculumElectiveGroup= function()
    {
    	  $scope.getCurriculumElectiveGroupById(0);
    };
   $scope.getCurriculumElectiveGroupById= function(CurriculumElectiveGroupId)
    {
        if(CurriculumElectiveGroupId!=null && CurriculumElectiveGroupId!=='')
        $scope.CurriculumElectiveGroupId=CurriculumElectiveGroupId;
        else return;
        
        $http.get($scope.getCurriculumElectiveGroupByIdUrl, {
            params: { "id": $scope.CurriculumElectiveGroupId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.CurriculumElectiveGroup = result.Data;
                updateUrlQuery('id' , $scope.CurriculumElectiveGroup.Id);
                $scope.onAfterGetCurriculumElectiveGroupById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Curriculum Elective Group! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Curriculum Elective Group! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadCurriculumElectiveGroupExtraData= function(CurriculumElectiveGroupId)
    {
        if(CurriculumElectiveGroupId!=null)
            $scope.CurriculumElectiveGroupId=CurriculumElectiveGroupId;
            
            $http.get($scope.getCurriculumElectiveGroupExtraDataUrl, {
                params: { "id": $scope.CurriculumElectiveGroupId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                        $scope.onAfterLoadCurriculumElectiveGroupExtraData(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Curriculum Elective Group! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Curriculum Elective Group! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveCurriculumElectiveGroup= function(){
    	if(!$scope.validateCurriculumElectiveGroup())
        {
          return;
        }
        $http.post($scope.saveCurriculumElectiveGroupUrl + "/", $scope.CurriculumElectiveGroup).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.CurriculumElectiveGroup = result.Data;
                    updateUrlQuery('id', $scope.CurriculumElectiveGroup.Id);
                   }
                   alertSuccess("Successfully saved Curriculum Elective Group data!");
                   $scope.onAfterSaveCurriculumElectiveGroup(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Curriculum Elective Group! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Curriculum Elective Group! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteCurriculumElectiveGroupById= function(){
    	
    };
   $scope.validateCurriculumElectiveGroup= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (CurriculumElectiveGroupId,getCurriculumElectiveGroupByIdUrl,getCurriculumElectiveGroupExtraDataUrl, saveCurriculumElectiveGroupUrl,deleteCurriculumElectiveGroupByIdUrl) {
        $scope.CurriculumElectiveGroupId = CurriculumElectiveGroupId;
        $scope.getCurriculumElectiveGroupByIdUrl = getCurriculumElectiveGroupByIdUrl;
        $scope.saveCurriculumElectiveGroupUrl = saveCurriculumElectiveGroupUrl;
        $scope.getCurriculumElectiveGroupExtraDataUrl = getCurriculumElectiveGroupExtraDataUrl;
        $scope.deleteCurriculumElectiveGroupByIdUrl = deleteCurriculumElectiveGroupByIdUrl;

        $scope.loadPage(CurriculumElectiveGroupId);
    };
    
  $scope.onAfterSaveCurriculumElectiveGroup= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetCurriculumElectiveGroupById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadCurriculumElectiveGroupExtraData= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
         if(result.DataExtra.ProgramList!=null)
         $scope.ProgramList =  result.DataExtra.ProgramList; 
  /*//Child Table Bindings
             $scope.ElectiveGroupIdList =  result.DataExtra.ElectiveGroupIdList; 

             $scope.ElectiveGroupIdList =  result.DataExtra.ElectiveGroupIdList; 
     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

