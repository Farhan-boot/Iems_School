
//File: General_Campus Anjular  Controller
emsApp.controller('CampusAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.Campus = [];
    $scope.CampusId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;

   $scope.loadPage= function (CampusId)
    {
       if(CampusId!=null)
        $scope.CampusId=CampusId;
       
       $scope.loadCampusExtraData($scope.CampusId);
       $scope.getCampusById($scope.CampusId);
    };
   $scope.getNewCampus= function()
    {
    	  $scope.getCampusById(0);
    };
   $scope.getCampusById= function(CampusId)
    {
        if(CampusId!=null && CampusId!=='')
        $scope.CampusId=CampusId;
        else return;
        
        $http.get($scope.getCampusByIdUrl, {
            params: { "id": $scope.CampusId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.Campus = result.Data;
                updateUrlQuery('id' , $scope.Campus.Id);
                $scope.onAfterGetCampusById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Campus! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Campus! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadCampusExtraData= function(CampusId)
    {
        if(CampusId!=null)
            $scope.CampusId=CampusId;
            
            $http.get($scope.getCampusExtraDataUrl, {
                params: { "id": $scope.CampusId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.TypeEnumList!=null)
                         $scope.TypeEnumList = result.DataExtra.TypeEnumList;
                      if(result.DataExtra.StatusEnumList!=null)
                         $scope.StatusEnumList = result.DataExtra.StatusEnumList;
                        $scope.onAfterLoadCampusExtraData(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Campus! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Campus! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveCampus= function(){
    	if(!$scope.validateCampus())
        {
          return;
        }
        $http.post($scope.saveCampusUrl + "/", $scope.Campus).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.Campus = result.Data;
                    updateUrlQuery('id', $scope.Campus.Id);
                   }
                   alertSuccess("Successfully saved Campus data!");
                   $scope.onAfterSaveCampus(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Campus! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Campus! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteCampusById= function(){
    	
    };
   $scope.validateCampus= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (CampusId,getCampusByIdUrl,getCampusExtraDataUrl, saveCampusUrl,deleteCampusByIdUrl) {
        $scope.CampusId = CampusId;
        $scope.getCampusByIdUrl = getCampusByIdUrl;
        $scope.saveCampusUrl = saveCampusUrl;
        $scope.getCampusExtraDataUrl = getCampusExtraDataUrl;
        $scope.deleteCampusByIdUrl = deleteCampusByIdUrl;

        $scope.loadPage(CampusId);
    };
    
  $scope.onAfterSaveCampus= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetCampusById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadCampusExtraData= function(result){
    //var DataExtra=result.DataExtra;
      //DropDown Option Tables 
      /*
  //Child Table Bindings, need to fix
             $scope.CampusIdList =  result.DataExtra.CampusIdList; 

             $scope.CampusIdList =  result.DataExtra.CampusIdList; 

             $scope.CampusIdList =  result.DataExtra.CampusIdList; 
     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

