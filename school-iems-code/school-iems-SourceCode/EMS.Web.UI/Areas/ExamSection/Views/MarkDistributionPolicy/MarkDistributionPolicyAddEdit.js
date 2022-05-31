
//File: Aca_MarkDistributionPolicy Anjular  Controller
emsApp.controller('MarkDistributionPolicyAddEditCtrl', function ($scope, $http, $filter) {
    $scope.MarkDistributionPolicy = [];
    $scope.MarkDistributionPolicyId = 0;


    
    $scope.ErrorMsg = "";
    $scope.HasError = false;

    $scope.loadPage = function (MarkDistributionPolicyId) {
        if (MarkDistributionPolicyId != null)
            $scope.MarkDistributionPolicyId = MarkDistributionPolicyId;

        $scope.loadMarkDistributionPolicyExtraData($scope.MarkDistributionPolicyId);
        $scope.getMarkDistributionPolicyById($scope.MarkDistributionPolicyId);
    }
    $scope.getNewMarkDistributionPolicy = function () {
        $scope.getMarkDistributionPolicyById(0);
    }
    $scope.getMarkDistributionPolicyById = function (MarkDistributionPolicyId) {
       
        if (MarkDistributionPolicyId != null)
            $scope.MarkDistributionPolicyId = MarkDistributionPolicyId;
        else {
            return;
        }

        
        $http.get($scope.getMarkDistributionPolicyByIdUrl, {
            params: { "id": $scope.MarkDistributionPolicyId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.MarkDistributionPolicy = result.Data;
                updateUrlQuery('id', $scope.MarkDistributionPolicyId);
                $scope.SetBestCount();
            } else {
                $scope.HasError = true;
                alertError($scope.ErrorMsg + "Unable to get MarkDistributionPolicy data! " + result.Errors.toString());
            }
            
        }).error(function (result, status) {
            $scope.HasError = true;
            alertError($scope.ErrorMsg + "Unable to get MarkDistributionPolicy data! " + result.toString());
            
        });
    }
    $scope.loadMarkDistributionPolicyExtraData = function (MarkDistributionPolicyId) {
        if (MarkDistributionPolicyId != null)
            $scope.MarkDistributionPolicyId = MarkDistributionPolicyId;

        
        $http.get($scope.getMarkDistributionPolicyExtraDataUrl, {
            params: { "id": $scope.MarkDistributionPolicyId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                //DropDown Option Tables

                //DropDown Option Enum List 
                $scope.StatusEnumList = result.DataExtra.StatusEnumList;
                $scope.CourseCategoryEnumList = result.DataExtra.CourseCategoryEnumList;
                $scope.ExamTypeEnumList = result.DataExtra.ExamTypeEnumList;
                if (result.DataExtra.ProgramTypeEnumList != null)
                    $scope.ProgramTypeEnumList = result.DataExtra.ProgramTypeEnumList;

                if (result.DataExtra.SemesterList!=null) {
                    $scope.StartSemesterList = angular.copy(result.DataExtra.SemesterList);
                    $scope.EndSemesterList = angular.copy(result.DataExtra.SemesterList);
                }

                if (result.DataExtra.ProgramList != null) {
                    $scope.ProgramList = result.DataExtra.ProgramList;
                }
                

                $scope.onAfterLoadMarkDistributionPolicyExtraData(result);
            } else {
                $scope.HasError = true;
                alertError($scope.ErrorMsg + "Unable to Load List data for Mark Distribution Policy! " + result.Errors.toString());
            }
            
        }).error(function (result, status) {
            $scope.HasError = true;
            alertError($scope.ErrorMsg + "Unable to Load List data for MarkDistributionPolicy! " + result.toString());
            
        });
    }
    $scope.saveMarkDistributionPolicy = function () {

        
        $http.post($scope.saveMarkDistributionPolicyUrl + "/", $scope.MarkDistributionPolicy).
            success(function (result, status) {
                if (!result.HasError) {
                    $scope.HasError = false;
                    if (result.Data != null) {
                        $scope.MarkDistributionPolicy = result.Data;
                      //  console.log(result.Data);
                        $scope.MarkDistributionPolicyId = $scope.MarkDistributionPolicy.Id;
                        updateUrlQuery('id', $scope.MarkDistributionPolicyId);
                        $scope.SetBestCount();
                    }
                    alertSuccess("Successfully saved Mark Distribution Policy.");
                } else {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to save MarkDistributionPolicy! " + result.Errors.toString();
                    alertError($scope.ErrorMsg);
                }
                
            }).
            error(function (result, status) {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to save MarkDistributionPolicy! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
                
            });
    }


    $scope.deleteMarkDistributionPolicyById = function (obj) {
        console.log(obj);
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                
                $http.get($scope.deleteMarkDistributionPolicyByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.MarkDistributionPolicyList.indexOf(obj);
                        $scope.MarkDistributionPolicyList.slice(i, 1);
                        alertSuccess("Successfully deleted Mark Distribution Policy data!");

                    } else {
                        $scope.HasError = true;
                        $scope.ErrorMsg = "Unable to delete Mark Distribution Policy data! " + result.Errors.toString();
                        alertError($scope.ErrorMsg);
                    }
                    
                }).error(function (result, status) {
                    $scope.HasError = true;
                    $scope.ErrorMsg = "Unable to delete Mark Distribution Policy data! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
                    alertError($scope.ErrorMsg);
                    
                });
            }
        });
    };

    //======Custom property and Functions Start=======================================================  
    //======Custom Variables goes hare=====
    $scope.MarkComponentToEdit = [];
    $scope.MarkBreakdownToEdit = [];
    $scope.Init = function (
        MarkDistributionPolicyId,
        getMarkDistributionPolicyByIdUrl,
        getMarkDistributionPolicyExtraDataUrl,
        saveMarkDistributionPolicyUrl,
        deleteMarkDistributionPolicyByIdUrl,
        getNewMarkPolicyComponentByPolicyIdUrl,
        getNewMarkPolicyBreakdownByComponentIdUrl,
        deleteMarkDistributionPolicyComponentByIdUrl,
        deleteMarkDistributionPolicyBreakdownByIdUrl
        ) {
        $scope.MarkDistributionPolicyId = MarkDistributionPolicyId;
        $scope.getMarkDistributionPolicyByIdUrl = getMarkDistributionPolicyByIdUrl;
        $scope.saveMarkDistributionPolicyUrl = saveMarkDistributionPolicyUrl;
        $scope.getMarkDistributionPolicyExtraDataUrl = getMarkDistributionPolicyExtraDataUrl;
        $scope.deleteMarkDistributionPolicyByIdUrl = deleteMarkDistributionPolicyByIdUrl;
        //added
        $scope.getNewMarkPolicyBreakdownByComponentIdUrl = getNewMarkPolicyBreakdownByComponentIdUrl;
        $scope.getNewMarkPolicyComponentByPolicyIdUrl = getNewMarkPolicyComponentByPolicyIdUrl;

        $scope.deleteMarkDistributionPolicyComponentByIdUrl = deleteMarkDistributionPolicyComponentByIdUrl;
        $scope.deleteMarkDistributionPolicyBreakdownByIdUrl = deleteMarkDistributionPolicyBreakdownByIdUrl;

        $scope.loadPage(MarkDistributionPolicyId);
    };

    $scope.onAfterLoadMarkDistributionPolicyExtraData = function (result) {
        $scope.TestTypeEnumList = result.DataExtra.TestTypeEnumList;
    }
    //======Other tabale's get save Functions start============================================================== 

    //======Supporting Functions start================================================================ 
    //component

    $scope.editComponent = function (component) {
        //alert(component.Name);
        $scope.MarkComponentToEdit = component;
        $('#componentModal').modal('show');
    }
    $scope.addNewComponent = function (component) {
        var componentList = $scope.MarkDistributionPolicy.Aca_MarkDistributionPolicyComponentJson;
        if (componentList == null) {
            componentList = new Array();
        }
        if (component.Id !== '0')// if new object no need to check
            var newTemp = $filter("filter")(componentList, { Id: component.Id }, true);


        //checking if component exist

        if (!component.IsAlreadyAdded || newTemp == null)
        {
            var temp = new Object(); angular.copy(component, temp);//cloning object
            componentList.push(temp);//adding new component to list
            $('#componentModal').modal('hide');
        } else {
            $('#componentModal').modal('hide');
        }
    }
    $scope.getNewComponent = function () {
        $scope.MarkComponentToEdit = $scope.getNewMarkPolicyComponentByPolicyId();
        $('#componentModal').modal('show');
    }
    $scope.deleteComponent = function (obj) {
        if (obj == null) {
            alertError("Please select a component to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteMarkDistributionPolicyComponentByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.MarkDistributionPolicy.Aca_MarkDistributionPolicyComponentJson.indexOf(obj);
                        $scope.MarkDistributionPolicy.Aca_MarkDistributionPolicyComponentJson.splice(i, 1);
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
    }

    //breakdown
    $scope.editBreakdown = function (breakdown) {
        $scope.MarkBreakdownToEdit = breakdown;
        $('#breakdownModal').modal('show');
    }
    $scope.addNewBreakdown = function (brakdown) {
        var componentList = $scope.MarkDistributionPolicy.Aca_MarkDistributionPolicyComponentJson;//existing component
        var component = $filter("filter")(componentList, { Id: brakdown.ComponentId }, true);//getting parent component 
        if (component == null || component.length <= 0)
            return;
        var brakdowsList = component[0].Aca_MarkDistributionPolicyBreakdownJson;
        if (brakdowsList == null) {
            brakdowsList = new Array();
        }

        if (brakdown.Id !== '0')// if new object no need to check
        var newTemp2 = $filter("filter")(brakdowsList, { Id: brakdown.Id }, true);
        //console.log(newTemp2);
        //if (newTemp2 == null || newTemp2.length <= 0)
        if (!brakdown.IsAlreadyAdded || newTemp2 == null)
        {
          
            var temp = new Object(); angular.copy(brakdown, temp);//cloning object
            temp.IsAlreadyAdded = true;
            brakdowsList.push(temp);
            $scope.SetBestCount();
        }
        $('#breakdownModal').modal('hide');
    }
    $scope.getNewBreakdown = function (component) {
        if (component.Id == null || component.Id === '0')
        { alertError("Invalid Component to Add Breakdown!"); return; }

        $scope.MarkComponentToEdit = $scope.getNewMarkDistributionPolicyBreakdown(component.Id);
        $('#breakdownModal').modal('show');
    }
    $scope.deleteBreakdown = function (breakDownTodelete,componet) {
        if (breakDownTodelete == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteMarkDistributionPolicyBreakdownByIdUrl, {
                    params: { "id": breakDownTodelete.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;

                        var i = componet.Aca_MarkDistributionPolicyBreakdownJson.indexOf(breakDownTodelete);
                        componet.Aca_MarkDistributionPolicyBreakdownJson.splice(i, 1);
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
    }

    $scope.deleteMarkDistributionPolicyComponentById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteMarkDistributionPolicyComponentByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.MarkDistributionPolicyComponentList.indexOf(obj);
                        $scope.MarkDistributionPolicyComponentList.splice(i, 1);
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
    $scope.deleteMarkDistributionPolicyBreakdownById = function (obj) {
        if (obj == null) {
            alertError("Please select a row to delete!");
        }
        bootbox.confirm("Are you sure you want to delete this data?", function (yes) {
            if (yes) {
                $http.get($scope.deleteMarkDistributionPolicyBreakdownByIdUrl, {
                    params: { "id": obj.Id }
                }).success(function (result, status) {
                    if (!result.HasError) {
                        $scope.HasError = false;
                        var i = $scope.MarkDistributionPolicyBreakdownList.indexOf(obj);
                        $scope.MarkDistributionPolicyBreakdownList.splice(i, 1);
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

    //for component
    $scope.SetBestCount = function () {
        var componentList = $scope.MarkDistributionPolicy.Aca_MarkDistributionPolicyComponentJson;
        if (componentList != null && componentList.length > 0) {
            angular.forEach(componentList, function (component, key) {
                component.BestCountOptionList = [];
                var bestCountOptionAll = {
                    Id: 0,
                    Name: 'All'
                }
                component.BestCountOptionList.push(bestCountOptionAll);
                var breakdownList = component.Aca_MarkDistributionPolicyBreakdownJson;
                
                if (breakdownList != null && breakdownList.length > 0) {
                    component.child = breakdownList.length;
                   
                    for (var i = 1; i <= breakdownList.length-1; i++) {
                        var bestCountOption = {
                            Id: i,
                            Name: i.toString()
                        }
                        component.BestCountOptionList.push(bestCountOption);
                    }
                    var bestCountOptionNone = {
                        Id: -1,
                        Name: 'None'
                    }
                    component.BestCountOptionList.push(bestCountOptionNone);
                }
            });
        }
    }
    $scope.getNewMarkPolicyComponentByPolicyId = function () {
        if ($scope.MarkDistributionPolicyId == null)
        { alertError("Invalid Policy!"); return; }
         
        
        $http.get($scope.getNewMarkPolicyComponentByPolicyIdUrl, {
            params: { "id": $scope.MarkDistributionPolicyId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.MarkComponentToEdit = result.Data;
                //console.log($scope.MarkComponentToEdit);
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Mark Distribution Policy Component! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
            
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Mark Distribution Policy Component! " + "Status: " + JSON.stringify(status).toString() + ". " + JSON.stringify(result).toString();
            alertError($scope.ErrorMsg);
            
        });
    };
    $scope.getNewMarkDistributionPolicyBreakdown = function (componentId) {

        
        $http.get($scope.getNewMarkPolicyBreakdownByComponentIdUrl, {
            params: { "id": componentId }
        }).success(function (result, status) {
            if (!result.HasError) {
                $scope.HasError = false;
                $scope.MarkBreakdownToEdit = result.Data;
                //console.log($scope.MarkBreakdownToEdit);
               
            } else {
                $scope.HasError = true;
                $scope.ErrorMsg = "Unable to get Mark Distribution Policy Breakdown! " + result.Errors.toString();
                alertError($scope.ErrorMsg);
            }
            
        }).error(function (result, status) {
            $scope.HasError = true;
            $scope.ErrorMsg = "Unable to get Mark Distribution Policy Breakdown! " + "Status: " + status.toString() + ". " + result.toString();
            console.log(result);
            alertError($scope.ErrorMsg);
            
        });
    }


    //======Custom property and Functions End========================================================== 
});

