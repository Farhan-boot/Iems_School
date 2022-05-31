
//File:BasicAccounting_Ledger List Anjular  Controller
emsApp.controller('LedgerListCtrl', function($scope, $http, $filter, cfpLoadingBar) {
    $scope.LedgerList = [];
    $scope.SelectedLedger = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
    $scope.SelectedBranchId = 0;
    $scope.SelectedCompanyId = 0;

    $scope.IsTreeEnable = true;

    var parentIdList = [];
    var searchedId = 0;

    //search items
    $scope.SearchText = "";
    $scope.totalItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.SelectedLedgerForSearch = [];

    $scope.loadPage = function() {
        $scope.getLedgerListExtraData();
        //$scope.getPagedLedgerList();

    };
    /*For Search on data start*/

    $scope.searchLedgerList = function() {
        $scope.getPagedLedgerList();
    };

    $scope.getPagedLedgerList = function() {
        $scope.getLedgerList();
    };
    /*For Search on data end*/
    $scope.getLedgerList = function() {
        $http.get($scope.getAllLedgerUrl,
            {
                params: {
                    "branchId": $scope.SelectedBranchId
                }
            }).success(function(result) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.LedgerList = result.Data;
                log($scope.LedgerList);
                $scope.totalItems = result.Count;

                parentIdList = [];

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Ledger list data! " + result.Errors.toString();
                parentIdList = [];
                alertError($scope.ErrorMsg);
            }
        }).error(function(result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Ledger list! " +
                "Status: " +
                JSON.stringify(status).toString() +
                ". " +
                JSON.stringify(result).toString();
            parentIdList = [];
            alertError($scope.ErrorMsg);
        });
    };

    $scope.treeTable = function (index) {

        if (!$scope.IsTreeEnable) {
            return;
        }
        //log(id);
        if ($scope.totalItems === index) {
            setTimeout(function() {
                    //alert("Hello");
                    //$scope.treeTable();
                    $("table").agikiTreeTable({ persist: false, persistStoreName: "files" });
                },
                1000);
        }

    }

    $scope.getLedgerListExtraData = function() {
        $http.get($scope.getLedgerListDataExtraUrl,
            {
                params: {}
            }).success(function(result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.DataExtra.TypeEnumList != null)
                    $scope.TypeEnumList = result.DataExtra.TypeEnumList;
                //DropDown Option Tables
                if (result.DataExtra.BranchList != null) {
                    $scope.BranchList = result.DataExtra.BranchList;
                    $scope.SelectedBranchId = result.DataExtra.SelectedBranchId;
                }

                if (result.DataExtra.CompanyAccountList != null)
                    $scope.CompanyAccountList = result.DataExtra.CompanyAccountList;

                // Empty Ledger Json
                $scope.EmptyLedgerJson = result.DataExtra.EmptyLedgerJson;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Ledger! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function(result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Ledger! " +
                JSON.stringify(result).toString() +
                ". " +
                "Status: " +
                JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    $scope.getLedgerById = function(LedgerId) {
        if (LedgerId != null && LedgerId !== '')
            $scope.LedgerId = LedgerId;
        else return;

        $http.get($scope.getLedgerByIdUrl,
            {
                params: { "id": $scope.LedgerId }
            }).success(function(result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.Ledger = result.Data;
                log($scope.Ledger);
                $('#addLedgerModal').modal('show');
                //updateUrlQuery('id', $scope.Ledger.Id);
                //$scope.onAfterGetLedgerById(result);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Ledger! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function(result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Ledger! " +
                JSON.stringify(result).toString() +
                ". " +
                "Status: " +
                JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.saveLedger = function() {
        $http.post($scope.saveLedgerUrl + "/", $scope.Ledger).success(function(result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.Data != null) {
                    $scope.Ledger = result.Data;
                    //updateUrlQuery('id', $scope.Ledger.Id);
                    $('#addLedgerModal').modal('hide');
                    $scope.getLedgerList();

                }
                alertSuccess("Successfully saved Ledger data!");
                //$scope.onAfterSaveLedger(result);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to save Ledger! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function(result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to save Ledger! " +
                JSON.stringify(result).toString() +
                ". " +
                "Status: " +
                JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };


    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllLedgerList = function() {
        $scope.IsLoading++;
        $http.get($scope.getLedgerListUrl,
            {
                params: {}
            }).success(function(result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.LedgerList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Ledger list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function(result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Ledger list! " +
                JSON.stringify(result).toString() +
                ". " +
                "Status: " +
                JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.deleteLedgerById = function(obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?",
            function(yes) {
                if (yes) {
                    $http.get($scope.deleteLedgerByIdUrl,
                        {
                            params: { "id": obj.Id }
                        }).success(function(result, status) {
                        if (!result.HasError) {
                            $scope.HasError = false;
                            var i = $scope.LedgerList.indexOf(obj);
                            $scope.LedgerList.splice(i, 1);
                            alertSuccess("Data successfully deleted!");
                        } else {
                            $scope.HasError = true;
                            $scope.ErrorMsg = "Sorry unable to delete! " + result.Errors.toString();
                            alertError($scope.ErrorMsg);
                        }
                    }).error(function(result, status) {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Sorry unable to delete! " +
                            "Status: " +
                            JSON.stringify(status).toString() +
                            ". " +
                            JSON.stringify(result).toString();
                        alertError($scope.ErrorMsg);
                    });
                }
            });
    };
    /*$scope.deleteLedgerByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteLedgerByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.LedgerList.indexOf(obj);
                        $scope.LedgerList.splice(i, 1);
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

    $scope.selectAll = function($event) {
        var checkbox = $event.target;
        for (var i = 0; i < $scope.LedgerList.length; i++) {
            var entity = $scope.LedgerList[i];
            entity.IsSelected = checkbox.checked;
        }
    };
    //======Custom property and Functions Start=======================================================  

    $scope.addNewChildLedger = function(obj) {

        $scope.Ledger = angular.copy($scope.EmptyLedgerJson);

        $scope.Ledger.ParentName = "Child of " + obj.Code + "-" + obj.Name;
        $scope.Ledger.ParentId = obj.Id;
        $scope.Ledger.BranchId = $scope.SelectedBranchId;
        $scope.Ledger.CodeName = obj.Type;

        $('#addLedgerModal').modal('show');
    }

    $scope.onChangeTypeEnum=function(obj) {
        var type = $filter('filter')($scope.TypeEnumList, { Id: obj.TypeEnumId }, true)[0];
        $scope.Ledger.CodeName = type.Name;
    }
    

    $scope.searchLedger = function(obj) {
        $scope.SelectedLedgerForSearch = obj;

        if (parentIdList.length > 0) {
            $scope.closeOpenedLedgerPath();
        }

        $scope.openSearchedLedgerPath(obj.Id);
    }

    $scope.openSearchedLedgerPath = function (id) {

        // Searched Row
        searchedId = id;
        var searchedTr = $("#" + searchedId);
        searchedTr.css({ 'background-color': '#7ec77e' });

        //Make Parent List Loop Start
        for (; ;) {

            var childTr = $("#" + id);

            var parentId = childTr.attr('data-tt-parent-id');

            var isGroup = childTr.attr('is-group');

            // Check Group
            if (isGroup === "true") {
                parentIdList.push(id);
            }

            // Check Root Parent
            if (parentId === "") {
                break;
            }

            id = parentId;

        }//Make Parent List Loop End


        //  expand Loop Start
        for (var i = parentIdList.length; i >= 0; i--) {
            var expandableId = parentIdList[i];
            var selector = "#" + expandableId + " span.indenter a";
            var arrowTag = $(selector);
            arrowTag.click();

        }//  expand Loop End

        setTimeout(function () {
                location.href = "#" + searchedId;
            },100);

    }//end Function

    $scope.closeOpenedLedgerPath = function () {

        //collapsed Loop Start
        for (var i =0 ; i <parentIdList.length; i++) {
            var expandableId = parentIdList[i];
            var selector = "#" + expandableId + " span.indenter a";
            var arrowTag = $(selector);
            arrowTag.click();

        }//collapsed Loop End

        var searchedTr = $("#" + searchedId);
        searchedTr.css({ 'background-color': '' });

        parentIdList = [];


    }//closeOpenedLedgerPath Function End

    //======Custom Variables goes hare=====

    
    $scope.Init = function (
        getAllLedgerUrl
        ,deleteLedgerByIdUrl
        ,getLedgerListDataExtraUrl
        ,saveLedgerListUrl
        ,getLedgerByIdUrl
        ,getLedgerDataExtraUrl
        ,saveLedgerUrl
        ) {
        $scope.getAllLedgerUrl = getAllLedgerUrl;
        $scope.deleteLedgerByIdUrl = deleteLedgerByIdUrl;
        /*bind extra url if need*/
        $scope.getLedgerListDataExtraUrl = getLedgerListDataExtraUrl;
        $scope.saveLedgerListUrl = saveLedgerListUrl;
        $scope.getLedgerByIdUrl = getLedgerByIdUrl;
        $scope.getLedgerDataExtraUrl = getLedgerDataExtraUrl;
        $scope.saveLedgerUrl = saveLedgerUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



