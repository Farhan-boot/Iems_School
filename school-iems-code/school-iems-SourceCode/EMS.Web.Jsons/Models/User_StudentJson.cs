using System;
using System.Collections.Generic;
using System.Linq;
using EMS.CoreLibrary.Helpers;
using EMS.Framework;
using EMS.DataAccess.Data;

namespace EMS.Web.Jsons.Models
{
    public partial class User_StudentJson
    {
        #region Custom Variables
        public String UserName { get; set; }
        public String FullName { get; set; }
        public String Password { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public String FatherName { get; set; }//employee->studentList
        public String MotherName { get; set; }//employee->studentList
        public String Remarks { get; set; }
        public string RankName { get; set; }
        public bool IsApproved { get; set; }
        public string DeptName { get; set; }
        public string DeptShortName { get; set; }
        public User_AccountJson User_AccountJson { get; set; }
        public int ExamRollNo { get; set; }
        public string UserMobile { get; set; }  //employee->studentList
        public string UserEmail { get; set; } //employee->studentList
        public string EmergencyMobile { get; set; } //employee->studentList


        public string FullPresentAddress { get; set; }   //employee->studentList
        public string FullPermanentAddress { get; set; }   //employee->studentList
        public string ProgramShortTitle { get; set; }

        public string BirthCertificateNumber { get; set; }  //to show in the student's List.
        public string NationalIdNumber { get; set; }  //to show in the student's List.
        public DateTime DateOfBirth { get; set; }  //to show in the student's List.


        //very temp For show class result
        public string ClassName { get; set; }
        //temporary 
        public bool IsGraduated { get; set; }
        public bool IsProfileConfirmed { get; set; }
        public string LevelTermName { get; set; }
        public string ClassSectionName { get; set; }
        //public string TermName { get; set; }
        public string BatchName { get; internal set; }
        public string BloodGroup { get; set; }
        public string Gender { get; set; }
        public string UserStatus { get; set; }

        //public bool IsMilitary { get; set; }

        #endregion
    }
    public static partial class EntityMapper
    {
        private static void MapExtra(User_Student entity, User_StudentJson toJson)
        {
            toJson.ExamRollNo = entity.ExamRollNo;


            if (entity.User_Account != null)
            {
                toJson.IsGraduated = entity.User_Account.LeavingDate != null;
                toJson.BloodGroup = entity.User_Account.BloodGroup.ToString().AddBloodGroupSignToSentence();
                toJson.Gender = entity.User_Account.Gender.ToString();
                toJson.UserName = entity.User_Account.UserName;
                toJson.FullName = entity.User_Account.FullName.ToTitleCase();
                toJson.FatherName = entity.User_Account.FatherName.ToTitleCase();
                toJson.MotherName = entity.User_Account.MotherName.ToTitleCase();
                toJson.Remarks = entity.User_Account.Remarks;
                toJson.LastLoginDate = entity.User_Account.LastLoginDate;
                toJson.UserEmail = entity.User_Account.UserEmail;
                toJson.UserMobile = entity.User_Account.UserMobile;
                toJson.EmergencyMobile = entity.User_Account.EmergencyMobile;

                //to show in the student's List.
                toJson.NationalIdNumber = entity.User_Account.NationalIdNumber;
                toJson.BirthCertificateNumber = entity.User_Account.BirthCertificateNumber;
                toJson.DateOfBirth = entity.User_Account.DateOfBirth;

                //toJson.IsMilitary = entity.User_Account.IsMilitary;
                toJson.IsApproved = entity.User_Account.IsApproved;
                toJson.UserStatus = entity.User_Account.UserStatus.ToString();
                toJson.IsApproved = entity.User_Account.IsApproved;
                toJson.IsProfileConfirmed = !entity.User_Account.IsMigrate;
                //if (entity.User_Account.User_Rank != null)
                //{
                //    toJson.RankName = entity.User_Account.User_Rank.Name;
                //}
                //var userContactNumber = entity.User_Account.User_ContactNumber?.FirstOrDefault(x=>x.ContactNumberType==User_ContactNumber.ContactNumberTypeEnum.PersonalMobile);
                //if (userContactNumber != null)
                //    toJson.PersonalMobile = userContactNumber.ContactNumber;

                //var userContactEmail = entity.User_Account.User_ContactEmail?.FirstOrDefault(x => x.ContactEmailType == User_ContactEmail.ContactEmailTypeEnum.PersonalEmail);
                //if (userContactEmail != null)
                //    toJson.PersonalEmail = userContactEmail.ContactEmail;
                if (entity.User_Account.HR_Department != null)
                {
                    toJson.DeptName = entity.User_Account.HR_Department.Name;
                    toJson.DeptShortName = entity.User_Account.HR_Department.ShortName;
                }
                var userContactAddress = entity.User_Account.User_ContactAddress?.FirstOrDefault(x => x.ContactAddressType == User_ContactAddress.ContactAddressTypeEnum.Present);
                if (userContactAddress != null)
                    toJson.FullPresentAddress = userContactAddress.ToString();

                userContactAddress = entity.User_Account.User_ContactAddress?.FirstOrDefault(x => x.ContactAddressType == User_ContactAddress.ContactAddressTypeEnum.Permanent);
                if (userContactAddress != null)
                    toJson.FullPermanentAddress = userContactAddress.ToString();

                //$"{ContactAddressType.ToString().AddSpacesToSentence()}:"
            }
            else
            {
                toJson.UserName = entity.UserName.ToTitleCase();
                toJson.FullName = entity.FullName.ToTitleCase();
            }

            if (entity.Aca_StudyLevelTerm != null)
            {
                toJson.LevelTermName = entity.Aca_StudyLevelTerm.Name;
            }
            //if (entity.Aca_StudyTerm != null)
            //{
            //    toJson.TermName = entity.Aca_StudyTerm.Name;
            //}

            if (entity.Aca_Program != null)
            {
                toJson.ProgramShortTitle = entity.Aca_Program.ShortTitle;
            }
            if (entity.Aca_DeptBatch != null)
            {
                toJson.BatchName = entity.Aca_DeptBatch.Name;
            }
            if (entity.Aca_ClassSection != null)
            {
                toJson.ClassSectionName = entity.Aca_ClassSection.Name;
            }


            ////used for display class result
            //if (entity.Aca_ResultClass != null && entity.Aca_ResultClass.Count > 0)
            //{
            //    toJson.Aca_ResultClassJson = new Aca_ResultClassJson();
            //    var first = entity.Aca_ResultClass.FirstOrDefault();
            //    first.Map(toJson.Aca_ResultClassJson);
            //}
            ////used for display class result
            //if (entity.Aca_ResultComponent != null && entity.Aca_ResultComponent.Count > 0)
            //{
            //    //making null for circular dependency
            //    foreach (var resultComponent in entity.Aca_ResultComponent)
            //    {
            //        resultComponent.Aca_Class = null;
            //    }

            //    toJson.Aca_ResultComponentJsonList = new List<Aca_ResultComponentJson>();
            //    entity.Aca_ResultComponent.Map(toJson.Aca_ResultComponentJsonList);
            //}
        }
        private static void MapExtra(User_StudentJson json, User_Student toEntity)
        {
            toEntity.ExamRollNo = json.ExamRollNo;

            //if (json.User_AccountJson != null)
            //{
            //    toEntity.User_Account = new User_Account();
            //    toEntity.User_Account.Map(json.User_AccountJson);
            //}
            toEntity.UserName = json.UserName;
            toEntity.FullName = json.FullName;
        }
    }
}
