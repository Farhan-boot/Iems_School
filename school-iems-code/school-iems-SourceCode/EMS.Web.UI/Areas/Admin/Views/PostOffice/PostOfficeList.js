
//File:Admin_PostOffice List Anjular  Controller
emsApp.controller('PostOfficeListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.PostOfficeList = [];
    $scope.SelectedPostOffice = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
     $scope.SelectedDistrictId=0;      
     $scope.SelectedPoliceStationId=0;      
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
      $scope.getPostOfficeListExtraData();
      $scope.getPagedPostOfficeList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedPostOfficeList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedPostOfficeList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedPostOfficeList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedPostOfficeList();
    };
    $scope.searchPostOfficeList = function () {
        $scope.getPagedPostOfficeList();
    };
    $scope.getPagedPostOfficeList = function () {
        $scope.getPostOfficeList();
    };
    /*For Search on data end*/
    $scope.getPostOfficeList = function () {
        $http.get($scope.getPagedPostOfficeUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
           ,"districtId": $scope.SelectedDistrictId      
           ,"policeStationId": $scope.SelectedPoliceStationId      
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.PostOfficeList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Post Office list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Post Office list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getPostOfficeListExtraData= function()
    {
            $http.get($scope.getPostOfficeListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.StatusEnumList!=null)
                         $scope.StatusEnumList = result.DataExtra.StatusEnumList;
                  //DropDown Option Tables
                     if(result.DataExtra.DistrictList!=null)
                       $scope.DistrictList =  result.DataExtra.DistrictList; 

                     if(result.DataExtra.PoliceStationList!=null)
                       $scope.PoliceStationList =  result.DataExtra.PoliceStationList; 

                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Post Office! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Post Office! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllPostOfficeList = function () {
        $scope.IsLoading++;
        $http.get($scope.getPostOfficeListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.PostOfficeList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Post Office list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Post Office list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deletePostOfficeById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deletePostOfficeByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.PostOfficeList.indexOf(obj);
                        $scope.PostOfficeList.splice(i, 1);
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
    /*$scope.deletePostOfficeByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deletePostOfficeByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.PostOfficeList.indexOf(obj);
                        $scope.PostOfficeList.splice(i, 1);
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
    for (var i = 0; i < $scope.PostOfficeList.length; i++) {
        var entity = $scope.PostOfficeList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedPostOfficeUrl
        ,deletePostOfficeByIdUrl
        ,getPostOfficeListDataExtraUrl
        ,savePostOfficeListUrl
        ,getPostOfficeByIdUrl
        ,getPostOfficeDataExtraUrl
        ,savePostOfficeUrl
        ) {
        $scope.getPagedPostOfficeUrl = getPagedPostOfficeUrl;
        $scope.deletePostOfficeByIdUrl = deletePostOfficeByIdUrl;
        /*bind extra url if need*/
        $scope.getPostOfficeListDataExtraUrl = getPostOfficeListDataExtraUrl;
        $scope.savePostOfficeListUrl = savePostOfficeListUrl;
        $scope.getPostOfficeByIdUrl = getPostOfficeByIdUrl;
        $scope.getPostOfficeDataExtraUrl = getPostOfficeDataExtraUrl;
        $scope.savePostOfficeUrl = savePostOfficeUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



