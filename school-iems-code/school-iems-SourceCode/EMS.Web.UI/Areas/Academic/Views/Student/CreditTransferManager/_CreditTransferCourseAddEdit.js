
//File: Aca_CreditTransferInstitute Anjular  Controller
emsApp.controller('CreditTransferManagerCtrl', function ($scope, $http, $filter, cfpLoadingBar) {

    $scope.CreditTransferList = [];
    $scope.CreditTransferId = 0;
    $scope.StudentId = 0;
    $scope.StudentName = "";
    $scope.UserName = "";
    $scope.AccountId = 0;
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
    $scope.SelectedCreditTransfer = [];
    $scope.SelectedGrade = null;
    

    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;


    $scope.loadPage = function (accountId) {
        if (accountId != null)
            $scope.AccountId = accountId;

        $scope.loadCreditTransferDataExtra();
        $scope.getCreditTransferAndCourseListByAcntId($scope.AccountId);
    };


    $scope.getNewCreditTransfer = function () {
        $scope.getCreditTransferById(0);
    };

    $scope.getCreditTransferById = function (CreditTransferId) {
        if (CreditTransferId != null && CreditTransferId !== '')
            $scope.CreditTransferId = CreditTransferId;
        else return;

        $http.get($scope.getCreditTransferByIdUrl, {
            params: {
                "id": $scope.CreditTransferId,
                "stdId": $scope.StudentId
            }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.CreditTransfer = result.Data;
                //updateUrlQuery('id', $scope.CreditTransfer.Id);
                //$scope.onAfterGetCreditTransferById(result);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Credit Transfer! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Credit Transfer! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.getCreditTransferAndCourseListByAcntId = function (accountId) {
        if (accountId != null && accountId !== '')
            $scope.AccountId = accountId;
        else return;

        $http.get($scope.getCreditTransferAndCourseListByAcntIdUrl, {
            params: { "aId": $scope.AccountId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.CreditTransferList = result.Data;
                if ($scope.CreditTransferList == null || $scope.CreditTransferList.length === 0) {
                    $scope.getCreditTransferById(0);
                }

                $scope.StudentId = result.DataExtra.StudentId;
                $scope.StudentName = result.DataExtra.StudentName;
                $scope.UserName = result.DataExtra.UserName;
                $scope.CurriculumCourseList = result.DataExtra.CurriculumCourseList;


                if ($scope.CreditTransferList!=null && $scope.CreditTransferList.length > 0) {
                    $scope.CreditTransfer = $scope.CreditTransferList[0]; //For first time first Credit Transfer selected and edit mode.
                    $scope.selectedCreditTransferIndex = 0;
                }
                

                $scope.CreditTransferCoursesList = result.DataExtra.CreditTransferCoursesList;

                //updateUrlQuery('stdId', $scope.CreditTransfer.Id);
                $scope.onAfterGetCreditTransferById(result);
                
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Credit Transfer! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Credit Transfer! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };


    /*Edit Credit Transfer*/
    $scope.editCreditTransfer = function ($index) {
        $scope.CreditTransfer = $scope.CreditTransferList[$index];
        $scope.selectedCreditTransferIndex = $index;
    };

    $scope.loadCreditTransferDataExtra = function () {
        

        $http.get($scope.getCreditTransferDataExtraUrl, {
            
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.DataExtra.CreditTransferInstituteList != null)
                    $scope.CreditTransferInstituteList = result.DataExtra.CreditTransferInstituteList;


                if (result.DataExtra.GradingPolicyOptionList != null)
                    $scope.GradingPolicyOptionList = result.DataExtra.GradingPolicyOptionList;

                
                $scope.onAfterLoadCreditTransferDataExtra(result);
                
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Credit Transfer! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Credit Transfer! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.saveCreditTransfer = function () {
        if (!$scope.validateCreditTransfer()) {
            return;
        }
        $http.post($scope.saveCreditTransferUrl + "/", $scope.CreditTransfer).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    if (result.Data != null) {
                        $scope.CreditTransfer = result.Data;
                        $scope.getCreditTransferAndCourseListByAcntId($scope.AccountId);
                        //updateUrlQuery('id', $scope.CreditTransfer.Id);
                    }
                    alertSuccess("Successfully saved Credit Transfer data!");
                    $scope.onAfterSaveCreditTransfer(result);
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to save Credit Transfer! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to save Credit Transfer! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    };

    $scope.deleteCreditTransferById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this Credit Transfer?", function (yes) {
            if (yes) {
                $http.get($scope.deleteCreditTransferByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.CreditTransferList.indexOf(obj);
                        $scope.CreditTransferList.splice(i, 1);

                        if ($scope.CreditTransferList != null && $scope.CreditTransferList.length > 0) {
                            $scope.CreditTransfer = $scope.CreditTransferList[0]; //For first time first Credit Transfer selected and edit mode.
                            $scope.selectedCreditTransferIndex = 0;
                        }
                        alertSuccess("Credit Transfer successfully deleted!");
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
    $scope.validateCreditTransfer = function () {
        return true;
    };
    // ============= Start Credit Transfer Courses ========
    $scope.getNewTransferredCourseModal = function () {
        $scope.getCreditTransferCoursesById(0);

        $('#TransferredCourseModal').modal('show');
    }

    $scope.getEditTransferredCourseModal = function (id) {
        $scope.getCreditTransferCoursesById(id);

        $('#TransferredCourseModal').modal('show');
    }

    $scope.getCreditTransferCoursesById = function (CreditTransferCoursesId) {
        if (CreditTransferCoursesId != null && CreditTransferCoursesId !== '')
            $scope.CreditTransferCoursesId = CreditTransferCoursesId;
        else return;

        $http.get($scope.getCreditTransferCoursesByIdUrl, {
            params: {
                "id": $scope.CreditTransferCoursesId,
                "stdId": $scope.StudentId
            }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.CreditTransferCourses = result.Data;
                
                //updateUrlQuery('id', $scope.CreditTransferCourses.Id);
                //$scope.onAfterGetCreditTransferCoursesById(result);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Credit Transfer Courses! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Credit Transfer Courses! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.saveCreditTransferCourses = function () {
        //if (!$scope.validateCreditTransferCourses()) {
        //    return;
        //}
        $http.post($scope.saveCreditTransferCoursesUrl + "/", $scope.CreditTransferCourses).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    if (result.Data != null) {
                        $scope.CreditTransferCourses = result.Data;
                        $scope.getCreditTransferAndCourseListByAcntId($scope.AccountId);
                        
                        $('#TransferredCourseModal').modal('hide');
                        //updateUrlQuery('id', $scope.CreditTransferCourses.Id);
                    }
                    alertSuccess("Successfully saved Credit Transfer Courses data!");
                    //$scope.onAfterSaveCreditTransferCourses(result);
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to save Credit Transfer Courses! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to save Credit Transfer Courses! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    };

    $scope.getDeleteCreditTransferCoursesById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this Credit Transfer Course?", function (yes) {
            if (yes) {
                $http.get($scope.getDeleteCreditTransferCoursesByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        $scope.getCreditTransferAndCourseListByAcntId($scope.AccountId);
                        //var i = $scope.CreditTransferCoursesList.indexOf(obj);
                        //$scope.CreditTransferCoursesList.splice(i, 1);
                        alertSuccess("Credit Transfer Course successfully deleted!");
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

    $scope.creditTransferCoursesListFilter = function () {
        if ($scope.SelectedCreditTransfer==null) {
            $scope.getCreditTransferCoursesList(0);
        } else {
            $scope.getCreditTransferCoursesList($scope.SelectedCreditTransfer.Id);
        }
        
    };

    $scope.getCreditTransferCoursesList = function (id) {
        $http.get($scope.getPagedCreditTransferCoursesUrl, {
            params: {
                "textkey": $scope.SearchText
                , "pageSize": $scope.PageSize
                , "pageNo": $scope.PageNo
                , "stdId": $scope.StudentId
                , "creditTransfarId": id
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.CreditTransferCoursesList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Credit Transfer Courses list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Credit Transfer Courses list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.onChangeGrade = function (grade) {
        var selectedGrade = $filter('filter')($scope.GradingPolicyOptionList, { Grade: grade }, true)[0];

        if (selectedGrade != null) {
            $scope.CreditTransferCourses.AcceptedGradePoint = selectedGrade.GradePoint;
        } else {
            $scope.CreditTransferCourses.AcceptedGradePoint = 0;
        }
        
    };
    $scope.onChangeCurriculumCourse = function (id) {
        
        var selectedCurriculumCourse = $filter('filter')($scope.CurriculumCourseList, { Id: id }, true)[0];
        if (selectedCurriculumCourse!=null) {
            $scope.CreditTransferCourses.AcceptedCourseCredit = selectedCurriculumCourse.CreditLoad;
        } else {
            $scope.CreditTransferCourses.AcceptedCourseCredit = 0;
        }
        
        
    };

    
   
    //=========End Credit Transfer Courses==========

    //======Custom property and Functions Start=======================================================  
    //======Custom Variabales goes hare=====

    $scope.Init = function (
        AccountId,
        StudentId,
        getCreditTransferAndCourseListByAcntIdUrl,
        getCreditTransferDataExtraUrl,
        getCreditTransferCoursesByIdUrl,
        saveCreditTransferCoursesUrl,
        getDeleteCreditTransferCoursesByIdUrl,
        saveCreditTransferUrl,
        getCreditTransferByIdUrl,
        deleteCreditTransferByIdUrl,
        getPagedCreditTransferCoursesUrl
        
    )
    {
        $scope.AccountId = AccountId;
        $scope.StudentId = StudentId;
        $scope.getCreditTransferAndCourseListByAcntIdUrl = getCreditTransferAndCourseListByAcntIdUrl;
        $scope.getCreditTransferDataExtraUrl = getCreditTransferDataExtraUrl;
        $scope.getCreditTransferCoursesByIdUrl = getCreditTransferCoursesByIdUrl;
        $scope.saveCreditTransferCoursesUrl = saveCreditTransferCoursesUrl;
        $scope.getDeleteCreditTransferCoursesByIdUrl = getDeleteCreditTransferCoursesByIdUrl;
        $scope.saveCreditTransferUrl = saveCreditTransferUrl;
        $scope.getCreditTransferByIdUrl = getCreditTransferByIdUrl;
        $scope.deleteCreditTransferByIdUrl = deleteCreditTransferByIdUrl;
        $scope.getPagedCreditTransferCoursesUrl = getPagedCreditTransferCoursesUrl;

        $scope.loadPage(AccountId);
    };

    $scope.onAfterSaveCreditTransfer = function (result) {
        //var data=result.Data;
    };
    $scope.onAfterGetCreditTransferById = function (result) {
        //var data=result.Data;

    };
    $scope.onAfterLoadCreditTransferDataExtra = function (result) {
        //var DataExtra=result.DataExtra;
        //DropDown Option Tables
        if (result.DataExtra.StudentList != null)
            $scope.StudentList = result.DataExtra.StudentList;
        /*
        //Child Table Bindings, need to fix     */
    };
    //======Other tabale's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 



});

