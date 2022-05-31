
//File: HR_AppointmentHistory Anjular  Controller
emsApp.controller('AppointmentHistoryAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.AppointmentHistory = [];
    $scope.AppointmentHistoryId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (AppointmentHistoryId)
    {
       if(AppointmentHistoryId!=null)
        $scope.AppointmentHistoryId=AppointmentHistoryId;
       
       $scope.loadAppointmentHistoryDataExtra($scope.AppointmentHistoryId);
       $scope.getAppointmentHistoryById($scope.AppointmentHistoryId);
    };
   $scope.getNewAppointmentHistory= function()
    {
    	  $scope.getAppointmentHistoryById(0);
    };
   $scope.getAppointmentHistoryById= function(AppointmentHistoryId)
    {
        if(AppointmentHistoryId!=null && AppointmentHistoryId!=='')
        $scope.AppointmentHistoryId=AppointmentHistoryId;
        else return;
        
        $http.get($scope.getAppointmentHistoryByIdUrl, {
            params: { "id": $scope.AppointmentHistoryId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.AppointmentHistory = result.Data;
                updateUrlQuery('id' , $scope.AppointmentHistory.Id);
                $scope.onAfterGetAppointmentHistoryById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Appointment History! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Appointment History! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadAppointmentHistoryDataExtra= function(AppointmentHistoryId)
    {
        if(AppointmentHistoryId!=null)
            $scope.AppointmentHistoryId=AppointmentHistoryId;
            
            $http.get($scope.getAppointmentHistoryDataExtraUrl, {
                params: { "id": $scope.AppointmentHistoryId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.HistoryTypeEnumList!=null)
                         $scope.HistoryTypeEnumList = result.DataExtra.HistoryTypeEnumList;
                        $scope.onAfterLoadAppointmentHistoryDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Appointment History! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Appointment History! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveAppointmentHistory= function(){
    	if(!$scope.validateAppointmentHistory())
        {
          return;
        }
        $http.post($scope.saveAppointmentHistoryUrl + "/", $scope.AppointmentHistory).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.AppointmentHistory = result.Data;
                    updateUrlQuery('id', $scope.AppointmentHistory.Id);
                   }
                   alertSuccess("Successfully saved Appointment History data!");
                   $scope.onAfterSaveAppointmentHistory(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Appointment History! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Appointment History! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteAppointmentHistoryById= function(){
    	
    };
   $scope.validateAppointmentHistory= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (AppointmentHistoryId,getAppointmentHistoryByIdUrl,getAppointmentHistoryDataExtraUrl, saveAppointmentHistoryUrl,deleteAppointmentHistoryByIdUrl) {
        $scope.AppointmentHistoryId = AppointmentHistoryId;
        $scope.getAppointmentHistoryByIdUrl = getAppointmentHistoryByIdUrl;
        $scope.saveAppointmentHistoryUrl = saveAppointmentHistoryUrl;
        $scope.getAppointmentHistoryDataExtraUrl = getAppointmentHistoryDataExtraUrl;
        $scope.deleteAppointmentHistoryByIdUrl = deleteAppointmentHistoryByIdUrl;

        $scope.loadPage(AppointmentHistoryId);
    };
    
  $scope.onAfterSaveAppointmentHistory= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetAppointmentHistoryById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadAppointmentHistoryDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
         if(result.DataExtra.DepartmentList!=null)
         $scope.DepartmentList =  result.DataExtra.DepartmentList; 
         if(result.DataExtra.PositionList!=null)
         $scope.PositionList =  result.DataExtra.PositionList; 
         if(result.DataExtra.EmployeeList!=null)
         $scope.EmployeeList =  result.DataExtra.EmployeeList; 
  /*
  //Child Table Bindings, need to fix     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

