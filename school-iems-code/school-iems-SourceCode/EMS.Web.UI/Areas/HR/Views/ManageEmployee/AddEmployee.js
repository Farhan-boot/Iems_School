//File: User_Account Anjular  Controller
emsApp.controller('AddNewEmployeeCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.Account = [];
    $scope.AccountId = '0'; //201701241753321453
    $scope.EducationId = 0;
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    //$scope.UserIdPrefix = "";

    $scope.loadPage = function (AccountId) {
        if (AccountId != null) {
            $scope.AccountId = AccountId;
        }
        $scope.getAccountById($scope.AccountId);
    };

    $scope.getNewAccount = function () {
        $scope.getAccountById(0);
    };

    $scope.getAccountById = function (AccountId) {
        if (AccountId != null && AccountId !== '')
            $scope.AccountId = AccountId;
        else return;

        $http.get($scope.getAccountByIdUrl, {
            params: {
                "id": $scope.AccountId
            }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.Account = result.Data;
                
                updateUrlQuery('id', $scope.Account.Id);

                //if (result.DataExtra.EnrollmentTypeEnumList != null)
                $scope.EnrollmentTypeEnumList = result.DataExtra.EnrollmentTypeEnumList;

                //if (result.DataExtra.ProgramList != null)
                $scope.ProgramList = result.DataExtra.ProgramList;

                //if (result.DataExtra.AdmissionExamList != null)
                if (result.DataExtra.UserTypeEnumList != null)
                    $scope.UserTypeEnumList = result.DataExtra.UserTypeEnumList;
                if (result.DataExtra.UserStatusEnumList != null)
                    $scope.UserStatusEnumList = result.DataExtra.UserStatusEnumList;
                if (result.DataExtra.GenderEnumList != null)
                    $scope.GenderEnumList = result.DataExtra.GenderEnumList;
                //if (result.DataExtra.ReligionEnumList != null)
                //    $scope.ReligionEnumList = result.DataExtra.ReligionEnumList;
                //if (result.DataExtra.BloodGroupEnumList != null)
                //    $scope.BloodGroupEnumList = result.DataExtra.BloodGroupEnumList;
                //if (result.DataExtra.MaritalStatusEnumList != null)
                //    $scope.MaritalStatusEnumList = result.DataExtra.MaritalStatusEnumList;

                //if (result.DataExtra.CityList != null)
                //    $scope.CityList = result.DataExtra.CityList;
                //if (result.DataExtra.DistrictList != null)
                //    $scope.DistrictList = result.DataExtra.DistrictList;

                //if (result.DataExtra.PoliceStationList != null)
                //    $scope.PoliceStationList = result.DataExtra.PoliceStationList;

                //if (result.DataExtra.PostOfficeList != null)
                //    $scope.PostOfficeList = result.DataExtra.PostOfficeList;

                //if (result.DataExtra.CountryList != null)
                //    $scope.CountryList = result.DataExtra.CountryList;

                if (result.DataExtra.SemesterList != null)
                    $scope.SemesterList = result.DataExtra.SemesterList;

                //if (result.DataExtra.RelationshipList != null)
                //    $scope.RelationshipList = result.DataExtra.RelationshipList;

                //if (result.DataExtra.UserContactPrivacyEnumList != null)
                //    $scope.UserContactPrivacyEnumList = result.DataExtra.UserContactPrivacyEnumList;

                if (result.DataExtra.DepartmentList != null)
                    $scope.DepartmentList = result.DataExtra.DepartmentList;

                //if (result.DataExtra.CampusList != null)
                //    $scope.CampusList = result.DataExtra.CampusList;

                //if (result.DataExtra.IncrementMonthEnumList != null)
                //    $scope.IncrementMonthEnumList = result.DataExtra.IncrementMonthEnumList;

                if (result.DataExtra.PositionList != null)
                    $scope.PositionList = result.DataExtra.PositionList;

                if (result.DataExtra.WorkingStatusEnumList != null)
                    $scope.WorkingStatusEnumList = result.DataExtra.WorkingStatusEnumList;

                if (result.DataExtra.JobClassEnumList != null)
                    $scope.JobClassEnumList = result.DataExtra.JobClassEnumList;

                if (result.DataExtra.JobTypeEnumList != null)
                    $scope.JobTypeEnumList = result.DataExtra.JobTypeEnumList;

                if (result.DataExtra.EmployeeCategoryEnumList != null)
                    $scope.EmployeeCategoryEnumList = result.DataExtra.EmployeeCategoryEnumList;

                if (result.DataExtra.EmployeeTypeEnumList != null)
                    $scope.EmployeeTypeEnumList = result.DataExtra.EmployeeTypeEnumList;
                ////

                //if ($scope.AdmissionExamList != null && $scope.Account.AdmissionExamId > 0) {
                //    var admExam = $filter('filter')($scope.AdmissionExamList, { Id: $scope.Account.AdmissionExamId })[0];
                //    $scope.SelectedAdmissionExam = admExam;
                //    $scope.onChangeAdmissionExam();
                //}
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get User Account! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get User Account! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
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


        $http.post($scope.saveAccountUrl + "/", $scope.Account).
        success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.Data != null) {
                    $scope.Account = result.Data;
                    //updateUrlQuery('id', $scope.Account.Id);
                    alertSuccess("New Employee Successfully Created!");
                    window.location.replace($scope.editEmployeeByIdPageUrl + '?id=' + $scope.Account.Id);
                }

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).
        error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to Created New Employee! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.validateAccount = function () {
        return true;
    };



    //======Custom Variabales goes hare===== ?id=0

    $scope.Init = function (
        getAccountByIdUrl,
        saveAccountUrl,
        //getDataExtraOnChangeProgramUrl,
        editEmployeeByIdPageUrl) {
        //$scope.AccountId = AccountId;
        $scope.getAccountByIdUrl = getAccountByIdUrl;
        $scope.saveAccountUrl = saveAccountUrl;
        $scope.editEmployeeByIdPageUrl = editEmployeeByIdPageUrl;

        $scope.loadPage(0);
    };



});