
//File: HR_Attendance Anjular  Controller
emsApp.controller('AttendanceAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.Attendance = [];
    $scope.AttendanceId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (AttendanceId)
    {
       if(AttendanceId!=null)
        $scope.AttendanceId=AttendanceId;
       
       $scope.loadAttendanceDataExtra($scope.AttendanceId);
       $scope.getAttendanceById($scope.AttendanceId);
    };
   $scope.getNewAttendance= function()
    {
    	  $scope.getAttendanceById(0);
    };
   $scope.getAttendanceById= function(AttendanceId)
    {
        if(AttendanceId!=null && AttendanceId!=='')
        $scope.AttendanceId=AttendanceId;
        else return;
        
        $http.get($scope.getAttendanceByIdUrl, {
            params: { "id": $scope.AttendanceId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.Attendance = result.Data;
                updateUrlQuery('id' , $scope.Attendance.Id);
                $scope.onAfterGetAttendanceById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Attendance! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Attendance! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadAttendanceDataExtra= function(AttendanceId)
    {
        if(AttendanceId!=null)
            $scope.AttendanceId=AttendanceId;
            
            $http.get($scope.getAttendanceDataExtraUrl, {
                params: { "id": $scope.AttendanceId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.PunchTypeEnumList!=null)
                         $scope.PunchTypeEnumList = result.DataExtra.PunchTypeEnumList;
                      if(result.DataExtra.PunchModeEnumList!=null)
                         $scope.PunchModeEnumList = result.DataExtra.PunchModeEnumList;
                        $scope.onAfterLoadAttendanceDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Attendance! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Attendance! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveAttendance= function(){
    	if(!$scope.validateAttendance())
        {
          return;
        }
        $http.post($scope.saveAttendanceUrl + "/", $scope.Attendance).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.Attendance = result.Data;
                    updateUrlQuery('id', $scope.Attendance.Id);
                   }
                   alertSuccess("Successfully saved Attendance data!");
                   $scope.onAfterSaveAttendance(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Attendance! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Attendance! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteAttendanceById= function(){
    	
    };
   $scope.validateAttendance= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (AttendanceId,getAttendanceByIdUrl,getAttendanceDataExtraUrl, saveAttendanceUrl,deleteAttendanceByIdUrl) {
        $scope.AttendanceId = AttendanceId;
        $scope.getAttendanceByIdUrl = getAttendanceByIdUrl;
        $scope.saveAttendanceUrl = saveAttendanceUrl;
        $scope.getAttendanceDataExtraUrl = getAttendanceDataExtraUrl;
        $scope.deleteAttendanceByIdUrl = deleteAttendanceByIdUrl;

        $scope.loadPage(AttendanceId);
    };
    
  $scope.onAfterSaveAttendance= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetAttendanceById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadAttendanceDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
         if(result.DataExtra.EmployeeList!=null)
         $scope.EmployeeList =  result.DataExtra.EmployeeList; 
  /*
  //Child Table Bindings, need to fix     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

