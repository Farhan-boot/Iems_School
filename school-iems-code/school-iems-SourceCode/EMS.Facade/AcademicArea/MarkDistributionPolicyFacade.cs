using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.DataService;
using EMS.Framework.Objects;
using EMS.Framework.Utils;

namespace EMS.Facade.Academic
{
    public class MarkDistributionPolicyFacade : BaseFacade
    {
        private Aca_MarkDistributionPolicyDataService _markDistPolicyService;
        private Aca_MarkDistributionPolicyComponentDataService _markDistPolicyComponentService;
        private Aca_MarkDistributionPolicyBreakdownDataService _markDistPolicyBreakdownService;
        public MarkDistributionPolicyFacade(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
        {
            _markDistPolicyService = new Aca_MarkDistributionPolicyDataService(emsDbContext, usersProfile);
            _markDistPolicyComponentService = new Aca_MarkDistributionPolicyComponentDataService(emsDbContext, usersProfile);
            _markDistPolicyBreakdownService = new Aca_MarkDistributionPolicyBreakdownDataService(emsDbContext, usersProfile);
        }
        public List<Aca_MarkDistributionPolicy> GetAllMarkDistributionPolicy()
        {
            string error = "";

            return _markDistPolicyService.GetAll(out error);
        }

        public Aca_MarkDistributionPolicy GetMarkDistributionPolicyByIdWithChild(long id)
        {
            string error = "";
            List<string> includeTables = new List<string>();
            includeTables.Add("Aca_MarkDistributionPolicyComponent");
            includeTables.Add("Aca_MarkDistributionPolicyComponent.Aca_MarkDistributionPolicyBreakdown");
            return _markDistPolicyService.GetById(id, includeTables.ToArray());
        }


        public bool SaveMarkDistributionPolicy(Aca_MarkDistributionPolicy Obj, out string error)
        {   //check same name exist
            using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
            {
                try
                {
                    bool res = _markDistPolicyService.Save(Obj, out error, scope);
                    if (!res)
                    {
                        scope.Rollback();
                        return false;
                    }
                    DbInstance.SaveChanges();
                    scope.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    //log error
                    error = ex.ToString();
                    scope.Rollback();
                    return false;
                }
            }
        }

        private bool IsValidToSaveMarkDistributionPolicy(Aca_MarkDistributionPolicy newObj, out string error)
        {
            error = "";
            if (newObj == null)
            {
                error = "Object to save can't be null!";
                return false;
            }
            if (!newObj.Name.IsValid())
            {
                error += "Mark Distribution Policy Name Required!";
                return false;
            }
            if (newObj.Name.Length > 256)
            {
                error += "Name exceeded its maximum length 256.";
                return false;
            }
            if (newObj.CreditHour <= 0)
            {
                error = "Mark Distribution Policy Total Credit Hour should be greater then zero!";
                return false;
            }
            if (newObj.TotalMark <= 0)
            {
                error += "Mark Distribution Policy Total Mark should be greater then zero!";
                return false;
            }
            if (newObj.ConvertPercentage <= 0)
            {
                error += "Mark Distribution Policy Convert Percentage should be greater then zero!";
                return false;
            }

            //if (newObj.CourseCategoryEnumId == null)
            //{
            //    error += "Please select valid Course Category.";
            //    return false;
            //}
            //if (newObj.ConvertPercentage == null)
            //{
            //    error += "Convert Percentage required.";
            //    return false;
            //}
            //if (newObj.ExamTypeEnumId == null)
            //{
            //    error += "Please select valid Exam Type.";
            //    return false;
            //}
            //if (newObj.IsFixedPolicy == null)
            //{
            //    error += "Is Fixed Policy required.";
            //    return false;
            //}
            //if (newObj.StatusEnumId == null)
            //{
            //    error += "Please select valid Status.";
            //    return false;
            //}
            if (newObj.Remark.IsValid() && newObj.Remark.Length > 256)
            {
                error += "Remark exceeded its maximum length 256.";
                return false;
            }
            //if (newObj.IsBachelorProgram == null)
            //{
            //    error += "Is Bachelor Program required.";
            //    return false;
            //}
            //if (newObj.IsDeleted == null)
            //{
            //    error += "Is Deleted required.";
            //    return false;
            //}
            //TODO write your custom validation here.
            //var res =  DbInstance.Aca_MarkDistributionPolicy.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
            //error = "A MarkDistributionPolicy already exists!";
            //return false;
            //}

            if (newObj.EndSemesterId < newObj.StartSemesterId)
            {
                error += "End semester cannot be small from Start semester.";
                return false;
            }


            var policyList = DbInstance.Aca_MarkDistributionPolicy.Where(x => x.CreditHour == newObj.CreditHour
                                                                      && x.CourseCategoryEnumId == newObj.CourseCategoryEnumId
                                                                      && x.Id != newObj.Id
                                                                      && x.ProgramId == newObj.ProgramId)
                .ToList();


            foreach (var policy in policyList)
            {
                if ((newObj.StartSemesterId <=policy.EndSemesterId && newObj.StartSemesterId >= policy.StartSemesterId) ||(policy.EndSemesterId==null && !(newObj.EndSemesterId < policy.StartSemesterId)))
                {
                    error += $"Same policy already exist at {policy.Name}!";
                    return false;
                }

                if ((newObj.EndSemesterId <= policy.EndSemesterId && newObj.EndSemesterId >= policy.StartSemesterId) || (policy.EndSemesterId == null && !(newObj.EndSemesterId < policy.StartSemesterId)))
                {
                    error += $"Same policy already exist at {policy.Name}!";
                    return false;
                }

            }
            


            return true;
        }

        private bool IsValidPolicyComponent(Aca_MarkDistributionPolicyComponent Obj, out string error)
        {
            error = "";
            if (Obj == null)
            {
                error = "Mark Distribution Policy Component to save can't be null!";
                return false;
            }
            if (!Obj.Name.IsValid())
            {
                error = "Mark Distribution Policy Component Name Required!";
                return false;
            }
            if (Obj.DefaultTotalMark <= 0)
            {
                error = "Mark Distribution Policy Component Total Mark should be greater then zero!";
                return false;
            }
            if (Obj.ConvertPercentage <= 0)
            {
                error = "Mark Distribution Policy Component Convert Percentage should be greater then zero!";
                return false;
            }
            return true;
        }

        private bool IsValidPolicyComponentBreakdown(Aca_MarkDistributionPolicyBreakdown Obj, out string error)
        {
            error = "";
            if (Obj == null)
            {
                error = "Mark Distribution Policy Breakdown to save can't be null!";
                return false;
            }
            if (!Obj.Name.IsValid())
            {
                error = "Mark Distribution Policy Breakdown Name Required!";
                return false;
            }
            if (Obj.DefaultTotalMark <= 0)
            {
                error = "Mark Distribution Policy Breakdown Total Mark should be greater then zero!";
                return false;
            }
            if (Obj.ConvertPercentage <= 0)
            {
                error = "Mark Distribution Policy Breakdown Convert Percentage should be greater then zero!";
                return false;
            }
            return true;
        }

        public bool DeepSaveMarkDistributionPolicy(Aca_MarkDistributionPolicy policyObj, ref Aca_MarkDistributionPolicy returnObj, out string error)
        {   //check same name exist
            error = string.Empty;
            if (!IsValidToSaveMarkDistributionPolicy(policyObj, out error))
            {
                return false;
            }
            using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
            {
                try
                {
                    Aca_MarkDistributionPolicy dbMarkDistributionPolicy = null;
                    if (!_markDistPolicyService.Save(policyObj, ref dbMarkDistributionPolicy, out error, scope))
                    {
                        scope.Rollback();
                        return false;
                    }
                    var policyComponentList = policyObj.Aca_MarkDistributionPolicyComponent;
                    if (policyComponentList != null && policyComponentList.Count > 0)
                    {
                        //saving child component
                        var totalComponetPercentage = 0.0;
                        foreach (var component in policyComponentList)
                        {
                            if (!IsValidPolicyComponent(component, out error))
                            {
                                return false;
                            }
                            component.PolicyId = policyObj.Id;
                            //if (!(policyObj.CourseCategory == Aca_Course.CourseCategoryEnum.Theory && policyObj.IsBachelorProgram))
                            //{
                            //    component.TestType = Aca_MarkDistributionPolicyComponent.TestTypeEnum.AssesmentOrExam;
                            //}
                            //saving child component
                            Aca_MarkDistributionPolicyComponent dbComponent = null;
                            if (!_markDistPolicyComponentService.Save(component, ref dbComponent, out error, scope))
                            {
                                scope.Rollback();
                                return false;
                            }
                            dbComponent.IsAlreadyAdded = true;
                            totalComponetPercentage += component.ConvertPercentage;
                            //saving child policyBreakdown
                            float totalCobrakdownPercentage = 0.00f;
                            var policyBreakdownList = component.Aca_MarkDistributionPolicyBreakdown;
                            if (policyBreakdownList != null && policyBreakdownList.Count > 0)
                            {
                                var child = policyBreakdownList.Count;
                                float validPercentage = component.DefaultTotalMark;//Math.Round(component.DefaultTotalMark / component.BestCount,10) ;
                                //todo need calculation of mark
                                if (component.BestCount == 0)//all count
                                {
                                    validPercentage = component.ConvertPercentage / (float)child;

                                }
                                else if (component.BestCount > 0)//selected best
                                {
                                    validPercentage = component.DefaultTotalMark / (float)component.BestCount;
                                }

                                foreach (var breakdown in policyBreakdownList)
                                {
                                    if (!IsValidPolicyComponentBreakdown(breakdown, out error))
                                    {
                                        return false;
                                    }
                                    breakdown.ComponentId = component.Id;
                                    // saving policyBreakdown
                                    Aca_MarkDistributionPolicyBreakdown dbBreakdown = null;
                                    if (!_markDistPolicyBreakdownService.Save(breakdown, ref dbBreakdown, out error, scope))
                                    {
                                        scope.Rollback();
                                        return false;
                                    }
                                    dbBreakdown.IsAlreadyAdded = true;
                                    if (component.BestCount == 0)
                                    {
                                        totalCobrakdownPercentage += breakdown.ConvertPercentage;
                                    }

                                }

                                if (component.BestCount > 0 && !validPercentage.Equals(totalCobrakdownPercentage))
                                {
                                    var msg =
                                        string.Format(
                                            "As you are taking Best {0}, each breakup must have {1}%. " +
                                            "Otherwise, students will not be able to complete exactly {2} " +
                                            "in any valid combination.",//"Please fix the red marked percent below by clicking on the \"Edit\" link beside the items.",
                                            dbComponent.BestCount, validPercentage, component.DefaultTotalMark);
                                    dbComponent.Remark = msg;
                                }
                                //setting  Percentage calculation  of component
                                else if (totalCobrakdownPercentage > component.DefaultTotalMark)
                                {
                                    var msg =
                                        string.Format(
                                            "Contributing percentage overflowed. Percentage has become {0}% of {1}%. Please adjust Breakdown's percentage below by clicking on the \"Edit\" link beside the items.", totalCobrakdownPercentage, component.DefaultTotalMark);
                                    dbComponent.Remark = msg;
                                }
                                else if (totalCobrakdownPercentage < component.DefaultTotalMark)
                                {
                                    var msg =
                                       string.Format(
                                           "Contributing percentage pending. Only {0}% of {1}% covered. Please adjust Breakdown's percentage below by clicking on the \"Edit\" link beside the items.", totalCobrakdownPercentage, component.DefaultTotalMark);
                                    dbComponent.Remark = msg;
                                }
                                else dbComponent.Remark = null;


                            }
                            else
                            {
                                dbComponent.Remark = null;
                                dbComponent.BestCount = 0;//count all
                            } // //saving child policyBreakdown end

                        }
                        //setting  Percentage calculation  of policyObj
                        if (totalComponetPercentage > policyObj.TotalMark)
                        {
                            var msg =
                                 string.Format(
                                            "Contributing percentage overflowed. Percentage has become {0}% of {1}%. Please adjust Component's percentage below by clicking on the \"Edit\" link beside the items.", totalComponetPercentage, policyObj.TotalMark);
                            dbMarkDistributionPolicy.Remark = msg;
                        }
                        else if (totalComponetPercentage < policyObj.TotalMark)
                        {
                            var msg =
                                       string.Format(
                                           "Contributing percentage pending. Only {0}% of {1}% covered. Please adjust Component's percentage below by clicking on the \"Edit\" link beside the items.", totalComponetPercentage, policyObj.TotalMark);
                            dbMarkDistributionPolicy.Remark = msg;
                        }
                        else dbMarkDistributionPolicy.Remark = null;

                    }
                    
                    returnObj = dbMarkDistributionPolicy;
                    DbInstance.SaveChanges();
                    scope.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    //log error
                    error = ex.ToString();
                    scope.Rollback();
                    return false;
                }
            }

        }


    }
}
