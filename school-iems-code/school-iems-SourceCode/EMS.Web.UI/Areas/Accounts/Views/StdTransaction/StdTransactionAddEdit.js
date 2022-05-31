
//File: Acnt_StdTransaction Anjular  Controller
emsApp.controller('StdTransactionAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.StdTransaction = [];
    $scope.StdTransactionId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (StdTransactionId)
    {
       if(StdTransactionId!=null)
        $scope.StdTransactionId=StdTransactionId;
       
       $scope.loadStdTransactionDataExtra($scope.StdTransactionId);
       $scope.getStdTransactionById($scope.StdTransactionId);
    };
   $scope.getNewStdTransaction= function()
    {
    	  $scope.getStdTransactionById(0);
    };
   $scope.getStdTransactionById= function(StdTransactionId)
    {
        if(StdTransactionId!=null && StdTransactionId!=='')
        $scope.StdTransactionId=StdTransactionId;
        else return;
        
        $http.get($scope.getStdTransactionByIdUrl, {
            params: { "id": $scope.StdTransactionId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.StdTransaction = result.Data;
                updateUrlQuery('id' , $scope.StdTransaction.Id);
                $scope.onAfterGetStdTransactionById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Std Transaction! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Std Transaction! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadStdTransactionDataExtra= function(StdTransactionId)
    {
        if(StdTransactionId!=null)
            $scope.StdTransactionId=StdTransactionId;
            
            $http.get($scope.getStdTransactionDataExtraUrl, {
                params: { "id": $scope.StdTransactionId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.PaymentStatusEnumList!=null)
                         $scope.PaymentStatusEnumList = result.DataExtra.PaymentStatusEnumList;
                      if(result.DataExtra.TransactionTypeEnumList!=null)
                         $scope.TransactionTypeEnumList = result.DataExtra.TransactionTypeEnumList;
                        $scope.onAfterLoadStdTransactionDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Std Transaction! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Std Transaction! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveStdTransaction= function(){
    	if(!$scope.validateStdTransaction())
        {
          return;
        }
        $http.post($scope.saveStdTransactionUrl + "/", $scope.StdTransaction).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.StdTransaction = result.Data;
                    updateUrlQuery('id', $scope.StdTransaction.Id);
                   }
                   alertSuccess("Successfully saved Std Transaction data!");
                   $scope.onAfterSaveStdTransaction(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Std Transaction! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Std Transaction! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteStdTransactionById= function(){
    	
    };
   $scope.validateStdTransaction= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (StdTransactionId,getStdTransactionByIdUrl,getStdTransactionDataExtraUrl, saveStdTransactionUrl,deleteStdTransactionByIdUrl) {
        $scope.StdTransactionId = StdTransactionId;
        $scope.getStdTransactionByIdUrl = getStdTransactionByIdUrl;
        $scope.saveStdTransactionUrl = saveStdTransactionUrl;
        $scope.getStdTransactionDataExtraUrl = getStdTransactionDataExtraUrl;
        $scope.deleteStdTransactionByIdUrl = deleteStdTransactionByIdUrl;

        $scope.loadPage(StdTransactionId);
    };
    
  $scope.onAfterSaveStdTransaction= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetStdTransactionById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadStdTransactionDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
         if(result.DataExtra.SemesterList!=null)
         $scope.SemesterList =  result.DataExtra.SemesterList; 
         if(result.DataExtra.StudentList!=null)
         $scope.StudentList =  result.DataExtra.StudentList; 
  /*
  //Child Table Bindings, need to fix
             $scope.TrasectionIdList =  result.DataExtra.TrasectionIdList; 
     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

