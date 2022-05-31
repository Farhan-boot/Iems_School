//File: User_Employee Anjular  Controller
var testData = '';
emsApp.controller('EmployeeAddEditCtrl', function($scope, $http, $rootScope, $filter, cfpLoadingBar) {
    $scope.Employee = [];
    $scope.EmployeeId = 0;
    $scope.ErrorMsg = "";
    $scope.HasError = false;


    $scope.test = function(value) {
        testData = $scope.EmployeeId;
        //$rootScope.$broadcast('test');

        $rootScope.$emit('test', $scope.EmployeeId);
    }

    $scope.loadPage = function(EmployeeId) {
        if (EmployeeId != null)
            $scope.EmployeeId = EmployeeId;

        $scope.loadEmployeeExtraData($scope.EmployeeId);
        $scope.getEmployeeById($scope.EmployeeId);
    };
    $scope.getNewEmployee = function() {
        $scope.getEmployeeById(0);
    };
    $scope.getEmployeeById = function(EmployeeId) {
        if (EmployeeId != null && EmployeeId !== "")
            $scope.EmployeeId = EmployeeId;
        else return;

        $http.get($scope.getEmployeeByIdUrl, {
            params: { "id": $scope.EmployeeId }
        }).success(function(result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.Obj = result.Data;
                updateUrlQuery('id', $scope.Obj.Employee.Id);
                $scope.onAfterGetEmployeeById(result);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Employee! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
            log(result.Data);

        }).error(function(result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Employee! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.loadEmployeeExtraData = function(EmployeeId) {
        if (EmployeeId != null)
            $scope.EmployeeId = EmployeeId;

        $http.get($scope.getEmployeeExtraDataUrl, {
            params: { "id": $scope.EmployeeId }
        }).success(function(result, status) {
            if (!result.HasError) {
                $scope.HasError = false;

                if (result.DataExtra.EmployeeCategoryEnumList != null)
                    $scope.EmployeeCategoryEnumList = result.DataExtra.EmployeeCategoryEnumList;
                if (result.DataExtra.EmployeeTypeEnumList != null)
                    $scope.EmployeeTypeEnumList = result.DataExtra.EmployeeTypeEnumList;
                if (result.DataExtra.JobClassEnumList != null)
                    $scope.JobClassEnumList = result.DataExtra.JobClassEnumList;
                if (result.DataExtra.JobTypeEnumList != null)
                    $scope.JobTypeEnumList = result.DataExtra.JobTypeEnumList;
                if (result.DataExtra.WorkingStatusEnumList != null)
                    $scope.WorkingStatusEnumList = result.DataExtra.WorkingStatusEnumList;
                if (result.DataExtra.IncrementMonthEnumList != null)
                    $scope.IncrementMonthEnumList = result.DataExtra.IncrementMonthEnumList;
                $scope.onAfterLoadEmployeeExtraData(result);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Employee! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }

            log(result);

        }).error(function(result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Employee! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.saveEmployee = function() {
        if (!$scope.validateEmployee()) {
            return;
        }

        //$scope.Obj.Account.DateOfBirth = $filter('date')($scope.Obj.Account.DateOfBirth, $scope.formatDateToSave);
        //$scope.Obj.Employee.JoiningDate = $filter('date')($scope.Obj.Employee.JoiningDate, $scope.formatDateToSave);
        $http.post($scope.saveEmployeeUrl + "/", $scope.Obj).
        success(function(result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.Data != null) {
                    $scope.Obj = result.Data;
                }
                alertSuccess("Successfully saved Employee data!");
                $scope.onAfterSaveEmployee(result);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to save Employee! " + result.Errors.toString();
                //$scope.formateDate();
                alertError($scope.ErrorMsg);
            }
        }).
        error(function(result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to save Employee! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            //$scope.formateDate();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.formateDate = function() {
        //$scope.Obj.Account.DateOfBirth = $filter('date')(new Date($scope.Obj.Account.DateOfBirth), $scope.formatDate);
        //$scope.Obj.Employee.JoiningDate = $filter('date')(new Date($scope.Obj.Employee.JoiningDate), $scope.formatDate);
        //$scope.Obj.AppointmentHistoryList[0].StartDate = $filter('date')(new Date($scope.Obj.AppointmentHistoryList[0].StartDate), $scope.formatDate);
    };

    $scope.deleteEmployeeById = function() {

    };
    $scope.validateEmployee = function() {
        var msg = "";
        //if ($scope.Obj.Account.FullName == null || $scope.Obj.Account.FullName === "") {
        //    msg += "Name can't be blank. ";
        //}
        //if ($scope.Obj.Account.FatherName == null || $scope.Obj.Account.FatherName === "") {
        //    msg += "Father's Name can't be blank. ";
        //}
        //if ($scope.Obj.Account.MotherName == null || $scope.Obj.Account.MotherName === "") {
        //    msg += "Mother's Name can't be blank. ";
        //}
        //if ($scope.Obj.Account.DateOfBirth == null || $scope.Obj.Account.DateOfBirth === "") {
        //    msg += "Date of Birth can't be blank. ";
        //}
        //if ($scope.Obj.Account.Nationality == null || $scope.Obj.Account.Nationality === "") {
        //    msg += "Nationality can't be blank. ";
        //}
        ////if ($scope.Obj.Account.RankId == null || $scope.Obj.Account.RankId <= 0) {
        ////    msg += "Please Select Rank or Designation. ";
        ////}
        //if ($scope.Obj.Account.CampusId == null || $scope.Obj.Account.CampusId <= 0) {
        //    msg += "Please Select Campus. ";
        //}
        //if ($scope.Obj.Employee.JoiningDate == null || $scope.Obj.Employee.JoiningDate === "") {
        //    msg += "Joining Date of this institute can't be blank. ";
        //}
        //if ($scope.Obj.Employee.OldIdNo == null || $scope.Obj.Employee.OldIdNo === "") {
        //    msg += "Institute ID No. can't be blank. ";
        //}
        //for (var i = 0; i < $scope.Obj.AppointmentHistoryList.length; i++) {
        //    if ($scope.Obj.AppointmentHistoryList[i].PositionId == null || $scope.Obj.AppointmentHistoryList[i].PositionId <= 0) {
        //        msg += "Please Select Appointed Post or Position. ";
        //    }
        //    if ($scope.Obj.AppointmentHistoryList[i].DepartmentId == null || $scope.Obj.AppointmentHistoryList[i].DepartmentId <= 0) {
        //        msg += "Please Select Appointed Office or Faculty or Dept. ";
        //    }
        //    if ($scope.Obj.AppointmentHistoryList[i].StartDate == null || $scope.Obj.AppointmentHistoryList[i].StartDate == "") {
        //        msg += "Appointed Date can't be blank. ";
        //    } else {
        //        //$scope.Obj.AppointmentHistoryList[i].StartDate = $filter('date')($scope.Obj.AppointmentHistoryList[i].StartDate, $scope.formatDateToSave);
        //    }
        //}
        //for (var i = 0; i < $scope.Obj.ContactAddressList.length; i++) {
        //    if ($scope.Obj.ContactAddressList[i].AreaInfo == null || $scope.Obj.ContactAddressList[i].AreaInfo == "") {
        //        msg += "Area Name can't be blank. ";
        //    }
        //    if ($scope.Obj.ContactAddressList[i].PostOffice == null || $scope.Obj.ContactAddressList[i].PostOffice == "") {
        //        msg += "Post Office can't be blank. ";
        //    }
        //    if ($scope.Obj.ContactAddressList[i].PoliceStation == null || $scope.Obj.ContactAddressList[i].PoliceStation == "") {
        //        msg += "Police Station can't be blank. ";
        //    }
        //    if ($scope.Obj.ContactAddressList[i].District == null || $scope.Obj.ContactAddressList[i].District == "") {
        //        msg += "District/State/City can't be blank. ";
        //    }
        //}
        if (msg === "") {
            return true;
        } else {
            msg += "Please provide all the required information! ";
            alertWarning(msg);
            return false;
        }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variabales goes hare=====

    $scope.Init = function(EmployeeId, getEmployeeByIdUrl, getEmployeeExtraDataUrl, saveEmployeeUrl, saveApprovalUrl, changePasswordUrl, deleteEmployeeByIdUrl) {
        $scope.EmployeeId = EmployeeId;
        $scope.getEmployeeByIdUrl = getEmployeeByIdUrl;
        $scope.saveEmployeeUrl = saveEmployeeUrl;
        $scope.saveApprovalUrl = saveApprovalUrl;
        $scope.changePasswordUrl = changePasswordUrl;
        $scope.getEmployeeExtraDataUrl = getEmployeeExtraDataUrl;
        $scope.deleteEmployeeByIdUrl = deleteEmployeeByIdUrl;

        $scope.loadPage(EmployeeId);
    };

    $scope.onAfterSaveEmployee = function(result) {
        //var data=result.Data;
    };
    $scope.onAfterGetEmployeeById = function(result) {
        $scope.changeMilitaryCategory();
        //$scope.Obj.Account.DateOfBirth = $filter('date')(new Date($scope.Obj.Account.DateOfBirth), $scope.formatDate);
        //$scope.Obj.Employee.JoiningDate = $filter('date')(new Date($scope.Obj.Employee.JoiningDate), $scope.formatDate);
        //$scope.Obj.AppointmentHistoryList[0].StartDate = $filter('date')(new Date($scope.Obj.AppointmentHistoryList[0].StartDate), $scope.formatDate);
    };
    $scope.onAfterLoadEmployeeExtraData = function(result) {
        if (result.DataExtra.CampusList != null)
            $scope.CampusList = result.DataExtra.CampusList;
        if (result.DataExtra.UserTypeEnumList != null)
            $scope.UserTypeEnumList = result.DataExtra.UserTypeEnumList;
        if (result.DataExtra.CountryList != null)
            $scope.CountryList = result.DataExtra.CountryList;
        if (result.DataExtra.RankList != null) {
            $scope.RankList = result.DataExtra.RankList;
            for (var i = 0; i < $scope.RankList.length; i++) {
                if ($scope.RankList[i].CategoryEnumId == 1) {
                    $scope.RankArmyList.push($scope.RankList[i]);
                } else if ($scope.RankList[i].CategoryEnumId == 2) {
                    $scope.RankNavyList.push($scope.RankList[i]);
                } else if ($scope.RankList[i].CategoryEnumId == 3) {
                    $scope.RankAirForceList.push($scope.RankList[i]);
                } else {
                    $scope.RankCivilList.push($scope.RankList[i]);
                }
            }
        }
        if (result.DataExtra.BankList != null)
            $scope.BankList = result.DataExtra.BankList;
        if (result.DataExtra.UserCategoryEnumList != null)
            $scope.UserCategoryEnumList = result.DataExtra.UserCategoryEnumList;
        if (result.DataExtra.UserStatusEnumList != null)
            $scope.UserStatusEnumList = result.DataExtra.UserStatusEnumList;
        if (result.DataExtra.BloodGroupEnumList != null)
            $scope.BloodGroupEnumList = result.DataExtra.BloodGroupEnumList;
        if (result.DataExtra.GenderEnumList != null)
            $scope.GenderEnumList = result.DataExtra.GenderEnumList;
        if (result.DataExtra.MaritalStatusEnumList != null)
            $scope.MaritalStatusEnumList = result.DataExtra.MaritalStatusEnumList;
        if (result.DataExtra.ReligionEnumList != null)
            $scope.ReligionEnumList = result.DataExtra.ReligionEnumList;

        if (result.DataExtra.DepartmentList != null)
            $scope.DepartmentList = result.DataExtra.DepartmentList;

        if (result.DataExtra.PositionList != null)
            $scope.PositionList = result.DataExtra.PositionList;

        if (result.DataExtra.ContactAddressTypeEnumList != null)
            $scope.ContactAddressTypeEnumList = result.DataExtra.ContactAddressTypeEnumList;
        if (result.DataExtra.ContactEmailTypeEnumList != null)
            $scope.ContactEmailTypeEnumList = result.DataExtra.ContactEmailTypeEnumList;
        if (result.DataExtra.ContactNumberTypeEnumList != null)
            $scope.ContactNumberTypeEnumList = result.DataExtra.ContactNumberTypeEnumList;
        if (result.DataExtra.PrivacyEnumList != null)
            $scope.PrivacyEnumList = result.DataExtra.PrivacyEnumList;

        if (result.DataExtra.AccountList != null)
            $scope.AccountList = result.DataExtra.AccountList;

        if (result.DataExtra.EmployeeList != null)
            $scope.EmployeeList = result.DataExtra.EmployeeList;
    };
    //======Other tabale's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 
    //// Military Category Change
    $scope.changeMilitaryCategory = function() {
        if ($scope.Obj.Account.CategoryEnumId === 1) {
            $scope.RankFilterList = $scope.RankArmyList;
            $scope.Obj.Account.IsMilitary = true;
            if ($scope.Obj.Account.PersonalNo != null) {
                if ($scope.Obj.Account.PersonalNo.length < 4 ||
                    $scope.Obj.Account.PersonalNo == "PN00" ||
                    $scope.Obj.Account.PersonalNo == "BD00") {
                    $scope.Obj.Account.PersonalNo = "BA00";
                }
            } else {
                $scope.Obj.Account.PersonalNo = "BA00";
            }
        } else if ($scope.Obj.Account.CategoryEnumId === 2) {
            $scope.RankFilterList = $scope.RankNavyList;
            $scope.Obj.Account.IsMilitary = true;
            if ($scope.Obj.Account.PersonalNo != null) {
                if ($scope.Obj.Account.PersonalNo.length < 4 ||
                    $scope.Obj.Account.PersonalNo == "BA00" ||
                    $scope.Obj.Account.PersonalNo == "BD00") {
                    $scope.Obj.Account.PersonalNo = "PN00";
                }
            } else {
                $scope.Obj.Account.PersonalNo = "PN00";
            }
        } else if ($scope.Obj.Account.CategoryEnumId === 3) {
            $scope.RankFilterList = $scope.RankAirForceList;
            $scope.Obj.Account.IsMilitary = true;
            if ($scope.Obj.Account.PersonalNo != null) {
                if ($scope.Obj.Account.PersonalNo.length < 4 ||
                    $scope.Obj.Account.PersonalNo == "PN00" ||
                    $scope.Obj.Account.PersonalNo == "BA00") {
                    $scope.Obj.Account.PersonalNo = "BD00";
                }
            } else {
                $scope.Obj.Account.PersonalNo = "BD00";
            }
        } else {
            $scope.RankFilterList = $scope.RankCivilList;
            $scope.Obj.Account.IsMilitary = false;
            if ($scope.Obj.Account.PersonalNo != null) {
                if ($scope.Obj.Account.PersonalNo.length < 3 ||
                    $scope.Obj.Account.PersonalNo == "BA00" ||
                    $scope.Obj.Account.PersonalNo == "PN00" ||
                    $scope.Obj.Account.PersonalNo == "BD00") {
                    $scope.Obj.Account.PersonalNo = "";
                }
            } else {
                $scope.Obj.Account.PersonalNo = "";
            }
        }
    };
    //======Custom property and Functions End========================================================== 
    $scope.formatDate = 'dd/MM/yyyy';
    $scope.formatDateToSave = 'yyyy-MM-dd';
    $scope.RankList = [];
    $scope.RankFilterList = [];
    $scope.RankArmyList = [];
    $scope.RankNavyList = [];
    $scope.RankAirForceList = [];
    $scope.RankCivilList = [];
    $scope.Message = "";
    $scope.IsForceToGenerateUsername = true;
    $scope.IsSendEmailOnApproval = true;

    $scope.saveApproval = function() {
        $scope.IsApproved = !$scope.Obj.Account.IsApproved;
        if ($scope.IsApproved) {
            var msg = "<span  class='text-danger'><i class='fa fa-question-circle'></i> " +
                "Are you sure want to Approved?" +
                "<span >";
        } else {
            var msg = "<span  class='text-danger'><i class='fa fa-question-circle'></i> " +
                "Are you sure want to Disapproved?" +
                "<span >";
        }
        bootbox.confirm(msg, function(ok) {
            if (ok) {
                msg = "";

                $http.post($scope.saveApprovalUrl +
                    "/" + "?userId=" + $scope.Obj.Account.Id +
                    "&isApproved=" + $scope.IsApproved +
                    "&isSendEmailOnApproval=" + $scope.IsSendEmailOnApproval +
                    "&isForceToGenerateUsername=" + $scope.IsForceToGenerateUsername
                ).
                success(function(result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        $scope.Message = "";
                        if (result.DataExtra.UserName != null) {
                            $scope.Obj.Account.UserName = result.DataExtra.UserName;
                        }
                        if (result.DataExtra.Message != null) {
                            $scope.Message = result.DataExtra.Message;
                        }
                        $scope.Obj.Account.IsApproved = $scope.IsApproved;
                        if ($scope.IsApproved) {
                            alertSuccess("Successfully Approved this user!" + $scope.Message);
                        } else {
                            alertSuccess("Successfully Disapproved this user!" + $scope.Message);
                        }
                    } else {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Unable to save Approval! " + result.Errors.toString();
                        alertError($scope.ErrorMsg);
                    }
                }).
                error(function(result, status) {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to save Approval! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                    alertError($scope.ErrorMsg);
                });

            } else {
                //msg = "Thank You for your decision.";
                //toastr.success(msg, 'Operation Recovered!', { positionClass: 'toast-top-center', timeOut: 10000, closeButton: true });
            }
        });
    };
    $scope.Password = null;
    $scope.ConfirmPassword = null;
    $scope.changePassword = function() {
        var msg = "";
        if ($scope.Password == null || $scope.Password == "") {
            msg += "Password can't blank! ";
        } else if ($scope.ConfirmPassword == null || $scope.ConfirmPassword == "") {
            msg += "Confirm Password can't blank! ";
        } else if ($scope.Password.length < 6) {
            msg += "Password should be 6 characters long! ";
        } else if ($scope.ConfirmPassword < 6) {
            msg += "Confirm Password should be 6 characters long! ";
        }
        if (msg !== "") {
            alertWarning(msg);
        } else {
            $http.post($scope.changePasswordUrl +
                "/" + "?userId=" + $scope.Obj.Account.Id +
                "&password=" + $scope.Password +
                "&confirmPassword=" + $scope.ConfirmPassword
            ).
            success(function(result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    $scope.Message = "";
                    if (result.DataExtra.Message != null) {
                        $scope.Message = result.DataExtra.Message;
                    }
                    alertSuccess("Successfully changed password! " + $scope.Message);
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to changed password! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }

            }).
            error(function(result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to changed password! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();

            });
        }
    };
});