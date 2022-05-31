
//File:Academic_HomeWorkSubmission List Anjular  Controller
emsApp.controller('HomeWorkSubmissionListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.HomeWorkSubmissionList = [];
    $scope.SelectedHomeWorkSubmission = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
     $scope.SelectedHomeworkId=0;      
     $scope.SelectedStudentId=0;      
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
      $scope.getHomeWorkSubmissionListExtraData();
      $scope.getPagedHomeWorkSubmissionList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedHomeWorkSubmissionList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedHomeWorkSubmissionList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedHomeWorkSubmissionList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedHomeWorkSubmissionList();
    };
    $scope.searchHomeWorkSubmissionList = function () {
        $scope.getPagedHomeWorkSubmissionList();
    };
    $scope.getPagedHomeWorkSubmissionList = function () {
        $scope.getHomeWorkSubmissionList();
    };
    /*For Search on data end*/
    $scope.getHomeWorkSubmissionList = function () {
        $http.get($scope.getPagedHomeWorkSubmissionUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
           ,"homeworkId": $scope.SelectedHomeworkId      
           ,"studentId": $scope.SelectedStudentId      
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.HomeWorkSubmissionList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Home Work Submission list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Home Work Submission list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getHomeWorkSubmissionListExtraData= function()
    {
            $http.get($scope.getHomeWorkSubmissionListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.SubmissionStatusEnumList!=null)
                         $scope.SubmissionStatusEnumList = result.DataExtra.SubmissionStatusEnumList;
                  //DropDown Option Tables
                     if(result.DataExtra.HomeWorkList!=null)
                       $scope.HomeWorkList =  result.DataExtra.HomeWorkList; 

                     if(result.DataExtra.StudentList!=null)
                       $scope.StudentList =  result.DataExtra.StudentList; 

                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Home Work Submission! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Home Work Submission! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllHomeWorkSubmissionList = function () {
        $scope.IsLoading++;
        $http.get($scope.getHomeWorkSubmissionListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HomeWorkSubmissionList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Home Work Submission list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Home Work Submission list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteHomeWorkSubmissionById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteHomeWorkSubmissionByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.HomeWorkSubmissionList.indexOf(obj);
                        $scope.HomeWorkSubmissionList.splice(i, 1);
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
    /*$scope.deleteHomeWorkSubmissionByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteHomeWorkSubmissionByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.HomeWorkSubmissionList.indexOf(obj);
                        $scope.HomeWorkSubmissionList.splice(i, 1);
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
    for (var i = 0; i < $scope.HomeWorkSubmissionList.length; i++) {
        var entity = $scope.HomeWorkSubmissionList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedHomeWorkSubmissionUrl
        ,deleteHomeWorkSubmissionByIdUrl
        ,getHomeWorkSubmissionListDataExtraUrl
        ,saveHomeWorkSubmissionListUrl
        ,getHomeWorkSubmissionByIdUrl
        ,getHomeWorkSubmissionDataExtraUrl
        ,saveHomeWorkSubmissionUrl
        ) {
        $scope.getPagedHomeWorkSubmissionUrl = getPagedHomeWorkSubmissionUrl;
        $scope.deleteHomeWorkSubmissionByIdUrl = deleteHomeWorkSubmissionByIdUrl;
        /*bind extra url if need*/
        $scope.getHomeWorkSubmissionListDataExtraUrl = getHomeWorkSubmissionListDataExtraUrl;
        $scope.saveHomeWorkSubmissionListUrl = saveHomeWorkSubmissionListUrl;
        $scope.getHomeWorkSubmissionByIdUrl = getHomeWorkSubmissionByIdUrl;
        $scope.getHomeWorkSubmissionDataExtraUrl = getHomeWorkSubmissionDataExtraUrl;
        $scope.saveHomeWorkSubmissionUrl = saveHomeWorkSubmissionUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



