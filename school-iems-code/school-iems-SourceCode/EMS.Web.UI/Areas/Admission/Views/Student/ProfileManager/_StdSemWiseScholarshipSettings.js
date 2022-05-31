
//File:Accounts_SemWiseScholarship List Anjular  Controller
emsApp.controller('SemWiseScholarshipListCtrl', function ($scope, $http, $filter, $document, cfpLoadingBar, $sce) {
    $scope.SemWiseScholarshipList = [];
    $scope.SelectedSemWiseScholarship = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
    $scope.SelectedProgramId = 0;
    $scope.SelectedEndSemesterId = 0;
    $scope.SelectedStartSemesterId = 0;
    $scope.SelectedDependOnParticularId = 0;
    $scope.SelectedNameParticularId = 0;
    $scope.SelectedDiscountCategoryEnumId = 0;
    $scope.isShowTrashedScholarship = false;

    $scope.IsLoadSemWiseScholarship = false;

    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 10;
    $scope.PageNo = 1;


    $scope.loadPage = function () {
        $scope.IsLoadSemWiseScholarship = true;
        $scope.getSemWiseScholarshipExtraData();
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
            "pageSize": $scope.PageSize
            , "pageNo": $scope.PageNo
            , "programId": $scope.SelectedProgramId
           , "endSemesterId": $scope.SelectedEndSemesterId
           , "startSemesterId": $scope.SelectedStartSemesterId
           , "dependOnParticularId": $scope.SelectedDependOnParticularId
           , "nameParticularId": $scope.SelectedNameParticularId
           , "studentId": $scope.StudentId
            , "discountCategoryEnumId": $scope.SelectedDiscountCategoryEnumId
                , "isShowTrashedScholarship": $scope.isShowTrashedScholarship
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.SemWiseScholarshipList = result.Data;
                log($scope.SemWiseScholarshipList);
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

    $scope.getSemWiseScholarshipExtraData = function () {
        $http.get($scope.getSemWiseScholarshipDataExtraUrl, {
            params: {
                "stdId": $scope.StudentId
            }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.DataExtra.DiscountCategoryEnumList != null)
                    $scope.DiscountCategoryEnumList = result.DataExtra.DiscountCategoryEnumList;
                //DropDown Option Tables
                if (result.DataExtra.ProgramList != null)
                    $scope.ProgramList = result.DataExtra.ProgramList;

                if (result.DataExtra.SemesterList != null)
                    $scope.SemesterList = result.DataExtra.SemesterList;

                if (result.DataExtra.ParticularNameList != null)
                    $scope.ParticularNameList = result.DataExtra.ParticularNameList;

                if (result.DataExtra.StudentList != null)
                    $scope.StudentList = result.DataExtra.StudentList;

                if (result.DataExtra.ScholarshipParticularNameList != null)
                    $scope.ScholarshipParticularNameList = result.DataExtra.ScholarshipParticularNameList;

                if (result.DataExtra.NewSemWiseScholarshipJson != null)
                    $scope.NewSemWiseScholarshipJson = result.DataExtra.NewSemWiseScholarshipJson;

                if (result.DataExtra.ScholarshipConditionTypeEnumList != null)
                    $scope.ScholarshipConditionTypeEnumList = result.DataExtra.ScholarshipConditionTypeEnumList;

                if (result.DataExtra.ScholarshipTypeEnumList != null)
                    $scope.ScholarshipTypeEnumList = result.DataExtra.ScholarshipTypeEnumList;


            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Sem Wise Scholarship! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Sem Wise Scholarship! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.StudentScholarship = [];
    $scope.individualStudentScholarshipEditModalOpen = function (studentScholarship) {
        $scope.StudentScholarship = studentScholarship;
        $('#individualStudentScholarshipAddEditModal').modal('show');

    };

    $scope.individualStudentScholarshipAddModalOpen = function () {
        $scope.StudentScholarship = angular.copy($scope.NewSemWiseScholarshipJson);
        $('#individualStudentScholarshipAddEditModal').modal('show');

    };

    $scope.individualStudentScholarshipViewModalOpen = function (studentScholarship) {
        $scope.StudentScholarship = studentScholarship;
        $('#individualStudentScholarshipViewModal').modal('show');

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

    $scope.saveTrashIndividualStudentScholarshipListForProfile = function () {

        bootbox.confirm("Are you sure you want to delete selected data?", function (yes) {
            if (yes) {
                $http.post($scope.saveTrashIndividualStudentScholarshipListForProfileUrl + "/", $scope.SemWiseScholarshipList)
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

    $scope.saveUnTrashIndividualStudentScholarshipListForProfile = function () {

        bootbox.confirm("Are you sure you want to Un-delete selected data?", function (yes) {
            if (yes) {
                $http.post($scope.saveUnTrashIndividualStudentScholarshipListForProfileUrl + "/", $scope.SemWiseScholarshipList)
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
    
    $scope.saveLockIndividualStudentScholarshipByIdForProfile = function () {

        bootbox.confirm("Are you sure you want to lock selected data?", function (yes) {
            if (yes) {
                $http.post($scope.saveLockIndividualStudentScholarshipByIdForProfileUrl + "/", $scope.SemWiseScholarshipList)
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

    $scope.saveUnlockIndividualStudentScholarshipByIdForProfile = function () {
        bootbox.confirm("Are you sure you want to unlock selected data?", function (yes) {
            if (yes) {
                $http.post($scope.saveUnlockIndividualStudentScholarshipByIdForProfileUrl + "/", $scope.SemWiseScholarshipList)
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

    $scope.renderHtml = function (html_code) {
        return $sce.trustAsHtml(html_code);
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
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Sem Wise Scholarship list! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
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

    $scope.IsSelectAll = false;
    $scope.selectAll = function () {
        //var checkbox = $event.target;
        for (var i = 0; i < $scope.SemWiseScholarshipList.length; i++) {
            var entity = $scope.SemWiseScholarshipList[i];
            log(entity);
            //if (!entity.IsLocked && !entity.IsDeleted) {
                
            //}
            entity.IsSelected = $scope.IsSelectAll;
        }
    };

    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====


    $scope.Init = function (
            StudentId
            ,getPagedSemWiseScholarshipUrl
            , deleteSemWiseScholarshipByIdUrl
            , getSemWiseScholarshipDataExtraUrl
        , saveSemWiseScholarshipUrl
        , getLockOrUnlockIndividualStudentScholarshipByIdUrl
        , getTrashIndividualStudentScholarshipByIdUrl
        , saveTrashIndividualStudentScholarshipListForProfileUrl
        , saveUnTrashIndividualStudentScholarshipListForProfileUrl
        , saveLockIndividualStudentScholarshipByIdForProfileUrl
        , saveUnlockIndividualStudentScholarshipByIdForProfileUrl
        ) {
        $scope.StudentId = StudentId;
        $scope.getPagedSemWiseScholarshipUrl = getPagedSemWiseScholarshipUrl;
        $scope.deleteSemWiseScholarshipByIdUrl = deleteSemWiseScholarshipByIdUrl;
        $scope.getSemWiseScholarshipDataExtraUrl = getSemWiseScholarshipDataExtraUrl;
        $scope.saveSemWiseScholarshipUrl = saveSemWiseScholarshipUrl;
        $scope.getLockOrUnlockIndividualStudentScholarshipByIdUrl = getLockOrUnlockIndividualStudentScholarshipByIdUrl;
        $scope.getTrashIndividualStudentScholarshipByIdUrl = getTrashIndividualStudentScholarshipByIdUrl;
        $scope.saveTrashIndividualStudentScholarshipListForProfileUrl =
            saveTrashIndividualStudentScholarshipListForProfileUrl;

        $scope.saveUnTrashIndividualStudentScholarshipListForProfileUrl =
            saveUnTrashIndividualStudentScholarshipListForProfileUrl;

        $scope.saveLockIndividualStudentScholarshipByIdForProfileUrl =
            saveLockIndividualStudentScholarshipByIdForProfileUrl;

        $scope.saveUnlockIndividualStudentScholarshipByIdForProfileUrl =
            saveUnlockIndividualStudentScholarshipByIdForProfileUrl;


        //$scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



