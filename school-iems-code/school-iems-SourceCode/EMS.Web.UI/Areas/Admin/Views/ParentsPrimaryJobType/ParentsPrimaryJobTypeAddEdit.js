
//File: Adm_ParentsPrimaryJobType Anjular  Controller
emsApp.controller('ParentsPrimaryJobTypeAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.ParentsPrimaryJobType = [];
    $scope.ParentsPrimaryJobTypeId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (ParentsPrimaryJobTypeId)
    {
       if(ParentsPrimaryJobTypeId!=null)
        $scope.ParentsPrimaryJobTypeId=ParentsPrimaryJobTypeId;
       
       $scope.loadParentsPrimaryJobTypeDataExtra($scope.ParentsPrimaryJobTypeId);
       $scope.getParentsPrimaryJobTypeById($scope.ParentsPrimaryJobTypeId);
    };
   $scope.getNewParentsPrimaryJobType= function()
    {
    	  $scope.getParentsPrimaryJobTypeById(0);
    };
   $scope.getParentsPrimaryJobTypeById= function(ParentsPrimaryJobTypeId)
    {
        if(ParentsPrimaryJobTypeId!=null && ParentsPrimaryJobTypeId!=='')
        $scope.ParentsPrimaryJobTypeId=ParentsPrimaryJobTypeId;
        else return;
        
        $http.get($scope.getParentsPrimaryJobTypeByIdUrl, {
            params: { "id": $scope.ParentsPrimaryJobTypeId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.ParentsPrimaryJobType = result.Data;
                updateUrlQuery('id' , $scope.ParentsPrimaryJobType.Id);
                $scope.onAfterGetParentsPrimaryJobTypeById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Parents Primary Job Type! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Parents Primary Job Type! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadParentsPrimaryJobTypeDataExtra= function(ParentsPrimaryJobTypeId)
    {
        if(ParentsPrimaryJobTypeId!=null)
            $scope.ParentsPrimaryJobTypeId=ParentsPrimaryJobTypeId;
            
            $http.get($scope.getParentsPrimaryJobTypeDataExtraUrl, {
                params: { "id": $scope.ParentsPrimaryJobTypeId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                        $scope.onAfterLoadParentsPrimaryJobTypeDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Parents Primary Job Type! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Parents Primary Job Type! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveParentsPrimaryJobType= function(){
    	if(!$scope.validateParentsPrimaryJobType())
        {
          return;
        }
        $http.post($scope.saveParentsPrimaryJobTypeUrl + "/", $scope.ParentsPrimaryJobType).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.ParentsPrimaryJobType = result.Data;
                    updateUrlQuery('id', $scope.ParentsPrimaryJobType.Id);
                   }
                   alertSuccess("Successfully saved Parents Primary Job Type data!");
                   $scope.onAfterSaveParentsPrimaryJobType(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Parents Primary Job Type! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Parents Primary Job Type! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteParentsPrimaryJobTypeById= function(){
    	
    };
   $scope.validateParentsPrimaryJobType= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (ParentsPrimaryJobTypeId,getParentsPrimaryJobTypeByIdUrl,getParentsPrimaryJobTypeDataExtraUrl, saveParentsPrimaryJobTypeUrl,deleteParentsPrimaryJobTypeByIdUrl) {
        $scope.ParentsPrimaryJobTypeId = ParentsPrimaryJobTypeId;
        $scope.getParentsPrimaryJobTypeByIdUrl = getParentsPrimaryJobTypeByIdUrl;
        $scope.saveParentsPrimaryJobTypeUrl = saveParentsPrimaryJobTypeUrl;
        $scope.getParentsPrimaryJobTypeDataExtraUrl = getParentsPrimaryJobTypeDataExtraUrl;
        $scope.deleteParentsPrimaryJobTypeByIdUrl = deleteParentsPrimaryJobTypeByIdUrl;

        $scope.loadPage(ParentsPrimaryJobTypeId);
    };
    
  $scope.onAfterSaveParentsPrimaryJobType= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetParentsPrimaryJobTypeById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadParentsPrimaryJobTypeDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
  /*
  //Child Table Bindings, need to fix
             $scope.ParentsPrimaryJobTypeIdList =  result.DataExtra.ParentsPrimaryJobTypeIdList; 
     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

