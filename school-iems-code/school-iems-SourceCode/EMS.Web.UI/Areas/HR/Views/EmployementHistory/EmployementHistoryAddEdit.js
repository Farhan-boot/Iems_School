
//File: HR_EmployementHistory Anjular  Controller
emsApp.controller('EmployementHistoryAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.EmployementHistory = [];
    $scope.EmployementHistoryId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (EmployementHistoryId)
    {
       if(EmployementHistoryId!=null)
        $scope.EmployementHistoryId=EmployementHistoryId;
       
       $scope.loadEmployementHistoryDataExtra($scope.EmployementHistoryId);
       $scope.getEmployementHistoryById($scope.EmployementHistoryId);
    };
   $scope.getNewEmployementHistory= function()
    {
    	  $scope.getEmployementHistoryById(0);
    };
   $scope.getEmployementHistoryById= function(EmployementHistoryId)
    {
        if(EmployementHistoryId!=null && EmployementHistoryId!=='')
        $scope.EmployementHistoryId=EmployementHistoryId;
        else return;
        
        $http.get($scope.getEmployementHistoryByIdUrl, {
            params: { "id": $scope.EmployementHistoryId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.EmployementHistory = result.Data;
                updateUrlQuery('id' , $scope.EmployementHistory.Id);
                $scope.onAfterGetEmployementHistoryById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Employement History! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Employement History! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadEmployementHistoryDataExtra= function(EmployementHistoryId)
    {
        if(EmployementHistoryId!=null)
            $scope.EmployementHistoryId=EmployementHistoryId;
            
            $http.get($scope.getEmployementHistoryDataExtraUrl, {
                params: { "id": $scope.EmployementHistoryId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.EmployementTypeEnumList!=null)
                         $scope.EmployementTypeEnumList = result.DataExtra.EmployementTypeEnumList;
                      if(result.DataExtra.JobTypeEnumList!=null)
                         $scope.JobTypeEnumList = result.DataExtra.JobTypeEnumList;
                      if(result.DataExtra.HistoryTypeEnumList!=null)
                         $scope.HistoryTypeEnumList = result.DataExtra.HistoryTypeEnumList;
                        $scope.onAfterLoadEmployementHistoryDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Employement History! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Employement History! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveEmployementHistory= function(){
    	if(!$scope.validateEmployementHistory())
        {
          return;
        }
        $http.post($scope.saveEmployementHistoryUrl + "/", $scope.EmployementHistory).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.EmployementHistory = result.Data;
                    updateUrlQuery('id', $scope.EmployementHistory.Id);
                   }
                   alertSuccess("Successfully saved Employement History data!");
                   $scope.onAfterSaveEmployementHistory(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Employement History! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Employement History! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteEmployementHistoryById= function(){
    	
    };
   $scope.validateEmployementHistory= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (EmployementHistoryId,getEmployementHistoryByIdUrl,getEmployementHistoryDataExtraUrl, saveEmployementHistoryUrl,deleteEmployementHistoryByIdUrl) {
        $scope.EmployementHistoryId = EmployementHistoryId;
        $scope.getEmployementHistoryByIdUrl = getEmployementHistoryByIdUrl;
        $scope.saveEmployementHistoryUrl = saveEmployementHistoryUrl;
        $scope.getEmployementHistoryDataExtraUrl = getEmployementHistoryDataExtraUrl;
        $scope.deleteEmployementHistoryByIdUrl = deleteEmployementHistoryByIdUrl;

        $scope.loadPage(EmployementHistoryId);
    };
    
  $scope.onAfterSaveEmployementHistory= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetEmployementHistoryById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadEmployementHistoryDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
         if(result.DataExtra.DepartmentList!=null)
         $scope.DepartmentList =  result.DataExtra.DepartmentList; 
         if(result.DataExtra.EmployeeList!=null)
         $scope.EmployeeList =  result.DataExtra.EmployeeList; 
         if(result.DataExtra.RankList!=null)
         $scope.RankList =  result.DataExtra.RankList; 
  /*
  //Child Table Bindings, need to fix     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

