
//File:Accounts_StdScholarship List Anjular  Controller
emsApp.controller('StdScholarshipListCtrl', function ($scope, $http, $filter, cfpLoadingBar, $sce) {
    $scope.StdScholarshipList = [];
    $scope.SelectedStdScholarship = [];
    $scope.ErrorMsg = "";
    $scope.HasError = false;
    $scope.HasViewPermission = false;
    $scope.IsShowTrashedItems = false;
     $scope.SelectedSemesterId=0;      
     $scope.SelectedScholarshipTypeId=0;      
     $scope.SelectedAmountTypeEnumId = 0;
     $scope.SelectedValidPeriodEnumId = 0;
     $scope.MinPercentageAmount = null;
     $scope.MaxPercentageAmount = null;
     //search items
     $scope.SearchText = "";
     $scope.HistoryMsg = "";
    $scope.totalItems = 0;
    $scope.totalServerItems = 0;
    $scope.PageSize = 100;
    $scope.PageNo = 1;

    $scope.loadPage = function () {
      $scope.getStdScholarshipListExtraData();
      $scope.getPagedStdScholarshipList();
      
    };
    /*For Search on data start*/
    $scope.changePageSize = function () {
        $scope.PageNo = 1;
        $scope.getPagedStdScholarshipList();
    };
    $scope.changePageNo = function () {
        $scope.getPagedStdScholarshipList();
    };
    $scope.prev = function () {
        $scope.PageNo = $scope.PageNo - 1;
        $scope.getPagedStdScholarshipList();
    };
    $scope.next = function () {
        $scope.PageNo = $scope.PageNo + 1;
        $scope.getPagedStdScholarshipList();
    };
    $scope.searchStdScholarshipList = function () {
        $scope.getPagedStdScholarshipList();
    };
    $scope.getPagedStdScholarshipList = function () {
        $scope.getStdScholarshipList();
    };
    /*For Search on data end*/
    $scope.getStdScholarshipList = function () {
        $http.get($scope.getPagedStdScholarshipUrl, {
            params: { 
            "textkey": $scope.SearchText 
            ,"pageSize": $scope.PageSize 
            ,"pageNo": $scope.PageNo 
           ,"semesterId": $scope.SelectedSemesterId      
           ,"scholarshipTypeId": $scope.SelectedScholarshipTypeId      
           , "validPeriodEnumId": $scope.SelectedValidPeriodEnumId
           , "amountTypeEnumId": $scope.SelectedAmountTypeEnumId     
            , "isShowTrashedItems": $scope.IsShowTrashedItems
            , "minPercentageAmount": $scope.MinPercentageAmount
            , "maxPercentageAmount": $scope.MaxPercentageAmount
          }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.StdScholarshipList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Std Scholarship list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Std Scholarship list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getStdScholarshipListExtraData= function()
    {
            $http.get($scope.getStdScholarshipListDataExtraUrl , {
                params: { }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                      if(result.DataExtra.AmountTypeEnumList!=null)
                         $scope.AmountTypeEnumList = result.DataExtra.AmountTypeEnumList;
                      if(result.DataExtra.ValidPeriodEnumList!=null)
                         $scope.ValidPeriodEnumList = result.DataExtra.ValidPeriodEnumList;
                  //DropDown Option Tables
                     if(result.DataExtra.SemesterList!=null)
                       $scope.SemesterList =  result.DataExtra.SemesterList; 

                     if(result.DataExtra.ScholarshipTypeList!=null)
                       $scope.ScholarshipTypeList =  result.DataExtra.ScholarshipTypeList; 


                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Std Scholarship! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Std Scholarship! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };

    /**
     * Caution:Very Dangerous to call if table has huge data
     * @returns {} 
     */
    $scope.getAllStdScholarshipList = function () {
        $scope.IsLoading++;
        $http.get($scope.getStdScholarshipListUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.StdScholarshipList = result.Data;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Std Scholarship list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg );
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Std Scholarship list! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
            alertError( $scope.ErrorMsg);
        });
    };
    $scope.deleteStdScholarshipById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteStdScholarshipByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.StdScholarshipList.indexOf(obj);
                        $scope.StdScholarshipList.splice(i, 1);
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
    /*$scope.deleteStdScholarshipByObj = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteStdScholarshipByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.StdScholarshipList.indexOf(obj);
                        $scope.StdScholarshipList.splice(i, 1);
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
    for (var i = 0; i < $scope.StdScholarshipList.length; i++) {
        var entity = $scope.StdScholarshipList[i];
        entity.IsSelected = checkbox.checked;
     }
    };
    //======Custom property and Functions Start=======================================================  

    $scope.openHistoryModal = function (history) {
        if (history == null || history === '') {
            $scope.HistoryMsg = '<center style="color:red;font-weight: bold;">No History Available.</center>';
        } else {
            $scope.HistoryMsg = history;
        }

        $('#HistoryModal').modal('show');
    }
    $scope.renderHtml = function (html_code) {
        return $sce.trustAsHtml(html_code);
    };


    //======Custom Variables goes hare=====

    
    $scope.Init = function (
         getPagedStdScholarshipUrl
        ,deleteStdScholarshipByIdUrl
        ,getStdScholarshipListDataExtraUrl
        ,saveStdScholarshipListUrl
        ,getStdScholarshipByIdUrl
        ,getStdScholarshipDataExtraUrl
        ,saveStdScholarshipUrl
        ) {
        $scope.getPagedStdScholarshipUrl = getPagedStdScholarshipUrl;
        $scope.deleteStdScholarshipByIdUrl = deleteStdScholarshipByIdUrl;
        /*bind extra url if need*/
        $scope.getStdScholarshipListDataExtraUrl = getStdScholarshipListDataExtraUrl;
        $scope.saveStdScholarshipListUrl = saveStdScholarshipListUrl;
        $scope.getStdScholarshipByIdUrl = getStdScholarshipByIdUrl;
        $scope.getStdScholarshipDataExtraUrl = getStdScholarshipDataExtraUrl;
        $scope.saveStdScholarshipUrl = saveStdScholarshipUrl;
       

        $scope.loadPage();
    };
    //======Other table's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 

    //======Custom property and Functions End========================================================== 

});



