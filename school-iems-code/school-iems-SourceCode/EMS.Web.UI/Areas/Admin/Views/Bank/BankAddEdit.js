
//File: General_Bank Anjular  Controller
emsApp.controller('BankAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.Bank = [];
    $scope.BankId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;

   $scope.loadPage= function (BankId)
    {
       if(BankId!=null)
        $scope.BankId=BankId;
       
       $scope.loadBankExtraData($scope.BankId);
       $scope.getBankById($scope.BankId);
    };
   $scope.getNewBank= function()
    {
    	  $scope.getBankById(0);
    };
   $scope.getBankById= function(BankId)
    {
        if(BankId!=null && BankId!=='')
        $scope.BankId=BankId;
        else return;
        
        $http.get($scope.getBankByIdUrl, {
            params: { "id": $scope.BankId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.Bank = result.Data;
                updateUrlQuery('id' , $scope.Bank.Id);
                $scope.onAfterGetBankById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Bank! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Bank! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadBankExtraData= function(BankId)
    {
        if(BankId!=null)
            $scope.BankId=BankId;
            
            $http.get($scope.getBankExtraDataUrl, {
                params: { "id": $scope.BankId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                        $scope.onAfterLoadBankExtraData(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Bank! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Bank! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveBank= function(){
    	if(!$scope.validateBank())
        {
          return;
        }
        $http.post($scope.saveBankUrl + "/", $scope.Bank).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.Bank = result.Data;
                    updateUrlQuery('id', $scope.Bank.Id);
                   }
                   alertSuccess("Successfully saved Bank data!");
                   $scope.onAfterSaveBank(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Bank! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Bank! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteBankById= function(){
    	
    };
   $scope.validateBank= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (BankId,getBankByIdUrl,getBankExtraDataUrl, saveBankUrl,deleteBankByIdUrl) {
        $scope.BankId = BankId;
        $scope.getBankByIdUrl = getBankByIdUrl;
        $scope.saveBankUrl = saveBankUrl;
        $scope.getBankExtraDataUrl = getBankExtraDataUrl;
        $scope.deleteBankByIdUrl = deleteBankByIdUrl;

        $scope.loadPage(BankId);
    };
    
  $scope.onAfterSaveBank= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetBankById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadBankExtraData= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
  /*
  //Child Table Bindings, need to fix
             $scope.BankIdList =  result.DataExtra.BankIdList; 
     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

