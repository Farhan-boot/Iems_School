
//File:Admin_ProfileChangeAudit List Anjular  Controller
emsApp.controller('ProfileChangeAuditListCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.ProfileChangeAuditList = [];
    $scope.SelectedProfileChangeAudit = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
     $scope.SelectedField=null;      
    $scope.SelectedStartSemesterId=0;
    $scope.SelectedEndSemesterId=0;
    $scope.oldPk=null;
    $scope.newPK=null;
    $scope.UserName=null;
    $scope.NewUserName=null;
    $scope.NewValue=null;
    $scope.OldValue=null;
    $scope.StartDate=null;
    $scope.EndDate=null;
    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
      $scope.getProfileChangeAuditListExtraData();
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedProfileChangeAuditList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedProfileChangeAuditList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedProfileChangeAuditList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedProfileChangeAuditList();
    };
    $scope.searchProfileChangeAuditList = function () {
        $scope.getPagedProfileChangeAuditList();
    };
    $scope.getPagedProfileChangeAuditList = function () {
        $scope.getProfileChangeAuditList();
    };
    /*For Search on data end*/
    $scope.getProfileChangeAuditList = function () {
        if ($scope.SelectedField == null) {
            alertError("Please Select a Valid Log Field");
            $scope.ProfileChangeAuditList = [];
            return;
        }

        $http.get($scope.getPagedProfileChangeAuditUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
            , "fieldEnumId": $scope.SelectedField.Id
                , "startSemesterId": $scope.SelectedStartSemesterId
                , "endSemesterId": $scope.SelectedEndSemesterId
                , "oldPk": $scope.oldPk
                , "newPK": $scope.newPK
                , "userName": $scope.UserName
                , "newUserName": $scope.NewUserName
                , "oldValue": $scope.OldValue
                , "newValue": $scope.NewValue
                , "startDate": $scope.StartDate
                , "endDate": $scope.EndDate
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.ProfileChangeAuditList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.ProfileChangeAuditList = [];
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Profile Change Audit list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Profile Change Audit list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getProfileChangeAuditListExtraData= function()
    {
            $http.get($scope.getProfileChangeAuditListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if (result.DataExtra.FieldEnumList != null) {
                          $scope.FieldEnumList = result.DataExtra.FieldEnumList;
                          $scope.SelectedField = result.DataExtra.FieldEnumList[0];
                      }

                    if (result.DataExtra.SemesterList != null)
                        $scope.SemesterList = result.DataExtra.SemesterList;

                    if (result.DataExtra.ProgramList != null)
                        $scope.ProgramList = result.DataExtra.ProgramList;
                  //DropDown Option Tables
                     if(result.DataExtra.AccountList!=null)
                       $scope.AccountList =  result.DataExtra.AccountList;

                     $scope.getPagedProfileChangeAuditList();
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Profile Change Audit! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Profile Change Audit! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllProfileChangeAuditList = function () {
        $scope.IsLoading++;
        $http.get($scope.getProfileChangeAuditListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.ProfileChangeAuditList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Profile Change Audit list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Profile Change Audit list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteProfileChangeAuditById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteProfileChangeAuditByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.ProfileChangeAuditList.indexOf(obj);
                        $scope.ProfileChangeAuditList.splice(i, 1);
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
    /*$scope.deleteProfileChangeAuditByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteProfileChangeAuditByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.ProfileChangeAuditList.indexOf(obj);
                        $scope.ProfileChangeAuditList.splice(i, 1);
                       alertSuccess("Data successfully deleted!");
                    } else {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Sorry unable to delete! " + result.Errors.toString();
                        alertError($scope.ErrorMsg);
                    }
                }).error(function (result, status) {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Sorry unable to delete! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                    alertError($scope.ErrorMsg);
                });
            }
        });
    };*/

    $scope.selectAll = function ($event) {
    var checkbox = $event.target;
    for (var i = 0; i < $scope.ProfileChangeAuditList.length; i++) {
        var entity = $scope.ProfileChangeAuditList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedProfileChangeAuditUrl
        ,deleteProfileChangeAuditByIdUrl
        ,getProfileChangeAuditListDataExtraUrl
        ,saveProfileChangeAuditListUrl
        ,getProfileChangeAuditByIdUrl
        ,getProfileChangeAuditDataExtraUrl
        ,saveProfileChangeAuditUrl
        ) {
        $scope.getPagedProfileChangeAuditUrl = getPagedProfileChangeAuditUrl;
        $scope.deleteProfileChangeAuditByIdUrl = deleteProfileChangeAuditByIdUrl;
        /*bind extra url if need*/
        $scope.getProfileChangeAuditListDataExtraUrl = getProfileChangeAuditListDataExtraUrl;
        $scope.saveProfileChangeAuditListUrl = saveProfileChangeAuditListUrl;
        $scope.getProfileChangeAuditByIdUrl = getProfileChangeAuditByIdUrl;
        $scope.getProfileChangeAuditDataExtraUrl = getProfileChangeAuditDataExtraUrl;
        $scope.saveProfileChangeAuditUrl = saveProfileChangeAuditUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



