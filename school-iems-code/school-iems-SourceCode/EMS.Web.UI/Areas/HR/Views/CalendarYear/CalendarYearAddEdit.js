
//File: HR_CalendarYear Anjular  Controller
emsApp.controller('CalendarYearAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.CalendarYear = [];
    $scope.CalendarYearId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (CalendarYearId)
    {
       if(CalendarYearId!=null)
        $scope.CalendarYearId=CalendarYearId;
       
       $scope.loadCalendarYearDataExtra($scope.CalendarYearId);
       $scope.getCalendarYearById($scope.CalendarYearId);
    };
   $scope.getNewCalendarYear= function()
    {
    	  $scope.getCalendarYearById(0);
    };
   $scope.getCalendarYearById= function(CalendarYearId)
    {
        if(CalendarYearId!=null && CalendarYearId!=='')
        $scope.CalendarYearId=CalendarYearId;
        else return;
        
        $http.get($scope.getCalendarYearByIdUrl, {
            params: { "id": $scope.CalendarYearId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.CalendarYear = result.Data;
                updateUrlQuery('id' , $scope.CalendarYear.Id);
                $scope.onAfterGetCalendarYearById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Calendar Year! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Calendar Year! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadCalendarYearDataExtra= function(CalendarYearId)
    {
        if(CalendarYearId!=null)
            $scope.CalendarYearId=CalendarYearId;
            
            $http.get($scope.getCalendarYearDataExtraUrl, {
                params: { "id": $scope.CalendarYearId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                        $scope.onAfterLoadCalendarYearDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Calendar Year! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Calendar Year! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveCalendarYear= function(){
    	if(!$scope.validateCalendarYear())
        {
          return;
        }
        $http.post($scope.saveCalendarYearUrl + "/", $scope.CalendarYear).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.CalendarYear = result.Data;
                    updateUrlQuery('id', $scope.CalendarYear.Id);
                   }
                   alertSuccess("Successfully saved Calendar Year data!");
                   $scope.onAfterSaveCalendarYear(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Calendar Year! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Calendar Year! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteCalendarYearById= function(){
    	
    };
   $scope.validateCalendarYear= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (CalendarYearId,getCalendarYearByIdUrl,getCalendarYearDataExtraUrl, saveCalendarYearUrl,deleteCalendarYearByIdUrl) {
        $scope.CalendarYearId = CalendarYearId;
        $scope.getCalendarYearByIdUrl = getCalendarYearByIdUrl;
        $scope.saveCalendarYearUrl = saveCalendarYearUrl;
        $scope.getCalendarYearDataExtraUrl = getCalendarYearDataExtraUrl;
        $scope.deleteCalendarYearByIdUrl = deleteCalendarYearByIdUrl;

        $scope.loadPage(CalendarYearId);
    };
    
  $scope.onAfterSaveCalendarYear= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetCalendarYearById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadCalendarYearDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
  /*
  //Child Table Bindings, need to fix
             $scope.CalendarYearIdList =  result.DataExtra.CalendarYearIdList; 

             $scope.CalendarYearIdList =  result.DataExtra.CalendarYearIdList; 
     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

