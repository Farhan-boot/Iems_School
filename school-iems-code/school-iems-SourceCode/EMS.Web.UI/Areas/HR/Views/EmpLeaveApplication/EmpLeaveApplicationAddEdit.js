
//File: HR_EmpLeaveApplication Anjular  Controller
emsApp.controller('EmpLeaveApplicationAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.EmpLeaveApplication = [];
    $scope.EmpLeaveApplicationId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (EmpLeaveApplicationId)
    {
       if(EmpLeaveApplicationId!=null)
        $scope.EmpLeaveApplicationId=EmpLeaveApplicationId;
       
       $scope.loadEmpLeaveApplicationDataExtra($scope.EmpLeaveApplicationId);
       $scope.getEmpLeaveApplicationById($scope.EmpLeaveApplicationId);
    };
   $scope.getNewEmpLeaveApplication= function()
    {
    	  $scope.getEmpLeaveApplicationById(0);
    };
   $scope.getEmpLeaveApplicationById= function(EmpLeaveApplicationId)
    {
        if(EmpLeaveApplicationId!=null && EmpLeaveApplicationId!=='')
        $scope.EmpLeaveApplicationId=EmpLeaveApplicationId;
        else return;
        
        $http.get($scope.getEmpLeaveApplicationByIdUrl, {
            params: { "id": $scope.EmpLeaveApplicationId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.EmpLeaveApplication = result.Data;
                updateUrlQuery('id' , $scope.EmpLeaveApplication.Id);
                $scope.onAfterGetEmpLeaveApplicationById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Emp Leave Application! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Emp Leave Application! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadEmpLeaveApplicationDataExtra= function(EmpLeaveApplicationId)
    {
        if(EmpLeaveApplicationId!=null)
            $scope.EmpLeaveApplicationId=EmpLeaveApplicationId;
            
            $http.get($scope.getEmpLeaveApplicationDataExtraUrl, {
                params: { "id": $scope.EmpLeaveApplicationId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.LeaveTypeEnumList!=null)
                         $scope.LeaveTypeEnumList = result.DataExtra.LeaveTypeEnumList;
                      if(result.DataExtra.StatusEnumList!=null)
                         $scope.StatusEnumList = result.DataExtra.StatusEnumList;
                        $scope.onAfterLoadEmpLeaveApplicationDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Emp Leave Application! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Emp Leave Application! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveEmpLeaveApplication= function(){
    	if(!$scope.validateEmpLeaveApplication())
        {
          return;
        }
        $http.post($scope.saveEmpLeaveApplicationUrl + "/", $scope.EmpLeaveApplication).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.EmpLeaveApplication = result.Data;
                    updateUrlQuery('id', $scope.EmpLeaveApplication.Id);
                   }
                   alertSuccess("Successfully saved Emp Leave Application data!");
                   $scope.onAfterSaveEmpLeaveApplication(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Emp Leave Application! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Emp Leave Application! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteEmpLeaveApplicationById= function(){
    	
    };
   $scope.validateEmpLeaveApplication= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (EmpLeaveApplicationId,getEmpLeaveApplicationByIdUrl,getEmpLeaveApplicationDataExtraUrl, saveEmpLeaveApplicationUrl,deleteEmpLeaveApplicationByIdUrl) {
        $scope.EmpLeaveApplicationId = EmpLeaveApplicationId;
        $scope.getEmpLeaveApplicationByIdUrl = getEmpLeaveApplicationByIdUrl;
        $scope.saveEmpLeaveApplicationUrl = saveEmpLeaveApplicationUrl;
        $scope.getEmpLeaveApplicationDataExtraUrl = getEmpLeaveApplicationDataExtraUrl;
        $scope.deleteEmpLeaveApplicationByIdUrl = deleteEmpLeaveApplicationByIdUrl;

        $scope.loadPage(EmpLeaveApplicationId);
    };
    
  $scope.onAfterSaveEmpLeaveApplication= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetEmpLeaveApplicationById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadEmpLeaveApplicationDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
         if(result.DataExtra.EmpLeaveAllocationSettingsList!=null)
         $scope.EmpLeaveAllocationSettingsList =  result.DataExtra.EmpLeaveAllocationSettingsList; 
         if(result.DataExtra.EmployeeList!=null)
         $scope.EmployeeList =  result.DataExtra.EmployeeList; 
  /*
  //Child Table Bindings, need to fix     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

