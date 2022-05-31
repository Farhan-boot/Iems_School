
//File:Admission_AdmissionExam List Anjular  Controller
emsApp.controller('AdmissionExamListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.AdmissionExamList = [];
    $scope.SelectedAdmissionExam = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
     $scope.SelectedSemesterId=0;      
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;
    $scope.SelectedSemesterDurationEnumId = 0;
    $scope.IsCopyingBusy = false;

    $scope.SemesterDurationEnumId = 0;
    $scope.SelectedSemesterName = "";
    $scope.SourceAdmissionExamId = 0;
    $scope.NewAdmissionExamSemesterId = 0;
    $scope.IncrementBatchCount = 1;
    $scope.NewAdmissionExamName = "";
    $scope.StudentIdPrefix = "";

    $scope.loadPage = function () {
      $scope.getAdmissionExamListExtraData();
      $scope.getPagedAdmissionExamList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedAdmissionExamList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedAdmissionExamList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedAdmissionExamList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedAdmissionExamList();
    };
    $scope.searchAdmissionExamList = function () {
        $scope.getPagedAdmissionExamList();
    };
    $scope.getPagedAdmissionExamList = function () {
        $scope.getAdmissionExamList();
    };
    /*For Search on data end*/
    $scope.getAdmissionExamList = function () {
        $http.get($scope.getPagedAdmissionExamUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
           ,"semesterId": $scope.SelectedSemesterId      
           , "selectedSemesterDurationEnumId": $scope.SelectedSemesterDurationEnumId
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.AdmissionExamList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Admission Exam list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Admission Exam list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getAdmissionExamListExtraData= function()
    {
            $http.get($scope.getAdmissionExamListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.ProgramTypeEnumList!=null)
                         $scope.ProgramTypeEnumList = result.DataExtra.ProgramTypeEnumList;
                      if(result.DataExtra.CircularStatusEnumList!=null)
                          $scope.CircularStatusEnumList = result.DataExtra.CircularStatusEnumList;
                      if (result.DataExtra.SemesterDurationEnumList != null)
                          $scope.SemesterDurationEnumList = result.DataExtra.SemesterDurationEnumList;
                  //DropDown Option Tables
                     if(result.DataExtra.SemesterList!=null)
                       $scope.SemesterList =  result.DataExtra.SemesterList; 

                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Admission Exam! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Admission Exam! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllAdmissionExamList = function () {
        $scope.IsLoading++;
        $http.get($scope.getAdmissionExamListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.AdmissionExamList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Admission Exam list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Admission Exam list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteAdmissionExamById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteAdmissionExamByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.AdmissionExamList.indexOf(obj);
                        $scope.AdmissionExamList.splice(i, 1);
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

    $scope.getCopyAdmissionExam = function () {
        if (!$scope.IsCopyingBusy) {
            $scope.IsCopyingBusy = true;

            $http.get($scope.getCopyAdmissionExamUrl, {
                params: {
                    "sourceAdmissionExamId": $scope.SourceAdmissionExamId,
                    "newAdmissionExamSemesterId": $scope.NewAdmissionExamSemesterId,
                    "newAdmissionExamName": $scope.NewAdmissionExamName,
                    "newStudentIdPrefix": $scope.StudentIdPrefix,
                    "studentUgcIdPrefix": $scope.StudentUgcIdPrefix,
                    "incrementBatchCount": $scope.IncrementBatchCount
                }
            }).success(function (result, status) {
                $scope.IsCopyingBusy = false;
                if (!result.HasError) {
                    $scope.HasError = false;
                    $('#CopyAdmissionExamModal').modal('hide');

                    $scope.AdmissionExamList.splice(0, 0, result.Data);

                    alertSuccess("Data successfully deleted!");
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Sorry unable to Copy! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                $scope.IsCopyingBusy = false;
                $scope.HasError = true;
                $scope.ErrorMsg = "Sorry unable to Copy! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
                alertError($scope.ErrorMsg);
            });
        }
    };

    /*$scope.deleteAdmissionExamByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteAdmissionExamByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.AdmissionExamList.indexOf(obj);
                        $scope.AdmissionExamList.splice(i, 1);
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
    for (var i = 0; i < $scope.AdmissionExamList.length; i++) {
        var entity = $scope.AdmissionExamList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  

    $scope.showCopyAdmissionExamModal = function (admissionExam) {
        //log(admissionExam);
        //log($scope.SemesterList);
        var index = $scope.SemesterList.indexOf($scope.SemesterList.find(x => x.Id === admissionExam.SemesterId));
        //log(index);
        $scope.SelectedSemesterName = admissionExam.Name;
        $scope.SourceAdmissionExamId = admissionExam.Id;
        $scope.SemesterDurationEnumId = admissionExam.SemesterDurationEnumId;
        $scope.NewAdmissionExamSemesterId = $scope.SemesterList[index+1].Id;
        $scope.IncrementBatchCount = 1;
        $scope.NewAdmissionExamName = "";
        $scope.StudentIdPrefix = "";
        $scope.StudentUgcIdPrefix = "";
        $('#CopyAdmissionExamModal').modal('show');
    }

    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedAdmissionExamUrl
        ,deleteAdmissionExamByIdUrl
        ,getAdmissionExamListDataExtraUrl
        ,saveAdmissionExamListUrl
        ,getAdmissionExamByIdUrl
        ,getAdmissionExamDataExtraUrl
        ,saveAdmissionExamUrl
        , getCopyAdmissionExamUrl
        ) {
        $scope.getPagedAdmissionExamUrl = getPagedAdmissionExamUrl;
        $scope.deleteAdmissionExamByIdUrl = deleteAdmissionExamByIdUrl;
        /*bind extra url if need*/
        $scope.getAdmissionExamListDataExtraUrl = getAdmissionExamListDataExtraUrl;
        $scope.saveAdmissionExamListUrl = saveAdmissionExamListUrl;
        $scope.getAdmissionExamByIdUrl = getAdmissionExamByIdUrl;
        $scope.getAdmissionExamDataExtraUrl = getAdmissionExamDataExtraUrl;
        $scope.saveAdmissionExamUrl = saveAdmissionExamUrl;
        $scope.getCopyAdmissionExamUrl = getCopyAdmissionExamUrl;

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



s