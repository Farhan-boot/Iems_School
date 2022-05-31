
//File: HR_SalaryTemplate Anjular  Controller
emsApp.controller('SalaryTemplateAddEditCtrl', function ($scope, $http, $filter, cfpLoadingBar, $sce) {
    $scope.SalaryTemplate = [];
    $scope.SalaryTemplateId=0;
    $scope.ErrorMsg = "";
    $scope.SearchText = "";
    $scope.HasError= false;
    $scope.HasViewPermission = false;
   $scope.loadPage= function (SalaryTemplateId)
    {
       if(SalaryTemplateId!=null)
        $scope.SalaryTemplateId=SalaryTemplateId;
       
       //$scope.loadSalaryTemplateDataExtra($scope.SalaryTemplateId);
       $scope.getSalaryTemplateById($scope.SalaryTemplateId);

       if ($scope.SalaryTemplateId !== 0) {
           $scope.getPagedSalaryTemplateDetailsList();
           $scope.getSalaryTemplateDetailsListExtraData();
       }
       
    };
   $scope.getNewSalaryTemplate= function()
    {
    	  $scope.getSalaryTemplateById(0);
    };
   $scope.getSalaryTemplateById= function(SalaryTemplateId)
    {
        if(SalaryTemplateId!=null && SalaryTemplateId!=='')
        $scope.SalaryTemplateId=SalaryTemplateId;
        else return;
        
        $http.get($scope.getSalaryTemplateByIdUrl, {
            params: { "id": $scope.SalaryTemplateId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError= false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.SalaryTemplate = result.Data;
                updateUrlQuery('id' , $scope.SalaryTemplate.Id);
                $scope.onAfterGetSalaryTemplateById(result);
            } else {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Salary Template! "+result.Errors.toString();
             alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
             $scope.HasError= true;
             $scope.ErrorMsg="Unable to get Salary Template! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
             alertError($scope.ErrorMsg);
        });
    };
   $scope.loadSalaryTemplateDataExtra= function(SalaryTemplateId)
    {
        if(SalaryTemplateId!=null)
            $scope.SalaryTemplateId=SalaryTemplateId;
            
            $http.get($scope.getSalaryTemplateDataExtraUrl, {
                params: { "id": $scope.SalaryTemplateId }
            }).success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                        $scope.onAfterLoadSalaryTemplateDataExtra(result);
                } else {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Salary Template! "+result.Errors.toString();
                 alertError($scope.ErrorMsg);
                }
            }).error(function (result, status) {
                 $scope.HasError= true;
                 $scope.ErrorMsg="Unable to load option data for Salary Template! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                 alertError($scope.ErrorMsg);
            });
    };
   $scope.saveSalaryTemplate= function(){
    	if(!$scope.validateSalaryTemplate())
        {
          return;
        }
        $http.post($scope.saveSalaryTemplateUrl + "/", $scope.SalaryTemplate).
            success(function (result, status) {
                if (!result.HasError) {
                   $scope.HasError= false;
                   if(result.Data!=null)
                   {
                       $scope.SalaryTemplate = result.Data;
                       $scope.SalaryTemplateId = $scope.SalaryTemplate.Id;
                    updateUrlQuery('id', $scope.SalaryTemplate.Id);
                   }
                   alertSuccess("Successfully saved Salary Template data!");
                    $scope.onAfterSaveSalaryTemplate(result);
                    $scope.getPagedSalaryTemplateDetailsList();
                    $scope.getSalaryTemplateDetailsListExtraData();
                } else {
                    $scope.HasError= true;
                    $scope.ErrorMsg="Unable to save Salary Template! "+result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError= true;
                $scope.ErrorMsg="Unable to save Salary Template! "+JSON.stringify(result).toString()+". "+"Status: " +JSON.stringify(status).toString() ;
                alertError($scope.ErrorMsg);
            });
    };

   $scope.deleteSalaryTemplateById= function(){
    	
    };
   $scope.validateSalaryTemplate= function(){
        return true;
    }; 
//======Custom property and Functions Start=======================================================  
//======Custom Variabales goes hare=====

    $scope.Init = function (SalaryTemplateId, getSalaryTemplateByIdUrl, getSalaryTemplateDataExtraUrl
        , saveSalaryTemplateUrl
        , deleteSalaryTemplateByIdUrl
        , getPagedSalaryTemplateDetailsUrl
        , deleteSalaryTemplateDetailsByIdUrl
        , getSalaryTemplateDetailsListDataExtraUrl
        , getSalaryTemplateDetailsByIdUrl
        , saveSalaryTemplateDetailsUrl
    ) {
        $scope.SalaryTemplateId = SalaryTemplateId;
        $scope.getSalaryTemplateByIdUrl = getSalaryTemplateByIdUrl;
        $scope.saveSalaryTemplateUrl = saveSalaryTemplateUrl;
        $scope.getSalaryTemplateDataExtraUrl = getSalaryTemplateDataExtraUrl;
        $scope.deleteSalaryTemplateByIdUrl = deleteSalaryTemplateByIdUrl;

        /*Salary Template Details Function*/
        $scope.getPagedSalaryTemplateDetailsUrl = getPagedSalaryTemplateDetailsUrl;
        $scope.deleteSalaryTemplateDetailsByIdUrl = deleteSalaryTemplateDetailsByIdUrl;
        $scope.getSalaryTemplateDetailsListDataExtraUrl = getSalaryTemplateDetailsListDataExtraUrl;
        $scope.getSalaryTemplateDetailsByIdUrl = getSalaryTemplateDetailsByIdUrl;
        $scope.saveSalaryTemplateDetailsUrl = saveSalaryTemplateDetailsUrl;

        $scope.loadPage(SalaryTemplateId);
    };
    
  $scope.onAfterSaveSalaryTemplate= function(result){
     //var data=result.Data;
    };
  $scope.onAfterGetSalaryTemplateById= function(result){
    //var data=result.Data;
    
    };
  $scope.onAfterLoadSalaryTemplateDataExtra= function(result){
    //var DataExtra=result.DataExtra;
  //DropDown Option Tables
  /*
  //Child Table Bindings, need to fix
             $scope.SalaryTemplateIdList =  result.DataExtra.SalaryTemplateIdList; 
     */
    };
//======Other tabale's get save Functions start============================================================== 

//======Supporting Functions start================================================================ 
    $scope.renderHtml = function (html_code) {
        if (html_code == null || html_code === '') {
            html_code = '<center style="color:red;font-weight: bold;">No History Available.</center>';
        }
        return $sce.trustAsHtml(html_code);
    };

    $scope.SuggestName = function () {
        var selectedCategory =
            $scope.CategoryTypeEnumList.find(x => x.Id === $scope.SalaryTemplateDetails.CategoryTypeEnumId, true);
        if (selectedCategory != null) {
            $scope.SalaryTemplateDetails.Name = selectedCategory.Name;
        }
    }
//======Custom property and Functions End========================================================== 

//=========================Salary Template Details Function Start================================

    $scope.searchSalaryTemplateDetailsList = function () {
        $scope.getPagedSalaryTemplateDetailsList();
    };
    $scope.getPagedSalaryTemplateDetailsList = function () {
        $scope.getSalaryTemplateDetailsList();
    };
    /*For Search on data end*/
    $scope.getSalaryTemplateDetailsList = function () {
        $http.get($scope.getPagedSalaryTemplateDetailsUrl, {
            params: {
                "textkey": $scope.SearchText
                , "salaryTemplateId": $scope.SalaryTemplateId
            }
        }).success(function (result) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.SalaryTemplateDetailsList = result.Data;
                $scope.totalItems = result.Count;
                $scope.totalServerItems = result.Data.length;
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Salary Template Details list data! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Salary Template Details list! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.getSalaryTemplateDetailsListExtraData = function () {
        $http.get($scope.getSalaryTemplateDetailsListDataExtraUrl, {
            params: {}
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                if (result.DataExtra.SalaryTypeEnumList != null)
                    $scope.SalaryTypeEnumList = result.DataExtra.SalaryTypeEnumList;
                if (result.DataExtra.CategoryTypeEnumList != null)
                    $scope.CategoryTypeEnumList = result.DataExtra.CategoryTypeEnumList;
                //DropDown Option Tables
                if (result.DataExtra.SalaryTemplateDetailsList != null)
                    $scope.SalaryTemplateDetailsList = result.DataExtra.SalaryTemplateDetailsList;

                if (result.DataExtra.SalaryTemplateList != null)
                    $scope.SalaryTemplateList = result.DataExtra.SalaryTemplateList;

            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to load option data for Salary Template Details! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to load option data for Salary Template Details! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.deleteSalaryTemplateDetailsById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteSalaryTemplateDetailsByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.SalaryTemplateDetailsList.indexOf(obj);
                        $scope.SalaryTemplateDetailsList.splice(i, 1);
                        alertSuccess("Data successfully deleted!");
                        
                        if (result.DataExtra.History != null) {
                            $scope.SalaryTemplate.History = result.DataExtra.History;
                        }
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

    $scope.getNewSalaryTemplateDetails = function () {
        $scope.getSalaryTemplateDetailsById(0);
    };
    $scope.getSalaryTemplateDetailsById = function (SalaryTemplateDetailsId) {
        if (SalaryTemplateDetailsId != null && SalaryTemplateDetailsId !== '')
            $scope.SalaryTemplateDetailsId = SalaryTemplateDetailsId;
        else return;

        $http.get($scope.getSalaryTemplateDetailsByIdUrl, {
            params: {
                 "id": $scope.SalaryTemplateDetailsId
                 ,"salaryTemplateId": $scope.SalaryTemplateId
            }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.HasViewPermission = result.HasViewPermission;
                $scope.SalaryTemplateDetails = result.Data;
                $('#SalaryTemplateModal').modal('show');
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Salary Template Details! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Salary Template Details! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
            alertError($scope.ErrorMsg);
        });
    };
    $scope.saveSalaryTemplateDetails = function () {
        $http.post($scope.saveSalaryTemplateDetailsUrl + "/", $scope.SalaryTemplateDetails).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    if (result.Data != null) {
                        $scope.SalaryTemplateDetails = result.Data;
                        $('#SalaryTemplateModal').modal('hide');
                    }
                    $scope.getPagedSalaryTemplateDetailsList();
                    alertSuccess("Successfully saved Salary Template Details data!");
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to save Salary Template Details! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to save Salary Template Details! " + JSON.stringify(result).toString() + ". " + "Status: " + JSON.stringify(status).toString();
                alertError($scope.ErrorMsg);
            });
    };
//=========================Salary Template Details Function End=============================

});

