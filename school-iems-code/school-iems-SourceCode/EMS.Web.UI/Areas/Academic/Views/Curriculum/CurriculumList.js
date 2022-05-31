
//File:Academic_Curriculum List Anjular  Controller
emsApp.controller('CurriculumListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.CurriculumList = [];
    $scope.SelectedCurriculum = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
     $scope.SelectedGradingPolicyId=0;      
     $scope.SelectedProgramId=0;      
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
      $scope.getCurriculumListExtraData();
      $scope.getPagedCurriculumList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedCurriculumList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedCurriculumList();
    };
    $scope.searchCurriculumList = function () {
        $scope.getPagedCurriculumList();
    };
    $scope.getPagedCurriculumList = function () {
        $scope.getCurriculumList();
    };
    /*For Search on data end*/
    $scope.getCurriculumList = function () {
        $http.get($scope.getPagedCurriculumUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
           ,"gradingPolicyId": $scope.SelectedGradingPolicyId      
           ,"programId": $scope.SelectedProgramId      
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.CurriculumList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Curriculum list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Curriculum list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getCurriculumListExtraData= function()
    {
            $http.get($scope.getCurriculumListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                  //DropDown Option Tables
                     if(result.DataExtra.GradingPolicyList!=null)
                       $scope.GradingPolicyList =  result.DataExtra.GradingPolicyList; 

                     if(result.DataExtra.ProgramList!=null)
                       $scope.ProgramList =  result.DataExtra.ProgramList; 

                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Curriculum! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Curriculum! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllCurriculumList = function () {
        $scope.IsLoading++;
        $http.get($scope.getCurriculumListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.CurriculumList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Curriculum list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Curriculum list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteCurriculumById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to <b>delete</b> this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteCurriculumByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.CurriculumList.indexOf(obj);
                        $scope.CurriculumList.splice(i, 1);
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
    /*$scope.deleteCurriculumByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteCurriculumByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.CurriculumList.indexOf(obj);
                        $scope.CurriculumList.splice(i, 1);
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
    for (var i = 0; i < $scope.CurriculumList.length; i++) {
        var entity = $scope.CurriculumList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start======================================================= 

    $scope.getDuplicateCurriculumWithCourseById = function (CurriculumId) {
        if (CurriculumId != null && CurriculumId !== '')
            $scope.CurriculumId = CurriculumId;
        else return;
        bootbox.confirm("Are you sure you want to <b>duplicate</b> this data?", function (yes) {
            if (yes) {

                $http.get($scope.getDuplicateCurriculumWithCourseByIdUrl,
                    {
                        params: { "id": $scope.CurriculumId }
                    }).success(function(result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        $scope.getPagedCurriculumList();
                        alertSuccess("Successfully Duplicate Curriculum data!");
                    } else {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Unable to Duplicate Curriculum! " + result.Errors.toString();
                        alertError($scope.ErrorMsg);
                    }
                }).error(function(result, status) {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to Duplicate Curriculum! " +
                        JSON.stringify(result).toString() +
                        ". " +
                        "Status: " +
                        JSON.stringify(status).toString();
                    alertError($scope.ErrorMsg);
                });
            }
        });
    };

    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedCurriculumUrl
        ,deleteCurriculumByIdUrl
        ,getCurriculumListDataExtraUrl
        ,saveCurriculumListUrl
        ,getCurriculumByIdUrl
        ,getCurriculumDataExtraUrl
        , saveCurriculumUrl
        , getDuplicateCurriculumWithCourseByIdUrl
        ) {
        $scope.getPagedCurriculumUrl = getPagedCurriculumUrl;
        $scope.deleteCurriculumByIdUrl = deleteCurriculumByIdUrl;
        /*bind extra url if need*/
        $scope.getCurriculumListDataExtraUrl = getCurriculumListDataExtraUrl;
        $scope.saveCurriculumListUrl = saveCurriculumListUrl;
        $scope.getCurriculumByIdUrl = getCurriculumByIdUrl;
        $scope.getCurriculumDataExtraUrl = getCurriculumDataExtraUrl;
        $scope.saveCurriculumUrl = saveCurriculumUrl;
        $scope.getDuplicateCurriculumWithCourseByIdUrl = getDuplicateCurriculumWithCourseByIdUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



