
//File: HR_EmpLeaveAllocationSettings Anjular  Controller
emsApp.controller('EmpLeaveAllocationSettingsAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.EmpLeaveAllocationSettings = [];
    $scope.EmpLeaveAllocationSettingsId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (EmpLeaveAllocationSettingsId)
    {
       if(EmpLeaveAllocationSettingsId!=null)
        $scope.EmpLeaveAllocationSettingsId=EmpLeaveAllocationSettingsId;
       
       $scope.loadEmpLeaveAllocationSettingsDataExtra($scope.EmpLeaveAllocationSettingsId);
       $scope.getEmpLeaveAllocationSettingsById($scope.EmpLeaveAllocationSettingsId);
    };
   $scope.getNewEmpLeaveAllocationSettings= function()
    {
    	  $scope.getEmpLeaveAllocationSettingsById(0);
    };
   $scope.getEmpLeaveAllocationSettingsById= function(EmpLeaveAllocationSettingsId)
    {
        if(EmpLeaveAllocationSettingsId!=null && EmpLeaveAllocationSettingsId!=='')
        $scope.EmpLeaveAllocationSettingsId=EmpLeaveAllocationSettingsId;
        else return;
        
        $http.get($scope.getEmpLeaveAllocationSettingsByIdUrl, {
            params: { "id": $scope.EmpLeaveAllocationSettingsId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.EmpLeaveAllocationSettings = result.Data;
                updateUrlQuery('id' , $scope.EmpLeaveAllocationSettings.Id);
                $scope.onAfterGetEmpLeaveAllocationSettingsById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Emp Leave Allocation Settings! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Emp Leave Allocation Settings! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadEmpLeaveAllocationSettingsDataExtra= function(EmpLeaveAllocationSettingsId)
    {
        if(EmpLeaveAllocationSettingsId!=null)
            $scope.EmpLeaveAllocationSettingsId=EmpLeaveAllocationSettingsId;
            
            $http.get($scope.getEmpLeaveAllocationSettingsDataExtraUrl, {
                params: { "id": $scope.EmpLeaveAllocationSettingsId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                        $scope.onAfterLoadEmpLeaveAllocationSettingsDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Emp Leave Allocation Settings! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Emp Leave Allocation Settings! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveEmpLeaveAllocationSettings= function(){
    	if(!$scope.validateEmpLeaveAllocationSettings())
        {
          return;
        }
        $http.post($scope.saveEmpLeaveAllocationSettingsUrl + "/", $scope.EmpLeaveAllocationSettings).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.EmpLeaveAllocationSettings = result.Data;
                    updateUrlQuery('id', $scope.EmpLeaveAllocationSettings.Id);
                   }
                   alertSuccess("Successfully saved Emp Leave Allocation Settings data!");
                   $scope.onAfterSaveEmpLeaveAllocationSettings(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Emp Leave Allocation Settings! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Emp Leave Allocation Settings! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteEmpLeaveAllocationSettingsById= function(){
    	
    };
   $scope.validateEmpLeaveAllocationSettings= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (EmpLeaveAllocationSettingsId,getEmpLeaveAllocationSettingsByIdUrl,getEmpLeaveAllocationSettingsDataExtraUrl, saveEmpLeaveAllocationSettingsUrl,deleteEmpLeaveAllocationSettingsByIdUrl) {
        $scope.EmpLeaveAllocationSettingsId = EmpLeaveAllocationSettingsId;
        $scope.getEmpLeaveAllocationSettingsByIdUrl = getEmpLeaveAllocationSettingsByIdUrl;
        $scope.saveEmpLeaveAllocationSettingsUrl = saveEmpLeaveAllocationSettingsUrl;
        $scope.getEmpLeaveAllocationSettingsDataExtraUrl = getEmpLeaveAllocationSettingsDataExtraUrl;
        $scope.deleteEmpLeaveAllocationSettingsByIdUrl = deleteEmpLeaveAllocationSettingsByIdUrl;

        $scope.loadPage(EmpLeaveAllocationSettingsId);
    };
    
  $scope.onAfterSaveEmpLeaveAllocationSettings= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetEmpLeaveAllocationSettingsById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadEmpLeaveAllocationSettingsDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
         if(result.DataExtra.EmployeeList!=null)
         $scope.EmployeeList =  result.DataExtra.EmployeeList; 
  /*
  //Child Table Bindings, need to fix
             $scope.EmpLeaveAllocationSettingsIdList =  result.DataExtra.EmpLeaveAllocationSettingsIdList; 

             $scope.LeaveAllocationSettingsIdList =  result.DataExtra.LeaveAllocationSettingsIdList; 
     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

