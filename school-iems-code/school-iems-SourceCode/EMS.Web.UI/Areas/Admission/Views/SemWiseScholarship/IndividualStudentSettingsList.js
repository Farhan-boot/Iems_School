
//File:Accounts_SemWiseScholarship List Anjular  Controller
emsApp.controller('SemWiseScholarshipListForFirstSemCtrl', function ($scope, $http, $filter, $document, cfpLoadingBar, $sce) {
    $scope.SemWiseScholarshipList = [];
    $scope.SelectedSemWiseScholarship = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
     $scope.SelectedProgramId=0;      
     $scope.SelectedEndSemesterId = 0;
     $scope.SelectedStartSemesterId = "0";
     $scope.SelectedStartSemester=[];      
     $scope.SelectedDependOnParticularId=0;      
     $scope.SelectedNameParticularId=0;      
     $scope.SelectedStudentId = 0;
     $scope.SelectedDiscountCategoryEnumId = 0;
     $scope.Student = null;
     $scope.isShowTrashedScholarship = false;
    //search items
    $scope.UserName = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
        if ($scope.UserName !== "") {
            $scope.getStudentInfoByUserName();
        } else {
            $scope.getSemWiseScholarshipListExtraData();
        }
    };
    /*For Search on data start*/

    $scope.StudentScholarship = [];

    $scope.SelectedStudentInfo = {
        "StudentId": "",
        "StudentFullName":"",
        "StudentUserName": "",
        "StudentProgramId":""
    }

    $scope.individualStudentScholarshipEditModalOpen = function (studentScholarship) {
        log(studentScholarship);
        $scope.mapSelectedStudentInfo(studentScholarship);

        $scope.StudentScholarship = studentScholarship;
        $('#individualStudentScholarshipAddEditModal').modal('show');

    };


    $scope.mapSelectedStudentInfo = function (studentScholarship) {
        $scope.SelectedStudentInfo.StudentId = studentScholarship.StudentId;
        $scope.SelectedStudentInfo.StudentFullName = studentScholarship.StudentFullName;
        $scope.SelectedStudentInfo.StudentUserName = studentScholarship.StudentUserName;
        $scope.SelectedStudentInfo.StudentProgramId = studentScholarship.ProgramId;
    }

    $scope.mapSelectedStudentInfoRemove = function (studentScholarship) {
        $scope.SelectedStudentInfo.StudentId = "";
        $scope.SelectedStudentInfo.StudentFullName = "";
        $scope.SelectedStudentInfo.StudentUserName = "";
        $scope.SelectedStudentInfo.StudentProgramId = "";
    }


    $scope.individualStudentScholarshipAddModalOpen = function () {
        $scope.StudentScholarship = angular.copy($scope.NewSemWiseScholarshipJson);

        $scope.mapSelectedStudentInfo($scope.StudentScholarship);

        $('#individualStudentScholarshipAddEditModal').modal('show');

    };

    $scope.individualStudentScholarshipViewModalOpen = function (studentScholarship) {
        $scope.StudentScholarship = studentScholarship;
        $('#individualStudentScholarshipViewModal').modal('show');

    };

    $scope.getPagedSemWiseScholarshipList = function () {
        updateUrlQuery('un', $scope.UserName);
        $scope.getSemWiseScholarshipList();
    };

    $scope.renderHtml = function (html_code) {
        return $sce.trustAsHtml(html_code);
    };




    $scope.UserNameOnChange = function () {
        $scope.Student = null;
        $scope.SelectedStudentId = 0;
        $scope.mapSelectedStudentInfoRemove();
        $scope.ClearScholarshipList();

    };
    
    $scope.SelectedProgramName = "All Program";
    $scope.onChangeProgram = function () {

        $scope.SelectedProgramName = "All Program";
        $scope.SelectedProgramId = 0;
        if ($scope.SelectedProgram!=null) {
            $scope.SelectedProgramId = $scope.SelectedProgram.Id;
            $scope.SelectedProgramName = $scope.SelectedProgram.Name;
        }
        
    }

    $scope.VerifiedStatusName = "All Eligible(Verified & Unverified)";
    $scope.VerifiedStatusEnumId = 0;
    $scope.onChangeVerifiedStatus = function () {
        $scope.VerifiedStatusName = "All Eligible(Verified & Unverified)";
        $scope.VerifiedStatusEnumId = 0;
        if ($scope.VerifiedStatus != null) {
            $scope.VerifiedStatusEnumId = $scope.VerifiedStatus.Id;
            $scope.VerifiedStatusName = $scope.VerifiedStatus.Name;
        }
    }
    
    $scope.SelectedStartSemesterName = "All Semester";

    $scope.OnChangeStartSemester = function () {
        if ($scope.SelectedStartSemesterId>0) {
            $scope.SelectedStartSemester = $filter('filter')($scope.SemesterList, { Id: $scope.SelectedStartSemesterId }, true)[0];
            $scope.SelectedStartSemesterName = $scope.SelectedStartSemester.Name;
        }
        $scope.SemWiseScholarshipList = null;
    };

    $scope.ClearScholarshipList = function () {
        $scope.SemWiseScholarshipList = null;
    };

    /*For Search on data end*/
    $scope.getSemWiseScholarshipList = function () {
        $http.get($scope.getPagedSemWiseScholarshipUrl, {
            params: {
            "pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
            , "programId": $scope.SelectedProgramId
           ,"endSemesterId": $scope.SelectedEndSemesterId      
           ,"startSemesterId": $scope.SelectedStartSemesterId      
           ,"dependOnParticularId": $scope.SelectedDependOnParticularId      
           ,"nameParticularId": $scope.SelectedNameParticularId      
           , "studentId": $scope.SelectedStudentId
            , "discountCategoryEnumId": $scope.SelectedDiscountCategoryEnumId
            , "isShowTrashedScholarship": $scope.isShowTrashedScholarship
            , "verifiedStatusEnumId": $scope.VerifiedStatusEnumId
            , "startDate": $scope.StartDate
            , "endDate": $scope.EndDate
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.SemWiseScholarshipList = result.Data;
                //log($scope.SemWiseScholarshipList);
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

    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getSemWiseScholarshipList();
    };
    $scope.changePageNo = function () {
        $scope.getSemWiseScholarshipList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getSemWiseScholarshipList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getSemWiseScholarshipList();
    };

    $scope.getStudentInfoByUserName = function () {
        $http.get($scope.getStudentInfoByUserNameUrl, {
            params: {
                "userName": $scope.UserName
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.Student = result.Data;


                $scope.SelectedStudentId = $scope.Student.StudentId;

                $scope.SelectedStudentInfo.StudentId = $scope.Student.StudentId;
                $scope.SelectedStudentInfo.StudentFullName = $scope.Student.FullName;
                $scope.SelectedStudentInfo.StudentUserName = $scope.Student.UserName;
                $scope.SelectedStudentInfo.StudentProgramId = $scope.Student.ProgramId;

                $scope.NewSemWiseScholarshipJson = result.DataExtra.NewSemWiseScholarshipJson;
                $scope.getSemWiseScholarshipListExtraData();
                $scope.getSemWiseScholarshipList();

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Student data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Student Data! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.getSemWiseScholarshipListExtraData= function() {
        var programId = 0;

        if ($scope.SelectedStudentInfo != null && $scope.SelectedStudentInfo.StudentProgramId!="") {
            programId = $scope.SelectedStudentInfo.StudentProgramId;
        }
        $http.get($scope.getSemWiseScholarshipListDataExtraUrl , {
                params: {
                    "programId": programId, 
                    "studentId": $scope.SelectedStudentId
                }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.DiscountCategoryEnumList!=null)
                         $scope.DiscountCategoryEnumList = result.DataExtra.DiscountCategoryEnumList;
                  //DropDown Option Tables
                     if(result.DataExtra.ProgramList!=null)
                       $scope.ProgramList =  result.DataExtra.ProgramList; 

                     
                     if (result.DataExtra.SemesterList != null) {
                         $scope.SemesterList = result.DataExtra.SemesterList;
                         $scope.SelectedStartSemesterId = $scope.SemesterList[0].Id;
                     }


                     if (result.DataExtra.VerifiedStatusEnumList!=null) {
                         $scope.VerifiedStatusEnumList = result.DataExtra.VerifiedStatusEnumList;
                     }
                     
                     

                     if(result.DataExtra.ParticularNameList!=null)
                       $scope.ParticularNameList =  result.DataExtra.ParticularNameList; 

                     if(result.DataExtra.StudentList!=null)
                         $scope.StudentList = result.DataExtra.StudentList;

                     if (result.DataExtra.ScholarshipParticularNameList != null)
                         $scope.ScholarshipParticularNameList = result.DataExtra.ScholarshipParticularNameList;

                     if (result.DataExtra.ScholarshipConditionTypeEnumList != null)
                         $scope.ScholarshipConditionTypeEnumList = result.DataExtra.ScholarshipConditionTypeEnumList;

                     if (result.DataExtra.ScholarshipTypeEnumList != null)
                         $scope.ScholarshipTypeEnumList = result.DataExtra.ScholarshipTypeEnumList;



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

    $scope.onChangeScholarshipParticularName = function (id) {

        var semWiseScholarshipObj = $filter('filter')($scope.ScholarshipParticularNameList, { Id: id }, true)[0];


        $scope.StudentScholarship = angular.copy(semWiseScholarshipObj.SemWiseScholarshipJson);

        $scope.StudentScholarship.StudentId = $scope.SelectedStudentInfo.StudentId;
        $scope.StudentScholarship.StudentFullName = $scope.SelectedStudentInfo.StudentFullName;
        $scope.StudentScholarship.StudentUserName = $scope.SelectedStudentInfo.StudentUserName;
        $scope.StudentScholarship.ProgramId = $scope.SelectedStudentInfo.StudentProgramId;
    }


    $scope.deleteSemWiseScholarshipById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to permanently delete this data?", function (yes) {
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

    $scope.getLockOrUnlockIndividualStudentScholarshipById = function (row, isLock) {

        if (row != null) {
            $scope.ScholarshipId = row.Id;
        } else {
            return;
        }
        var msg = "";
        if (isLock) {
            msg = "Verified";
        } else {
            msg = "Unverified";
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

    $scope.saveSemWiseScholarship = function () {
        
        $http.post($scope.saveSemWiseScholarshipUrl + "/", $scope.StudentScholarship).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    if (result.Data != null) {
                        $scope.StudentScholarship = result.Data;
                        //updateUrlQuery('id', $scope.SemWiseScholarship.Id);
                    }
                    alertSuccess("Successfully saved Sem Wise Scholarship data!");
                    $scope.getPagedSemWiseScholarshipList();
                    //$scope.onAfterSaveSemWiseScholarship(result);
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to save Sem Wise Scholarship! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to save Sem Wise Scholarship! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    };


    $scope.getApplyOrCancelScholarshipForIndividualStudentByScholarshipId = function (row, isApply) {

        var studentId = 0;
        if (row != null) {
            $scope.ScholarshipId = row.Id;
            studentId = row.StudentId;
        } else {
            return;
        }
        var msg = "Apply";
        if (!isApply) {
            msg = "Cancel";
        }
        bootbox.confirm("Are you sure you want to " + msg + " this data?", function (yes) {
            if (yes) {
                log(isApply);
                $http.get($scope.getApplyOrCancelScholarshipForIndividualStudentByScholarshipIdUrl,
                    {
                        params: {
                            "studentId": studentId,
                            "scholarshipId": $scope.ScholarshipId,
                            "isApply":isApply
                        }
                    }).success(function (result, status) {
                        if (!result.HasError) {
                            $scope.HasError = false;

                            alertSuccess("Student Scholarship " + msg + " Success!");
                            $scope.getSemWiseScholarshipList();

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


    $scope.IsSelectAll = false;
    $scope.selectAll = function (isSelectAll) {
        $scope.IsSelectAll = isSelectAll;
        //$scope.nonAffectedScholarshipList = $filter('filter')($scope.SemWiseScholarshipList, { IsAffected: false }, true);
        for (var i = 0; i < $scope.SemWiseScholarshipList.length; i++) {
            var entity = $scope.SemWiseScholarshipList[i];
            entity.IsSelected = $scope.IsSelectAll;
        }
    };

    $scope.IsSelectAllForAffectedList = false;
    $scope.selectAllForAffectedList = function () {

        $scope.affectedScholarshipList = $filter('filter')($scope.SemWiseScholarshipList, { IsAffected: true }, true);

        for (var i = 0; i < $scope.affectedScholarshipList.length; i++) {
            var entity = $scope.affectedScholarshipList[i];
            entity.IsSelected = $scope.IsSelectAllForAffectedList;
        }
    };


    $scope.saveTrashIndividualStudentScholarshipListForGlobal = function () {

        bootbox.confirm("Are you sure you want to delete selected data?", function (yes) {
            if (yes) {
                $http.post($scope.saveTrashIndividualStudentScholarshipListForGlobalUrl + "/", $scope.SemWiseScholarshipList)
                    .success(function (result, status) {
                        if (!result.HasError) {
                            $scope.HasError = false;
                            $scope.getSemWiseScholarshipList();
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

    $scope.saveUnTrashIndividualStudentScholarshipListForGlobal = function () {

        bootbox.confirm("Are you sure you want to Un-delete selected data?", function (yes) {
            if (yes) {
                $http.post($scope.saveUnTrashIndividualStudentScholarshipListForGlobalUrl + "/", $scope.SemWiseScholarshipList)
                    .success(function (result, status) {
                        if (!result.HasError) {
                            $scope.HasError = false;
                            $scope.getSemWiseScholarshipList();
                            alertSuccess("Data successfully un-deleted!");
                        } else {
                            $scope.HasError = true;
                            $scope.ErrorMsg = "Sorry unable to un-delete! " + result.Errors.toString();
                            alertError($scope.ErrorMsg);
                        }
                    }).error(function (result, status) {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Sorry unable to un-delete! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
                        alertError($scope.ErrorMsg);
                    });
            }
        });
    };

    $scope.saveLockIndividualStudentScholarshipByIdForGlobal = function () {

        bootbox.confirm("Are you sure you want to verified selected data?", function (yes) {
            if (yes) {
                $scope.SelectedScholarshipList = $filter('filter')($scope.SemWiseScholarshipList, { IsSelected: true }, true);
                $http.post($scope.saveLockIndividualStudentScholarshipByIdForGlobalUrl + "/", $scope.SelectedScholarshipList)
                    .success(function (result, status) {
                        if (!result.HasError) {
                            $scope.HasError = false;
                            $scope.getSemWiseScholarshipList();
                            alertSuccess("Data successfully lock!");
                        } else {
                            $scope.HasError = true;
                            $scope.ErrorMsg = "Sorry unable to lock! " + result.Errors.toString();
                            alertError($scope.ErrorMsg);
                        }
                    }).error(function (result, status) {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Sorry unable to lock! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
                        alertError($scope.ErrorMsg);
                    });
            }
        });
    };

    $scope.saveUnlockIndividualStudentScholarshipByIdForGlobal = function () {
        bootbox.confirm("Are you sure you want to unverified selected data?", function (yes) {
            if (yes) {
                $scope.SelectedScholarshipList = $filter('filter')($scope.SemWiseScholarshipList, { IsSelected: true }, true);
                $http.post($scope.saveUnlockIndividualStudentScholarshipByIdForGlobalUrl + "/", $scope.SelectedScholarshipList)
                    .success(function (result, status) {
                        if (!result.HasError) {
                            $scope.HasError = false;
                            $scope.getSemWiseScholarshipList();
                            alertSuccess("Data successfully lock!");
                        } else {
                            $scope.HasError = true;
                            $scope.ErrorMsg = "Sorry unable to unlock! " + result.Errors.toString();
                            alertError($scope.ErrorMsg);
                        }
                    }).error(function (result, status) {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Sorry unable to unlock! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
                        alertError($scope.ErrorMsg);
                    });
            }
        });
    };


    $scope.saveMarkAsApplyScholarshipForIndividualStudentForGlobal = function () {

        bootbox.confirm("Are you sure you want to apply selected data?", function (yes) {
            if (yes) {

                $scope.SelectedScholarshipList = $filter('filter')($scope.SemWiseScholarshipList, { IsSelected: true }, true);

                $http.post($scope.saveMarkAsApplyScholarshipForIndividualStudentForGlobalUrl + "/", $scope.SelectedScholarshipList)
                    .success(function (result, status) {
                        if (!result.HasError) {
                            $scope.HasError = false;
                            $scope.getSemWiseScholarshipList();
                            alertSuccess("Data successfully apply!");
                        } else {
                            $scope.HasError = true;
                            $scope.ErrorMsg = "Sorry unable to apply! " + result.Errors.toString();
                            alertError($scope.ErrorMsg);
                        }
                    }).error(function (result, status) {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Sorry unable to apply! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
                        alertError($scope.ErrorMsg);
                    });
            }
        });
    };

   

    $scope.getScholarshipCreateFromSscAndHscResultByStudentId = function () {
        
        var msg = "create";
        bootbox.confirm("Are you sure you want to create scholarship by Ssc by Hsc result data?", function (yes) {
            if (yes) {

                $http.get($scope.getScholarshipCreateFromSscAndHscResultByStudentIdUrl,
                    {
                        params: {
                            "studentId": $scope.SelectedStudentId
                        }
                    }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;

                        alertSuccess("Student Scholarship " + msg + " Success!");
                        $scope.getSemWiseScholarshipList();

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
    
    

    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
        userName
         ,getPagedSemWiseScholarshipUrl
        ,deleteSemWiseScholarshipByIdUrl
        ,getSemWiseScholarshipListDataExtraUrl
        ,saveSemWiseScholarshipListUrl
        ,getSemWiseScholarshipByIdUrl
        ,getSemWiseScholarshipDataExtraUrl
        , saveSemWiseScholarshipUrl
        , getStudentInfoByUserNameUrl
        , getTrashIndividualStudentScholarshipByIdUrl
        , getLockOrUnlockIndividualStudentScholarshipByIdUrl
        ,saveLockIndividualStudentScholarshipByIdForGlobalUrl
        ,saveUnlockIndividualStudentScholarshipByIdForGlobalUrl
        ,saveTrashIndividualStudentScholarshipListForGlobalUrl
        , saveUnTrashIndividualStudentScholarshipListForGlobalUrl
        , getApplyOrCancelScholarshipForIndividualStudentByScholarshipIdUrl
        , saveMarkAsApplyScholarshipForIndividualStudentForGlobalUrl
        , getScholarshipCreateFromSscAndHscResultByStudentIdUrl
        ) {
        $scope.UserName = userName;
        $scope.getPagedSemWiseScholarshipUrl = getPagedSemWiseScholarshipUrl;
        $scope.deleteSemWiseScholarshipByIdUrl = deleteSemWiseScholarshipByIdUrl;
        /*bind extra url if need*/
        $scope.getSemWiseScholarshipListDataExtraUrl = getSemWiseScholarshipListDataExtraUrl;
        $scope.saveSemWiseScholarshipListUrl = saveSemWiseScholarshipListUrl;
        $scope.getSemWiseScholarshipByIdUrl = getSemWiseScholarshipByIdUrl;
        $scope.getSemWiseScholarshipDataExtraUrl = getSemWiseScholarshipDataExtraUrl;
        $scope.saveSemWiseScholarshipUrl = saveSemWiseScholarshipUrl;
        $scope.getStudentInfoByUserNameUrl = getStudentInfoByUserNameUrl;
        $scope.getTrashIndividualStudentScholarshipByIdUrl = getTrashIndividualStudentScholarshipByIdUrl;
        $scope.getLockOrUnlockIndividualStudentScholarshipByIdUrl = getLockOrUnlockIndividualStudentScholarshipByIdUrl;

        $scope.saveLockIndividualStudentScholarshipByIdForGlobalUrl =
            saveLockIndividualStudentScholarshipByIdForGlobalUrl;
        $scope.saveUnlockIndividualStudentScholarshipByIdForGlobalUrl =
            saveUnlockIndividualStudentScholarshipByIdForGlobalUrl;
        $scope.saveTrashIndividualStudentScholarshipListForGlobalUrl =
                saveTrashIndividualStudentScholarshipListForGlobalUrl,
            $scope.saveUnTrashIndividualStudentScholarshipListForGlobalUrl =
                saveUnTrashIndividualStudentScholarshipListForGlobalUrl;

        $scope.getApplyOrCancelScholarshipForIndividualStudentByScholarshipIdUrl =
            getApplyOrCancelScholarshipForIndividualStudentByScholarshipIdUrl;

        $scope.saveMarkAsApplyScholarshipForIndividualStudentForGlobalUrl =
            saveMarkAsApplyScholarshipForIndividualStudentForGlobalUrl;

        $scope.getScholarshipCreateFromSscAndHscResultByStudentIdUrl =
            getScholarshipCreateFromSscAndHscResultByStudentIdUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



