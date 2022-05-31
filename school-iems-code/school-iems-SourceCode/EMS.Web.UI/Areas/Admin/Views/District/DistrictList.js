
//File:Admin_District List Anjular  Controller
emsApp.controller('DistrictListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.DistrictList = [];
    $scope.SelectedDistrict = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
     $scope.SelectedCountryId=0;      
     $scope.SelectedDivisionId=0;      
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
      $scope.getDistrictListExtraData();
      $scope.getPagedDistrictList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedDistrictList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedDistrictList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedDistrictList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedDistrictList();
    };
    $scope.searchDistrictList = function () {
        $scope.getPagedDistrictList();
    };
    $scope.getPagedDistrictList = function () {
        $scope.getDistrictList();
    };
    /*For Search on data end*/
    $scope.getDistrictList = function () {
        $http.get($scope.getPagedDistrictUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
           ,"countryId": $scope.SelectedCountryId      
           ,"divisionId": $scope.SelectedDivisionId      
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.DistrictList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get District list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get District list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getDistrictListExtraData= function()
    {
            $http.get($scope.getDistrictListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.StatusEnumList!=null)
                         $scope.StatusEnumList = result.DataExtra.StatusEnumList;
                  //DropDown Option Tables
                     if(result.DataExtra.CountryList!=null)
                       $scope.CountryList =  result.DataExtra.CountryList; 

                     if(result.DataExtra.DivisionList!=null)
                       $scope.DivisionList =  result.DataExtra.DivisionList; 

                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for District! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for District! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllDistrictList = function () {
        $scope.IsLoading++;
        $http.get($scope.getDistrictListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.DistrictList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get District list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get District list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteDistrictById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteDistrictByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.DistrictList.indexOf(obj);
                        $scope.DistrictList.splice(i, 1);
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
    /*$scope.deleteDistrictByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteDistrictByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.DistrictList.indexOf(obj);
                        $scope.DistrictList.splice(i, 1);
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
    for (var i = 0; i < $scope.DistrictList.length; i++) {
        var entity = $scope.DistrictList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedDistrictUrl
        ,deleteDistrictByIdUrl
        ,getDistrictListDataExtraUrl
        ,saveDistrictListUrl
        ,getDistrictByIdUrl
        ,getDistrictDataExtraUrl
        ,saveDistrictUrl
        ) {
        $scope.getPagedDistrictUrl = getPagedDistrictUrl;
        $scope.deleteDistrictByIdUrl = deleteDistrictByIdUrl;
        /*bind extra url if need*/
        $scope.getDistrictListDataExtraUrl = getDistrictListDataExtraUrl;
        $scope.saveDistrictListUrl = saveDistrictListUrl;
        $scope.getDistrictByIdUrl = getDistrictByIdUrl;
        $scope.getDistrictDataExtraUrl = getDistrictDataExtraUrl;
        $scope.saveDistrictUrl = saveDistrictUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



