
//File: HR_EmpLeaveAllocationSettingsDetails Anjular  Controller
emsApp.controller('EmpLeaveAllocationSettingsDetailsAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.EmpLeaveAllocationSettingsDetails = [];
    $scope.EmpLeaveAllocationSettingsDetailsId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (EmpLeaveAllocationSettingsDetailsId)
    {
       if(EmpLeaveAllocationSettingsDetailsId!=null)
        $scope.EmpLeaveAllocationSettingsDetailsId=EmpLeaveAllocationSettingsDetailsId;
       
       $scope.loadEmpLeaveAllocationSettingsDetailsDataExtra($scope.EmpLeaveAllocationSettingsDetailsId);
       $scope.getEmpLeaveAllocationSettingsDetailsById($scope.EmpLeaveAllocationSettingsDetailsId);
    };
   $scope.getNewEmpLeaveAllocationSettingsDetails= function()
    {
    	  $scope.getEmpLeaveAllocationSettingsDetailsById(0);
    };
   $scope.getEmpLeaveAllocationSettingsDetailsById= function(EmpLeaveAllocationSettingsDetailsId)
    {
        if(EmpLeaveAllocationSettingsDetailsId!=null && EmpLeaveAllocationSettingsDetailsId!=='')
        $scope.EmpLeaveAllocationSettingsDetailsId=EmpLeaveAllocationSettingsDetailsId;
        else return;
        
        $http.get($scope.getEmpLeaveAllocationSettingsDetailsByIdUrl, {
            params: { "id": $scope.EmpLeaveAllocationSettingsDetailsId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.EmpLeaveAllocationSettingsDetails = result.Data;
                updateUrlQuery('id' , $scope.EmpLeaveAllocationSettingsDetails.Id);
                $scope.onAfterGetEmpLeaveAllocationSettingsDetailsById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Emp Leave Allocation Settings Details! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Emp Leave Allocation Settings Details! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadEmpLeaveAllocationSettingsDetailsDataExtra= function(EmpLeaveAllocationSettingsDetailsId)
    {
        if(EmpLeaveAllocationSettingsDetailsId!=null)
            $scope.EmpLeaveAllocationSettingsDetailsId=EmpLeaveAllocationSettingsDetailsId;
            
            $http.get($scope.getEmpLeaveAllocationSettingsDetailsDataExtraUrl, {
                params: { "id": $scope.EmpLeaveAllocationSettingsDetailsId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.LeaveTypeEnumList!=null)
                         $scope.LeaveTypeEnumList = result.DataExtra.LeaveTypeEnumList;
                        $scope.onAfterLoadEmpLeaveAllocationSettingsDetailsDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Emp Leave Allocation Settings Details! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Emp Leave Allocation Settings Details! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveEmpLeaveAllocationSettingsDetails= function(){
    	if(!$scope.validateEmpLeaveAllocationSettingsDetails())
        {
          return;
        }
        $http.post($scope.saveEmpLeaveAllocationSettingsDetailsUrl + "/", $scope.EmpLeaveAllocationSettingsDetails).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.EmpLeaveAllocationSettingsDetails = result.Data;
                    updateUrlQuery('id', $scope.EmpLeaveAllocationSettingsDetails.Id);
                   }
                   alertSuccess("Successfully saved Emp Leave Allocation Settings Details data!");
                   $scope.onAfterSaveEmpLeaveAllocationSettingsDetails(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Emp Leave Allocation Settings Details! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Emp Leave Allocation Settings Details! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteEmpLeaveAllocationSettingsDetailsById= function(){
    	
    };
   $scope.validateEmpLeaveAllocationSettingsDetails= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (EmpLeaveAllocationSettingsDetailsId,getEmpLeaveAllocationSettingsDetailsByIdUrl,getEmpLeaveAllocationSettingsDetailsDataExtraUrl, saveEmpLeaveAllocationSettingsDetailsUrl,deleteEmpLeaveAllocationSettingsDetailsByIdUrl) {
        $scope.EmpLeaveAllocationSettingsDetailsId = EmpLeaveAllocationSettingsDetailsId;
        $scope.getEmpLeaveAllocationSettingsDetailsByIdUrl = getEmpLeaveAllocationSettingsDetailsByIdUrl;
        $scope.saveEmpLeaveAllocationSettingsDetailsUrl = saveEmpLeaveAllocationSettingsDetailsUrl;
        $scope.getEmpLeaveAllocationSettingsDetailsDataExtraUrl = getEmpLeaveAllocationSettingsDetailsDataExtraUrl;
        $scope.deleteEmpLeaveAllocationSettingsDetailsByIdUrl = deleteEmpLeaveAllocationSettingsDetailsByIdUrl;

        $scope.loadPage(EmpLeaveAllocationSettingsDetailsId);
    };
    
  $scope.onAfterSaveEmpLeaveAllocationSettingsDetails= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetEmpLeaveAllocationSettingsDetailsById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadEmpLeaveAllocationSettingsDetailsDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
         if(result.DataExtra.EmpLeaveAllocationSettingsList!=null)
         $scope.EmpLeaveAllocationSettingsList =  result.DataExtra.EmpLeaveAllocationSettingsList; 
  /*
  //Child Table Bindings, need to fix     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

