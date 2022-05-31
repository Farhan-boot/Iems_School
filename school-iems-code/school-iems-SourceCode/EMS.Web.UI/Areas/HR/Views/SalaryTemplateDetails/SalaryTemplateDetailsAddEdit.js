
//File: HR_SalaryTemplateDetails Anjular  Controller
emsApp.controller('SalaryTemplateDetailsAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.SalaryTemplateDetails = [];
    $scope.SalaryTemplateDetailsId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (SalaryTemplateDetailsId)
    {
       if(SalaryTemplateDetailsId!=null)
        $scope.SalaryTemplateDetailsId=SalaryTemplateDetailsId;
       
       $scope.loadSalaryTemplateDetailsDataExtra($scope.SalaryTemplateDetailsId);
       $scope.getSalaryTemplateDetailsById($scope.SalaryTemplateDetailsId);
    };
   $scope.getNewSalaryTemplateDetails= function()
    {
    	  $scope.getSalaryTemplateDetailsById(0);
    };
   $scope.getSalaryTemplateDetailsById= function(SalaryTemplateDetailsId)
    {
        if(SalaryTemplateDetailsId!=null && SalaryTemplateDetailsId!=='')
        $scope.SalaryTemplateDetailsId=SalaryTemplateDetailsId;
        else return;
        
        $http.get($scope.getSalaryTemplateDetailsByIdUrl, {
            params: { "id": $scope.SalaryTemplateDetailsId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.SalaryTemplateDetails = result.Data;
                updateUrlQuery('id' , $scope.SalaryTemplateDetails.Id);
                $scope.onAfterGetSalaryTemplateDetailsById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Salary Template Details! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Salary Template Details! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadSalaryTemplateDetailsDataExtra= function(SalaryTemplateDetailsId)
    {
        if(SalaryTemplateDetailsId!=null)
            $scope.SalaryTemplateDetailsId=SalaryTemplateDetailsId;
            
            $http.get($scope.getSalaryTemplateDetailsDataExtraUrl, {
                params: { "id": $scope.SalaryTemplateDetailsId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.SalaryTypeEnumList!=null)
                         $scope.SalaryTypeEnumList = result.DataExtra.SalaryTypeEnumList;
                      if(result.DataExtra.CategoryTypeEnumList!=null)
                         $scope.CategoryTypeEnumList = result.DataExtra.CategoryTypeEnumList;
                        $scope.onAfterLoadSalaryTemplateDetailsDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Salary Template Details! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Salary Template Details! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveSalaryTemplateDetails= function(){
    	if(!$scope.validateSalaryTemplateDetails())
        {
          return;
        }
        $http.post($scope.saveSalaryTemplateDetailsUrl + "/", $scope.SalaryTemplateDetails).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.SalaryTemplateDetails = result.Data;
                    updateUrlQuery('id', $scope.SalaryTemplateDetails.Id);
                   }
                   alertSuccess("Successfully saved Salary Template Details data!");
                   $scope.onAfterSaveSalaryTemplateDetails(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Salary Template Details! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Salary Template Details! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteSalaryTemplateDetailsById= function(){
    	
    };
   $scope.validateSalaryTemplateDetails= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (SalaryTemplateDetailsId
        , getSalaryTemplateDetailsByIdUrl,
        getSalaryTemplateDetailsDataExtraUrl,
        saveSalaryTemplateDetailsUrl, deleteSalaryTemplateDetailsByIdUrl) {

        $scope.SalaryTemplateDetailsId = SalaryTemplateDetailsId;
        $scope.getSalaryTemplateDetailsByIdUrl = getSalaryTemplateDetailsByIdUrl;
        $scope.saveSalaryTemplateDetailsUrl = saveSalaryTemplateDetailsUrl;
        $scope.getSalaryTemplateDetailsDataExtraUrl = getSalaryTemplateDetailsDataExtraUrl;
        $scope.deleteSalaryTemplateDetailsByIdUrl = deleteSalaryTemplateDetailsByIdUrl;

        $scope.loadPage(SalaryTemplateDetailsId);
    };
    
  $scope.onAfterSaveSalaryTemplateDetails= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetSalaryTemplateDetailsById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadSalaryTemplateDetailsDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
         if(result.DataExtra.SalaryTemplateDetailsList!=null)
         $scope.SalaryTemplateDetailsList =  result.DataExtra.SalaryTemplateDetailsList; 
         if(result.DataExtra.SalaryTemplateList!=null)
         $scope.SalaryTemplateList =  result.DataExtra.SalaryTemplateList; 
  /*
  //Child Table Bindings, need to fix
             $scope.ParentIdList =  result.DataExtra.ParentIdList; 
     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

