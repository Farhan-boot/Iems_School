
//File: HR_Payroll Anjular  Controller
emsApp.controller('PayrollAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar, $sce) {
    $scope.Payroll = [];
    $scope.PayrollId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (PayrollId)
    {
       if(PayrollId!=null)
        $scope.PayrollId=PayrollId;
       
       $scope.loadPayrollDataExtra($scope.PayrollId);
       $scope.getPayrollById($scope.PayrollId);
    };
   $scope.getNewPayroll= function()
    {
    	  $scope.getPayrollById(0);
    };
   $scope.getPayrollById= function(PayrollId)
    {
        if(PayrollId!=null && PayrollId!=='')
        $scope.PayrollId=PayrollId;
        else return;
        
        $http.get($scope.getPayrollByIdUrl, {
            params: { "id": $scope.PayrollId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.Payroll = result.Data;
                updateUrlQuery('id' , $scope.Payroll.Id);
                $scope.onAfterGetPayrollById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Payroll! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Payroll! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadPayrollDataExtra= function(PayrollId)
    {
        if(PayrollId!=null)
            $scope.PayrollId=PayrollId;
            
            $http.get($scope.getPayrollDataExtraUrl, {
                params: { "id": $scope.PayrollId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.MonthEnumList!=null)
                         $scope.MonthEnumList = result.DataExtra.MonthEnumList;
                        $scope.onAfterLoadPayrollDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Payroll! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Payroll! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.savePayroll= function(){
    	if(!$scope.validatePayroll())
        {
          return;
        }
        $http.post($scope.savePayrollUrl + "/", $scope.Payroll).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.Payroll = result.Data;
                    updateUrlQuery('id', $scope.Payroll.Id);
                   }
                   alertSuccess("Successfully saved Payroll data!");
                   $scope.onAfterSavePayroll(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Payroll! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Payroll! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deletePayrollById= function(){
    	
    };
   $scope.validatePayroll= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (PayrollId,getPayrollByIdUrl,getPayrollDataExtraUrl, savePayrollUrl,deletePayrollByIdUrl) {
        $scope.PayrollId = PayrollId;
        $scope.getPayrollByIdUrl = getPayrollByIdUrl;
        $scope.savePayrollUrl = savePayrollUrl;
        $scope.getPayrollDataExtraUrl = getPayrollDataExtraUrl;
        $scope.deletePayrollByIdUrl = deletePayrollByIdUrl;

        $scope.loadPage(PayrollId);
    };
    
  $scope.onAfterSavePayroll= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetPayrollById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadPayrollDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
  /*
  //Child Table Bindings, need to fix
             $scope.PayrollIdList =  result.DataExtra.PayrollIdList; 
     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 
    $scope.renderHtml = function (html_code) {
        if (html_code == null || html_code === '') {
            html_code = '<center style="color:red;font-weight: bold;">No History Available.</center>';
        }
        return $sce.trustAsHtml(html_code);
    };
//======Custom property and Functions End========================================================== 
});

