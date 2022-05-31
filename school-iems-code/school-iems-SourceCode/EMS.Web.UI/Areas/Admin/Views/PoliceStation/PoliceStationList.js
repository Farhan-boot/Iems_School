
//File:Admin_PoliceStation List Anjular  Controller
emsApp.controller('PoliceStationListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.PoliceStationList = [];
    $scope.SelectedPoliceStation = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
     $scope.SelectedDistrictId=0;      
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
      $scope.getPoliceStationListExtraData();
      $scope.getPagedPoliceStationList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedPoliceStationList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedPoliceStationList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedPoliceStationList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedPoliceStationList();
    };
    $scope.searchPoliceStationList = function () {
        $scope.getPagedPoliceStationList();
    };
    $scope.getPagedPoliceStationList = function () {
        $scope.getPoliceStationList();
    };
    /*For Search on data end*/
    $scope.getPoliceStationList = function () {
        $http.get($scope.getPagedPoliceStationUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
           ,"districtId": $scope.SelectedDistrictId      
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.PoliceStationList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Police Station list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Police Station list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getPoliceStationListExtraData= function()
    {
            $http.get($scope.getPoliceStationListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.StatusEnumList!=null)
                         $scope.StatusEnumList = result.DataExtra.StatusEnumList;
                  //DropDown Option Tables
                     if(result.DataExtra.DistrictList!=null)
                       $scope.DistrictList =  result.DataExtra.DistrictList; 

                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Police Station! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Police Station! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllPoliceStationList = function () {
        $scope.IsLoading++;
        $http.get($scope.getPoliceStationListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.PoliceStationList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Police Station list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Police Station list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deletePoliceStationById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deletePoliceStationByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.PoliceStationList.indexOf(obj);
                        $scope.PoliceStationList.splice(i, 1);
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
    /*$scope.deletePoliceStationByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deletePoliceStationByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.PoliceStationList.indexOf(obj);
                        $scope.PoliceStationList.splice(i, 1);
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
    for (var i = 0; i < $scope.PoliceStationList.length; i++) {
        var entity = $scope.PoliceStationList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedPoliceStationUrl
        ,deletePoliceStationByIdUrl
        ,getPoliceStationListDataExtraUrl
        ,savePoliceStationListUrl
        ,getPoliceStationByIdUrl
        ,getPoliceStationDataExtraUrl
        ,savePoliceStationUrl
        ) {
        $scope.getPagedPoliceStationUrl = getPagedPoliceStationUrl;
        $scope.deletePoliceStationByIdUrl = deletePoliceStationByIdUrl;
        /*bind extra url if need*/
        $scope.getPoliceStationListDataExtraUrl = getPoliceStationListDataExtraUrl;
        $scope.savePoliceStationListUrl = savePoliceStationListUrl;
        $scope.getPoliceStationByIdUrl = getPoliceStationByIdUrl;
        $scope.getPoliceStationDataExtraUrl = getPoliceStationDataExtraUrl;
        $scope.savePoliceStationUrl = savePoliceStationUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



