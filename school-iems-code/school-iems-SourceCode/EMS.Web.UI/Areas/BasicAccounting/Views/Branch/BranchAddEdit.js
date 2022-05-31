
//File: BAcnt_Branch Anjular  Controller
emsApp.controller('BranchAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.Branch = [];
    $scope.BranchId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (BranchId)
    {
       if(BranchId!=null)
        $scope.BranchId=BranchId;
       
       $scope.loadBranchDataExtra($scope.BranchId);
       $scope.getBranchById($scope.BranchId);
    };
   $scope.getNewBranch= function()
    {
    	  $scope.getBranchById(0);
    };
   $scope.getBranchById= function(BranchId)
    {
        if(BranchId!=null && BranchId!=='')
        $scope.BranchId=BranchId;
        else return;
        
        $http.get($scope.getBranchByIdUrl, {
            params: { "id": $scope.BranchId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.Branch = result.Data;
                updateUrlQuery('id' , $scope.Branch.Id);
                $scope.onAfterGetBranchById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Branch! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Branch! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadBranchDataExtra= function(BranchId)
    {
        if(BranchId!=null)
            $scope.BranchId=BranchId;
            
            $http.get($scope.getBranchDataExtraUrl, {
                params: { "id": $scope.BranchId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                        $scope.onAfterLoadBranchDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Branch! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Branch! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveBranch= function(){
    	if(!$scope.validateBranch())
        {
          return;
        }
        $http.post($scope.saveBranchUrl + "/", $scope.Branch).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.Branch = result.Data;
                    updateUrlQuery('id', $scope.Branch.Id);
                   }
                   alertSuccess("Successfully saved Branch data!");
                   $scope.onAfterSaveBranch(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Branch! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Branch! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteBranchById= function(){
    	
    };
   $scope.validateBranch= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (BranchId,getBranchByIdUrl,getBranchDataExtraUrl, saveBranchUrl,deleteBranchByIdUrl) {
        $scope.BranchId = BranchId;
        $scope.getBranchByIdUrl = getBranchByIdUrl;
        $scope.saveBranchUrl = saveBranchUrl;
        $scope.getBranchDataExtraUrl = getBranchDataExtraUrl;
        $scope.deleteBranchByIdUrl = deleteBranchByIdUrl;

        $scope.loadPage(BranchId);
    };
    
  $scope.onAfterSaveBranch= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetBranchById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadBranchDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
         if(result.DataExtra.CompanyAccountList!=null)
         $scope.CompanyAccountList =  result.DataExtra.CompanyAccountList; 
  /*
  //Child Table Bindings, need to fix
             $scope.BranchIdList =  result.DataExtra.BranchIdList; 

             $scope.BranchIdList =  result.DataExtra.BranchIdList; 

             $scope.BranchIdList =  result.DataExtra.BranchIdList; 
     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

