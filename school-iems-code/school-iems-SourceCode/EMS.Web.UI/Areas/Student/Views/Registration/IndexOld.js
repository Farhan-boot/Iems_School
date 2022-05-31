
//File:Academic_ClassTakenByStudent List Anjular  Controller
emsApp.controller('ClassTakenByStudentListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.ClassTakenByStudentList = [];
    $scope.SelectedClassTakenByStudent = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
     $scope.SelectedClassId=0;      
     $scope.SelectedStudentId=0;      
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
      $scope.getClassTakenByStudentListExtraData();
      $scope.getPagedClassTakenByStudentList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedClassTakenByStudentList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedClassTakenByStudentList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedClassTakenByStudentList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedClassTakenByStudentList();
    };
    $scope.searchClassTakenByStudentList = function () {
        $scope.getPagedClassTakenByStudentList();
    };
    $scope.getPagedClassTakenByStudentList = function () {
        $scope.getClassTakenByStudentList();
    };
    /*For Search on data end*/
    $scope.getClassTakenByStudentList = function () {
        $http.get($scope.getPagedClassTakenByStudentUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
           ,"classId": $scope.SelectedClassId      
           ,"studentId": $scope.SelectedStudentId      
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.ClassTakenByStudentList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Class Taken By Student list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Class Taken By Student list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getClassTakenByStudentListExtraData= function()
    {
            $http.get($scope.getClassTakenByStudentListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.EnrollTypeEnumList!=null)
                         $scope.EnrollTypeEnumList = result.DataExtra.EnrollTypeEnumList;
                      if(result.DataExtra.RegistrationStatusEnumList!=null)
                         $scope.RegistrationStatusEnumList = result.DataExtra.RegistrationStatusEnumList;
                      if(result.DataExtra.StatusEnumList!=null)
                         $scope.StatusEnumList = result.DataExtra.StatusEnumList;
                  //DropDown Option Tables
                     if(result.DataExtra.ClassList!=null)
                       $scope.ClassList =  result.DataExtra.ClassList; 

                     if(result.DataExtra.StudentList!=null)
                       $scope.StudentList =  result.DataExtra.StudentList; 

                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Class Taken By Student! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Class Taken By Student! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllClassTakenByStudentList = function () {
        $scope.IsLoading++;
        $http.get($scope.getClassTakenByStudentListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.ClassTakenByStudentList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Class Taken By Student list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Class Taken By Student list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteClassTakenByStudentById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteClassTakenByStudentByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.ClassTakenByStudentList.indexOf(obj);
                        $scope.ClassTakenByStudentList.splice(i, 1);
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
    /*$scope.deleteClassTakenByStudentByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteClassTakenByStudentByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.ClassTakenByStudentList.indexOf(obj);
                        $scope.ClassTakenByStudentList.splice(i, 1);
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
    for (var i = 0; i < $scope.ClassTakenByStudentList.length; i++) {
        var entity = $scope.ClassTakenByStudentList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedClassTakenByStudentUrl
        ,deleteClassTakenByStudentByIdUrl
        ,getClassTakenByStudentListDataExtraUrl
        ,saveClassTakenByStudentListUrl
        ,getClassTakenByStudentByIdUrl
        ,getClassTakenByStudentDataExtraUrl
        ,saveClassTakenByStudentUrl
        ) {
        $scope.getPagedClassTakenByStudentUrl = getPagedClassTakenByStudentUrl;
        $scope.deleteClassTakenByStudentByIdUrl = deleteClassTakenByStudentByIdUrl;
        /*bind extra url if need*/
        $scope.getClassTakenByStudentListDataExtraUrl = getClassTakenByStudentListDataExtraUrl;
        $scope.saveClassTakenByStudentListUrl = saveClassTakenByStudentListUrl;
        $scope.getClassTakenByStudentByIdUrl = getClassTakenByStudentByIdUrl;
        $scope.getClassTakenByStudentDataExtraUrl = getClassTakenByStudentDataExtraUrl;
        $scope.saveClassTakenByStudentUrl = saveClassTakenByStudentUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



