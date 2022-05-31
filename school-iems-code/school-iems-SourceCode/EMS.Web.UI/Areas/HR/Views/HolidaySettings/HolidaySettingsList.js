
//File:HR_HolidaySettings List Anjular  Controller
emsApp.controller('HolidaySettingsListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.HolidaySettingsList = [];
    $scope.SelectedHolidaySettings = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
     $scope.SelectedCalendarYearId=0;      
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
      $scope.getHolidaySettingsListExtraData();
      $scope.getPagedHolidaySettingsList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedHolidaySettingsList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedHolidaySettingsList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedHolidaySettingsList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedHolidaySettingsList();
    };
    $scope.searchHolidaySettingsList = function () {
        $scope.getPagedHolidaySettingsList();
    };
    $scope.getPagedHolidaySettingsList = function () {
        $scope.getHolidaySettingsList();
    };
    /*For Search on data end*/
    $scope.getHolidaySettingsList = function () {
        $http.get($scope.getPagedHolidaySettingsUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
           ,"calendarYearId": $scope.SelectedCalendarYearId      
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.HolidaySettingsList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Holiday Settings list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Holiday Settings list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getHolidaySettingsListExtraData= function()
    {
            $http.get($scope.getHolidaySettingsListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.WorkingDayTypeEnumList!=null)
                         $scope.WorkingDayTypeEnumList = result.DataExtra.WorkingDayTypeEnumList;
                  //DropDown Option Tables
                     if(result.DataExtra.CalendarYearList!=null)
                       $scope.CalendarYearList =  result.DataExtra.CalendarYearList; 

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

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllHolidaySettingsList = function () {
        $scope.IsLoading++;
        $http.get($scope.getHolidaySettingsListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HolidaySettingsList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Holiday Settings list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Holiday Settings list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteHolidaySettingsById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteHolidaySettingsByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.HolidaySettingsList.indexOf(obj);
                        $scope.HolidaySettingsList.splice(i, 1);
                        alertSuccess("Data successfully deleted!");
                    } else {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Sorry unable to delete! " + result.Errors.toString();
                        alertError($scope.ErrorMsg);
                    }
                }).error(function (result, status) {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Sorry unable to delete! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
                    alertError($scope.ErrorMsg);
                });
            }
        });
    };
    /*$scope.deleteHolidaySettingsByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteHolidaySettingsByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.HolidaySettingsList.indexOf(obj);
                        $scope.HolidaySettingsList.splice(i, 1);
                       alertSuccess("Data successfully deleted!");
                    } else {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Sorry unable to delete! " + result.Errors.toString();
                        alertError($scope.ErrorMsg);
                    }
                }).error(function (result, status) {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Sorry unable to delete! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                    alertError($scope.ErrorMsg);
                });
            }
        });
    };*/

    $scope.selectAll = function ($event) {
    var checkbox = $event.target;
    for (var i = 0; i < $scope.HolidaySettingsList.length; i++) {
        var entity = $scope.HolidaySettingsList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedHolidaySettingsUrl
        ,deleteHolidaySettingsByIdUrl
        ,getHolidaySettingsListDataExtraUrl
        ,saveHolidaySettingsListUrl
        ,getHolidaySettingsByIdUrl
        ,getHolidaySettingsDataExtraUrl
        ,saveHolidaySettingsUrl
        ) {
        $scope.getPagedHolidaySettingsUrl = getPagedHolidaySettingsUrl;
        $scope.deleteHolidaySettingsByIdUrl = deleteHolidaySettingsByIdUrl;
        /*bind extra url if need*/
        $scope.getHolidaySettingsListDataExtraUrl = getHolidaySettingsListDataExtraUrl;
        $scope.saveHolidaySettingsListUrl = saveHolidaySettingsListUrl;
        $scope.getHolidaySettingsByIdUrl = getHolidaySettingsByIdUrl;
        $scope.getHolidaySettingsDataExtraUrl = getHolidaySettingsDataExtraUrl;
        $scope.saveHolidaySettingsUrl = saveHolidaySettingsUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



