
//File: Aca_CreditTransferInstitute Anjular  Controller
emsApp.controller('CreditTransferInstituteAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.CreditTransferInstitute = [];
    $scope.CreditTransferInstituteId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (CreditTransferInstituteId)
    {
       if(CreditTransferInstituteId!=null)
        $scope.CreditTransferInstituteId=CreditTransferInstituteId;
       
       $scope.loadCreditTransferInstituteDataExtra($scope.CreditTransferInstituteId);
       $scope.getCreditTransferInstituteById($scope.CreditTransferInstituteId);
    };
   $scope.getNewCreditTransferInstitute= function()
    {
    	  $scope.getCreditTransferInstituteById(0);
    };
   $scope.getCreditTransferInstituteById= function(CreditTransferInstituteId)
    {
        if(CreditTransferInstituteId!=null && CreditTransferInstituteId!=='')
        $scope.CreditTransferInstituteId=CreditTransferInstituteId;
        else return;
        
        $http.get($scope.getCreditTransferInstituteByIdUrl, {
            params: { "id": $scope.CreditTransferInstituteId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.CreditTransferInstitute = result.Data;
                updateUrlQuery('id' , $scope.CreditTransferInstitute.Id);
                $scope.onAfterGetCreditTransferInstituteById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Credit Transfer Institute! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Credit Transfer Institute! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadCreditTransferInstituteDataExtra= function(CreditTransferInstituteId)
    {
        if(CreditTransferInstituteId!=null)
            $scope.CreditTransferInstituteId=CreditTransferInstituteId;
            
            $http.get($scope.getCreditTransferInstituteDataExtraUrl, {
                params: { "id": $scope.CreditTransferInstituteId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                        $scope.onAfterLoadCreditTransferInstituteDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Credit Transfer Institute! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Credit Transfer Institute! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveCreditTransferInstitute= function(){
    	if(!$scope.validateCreditTransferInstitute())
        {
          return;
        }
        $http.post($scope.saveCreditTransferInstituteUrl + "/", $scope.CreditTransferInstitute).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.CreditTransferInstitute = result.Data;
                    updateUrlQuery('id', $scope.CreditTransferInstitute.Id);
                   }
                   alertSuccess("Successfully saved Credit Transfer Institute data!");
                   $scope.onAfterSaveCreditTransferInstitute(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Credit Transfer Institute! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Credit Transfer Institute! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteCreditTransferInstituteById= function(){
    	
    };
   $scope.validateCreditTransferInstitute= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (CreditTransferInstituteId,getCreditTransferInstituteByIdUrl,getCreditTransferInstituteDataExtraUrl, saveCreditTransferInstituteUrl,deleteCreditTransferInstituteByIdUrl) {
        $scope.CreditTransferInstituteId = CreditTransferInstituteId;
        $scope.getCreditTransferInstituteByIdUrl = getCreditTransferInstituteByIdUrl;
        $scope.saveCreditTransferInstituteUrl = saveCreditTransferInstituteUrl;
        $scope.getCreditTransferInstituteDataExtraUrl = getCreditTransferInstituteDataExtraUrl;
        $scope.deleteCreditTransferInstituteByIdUrl = deleteCreditTransferInstituteByIdUrl;

        $scope.loadPage(CreditTransferInstituteId);
    };
    
  $scope.onAfterSaveCreditTransferInstitute= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetCreditTransferInstituteById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadCreditTransferInstituteDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
  /*
  //Child Table Bindings, need to fix     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

