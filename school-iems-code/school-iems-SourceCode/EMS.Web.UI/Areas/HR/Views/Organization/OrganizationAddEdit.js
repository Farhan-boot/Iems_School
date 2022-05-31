
//File: HR_Organization Anjular  Controller
emsApp.controller('OrganizationAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.Organization = [];
    $scope.OrganizationId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (OrganizationId)
    {
       if(OrganizationId!=null)
        $scope.OrganizationId=OrganizationId;
       
       $scope.loadOrganizationDataExtra($scope.OrganizationId);
       $scope.getOrganizationById($scope.OrganizationId);
    };
   $scope.getNewOrganization= function()
    {
    	  $scope.getOrganizationById(0);
    };
   $scope.getOrganizationById= function(OrganizationId)
    {
        if(OrganizationId!=null && OrganizationId!=='')
        $scope.OrganizationId=OrganizationId;
        else return;
        
        $http.get($scope.getOrganizationByIdUrl, {
            params: { "id": $scope.OrganizationId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.Organization = result.Data;
                updateUrlQuery('id' , $scope.Organization.Id);
                $scope.onAfterGetOrganizationById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Organization! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Organization! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadOrganizationDataExtra= function(OrganizationId)
    {
        if(OrganizationId!=null)
            $scope.OrganizationId=OrganizationId;
            
            $http.get($scope.getOrganizationDataExtraUrl, {
                params: { "id": $scope.OrganizationId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.TypeEnumList!=null)
                         $scope.TypeEnumList = result.DataExtra.TypeEnumList;
                      if(result.DataExtra.StatusEnumList!=null)
                         $scope.StatusEnumList = result.DataExtra.StatusEnumList;
                        $scope.onAfterLoadOrganizationDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Organization! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Organization! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveOrganization= function(){
    	if(!$scope.validateOrganization())
        {
          return;
        }
        $http.post($scope.saveOrganizationUrl + "/", $scope.Organization).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.Organization = result.Data;
                    updateUrlQuery('id', $scope.Organization.Id);
                   }
                   alertSuccess("Successfully saved Organization data!");
                   $scope.onAfterSaveOrganization(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Organization! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Organization! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteOrganizationById= function(){
    	
    };
   $scope.validateOrganization= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (OrganizationId,getOrganizationByIdUrl,getOrganizationDataExtraUrl, saveOrganizationUrl,deleteOrganizationByIdUrl) {
        $scope.OrganizationId = OrganizationId;
        $scope.getOrganizationByIdUrl = getOrganizationByIdUrl;
        $scope.saveOrganizationUrl = saveOrganizationUrl;
        $scope.getOrganizationDataExtraUrl = getOrganizationDataExtraUrl;
        $scope.deleteOrganizationByIdUrl = deleteOrganizationByIdUrl;

        $scope.loadPage(OrganizationId);
    };
    
  $scope.onAfterSaveOrganization= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetOrganizationById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadOrganizationDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
  /*
  //Child Table Bindings, need to fix
             $scope.OrgIdList =  result.DataExtra.OrgIdList; 
     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

