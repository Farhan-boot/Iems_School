
//File: HR_LeaveAllocationTemplateDetails Anjular  Controller
emsApp.controller('LeaveAllocationTemplateDetailsAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.LeaveAllocationTemplateDetails = [];
    $scope.LeaveAllocationTemplateDetailsId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (LeaveAllocationTemplateDetailsId)
    {
       if(LeaveAllocationTemplateDetailsId!=null)
        $scope.LeaveAllocationTemplateDetailsId=LeaveAllocationTemplateDetailsId;
       
       $scope.loadLeaveAllocationTemplateDetailsDataExtra($scope.LeaveAllocationTemplateDetailsId);
       $scope.getLeaveAllocationTemplateDetailsById($scope.LeaveAllocationTemplateDetailsId);
    };
   $scope.getNewLeaveAllocationTemplateDetails= function()
    {
    	  $scope.getLeaveAllocationTemplateDetailsById(0);
    };
   $scope.getLeaveAllocationTemplateDetailsById= function(LeaveAllocationTemplateDetailsId)
    {
        if(LeaveAllocationTemplateDetailsId!=null && LeaveAllocationTemplateDetailsId!=='')
        $scope.LeaveAllocationTemplateDetailsId=LeaveAllocationTemplateDetailsId;
        else return;
        
        $http.get($scope.getLeaveAllocationTemplateDetailsByIdUrl, {
            params: { "id": $scope.LeaveAllocationTemplateDetailsId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.LeaveAllocationTemplateDetails = result.Data;
                updateUrlQuery('id' , $scope.LeaveAllocationTemplateDetails.Id);
                $scope.onAfterGetLeaveAllocationTemplateDetailsById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Leave Allocation Template Details! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Leave Allocation Template Details! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadLeaveAllocationTemplateDetailsDataExtra= function(LeaveAllocationTemplateDetailsId)
    {
        if(LeaveAllocationTemplateDetailsId!=null)
            $scope.LeaveAllocationTemplateDetailsId=LeaveAllocationTemplateDetailsId;
            
            $http.get($scope.getLeaveAllocationTemplateDetailsDataExtraUrl, {
                params: { "id": $scope.LeaveAllocationTemplateDetailsId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.LeaveTypeEnumList!=null)
                         $scope.LeaveTypeEnumList = result.DataExtra.LeaveTypeEnumList;
                        $scope.onAfterLoadLeaveAllocationTemplateDetailsDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Leave Allocation Template Details! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Leave Allocation Template Details! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveLeaveAllocationTemplateDetails= function(){
    	if(!$scope.validateLeaveAllocationTemplateDetails())
        {
          return;
        }
        $http.post($scope.saveLeaveAllocationTemplateDetailsUrl + "/", $scope.LeaveAllocationTemplateDetails).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.LeaveAllocationTemplateDetails = result.Data;
                    updateUrlQuery('id', $scope.LeaveAllocationTemplateDetails.Id);
                   }
                   alertSuccess("Successfully saved Leave Allocation Template Details data!");
                   $scope.onAfterSaveLeaveAllocationTemplateDetails(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Leave Allocation Template Details! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Leave Allocation Template Details! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteLeaveAllocationTemplateDetailsById= function(){
    	
    };
   $scope.validateLeaveAllocationTemplateDetails= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (LeaveAllocationTemplateDetailsId,getLeaveAllocationTemplateDetailsByIdUrl,getLeaveAllocationTemplateDetailsDataExtraUrl, saveLeaveAllocationTemplateDetailsUrl,deleteLeaveAllocationTemplateDetailsByIdUrl) {
        $scope.LeaveAllocationTemplateDetailsId = LeaveAllocationTemplateDetailsId;
        $scope.getLeaveAllocationTemplateDetailsByIdUrl = getLeaveAllocationTemplateDetailsByIdUrl;
        $scope.saveLeaveAllocationTemplateDetailsUrl = saveLeaveAllocationTemplateDetailsUrl;
        $scope.getLeaveAllocationTemplateDetailsDataExtraUrl = getLeaveAllocationTemplateDetailsDataExtraUrl;
        $scope.deleteLeaveAllocationTemplateDetailsByIdUrl = deleteLeaveAllocationTemplateDetailsByIdUrl;

        $scope.loadPage(LeaveAllocationTemplateDetailsId);
    };
    
  $scope.onAfterSaveLeaveAllocationTemplateDetails= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetLeaveAllocationTemplateDetailsById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadLeaveAllocationTemplateDetailsDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
         if(result.DataExtra.SalaryTemplateList!=null)
         $scope.SalaryTemplateList =  result.DataExtra.SalaryTemplateList; 
  /*
  //Child Table Bindings, need to fix     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

