
//File:Academic_HomeWork List Anjular  Controller
emsApp.controller('HomeWorkListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.HomeWorkList = [];
    $scope.SelectedHomeWork = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
     $scope.SelectedClassShiftSectionMapId=0;      
     $scope.SelectedSubjectId=0;      
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
      $scope.getHomeWorkListExtraData();
      $scope.getPagedHomeWorkList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedHomeWorkList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedHomeWorkList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedHomeWorkList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedHomeWorkList();
    };
    $scope.searchHomeWorkList = function () {
        $scope.getPagedHomeWorkList();
    };
    $scope.getPagedHomeWorkList = function () {
        $scope.getHomeWorkList();
    };
    /*For Search on data end*/
    $scope.getHomeWorkList = function () {
        $http.get($scope.getPagedHomeWorkUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
           ,"classShiftSectionMapId": $scope.SelectedClassShiftSectionMapId      
           ,"subjectId": $scope.SelectedSubjectId      
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.HomeWorkList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Home Work list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Home Work list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getHomeWorkListExtraData= function()
    {
            $http.get($scope.getHomeWorkListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.GroupEnumList!=null)
                         $scope.GroupEnumList = result.DataExtra.GroupEnumList;
                      if(result.DataExtra.HomeworkTypeEnumList!=null)
                         $scope.HomeworkTypeEnumList = result.DataExtra.HomeworkTypeEnumList;
                  //DropDown Option Tables
                     if(result.DataExtra.ClassShiftSectionMapList!=null)
                       $scope.ClassShiftSectionMapList =  result.DataExtra.ClassShiftSectionMapList; 

                     if(result.DataExtra.SubjectList!=null)
                       $scope.SubjectList =  result.DataExtra.SubjectList; 

                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Home Work! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Home Work! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllHomeWorkList = function () {
        $scope.IsLoading++;
        $http.get($scope.getHomeWorkListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HomeWorkList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Home Work list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Home Work list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteHomeWorkById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteHomeWorkByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.HomeWorkList.indexOf(obj);
                        $scope.HomeWorkList.splice(i, 1);
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
    /*$scope.deleteHomeWorkByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteHomeWorkByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.HomeWorkList.indexOf(obj);
                        $scope.HomeWorkList.splice(i, 1);
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
    for (var i = 0; i < $scope.HomeWorkList.length; i++) {
        var entity = $scope.HomeWorkList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedHomeWorkUrl
        ,deleteHomeWorkByIdUrl
        ,getHomeWorkListDataExtraUrl
        ,saveHomeWorkListUrl
        ,getHomeWorkByIdUrl
        ,getHomeWorkDataExtraUrl
        ,saveHomeWorkUrl
        ) {
        $scope.getPagedHomeWorkUrl = getPagedHomeWorkUrl;
        $scope.deleteHomeWorkByIdUrl = deleteHomeWorkByIdUrl;
        /*bind extra url if need*/
        $scope.getHomeWorkListDataExtraUrl = getHomeWorkListDataExtraUrl;
        $scope.saveHomeWorkListUrl = saveHomeWorkListUrl;
        $scope.getHomeWorkByIdUrl = getHomeWorkByIdUrl;
        $scope.getHomeWorkDataExtraUrl = getHomeWorkDataExtraUrl;
        $scope.saveHomeWorkUrl = saveHomeWorkUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



