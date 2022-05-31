
//File: BAcnt_CompanyAccount Anjular  Controller
emsApp.controller('CompanyAccountAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.CompanyAccount = [];
    $scope.CompanyAccountId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (CompanyAccountId)
    {
       if(CompanyAccountId!=null)
        $scope.CompanyAccountId=CompanyAccountId;
       
       $scope.loadCompanyAccountDataExtra($scope.CompanyAccountId);
       $scope.getCompanyAccountById($scope.CompanyAccountId);
    };
   $scope.getNewCompanyAccount= function()
    {
    	  $scope.getCompanyAccountById(0);
    };
   $scope.getCompanyAccountById= function(CompanyAccountId)
    {
        if(CompanyAccountId!=null && CompanyAccountId!=='')
        $scope.CompanyAccountId=CompanyAccountId;
        else return;
        
        $http.get($scope.getCompanyAccountByIdUrl, {
            params: { "id": $scope.CompanyAccountId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.CompanyAccount = result.Data;
                updateUrlQuery('id' , $scope.CompanyAccount.Id);
                $scope.onAfterGetCompanyAccountById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Company Account! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Company Account! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadCompanyAccountDataExtra= function(CompanyAccountId)
    {
        if(CompanyAccountId!=null)
            $scope.CompanyAccountId=CompanyAccountId;
            
            $http.get($scope.getCompanyAccountDataExtraUrl, {
                params: { "id": $scope.CompanyAccountId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                        $scope.onAfterLoadCompanyAccountDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Company Account! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Company Account! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveCompanyAccount= function(){
    	if(!$scope.validateCompanyAccount())
        {
          return;
        }
        $http.post($scope.saveCompanyAccountUrl + "/", $scope.CompanyAccount).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.CompanyAccount = result.Data;
                    updateUrlQuery('id', $scope.CompanyAccount.Id);
                   }
                   alertSuccess("Successfully saved Company Account data!");
                   $scope.onAfterSaveCompanyAccount(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Company Account! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Company Account! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteCompanyAccountById= function(){
    	
    };
   $scope.validateCompanyAccount= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (CompanyAccountId,getCompanyAccountByIdUrl,getCompanyAccountDataExtraUrl, saveCompanyAccountUrl,deleteCompanyAccountByIdUrl) {
        $scope.CompanyAccountId = CompanyAccountId;
        $scope.getCompanyAccountByIdUrl = getCompanyAccountByIdUrl;
        $scope.saveCompanyAccountUrl = saveCompanyAccountUrl;
        $scope.getCompanyAccountDataExtraUrl = getCompanyAccountDataExtraUrl;
        $scope.deleteCompanyAccountByIdUrl = deleteCompanyAccountByIdUrl;

        $scope.loadPage(CompanyAccountId);
    };
    
  $scope.onAfterSaveCompanyAccount= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetCompanyAccountById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadCompanyAccountDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
  /*
  //Child Table Bindings, need to fix
             $scope.CompanyIdList =  result.DataExtra.CompanyIdList; 

             $scope.CompanyIdList =  result.DataExtra.CompanyIdList; 

             $scope.CompanyIdList =  result.DataExtra.CompanyIdList; 

             $scope.CompanyIdList =  result.DataExtra.CompanyIdList; 
     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

