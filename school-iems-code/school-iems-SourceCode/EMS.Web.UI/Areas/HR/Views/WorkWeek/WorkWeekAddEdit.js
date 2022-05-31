
//File: HR_WorkWeek Anjular  Controller
emsApp.controller('WorkWeekAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.WorkWeek = [];
    $scope.WorkWeekId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (WorkWeekId)
    {
       if(WorkWeekId!=null)
        $scope.WorkWeekId=WorkWeekId;
       
       $scope.loadWorkWeekDataExtra($scope.WorkWeekId);
       $scope.getWorkWeekById($scope.WorkWeekId);
    };
   $scope.getNewWorkWeek= function()
    {
    	  $scope.getWorkWeekById(0);
    };
   $scope.getWorkWeekById= function(WorkWeekId)
    {
        if(WorkWeekId!=null && WorkWeekId!=='')
        $scope.WorkWeekId=WorkWeekId;
        else return;
        
        $http.get($scope.getWorkWeekByIdUrl, {
            params: { "id": $scope.WorkWeekId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.WorkWeek = result.Data;
                updateUrlQuery('id' , $scope.WorkWeek.Id);
                $scope.onAfterGetWorkWeekById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Work Week! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Work Week! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadWorkWeekDataExtra= function(WorkWeekId)
    {
        if(WorkWeekId!=null)
            $scope.WorkWeekId=WorkWeekId;
            
            $http.get($scope.getWorkWeekDataExtraUrl, {
                params: { "id": $scope.WorkWeekId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.DayEnumList!=null)
                         $scope.DayEnumList = result.DataExtra.DayEnumList;
                      if(result.DataExtra.WorkingDayTypeEnumList!=null)
                         $scope.WorkingDayTypeEnumList = result.DataExtra.WorkingDayTypeEnumList;
                        $scope.onAfterLoadWorkWeekDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Work Week! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Work Week! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveWorkWeek= function(){
    	if(!$scope.validateWorkWeek())
        {
          return;
        }
        $http.post($scope.saveWorkWeekUrl + "/", $scope.WorkWeek).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.WorkWeek = result.Data;
                    updateUrlQuery('id', $scope.WorkWeek.Id);
                   }
                   alertSuccess("Successfully saved Work Week data!");
                   $scope.onAfterSaveWorkWeek(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Work Week! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Work Week! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteWorkWeekById= function(){
    	
    };
   $scope.validateWorkWeek= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (WorkWeekId,getWorkWeekByIdUrl,getWorkWeekDataExtraUrl, saveWorkWeekUrl,deleteWorkWeekByIdUrl) {
        $scope.WorkWeekId = WorkWeekId;
        $scope.getWorkWeekByIdUrl = getWorkWeekByIdUrl;
        $scope.saveWorkWeekUrl = saveWorkWeekUrl;
        $scope.getWorkWeekDataExtraUrl = getWorkWeekDataExtraUrl;
        $scope.deleteWorkWeekByIdUrl = deleteWorkWeekByIdUrl;

        $scope.loadPage(WorkWeekId);
    };
    
  $scope.onAfterSaveWorkWeek= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetWorkWeekById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadWorkWeekDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
         if(result.DataExtra.CalendarYearList!=null)
         $scope.CalendarYearList =  result.DataExtra.CalendarYearList; 
  /*
  //Child Table Bindings, need to fix     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

