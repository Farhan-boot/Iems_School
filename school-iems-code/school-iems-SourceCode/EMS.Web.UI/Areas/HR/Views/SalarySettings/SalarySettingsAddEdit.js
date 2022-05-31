
//File: HR_SalarySettings Anjular  Controller
emsApp.controller('SalarySettingsAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.SalarySettings = [];
    $scope.SalarySettingsId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (SalarySettingsId)
    {
       if(SalarySettingsId!=null)
        $scope.SalarySettingsId=SalarySettingsId;
       
       $scope.loadSalarySettingsDataExtra($scope.SalarySettingsId);
       $scope.getSalarySettingsById($scope.SalarySettingsId);
    };
   $scope.getNewSalarySettings= function()
    {
    	  $scope.getSalarySettingsById(0);
    };
   $scope.getSalarySettingsById= function(SalarySettingsId)
    {
        if(SalarySettingsId!=null && SalarySettingsId!=='')
        $scope.SalarySettingsId=SalarySettingsId;
        else return;
        
        $http.get($scope.getSalarySettingsByIdUrl, {
            params: { "id": $scope.SalarySettingsId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.SalarySettings = result.Data;
                updateUrlQuery('id' , $scope.SalarySettings.Id);
                $scope.onAfterGetSalarySettingsById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Salary Settings! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Salary Settings! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadSalarySettingsDataExtra= function(SalarySettingsId)
    {
        if(SalarySettingsId!=null)
            $scope.SalarySettingsId=SalarySettingsId;
            
            $http.get($scope.getSalarySettingsDataExtraUrl, {
                params: { "id": $scope.SalarySettingsId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.SalaryTypeEnumList!=null)
                         $scope.SalaryTypeEnumList = result.DataExtra.SalaryTypeEnumList;
                        $scope.onAfterLoadSalarySettingsDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Salary Settings! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Salary Settings! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveSalarySettings= function(){
    	if(!$scope.validateSalarySettings())
        {
          return;
        }
        $http.post($scope.saveSalarySettingsUrl + "/", $scope.SalarySettings).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.SalarySettings = result.Data;
                    updateUrlQuery('id', $scope.SalarySettings.Id);
                   }
                   alertSuccess("Successfully saved Salary Settings data!");
                   $scope.onAfterSaveSalarySettings(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Salary Settings! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Salary Settings! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteSalarySettingsById= function(){
    	
    };
   $scope.validateSalarySettings= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (SalarySettingsId,getSalarySettingsByIdUrl,getSalarySettingsDataExtraUrl, saveSalarySettingsUrl,deleteSalarySettingsByIdUrl) {
        $scope.SalarySettingsId = SalarySettingsId;
        $scope.getSalarySettingsByIdUrl = getSalarySettingsByIdUrl;
        $scope.saveSalarySettingsUrl = saveSalarySettingsUrl;
        $scope.getSalarySettingsDataExtraUrl = getSalarySettingsDataExtraUrl;
        $scope.deleteSalarySettingsByIdUrl = deleteSalarySettingsByIdUrl;

        $scope.loadPage(SalarySettingsId);
    };
    
  $scope.onAfterSaveSalarySettings= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetSalarySettingsById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadSalarySettingsDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
         if(result.DataExtra.EmployeeList!=null)
         $scope.EmployeeList =  result.DataExtra.EmployeeList; 
  /*
  //Child Table Bindings, need to fix
             $scope.SalarySettingsIdList =  result.DataExtra.SalarySettingsIdList; 

             $scope.SalarySettingsIdList =  result.DataExtra.SalarySettingsIdList; 
     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

