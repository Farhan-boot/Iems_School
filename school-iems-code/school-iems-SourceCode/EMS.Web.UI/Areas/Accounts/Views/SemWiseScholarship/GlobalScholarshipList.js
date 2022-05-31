
//File:Accounts_SemWiseScholarship List Anjular  Controller
emsApp.controller('SemWiseScholarshipListCtrl', function ($scope, $http, $filter, cfpLoadingBar, $sce) {
    $scope.SemWiseScholarshipList = [];
    $scope.SelectedSemWiseScholarship = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
    $scope.isShowTrashedScholarship = false;
     $scope.SelectedProgramId=0;      
     $scope.SelectedEndSemesterId=0;      
     $scope.SelectedStartSemesterId=0;      
     $scope.SelectedDependOnParticularId=0;      
     $scope.SelectedNameParticularId=0;      
     $scope.SelectedStudentId = 0;
     $scope.SelectedDiscountCategoryEnumId = 0;
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
      $scope.getSemWiseScholarshipListExtraData();
      $scope.getPagedSemWiseScholarshipList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedSemWiseScholarshipList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedSemWiseScholarshipList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedSemWiseScholarshipList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedSemWiseScholarshipList();
    };
    $scope.searchSemWiseScholarshipList = function () {
        $scope.getPagedSemWiseScholarshipList();
    };
    $scope.getPagedSemWiseScholarshipList = function () {
        $scope.getSemWiseScholarshipList();
    };
    /*For Search on data end*/
    $scope.getSemWiseScholarshipList = function () {
        $http.get($scope.getPagedSemWiseScholarshipUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
           ,"programId": $scope.SelectedProgramId      
           ,"endSemesterId": $scope.SelectedEndSemesterId      
           ,"startSemesterId": $scope.SelectedStartSemesterId      
           ,"dependOnParticularId": $scope.SelectedDependOnParticularId      
           ,"nameParticularId": $scope.SelectedNameParticularId      
           , "studentId": $scope.SelectedStudentId
            , "discountCategoryEnumId": $scope.SelectedDiscountCategoryEnumId
            , "isShowTrashedScholarship": $scope.isShowTrashedScholarship
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.SemWiseScholarshipList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Sem Wise Scholarship list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Sem Wise Scholarship list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getSemWiseScholarshipListExtraData= function()
    {
            $http.get($scope.getSemWiseScholarshipListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.DiscountCategoryEnumList!=null)
                         $scope.DiscountCategoryEnumList = result.DataExtra.DiscountCategoryEnumList;
                  //DropDown Option Tables
                     if(result.DataExtra.ProgramList!=null)
                       $scope.ProgramList =  result.DataExtra.ProgramList; 

                     if(result.DataExtra.SemesterList!=null)
                       $scope.SemesterList =  result.DataExtra.SemesterList; 

                     if(result.DataExtra.ParticularNameList!=null)
                         $scope.ParticularNameList = result.DataExtra.ParticularNameList;

                     if (result.DataExtra.ScholarshipParticularNameList != null)
                         $scope.ScholarshipParticularNameList = result.DataExtra.ScholarshipParticularNameList;

                     if(result.DataExtra.StudentList!=null)
                       $scope.StudentList =  result.DataExtra.StudentList; 

                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Sem Wise Scholarship! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Sem Wise Scholarship! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllSemWiseScholarshipList = function () {
        $scope.IsLoading++;
        $http.get($scope.getSemWiseScholarshipListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.SemWiseScholarshipList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Sem Wise Scholarship list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Sem Wise Scholarship list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteSemWiseScholarshipById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteSemWiseScholarshipByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.SemWiseScholarshipList.indexOf(obj);
                        $scope.SemWiseScholarshipList.splice(i, 1);
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
    /*$scope.deleteSemWiseScholarshipByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteSemWiseScholarshipByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.SemWiseScholarshipList.indexOf(obj);
                        $scope.SemWiseScholarshipList.splice(i, 1);
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
    for (var i = 0; i < $scope.SemWiseScholarshipList.length; i++) {
        var entity = $scope.SemWiseScholarshipList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  

    $scope.individualStudentScholarshipViewModalOpen = function (studentScholarship) {
        $scope.StudentScholarship = studentScholarship;
        $('#individualStudentScholarshipViewModal').modal('show');

    };

    $scope.getLockOrUnlockIndividualStudentScholarshipById = function (row, isLock) {

        if (row != null) {
            $scope.ScholarshipId = row.Id;
        } else {
            return;
        }
        var msg = "";
        if (isLock) {
            msg = "Lock";
        } else {
            msg = "Unlock";
        }

        bootbox.confirm("Are you sure you want to " + msg + " this data?", function (yes) {
            if (yes) {

                $http.get($scope.getLockOrUnlockIndividualStudentScholarshipByIdUrl,
                    {
                        params: {
                            "id": $scope.ScholarshipId,
                            "isLock": isLock
                        }
                    }).success(function (result, status) {
                        if (!result.HasError) {
                            $scope.HasError = false;

                            /*if ($scope.SemWiseScholarshipList.length > 0) {
                                //var index = $scope.SemWiseScholarshipList.indexOf(row);
                                //$scope.SemWiseScholarshipList[index] = result.Data;
                            }*/
                            //row.IsLocked = !row.IsLocked;
                            alertSuccess("Student Scholarship " + msg + " Success!");
                            $scope.getPagedSemWiseScholarshipList();

                        } else {
                            $scope.HasError = true;
                            $scope.ErrorMsg = "Sorry you can't " + msg + " Scholarship! " + result.Errors.toString();
                            alertError($scope.ErrorMsg);
                        }
                    }).error(function (result, status) {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Sorry you can't " + msg + " Scholarship! " +
                            JSON.stringify(result).toString() +
                            ". " +
                            "Status: " +
                            JSON.stringify(status).toString();
                        alertError($scope.ErrorMsg);
                    });
            }
        });
    };

    $scope.getTrashIndividualStudentScholarshipById = function (obj, isPutInTrash) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.getTrashIndividualStudentScholarshipByIdUrl, {
                    params: {
                        "id": obj.Id,
                        "isPutInTrash": isPutInTrash
                    }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.SemWiseScholarshipList.indexOf(obj);
                        $scope.SemWiseScholarshipList.splice(i, 1);
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


    $scope.renderHtml = function (html_code) {
        return $sce.trustAsHtml(html_code);
    };
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedSemWiseScholarshipUrl
        ,deleteSemWiseScholarshipByIdUrl
        ,getSemWiseScholarshipListDataExtraUrl
        ,saveSemWiseScholarshipListUrl
        ,getSemWiseScholarshipByIdUrl
        ,getSemWiseScholarshipDataExtraUrl
        ,saveSemWiseScholarshipUrl
        , getLockOrUnlockIndividualStudentScholarshipByIdUrl
        , getTrashIndividualStudentScholarshipByIdUrl
        ) {
        $scope.getPagedSemWiseScholarshipUrl = getPagedSemWiseScholarshipUrl;
        $scope.deleteSemWiseScholarshipByIdUrl = deleteSemWiseScholarshipByIdUrl;
        /*bind extra url if need*/
        $scope.getSemWiseScholarshipListDataExtraUrl = getSemWiseScholarshipListDataExtraUrl;
        $scope.saveSemWiseScholarshipListUrl = saveSemWiseScholarshipListUrl;
        $scope.getSemWiseScholarshipByIdUrl = getSemWiseScholarshipByIdUrl;
        $scope.getSemWiseScholarshipDataExtraUrl = getSemWiseScholarshipDataExtraUrl;
        $scope.saveSemWiseScholarshipUrl = saveSemWiseScholarshipUrl;
        $scope.getLockOrUnlockIndividualStudentScholarshipByIdUrl = getLockOrUnlockIndividualStudentScholarshipByIdUrl;
        $scope.getTrashIndividualStudentScholarshipByIdUrl = getTrashIndividualStudentScholarshipByIdUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



