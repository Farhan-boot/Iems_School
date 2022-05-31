
//File: HR_HolidaySettings Anjular  Controller
emsApp.controller('HolidaySettingsAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.HolidaySettings = [];
    $scope.HolidaySettingsId=0;
    $scope.ErrorMsg = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (HolidaySettingsId)
    {
       if(HolidaySettingsId!=null)
        $scope.HolidaySettingsId=HolidaySettingsId;
       
       $scope.loadHolidaySettingsDataExtra($scope.HolidaySettingsId);
       $scope.getHolidaySettingsById($scope.HolidaySettingsId);
    };
   $scope.getNewHolidaySettings= function()
    {
    	  $scope.getHolidaySettingsById(0);
    };
   $scope.getHolidaySettingsById= function(HolidaySettingsId)
    {
        if(HolidaySettingsId!=null && HolidaySettingsId!=='')
        $scope.HolidaySettingsId=HolidaySettingsId;
        else return;
        
        $http.get($scope.getHolidaySettingsByIdUrl, {
            params: { "id": $scope.HolidaySettingsId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.HolidaySettings = result.Data;
                updateUrlQuery('id' , $scope.HolidaySettings.Id);
                $scope.onAfterGetHolidaySettingsById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Holiday Settings! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Holiday Settings! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadHolidaySettingsDataExtra= function(HolidaySettingsId)
    {
        if(HolidaySettingsId!=null)
            $scope.HolidaySettingsId=HolidaySettingsId;
            
            $http.get($scope.getHolidaySettingsDataExtraUrl, {
                params: { "id": $scope.HolidaySettingsId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.WorkingDayTypeEnumList!=null)
                         $scope.WorkingDayTypeEnumList = result.DataExtra.WorkingDayTypeEnumList;
                        $scope.onAfterLoadHolidaySettingsDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Holiday Settings! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Holiday Settings! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveHolidaySettings= function(){
    	if(!$scope.validateHolidaySettings())
        {
          return;
        }
        $http.post($scope.saveHolidaySettingsUrl + "/", $scope.HolidaySettings).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {$scope.HolidaySettings = result.Data;
                    updateUrlQuery('id', $scope.HolidaySettings.Id);
                   }
                   alertSuccess("Successfully saved Holiday Settings data!");
                   $scope.onAfterSaveHolidaySettings(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Holiday Settings! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Holiday Settings! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteHolidaySettingsById= function(){
    	
    };
   $scope.validateHolidaySettings= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (HolidaySettingsId,getHolidaySettingsByIdUrl,getHolidaySettingsDataExtraUrl, saveHolidaySettingsUrl,deleteHolidaySettingsByIdUrl) {
        $scope.HolidaySettingsId = HolidaySettingsId;
        $scope.getHolidaySettingsByIdUrl = getHolidaySettingsByIdUrl;
        $scope.saveHolidaySettingsUrl = saveHolidaySettingsUrl;
        $scope.getHolidaySettingsDataExtraUrl = getHolidaySettingsDataExtraUrl;
        $scope.deleteHolidaySettingsByIdUrl = deleteHolidaySettingsByIdUrl;

        $scope.loadPage(HolidaySettingsId);
    };
    
  $scope.onAfterSaveHolidaySettings= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetHolidaySettingsById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadHolidaySettingsDataExtra= function(result){
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

