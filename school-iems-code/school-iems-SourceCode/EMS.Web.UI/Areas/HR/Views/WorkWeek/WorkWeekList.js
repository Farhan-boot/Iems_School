
//File:HR_WorkWeek List Anjular  Controller
emsApp.controller('WorkWeekListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.WorkWeekList = [];
    $scope.SelectedWorkWeek = [];
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
      $scope.getWorkWeekListExtraData();
      $scope.getPagedWorkWeekList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedWorkWeekList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedWorkWeekList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedWorkWeekList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedWorkWeekList();
    };
    $scope.searchWorkWeekList = function () {
        $scope.getPagedWorkWeekList();
    };
    $scope.getPagedWorkWeekList = function () {
        $scope.getWorkWeekList();
    };
    /*For Search on data end*/
    $scope.getWorkWeekList = function () {
        $http.get($scope.getPagedWorkWeekUrl, {
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
                $scope.WorkWeekList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Work Week list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Work Week list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getWorkWeekListExtraData= function()
    {
            $http.get($scope.getWorkWeekListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.DayEnumList!=null)
                         $scope.DayEnumList = result.DataExtra.DayEnumList;
                      if(result.DataExtra.WorkingDayTypeEnumList!=null)
                         $scope.WorkingDayTypeEnumList = result.DataExtra.WorkingDayTypeEnumList;
                  //DropDown Option Tables
                     if(result.DataExtra.CalendarYearList!=null)
                       $scope.CalendarYearList =  result.DataExtra.CalendarYearList; 

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

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllWorkWeekList = function () {
        $scope.IsLoading++;
        $http.get($scope.getWorkWeekListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.WorkWeekList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Work Week list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Work Week list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteWorkWeekById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteWorkWeekByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.WorkWeekList.indexOf(obj);
                        $scope.WorkWeekList.splice(i, 1);
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
    /*$scope.deleteWorkWeekByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteWorkWeekByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.WorkWeekList.indexOf(obj);
                        $scope.WorkWeekList.splice(i, 1);
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
    for (var i = 0; i < $scope.WorkWeekList.length; i++) {
        var entity = $scope.WorkWeekList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedWorkWeekUrl
        ,deleteWorkWeekByIdUrl
        ,getWorkWeekListDataExtraUrl
        ,saveWorkWeekListUrl
        ,getWorkWeekByIdUrl
        ,getWorkWeekDataExtraUrl
        ,saveWorkWeekUrl
        ) {
        $scope.getPagedWorkWeekUrl = getPagedWorkWeekUrl;
        $scope.deleteWorkWeekByIdUrl = deleteWorkWeekByIdUrl;
        /*bind extra url if need*/
        $scope.getWorkWeekListDataExtraUrl = getWorkWeekListDataExtraUrl;
        $scope.saveWorkWeekListUrl = saveWorkWeekListUrl;
        $scope.getWorkWeekByIdUrl = getWorkWeekByIdUrl;
        $scope.getWorkWeekDataExtraUrl = getWorkWeekDataExtraUrl;
        $scope.saveWorkWeekUrl = saveWorkWeekUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



