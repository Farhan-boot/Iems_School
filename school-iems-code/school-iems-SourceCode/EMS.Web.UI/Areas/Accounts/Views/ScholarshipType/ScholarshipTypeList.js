
//File:Accounts_ScholarshipType List Anjular  Controller
emsApp.controller('ScholarshipTypeListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.ScholarshipTypeList = [];
    $scope.SelectedScholarshipType = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
      $scope.getScholarshipTypeListExtraData();
      $scope.getPagedScholarshipTypeList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedScholarshipTypeList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedScholarshipTypeList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedScholarshipTypeList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedScholarshipTypeList();
    };
    $scope.searchScholarshipTypeList = function () {
        $scope.getPagedScholarshipTypeList();
    };
    $scope.getPagedScholarshipTypeList = function () {
        $scope.getScholarshipTypeList();
    };
    /*For Search on data end*/
    $scope.getScholarshipTypeList = function () {
        $http.get($scope.getPagedScholarshipTypeUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.ScholarshipTypeList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Scholarship Type list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Scholarship Type list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getScholarshipTypeListExtraData= function()
    {
            $http.get($scope.getScholarshipTypeListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.StatusEnumList!=null)
                         $scope.StatusEnumList = result.DataExtra.StatusEnumList;
                  //DropDown Option Tables
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Scholarship Type! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Scholarship Type! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllScholarshipTypeList = function () {
        $scope.IsLoading++;
        $http.get($scope.getScholarshipTypeListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.ScholarshipTypeList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Scholarship Type list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Scholarship Type list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteScholarshipTypeById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteScholarshipTypeByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.ScholarshipTypeList.indexOf(obj);
                        $scope.ScholarshipTypeList.splice(i, 1);
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
    /*$scope.deleteScholarshipTypeByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteScholarshipTypeByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.ScholarshipTypeList.indexOf(obj);
                        $scope.ScholarshipTypeList.splice(i, 1);
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
    for (var i = 0; i < $scope.ScholarshipTypeList.length; i++) {
        var entity = $scope.ScholarshipTypeList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedScholarshipTypeUrl
        ,deleteScholarshipTypeByIdUrl
        ,getScholarshipTypeListDataExtraUrl
        ,saveScholarshipTypeListUrl
        ,getScholarshipTypeByIdUrl
        ,getScholarshipTypeDataExtraUrl
        ,saveScholarshipTypeUrl
        ) {
        $scope.getPagedScholarshipTypeUrl = getPagedScholarshipTypeUrl;
        $scope.deleteScholarshipTypeByIdUrl = deleteScholarshipTypeByIdUrl;
        /*bind extra url if need*/
        $scope.getScholarshipTypeListDataExtraUrl = getScholarshipTypeListDataExtraUrl;
        $scope.saveScholarshipTypeListUrl = saveScholarshipTypeListUrl;
        $scope.getScholarshipTypeByIdUrl = getScholarshipTypeByIdUrl;
        $scope.getScholarshipTypeDataExtraUrl = getScholarshipTypeDataExtraUrl;
        $scope.saveScholarshipTypeUrl = saveScholarshipTypeUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



