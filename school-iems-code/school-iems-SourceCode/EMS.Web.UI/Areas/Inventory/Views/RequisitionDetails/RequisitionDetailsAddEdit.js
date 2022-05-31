
//File: Inv_RequisitionDetails Anjular  Controller
emsApp.controller('RequisitionDetailsAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.RequisitionDetails = [];
    $scope.RequisitionDetailsId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (RequisitionDetailsId)
    {
       if(RequisitionDetailsId!=null)
        $scope.RequisitionDetailsId=RequisitionDetailsId;
       
       $scope.loadRequisitionDetailsDataExtra($scope.RequisitionDetailsId);
       $scope.getRequisitionDetailsById($scope.RequisitionDetailsId);
    };
   $scope.getNewRequisitionDetails= function()
    {
    	  $scope.getRequisitionDetailsById(0);
    };
   $scope.getRequisitionDetailsById= function(RequisitionDetailsId)
    {
        if(RequisitionDetailsId!=null && RequisitionDetailsId!=='')
        $scope.RequisitionDetailsId=RequisitionDetailsId;
        else return;
        
        $http.get($scope.getRequisitionDetailsByIdUrl, {
            params: { "id": $scope.RequisitionDetailsId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.RequisitionDetails = result.Data;
                updateUrlQuery('id' , $scope.RequisitionDetails.Id);
                $scope.onAfterGetRequisitionDetailsById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Requisition Details! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Requisition Details! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadRequisitionDetailsDataExtra= function(RequisitionDetailsId)
    {
        if(RequisitionDetailsId!=null)
            $scope.RequisitionDetailsId=RequisitionDetailsId;
            
            $http.get($scope.getRequisitionDetailsDataExtraUrl, {
                params: { "id": $scope.RequisitionDetailsId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                        $scope.onAfterLoadRequisitionDetailsDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Requisition Details! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Requisition Details! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveRequisitionDetails= function(){
    	if(!$scope.validateRequisitionDetails())
        {
          return;
        }
        $http.post($scope.saveRequisitionDetailsUrl + "/", $scope.RequisitionDetails).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.RequisitionDetails = result.Data;
                    updateUrlQuery('id', $scope.RequisitionDetails.Id);
                   }
                   alertSuccess("Successfully saved Requisition Details data!");
                   $scope.onAfterSaveRequisitionDetails(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Requisition Details! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Requisition Details! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteRequisitionDetailsById= function(){
    	
    };
   $scope.validateRequisitionDetails= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (RequisitionDetailsId,getRequisitionDetailsByIdUrl,getRequisitionDetailsDataExtraUrl, saveRequisitionDetailsUrl,deleteRequisitionDetailsByIdUrl) {
        $scope.RequisitionDetailsId = RequisitionDetailsId;
        $scope.getRequisitionDetailsByIdUrl = getRequisitionDetailsByIdUrl;
        $scope.saveRequisitionDetailsUrl = saveRequisitionDetailsUrl;
        $scope.getRequisitionDetailsDataExtraUrl = getRequisitionDetailsDataExtraUrl;
        $scope.deleteRequisitionDetailsByIdUrl = deleteRequisitionDetailsByIdUrl;

        $scope.loadPage(RequisitionDetailsId);
    };
    
  $scope.onAfterSaveRequisitionDetails= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetRequisitionDetailsById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadRequisitionDetailsDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
         if(result.DataExtra.RequisitionList!=null)
         $scope.RequisitionList =  result.DataExtra.RequisitionList; 
  /*
  //Child Table Bindings, need to fix     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

