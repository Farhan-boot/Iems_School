
//File: User_Account Anjular  Controller
emsApp.controller('ProfileManager', function ($scope, $http, $filter, cfpLoadingBar, $sce) {
    $scope.Account = [];
    $scope.AccountId = "";
    $scope.StudentId = "";
    $scope.EducationId = 0;
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $('#isPhotoUploadAble').hide();
    $('#isPhotoRemoveable').hide();

    $scope.SelectedFileForUpload = null;

    $scope.IsDisabledUserNameGroup = true;
    $scope.IsDisabledStudentNameGroup = true;
    $scope.IsDisabledAcademicGroup = true;

    $scope.CancelAdmissionReasonEnumId = null;
    $scope.IsSendEmail = true;

    $scope.SelectedAdmissionExam = [];
    $scope.SelectedProgram = [];

    $scope.loadPage= function (AccountId)
    {
       if (AccountId != null) {
           $scope.AccountId =AccountId;
       }
       
       $scope.loadAccountExtraData($scope.AccountId);
       $scope.loadEducationExtraData($scope.EducationId);
       $scope.loadStdScholarshipExtraData(AccountId);

       $scope.getAccountById($scope.AccountId);

    };
   $scope.getNewAccount= function()
    {
    	  $scope.getAccountById(0);
    };
   $scope.getAccountById= function(AccountId)
    {
        if(AccountId!=null && AccountId!=='')
        $scope.AccountId=AccountId;
        else return;
        
        $http.get($scope.getAccountByIdUrl, {
            params: { "id": $scope.AccountId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.Account = result.Data;
                $scope.StudentId = $scope.Account.StudentId;
                $scope.AccountId = $scope.Account.Id;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.IsValidEmail();
                updateUrlQuery('id', $scope.Account.Id);
                updateUrlQuery('stdId', $scope.Account.StudentId);
                updateUrlQuery('un', $scope.Account.UserName);

                $scope.onAfterGetAccountById($scope.Account);
                
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg = "Unable to get Student Profile! " + result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg = "Unable to get Student Profile! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadAccountExtraData= function(AccountId)
    {
        if(AccountId!=null)
            $scope.AccountId=AccountId;
            
            $http.get($scope.getAccountExtraDataUrl, {
                params: { "id": $scope.AccountId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.GenderEnumList!=null)
                         $scope.GenderEnumList = result.DataExtra.GenderEnumList;
                      if(result.DataExtra.ReligionEnumList!=null)
                         $scope.ReligionEnumList = result.DataExtra.ReligionEnumList;
                    if (result.DataExtra.BloodGroupEnumList != null)
                         $scope.BloodGroupEnumList = result.DataExtra.BloodGroupEnumList;
                      if(result.DataExtra.MaritalStatusEnumList!=null)
                         $scope.MaritalStatusEnumList = result.DataExtra.MaritalStatusEnumList;

                    if (result.DataExtra.CityList != null)
                          $scope.CityList = result.DataExtra.CityList;
                    if (result.DataExtra.DistrictList != null)
                        $scope.DistrictList = result.DataExtra.DistrictList;

                    if (result.DataExtra.PoliceStationList != null)
                        $scope.PoliceStationList = result.DataExtra.PoliceStationList;

                    if (result.DataExtra.PostOfficeList != null)
                        $scope.PostOfficeList = result.DataExtra.PostOfficeList;

                    if (result.DataExtra.CountryList != null)
                        $scope.CountryList = result.DataExtra.CountryList;

                    if (result.DataExtra.AdmissionExamList != null)
                        $scope.AdmissionExamList = result.DataExtra.AdmissionExamList;
                    if (result.DataExtra.ProgramList != null)
                        $scope.ProgramList = result.DataExtra.ProgramList;
                    if (result.DataExtra.SemesterList != null)
                        $scope.SemesterList = result.DataExtra.SemesterList;
                    if (result.DataExtra.AdmissionStatusEnumList != null)
                        $scope.AdmissionStatusEnumList = result.DataExtra.AdmissionStatusEnumList;
                    if (result.DataExtra.EnrollmentStatusEnumList != null)
                        $scope.EnrollmentStatusEnumList = result.DataExtra.EnrollmentStatusEnumList;
                    if (result.DataExtra.EnrolmentTypeEnumList != null)
                        $scope.EnrolmentTypeEnumList = result.DataExtra.EnrolmentTypeEnumList;
                    if (result.DataExtra.RelationshipList != null)
                        $scope.RelationshipList = result.DataExtra.RelationshipList;
                    if (result.DataExtra.UserStatusEnumList != null)
                        $scope.UserStatusEnumList = result.DataExtra.UserStatusEnumList;
                    if (result.DataExtra.UserContactPrivacyEnumList != null)
                        $scope.UserContactPrivacyEnumList = result.DataExtra.UserContactPrivacyEnumList;
                    if (result.DataExtra.UserTypeEnumList != null)
                        $scope.UserTypeEnumList = result.DataExtra.UserTypeEnumList;
                    if (result.DataExtra.ParentsPrimaryJobTypeList != null)
                        $scope.ParentsPrimaryJobTypeList = result.DataExtra.ParentsPrimaryJobTypeList;
                    if (result.DataExtra.ClassTimingGroupEnumList != null)
                        $scope.ClassTimingGroupEnumList = result.DataExtra.ClassTimingGroupEnumList;
                    if (result.DataExtra.DeptBatchList != null)
                        $scope.DeptBatchList = result.DataExtra.DeptBatchList;
                    if (result.DataExtra.GradingPolicyList != null)
                        $scope.GradingPolicyList = result.DataExtra.GradingPolicyList;
                    if (result.DataExtra.DepartmentList != null)
                        $scope.DepartmentList = result.DataExtra.DepartmentList;
                    if (result.DataExtra.StudyLevelTermList != null)
                        $scope.StudyLevelTermList = result.DataExtra.StudyLevelTermList;

                    if (result.DataExtra.ClassSectionList != null)
                        $scope.ClassSectionList = result.DataExtra.ClassSectionList;
                    
                    if (result.DataExtra.CancelAdmissionReasonEnumList != null)
                        $scope.CancelAdmissionReasonEnumList = result.DataExtra.CancelAdmissionReasonEnumList;

                    
                        

                    
                   

                    //if (result.DataExtra.StudyLevelList != null)
                    //    $scope.StudyLevelList = result.DataExtra.StudyLevelList;
                    //if (result.DataExtra.StudyTermList != null)
                    //    $scope.StudyTermList = result.DataExtra.StudyTermList;

                    if (result.DataExtra.StudentQuotaNameList != null)
                        $scope.StudentQuotaNameList = result.DataExtra.StudentQuotaNameList;
                    if (result.DataExtra.CampusList != null)
                        $scope.CampusList = result.DataExtra.CampusList;

                        $scope.onAfterLoadAccountExtraData(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable supporting data for Student Profile! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg = "Unable supporting data for Student Profile! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveAccount= function(){
    	if(!$scope.validateAccount())
        {
          return;
    	}
        $http.post($scope.saveAccountUrl + "/", $scope.Account).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    $scope.ErrorMsg = "";
                   if(result.Data!=null)
                   {
                       $scope.Account = result.Data;
                       $scope.getAccountById($scope.AccountId);
                        updateUrlQuery('id', $scope.Account.Id);
                   }
                   alertSuccess("Student Profile Successfully Saved!");
                   $scope.onAfterSaveAccount(result);
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg = "Profile Saving Failed! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg = "Profile Saving Failed! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    };
   $scope.getDataExtraOnChangeProgram = function (ProgramId) {
        if ($scope.Account.ProgramId != null)
            $scope.ProgramId = ProgramId;

        $http.get($scope.getDataExtraOnChangeProgramUrl, {
            params: {
                "id": $scope.ProgramId,
                "stdId": $scope.Account.Id
            }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.ErrorMsg = result.Errors.toString();
                //if (result.DataExtra.CurriculumList != null) 
                //If Curriculum not found for this program than return null
                $scope.CurriculumList = result.DataExtra.CurriculumList;
                //if (result.DataExtra.CurriculumElectiveGroupList != null)
                    $scope.CurriculumElectiveGroupList = result.DataExtra.CurriculumElectiveGroupList;
                //if (result.DataExtra.FeeCodeList != null)
                    $scope.FeeCodeList = result.DataExtra.FeeCodeList;
                    $scope.onAfterLoadOnChangeProgramExtraData(result);

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Some Required data not found in the System! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load Required data, Please try again! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
   $scope.onAfterLoadOnChangeProgramExtraData = function (result) {
        // For Edit mode
        if ($scope.AccountId > 0) {
            if ($scope.Account.FeeCodeId != null || $scope.Account.FeeCodeId >= 0) {
                if ($scope.FeeCodeList.length > 0) {
                    var feeCode = $filter('filter')($scope.FeeCodeList,
                        {
                            Id: $scope.Account.FeeCodeId
                        },true)[0];
                    $scope.SelectedFeeCode = feeCode;
                    $scope.onChangeFeeCode();
                }
            }

            $scope.filterSelectedCurriculum();
        }

    };

    /*
    $scope.getDataExtraOnChangeAdmissionExam = function (AdmissionExamId) {
        if ($scope.Account.AdmissionExamSettingId != null)
            $scope.AdmissionExamSettingId = AdmissionExamId;

        $http.get($scope.getDataExtraOnChangeAdmissionExamUrl, {
            params: { "id": $scope.AdmissionExamSettingId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.DataExtra.Semester != null)
                    $scope.Semester = result.DataExtra.Semester;

                //$scope.onAfterLoadEducationExtraData(result);
                console.log("Semester");
                console.log($scope.Semester);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Admission Exam! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Admission Exam! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
*/

   $scope.deleteAccountById= function(){
    	
    };
   $scope.validateAccount= function(){
        return true;
   };


//======Custom property and Functions Start=======================================================  

   $scope.removeUploadedPhoto = function () {
       bootbox.confirm("Are you sure you want to remove Student's Profile Picture?",
           function (yes) {
               if (yes) {
                   $http.get($scope.removePhotoOrSignatureByIdUrl, {
                       params: {
                           "userId": $scope.Account.Id
                       }
                   }).success(function (result, status) {
                       if (!result.HasError) {
                           $scope.HasError = false;
                           alertSuccess("Profile Picture Removed Successfully");
                           
                            $("#profilePhoto").attr("src", "/assets/img/avatars/BlankPerson.png");
                            $scope.isPhotoUploadAble = false;
                            $scope.Account.ProfilePictureImageUrl = null;
                         
                          
                       } else {
                           $scope.HasError = true;
                           $scope.ErrorMsg = result.Errors.toString();
                           alertError($scope.ErrorMsg);
                       }
                   }).error(function (result, status) {
                       $scope.HasError = true;
                       $scope.ErrorMsg = "Unable to load option data for Applicant! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                       alertError($scope.ErrorMsg);
                   });
               }
           });
   }

   $scope.uploadProfilePic = function () {
       if ($scope.CheckIfFileValidToUpload($scope.SelectedFileForUpload)) {

           var formData = new FormData();
           formData.append("profilePic", $scope.SelectedFileForUpload);

           $http.post($scope.uploadProfilePictureUrl, formData, {
               params: {
                   "userId": $scope.Account.Id
               },
               withCredentials: true,
               headers: { 'Content-Type': undefined },
               transformRequest: angular.identity
           })
            .success(function (result, status) {

                if (!result.HasError) {
                    alertSuccess("Profile Picture uploaded Successfully.");
                    $scope.Account.ProfilePictureImageUrl = result.Data;
                    $('#isPhotoUploadAble').hide();
                    //$('#profilePhoto').show();
                    $scope.isPhotoUploadAble = false;

                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to Upload Signature! " + result.Errors.toString();
                    $("#profilePhoto").attr("src", "/assets/img/avatars/BlankPerson.png");
                    alertError($scope.ErrorMsg);
                }
            })
            .error(function (result, status) {
                log(result);
                alertError("Something Went Wrong");
            });

           //bootbox.confirm("Are you sure you want to Upload This Profile Picture?",
           //function (yes) {
           //    if (yes) {
                   
           //    }
           //});
       }
       else {
           alertError($scope.FileInvalidMessag);
       }
       
   }

   $scope.selectedProfilePhotoOnChange = function (input) {
       $('#isPhotoUploadAble').show();
       //$('#profilePhoto').show();
       $scope.isPhotoUploadAble = true;

       if (input.files && input.files[0]) {
           var reader = new FileReader();

           reader.onload = function () {
               $("#profilePhoto").attr("src", reader.result);
               $scope.SelectedFileForUpload = input.files[0];
               $scope.uploadProfilePic();
           }
          
           reader.readAsDataURL(input.files[0]);
       }
   }

   $scope.CheckIfFileValidToUpload = function (file) {
       if ($scope.SelectedFileForUpload != null) {
           if ((file.type == 'image/jpeg')) {
               $scope.FileInvalidMessage = "";
               return true;
           }
           else {
               $scope.FileInvalidMessage = "Selected file is invalid. (only jpg image)";
               return false;
           }
       }
   };

    //Old Function
    //$scope.sameAsPresentAddress = function (isTrue) {
    //    if (isTrue) {
    //        $scope.Account.ContactAddressJsonList[1].Address1 = $scope.Account.ContactAddressJsonList[0].Address1;
    //        $scope.Account.ContactAddressJsonList[1].Address2 = $scope.Account.ContactAddressJsonList[0].Address2;
    //        $scope.Account.ContactAddressJsonList[1].CountryId = $scope.Account.ContactAddressJsonList[0].CountryId;
    //        $scope.Account.ContactAddressJsonList[1].District = $scope.Account.ContactAddressJsonList[0].District;
    //        $scope.Account.ContactAddressJsonList[1].PoliceStation = $scope.Account.ContactAddressJsonList[0].PoliceStation;
    //        $scope.Account.ContactAddressJsonList[1].PostOffice = $scope.Account.ContactAddressJsonList[0].PostOffice;
    //    } else {
    //        $scope.Account.ContactAddressJsonList[1].Address1 = "";
    //        $scope.Account.ContactAddressJsonList[1].Address2 = "";
    //        $scope.Account.ContactAddressJsonList[1].CountryId = "";
    //        $scope.Account.ContactAddressJsonList[1].District = "";
    //        $scope.Account.ContactAddressJsonList[1].PoliceStation = "";
    //        $scope.Account.ContactAddressJsonList[1].PostOffice = "";
    //    }
    //    $scope.SameAddress = isTrue;
    //};

    $scope.filterSelectedCurriculum = function () {
        if ($scope.Account.CurriculumId != null && $scope.Account.CurriculumId > 0) {
            var curriculum = $filter('filter')($scope.CurriculumList, { Id: $scope.Account.CurriculumId },true)[0];
            $scope.TotalSemester = curriculum.TotalSemester;
            $scope.TotalCredit = curriculum.TotalCredit;
        } else {
            $scope.TotalSemester = "";
            $scope.TotalCredit = "";
        }
    };

    $scope.onChangeProgram = function() {
        if ($scope.SelectedProgram != null) {
            $scope.Account.ProgramId = $scope.SelectedProgram.Id;
            $scope.Account.SemesterDurationEnumId = $scope.SelectedProgram.SemesterDurationEnumId;
            $scope.Account.ClassTimingGroupEnumId = $scope.SelectedProgram.ClassTimingGroupEnumId;
            $scope.getDataExtraOnChangeProgram($scope.SelectedProgram.Id);
            $scope.Account.DepartmentId = $scope.SelectedProgram.DepartmentId;
            
        }
    };
    $scope.onChangeAdmissionExam = function() {
        if ($scope.SelectedAdmissionExam != null) {
            $scope.Account.AdmissionExamId = $scope.SelectedAdmissionExam.Id;
            $scope.Account.JoiningSemesterId = $scope.SelectedAdmissionExam.SemesterId;
        }
    };

    $scope.cancelAdmissionModalModelOpen = function () {
        $('#CancelAdmissionModal').modal('show');
    };
    
    //============= Start Personal===========
    $scope.SameAddress = false;
    $scope.EmergencyContactWith = "";
    $scope.SelectedPoliceStationId_Present = "";
    $scope.SelectedDistrictId_Present = "";
    $scope.SelectedPoliceStationId_Permanent = "";
    $scope.SelectedDistrictId_Permanent = "";
    var objClone = new Object();

    $scope.sameAsPermanentAddress = function (isTrue) {
        if (isTrue) {
            angular.copy($scope.Account.PresentAddressJson, objClone);

            $scope.Account.PresentAddressJson.Address1=$scope.Account.PermanentAddressJson.Address1;
            $scope.Account.PresentAddressJson.Address2=$scope.Account.PermanentAddressJson.Address2;
            $scope.Account.PresentAddressJson.CountryId=$scope.Account.PermanentAddressJson.CountryId;
            $scope.Account.PresentAddressJson.District=$scope.Account.PermanentAddressJson.District;
            $scope.Account.PresentAddressJson.PoliceStation=$scope.Account.PermanentAddressJson.PoliceStation;
            $scope.Account.PresentAddressJson.PostOffice=$scope.Account.PermanentAddressJson.PostOffice;
        } else {
            $scope.Account.PresentAddressJson.Address1 = objClone.Address1;
            $scope.Account.PresentAddressJson.Address2 = objClone.Address2;
            $scope.Account.PresentAddressJson.CountryId = objClone.CountryId;
            $scope.Account.PresentAddressJson.District = objClone.District;
            $scope.Account.PresentAddressJson.PoliceStation = objClone.PoliceStation;
            $scope.Account.PresentAddressJson.PostOffice = objClone.PostOffice;
        }
        $scope.SameAddress = isTrue;
    };
    $scope.onChangePermanentAddress = function () {
        if ($scope.SameAddress) {
            $scope.Account.PresentAddressJson.Address1 = $scope.Account.PermanentAddressJson.Address1;
            $scope.Account.PresentAddressJson.Address2 = $scope.Account.PermanentAddressJson.Address2;
            $scope.Account.PresentAddressJson.CountryId = $scope.Account.PermanentAddressJson.CountryId;
            $scope.Account.PresentAddressJson.District = $scope.Account.PermanentAddressJson.District;
            $scope.Account.PresentAddressJson.PoliceStation = $scope.Account.PermanentAddressJson.PoliceStation;
            $scope.Account.PresentAddressJson.PostOffice = $scope.Account.PermanentAddressJson.PostOffice;
        }  
    };
    

    $scope.emergencyContactSelect = function () {
        if (angular.equals($scope.EmergencyContactWith, "Father")) {

            $scope.Account.EmergencyContactPersonName = $scope.Account.FatherName;
            $scope.Account.EmergencyMobile = $scope.Account.FatherMobile;
            $scope.Account.EmergencyContactRelationshipId = 1;
            //$scope.Account.EmergencyContactAdress = "";
            return;
        }
        if (angular.equals($scope.EmergencyContactWith, "Mother")) {

            $scope.Account.EmergencyContactPersonName = $scope.Account.MotherName;
            $scope.Account.EmergencyMobile = $scope.Account.MotherMobile;
            $scope.Account.EmergencyContactRelationshipId = 2;
            //$scope.Account.EmergencyContactAdress = "";
            return;
        }
        if (angular.equals($scope.EmergencyContactWith, "Other")) {

            $scope.Account.EmergencyMobile = "";
            $scope.Account.EmergencyContactPersonName = "";
            $scope.Account.EmergencyContactRelationshipId = 0;
            //$scope.Account.EmergencyContactAdress = "";
            return;
        }

    }

    $scope.emergencyContactSelectRedioBtn = function (id) {
        if (id == 1) {

            $scope.Account.EmergencyContactPersonName = $scope.Account.FatherName;
            $scope.Account.EmergencyMobile = $scope.Account.FatherMobile;
            $scope.Account.EmergencyContactRelationshipId = 1;
            //$scope.Account.EmergencyContactAdress = "";

            // for redio button selecte
            $scope.EmergencyContactWith = "Father";
            return;
        }
        if (id == 2) {

            $scope.Account.EmergencyContactPersonName = $scope.Account.MotherName;
            $scope.Account.EmergencyMobile = $scope.Account.MotherMobile;
            $scope.Account.EmergencyContactRelationshipId = 2;
            //$scope.Account.EmergencyContactAdress = "";

            // for redio button selecte
            $scope.EmergencyContactWith = "Mother";
            return;
        }

        // for redio button selecte
        $scope.EmergencyContactWith = "Other";

        $scope.Account.EmergencyMobile = "";
        $scope.Account.EmergencyContactPersonName = "";

    }
    //Searching for District
    $('.textDropdownSearch').find('input').click(function (e) {
        e.stopPropagation();
    });


    $scope.selectDistrict = function (name, contactAddressType) {

        if (angular.equals(contactAddressType, "Present")) {
            $scope.Account.PresentAddressJson.District = name;
        }

        if (angular.equals(contactAddressType, "Permanent")) {
            $scope.Account.PermanentAddressJson.District = name;
        }

    };

    $scope.selectPoliceStation = function (policeStation, contactAddressType) {

        if (angular.equals(contactAddressType, "Present")) {
            $scope.Account.PresentAddressJson.PoliceStation = policeStation.Name;
        }

        if (angular.equals(contactAddressType, "Permanent")) {
            $scope.Account.PermanentAddressJson.PoliceStation = policeStation.Name;
        }

    };

    $scope.selectPostOffice = function (postOffice, contactAddressType) {

        if (angular.equals(contactAddressType, "Present")) {
            $scope.Account.PresentAddressJson.PostOffice = postOffice.Name;

            $scope.SelectedPoliceStationId_Present = postOffice.PoliceStationId;
            $scope.SelectedDistrictId_Present = postOffice.DistrictId;

            $scope.Account.PresentAddressJson.District = $scope.filterDistrict($scope.SelectedDistrictId_Present);
            $scope.Account.PresentAddressJson.PoliceStation = $scope.filterPoliceStation($scope.SelectedPoliceStationId_Present);

        }

        if (angular.equals(contactAddressType, "Permanent")) {
            $scope.Account.PermanentAddressJson.PostOffice = postOffice.Name;

            $scope.SelectedPoliceStationId_Permanent = postOffice.PoliceStationId;
            $scope.SelectedDistrictId_Permanent = postOffice.DistrictId;

            $scope.Account.PermanentAddressJson.District = $scope.filterDistrict($scope.SelectedDistrictId_Permanent);
            $scope.Account.PermanentAddressJson.PoliceStation = $scope.filterPoliceStation($scope.SelectedPoliceStationId_Permanent);
        }

    };

    $scope.filterDistrict = function (id) {
        if (id != null && id > 0) {
            var district = $filter('filter')($scope.DistrictList, { Id: id })[0];
            if (district != null)
                return district.Name;
        } else {
            return "";
        }
        return "";
    };

    $scope.filterPoliceStation = function (id) {
        if (id != null && id > 0) {
            var policeStation = $filter('filter')($scope.PoliceStationList, { Id: id })[0];
            if (policeStation != null)
                return policeStation.Name;
        } else {
            return "";
        }
        return "";
    };

    $scope.selectPlaceOfBirthCity = function (name) {
        $scope.Account.PlaceOfBirthCity = name;
    };

    //============= End Personal===========

    //============= Start Previous Education===========
    $scope.loadEducationExtraData = function (educationId) {
        if (educationId != null)
            $scope.EducationId = educationId;

        $http.get($scope.getEducationExtraDataUrl, {
            params: { "id": $scope.EducationId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.DataExtra.DegreeEquivalentEnumList != null)
                    $scope.DegreeEquivalentEnumList = result.DataExtra.DegreeEquivalentEnumList;
                $scope.DegreeGroupMajorEnumList = result.DataExtra.DegreeGroupMajorEnumList;
                $scope.ResultSystemEnumList = result.DataExtra.ResultSystemEnumList;

                $scope.onAfterLoadEducationExtraData(result);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Education! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Education! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.getNewEducationByUserId = function () {

        $http.get($scope.getNewEducationByUserIdUrl, {
            params: { "id": $scope.Account.Id }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.Data != null)
                    $scope.Account.EducationJsonList.push(result.Data);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Another Education! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Another Education! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    //$scope.filterSelectedResultSystem = function (id) {
    //    var degreeType = null;
    //    if (id != null || id >= 0) {
    //        degreeType = $filter('filter')($scope.Account.EducationJsonList, { Id: id })[0];
    //    }
    //    if (degreeType.ResultSystemEnumId != null || degreeType.ResultSystemEnumId >= 0) {

    //        var resultSystem = $filter('filter')($scope.ResultSystemEnumList, { Id: degreeType.ResultSystemEnumId })[0];
    //        degreeType.ResultSystem = resultSystem.Name;

    //        //if (!angular.equals(resultSystem.Name, "G P A") || resultSystem !== 0) {
    //        //    degreeType.GradeOrClass = resultSystem.Name;
    //        //}
    //    } //
    //};

    $scope.removeEducationByIndex = function (index) {
        $scope.Account.EducationJsonList.splice(index, 1);
    }
    $scope.getDeleteEducationById = function (id, index) {
        bootbox.confirm("Are you sure you want to delete this Education?", function (yes) {
            if (yes) {
                $http.get($scope.getDeleteEducationByIdUrl, {
                    params: { "id": id }
                }).success(function(result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        if (result.Data === true) {
                            if ($scope.Account.EducationJsonList.length > 0) {
                                $scope.Account.EducationJsonList.splice(index, 1);
                                alertSuccess("Selected Education Deleted Successfully.");
                            }

                        }
                        //$scope.Account.EducationJsonList.push(result.Data);


                    } else {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Unable to Delete Selected Education! " + result.Errors.toString();
                        alertError($scope.ErrorMsg);
                    }
                }).error(function(result, status) {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to Delete Selected Education! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                    alertError($scope.ErrorMsg);
                });
            }
        });//
    };


    $scope.filterSelectedDegree = function (education,isOnChange) {
        if (education.DegreeCategoryId != null) {
            var degree = $filter('filter')($scope.DegreeCategoryList, { Id: education.DegreeCategoryId },true)[0];
            if (degree != null) {
                education.BoardTypeEnumId = degree.BoardTypeEnumId;
            } else {
                education.BoardTypeEnumId = 0;
            }

            //for auto select Technical Board when selected Diploma
            if (isOnChange) {
                if (education.DegreeCategoryId == 607) { //Diploma=607
                    education.BoardId = 710; //for Technical=710
                    education.DegreeGroupMajorEnumId = 4; //for Diploma Or Technical
                } else {
                    education.DegreeGroupMajorEnumId = null;
                }
            }
            


            //$scope.showALevelOLevelModal(education.DegreeCategoryId);
        }

    };

    $scope.showALevelOLevelModal = function (degreeId) {
        /* Db Id number
 
            O Level=605
            A Level=606
         */
        //For O Level
        if (degreeId == 605) {
            $scope.NameOfModal = "O Level Details";
            $('#aLevelOLevelModal').modal('show');
        }

        //For A Level
        if (degreeId == 606) {
            $scope.NameOfModal = "A Level Details";
            $('#aLevelOLevelModal').modal('show');
        }
    };

    $scope.getAllEducationByUserId = function () {

        $http.get($scope.getAllEducationByUserIdUrl,
            {
                params: { "id": $scope.Account.Id }
            }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.Account.EducationJsonList = result.Data;

                $scope.onAfterLoadEducationMap();

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Sorry unable to get Education information! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Sorry unable to get Education information! " +
                JSON.stringify(result).toString() +
                ". " +
                "Status: " +
                JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    //============= End Previous Education===========



    //============= Start Package & Discount===========

    $scope.SelectedScholarship = [];
    $scope.SelectedFeeCode = [];
    $scope.SemesterBillOnFixedCost = 0;
    $scope.SemesterBillOnTuitionFee = 0;

    var oldValueInPercentAmount = 0;
    var oldValueInRealAmount = 0;


    $scope.onChangeFeeCode = function () {
        if ($scope.SelectedFeeCode != null) {
            $scope.Account.FeeCodeId = $scope.SelectedFeeCode.Id;
        }

    }

    $scope.viewDetailScholarship = function (discount) {
        $scope.SelectedScholarship = discount;
        $('#addDiscountModal').modal('show');
    };

    $scope.getNewScholarshipByStudentId = function () {

        $http.get($scope.getNewScholarshipByStudentIdUrl, {
            params: { "id": $scope.Account.Id }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.Data != null)
                    $scope.Account.ScholarshipJsonList.push(result.Data);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Another Scholarship! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Another Scholarship! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.loadStdScholarshipExtraData = function (stdScholarshipId) {
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
                if (result.DataExtra.SemesterListForScholarship != null)
                    $scope.SemesterListForScholarship = result.DataExtra.SemesterListForScholarship;
                if (result.DataExtra.ScholarshipTypeList != null)
                    $scope.ScholarshipTypeList = result.DataExtra.ScholarshipTypeList;

                
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
                    if (result.DataExtra.PerCredit!=null) {
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
    $scope.scholarshipCalculation = function(row, changeBy) {

        if (changeBy === "PercentAmount") {
            row.ChangeByTextFieldName = changeBy;
        }
        if (changeBy === "RealAmount") {
            row.ChangeByTextFieldName = changeBy;
        }


        /* Check ValidPeriodEnumId

            StartToUpcoming = 1,
            ThisSemesterOnly = 2,
            ThisToUpcoming = 3,

        */
        if (row.ValidPeriodEnumId === 1) // For StartToUpcoming = 1
        {
            var tuitionFee = $scope.SelectedFeeCode.PackageTuitionFee;
            var fixedCost = $scope.SelectedFeeCode.TotalPackageAmount - $scope.SelectedFeeCode.PackageTuitionFee;
            $scope.percentAndRealAmountCalculation(row, tuitionFee, fixedCost);

        }

        if (row.ValidPeriodEnumId === 2) // ThisSemesterOnly = 2,
        {
            $scope.percentAndRealAmountCalculation(row, $scope.SemesterBillOnTuitionFee, $scope.SemesterBillOnFixedCost);

        }

       
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

    $scope.getTrashScholarshipById = function (row, isPutInTrash) {

        if (row != null) {
            $scope.ScholarshipId = row.Id;
        }
        else {
            return;
        }
        var msg = "";
        if (isPutInTrash) {
            msg = "Delete";
        } else {
            msg = "Undelete";
        }
        
        bootbox.confirm("Are you sure you want to " + msg + " this data?", function (yes) {
            if (yes) {

                $http.get($scope.getTrashScholarshipByIdUrl,
                    {
                        params: {
                            "id": $scope.ScholarshipId,
                            "isPutInTrash": isPutInTrash
                        }
                    }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        if ($scope.Account.ScholarshipJsonList.length > 0) {
                            var index = $scope.Account.ScholarshipJsonList.indexOf(row);
                            $scope.Account.ScholarshipJsonList[index] = result.Data;
                        }
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

    $scope.onAfterLoadGetSemesterBillByStudentIdSemesterId = function(row) {
        $scope.scholarshipCalculation(row, null);

    };

    var isPercentChenge=false;
    $scope.onChangeIsPercent = function(row) {
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
    //================Start Academic=======================
    $scope.getConfirmOrCancelAdmissionByUserId = function (isConfirmAdmission) {

        var isCallServerApi = false;
        if (isConfirmAdmission) {
            var msg = "<span  class='text-danger'><i class='fa fa-question-circle'></i> " +
                "Are you sure want to Confirm Admission and Active this Student?" +
                "<span >";

            bootbox.confirm(msg, function (ok) {
                if (ok) {
                    msg = "";
                    $scope.getConfirmOrCancelAdmissionByUserIdApiCall(isConfirmAdmission);
                }
            });
            
        } else {

            $scope.getConfirmOrCancelAdmissionByUserIdApiCall(isConfirmAdmission);
           
        }
    };

    $scope.getConfirmOrCancelAdmissionByUserIdApiCall = function (isConfirmAdmission) {
        $http.get($scope.getConfirmOrCancelAdmissionByUserIdUrl,
                {
                    params: {
                        "id": $scope.Account.Id,
                        "isConfirmAdmission": isConfirmAdmission,
                        "enrollmentStatusEnumId": $scope.CancelAdmissionReasonEnumId,
                        "admissionRemark": $scope.Account.AdmissionRemark,
                        "isSendEmail": $scope.IsSendEmail
                    }
                }).success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    $scope.getAccountById($scope.AccountId);
                    if (isConfirmAdmission) {
                        if (result.DataExtra.Message != null) {
                            $scope.Message = result.DataExtra.Message;
                        }
                        if ($scope.IsSendEmail) {
                            alertSuccess("Successfully Confirm Admission this Student. Mail Sent Status:" + $scope.Message);
                        }
                        else {
                            alertSuccess("Successfully Confirm Admission this Student.");
                        }
                        
                    } else {
                        alertSuccess("Successfully Cancel Admission this Student!");
                        $('#CancelAdmissionModal').modal('hide');
                    }


                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg =
                        "Sorry unable to save Confirm Or Cancel Admission! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Sorry unable to save Confirm Or Cancel Admission! " +
                    JSON.stringify(result).toString() +
                    ". " +
                    "Status: " +
                    JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
        
    }


    //===============End Academic========================

    $scope.saveCertificateInformation = function () {
        if (!$scope.validateAccount()) {
            return;
        }

        $http.post($scope.saveCertificateInformationUrl + "/", $scope.Account).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    $scope.ErrorMsg = "";
                    if (result.Data != null) {
                        $scope.Account = result.Data;
                        $scope.getAccountById($scope.AccountId);
                        updateUrlQuery('id', $scope.Account.Id);
                    }
                    alertSuccess("Student Certificate Information Successfully Saved!");
                    $scope.onAfterSaveAccount(result);
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Certificate Information Saving Failed! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Certificate Information Saving Failed! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    };


    //=======================================
    $scope.saveUpdateUserNameGroup = function () {
        if (!$scope.validateAccount()) {
            return;
        }
        $http.post($scope.saveUpdateUserNameGroupUrl + "/", $scope.Account).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    $scope.ErrorMsg = "";
                    if (result.Data != null) {
                        $scope.Account = result.Data;
                        
                    }

                    $scope.getAccountById($scope.AccountId);
                    updateUrlQuery('id', $scope.Account.Id);

                    $scope.IsDisabledUserNameGroup = true;
                    alertSuccess("Update UserName Successfully!");
                    $scope.onAfterSaveAccount(result);
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Update UserName Failed! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Update UserName Failed! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    };

    $scope.saveUpdateStudentNameGroup = function () {
        if (!$scope.validateAccount()) {
            return;
        }
        $http.post($scope.saveUpdateStudentNameGroupUrl + "/", $scope.Account).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    $scope.ErrorMsg = "";

                    if (result.Data != null) {
                        $scope.Account = result.Data;
                    }

                    $scope.getAccountById($scope.AccountId);
                    updateUrlQuery('id', $scope.Account.Id);

                    $scope.IsDisabledStudentNameGroup = true;
                    alertSuccess("Update Student Name Successfully!");
                    $scope.onAfterSaveAccount(result);
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Update Student Name Failed! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Update Student Name Failed! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    };

    $scope.saveUpdateAcademicInfoGroup = function () {
        if (!$scope.validateAccount()) {
            return;
        }
        log($scope.Account);
        $http.post($scope.saveUpdateAcademicInfoGroupUrl + "/", $scope.Account).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    $scope.ErrorMsg = "";
                    if (result.Data != null) {
                        $scope.Account = result.Data;
                        $scope.getAccountById($scope.AccountId);
                        updateUrlQuery('id', $scope.Account.Id);
                    }
                    $scope.IsDisabledAcademicGroup = true;
                    alertSuccess("Update Academic Info Successfully!");
                    $scope.onAfterSaveAccount(result);
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Update Academic Info Failed! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Update Academic Info Failed! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    };

    $scope.renderHtml = function (html_code) {
        return $sce.trustAsHtml(html_code);
    };

    $scope.openHistoryModal = function () {
        $('#HistoryModal').modal('show');
    }

    $scope.resetPassword = function () {
        var msg = "<span  class='text-danger'><i class='fa fa-question-circle'></i> " +
            "Are you sure want to Reset Password and Sent Email?" +
            "<span >";
        bootbox.confirm(msg, function (ok) {
            if (ok) {
                $http.post($scope.saveResetAndSendPasswordUrl +
                        "/" + "?userId=" + $scope.Account.Id
                    ).
                    success(function (result, status) {
                        if (!result.HasError) {
                            $scope.HasError = false;
                            $scope.Message = "";
                            if (result.DataExtra.Message != null) {
                                $scope.Message = result.DataExtra.Message;
                            }
                            $scope.Account.Password = result.DataExtra.NewPassword;
                            //$scope.getAccountById($scope.AccountId);
                            alertSuccess("Successfully Reset password and Email Sent! " + $scope.Message);
                        } else {
                            $scope.HasError = true;
                            $scope.ErrorMsg = "Unable to Reset password and Sent Email! " + result.Errors.toString();
                            alertError($scope.ErrorMsg);
                        }

                    }).
                    error(function (result, status) {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Unable to changed password! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();

                    });
            }
        });
    };
    $scope.sendPassword = function () {
        var msg = "<span  class='text-warning'><i class='fa fa-question-circle'></i> " +
            "Are you sure want to Send Password?" +
            "<span >";
        bootbox.confirm(msg, function (ok) {
            if (ok) {
                $http.post($scope.saveSendPasswordUrl +
                        "/" + "?userId=" + $scope.Account.Id
                    ).
                    success(function (result, status) {
                        if (!result.HasError) {
                            $scope.HasError = false;
                            $scope.Message = "";
                            if (result.DataExtra.Message != null) {
                                $scope.Message = result.DataExtra.Message;
                            }
                            $scope.Account.Password = result.DataExtra.NewPassword;
                            //$scope.getAccountById($scope.AccountId);
                            alertSuccess("Successfully Send password! " + $scope.Message);
                        } else {
                            $scope.HasError = true;
                            $scope.ErrorMsg = "Unable to Send password! " + result.Errors.toString();
                            alertError($scope.ErrorMsg);
                        }

                    }).
                    error(function (result, status) {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Unable to Send password! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();

                    });
            }
        });
    };
    //========================================

    $scope.getRecalculateCGPAByStdId = function () {
        $http.get($scope.getRecalculateCGPAByStdIdUrl, {
            params: {
                "studentId": $scope.StudentId
            }
        }).success(function (result) {
            if (!result.HasError) {

                $scope.Account.CGPA = result.DataExtra.CGPA;
                $scope.Account.CourseCompleted = result.DataExtra.CourseCompleted;
                $scope.Account.CreditCompleted = result.DataExtra.CreditCompleted;
                $scope.Account.IncompleteCredits = result.DataExtra.IncompleteCredits;

                alertSuccess("Successfully Recalculate CGPA!");

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to Recalculate CGPA! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }

        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to Recalculate CGPA! " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);

        });
    };

    $scope.getRecalculateDropForRetakeByStdId = function () {
        //Call From \areas\academic\views\student\registrationmanager\_courselist.js
        studentRegistrationCtrl.getRecalculateDropForRetakeByStdIdCallAnotherCtrl();
    };

    $scope.CheckUserEmail = function () {
        if ($scope.IsSendEmail) {
            if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test($scope.Account.UserEmail)) {
                return true;
            }
            alertError("Please Provide a Valid Email Address.");
            $scope.IsSendEmail = false;
        }
    }

    $scope.IsValidEmail = function(){
        if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test($scope.Account.UserEmail)) {
            $scope.IsSendEmail = true;
            return true;
        }
        $scope.IsSendEmail = false;
        return false;
    }

//======Custom Variabales goes hare=====

    $scope.Init = function (
          AccountId
          , getAccountByIdUrl
        , getAccountExtraDataUrl
        , saveAccountUrl
        , deleteAccountByIdUrl
        , getEducationExtraDataUrl
        , getDataExtraOnChangeProgramUrl
        , getNewEducationByUserIdUrl
        , getDeleteEducationByIdUrl
        , getNewScholarshipByStudentIdUrl
        , getStdScholarshipDataExtraUrl
        , getLockOrUnlockScholarshipByIdUrl
        , getAllScholarshipByStudentIdUrl
        , getAllEducationByUserIdUrl
        , getConfirmOrCancelAdmissionByUserIdUrl
        , getSemesterBillByStudentIdSemesterIdUrl
        , saveCertificateInformationUrl
          ,saveUpdateUserNameGroupUrl
        , saveUpdateAcademicInfoGroupUrl
        , saveResetAndSendPasswordUrl
        , saveSendPasswordUrl
        , saveScholarshipListByStudentUrl
        , getTrashScholarshipByIdUrl
        , getRecalculateStudentPerCreditFeeUrl
        , getRegenerateStudentBillByStdIdUrl
          , getRecalculateCGPAByStdIdUrl
          , uploadProfilePictureUrl
          , removePhotoOrSignatureByIdUrl
          , saveUpdateStudentNameGroupUrl
    )
    {
        $scope.AccountId = AccountId;
        $scope.getAccountByIdUrl = getAccountByIdUrl;
        $scope.saveAccountUrl = saveAccountUrl;
        $scope.getAccountExtraDataUrl = getAccountExtraDataUrl;
        $scope.deleteAccountByIdUrl = deleteAccountByIdUrl;

        $scope.getDataExtraOnChangeProgramUrl = getDataExtraOnChangeProgramUrl;
        $scope.getNewEducationByUserIdUrl = getNewEducationByUserIdUrl;
        $scope.getDeleteEducationByIdUrl = getDeleteEducationByIdUrl;

        $scope.getEducationExtraDataUrl = getEducationExtraDataUrl;
        $scope.getNewScholarshipByStudentIdUrl = getNewScholarshipByStudentIdUrl;
        $scope.getStdScholarshipDataExtraUrl = getStdScholarshipDataExtraUrl;
        $scope.getLockOrUnlockScholarshipByIdUrl = getLockOrUnlockScholarshipByIdUrl;
        $scope.getSemesterBillByStudentIdSemesterIdUrl = getSemesterBillByStudentIdSemesterIdUrl;


        $scope.getAllScholarshipByStudentIdUrl = getAllScholarshipByStudentIdUrl;
        $scope.getAllEducationByUserIdUrl = getAllEducationByUserIdUrl;
        $scope.getConfirmOrCancelAdmissionByUserIdUrl = getConfirmOrCancelAdmissionByUserIdUrl;

        $scope.saveCertificateInformationUrl = saveCertificateInformationUrl;

        $scope.saveUpdateUserNameGroupUrl = saveUpdateUserNameGroupUrl;
        $scope.saveUpdateStudentNameGroupUrl = saveUpdateStudentNameGroupUrl;
        $scope.saveUpdateAcademicInfoGroupUrl = saveUpdateAcademicInfoGroupUrl;
        $scope.saveResetAndSendPasswordUrl = saveResetAndSendPasswordUrl;
        $scope.saveSendPasswordUrl = saveSendPasswordUrl;
        $scope.saveScholarshipListByStudentUrl = saveScholarshipListByStudentUrl;
        $scope.getTrashScholarshipByIdUrl = getTrashScholarshipByIdUrl;
        $scope.getRecalculateStudentPerCreditFeeUrl = getRecalculateStudentPerCreditFeeUrl;
        $scope.getRegenerateStudentBillByStdIdUrl = getRegenerateStudentBillByStdIdUrl;
        $scope.getRecalculateCGPAByStdIdUrl = getRecalculateCGPAByStdIdUrl;
        $scope.uploadProfilePictureUrl = uploadProfilePictureUrl;
        $scope.removePhotoOrSignatureByIdUrl = removePhotoOrSignatureByIdUrl;
        $scope.loadPage(AccountId);
    };
    
    $scope.onAfterSaveAccount= function(result){
     //var data=result.Data;
    };
    $scope.onAfterGetAccountById= function(data) {

        angular.forEach($scope.Account.EducationJsonList, function (value, key) {
            $scope.filterSelectedDegree(value,false);
        });

        //$scope.filterProgram();
        $scope.onAfterLoadAccountExtraData();
    };

    $scope.filterProgram = function () {
        if ($scope.Account.ProgramId != null || $scope.Account.ProgramId >= 0) {
            if ($scope.ProgramList !=null && $scope.ProgramList.length >= 1) {
                var program = $filter('filter')($scope.ProgramList, {Id: $scope.Account.ProgramId}, true)[0];
                $scope.SelectedProgram = program;
                $scope.onChangeProgram();

            }
        }
    };
    $scope.onAfterLoadAccountExtraData = function (result) {

        //Select first AdmissionExam From AdmissionExamList when Create mode
      /*if ($scope.Account.Id <= 0 || $scope.Account.Id == null) {
          if ($scope.AdmissionExamList.length > 0) {
              $scope.SelectedAdmissionExam = $scope.AdmissionExamList[0];
              $scope.Account.AdmissionExamId = $scope.AdmissionExamList[0].Id;
          }
  }*/
      // For Edit mode
        if ($scope.AccountId > 0) {

          $scope.filterProgram();

          if ($scope.Account.AdmissionExamId != null || $scope.Account.AdmissionExamId >= 0) {

              if ($scope.AdmissionExamList != null && $scope.AdmissionExamList.length >= 1) {
                  var admissionExam = $filter('filter')($scope.AdmissionExamList, {
                      Id: $scope.Account.AdmissionExamId
                  },true)[0];
                  $scope.SelectedAdmissionExam = admissionExam;
                  $scope.onChangeAdmissionExam();
              }
          }

          $scope.onAfterLoadEducationMap();

      }
          
  };

    $scope.onAfterLoadEducationMap = function() {
        angular.forEach($scope.Account.EducationJsonList, function (value, key) {
            $scope.filterSelectedDegree(value, false);
        });
    };

    $scope.onAfterLoadEducationExtraData = function (result) {
        //var DataExtra=result.DataExtra;
        //DropDown Option Tables
        if (result.DataExtra.DegreeCategoryList != null)
            $scope.DegreeCategoryList = result.DataExtra.DegreeCategoryList;
        if (result.DataExtra.EducationBoardList != null)
            $scope.EducationBoardList = result.DataExtra.EducationBoardList;
        if (result.DataExtra.AccountList != null)
            $scope.AccountList = result.DataExtra.AccountList;
        /*
        //Child Table Bindings, need to fix     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 

//======Custom property and Functions End========================================================== 
});

