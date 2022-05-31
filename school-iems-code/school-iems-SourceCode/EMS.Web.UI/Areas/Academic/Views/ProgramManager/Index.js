emsApp.controller('ProgramManagerCtrl', function ($scope, $http, $filter) {
    $scope.Program = [];
    $scope.ProgramList = [];
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 50;
    $scope.PageNo = 1;
    $scope.ProgramTypeEnumList = [];
    $scope.DepartmentList = [];
    $scope.SearchText = "";
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.SelectedDepartmentId = 0;
    $scope.CreditTypeSystemEnumId = 0;
    $scope.SelectedDurationEnumId = 0;
    
    $scope.selectedProgramIndex = 0;
    $scope.loadPage = function (ProgramId) {
        if (ProgramId != null)
            $scope.ProgramId = ProgramId;

        $scope.loadProgramExtraData($scope.ProgramId);
        $scope.getProgramList();
    };
    $scope.getNewProgram = function () {
        $scope.getProgramById(0);
    };
    $scope.getProgramById = function (ProgramId) {
        if (ProgramId != null && ProgramId !== '')
            $scope.ProgramId = ProgramId;
        else return;

        $http.get($scope.getProgramByIdUrl, {
            params: { "id": $scope.ProgramId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.Program = result.Data;
                updateUrlQuery('id', $scope.Program.Id);
                $scope.onAfterGetProgramById(result);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Program! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Program! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    /*Paging Option*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.search();
    };
    $scope.changePageNo = function () {
        $scope.search();
    };
    /*Search*/
    $scope.searchProgramList = function () {
        $scope.getPagedProgramList();
    };
    $scope.getPagedProgramList = function () {
        $scope.getProgramList();
    };
    $scope.refreshProgram = function () {
        $scope.getProgramList();
    };
    /*Get Program*/
    /*For Search on data end*/
    $scope.getProgramList = function () {
        $http.get($scope.getPagedProgramUrl, {
            params: {
                "textkey": $scope.SearchText
                ,"pageSize": $scope.PageSize
                ,"pageNo": $scope.PageNo
                ,"departmentId": $scope.SelectedDepartmentId
                , "selectedDurationEnumId": $scope.SelectedDurationEnumId
                , "creditTypeSystemEnumId": $scope.CreditTypeSystemEnumId
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.ProgramList = result.Data;
                $scope.editProgram($scope.selectedProgramIndex);
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Program list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Program list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.loadProgramExtraData = function (ProgramId) {
        if (ProgramId != null)
            $scope.ProgramId = ProgramId;

        $http.get($scope.getProgramExtraDataUrl, {
            params: { "id": $scope.ProgramId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.DataExtra.ProgramTypeEnumList != null)
                    $scope.ProgramTypeEnumList = result.DataExtra.ProgramTypeEnumList;
                if (result.DataExtra.ClassTimingGroupEnumList != null)
                    $scope.ClassTimingGroupEnumList = result.DataExtra.ClassTimingGroupEnumList;

                if (result.DataExtra.SemesterDurationEnumList != null)
                    $scope.SemesterDurationEnumList = result.DataExtra.SemesterDurationEnumList;

                if (result.DataExtra.CreditTypeSystemEnumList != null)
                    $scope.CreditTypeSystemEnumList = result.DataExtra.CreditTypeSystemEnumList;

                if (result.DataExtra.LanguageEnumList != null)
                    $scope.LanguageEnumList = result.DataExtra.LanguageEnumList;

                $scope.OfficialBankList = result.DataExtra.OfficialBankList;
                $scope.onAfterLoadProgramExtraData(result);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Program! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Program! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    /*Edit Program*/
    $scope.editProgram = function ($index) {
        $scope.Program = $scope.ProgramList[$index];
        $scope.selectedProgramIndex = $index;
    };
    /*Save Program*/
    $scope.saveProgram = function () {
        if (!$scope.validateProgram()) {
            return;
        }
        $http.post($scope.saveProgramUrl + "/", $scope.Program).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    if (result.Data != null) {
                        $scope.Program = result.Data;
                        updateUrlQuery('id', $scope.Program.Id);
                    }
                    alertSuccess("Successfully saved Program data!");
                    $scope.onAfterSaveProgram(result);
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to save Program! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to save Program! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    };
    $scope.validateProgram = function () {
        if ($scope.Program.Name == null || $scope.Program.Name === "") {
            alertError("Name can't be blank. ");
            return false;
        }
        return true;
    };
    /*Delete Program*/
    $scope.deleteProgramById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteProgramByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.ProgramList.indexOf(obj);
                        $scope.ProgramList.splice(i, 1);
                        $scope.editProgram($scope.selectedProgramIndex);
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
    //======Custom property and Functions Start=======================================================  
    //======Custom Variabales goes hare=====
    /*Initialize*/
    $scope.Init = function (
        ProgramId
        ,getPagedProgramUrl
        ,getProgramExtraDataUrl
        ,saveProgramUrl
        ,getProgramByIdUrl
        ,deleteProgramByIdUrl) {
        $scope.ProgramId = ProgramId;
        $scope.getPagedProgramUrl = RootApiUrl + getPagedProgramUrl;
        $scope.getProgramExtraDataUrl = RootApiUrl + getProgramExtraDataUrl;
        $scope.saveProgramUrl = RootApiUrl + saveProgramUrl;
        $scope.getProgramByIdUrl = RootApiUrl + getProgramByIdUrl;
        $scope.deleteProgramByIdUrl = RootApiUrl + deleteProgramByIdUrl;
        $scope.loadPage(ProgramId);
    };

    $scope.onAfterSaveProgram = function (result) {
        $scope.getProgramList();
        //var data=result.Data;
    };
    $scope.onAfterGetProgramById = function (result) {
        //var data=result.Data;

    };
    $scope.onAfterLoadProgramExtraData = function (result) {
        //var DataExtra=result.DataExtra;
        //DropDown Option Tables
        if (result.DataExtra.DepartmentList != null)
            $scope.DepartmentList = result.DataExtra.DepartmentList;
        /*
        //Child Table Bindings, need to fix
                   $scope.ProgramIdList =  result.DataExtra.ProgramIdList; 
      
                   $scope.ProgramIdList =  result.DataExtra.ProgramIdList; 
      
                   $scope.ProgramIdList =  result.DataExtra.ProgramIdList; 
      
                   $scope.ProgramIdList =  result.DataExtra.ProgramIdList; 
           */
    };
    //======Other tabale's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 
});