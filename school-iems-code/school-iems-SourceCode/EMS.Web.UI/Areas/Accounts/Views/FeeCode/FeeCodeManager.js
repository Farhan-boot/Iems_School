
//File: Acnt_FeeCode Anjular  Controller
emsApp.controller('FeeCodeAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.FeeCode = [];
    $scope.FeeCodeId = 0;
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;

    $scope.loadPage = function (FeeCodeId) {
        if (FeeCodeId != null)
            $scope.FeeCodeId = FeeCodeId;

        $scope.loadFeeCodeExtraData($scope.FeeCodeId);
        $scope.getFeeCodeById($scope.FeeCodeId);

        $scope.getFeeCodePolicyListExtraData();
        $scope.getPagedFeeCodePolicyList();
    };
    $scope.getNewFeeCode = function () {
        $scope.getFeeCodeById(0);
    };
    $scope.getFeeCodeById = function (FeeCodeId) {
        if (FeeCodeId != null && FeeCodeId !== '')
            $scope.FeeCodeId = FeeCodeId;
        else return;

        $http.get($scope.getFeeCodeByIdUrl, {
            params: { "id": $scope.FeeCodeId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.FeeCode = result.Data;
                updateUrlQuery('id', $scope.FeeCode.Id);
                $scope.onAfterGetFeeCodeById(result);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Fee Code! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Fee Code! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.loadFeeCodeExtraData = function (FeeCodeId) {
        if (FeeCodeId != null)
            $scope.FeeCodeId = FeeCodeId;

        $http.get($scope.getFeeCodeExtraDataUrl, {
            params: { "id": $scope.FeeCodeId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.DataExtra.StatusEnumList != null)
                    $scope.StatusEnumList = result.DataExtra.StatusEnumList;
                $scope.ProgramList = result.DataExtra.ProgramList;
                $scope.SemesterList = result.DataExtra.SemesterList;
                console.log($scope.SemesterList);
                $scope.onAfterLoadFeeCodeExtraData(result);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Fee Code! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Fee Code! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.saveFeeCode = function () {
        if (!$scope.validateFeeCode()) {
            return;
        }
        $http.post($scope.saveFeeCodeUrl + "/", $scope.FeeCode).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    if (result.Data != null) {
                        $scope.FeeCode = result.Data;
                        updateUrlQuery('id', $scope.FeeCode.Id);
                    }
                    alertSuccess("Successfully saved Fee Code data!");
                    $scope.onAfterSaveFeeCode(result);
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to save Fee Code! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to save Fee Code! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    };

    $scope.deleteFeeCodeById = function () {

    };
    $scope.validateFeeCode = function () {
        return true;
    };



    //======Custom property and Functions Start=======================================================  
    //======Custom Variabales goes hare=====

    $scope.Init = function (FeeCodeId
        , getFeeCodeByIdUrl
        , getFeeCodeExtraDataUrl
        , saveFeeCodeUrl
        , deleteFeeCodeByIdUrl
        , getFeeCodePolicyListByFeeCodeIdUrl
        , getFeeCodePolicyListDataExtraUrl
        , getFeeCodePolicyByIdUrl
        , saveFeeCodePolicyUrl
        , deleteFeeCodePolicyByIdUrl
     ) {
        $scope.FeeCodeId = FeeCodeId;
        $scope.getFeeCodeByIdUrl = getFeeCodeByIdUrl;
        $scope.saveFeeCodeUrl = saveFeeCodeUrl;
        $scope.getFeeCodeExtraDataUrl = getFeeCodeExtraDataUrl;
        $scope.deleteFeeCodeByIdUrl = deleteFeeCodeByIdUrl;
        $scope.getFeeCodePolicyListByFeeCodeIdUrl = getFeeCodePolicyListByFeeCodeIdUrl;
        $scope.getFeeCodePolicyListDataExtraUrl = getFeeCodePolicyListDataExtraUrl;
        //policy single row
        $scope.getFeeCodePolicyByIdUrl = getFeeCodePolicyByIdUrl;
        $scope.saveFeeCodePolicyUrl = saveFeeCodePolicyUrl;
        $scope.deleteFeeCodePolicyByIdUrl = deleteFeeCodePolicyByIdUrl;

        $scope.loadPage(FeeCodeId);
    };

    $scope.onAfterSaveFeeCode = function (result) {
        //var data=result.Data;
    };
    $scope.onAfterGetFeeCodeById = function (result) {
        //var data=result.Data;

    };
    $scope.onAfterLoadFeeCodeExtraData = function (result) {
        //var DataExtra=result.DataExtra;
        //DropDown Option Tables
        if (result.DataExtra.ProgramList != null)
            $scope.ProgramList = result.DataExtra.ProgramList;
        if (result.DataExtra.SemesterList != null)
            $scope.SemesterList = result.DataExtra.SemesterList;
    };
    //======Other tabale's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 
    $scope.makeName = function (obj) {
        var program = $filter("filter")($scope.ProgramList, { Id: $scope.FeeCode.ProgramId }, true);
        if (program != null && program.length > 0) {
            program = program[0].Name ? program[0].Name : "";
        }
        else
            program = "";

        var semester = $filter("filter")($scope.SemesterList, { Id: $scope.FeeCode.StartSemesterId }, true);
        if (semester != null && semester.length > 0) {
            semester = semester[0].Name ? " (" + semester[0].Name + ")" : "";
        } else semester = "";
        $scope.FeeCode.Name = program + semester;
    }
    

    //======Policy Code Policy Start and Functions End========================================================== 
    $scope.FeeCodePolicyId = 0;
    // FeeCodePolicyList
    $scope.FeeCodePolicyList = [];
    /*For FeeCodePolicy*/
    $scope.getPagedFeeCodePolicyList = function () {
        $scope.getFeeCodePolicyList();
    };
    $scope.getFeeCodePolicyList = function () {
        $http.get($scope.getFeeCodePolicyListByFeeCodeIdUrl, {
            params: { "feeCodeId": $scope.FeeCodeId }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.FeeCodePolicyList = result.Data;

                $scope.calculatePackage();
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Fee Code Policy list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Fee Code Policy list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };


    $scope.getNewFeeCodePolicyModal = function () {
        $scope.getFeeCodePolicyByIdModal(0);
  
        $('#FeeCodePolicyModal').modal('show');
    }

    $scope.getFeeCodePolicyByIdModal = function (FeeCodePolicyId) {
        if (FeeCodePolicyId != null && FeeCodePolicyId !== '')
            $scope.FeeCodePolicyId = FeeCodePolicyId;
        else return;

        $http.get($scope.getFeeCodePolicyByIdUrl, {
            params: { "id": $scope.FeeCodePolicyId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.FeeCodePolicy = result.Data;

                if ($scope.FeeCodePolicy!=null) {
                    $scope.FeeCodePolicy.FeeCodeId = parseInt($scope.FeeCodeId);
                }
               

                $('#FeeCodePolicyModal').modal('show');
                updateUrlQuery('policyId', $scope.FeeCodePolicy.Id);
          
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Fee Code Policy! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Fee Code Policy! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getFeeCodePolicyListExtraData = function () {
        $http.get($scope.getFeeCodePolicyListDataExtraUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.DataExtra.ParticularTypeEnumList != null)
                    $scope.ParticularTypeEnumList = result.DataExtra.ParticularTypeEnumList;
                if (result.DataExtra.FeeGenerateTermEnumList != null)
                    $scope.FeeGenerateTermEnumList = result.DataExtra.FeeGenerateTermEnumList;
                if (result.DataExtra.FeeCodeList != null)
                    $scope.FeeCodeList = result.DataExtra.FeeCodeList;
                if (result.DataExtra.ParticularNameList != null)
                    $scope.ParticularNameList = result.DataExtra.ParticularNameList;
                //log($scope.ParticularTypeEnumList);


            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Fee Code Policy! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Fee Code Policy! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.saveFeeCodePolicy = function () {

        $http.post($scope.saveFeeCodePolicyUrl + "/", $scope.FeeCodePolicy).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    if (result.Data != null) {
                        $scope.FeeCodePolicy = result.Data;
                        //if ($scope.FeeCodePolicyId==0)
                        { $scope.getPagedFeeCodePolicyList(); }
                        updateUrlQuery('policyId', $scope.FeeCodePolicy.Id);
                        $('#FeeCodePolicyModal').modal('hide');
                        $scope.calculatePackage();
                    }
                    alertSuccess("Successfully saved Fee Code Policy data!");
                    //$scope.onAfterSaveFeeCodePolicy(result);
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to save Fee Code Policy! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to save Fee Code Policy! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    };
    $scope.deleteFeeCodePolicyById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this Fee Code Policy?", function (yes) {
            if (yes) {
                $http.get($scope.deleteFeeCodePolicyByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.FeeCodePolicyList.indexOf(obj);
                        $scope.FeeCodePolicyList.splice(i, 1);
                        $scope.calculatePackage();
                        alertSuccess("Fee Code Policy successfully deleted!");
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
    /*public enum FeeGenerateTermEnum
    {
        PerSemester = 1,
        PerSemesterPerCredit = 2,
        OneTime = 3,
        Monthly = 4,
        Annually = 5
    }*/
    $scope.TotalAmount = 0;
    $scope.calculatePackage = function () {
        //setTimeout(function(){//pausing .5sec for data load time
        $scope.TotalAmount = 0;
      
            if ($scope.FeeCodePolicyList.length > 0) {
                angular.forEach($scope.FeeCodePolicyList, function(codePolicy, key) {
                    //console.log(key + ': ');
                    //console.log(codePolicy);
                    if (codePolicy.FeeGenerateTermEnumId === 3) //Acnt_FeeCodePolicy.FeeGenerateTermEnum.OneTime
                    {
                        codePolicy.Total = codePolicy.Amount * 1;
                        codePolicy.EquationTxt = codePolicy.Amount + "x1";
                    }
                    if (codePolicy.FeeGenerateTermEnumId === 4) //Acnt_FeeCodePolicy.FeeGenerateTermEnum.Monthly
                    {
                        codePolicy.Total = codePolicy.Amount * $scope.FeeCode.TotalYear * 12;
                        codePolicy.EquationTxt = codePolicy.Amount + "x" + $scope.FeeCode.TotalYear + "x12";
                    }
                    if (codePolicy.FeeGenerateTermEnumId === 1) //Acnt_FeeCodePolicy.FeeGenerateTermEnum.PerSemester
                    {
                        codePolicy.Total = codePolicy.Amount * $scope.FeeCode.TotalSemester;
                        codePolicy.EquationTxt = codePolicy.Amount + "x" + $scope.FeeCode.TotalSemester;
                    }
                    if (codePolicy.FeeGenerateTermEnumId === 5) //Acnt_FeeCodePolicy.FeeGenerateTermEnum.Annually
                    {
                        codePolicy.Total = codePolicy.Amount * $scope.FeeCode.TotalYear;
                        codePolicy.EquationTxt = codePolicy.Amount + "x" + $scope.FeeCode.TotalYear;
                    }

                    if (codePolicy.FeeGenerateTermEnumId === 2) //Acnt_FeeCodePolicy.FeeGenerateTermEnum.PerSemesterPerCredit
                    {
                        $scope.FeeCode.PackageTuitionFee = codePolicy.Total = codePolicy.Amount * $scope.FeeCode.TotalCredit;
                        codePolicy.EquationTxt = codePolicy.Amount + "x" + $scope.FeeCode.TotalCredit;
                    }
                    if (codePolicy.FeeGenerateTermEnumId === 6) //Acnt_FeeCodePolicy.FeeGenerateTermEnum.PerSemesterPerCourse
                    {
                        codePolicy.Total = codePolicy.Amount * $scope.FeeCode.TotalCourse;
                        codePolicy.EquationTxt = codePolicy.Amount + "x" + $scope.FeeCode.TotalCourse;
                    }

                    $scope.TotalAmount = $scope.TotalAmount + codePolicy.Total;
                });
            }
            //$scope.$apply(function() {
            //    $scope.FeeCodePolicyList
            //});//scope apply end
        //}, 500);
    }//calculation end
});

