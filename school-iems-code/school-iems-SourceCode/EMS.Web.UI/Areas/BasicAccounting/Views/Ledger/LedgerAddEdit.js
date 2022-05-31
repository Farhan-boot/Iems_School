
//File: BAcnt_Ledger Anjular  Controller
emsApp.controller('LedgerAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.Ledger = [];
    $scope.LedgerId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (LedgerId)
    {
       if(LedgerId!=null)
        $scope.LedgerId=LedgerId;
       
       $scope.loadLedgerDataExtra($scope.LedgerId);
       $scope.getLedgerById($scope.LedgerId);
    };
   $scope.getNewLedger= function()
    {
    	  $scope.getLedgerById(0);
    };
   $scope.getLedgerById= function(LedgerId)
    {
        if(LedgerId!=null && LedgerId!=='')
        $scope.LedgerId=LedgerId;
        else return;
        
        $http.get($scope.getLedgerByIdUrl, {
            params: { "id": $scope.LedgerId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.Ledger = result.Data;
                updateUrlQuery('id' , $scope.Ledger.Id);
                $scope.onAfterGetLedgerById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Ledger! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Ledger! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadLedgerDataExtra= function(LedgerId)
    {
        if(LedgerId!=null)
            $scope.LedgerId=LedgerId;
            
            $http.get($scope.getLedgerDataExtraUrl, {
                params: { "id": $scope.LedgerId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.TypeEnumList!=null)
                         $scope.TypeEnumList = result.DataExtra.TypeEnumList;
                        $scope.onAfterLoadLedgerDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Ledger! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Ledger! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveLedger= function(){
    	if(!$scope.validateLedger())
        {
          return;
        }
        $http.post($scope.saveLedgerUrl + "/", $scope.Ledger).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.Ledger = result.Data;
                    updateUrlQuery('id', $scope.Ledger.Id);
                   }
                   alertSuccess("Successfully saved Ledger data!");
                   $scope.onAfterSaveLedger(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Ledger! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Ledger! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteLedgerById= function(){
    	
    };
   $scope.validateLedger= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (LedgerId,getLedgerByIdUrl,getLedgerDataExtraUrl, saveLedgerUrl,deleteLedgerByIdUrl) {
        $scope.LedgerId = LedgerId;
        $scope.getLedgerByIdUrl = getLedgerByIdUrl;
        $scope.saveLedgerUrl = saveLedgerUrl;
        $scope.getLedgerDataExtraUrl = getLedgerDataExtraUrl;
        $scope.deleteLedgerByIdUrl = deleteLedgerByIdUrl;

        $scope.loadPage(LedgerId);
    };
    
  $scope.onAfterSaveLedger= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetLedgerById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadLedgerDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
         if(result.DataExtra.BranchList!=null)
         $scope.BranchList =  result.DataExtra.BranchList; 
         if(result.DataExtra.CompanyAccountList!=null)
         $scope.CompanyAccountList =  result.DataExtra.CompanyAccountList; 
  /*
  //Child Table Bindings, need to fix
             $scope.LedgerIdList =  result.DataExtra.LedgerIdList; 
     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

