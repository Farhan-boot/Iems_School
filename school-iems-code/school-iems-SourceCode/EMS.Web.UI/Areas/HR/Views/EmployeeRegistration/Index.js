emsApp.controller('EmployeeRegistrationCtrl', function($scope, $http, $filter) {
    $scope.formatDate = 'dd/MM/yyyy';
    $scope.formatDateToSave = 'yyyy-MM-dd';
    $scope.Obj = [];
    $scope.EmployeeCategoryEnumList = [];
    $scope.EmployeeTypeEnumList = [];
    $scope.ServiceStatusEnumList = [];
    $scope.ServiceTypeEnumList = [];
    $scope.UserStatusEnumList = [];
    $scope.RankList = [];
    $scope.RankFilterList = [];
    $scope.RankArmyList = [];
    $scope.RankNavyList = [];
    $scope.RankAirForceList = [];
    $scope.RankCivilList = [];
    $scope.IsLoading = 0;
    $scope.IsSameAddress = false;
    $scope.IsPresentAddressMIST = false;
    /*Initialize*/
    $scope.Init = function(
        getEmployeeUrl,
        getEmployeeDataExtraUrl,
        getEmployeeAppointmentHistoryUrl,
        saveEmployeeUrl) {
        $scope.IsLoading++;
        $scope.getEmployeeUrl = RootApiUrl + getEmployeeUrl;
        $scope.getEmployeeDataExtraUrl = RootApiUrl + getEmployeeDataExtraUrl;
        $scope.getEmployeeAppointmentHistoryUrl = RootApiUrl + getEmployeeAppointmentHistoryUrl;
        $scope.saveEmployeeUrl = RootApiUrl + saveEmployeeUrl;
        $scope.getEmployeeDataExtra();
        $scope.getEmployee();
        $scope.IsLoading--;
    };
    /*Get Employee*/
    $scope.getEmployee = function() {
        $scope.IsLoading++;
        $http.get($scope.getEmployeeUrl)
            .success(function(result) {
                if (!result.HasError) {
                    $scope.Obj = result.Data;
                    $scope.changeMilitaryCategory();
                    //$scope.Obj.Account.DateOfBirth = $filter('date')($scope.Obj.Account.DateOfBirth, $scope.formatDate);
                    //$scope.Obj.Employee.JoiningDate = $filter('date')($scope.Obj.Employee.JoiningDate, $scope.formatDate);
                    //$scope.Obj.AppointmentHistoryList[0].StartDate = $filter('date')($scope.Obj.AppointmentHistoryList[0].StartDate, $scope.formatDate);
                    $scope.IsLoading--;
                    log(result);
                } else {
                    $scope.IsLoading--;
                    toastr.error("Please try again later to get Data! " + result.Errors.toString(), "Oops. Something went wrong. ", { positionClass: "toast-top-center", timeOut: 10000, closeButton: true });
                }
            })
            .error(function(result) {
                $scope.IsLoading--;
                toastr.error("Please try again later to get Data!", "Oops. Something went wrong. ", { positionClass: "toast-top-center", timeOut: 10000, closeButton: true });
            });
    };

    $scope.changeFullName = function() {
        $scope.Obj.ContactAddressList[0].CareOfPersonName = $scope.Obj.Account.FullName;
    };

    //// Military Category Change
    $scope.changeMilitaryCategory = function() {
        $scope.IsLoading++;
        if ($scope.Obj.Account.CategoryEnumId === 1) {
            $scope.RankFilterList = $scope.RankArmyList;
            $scope.Obj.Account.IsMilitary = true;
            $scope.Obj.Account.PersonalNo = "BA00";
        } else if ($scope.Obj.Account.CategoryEnumId === 2) {
            $scope.RankFilterList = $scope.RankNavyList;
            $scope.Obj.Account.IsMilitary = true;
            $scope.Obj.Account.PersonalNo = "PN00";
        } else if ($scope.Obj.Account.CategoryEnumId === 3) {
            $scope.RankFilterList = $scope.RankAirForceList;
            $scope.Obj.Account.IsMilitary = true;
            $scope.Obj.Account.PersonalNo = "BD00";
        } else {
            $scope.RankFilterList = $scope.RankCivilList;
            $scope.Obj.Account.IsMilitary = false;
            $scope.Obj.Account.PersonalNo = "";
        }
        $scope.IsLoading--;
    };
    $scope.changeJoiningDate = function() {
        for (var i = 0; i < $scope.Obj.AppointmentHistoryList.length; i++) {
            if ($scope.Obj.AppointmentHistoryList[i].IsPrimary) {
                $scope.Obj.AppointmentHistoryList[i].StartDateString = $scope.Obj.Employee.JoiningDateString;
            }
        }
    };
    $scope.changeDept = function() {
        for (var i = 0; i < $scope.Obj.AppointmentHistoryList.length; i++) {
            if ($scope.Obj.AppointmentHistoryList[i].IsPrimary) {
                $scope.Obj.AppointmentHistoryList[i].DepartmentId = $scope.Obj.Employee.DepartmentId;
            }
        }
    };
    $scope.chnageAddress = function() {
        if ($scope.IsSameAddress) {
            $scope.Obj.ContactAddressList[1].CareOfPersonName = $scope.Obj.ContactAddressList[0].CareOfPersonName;
            $scope.Obj.ContactAddressList[1].AppartmentNo = $scope.Obj.ContactAddressList[0].AppartmentNo;
            $scope.Obj.ContactAddressList[1].HouseNo = $scope.Obj.ContactAddressList[0].HouseNo;
            $scope.Obj.ContactAddressList[1].RoadNo = $scope.Obj.ContactAddressList[0].RoadNo;
            $scope.Obj.ContactAddressList[1].AreaInfo = $scope.Obj.ContactAddressList[0].AreaInfo;
            $scope.Obj.ContactAddressList[1].PostOffice = $scope.Obj.ContactAddressList[0].PostOffice;
            $scope.Obj.ContactAddressList[1].PoliceStation = $scope.Obj.ContactAddressList[0].PoliceStation;
            $scope.Obj.ContactAddressList[1].District = $scope.Obj.ContactAddressList[0].District;
            $scope.Obj.ContactAddressList[1].PostCode = $scope.Obj.ContactAddressList[0].PostCode;
            $scope.Obj.ContactAddressList[1].CountryId = $scope.Obj.ContactAddressList[0].CountryId;
        }
    };
    $scope.chnageAddressMIST = function() {
        if ($scope.IsPresentAddressMIST) {
            $scope.Obj.ContactAddressList[0].CareOfPersonName = $scope.Obj.Account.FullName;
            $scope.Obj.ContactAddressList[0].AppartmentNo = "";
            $scope.Obj.ContactAddressList[0].HouseNo = "";
            $scope.Obj.ContactAddressList[0].RoadNo = "MIST Officers' Quarter";
            $scope.Obj.ContactAddressList[0].AreaInfo = "Mirpur Cantonment";
            $scope.Obj.ContactAddressList[0].PostOffice = "Mirpur";
            $scope.Obj.ContactAddressList[0].PoliceStation = "Mirpur";
            $scope.Obj.ContactAddressList[0].District = "Dhaka";
            $scope.Obj.ContactAddressList[0].PostCode = 1216;
            $scope.Obj.ContactAddressList[0].CountryId = 1;
        } else {
            $scope.Obj.ContactAddressList[0].CareOfPersonName = $scope.Obj.Account.FullName;
            $scope.Obj.ContactAddressList[0].AppartmentNo = "";
            $scope.Obj.ContactAddressList[0].HouseNo = "";
            $scope.Obj.ContactAddressList[0].RoadNo = "";
            $scope.Obj.ContactAddressList[0].AreaInfo = "";
            $scope.Obj.ContactAddressList[0].PostOffice = "";
            $scope.Obj.ContactAddressList[0].PoliceStation = "";
            $scope.Obj.ContactAddressList[0].District = "";
            $scope.Obj.ContactAddressList[0].PostCode = 0;
            $scope.Obj.ContactAddressList[0].CountryId = 1;
        }
    };
    //Appointment
    $scope.addAppointment = function() {
        $scope.getEmployeeAppointmentHistory();
    };
    $scope.changeIsPrimaryAppointment = function($index) {
        if ($scope.Obj.AppointmentHistoryList[$index].IsPrimary) {
            for (var i = 0; i < $scope.Obj.AppointmentHistoryList.length; i++) {
                if ($scope.Obj.AppointmentHistoryList[i].Id != $scope.Obj.AppointmentHistoryList[$index].Id) {
                    $scope.Obj.AppointmentHistoryList[i].IsPrimary = false;
                }
            }
        } else {
            for (var i = 0; i < $scope.Obj.AppointmentHistoryList.length; i++) {
                if (!$scope.Obj.AppointmentHistoryList[i].IsPrimary) {
                    $scope.Obj.AppointmentHistoryList[$index].IsPrimary = true;
                    alertWarning("You have to give only one Primary Appointment.");
                }
            }
        }
    };
    $scope.deleteAppointment = function($index) {
        if ($scope.Obj.AppointmentHistoryList.length == 1 || $scope.Obj.AppointmentHistoryList[$index].IsPrimary) {
            alertWarning("Can't Delete Primary Appointment. You have to give at least One Appointment.");
        } else {
            $scope.Obj.AppointmentHistoryList.splice($index, 1);
        }
    };

    $scope.getEmployeeAppointmentHistory = function() {
        $scope.IsLoading++;
        $http.get($scope.getEmployeeAppointmentHistoryUrl, {
            params: { "userEmployeeId": $scope.Obj.Employee.Id }
        }).success(function(result) {
            if (!result.HasError) {
                //result.Data.StartDate = $filter('date')(result.Data.StartDate, $scope.formatDate);
                $scope.Obj.AppointmentHistoryList.push(result.Data);
                $scope.IsLoading--;
            } else {
                $scope.IsLoading--;
                toastr.error("Please try again later to get Data! " + result.Errors.toString(), "Oops. Something went wrong. ", { positionClass: "toast-top-center", timeOut: 10000, closeButton: true });
            }
        }).error(function(result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Semester list! " + "Status: " + status.toString() + ". " + JSON.stringify(result).toString();
            console.log(result);
            alertError($scope.ErrorMsg);
            $scope.IsLoading--;
        });
    };

    $scope.getEmployeeDataExtra = function() {
        $scope.IsLoading++;
        $http.get($scope.getEmployeeDataExtraUrl)
            .success(function(result) {
                if (!result.HasError) {
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
                    $scope.BankList = result.DataExtra.BankList;
                    $scope.UserCategoryEnumList = result.DataExtra.UserCategoryEnumList;
                    $scope.UserStatusEnumList = result.DataExtra.UserStatusEnumList;
                    $scope.BloodGroupEnumList = result.DataExtra.BloodGroupEnumList;
                    $scope.GenderEnumList = result.DataExtra.GenderEnumList;
                    $scope.MaritalStatusEnumList = result.DataExtra.MaritalStatusEnumList;
                    $scope.ReligionEnumList = result.DataExtra.ReligionEnumList;

                    $scope.IncrementMonthEnumList = result.DataExtra.IncrementMonthEnumList;
                    $scope.EmployeeCategoryEnumList = result.DataExtra.EmployeeCategoryEnumList;
                    $scope.EmployeeTypeEnumList = result.DataExtra.EmployeeTypeEnumList;
                    $scope.JobClassEnumList = result.DataExtra.JobClassEnumList;
                    $scope.JobTypeEnumList = result.DataExtra.JobTypeEnumList;
                    $scope.WorkingStatusEnumList = result.DataExtra.WorkingStatusEnumList;

                    $scope.ContactAddressTypeEnumList = result.DataExtra.ContactAddressTypeEnumList;
                    $scope.ContactEmailTypeEnumList = result.DataExtra.ContactEmailTypeEnumList;
                    $scope.ContactNumberTypeEnumList = result.DataExtra.ContactNumberTypeEnumList;

                    $scope.DepartmentList = result.DataExtra.DepartmentList;
                    $scope.PositionList = result.DataExtra.PositionList;
                    $scope.JobClassList = result.DataExtra.JobClassList;
                    $scope.CountryList = result.DataExtra.CountryList;
                    $scope.CampusList = result.DataExtra.CampusList;
                    $scope.IsLoading--;
                } else {
                    $scope.IsLoading--;
                    alertError("Please try again later to get Extra Data!");
                }

                log(result);
            })
            .error(function(result) {
                $scope.IsLoading--;
                alertError("Please try again later to get Extra Data!");
            });
    };
    /*Save Employee*/
    $scope.saveEmployee = function() {
        var msg = "";
        if ($scope.Obj.Account.FullName == null || $scope.Obj.Account.FullName === "") {
            msg += "Name can't be blank. ";
        }
        if ($scope.Obj.Account.FatherName == null || $scope.Obj.Account.FatherName === "") {
            msg += "Father's Name can't be blank. ";
        }
        if ($scope.Obj.Account.MotherName == null || $scope.Obj.Account.MotherName === "") {
            msg += "Mother's Name can't be blank. ";
        }
        if ($scope.Obj.Account.Nationality == null || $scope.Obj.Account.Nationality === "") {
            msg += "Nationality can't be blank. ";
        }
        if ($scope.Obj.Account.Password == null || $scope.Obj.Account.Password === "") {
            msg += "Password can't be blank. ";
        }
        if (($scope.Obj.Account.Password != null || $scope.Obj.Account.Password !== "") && $scope.Obj.Account.Password.length < 6) {
            msg += "Password is required at least 6 characters. ";
        }
        if (($scope.Obj.Account.Password != null || $scope.Obj.Account.Password !== "") && $scope.Obj.Account.Password != $scope.Obj.ConfirmPassword) {
            msg += "Password and Confirm Password doesn't match! ";
        }
        if ($scope.Obj.Account.RankId == null || $scope.Obj.Account.RankId <= 0) {
            msg += "Please Select Rank or Designation. ";
        }
        if ($scope.Obj.Account.CampusId == null || $scope.Obj.Account.CampusId <= 0) {
            msg += "Please Select Campus. ";
        }
        if ($scope.Obj.Employee.OldIdNo == null || $scope.Obj.Employee.OldIdNo === "") {
            msg += "MIST ID No. can't be blank. ";
        }
        for (var i = 0; i < $scope.Obj.AppointmentHistoryList.length; i++) {
            if ($scope.Obj.AppointmentHistoryList[i].PositionId == null || $scope.Obj.AppointmentHistoryList[i].PositionId <= 0) {
                msg += "Please Select Appointed Post or Position. ";
            }
            if ($scope.Obj.AppointmentHistoryList[i].DepartmentId == null || $scope.Obj.AppointmentHistoryList[i].DepartmentId <= 0) {
                msg += "Please Select Appointed Office or Faculty or Dept. ";
            }
        }
        for (var i = 0; i < $scope.Obj.ContactAddressList.length; i++) {
            //if ($scope.Obj.ContactAddressList[i].AreaInfo == null || $scope.Obj.ContactAddressList[i].AreaInfo == "") {
            //    msg += "Area Name can't be blank. ";
            //}
            if ($scope.Obj.ContactAddressList[i].PostOffice == null || $scope.Obj.ContactAddressList[i].PostOffice == "") {
                msg += "Post Office can't be blank. ";
            }
            if ($scope.Obj.ContactAddressList[i].PoliceStation == null || $scope.Obj.ContactAddressList[i].PoliceStation == "") {
                msg += "Police Station can't be blank. ";
            }
            if ($scope.Obj.ContactAddressList[i].District == null || $scope.Obj.ContactAddressList[i].District == "") {
                msg += "District/State/City can't be blank. ";
            }
            //if ($scope.Obj.ContactAddressList[i].PostCode == null || $scope.Obj.ContactAddressList[i].PostCode == "") {
            //    msg += "PostCode can't be blank. ";
            //}
        }
        if (msg === "") {
            $scope.IsLoading++;
            //$scope.Obj.Account.DateOfBirth = $filter('date')($scope.Obj.Account.DateOfBirth, $scope.formatDateToSave);
            //$scope.Obj.Employee.JoiningDate = $filter('date')($scope.Obj.Employee.JoiningDate, $scope.formatDateToSave);
            $http.post($scope.saveEmployeeUrl + "/", $scope.Obj).
            success(function(result, status) {
                if (result.HasError) {
                    $scope.error = true;
                    msg += result.Errors.toString();
                    alertError(msg);
                } else {
                    msg += "Registration is successful! " +
                        "Your Username and Password will notified through email after verified by Admin. ";
                    alertSuccess(msg);
                    $scope.messagesSuccess = msg;
                    $scope.Obj = result.Data;
                }
                $scope.IsLoading--;
            }).error(function(result, status) {
                $scope.HasError = true;
                msg = "Unable to save now! " + "Status: " + status.toString() + ". " + JSON.stringify(result).toString();
                console.log(result);
                alertError(msg);
                $scope.IsLoading--;
            });
        } else {
            msg += "Please provide all the required information! ";
            alertWarning(msg);
        }
        $scope.messages = msg;
    };
});