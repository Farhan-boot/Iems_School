
//File: User_Student Anjular  Controller
emsApp.controller('StudentAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.Student = [];
    $scope.StudentId = 0;
    $scope.ErrorMsg = "";
    $scope.HasError = false;

    $scope.loadPage = function (StudentId) {
        if (StudentId != null)
            $scope.StudentId = StudentId;

        $scope.loadStudentExtraData($scope.StudentId);
        $scope.getStudentById($scope.StudentId);
    };
    $scope.getNewStudent = function () {
        $scope.getStudentById(0);
    };
    $scope.getStudentById = function (StudentId) {
        if (StudentId != null && StudentId !== "")
            $scope.StudentId = StudentId;
        else return;

        $http.get($scope.getStudentByIdUrl, {
            params: { "id": $scope.StudentId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.Obj = result.Data;
                updateUrlQuery('id', $scope.Obj.Student.Id);
                $scope.onAfterGetStudentById(result);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Student! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Student! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.loadStudentExtraData = function (StudentId) {
        if (StudentId != null)
            $scope.StudentId = StudentId;

        $http.get($scope.getStudentExtraDataUrl, {
            params: { "id": $scope.StudentId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.DataExtra.StudentQuotaEnumList != null)
                    $scope.StudentQuotaEnumList = result.DataExtra.StudentQuotaEnumList;
                if (result.DataExtra.EnrolmentStatusEnumList != null)
                    $scope.EnrolmentStatusEnumList = result.DataExtra.EnrolmentStatusEnumList;
                if (result.DataExtra.EnrolmentTypeEnumList != null)
                    $scope.EnrolmentTypeEnumList = result.DataExtra.EnrolmentTypeEnumList;
                if (result.DataExtra.ParentsPrimaryJobTypeEnumList != null)
                    $scope.ParentsPrimaryJobTypeEnumList = result.DataExtra.ParentsPrimaryJobTypeEnumList;
                if (result.DataExtra.GradeTypeEnumList != null)
                    $scope.GradeTypeEnumList = result.DataExtra.GradeTypeEnumList;
                $scope.onAfterLoadStudentExtraData(result);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Student! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Student! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.saveStudent = function () {
        if (!$scope.validateStudent()) {
            return;
        }
        $http.post($scope.saveStudentUrl + "/", $scope.Obj).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    if (result.Data != null) {
                        $scope.Obj = result.Data;
                        updateUrlQuery('id', $scope.Obj.Student.Id);
                    }
                    alertSuccess("Successfully saved Student data!");
                    $scope.onAfterSaveStudent(result);
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to save Student! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to save Student! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    };

    $scope.deleteStudentById = function () {

    };
    $scope.validateStudent = function () {
        return true;
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variabales goes hare=====

    $scope.Init = function (StudentId, getStudentByIdUrl, getStudentExtraDataUrl, saveStudentUrl, deleteStudentByIdUrl) {
        $scope.StudentId = StudentId;
        $scope.getStudentByIdUrl = getStudentByIdUrl;
        $scope.saveStudentUrl = saveStudentUrl;
        $scope.getStudentExtraDataUrl = getStudentExtraDataUrl;
        $scope.deleteStudentByIdUrl = deleteStudentByIdUrl;

        $scope.loadPage(StudentId);
    };

    $scope.onAfterSaveStudent = function (result) {
        //var data=result.Data;
    };
    $scope.onAfterGetStudentById = function (result) {
        $scope.changeMilitaryCategory();
        //var data=result.Data;
        /*//Child Table Bindings
                 $scope.StudentIdList =  result.DataExtra.StudentIdList; 
    
                 $scope.StudentIdList =  result.DataExtra.StudentIdList; 
    
                 $scope.StudentIdList =  result.DataExtra.StudentIdList; 
    
                 $scope.StudentIdList =  result.DataExtra.StudentIdList; 
    
                 $scope.StudentIdList =  result.DataExtra.StudentIdList; 
    
                 $scope.StudentIdList =  result.DataExtra.StudentIdList; 
         */
    };
    $scope.onAfterLoadStudentExtraData = function (result) {
        //var DataExtra=result.DataExtra;
        //DropDown Option Tables
        if (result.DataExtra.StudyLevelList != null)
            $scope.StudyLevelList = result.DataExtra.StudyLevelList;
        if (result.DataExtra.StudyTermList != null)
            $scope.StudyTermList = result.DataExtra.StudyTermList;
        if (result.DataExtra.ClassSectionList != null)
            $scope.ClassSectionList = result.DataExtra.ClassSectionList;
        if (result.DataExtra.CampusList != null)
            $scope.CampusList = result.DataExtra.CampusList;
        if (result.DataExtra.UserTypeEnumList != null)
            $scope.UserTypeEnumList = result.DataExtra.UserTypeEnumList;
        if (result.DataExtra.CountryList != null)
            $scope.CountryList = result.DataExtra.CountryList;
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
        if (result.DataExtra.CurriculumList != null)
            $scope.CurriculumList = result.DataExtra.CurriculumList;
        if (result.DataExtra.ProgramList != null)
            $scope.ProgramList = result.DataExtra.ProgramList;
        if (result.DataExtra.DeptBatchList != null)
            $scope.DeptBatchList = result.DataExtra.DeptBatchList;

        if (result.DataExtra.ContactAddressTypeEnumList != null)
            $scope.ContactAddressTypeEnumList = result.DataExtra.ContactAddressTypeEnumList;
        if (result.DataExtra.ContactEmailTypeEnumList != null)
            $scope.ContactEmailTypeEnumList = result.DataExtra.ContactEmailTypeEnumList;
        if (result.DataExtra.ContactNumberTypeEnumList != null)
            $scope.ContactNumberTypeEnumList = result.DataExtra.ContactNumberTypeEnumList;
        if (result.DataExtra.PrivacyEnumList != null)
            $scope.PrivacyEnumList = result.DataExtra.PrivacyEnumList;

        if (result.DataExtra.EducationTypeEnumList != null)
            $scope.EducationTypeEnumList = result.DataExtra.EducationTypeEnumList;
        if (result.DataExtra.DegreeEquivalentEnumList != null)
            $scope.DegreeEquivalentEnumList = result.DataExtra.DegreeEquivalentEnumList;

        if (result.DataExtra.RankList != null) {
            $scope.RankList = result.DataExtra.RankList;
            for (var i = 0; i < $scope.RankList.length; i++) {
                if ($scope.RankList[i].CategoryEnumId == 1) {
                    $scope.RankArmyList.push($scope.RankList[i]);
                }
                else if ($scope.RankList[i].CategoryEnumId == 2) {
                    $scope.RankNavyList.push($scope.RankList[i]);
                }
                else if ($scope.RankList[i].CategoryEnumId == 3) {
                    $scope.RankAirForceList.push($scope.RankList[i]);
                }
                else {
                    $scope.RankCivilList.push($scope.RankList[i]);
                }
            }
        }
    };
    //======Other tabale's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 
    //// Military Category Change
    $scope.changeMilitaryCategory = function () {
        if ($scope.Obj.Account.CategoryEnumId === 1) {
            $scope.RankFilterList = $scope.RankArmyList;
            $scope.Obj.Account.IsMilitary = true;
            if ($scope.Obj.Account.PersonalNo != null) {
                if ($scope.Obj.Account.PersonalNo.length < 4
                    || $scope.Obj.Account.PersonalNo == "PN00"
                    || $scope.Obj.Account.PersonalNo == "BD00") {
                    $scope.Obj.Account.PersonalNo = "BA00";
                }
            } else {
                $scope.Obj.Account.PersonalNo = "BA00";
            }
        }
        else if ($scope.Obj.Account.CategoryEnumId === 2) {
            $scope.RankFilterList = $scope.RankNavyList;
            $scope.Obj.Account.IsMilitary = true;
            if ($scope.Obj.Account.PersonalNo != null) {
                if ($scope.Obj.Account.PersonalNo.length < 4
                    || $scope.Obj.Account.PersonalNo == "BA00"
                    || $scope.Obj.Account.PersonalNo == "BD00") {
                    $scope.Obj.Account.PersonalNo = "PN00";
                }
            } else {
                $scope.Obj.Account.PersonalNo = "PN00";
            }
        }
        else if ($scope.Obj.Account.CategoryEnumId === 3) {
            $scope.RankFilterList = $scope.RankAirForceList;
            $scope.Obj.Account.IsMilitary = true;
            if ($scope.Obj.Account.PersonalNo != null) {
                if ($scope.Obj.Account.PersonalNo.length < 4
                    || $scope.Obj.Account.PersonalNo == "PN00"
                    || $scope.Obj.Account.PersonalNo == "BA00") {
                    $scope.Obj.Account.PersonalNo = "BD00";
                }
            } else {
                $scope.Obj.Account.PersonalNo = "BD00";
            }
        }
        else {
            $scope.RankFilterList = $scope.RankCivilList;
            $scope.Obj.Account.IsMilitary = false;
            if ($scope.Obj.Account.PersonalNo != null) {
                if ($scope.Obj.Account.PersonalNo.length < 3
                    || $scope.Obj.Account.PersonalNo == "BA00"
                    || $scope.Obj.Account.PersonalNo == "PN00"
                    || $scope.Obj.Account.PersonalNo == "BD00") {
                    $scope.Obj.Account.PersonalNo = "";
                }
            } else {
                $scope.Obj.Account.PersonalNo = "";
            }
        }
    };

    //======Custom property and Functions End========================================================== 
    $scope.RankList = [];
    $scope.RankFilterList = [];
    $scope.RankArmyList = [];
    $scope.RankNavyList = [];
    $scope.RankAirForceList = [];
    $scope.RankCivilList = [];
    $scope.Message = "";
});

