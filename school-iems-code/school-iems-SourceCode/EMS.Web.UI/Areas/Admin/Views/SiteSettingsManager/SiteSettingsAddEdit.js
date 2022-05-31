
//File: Adm_SiteSettings Anjular  Controller
emsApp.controller('SiteSettingsAddEditCtrl',
    function($scope, $http, $filter, cfpLoadingBar) {
        $scope.SiteSettings = [];
        $scope.ErrorMsg = "";
        $scope.HasError = false;
        $scope.HasViewPermission = false;
        $scope.loadPage = function() {


            $scope.loadSiteSettingsDataExtra();
            $scope.getSiteSettingsById(false);
        };
        $scope.resetSiteSettingsFromFile = function () {
            $scope.getSiteSettingsById(true);
        };
        $scope.getSiteSettingsById = function(isReloadCache) {

            $http.get($scope.getSiteSettingsByIdUrl,
                {
                    params: { "isReloadCache": isReloadCache }
                }).success(function(result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    $scope.HasViewPermission = result.HasViewPermission;
                    $scope.SiteSettings = result.Data;
                    if (isReloadCache) {
                        alertSuccess("Cache Reloaded Successfully");
                    }
                    //log($scope.SiteSettings);
                    //updateUrlQuery('id', $scope.SiteSettings.Instance.Id);
                    $scope.onAfterGetSiteSettingsById(result);
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to get Site Settings! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).error(function(result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Site Settings! " +
                    JSON.stringify(result).toString() +
                    ". " +
                    "Status: " +
                    JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
        };
        $scope.loadSiteSettingsDataExtra = function() {
            $http.get($scope.getSiteSettingsDataExtraUrl,
                {
                    params: { }
                }).success(function(result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    if (result.DataExtra.ParticularList != null)
                        $scope.ParticularList = result.DataExtra.ParticularList;
                    if (result.DataExtra.GradeList != null)
                        $scope.GradeList = result.DataExtra.GradeList;
                    if (result.DataExtra.ExamList != null)
                        $scope.ExamList = result.DataExtra.ExamList;
                    if (result.DataExtra.SemesterList != null)
                        $scope.SemesterList = result.DataExtra.SemesterList;

                    if (result.DataExtra.StudentIdGenerateTypeEnumList != null)
                        $scope.StudentIdGenerateTypeEnumList = result.DataExtra.StudentIdGenerateTypeEnumList;

                    if (result.DataExtra.CreditTransBillGenTypeEnumList != null)
                        $scope.CreditTransBillGenTypeEnumList = result.DataExtra.CreditTransBillGenTypeEnumList;
                    //log($scope.Da);
                    $scope.onAfterLoadSiteSettingsDataExtra(result);
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to load option data for Site Settings! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).error(function(result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Site Settings! " +
                    JSON.stringify(result).toString() +
                    ". " +
                    "Status: " +
                    JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
        };
        $scope.saveSiteSettings = function() {
            if (!$scope.validateSiteSettings()) {
                return;
            }

            $http.post($scope.saveSiteSettingsUrl + "/", $scope.SiteSettings).success(function(result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    if (result.Data != null) {
                        $scope.SiteSettings = result.Data;
                    }
                    alertSuccess("Successfully saved Site Settings data!");
                    $scope.onAfterSaveSiteSettings(result);
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to save Site Settings! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).error(function(result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to save Site Settings! " +
                    JSON.stringify(result).toString() +
                    ". " +
                    "Status: " +
                    JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
        };

        $scope.validateSiteSettings = function() {
            return true;
        };
        //======Custom property and Functions Start=======================================================  
        //======Custom Variabales goes hare=====

        $scope.Init = function(
            getSiteSettingsByIdUrl,
            saveSiteSettingsUrl,
            getSiteSettingsDataExtraUrl
        ) {
            $scope.getSiteSettingsByIdUrl = getSiteSettingsByIdUrl;
            $scope.saveSiteSettingsUrl = saveSiteSettingsUrl;
            $scope.getSiteSettingsDataExtraUrl = getSiteSettingsDataExtraUrl;

            $scope.loadPage($scope.SiteSettingsId);
        };

        $scope.onAfterSaveSiteSettings = function(result) {
            //var data=result.Data;
            return true;
        };
        $scope.onAfterGetSiteSettingsById = function(result) {
            //var data=result.Data;
            return true;

        };
        $scope.onAfterLoadSiteSettingsDataExtra = function(result) {
            return true;
        };
        //======Other tabale's get save Functions start============================================================== 

        //======Supporting Functions start================================================================ 

        //======Custom property and Functions End========================================================== 
    });

