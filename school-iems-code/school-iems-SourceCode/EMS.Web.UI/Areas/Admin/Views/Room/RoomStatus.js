
//File:Admin_Room List Anjular  Controller
emsApp.controller('RoomStatusCtrl', function ($scope, $http, $filter, cfpLoadingBar) {
    $scope.RoomList = [];
    $scope.SelectedRoom = [];
    $scope.CurrentRoom = [];
    $scope.ClassRoutineList = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.SelectedStatusEnumId = -1;
    $scope.SelectedTypeEnumId = -1;
    $scope.SelectedFloorId = -1;
    $scope.SelectedCampusId = 0;
    $scope.SelectedBuildingId = 0;
    $scope.SelectedDepartmentId = 0;
    $scope.StartDateTime = null;
    $scope.EndDateTime = null;
    //search items
    $scope.SearchRoomName = "";
    $scope.SearchRoomNo = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;

    $scope.loadPage = function () {
        $scope.getRoomListExtraData();
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedRoomList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedRoomList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedRoomList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedRoomList();
    };
    $scope.searchRoomList = function () {
        if ($scope.StartDateTime==null) {
            alertError("Invalid Start Date Time!");
        }
        else if ($scope.EndDateTime == null) {
            alertError("Invalid End Date Time!");
        }
        else if ($scope.EndDateTime < $scope.StartDateTime) {
            alertError("Invalid End Date Time! End Date Time must be greater than Start Date Time");
        }
        else{
            $scope.getPagedRoomList();
        }
    };
    $scope.getPagedRoomList = function () {
        $scope.getRoomList();
    };
    /*For Search on data end*/
    $scope.getRoomList = function () {
        $http.get($scope.getPagedRoomUrl, {
            params: {
            "roomName": $scope.SearchRoomName
            ,"roomNo": $scope.SearchRoomNo
            ,"startDateTime": $scope.StartDateTime
            ,"endDateTime": $scope.EndDateTime
            , "statusEnumId": $scope.SelectedStatusEnumId
            , "typeEnumId": $scope.SelectedTypeEnumId
            , "floorId": $scope.SelectedFloorId
            , "campusId": $scope.SelectedCampusId
            , "buildingId": $scope.SelectedBuildingId
            , "departmentId": $scope.SelectedDepartmentId
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.RoomList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
                $scope.ClassRoutineList = result.DataExtra.ClassRoutineList;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Room list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Room list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getRoomListExtraData = function () {
        $http.get($scope.getRoomListDataExtraUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.DataExtra.TypeEnumList != null)
                    $scope.TypeEnumList = result.DataExtra.TypeEnumList;
                if (result.DataExtra.StatusEnumList != null)
                    $scope.StatusEnumList = result.DataExtra.StatusEnumList;
                //DropDown Option Tables
                if (result.DataExtra.CampusList != null)
                    $scope.CampusList = result.DataExtra.CampusList;

                if (result.DataExtra.BuildingList != null)
                    $scope.BuildingList = result.DataExtra.BuildingList;

                if (result.DataExtra.FloorList != null)
                    $scope.FloorList = result.DataExtra.FloorList;

                if (result.DataExtra.DepartmentList != null)
                    $scope.DepartmentList = result.DataExtra.DepartmentList;

                if (result.DataExtra.StartDateTime != null)
                    $scope.StartDateTime = result.DataExtra.StartDateTime;

                if (result.DataExtra.EndDateTime != null)
                    $scope.EndDateTime = result.DataExtra.EndDateTime;

                $scope.getPagedRoomList();

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Room! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Room! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllRoomList = function () {
        $scope.IsLoading++;
        $http.get($scope.getRoomListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.RoomList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Room list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Room list! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.deleteRoomById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteRoomByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.RoomList.indexOf(obj);
                        $scope.RoomList.splice(i, 1);
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

    $scope.selectAll = function ($event) {
        var checkbox = $event.target;
        for (var i = 0; i < $scope.RoomList.length; i++) {
            var entity = $scope.RoomList[i];
            entity.IsSelected = checkbox.checked;
        }
    };
    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====


    $scope.Init = function (
         getPagedRoomUrl
        , deleteRoomByIdUrl
        , getRoomListDataExtraUrl
        , saveRoomListUrl
        , getRoomByIdUrl
        , getRoomDataExtraUrl
        , saveRoomUrl
        ) {
        $scope.getPagedRoomUrl = getPagedRoomUrl;
        $scope.deleteRoomByIdUrl = deleteRoomByIdUrl;
        /*bind extra url if need*/
        $scope.getRoomListDataExtraUrl = getRoomListDataExtraUrl;
        $scope.saveRoomListUrl = saveRoomListUrl;
        $scope.getRoomByIdUrl = getRoomByIdUrl;
        $scope.getRoomDataExtraUrl = getRoomDataExtraUrl;
        $scope.saveRoomUrl = saveRoomUrl;


        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 
    $scope.getRoomInformation = function (obj) {
        $scope.CurrentRoom = obj;
    };
    //======Custom property and Functions End========================================================== 

});



