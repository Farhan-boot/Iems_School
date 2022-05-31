
//File:Academic_CurriculumCourse List Anjular  Controller
emsApp.controller('CurriculumCourseListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.CurriculumCourseList = [];
    $scope.SelectedCurriculumCourse = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
     $scope.SelectedCurriculumId=0;      
     $scope.SelectedElectiveGroupId=0;      
     $scope.SelectedCreditTypeEnumId = -1;
     $scope.SelectedStudyLevelTermId=0;      
     $scope.SelectedOfferedByDepartmentId=0;      
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
      $scope.getCurriculumCourseListExtraData();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedCurriculumCourseList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedCurriculumCourseList();
    };
    $scope.searchCurriculumCourseList = function () {
        $scope.getPagedCurriculumCourseList();
    };
    $scope.getPagedCurriculumCourseList = function () {
        $scope.getCurriculumCourseList();
    };
    /*For Search on data end*/
    $scope.getCurriculumCourseList = function () {
        $http.get($scope.getPagedCurriculumCourseUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
           ,"curriculumId": $scope.SelectedCurriculumId      
           ,"electiveGroupId": $scope.SelectedElectiveGroupId      
           ,"studyLevelTermId": $scope.SelectedStudyLevelTermId      
           ,"offeredByDepartmentId": $scope.SelectedOfferedByDepartmentId      
           , "creditTypeEnumId": $scope.SelectedCreditTypeEnumId
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.CurriculumCourseList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Curriculum Course list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Curriculum Course list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getCurriculumCourseListExtraData= function()
    {
            $http.get($scope.getCurriculumCourseListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.CourseCategoryEnumList!=null)
                         $scope.CourseCategoryEnumList = result.DataExtra.CourseCategoryEnumList;
                      if(result.DataExtra.CreditTypeEnumList!=null)
                         $scope.CreditTypeEnumList = result.DataExtra.CreditTypeEnumList;
                      if(result.DataExtra.CourseTypeEnumList!=null)
                         $scope.CourseTypeEnumList = result.DataExtra.CourseTypeEnumList;
                      if(result.DataExtra.StatusEnumList!=null)
                         $scope.StatusEnumList = result.DataExtra.StatusEnumList;
                  //DropDown Option Tables
                     if(result.DataExtra.CurriculumList!=null){
                         $scope.CurriculumList = result.DataExtra.CurriculumList;
                         $scope.SelectedCurriculumId = $scope.CurriculumList[0].Id;
                     }

                     if(result.DataExtra.CurriculumElectiveGroupList!=null)
                       $scope.CurriculumElectiveGroupList =  result.DataExtra.CurriculumElectiveGroupList; 

                     if(result.DataExtra.StudyLevelTermList !=null)
                       $scope.StudyLevelTermList =  result.DataExtra.StudyLevelTermList; 

                     if(result.DataExtra.DepartmentList!=null)
                       $scope.DepartmentList =  result.DataExtra.DepartmentList; 

                     $scope.getPagedCurriculumCourseList();
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Curriculum Course! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Curriculum Course! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllCurriculumCourseList = function () {
        $scope.IsLoading++;
        $http.get($scope.getCurriculumCourseListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.CurriculumCourseList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Curriculum Course list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Curriculum Course list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteCurriculumCourseById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteCurriculumCourseByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.CurriculumCourseList.indexOf(obj);
                        $scope.CurriculumCourseList.splice(i, 1);
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
    /*$scope.deleteCurriculumCourseByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteCurriculumCourseByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.CurriculumCourseList.indexOf(obj);
                        $scope.CurriculumCourseList.splice(i, 1);
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
    for (var i = 0; i < $scope.CurriculumCourseList.length; i++) {
        var entity = $scope.CurriculumCourseList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedCurriculumCourseUrl
        ,deleteCurriculumCourseByIdUrl
        ,getCurriculumCourseListDataExtraUrl
        ,saveCurriculumCourseListUrl
        ,getCurriculumCourseByIdUrl
        ,getCurriculumCourseDataExtraUrl
        ,saveCurriculumCourseUrl
        ) {
        $scope.getPagedCurriculumCourseUrl = getPagedCurriculumCourseUrl;
        $scope.deleteCurriculumCourseByIdUrl = deleteCurriculumCourseByIdUrl;
        /*bind extra url if need*/
        $scope.getCurriculumCourseListDataExtraUrl = getCurriculumCourseListDataExtraUrl;
        $scope.saveCurriculumCourseListUrl = saveCurriculumCourseListUrl;
        $scope.getCurriculumCourseByIdUrl = getCurriculumCourseByIdUrl;
        $scope.getCurriculumCourseDataExtraUrl = getCurriculumCourseDataExtraUrl;
        $scope.saveCurriculumCourseUrl = saveCurriculumCourseUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



