
//File: User_Account Anjular  Controller
emsApp.controller('EmployeeProfileManager', function ($scope, $http, $filter, cfpLoadingBar, $sce) {
    $scope.Account = [];
    $scope.AccountId = "";
    $scope.EmpId = "";
    $scope.EducationId = 0;
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $('#isPhotoUploadAble').hide();
    $('#isPhotoRemoveable').hide();

    $scope.SelectedFileForUpload = null;

    $scope.SelectedAdmissionExam = [];
    $scope.SelectedProgram = [];

   $scope.loadPage= function (accountId)
    {
        $scope.getEmployeeAccountDataExtra();
       $scope.loadEducationExtraData($scope.EducationId);
       $scope.getAccountById($scope.AccountId);

    };



   $scope.getAccountById= function(accountId)
    {
       if (accountId != null && accountId !== '')
            $scope.AccountId = accountId;
        else return;
        
        $http.get($scope.getAccountByIdUrl, {
            params: { "userId": $scope.AccountId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.Account = result.Data;
                $scope.HasViewPermission = result.HasViewPermission;
                //updateUrlQuery('id', $scope.Account.Id); 
                $scope.onAfterGetAccountById($scope.Account);

                //console.log("AccountById");
               // console.log(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg = "Unable to get Employee Profile! " + result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg = "Unable to get Employee Profile! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
             alertError($scope.ErrorMsg);
        });
   };

   $scope.getEmployeeAccountDataExtra = function () {
       //log("Call Data Extra");
       $http.get($scope.getEmployeeAccountDataExtraUrl)
           .success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    if (result.DataExtra.UserTypeEnumList != null)
                        $scope.UserTypeEnumList = result.DataExtra.UserTypeEnumList;
                    if (result.DataExtra.UserStatusEnumList != null)
                        $scope.UserStatusEnumList = result.DataExtra.UserStatusEnumList;
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

                    if (result.DataExtra.SemesterList != null)
                        $scope.SemesterList = result.DataExtra.SemesterList;

                    if (result.DataExtra.RelationshipList != null)
                        $scope.RelationshipList = result.DataExtra.RelationshipList;

                    if (result.DataExtra.UserContactPrivacyEnumList != null)
                        $scope.UserContactPrivacyEnumList = result.DataExtra.UserContactPrivacyEnumList;

                    if (result.DataExtra.DepartmentList != null)
                        $scope.DepartmentList = result.DataExtra.DepartmentList;

                    if (result.DataExtra.CampusList != null)
                        $scope.CampusList = result.DataExtra.CampusList;
                    
                    if (result.DataExtra.IncrementMonthEnumList != null)
                        $scope.IncrementMonthEnumList = result.DataExtra.IncrementMonthEnumList;
                    
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

                    if (result.DataExtra.RankList != null)
                        $scope.RankList = result.DataExtra.RankList;

                    if (result.DataExtra.EmployeeList != null)
                        $scope.EmployeeList = result.DataExtra.EmployeeList;

                        $scope.onAfterLoadAccountExtraData(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable supporting data for Employee Profile! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg = "Unable supporting data for Employee Profile! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
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
                        //updateUrlQuery('id', $scope.Account.Id);
                   }
                   alertSuccess("Employee Profile Successfully Saved!");
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

   $scope.deleteAccountById= function(){
    	
    };
   $scope.validateAccount= function(){
        return true;
   };

   $scope.renderHtml = function (html_code) {
       return $sce.trustAsHtml(html_code);
   };

   $scope.openHistoryModal = function () {
       $('#HistoryModal').modal('show');
   }

//======Custom property and Functions Start=======================================================  

   $scope.removeUploadedPhoto = function () {
       bootbox.confirm("Are you sure you want to remove Employee's Profile Picture?",
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
                       $scope.ErrorMsg = "Unable to load option data for Employee! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
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
                    $scope.ErrorMsg = "Unable to Upload Profile Photo! " + result.Errors.toString();
                    $("#profilePhoto").attr("src", "/assets/img/avatars/BlankPerson.png");
                    alertError($scope.ErrorMsg);
                }
            })
            .error(function (result, status) {
                //log(result);
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
                //console.log("EducationExtra");
                //console.log(result);
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
                //console.log("getNewEducationByUserId");
                //console.log(result);
                //console.log("NewEducationList");
                //console.log($scope.Account.EducationJsonList);
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
        if (education.DegreeCategoryId != null ) {
            var degree = $filter('filter')($scope.DegreeCategoryList, { Id: education.DegreeCategoryId })[0];
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


    //================Start Academic=======================
    
    $scope.getConfirmOrCancelAdmissionByUserId = function () {

        $http.get($scope.getConfirmOrCancelAdmissionByUserIdUrl,
            {
                params: { "id": $scope.Account.Id }
            }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.getAccountById($scope.AccountId);

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Sorry unable to save Confirm Or Cancel Admission! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Sorry unable tosave Confirm Or Cancel Admission! " +
                JSON.stringify(result).toString() +
                ". " +
                "Status: " +
                JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    //===============End Academic========================


    //===============Start User-Credential====================

    $scope.IsForceToGenerateUsername = false;
    $scope.IsSendEmailOnApproval = false;

    $scope.saveApproval = function (isApproved) {
        $scope.IsApproved = isApproved;
        if ($scope.IsApproved) {
            if ($scope.IsSendEmailOnApproval) {
                var msg = "<span  class='text-danger'><i class='fa fa-question-circle'></i> " +
                    "Are you sure want to Approved then send Email and Password?" +
                    "<span >";
            } else {
                var msg = "<span  class='text-danger'><i class='fa fa-question-circle'></i> " +
                    "Are you sure want to Approved and Active this Employee?" +
                    "<span >";
            }
            
        } else {
            var msg = "<span  class='text-danger'><i class='fa fa-question-circle'></i> " +
                "Are you sure want to Disapproved?" +
                "<span >";
        }
        bootbox.confirm(msg, function (ok) {
            if (ok) {
                msg = "";

                $http.post($scope.saveApprovalUrl +
                    "/" + "?userId=" + $scope.Account.Id +
                    "&isApproved=" + $scope.IsApproved +
                    "&isSendEmailOnApproval=" + $scope.IsSendEmailOnApproval +
                    "&isForceToGenerateUsername=" + $scope.IsForceToGenerateUsername
                ).
                success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        $scope.Message = "";
                        if (result.DataExtra.UserName != null) {
                            $scope.Account.UserName = result.DataExtra.UserName;
                        }
                        if (result.DataExtra.Message != null) {
                            $scope.Message = result.DataExtra.Message;
                        }
                        $scope.Account.IsApproved = $scope.IsApproved;

                        $scope.getAccountById($scope.AccountId);
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
                error(function (result, status) {
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

    $scope.resetAndSendPassword = function () {
        var msg = "<span  class='text-danger'><i class='fa fa-question-circle'></i> " +
            "Are you sure want to Reset and send Password?" +
            "<span >";
        bootbox.confirm(msg, function (ok) {
            if (ok) {
                $http.post($scope.resetAndSendPasswordUrl +
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
                    alertSuccess("Successfully Reset and Send password! " + $scope.Message);
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to Reset and Send password! " + result.Errors.toString();
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
    //===============End User-Credential====================


    //======Custom Variabales goes hare=====

    $scope.Init = function (
        AccountId
        ,empId
        , getAccountByIdUrl
        , getEmployeeAccountDataExtraUrl
        , saveAccountUrl
        , deleteAccountByIdUrl
        , getEducationExtraDataUrl
        , getNewEducationByUserIdUrl
        , getDeleteEducationByIdUrl
        , getAllEducationByUserIdUrl
        , resetAndSendPasswordUrl
        , saveApprovalUrl
        , uploadProfilePictureUrl
        , removePhotoOrSignatureByIdUrl
    ) {

        $scope.AccountId = AccountId;
        $scope.EmpId = empId;
        $scope.getAccountByIdUrl = getAccountByIdUrl;
        $scope.getEmployeeAccountDataExtraUrl = getEmployeeAccountDataExtraUrl;
        $scope.saveAccountUrl = saveAccountUrl,
        $scope.deleteAccountByIdUrl = deleteAccountByIdUrl;
        $scope.getNewEducationByUserIdUrl = getNewEducationByUserIdUrl;
        $scope.getDeleteEducationByIdUrl = getDeleteEducationByIdUrl;
        $scope.getEducationExtraDataUrl = getEducationExtraDataUrl;
        $scope.getAllEducationByUserIdUrl = getAllEducationByUserIdUrl;
        $scope.resetAndSendPasswordUrl = resetAndSendPasswordUrl;
        $scope.saveApprovalUrl = saveApprovalUrl;
        $scope.uploadProfilePictureUrl = uploadProfilePictureUrl;
        $scope.removePhotoOrSignatureByIdUrl = removePhotoOrSignatureByIdUrl;

        $scope.loadPage();
    };
    
    $scope.onAfterSaveAccount= function(result){
     //var data=result.Data;
    };
    $scope.onAfterGetAccountById= function(data) {

        angular.forEach($scope.Account.EducationJsonList, function (value, key) {
            $scope.filterSelectedDegree(value,false);
        });
    };
    $scope.onAfterLoadAccountExtraData = function (result) {
        // For Edit mode
      if ($scope.AccountId > 0) {
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

