
//File: Inv_Requisition Anjular  Controller
emsApp.controller('RequisitionAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.Requisition = [];
    $scope.RequisitionId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (RequisitionId)
    {
       if(RequisitionId!=null)
        $scope.RequisitionId=RequisitionId;
       
       $scope.loadRequisitionDataExtra($scope.RequisitionId);
       $scope.getRequisitionById($scope.RequisitionId);
    };
   $scope.getNewRequisition= function()
    {
    	  $scope.getRequisitionById(0);
    };
   $scope.getRequisitionById= function(RequisitionId)
    {
        if(RequisitionId!=null && RequisitionId!=='')
        $scope.RequisitionId=RequisitionId;
        else return;
        
        $http.get($scope.getRequisitionByIdUrl, {
            params: { "id": $scope.RequisitionId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.Requisition = result.Data;
                updateUrlQuery('id' , $scope.Requisition.Id);
                $scope.onAfterGetRequisitionById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Requisition! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Requisition! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadRequisitionDataExtra= function(RequisitionId)
    {
        if(RequisitionId!=null)
            $scope.RequisitionId=RequisitionId;
            
            $http.get($scope.getRequisitionDataExtraUrl, {
                params: { "id": $scope.RequisitionId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                        $scope.onAfterLoadRequisitionDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Requisition! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Requisition! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveRequisition= function(){
    	if(!$scope.validateRequisition())
        {
          return;
        }
        $http.post($scope.saveRequisitionUrl + "/", $scope.Requisition).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.Requisition = result.Data;
                    updateUrlQuery('id', $scope.Requisition.Id);
                   }
                   alertSuccess("Successfully saved Requisition data!");
                   $scope.onAfterSaveRequisition(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Requisition! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Requisition! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteRequisitionById= function(){
    	
    };
   $scope.validateRequisition= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (RequisitionId,getRequisitionByIdUrl,getRequisitionDataExtraUrl, saveRequisitionUrl,deleteRequisitionByIdUrl) {
        $scope.RequisitionId = RequisitionId;
        $scope.getRequisitionByIdUrl = getRequisitionByIdUrl;
        $scope.saveRequisitionUrl = saveRequisitionUrl;
        $scope.getRequisitionDataExtraUrl = getRequisitionDataExtraUrl;
        $scope.deleteRequisitionByIdUrl = deleteRequisitionByIdUrl;

        $scope.loadPage(RequisitionId);
    };
    
  $scope.onAfterSaveRequisition= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetRequisitionById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadRequisitionDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
  /*
  //Child Table Bindings, need to fix
             $scope.RequisitionIdList =  result.DataExtra.RequisitionIdList; 

             $scope.RequisitionIdList =  result.DataExtra.RequisitionIdList; 
     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

