
//File: Acnt_StdOnlinePaySlip Anjular  Controller
emsApp.controller('StdOnlinePaySlipAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.StdOnlinePaySlip = [];
    $scope.StdOnlinePaySlipId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (StdOnlinePaySlipId)
    {
       if(StdOnlinePaySlipId!=null)
        $scope.StdOnlinePaySlipId=StdOnlinePaySlipId;
       
       $scope.loadStdOnlinePaySlipDataExtra($scope.StdOnlinePaySlipId);
       $scope.getStdOnlinePaySlipById($scope.StdOnlinePaySlipId);
    };
   $scope.getNewStdOnlinePaySlip= function()
    {
    	  $scope.getStdOnlinePaySlipById(0);
    };
   $scope.getStdOnlinePaySlipById= function(StdOnlinePaySlipId)
    {
        if(StdOnlinePaySlipId!=null && StdOnlinePaySlipId!=='')
        $scope.StdOnlinePaySlipId=StdOnlinePaySlipId;
        else return;
        
        $http.get($scope.getStdOnlinePaySlipByIdUrl, {
            params: { "id": $scope.StdOnlinePaySlipId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.StdOnlinePaySlip = result.Data;
                updateUrlQuery('id' , $scope.StdOnlinePaySlip.Id);
                $scope.onAfterGetStdOnlinePaySlipById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Std Online Pay Slip! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Std Online Pay Slip! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadStdOnlinePaySlipDataExtra= function(StdOnlinePaySlipId)
    {
        if(StdOnlinePaySlipId!=null)
            $scope.StdOnlinePaySlipId=StdOnlinePaySlipId;
            
            $http.get($scope.getStdOnlinePaySlipDataExtraUrl, {
                params: { "id": $scope.StdOnlinePaySlipId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.TransStatusEnumList!=null)
                         $scope.TransStatusEnumList = result.DataExtra.TransStatusEnumList;
                        $scope.onAfterLoadStdOnlinePaySlipDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Std Online Pay Slip! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Std Online Pay Slip! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveStdOnlinePaySlip= function(){
    	if(!$scope.validateStdOnlinePaySlip())
        {
          return;
        }
        $http.post($scope.saveStdOnlinePaySlipUrl + "/", $scope.StdOnlinePaySlip).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.StdOnlinePaySlip = result.Data;
                    updateUrlQuery('id', $scope.StdOnlinePaySlip.Id);
                   }
                   alertSuccess("Successfully saved Std Online Pay Slip data!");
                   $scope.onAfterSaveStdOnlinePaySlip(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Std Online Pay Slip! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Std Online Pay Slip! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteStdOnlinePaySlipById= function(){
    	
    };
   $scope.validateStdOnlinePaySlip= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (StdOnlinePaySlipId,getStdOnlinePaySlipByIdUrl,getStdOnlinePaySlipDataExtraUrl, saveStdOnlinePaySlipUrl,deleteStdOnlinePaySlipByIdUrl) {
        $scope.StdOnlinePaySlipId = StdOnlinePaySlipId;
        $scope.getStdOnlinePaySlipByIdUrl = getStdOnlinePaySlipByIdUrl;
        $scope.saveStdOnlinePaySlipUrl = saveStdOnlinePaySlipUrl;
        $scope.getStdOnlinePaySlipDataExtraUrl = getStdOnlinePaySlipDataExtraUrl;
        $scope.deleteStdOnlinePaySlipByIdUrl = deleteStdOnlinePaySlipByIdUrl;

        $scope.loadPage(StdOnlinePaySlipId);
    };
    
  $scope.onAfterSaveStdOnlinePaySlip= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetStdOnlinePaySlipById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadStdOnlinePaySlipDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
         if(result.DataExtra.SemesterList!=null)
         $scope.SemesterList =  result.DataExtra.SemesterList; 
         if(result.DataExtra.OfficialBankList!=null)
         $scope.OfficialBankList =  result.DataExtra.OfficialBankList; 
         if(result.DataExtra.StdTransactionList!=null)
         $scope.StdTransactionList =  result.DataExtra.StdTransactionList; 
         if(result.DataExtra.StudentList!=null)
         $scope.StudentList =  result.DataExtra.StudentList; 
  /*
  //Child Table Bindings, need to fix     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

