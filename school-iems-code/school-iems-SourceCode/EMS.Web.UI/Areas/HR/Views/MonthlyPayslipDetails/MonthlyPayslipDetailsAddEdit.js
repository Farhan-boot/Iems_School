
//File: HR_MonthlyPayslipDetails Anjular  Controller
emsApp.controller('MonthlyPayslipDetailsAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.MonthlyPayslipDetails = [];
    $scope.MonthlyPayslipDetailsId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (MonthlyPayslipDetailsId)
    {
       if(MonthlyPayslipDetailsId!=null)
        $scope.MonthlyPayslipDetailsId=MonthlyPayslipDetailsId;
       
       $scope.loadMonthlyPayslipDetailsDataExtra($scope.MonthlyPayslipDetailsId);
       $scope.getMonthlyPayslipDetailsById($scope.MonthlyPayslipDetailsId);
    };
   $scope.getNewMonthlyPayslipDetails= function()
    {
    	  $scope.getMonthlyPayslipDetailsById(0);
    };
   $scope.getMonthlyPayslipDetailsById= function(MonthlyPayslipDetailsId)
    {
        if(MonthlyPayslipDetailsId!=null && MonthlyPayslipDetailsId!=='')
        $scope.MonthlyPayslipDetailsId=MonthlyPayslipDetailsId;
        else return;
        
        $http.get($scope.getMonthlyPayslipDetailsByIdUrl, {
            params: { "id": $scope.MonthlyPayslipDetailsId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.MonthlyPayslipDetails = result.Data;
                updateUrlQuery('id' , $scope.MonthlyPayslipDetails.Id);
                $scope.onAfterGetMonthlyPayslipDetailsById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Monthly Payslip Details! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Monthly Payslip Details! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadMonthlyPayslipDetailsDataExtra= function(MonthlyPayslipDetailsId)
    {
        if(MonthlyPayslipDetailsId!=null)
            $scope.MonthlyPayslipDetailsId=MonthlyPayslipDetailsId;
            
            $http.get($scope.getMonthlyPayslipDetailsDataExtraUrl, {
                params: { "id": $scope.MonthlyPayslipDetailsId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.SalaryTypeEnumList!=null)
                         $scope.SalaryTypeEnumList = result.DataExtra.SalaryTypeEnumList;
                      if(result.DataExtra.CategoryTypeEnumList!=null)
                         $scope.CategoryTypeEnumList = result.DataExtra.CategoryTypeEnumList;
                        $scope.onAfterLoadMonthlyPayslipDetailsDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Monthly Payslip Details! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Monthly Payslip Details! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveMonthlyPayslipDetails= function(){
    	if(!$scope.validateMonthlyPayslipDetails())
        {
          return;
        }
        $http.post($scope.saveMonthlyPayslipDetailsUrl + "/", $scope.MonthlyPayslipDetails).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.MonthlyPayslipDetails = result.Data;
                    updateUrlQuery('id', $scope.MonthlyPayslipDetails.Id);
                   }
                   alertSuccess("Successfully saved Monthly Payslip Details data!");
                   $scope.onAfterSaveMonthlyPayslipDetails(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Monthly Payslip Details! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Monthly Payslip Details! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteMonthlyPayslipDetailsById= function(){
    	
    };
   $scope.validateMonthlyPayslipDetails= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (MonthlyPayslipDetailsId,getMonthlyPayslipDetailsByIdUrl,getMonthlyPayslipDetailsDataExtraUrl, saveMonthlyPayslipDetailsUrl,deleteMonthlyPayslipDetailsByIdUrl) {
        $scope.MonthlyPayslipDetailsId = MonthlyPayslipDetailsId;
        $scope.getMonthlyPayslipDetailsByIdUrl = getMonthlyPayslipDetailsByIdUrl;
        $scope.saveMonthlyPayslipDetailsUrl = saveMonthlyPayslipDetailsUrl;
        $scope.getMonthlyPayslipDetailsDataExtraUrl = getMonthlyPayslipDetailsDataExtraUrl;
        $scope.deleteMonthlyPayslipDetailsByIdUrl = deleteMonthlyPayslipDetailsByIdUrl;

        $scope.loadPage(MonthlyPayslipDetailsId);
    };
    
  $scope.onAfterSaveMonthlyPayslipDetails= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetMonthlyPayslipDetailsById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadMonthlyPayslipDetailsDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
         if(result.DataExtra.MonthlyPayslipList!=null)
         $scope.MonthlyPayslipList =  result.DataExtra.MonthlyPayslipList; 
         if(result.DataExtra.EmployeeList!=null)
         $scope.EmployeeList =  result.DataExtra.EmployeeList; 
  /*
  //Child Table Bindings, need to fix     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

