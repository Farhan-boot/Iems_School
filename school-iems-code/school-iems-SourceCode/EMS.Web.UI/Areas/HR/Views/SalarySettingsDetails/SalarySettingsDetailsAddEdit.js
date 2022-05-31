
//File: HR_SalarySettingsDetails Anjular  Controller
emsApp.controller('SalarySettingsDetailsAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.SalarySettingsDetails = [];
    $scope.SalarySettingsDetailsId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (SalarySettingsDetailsId)
    {
       if(SalarySettingsDetailsId!=null)
        $scope.SalarySettingsDetailsId=SalarySettingsDetailsId;
       
       $scope.loadSalarySettingsDetailsDataExtra($scope.SalarySettingsDetailsId);
       $scope.getSalarySettingsDetailsById($scope.SalarySettingsDetailsId);
    };
   $scope.getNewSalarySettingsDetails= function()
    {
    	  $scope.getSalarySettingsDetailsById(0);
    };
   $scope.getSalarySettingsDetailsById= function(SalarySettingsDetailsId)
    {
        if(SalarySettingsDetailsId!=null && SalarySettingsDetailsId!=='')
        $scope.SalarySettingsDetailsId=SalarySettingsDetailsId;
        else return;
        
        $http.get($scope.getSalarySettingsDetailsByIdUrl, {
            params: { "id": $scope.SalarySettingsDetailsId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.SalarySettingsDetails = result.Data;
                updateUrlQuery('id' , $scope.SalarySettingsDetails.Id);
                $scope.onAfterGetSalarySettingsDetailsById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Salary Settings Details! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Salary Settings Details! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadSalarySettingsDetailsDataExtra= function(SalarySettingsDetailsId)
    {
        if(SalarySettingsDetailsId!=null)
            $scope.SalarySettingsDetailsId=SalarySettingsDetailsId;
            
            $http.get($scope.getSalarySettingsDetailsDataExtraUrl, {
                params: { "id": $scope.SalarySettingsDetailsId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.SalaryTypeEnumList!=null)
                         $scope.SalaryTypeEnumList = result.DataExtra.SalaryTypeEnumList;
                      if(result.DataExtra.CategoryTypeEnumList!=null)
                         $scope.CategoryTypeEnumList = result.DataExtra.CategoryTypeEnumList;
                        $scope.onAfterLoadSalarySettingsDetailsDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Salary Settings Details! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Salary Settings Details! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveSalarySettingsDetails= function(){
    	if(!$scope.validateSalarySettingsDetails())
        {
          return;
        }
        $http.post($scope.saveSalarySettingsDetailsUrl + "/", $scope.SalarySettingsDetails).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.SalarySettingsDetails = result.Data;
                    updateUrlQuery('id', $scope.SalarySettingsDetails.Id);
                   }
                   alertSuccess("Successfully saved Salary Settings Details data!");
                   $scope.onAfterSaveSalarySettingsDetails(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Salary Settings Details! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Salary Settings Details! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteSalarySettingsDetailsById= function(){
    	
    };
   $scope.validateSalarySettingsDetails= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (SalarySettingsDetailsId,getSalarySettingsDetailsByIdUrl,getSalarySettingsDetailsDataExtraUrl, saveSalarySettingsDetailsUrl,deleteSalarySettingsDetailsByIdUrl) {
        $scope.SalarySettingsDetailsId = SalarySettingsDetailsId;
        $scope.getSalarySettingsDetailsByIdUrl = getSalarySettingsDetailsByIdUrl;
        $scope.saveSalarySettingsDetailsUrl = saveSalarySettingsDetailsUrl;
        $scope.getSalarySettingsDetailsDataExtraUrl = getSalarySettingsDetailsDataExtraUrl;
        $scope.deleteSalarySettingsDetailsByIdUrl = deleteSalarySettingsDetailsByIdUrl;

        $scope.loadPage(SalarySettingsDetailsId);
    };
    
  $scope.onAfterSaveSalarySettingsDetails= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetSalarySettingsDetailsById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadSalarySettingsDetailsDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
         if(result.DataExtra.SalarySettingsList!=null)
         $scope.SalarySettingsList =  result.DataExtra.SalarySettingsList; 
  /*
  //Child Table Bindings, need to fix     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

