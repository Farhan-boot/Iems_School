
//File: HR_MonthlyPayslip Anjular  Controller
emsApp.controller('MonthlyPayslipAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.MonthlyPayslip = [];
    $scope.MonthlyPayslipId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (MonthlyPayslipId)
    {
       if(MonthlyPayslipId!=null)
        $scope.MonthlyPayslipId=MonthlyPayslipId;
       
       $scope.loadMonthlyPayslipDataExtra($scope.MonthlyPayslipId);
       $scope.getMonthlyPayslipById($scope.MonthlyPayslipId);
    };
   $scope.getNewMonthlyPayslip= function()
    {
    	  $scope.getMonthlyPayslipById(0);
    };
   $scope.getMonthlyPayslipById= function(MonthlyPayslipId)
    {
        if(MonthlyPayslipId!=null && MonthlyPayslipId!=='')
        $scope.MonthlyPayslipId=MonthlyPayslipId;
        else return;
        
        $http.get($scope.getMonthlyPayslipByIdUrl, {
            params: { "id": $scope.MonthlyPayslipId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.MonthlyPayslip = result.Data;
                updateUrlQuery('id' , $scope.MonthlyPayslip.Id);
                $scope.onAfterGetMonthlyPayslipById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Monthly Payslip! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Monthly Payslip! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadMonthlyPayslipDataExtra= function(MonthlyPayslipId)
    {
        if(MonthlyPayslipId!=null)
            $scope.MonthlyPayslipId=MonthlyPayslipId;
            
            $http.get($scope.getMonthlyPayslipDataExtraUrl, {
                params: { "id": $scope.MonthlyPayslipId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                        $scope.onAfterLoadMonthlyPayslipDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Monthly Payslip! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Monthly Payslip! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveMonthlyPayslip= function(){
    	if(!$scope.validateMonthlyPayslip())
        {
          return;
        }
        $http.post($scope.saveMonthlyPayslipUrl + "/", $scope.MonthlyPayslip).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.MonthlyPayslip = result.Data;
                    updateUrlQuery('id', $scope.MonthlyPayslip.Id);
                   }
                   alertSuccess("Successfully saved Monthly Payslip data!");
                   $scope.onAfterSaveMonthlyPayslip(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Monthly Payslip! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Monthly Payslip! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteMonthlyPayslipById= function(){
    	
    };
   $scope.validateMonthlyPayslip= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (MonthlyPayslipId,getMonthlyPayslipByIdUrl,getMonthlyPayslipDataExtraUrl, saveMonthlyPayslipUrl,deleteMonthlyPayslipByIdUrl) {
        $scope.MonthlyPayslipId = MonthlyPayslipId;
        $scope.getMonthlyPayslipByIdUrl = getMonthlyPayslipByIdUrl;
        $scope.saveMonthlyPayslipUrl = saveMonthlyPayslipUrl;
        $scope.getMonthlyPayslipDataExtraUrl = getMonthlyPayslipDataExtraUrl;
        $scope.deleteMonthlyPayslipByIdUrl = deleteMonthlyPayslipByIdUrl;

        $scope.loadPage(MonthlyPayslipId);
    };
    
  $scope.onAfterSaveMonthlyPayslip= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetMonthlyPayslipById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadMonthlyPayslipDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
         if(result.DataExtra.PayrollList!=null)
         $scope.PayrollList =  result.DataExtra.PayrollList; 
         if(result.DataExtra.SalarySettingsList!=null)
         $scope.SalarySettingsList =  result.DataExtra.SalarySettingsList; 
         if(result.DataExtra.EmployeeList!=null)
         $scope.EmployeeList =  result.DataExtra.EmployeeList; 
  /*
  //Child Table Bindings, need to fix
             $scope.MonthlyPayslipIdList =  result.DataExtra.MonthlyPayslipIdList; 
     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

