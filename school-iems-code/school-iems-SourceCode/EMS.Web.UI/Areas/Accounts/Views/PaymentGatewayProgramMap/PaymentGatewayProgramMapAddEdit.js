
//File: Acnt_PaymentGatewayProgramMap Anjular  Controller
emsApp.controller('PaymentGatewayProgramMapAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.PaymentGatewayProgramMap = [];
    $scope.PaymentGatewayProgramMapId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (PaymentGatewayProgramMapId)
    {
       if(PaymentGatewayProgramMapId!=null)
        $scope.PaymentGatewayProgramMapId=PaymentGatewayProgramMapId;
       
       $scope.loadPaymentGatewayProgramMapDataExtra($scope.PaymentGatewayProgramMapId);
       $scope.getPaymentGatewayProgramMapById($scope.PaymentGatewayProgramMapId);
    };
   $scope.getNewPaymentGatewayProgramMap= function()
    {
    	  $scope.getPaymentGatewayProgramMapById(0);
    };
   $scope.getPaymentGatewayProgramMapById= function(PaymentGatewayProgramMapId)
    {
        if(PaymentGatewayProgramMapId!=null && PaymentGatewayProgramMapId!=='')
        $scope.PaymentGatewayProgramMapId=PaymentGatewayProgramMapId;
        else return;
        
        $http.get($scope.getPaymentGatewayProgramMapByIdUrl, {
            params: { "id": $scope.PaymentGatewayProgramMapId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.PaymentGatewayProgramMap = result.Data;
                updateUrlQuery('id' , $scope.PaymentGatewayProgramMap.Id);
                $scope.onAfterGetPaymentGatewayProgramMapById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Payment Gateway Program Map! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Payment Gateway Program Map! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadPaymentGatewayProgramMapDataExtra= function(PaymentGatewayProgramMapId)
    {
        if(PaymentGatewayProgramMapId!=null)
            $scope.PaymentGatewayProgramMapId=PaymentGatewayProgramMapId;
            
            $http.get($scope.getPaymentGatewayProgramMapDataExtraUrl, {
                params: { "id": $scope.PaymentGatewayProgramMapId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                        $scope.onAfterLoadPaymentGatewayProgramMapDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Payment Gateway Program Map! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Payment Gateway Program Map! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.savePaymentGatewayProgramMap= function(){
    	if(!$scope.validatePaymentGatewayProgramMap())
        {
          return;
        }
        $http.post($scope.savePaymentGatewayProgramMapUrl + "/", $scope.PaymentGatewayProgramMap).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.PaymentGatewayProgramMap = result.Data;
                    updateUrlQuery('id', $scope.PaymentGatewayProgramMap.Id);
                   }
                   alertSuccess("Successfully saved Payment Gateway Program Map data!");
                   $scope.onAfterSavePaymentGatewayProgramMap(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Payment Gateway Program Map! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Payment Gateway Program Map! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deletePaymentGatewayProgramMapById= function(){
    	
    };
   $scope.validatePaymentGatewayProgramMap= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (PaymentGatewayProgramMapId,getPaymentGatewayProgramMapByIdUrl,getPaymentGatewayProgramMapDataExtraUrl, savePaymentGatewayProgramMapUrl,deletePaymentGatewayProgramMapByIdUrl) {
        $scope.PaymentGatewayProgramMapId = PaymentGatewayProgramMapId;
        $scope.getPaymentGatewayProgramMapByIdUrl = getPaymentGatewayProgramMapByIdUrl;
        $scope.savePaymentGatewayProgramMapUrl = savePaymentGatewayProgramMapUrl;
        $scope.getPaymentGatewayProgramMapDataExtraUrl = getPaymentGatewayProgramMapDataExtraUrl;
        $scope.deletePaymentGatewayProgramMapByIdUrl = deletePaymentGatewayProgramMapByIdUrl;

        $scope.loadPage(PaymentGatewayProgramMapId);
    };
    
  $scope.onAfterSavePaymentGatewayProgramMap= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetPaymentGatewayProgramMapById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadPaymentGatewayProgramMapDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
         if(result.DataExtra.PaymentGatewayList!=null)
         $scope.PaymentGatewayList =  result.DataExtra.PaymentGatewayList; 
  /*
  //Child Table Bindings, need to fix     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

