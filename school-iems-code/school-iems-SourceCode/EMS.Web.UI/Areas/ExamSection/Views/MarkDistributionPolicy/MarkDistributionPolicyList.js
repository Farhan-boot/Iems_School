
//File:Academic_MarkDistributionPolicy List Anjular  Controller
emsApp.controller('MarkDistributionPolicyListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.MarkDistributionPolicyList = [];
    $scope.SelectedMarkDistributionPolicy = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
     $scope.SelectedProgramId=0;      
     $scope.SelectedEndSemesterId=0;      
     $scope.SelectedStartSemesterId=0;      
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
      $scope.getMarkDistributionPolicyListExtraData();
      $scope.getPagedMarkDistributionPolicyList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedMarkDistributionPolicyList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedMarkDistributionPolicyList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedMarkDistributionPolicyList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedMarkDistributionPolicyList();
    };
    $scope.searchMarkDistributionPolicyList = function () {
        $scope.getPagedMarkDistributionPolicyList();
    };
    $scope.getPagedMarkDistributionPolicyList = function () {
        $scope.getMarkDistributionPolicyList();
    };
    /*For Search on data end*/
    $scope.getMarkDistributionPolicyList = function () {
        $http.get($scope.getPagedMarkDistributionPolicyUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
           ,"programId": $scope.SelectedProgramId      
           ,"endSemesterId": $scope.SelectedEndSemesterId      
           ,"startSemesterId": $scope.SelectedStartSemesterId      
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.MarkDistributionPolicyList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Mark Distribution Policy list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Mark Distribution Policy list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getMarkDistributionPolicyListExtraData= function()
    {
            $http.get($scope.getMarkDistributionPolicyListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.CourseCategoryEnumList!=null)
                         $scope.CourseCategoryEnumList = result.DataExtra.CourseCategoryEnumList;
                      if(result.DataExtra.ExamTypeEnumList!=null)
                         $scope.ExamTypeEnumList = result.DataExtra.ExamTypeEnumList;
                      if(result.DataExtra.StatusEnumList!=null)
                         $scope.StatusEnumList = result.DataExtra.StatusEnumList;
                  //DropDown Option Tables
                     if(result.DataExtra.ProgramList!=null)
                       $scope.ProgramList =  result.DataExtra.ProgramList; 

                     if(result.DataExtra.SemesterList!=null)
                       $scope.SemesterList =  result.DataExtra.SemesterList; 

                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Mark Distribution Policy! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Mark Distribution Policy! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    $scope.getDuplicateMarkDistributionPolicyById = function(id) {
        bootbox.confirm("Are you sure you want to Copy this data?",
            function(yes) {
                if (yes) {
                    $http.get($scope.getDuplicateMarkDistributionPolicyByIdUrl,
                        {
                            params: {
                                "id": id
                            }
                        }).success(function(result) {
                        if (!result.HasError) {
                            $scope.HasError = false;
                            $scope.HasViewPermission = result.HasViewPermission;
                            $scope.getPagedMarkDistributionPolicyList();

                        } else {
                            $scope.HasError = true;
                            $scope.ErrorMsg =
                                "Unable to Duplicate Mark Distribution Policy! " + result.Errors.toString();
                            alertError($scope.ErrorMsg);
                        }
                    }).error(function(result, status) {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Unable to Duplicate Mark Distribution Policy! " +
                            "Status: " +
                            JSON.stringify(status).toString() +
                            ". " +
                            JSON.stringify(result).toString();
                        alertError($scope.ErrorMsg);
                    });
                }
            });
    };
    

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllMarkDistributionPolicyList = function () {
        $scope.IsLoading++;
        $http.get($scope.getMarkDistributionPolicyListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.MarkDistributionPolicyList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Mark Distribution Policy list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Mark Distribution Policy list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteMarkDistributionPolicyById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteMarkDistributionPolicyByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.MarkDistributionPolicyList.indexOf(obj);
                        $scope.MarkDistributionPolicyList.splice(i, 1);
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
    /*$scope.deleteMarkDistributionPolicyByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteMarkDistributionPolicyByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.MarkDistributionPolicyList.indexOf(obj);
                        $scope.MarkDistributionPolicyList.splice(i, 1);
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
    for (var i = 0; i < $scope.MarkDistributionPolicyList.length; i++) {
        var entity = $scope.MarkDistributionPolicyList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedMarkDistributionPolicyUrl
        ,deleteMarkDistributionPolicyByIdUrl
        ,getMarkDistributionPolicyListDataExtraUrl
        ,saveMarkDistributionPolicyListUrl
        ,getMarkDistributionPolicyByIdUrl
        ,getMarkDistributionPolicyDataExtraUrl
        , saveMarkDistributionPolicyUrl
        , getDuplicateMarkDistributionPolicyByIdUrl
        ) {
        $scope.getPagedMarkDistributionPolicyUrl = getPagedMarkDistributionPolicyUrl;
        $scope.deleteMarkDistributionPolicyByIdUrl = deleteMarkDistributionPolicyByIdUrl;
        /*bind extra url if need*/
        $scope.getMarkDistributionPolicyListDataExtraUrl = getMarkDistributionPolicyListDataExtraUrl;
        $scope.saveMarkDistributionPolicyListUrl = saveMarkDistributionPolicyListUrl;
        $scope.getMarkDistributionPolicyByIdUrl = getMarkDistributionPolicyByIdUrl;
        $scope.getMarkDistributionPolicyDataExtraUrl = getMarkDistributionPolicyDataExtraUrl;
        $scope.saveMarkDistributionPolicyUrl = saveMarkDistributionPolicyUrl;
        $scope.getDuplicateMarkDistributionPolicyByIdUrl = getDuplicateMarkDistributionPolicyByIdUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



