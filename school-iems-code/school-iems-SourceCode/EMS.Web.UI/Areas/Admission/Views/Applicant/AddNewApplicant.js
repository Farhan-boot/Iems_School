//File: User_Account Anjular  Controller
emsApp.controller('AddNewApplicantCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.Account = [];
    $scope.AccountId = '0'; //201701241753321453
    $scope.EducationId = 0;
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasPreviousUserNameError = false;
    //$scope.UserIdPrefix = "";

    $scope.SelectedAdmissionExam = null;
    $scope.SelectedProgram = null;
    $scope.SelectedFeeCode = null;



    $scope.loadPage = function (AccountId) {
        if (AccountId != null) {
            $scope.AccountId = AccountId;
        }
        $scope.getAccountById($scope.AccountId);
        $scope.getStdScholarshipDataExtra();
    };

    $scope.getNewAccount = function () {
        $scope.getAccountById(0);
    };

    $scope.getAccountById = function (AccountId) {
        if (AccountId != null && AccountId !== '')
            $scope.AccountId = AccountId;
        else return;

        $scope.SelectedAdmissionExam = null;
        $scope.SelectedProgram = null;
        $scope.SelectedFeeCode = null;



        $http.get($scope.getAccountByIdUrl, {
            params: {
                "id": $scope.AccountId
            }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.Account = result.Data;
                updateUrlQuery('id', $scope.Account.Id);

                //if (result.DataExtra.EnrollmentTypeEnumList != null)
                $scope.EnrollmentTypeEnumList = result.DataExtra.EnrollmentTypeEnumList;

                if (result.DataExtra.CampusList != null)
                    $scope.CampusList = result.DataExtra.CampusList;

                if (result.DataExtra.SelectedCampusId != null)
                    $scope.Account.CampusId = result.DataExtra.SelectedCampusId;

                //if (result.DataExtra.ProgramList != null)
                $scope.ProgramList = result.DataExtra.ProgramList;

                $scope.ClassSectionList = result.DataExtra.ClassSectionList;

                //if (result.DataExtra.AdmissionExamList != null)
                $scope.AdmissionExamList = result.DataExtra.AdmissionExamList;

                if ($scope.AdmissionExamList != null && $scope.Account.AdmissionExamId > 0) {
                    var admExam = $filter('filter')($scope.AdmissionExamList, { Id: $scope.Account.AdmissionExamId })[0];
                    $scope.SelectedAdmissionExam = admExam;
                    $scope.onChangeAdmissionExam();
                    //Admission ExamId.     
                }

                if (result.DataExtra.SscEquivalentDegreeCategoryList != null) {
                    $scope.SscEquivalentDegreeCategoryList = result.DataExtra.SscEquivalentDegreeCategoryList;
                }
                if (result.DataExtra.HscEquivalentDegreeCategoryList != null) {
                    $scope.HscEquivalentDegreeCategoryList = result.DataExtra.HscEquivalentDegreeCategoryList;
                }

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Account! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Account! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    //Id = "0",//done
    //UserName = "", /done
    //FullName = "",/done
    //AdmissionExamId = null, done
    //ProgramId = "",done//List
    //EnrollmentTypeEnumId = 0 done,
    //FeeCodeId = 0,//done
    $scope.saveAccount = function () {
        if (!$scope.validateAccount()) {
            return;
        }
        if ($scope.SelectedAdmissionExam == null) {
            alertError("Please Select Admission Circular!");
            return ;
        }
        if ($scope.SelectedAdmissionExam != null) {
            $scope.AdmissionExamId = $scope.SelectedAdmissionExam.Id;
        }
        if ($scope.SelectedProgram != null) {
            $scope.ProgramId = $scope.SelectedProgram.Id;
        }
        if ($scope.SelectedFeeCode != null) {
            $scope.FeeCodeId = $scope.SelectedFeeCode.Id;
        }


        $http.post($scope.saveAccountUrl + "/", $scope.Account).
        success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.Data != null) {
                    $scope.Account = result.Data;
                    updateUrlQuery('id', $scope.Account.Id);
                }
                alertSuccess("New Applicant Successfully Created!");
                $scope.onAfterSaveAccount(result);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).
        error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to Created New Applicant! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getDataExtraOnChangeProgram = function (ProgramId) {
        if (ProgramId != null)
            $scope.Account.ProgramId = ProgramId;


        if ($scope.SelectedAdmissionExam.DefaultSettingsJsonObj != null &&
            $scope.SelectedAdmissionExam.DefaultSettingsJsonObj.ProgramWiseBatchMapJsonList.length > 0 && $scope.SelectedAdmissionExam.DefaultSettingsJsonObj.IsEnableProgramWiseBatchMap) {
            var selectedBatch =
                $scope.SelectedAdmissionExam.DefaultSettingsJsonObj.ProgramWiseBatchMapJsonList.find(
                    x => x.ProgramId == ProgramId);

            if (selectedBatch != null) {
                $scope.Account.ContinuingBatchId = selectedBatch.BatchId;
            }
        }

        $http.get($scope.getDataExtraOnChangeProgramUrl, {
            params: {
                "id": $scope.Account.ProgramId
            }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;

                //if (result.DataExtra.FeeCodeList != null)
                $scope.FeeCodeList = result.DataExtra.FeeCodeList;
                if (result.DataExtra.FeeCodeList != null && result.DataExtra.FeeCodeList.length > 0) {
                    $scope.SelectedFeeCode = $scope.FeeCodeList[0];
                }
                if (result.DataExtra.DeptBatchList != null) {
                    $scope.DeptBatchList = result.DataExtra.DeptBatchList;
                }

                if (result.DataExtra.ContinuingBatchId != null) {
                    $scope.Account.ContinuingBatchId = result.DataExtra.ContinuingBatchId;
                }


                if (result.DataExtra.ClassSectionList != null)
                    $scope.ClassSectionList = result.DataExtra.ClassSectionList;

                if (result.DataExtra.CurriculumList != null)
                    $scope.CurriculumList = result.DataExtra.CurriculumList;

                if (result.DataExtra.SelectedCurriculumId != null)
                    $scope.Account.CurriculumId = result.DataExtra.SelectedCurriculumId;




                $scope.GetMaxAndSuggestUsername();
                $scope.onChangeFeeCode();


            } else {
                $scope.HasError = true;
                $scope.SelectedFeeCode = null;
                $scope.FeeCodeList = null;
                $scope.onChangeFeeCode();
                $scope.ErrorMsg = " Sorry Add new applicant for this Program is not possible! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = " Sorry Add new applicant for this Program is not possible! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.GetMaxAndSuggestUsername = function () {
        if ($scope.Account.ProgramId == null
            || $scope.Account.ProgramId == "" ||
            $scope.Account.AdmissionExamId == null
                || $scope.Account.AdmissionExamId=="") {

            return;
        }
        $http.get($scope.getMaxAndSuggestUsernameUrl, {
            params: {
                "programId": $scope.Account.ProgramId,
                "admissionExamId": $scope.Account.AdmissionExamId
            }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
               
                $scope.Account.MaxUserName = result.DataExtra.MaxUserName;


            } else {
                $scope.HasError = true;
                $scope.SelectedFeeCode = null;

                //$scope.ErrorMsg = " Sorry Add new applicant for this Program is not possible! " + result.Errors.toString();
                //alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            //$scope.ErrorMsg = " Sorry Add new applicant for this Program is not possible! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            //alertError($scope.ErrorMsg);
        });
    };

    $scope.checkEnrollmentType = function() {
        $scope.HasPreviousUserNameError = false;
        $scope.Account.PreviousStudentUserName = null;
    }

    $scope.GetPreviousStudentInformation = function () {
        if ($scope.Account.PreviousStudentUserName == null || $scope.Account.PreviousStudentUserName == '') {
            return;
        }

        $http.get($scope.getPreviousStudentInformationUrl, {
            params: {
                "userName": $scope.Account.PreviousStudentUserName
            }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HasPreviousUserNameError = false;

                $scope.Account.PreviousProgramId = result.Data.PreviousProgramId;
                $scope.Account.GenderEnumId = result.Data.GenderEnumId;
                $scope.Account.CampusId = result.Data.CampusId;
                $scope.Account.MobileNo = result.Data.MobileNo;
                $scope.Account.EmailAddress = result.Data.EmailAddress;
                $scope.Account.FullName = result.Data.FullName;
            } else {
                $scope.HasError = true;
                $scope.HasPreviousUserNameError = true;

                $scope.ErrorMsg = result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            //$scope.ErrorMsg = " Sorry Add new applicant for this Program is not possible! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            //alertError($scope.ErrorMsg);
        });
    };


    $scope.validateAccount = function () {
        return true;
    };



    $scope.onChangeProgram = function () {
        if ($scope.SelectedProgram != null) {
            $scope.Account.ProgramId = $scope.SelectedProgram.Id;
            $scope.cancelSemesterWiseScholarship();
            $scope.getDataExtraOnChangeProgram($scope.SelectedProgram.Id);
            
            $scope.getSemWiseScholarshipDataExtra();

            //$scope.Account.DepartmentId = $scope.SelectedProgram.DepartmentId;
            //$scope.Account.ClassTimingGroupEnumId = $scope.SelectedProgram.ClassTimingGroupEnumId;

        } else {
            $scope.Account.ProgramId = null;
            $scope.Account.DepartmentId = null;
        }
    };
    $scope.onChangeAdmissionExam = function () {
        if ($scope.SelectedAdmissionExam != null) {
            $scope.Account.AdmissionExamId = $scope.SelectedAdmissionExam.Id;
            //$scope.Account.JoiningSemesterId = $scope.SelectedAdmissionExam.SemesterId;
            //$scope.UserIdPrefix
            $scope.GetMaxAndSuggestUsername();

        } else {
            $scope.AdmissionExamId = null;
            $scope.Account.AdmissionExamId = -1;
            //$scope.UserIdPrefix="XXXXXX";
        }
    };
    $scope.onChangeFeeCode = function () {
        if ($scope.SelectedFeeCode != null) {

            $scope.Account.FeeCodeId = $scope.SelectedFeeCode.Id;
        } else {
            $scope.Account.FeeCodeId = null;
        }
    };

   


    //======Custom Variabales goes hare=====

    $scope.Init = function (AccountId,
        getAccountByIdUrl,
        saveAccountUrl,
        getDataExtraOnChangeProgramUrl,
        getStdScholarshipDataExtraUrl,
        getMaxAndSuggestUsernameUrl,
        getSemWiseScholarshipDataExtraUrl,
        getPreviousStudentInformationUrl
    ) {
        $scope.AccountId = AccountId;
        $scope.getAccountByIdUrl = getAccountByIdUrl;
        $scope.saveAccountUrl = saveAccountUrl;
        $scope.getDataExtraOnChangeProgramUrl = getDataExtraOnChangeProgramUrl;

        $scope.getStdScholarshipDataExtraUrl = getStdScholarshipDataExtraUrl;
        $scope.getMaxAndSuggestUsernameUrl = getMaxAndSuggestUsernameUrl;

        $scope.getPreviousStudentInformationUrl = getPreviousStudentInformationUrl;

        $scope.getSemWiseScholarshipDataExtraUrl = getSemWiseScholarshipDataExtraUrl;

        $scope.loadPage(AccountId);
    };

    $scope.onAfterSaveAccount = function (result) {
        //var data=result.Data;
    };
    $scope.onAfterGetAccountById = function (data) {


    };
    $scope.onAfterLoadAccountExtraData = function (result) {

        //Select first AdmissionExam From AdmissionExamList when Create mode
        //if ($scope.Account.Id <= 0 || $scope.Account.Id == null) {
        //    if ($scope.AdmissionExamList.length > 0) {
        //        $scope.SelectedAdmissionExam = $scope.AdmissionExamList[0];
        //        $scope.Account.AdmissionExamId = $scope.AdmissionExamList[0].Id;
        //    }
    }



    //============= Start Package & Discount===========

    $scope.SelectedScholarship = [];
    $scope.SelectedFeeCode = [];
    $scope.SemesterBillOnFixedCost = 0;
    $scope.SemesterBillOnTuitionFee = 0;
    $scope.IsEnableScholarship = false;


    var oldValueInPercentAmount = 0;
    var oldValueInRealAmount = 0;




    $scope.getStdScholarshipDataExtra = function (stdScholarshipId) {
        if (stdScholarshipId != null)
            $scope.StdScholarshipId = stdScholarshipId;

        $http.get($scope.getStdScholarshipDataExtraUrl, {
            params: { "id": $scope.StdScholarshipId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.DataExtra.AmountTypeEnumList != null)
                    $scope.AmountTypeEnumList = result.DataExtra.AmountTypeEnumList;
                if (result.DataExtra.ValidPeriodEnumList != null)
                    $scope.ValidPeriodEnumList = result.DataExtra.ValidPeriodEnumList;

                if (result.DataExtra.AmountInputTypeList != null)
                    $scope.AmountInputTypeList = result.DataExtra.AmountInputTypeList;
                

                if (result.DataExtra.ScholarshipTypeList != null)
                    $scope.ScholarshipTypeList = result.DataExtra.ScholarshipTypeList;

                $scope.NewStdScholarshipJson = result.DataExtra.NewStdScholarshipJson;

                //$scope.onAfterLoadStdScholarshipExtraData(result);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Std Scholarship! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Std Scholarship! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.enableScholarship=function() {
        $scope.IsEnableScholarship = true;
    }
    $scope.cancelScholarship = function () {

        $scope.Account.ScholarshipJsonList = [];
        $scope.IsEnableScholarship = false;
    }


    $scope.onChangeFeeCode = function () {
        if ($scope.SelectedFeeCode != null) {
            $scope.Account.FeeCodeId = $scope.SelectedFeeCode.Id;
        }

    }

    $scope.viewDetailScholarship = function (discount) {
        $scope.SelectedScholarship = discount;
        $('#addDiscountModal').modal('show');
    };

    $scope.addNewScholarship = function () {
        var scholarship = angular.copy($scope.NewStdScholarshipJson);
        $scope.Account.ScholarshipJsonList.push(scholarship);
    };
    $scope.removeScholarship = function (index) {
        $scope.Account.ScholarshipJsonList.splice(index, 1);
    };
    

    $scope.getAllScholarshipByStudentId = function () {

        $http.get($scope.getAllScholarshipByStudentIdUrl,
            {
                params: { "id": $scope.StudentId }
            }).success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;

                    $scope.Account.ScholarshipJsonList = result.Data;

                    //row.IsLocked = !row.IsLocked;

                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Sorry unable to get Scholarship/Discount information! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Sorry unable to get Scholarship/Discount information! " +
                    JSON.stringify(result).toString() +
                    ". " +
                    "Status: " +
                    JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    };
    $scope.saveScholarshipListByStudent = function () {
        if ($scope.Account.ScholarshipJsonList.length < 1) {
            alertError("There is no Scholarship/Discount To Save!");
        }
        $http.post($scope.saveScholarshipListByStudentUrl + "/", $scope.Account).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    $scope.ErrorMsg = "";
                    if (result.Data != null) {

                    }
                    if (result.DataExtra.PerCredit != null) {
                        $scope.Account.PerCreditFee = result.DataExtra.PerCredit;
                    }

                    $scope.getAllScholarshipByStudentId();
                    alertSuccess("Student Scholarship/Discount Successfully Saved!");

                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Student Scholarship/Discount Saving Failed! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Student Scholarship/Discount Saving Failed! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    };
    $scope.lockOrUnlockScholarshipByObj = function (row, isLock) {

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

                $http.get($scope.getLockOrUnlockScholarshipByIdUrl,
                    {
                        params: {
                            "id": $scope.ScholarshipId,
                            "isLock": isLock
                        }
                    }).success(function (result, status) {
                        if (!result.HasError) {
                            $scope.HasError = false;
                            if ($scope.Account.ScholarshipJsonList.length > 0) {
                                var index = $scope.Account.ScholarshipJsonList.indexOf(row);
                                $scope.Account.ScholarshipJsonList[index] = result.Data;
                            }
                            //row.IsLocked = !row.IsLocked;
                            alertSuccess("Student Scholarship/Discount " + msg + " Success!");

                        } else {
                            $scope.HasError = true;
                            $scope.ErrorMsg = "Sorry you can't " + msg + " Discount! " + result.Errors.toString();
                            alertError($scope.ErrorMsg);
                        }
                    }).error(function (result, status) {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Sorry you can't " + msg + " Discount! " +
                            JSON.stringify(result).toString() +
                            ". " +
                            "Status: " +
                            JSON.stringify(status).toString();
                        alertError($scope.ErrorMsg);
                    });
            }
        });
    };
    $scope.scholarshipCalculation = function (row, changeBy) {

        if (changeBy === "PercentAmount") {
            row.ChangeByTextFieldName = changeBy;
        }
        if (changeBy === "RealAmount") {
            row.ChangeByTextFieldName = changeBy;
        }

        
        $scope.percentAndRealAmountCalculation(row);
        
        


    };

    $scope.percentAndRealAmountCalculation = function (row) {

        var tuitionFee = $scope.SelectedFeeCode.PackageTuitionFee;

        var admissionFee = $scope.SelectedFeeCode.FixedCostOnlyAdmissionFee;

        var otherFee = $scope.SelectedFeeCode.FixedCostWithoutAdmissionFee;

      

        
        var realAmount = 0;
        var percentAmount = 0;

        /* Check AmountTypeEnumId
            DiscountOnTutionFee = 1,
            DiscountOnOtherFee = 2,
              DiscountOnAdmissionFee = 3
         */
        if (row.AmountTypeEnumId === 1) // For DiscountOnTutionFee = 1
        {
            if (row.ChangeByTextFieldName === "PercentAmount") {
                realAmount = tuitionFee * (row.PercentAmount / 100);
                row.RealAmount = realAmount.toFixed(3);
            }
            if (row.ChangeByTextFieldName === "RealAmount" && tuitionFee > 0) {
                percentAmount = (row.RealAmount / tuitionFee) * 100;
                row.PercentAmount = percentAmount.toFixed(3);
            }
        }
        if (row.AmountTypeEnumId === 2) // For DiscountOnOtherFee = 2
        {
            if (row.ChangeByTextFieldName === "PercentAmount") {
                realAmount = otherFee * (row.PercentAmount / 100);
                row.RealAmount = realAmount.toFixed(3);
            }
            if (row.ChangeByTextFieldName === "RealAmount" && otherFee > 0) {
                percentAmount = (row.RealAmount / otherFee) * 100;
                row.PercentAmount = percentAmount.toFixed(3);
            }
        }
        if (row.AmountTypeEnumId === 3) // For DiscountOnAdmissionFee = 3
        {
            if (row.ChangeByTextFieldName === "PercentAmount") {
                realAmount = admissionFee * (row.PercentAmount / 100);
                row.RealAmount = realAmount.toFixed(3);
            }
            if (row.ChangeByTextFieldName === "RealAmount" && admissionFee > 0) {
                percentAmount = (row.RealAmount / admissionFee) * 100;
                row.PercentAmount = percentAmount.toFixed(3);
            }
        }

        
    };

    


    $scope.getSemesterBillByStudentIdSemesterId = function (row) {

        // called for calculate discount on this semester only

        $http.get($scope.getSemesterBillByStudentIdSemesterIdUrl,
                {
                    params: {
                        "stdId": $scope.StudentId,
                        "semesterId": row.SemesterId
                    }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;

                        $scope.SemesterBillOnFixedCost = result.DataExtra.SemesterBillOnFixedCost;
                        $scope.SemesterBillOnTuitionFee = result.DataExtra.SemesterBillOnTuitionFee;;

                        $scope.onAfterLoadGetSemesterBillByStudentIdSemesterId(row);

                    } else {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Sorry unable to get this Semester bill information! " + result.Errors.toString();
                        alertError($scope.ErrorMsg);
                    }
                }).error(function (result, status) {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Sorry unable to get this Semester bill information! " +
                        JSON.stringify(result).toString() +
                        ". " +
                        "Status: " +
                        JSON.stringify(status).toString();
                    alertError($scope.ErrorMsg);
                });
    };

    $scope.onAfterLoadGetSemesterBillByStudentIdSemesterId = function (row) {
        $scope.scholarshipCalculation(row, null);

    };

    var isPercentChenge = false;
    $scope.onChangeIsPercent = function (row) {
        /* Check ValidPeriodEnumId
            StartToUpcoming = 1,
            ThisSemesterOnly = 2,
            ThisToUpcoming = 3,

        */

        if (row.IsPercentAmount && row.ValidPeriodEnumId === 2) {

            $scope.getSemesterBillByStudentIdSemesterId(row);

        }

        //todo This condition check another time.
        // ( isPercentChenge !== row.IsPercentAmount ) check for only chenge by Amount in Percent check box.
        //if ($scope.SemesterBillOnFixedCost === 0 && $scope.SemesterBillOnTuitionFee === 0 && isPercentChenge !== row.IsPercent) {

        //    if (row.IsPercentAmount) {
        //        oldValueInRealAmount = row.RealAmount;
        //        row.RealAmount = 0;
        //        row.PercentAmount = oldValueInPercentAmount;
        //    } else {
        //        oldValueInPercentAmount = row.PercentAmount;
        //        row.PercentAmount = 0;
        //        row.RealAmount = oldValueInRealAmount;
        //    }

        //    isPercentChenge = row.IsPercentAmount;
        //}
    };

    $scope.getRecalculateStudentPerCreditFee = function () {

        $http.get($scope.getRecalculateStudentPerCreditFeeUrl, {
            params: { "studentId": $scope.StudentId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.Account.PerCreditFee = result.DataExtra.PerCredit;
                alertSuccess("Recalculate Student PerCredit Fee Successfully!");
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to Recalculate Student PerCredit Fee! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to Recalculate Student PerCredit Fee! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.getRegenerateStudentBillByStdId = function () {
        $http.get($scope.getRegenerateStudentBillByStdIdUrl, {
            params: {
                "stdId": $scope.StudentId
            }
        }).success(function (result) {
            if (!result.HasError) {
                alertSuccess("Regenerate Student Bill Successfully!");

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to Regenerate Student Bill! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to Regenerate Student Bill ! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    //============= End Package & Discount=============



    //==================== Start Semester Wise Waiver =====================
    $scope.IsEnableSemesterWiseScholarship = false;

    $scope.getSemWiseScholarshipDataExtra = function (programId) {
        $http.get($scope.getSemWiseScholarshipDataExtraUrl, {
            params: {
                "programId": $scope.SelectedProgram.Id,
                "semesterDurationEnumId": $scope.SelectedProgram.SemesterDurationEnumId,
                "admissionSemesterId": $scope.SelectedAdmissionExam.SemesterId
            }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.DataExtra.DiscountCategoryEnumList != null)
                    $scope.DiscountCategoryEnumList = result.DataExtra.DiscountCategoryEnumList;
                //DropDown Option Tables
                /*if (result.DataExtra.ProgramList != null)
                    $scope.ProgramList = result.DataExtra.ProgramList;*/

                if (result.DataExtra.SemesterList != null)
                    $scope.SemesterList = result.DataExtra.SemesterList;

                if (result.DataExtra.ParticularNameList != null)
                    $scope.ParticularNameList = result.DataExtra.ParticularNameList;

                /*if (result.DataExtra.StudentList != null)
                    $scope.StudentList = result.DataExtra.StudentList;*/

                if (result.DataExtra.ScholarshipParticularNameList != null)
                    $scope.ScholarshipParticularNameList = result.DataExtra.ScholarshipParticularNameList;


                $scope.NewSemWiseScholarshipJson = result.DataExtra.NewSemWiseScholarshipJson;
                $scope.enableScholarshipForMastersProgram();


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

    $scope.onChangeSscHscGpa = function () {
        //$scope.cancelSemesterWiseScholarship();
        $scope.enableScholarshipForMastersProgram();
        if ($scope.Account.SscGpa != null && $scope.Account.HscGpa != null && $scope.Account.SscGpa > 0 && $scope.Account.HscGpa > 0) {

            log("Ssc:" + $scope.Account.SscGpa + " Hsc:" + $scope.Account.HscGpa);

            $scope.Account.AverageGpa = ($scope.Account.SscGpa + $scope.Account.HscGpa) / 2.0;
            //$scope.enableSemesterWiseScholarship();
            $scope.enableScholarshipForMastersProgram();

        } else {
            $scope.Account.AverageGpa = 0;
            //$scope.cancelSemesterWiseScholarship();
            $scope.enableScholarshipForMastersProgram();
        }

        if ($scope.Account.SscIsGolden) {
            $scope.Account.SscIsGolden = $scope.Account.SscGpa == 5 ? true : false;
            $scope.onChangeSscHscIsGolden();
        }
        if ($scope.Account.HscIsGolden) {
            $scope.Account.HscIsGolden = $scope.Account.HscGpa == 5 ? true : false;
            $scope.onChangeSscHscIsGolden();
        }
    }

    $scope.onChangeSscHscIsGolden = function () {
        $scope.cancelSemesterWiseScholarship();

        if ($scope.Account.SscIsGolden && $scope.Account.HscIsGolden) {
            $scope.Account.AverageIsGolden = true;
        } else {
            $scope.Account.AverageIsGolden = false;
        }

        $scope.enableSemesterWiseScholarship();
    }
    $scope.SemWiseScholarshipListForThisApplicant = [];
    $scope.ScholarshipParticularNameList = [];
    $scope.enableSemesterWiseScholarship = function () {

        $scope.SemWiseScholarshipListForThisApplicant = $scope.ScholarshipParticularNameList
            .filter(x => (x.MinimumBoardAverageGpa <= $scope.Account.AverageGpa &&
                x.MaximumBoardAverageGpa >= $scope.Account.AverageGpa
                && x.IsCheckGoldenBoardResult == $scope.Account.AverageIsGolden) || !x.IsNeedToCheckBoardResult
            );


        if ($scope.SemWiseScholarshipListForThisApplicant.length <= 0) {
           
            return;
        }
        var semWiseScholarshipObj = $scope.SemWiseScholarshipListForThisApplicant[0];
       
            //$filter('filter')($scope.ScholarshipParticularNameList, { Id: id }, true)[0];
        var scholarship = angular.copy(semWiseScholarshipObj.SemWiseScholarshipJson);
       
        $scope.Account.SemWiseScholarshipJsonList.push(scholarship);

        $scope.IsEnableSemesterWiseScholarship = true;
    }
    $scope.cancelSemesterWiseScholarship = function () {

        $scope.Account.SemWiseScholarshipJsonList = [];
        $scope.IsEnableSemesterWiseScholarship = false;
    }

    $scope.IsInternalFreshStudent = false;

    $scope.enableScholarshipForMastersProgram = function () {
        $scope.cancelSemesterWiseScholarship();
        if (!$scope.IsInternalFreshStudent) {
            $scope.enableSemesterWiseScholarship();
        }
        
    }

    $scope.OnChangeInternalFreshStudent = function (isInternalFreshStudent) {
        $scope.IsInternalFreshStudent = isInternalFreshStudent;
        $scope.enableScholarshipForMastersProgram();
    }




    $scope.addNewSemesterWiseScholarship = function () {
        var scholarship = angular.copy($scope.NewSemWiseScholarshipJson);
        $scope.Account.SemWiseScholarshipJsonList.push(scholarship);
    };

    $scope.onChangeScholarshipParticularName = function (id, index) {

        var semWiseScholarshipObj = $filter('filter')($scope.ScholarshipParticularNameList, { Id: id }, true)[0];
        $scope.Account.SemWiseScholarshipJsonList[index] = angular.copy(semWiseScholarshipObj.SemWiseScholarshipJson);

    }


    $scope.removeSemesterWiseScholarship = function (index) {
        $scope.Account.SemWiseScholarshipJsonList.splice(index, 1);
    };

    $scope.semesterWiseScholarshipViewModalOpen = function (studentScholarship) {
        $scope.StudentScholarship = studentScholarship;
        $scope.ViewedScholarshipName = $filter('filter')($scope.ScholarshipParticularNameList, { Id: studentScholarship.NameParticularId }, true)[0].Name;
       
        $('#individualStudentScholarshipViewModal').modal('show');

    };

    //==================== End Semester Wise Waiver =====================




});