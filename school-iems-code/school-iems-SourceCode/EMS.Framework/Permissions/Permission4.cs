using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
//sample or example Demo class file
namespace EMS.Framework.Permissions
{
    public class Permission4Base
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public int? ParentID { get; set; }

        public List<Permission4Base> Childs { get; set; }

        public bool? IsChecked { get; set; }

        public bool IsExpanded { get; set; }
    }


    public class Permission4 : Permission4Base
    {
        private static List<Permission4Base> _Permissions;

        public static List<Permission4Base> GetPermissions()
        {
            _Permissions = new List<Permission4Base>();
            //Type[] test = typeof (Permission4).GetNestedTypes();
            Type[] parents = typeof(Permission4).GetNestedTypes().Where(t => t.IsSealed == true).ToArray();
            foreach (Type parent in parents)
            {
                //BuildTree(parent, null);
                int id =
                    Convert.ToInt32(
                        (parent.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false)[0] as
                         System.ComponentModel.DescriptionAttribute).Description);
                Permission4Base permission4 = new Permission4Base
                                                  {
                                                      ID = id,
                                                      ParentID = 0,
                                                      Title = parent.Name,
                                                      IsChecked = false,
                                                      IsExpanded = false,
                                                      Childs =
                                                          parent.GetNestedTypes().ToArray().Any()
                                                              ? BuildChilds(parent.GetNestedTypes().ToArray(), id)
                                                              : BuildNodes(parent, id)
                                                  };

                _Permissions.Add(permission4);
                //List<Permission4Base> childs =BuildChilds(parent.GetNestedTypes().Where(tt => tt.IsSealed == true).ToArray(),null);
            }

            return _Permissions;
        }

        //public static List<Permission4Base> GetPermissions(TList<UserModulePermission> userModulePermissions)
        //{
        //    _Permissions = new List<Permission4Base>();
        //    if (userModulePermissions != null && userModulePermissions.Count > 0)
        //    {
        //        Type[] parents = typeof(Permission4).GetNestedTypes().Where(t => t.IsSealed == true).ToArray();
        //        foreach (Type parent in parents)
        //        {
        //            bool includePermisison = false;
        //            var descriptionAttribute =
        //                parent.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false)[0] as
        //                System.ComponentModel.DescriptionAttribute;

        //            if (descriptionAttribute != null)
        //            {
        //                int id = Convert.ToInt32(descriptionAttribute.Description);

        //                if (userModulePermissions.FirstOrDefault(
        //                    p =>
        //                    p.ModulePermissionID ==
        //                    (int)UserModulePermission.UsersModulePermissionEnum.AcademicModule) !=
        //                    null &&
        //                    id ==
        //                    (int)UserModulePermission.UsersModulePermissionEnum.AcademicModule)
        //                {
        //                    includePermisison = true;
        //                }
        //                else if (userModulePermissions.FirstOrDefault(
        //                    p =>
        //                    p.ModulePermissionID ==
        //                    (int)UserModulePermission.UsersModulePermissionEnum.LibraryModule) !=
        //                         null &&
        //                         id ==
        //                         (int)
        //                         UserModulePermission.UsersModulePermissionEnum.LibraryModule)
        //                {
        //                    includePermisison = true;
        //                }
        //                else if (userModulePermissions.FirstOrDefault(
        //                    p =>
        //                    p.ModulePermissionID ==
        //                    (int)UserModulePermission.UsersModulePermissionEnum.HumanResourceModule) !=
        //                         null &&
        //                         id ==
        //                         (int)
        //                         UserModulePermission.UsersModulePermissionEnum.HumanResourceModule)
        //                {
        //                    includePermisison = true;
        //                }
        //                else if (userModulePermissions.FirstOrDefault(
        //                    p =>
        //                    p.ModulePermissionID ==
        //                    (int)UserModulePermission.UsersModulePermissionEnum.InventoryModule) !=
        //                         null &&
        //                         id ==
        //                         (int)
        //                         UserModulePermission.UsersModulePermissionEnum.InventoryModule)
        //                {
        //                    includePermisison = true;
        //                }
        //                else if (userModulePermissions.FirstOrDefault(
        //                    p =>
        //                    p.ModulePermissionID ==
        //                    (int)UserModulePermission.UsersModulePermissionEnum.AdmissionModule) !=
        //                         null &&
        //                         id ==
        //                         (int)
        //                         UserModulePermission.UsersModulePermissionEnum.AdmissionModule)
        //                {
        //                    includePermisison = true;
        //                }
        //                else if (userModulePermissions.FirstOrDefault(
        //                    p =>
        //                    p.ModulePermissionID ==
        //                    (int)UserModulePermission.UsersModulePermissionEnum.AccountingModule) !=
        //                         null &&
        //                         id ==
        //                         (int)
        //                         UserModulePermission.UsersModulePermissionEnum.AccountingModule)
        //                {
        //                    includePermisison = true;
        //                }
        //                else if (
        //                    userModulePermissions.FirstOrDefault(
        //                        p =>
        //                        p.ModulePermissionID ==
        //                        (int)UserModulePermission.UsersModulePermissionEnum.ToolsModule) != null &&
        //                    id == (int)UserModulePermission.UsersModulePermissionEnum.ToolsModule)
        //                {
        //                    includePermisison = true;
        //                }
        //                else if (userModulePermissions.FirstOrDefault(
        //                    p =>
        //                    p.ModulePermissionID ==
        //                    (int)UserModulePermission.UsersModulePermissionEnum.SystemSettingsModule) !=
        //                         null &&
        //                         id ==
        //                         (int)
        //                         UserModulePermission.UsersModulePermissionEnum.SystemSettingsModule)
        //                {
        //                    includePermisison = true;
        //                }
        //                else if (userModulePermissions.FirstOrDefault(
        //                    p =>
        //                    p.ModulePermissionID ==
        //                    (int)UserModulePermission.UsersModulePermissionEnum.UserScanDocumentModule) !=
        //                         null &&
        //                         id ==
        //                         (int)
        //                         UserModulePermission.UsersModulePermissionEnum.UserScanDocumentModule)
        //                {
        //                    includePermisison = true;
        //                }

        //                if (includePermisison)
        //                {
        //                    var permission4 = new Permission4Base
        //                                          {
        //                                              ID = id,
        //                                              ParentID = 0,
        //                                              Title = parent.Name,
        //                                              IsChecked = false,
        //                                              IsExpanded = false,
        //                                              Childs = BuildChilds(parent.GetNestedTypes().ToArray(), id)
        //                                          };

        //                    _Permissions.Add(permission4);
        //                }
        //            }
        //        }
        //    }

        //    return _Permissions;
        //}


        public static void BuildTree(Type t, int? parentID)
        {
    
            int id =
                Convert.ToInt32(
                    (t.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false)[0] as
                     System.ComponentModel.DescriptionAttribute).Description);
            Permission4 parent = new Permission4();
            parent.ID = id;
            parent.Title = t.Name;
            parent.ParentID = parentID;
            Type[] childs = t.GetNestedTypes().Where(tt => tt.IsSealed == true).ToArray();
            parent.Childs = BuildChilds(childs, id);
            _Permissions.Add(parent);

            //BuildTree(t, parent.ID);

            //Type[] classes = t.GetNestedTypes().Where(tt => tt.IsSealed == true).ToArray();
            //foreach (Type tt in classes)
            //{
            //    BuildTree(tt, parent.ID);
            //}
            //#region Blocked
            ////object[] os = t.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
            ////foreach (object o in os)
            ////{
            ////    System.ComponentModel.DescriptionAttribute ds = o as System.ComponentModel.DescriptionAttribute;

            ////}
            //#endregion

            //FieldInfo[] fields = t.GetFields();
            //foreach (FieldInfo f in fields)
            //{
            //    Permission4 p = new Permission4();
            //    p.ID = Convert.ToInt32(f.GetValue(f));
            //    p.Title = f.Name;
            //    p.ParentID = parent.ID;
            //    _Permissions.Add(p);
            //}
        }

        public static List<Permission4Base> BuildChilds(Type[] types, int? parentID)
        {
       
            List<Permission4Base> parent = new List<Permission4Base>();

            foreach (var type in types)
            {
                Permission4Base permission4 = new Permission4Base();
                int id =
                    Convert.ToInt32(
                        (type.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false)[0] as
                         System.ComponentModel.DescriptionAttribute).Description);

                permission4.ID = id;
                permission4.Title = type.Name;
                permission4.ParentID = parentID;
                permission4.IsChecked = false;
                permission4.IsExpanded = false;
                permission4.Childs = BuildChilds(type.GetNestedTypes().ToArray(), id);
                if (type.GetFields().Any())
                {
                    permission4.Childs.AddRange(BuildNodes(type, id));
                }
                parent.Add(permission4);
            }
            return parent;
        }

        public static List<Permission4Base> BuildNodes(Type type, int? parentID)
        {
         
            List<Permission4Base> nodes = new List<Permission4Base>();
            FieldInfo[] fields = type.GetFields();
            foreach (FieldInfo f in fields)
            {
                Permission4Base p4 = new Permission4Base();
                p4.ID = Convert.ToInt32(f.GetValue(f));
                p4.Title = f.Name;
                p4.ParentID = parentID;
                p4.IsChecked = false;
                p4.IsExpanded = false;
                p4.Childs = new List<Permission4Base>();
                nodes.Add(p4);
            }

            return nodes;
        }

        //[Description("1")]
        //public sealed class Academic
        //{
        //    [Description("101")]
        //    public sealed class StudentModule
        //    {
        //        [Description("10101")]
        //        public sealed class FreshmanModule
        //        {
        //            public const int Can_View_Freshman_Profile = 1010101;
        //            public const int Can_Update_Freshman_Profile = 1010102;
        //            public const int Can_Create_Freshman_Profile = 1010103;
        //            public const int Can_Generate_Freshman_password = 1010104;
        //        }

        //        [Description("10102")]
        //        public sealed class StudentProfile
        //        {
        //            public const int Can_View_Student_Profile = 1010201;
        //            public const int Can_Create_Student_Profile = 1010202;
        //            public const int Can_Update_Student_Profile = 1010203;
        //            public const int Can_Change_Student_TrackingID = 1010204;
        //            public const int Can_Update_Student_Transcript_Information = 1010205;
        //            public const int Can_View_Student_PersonalDetail = 1010206;
        //            public const int Can_View_Student_UserContact = 1010207;
        //        }

        //        [Description("10103")]
        //        public sealed class StudentEducation
        //        {
        //            public const int Can_View_Student_Education = 1010301;
        //            public const int Can_Update_Student_Education = 1010302;
        //        }

        //        [Description("10104")]
        //        public sealed class StudentSendEmailSms
        //        {
        //            public const int Can_Send_Email_To_Student = 1010401;
        //        }

        //        [Description("10105")]
        //        public sealed class StudentCourses
        //        {
        //            public const int Can_View_Student_Course = 1010501;
        //            public const int Can_Edit_Student_Courses = 1010502;
        //        }

        //        [Description("10106")]
        //        public sealed class TransferredStudentAndCourses
        //        {
        //            public const int Can_View_Transferred_Student = 1010601;
        //            public const int Can_Save_Transferred_Student = 1010602;
        //            public const int Can_Delete_Transferred_Student = 1010603;
        //            public const int Can_View_Transferred_Student_Courses = 1010604;
        //            public const int Can_Save_Transferred_Student_Courses = 1010605;
        //            public const int Can_Edit_Transferred_Student_Courses = 1010606;
        //            public const int Can_Delete_Transferred_Student_Courses = 1010607;
        //            public const int Can_Validate_Transferred_Student_Courses = 1010608;
        //        }

        //        [Description("10107")]
        //        public sealed class StudentCourseResultSynchronization
        //        {
        //            public const int Can_Synchronize_I_UW_Blank_Grades = 1010701;
        //            public const int Can_Synchronize_Course_Result = 1010702;
        //            public const int Can_Synchronize_Student_Data = 1010703;
        //        }

        //        [Description("10108")]
        //        public sealed class StudentCourseResult
        //        {
        //            public const int Can_Lock_Student_Course_Result = 1010801;
        //        }

        //        [Description("10109")]
        //        public sealed class StudentDocumentPrint
        //        {
        //            public const int Can_Print_Students_Official_Transcript = 1010901;
        //            public const int Can_Print_Students_Temporary_Transcript = 1010902;
        //            public const int Can_Print_Students_Certificate = 1010903;
        //            public const int Can_Print_Students_Testimonial = 1010904;
        //            public const int Can_Print_Students_Awards = 1010905;
        //            public const int Can_Print_Provisional_Students_Transcript = 1010906;
        //            public const int Can_Print_Students_TemporaryID = 1010907;
        //            public const int Can_Print_Provisional_Students_Testimonial = 1010908;
        //            public const int Can_Print_Students_Accounts_Payslip = 1010909;
        //            public const int Can_Print_Students_Migration_Certificate = 1010910;
        //            public const int Can_Print_Students_Certificate_Of_Medium_Of_Instruction = 1010911;

        //        }

        //        [Description("10110")]
        //        public sealed class StudentAccountsRecords
        //        {
        //            public const int Can_View_Student_Accounts_Records = 1011001;
        //            public const int Can_Edit_Student_Accounts_Records = 1011002;
        //            public const int Can_Delete_Student_Accounts_Records = 1011003;
        //            public const int Can_Print_Student_Accounts_Payslip = 1011004;
        //            public const int Can_Upload_Student_Accounts_Documents_For_Posting = 1011005;
        //            public const int Can_Print_Student_Accounts_Ledger = 1011006;
        //            public const int Can_Define_Payable_Amount = 1011007;
        //            public const int Can_Clear_Account_For_ExamPermit = 1011008;
        //            public const int Can_Override_Vat_Amount = 1011009;
        //            public const int Can_View_Student_Accounts_Payslip = 1011010;
        //            public const int Can_Save_Student_Accounts_Payslip_To_Disk = 1011011;
        //        }

        //        [Description("10111")]
        //        public sealed class StudentAssesment
        //        {
        //            public const int Can_View = 1011101;
        //            public const int Can_Edit = 1011102;
        //        }

        //        [Description("10112")]
        //        public sealed class StudentRegistration
        //        {
        //            public const int Can_View_Student_Registration = 1011201;
        //            public const int Can_Print_Students_Registration = 1011202;
        //            public const int Can_Change_PaymentSlip_Bank = 1011203;
        //        }

        //        [Description("10113")]
        //        public sealed class StudentScholership
        //        {
        //            public const int CanView = 1011301;
        //            public const int CanSave = 1011302;
        //            public const int CanAudit = 1011303;
        //            public const int CanClear = 1011304;
        //        }

        //        [Description("10114")]
        //        public sealed class StudentDisciplinaryActions
        //        {
        //            public const int CanView = 1011401;
        //            public const int CanManage = 1011402;
        //        }
        //    }

        //    [Description("102")]
        //    public sealed class RegistrationModule
        //    {
        //        public const int Can_Register_Current_Semester = 10201;
        //        public const int Can_Register_Other_Semester = 10202;
        //        public const int Can_Register = 10203;
        //        public const int Can_Register_Regular_Student = 10204;
        //        public const int Can_Register_Freshmen_Student = 10205;
        //        public const int Can_Register_Probation_Student = 10206;
        //        public const int Can_Register_Blocked_Student = 10207;
        //        public const int Can_Register_Enrollment_Time_Exceeded_Student = 10208;
        //        public const int Can_Register_Tpe_Pending_Student = 10209;
        //        public const int Can_Register_Not_In_Slot_Student = 10210;
        //        public const int Can_Register_More_Credit_Than_Max_Credit = 10211;
        //        public const int Can_Register_Less_Credit_Then_Min_Credit = 10212;
        //        public const int Can_Register_Incomplete_PreRequisite_Course = 10213;
        //        public const int Can_Register_Course_Not_In_Curriculum = 10214;
        //        public const int Can_DropUndrop_Course = 10215;
        //        public const int Can_Delete_Course = 10216;
        //        public const int Can_Clear_Registration = 10217;
        //        public const int Can_Change_Registration_Status = 10218;
        //        public const int Can_Register_Close_Section_Force = 10219;
        //        public const int Can_Register_Freshmen_Section_Force = 10220;
        //        public const int Can_Valid_Invalid_Course = 10221;
        //        public const int Can_Override_Section_Tolerance = 10222;
        //        public const int Can_Confirm_Registration = 10223;
        //        public const int Can_Registerer_ReadmissionRequire_Student = 10224;
        //        public const int Can_Change_Drop_Percent = 10225;
        //        public const int Can_Register_IndividualCourse_From_CourseGroup = 10226;
        //    }

        //    [Description("103")]
        //    public sealed class CurriculumAndCourseModule
        //    {
        //        [Description("10301")]
        //        public sealed class Course
        //        {
        //            public const int Can_View_Course = 1030101;
        //            public const int Can_Edit_Course = 1030102;
        //            public const int Can_Edit_OfferedCourse = 1030103;
        //            public const int Can_Edit_Section = 1030104;
        //        }

        //        [Description("10401")]
        //        public sealed class Curriculum
        //        {
        //            public const int Can_Create_Curriculum = 1040101;
        //            public const int Can_Update_Curriculum = 1040102;
        //            public const int Can_Delete_Curriculum = 1040103;
        //            public const int Can_MapCourseFromCurriculum = 1040104;
        //        }
        //    }


        //    [Description("105")]
        //    public sealed class Semester
        //    {
        //        public const int Can_Create_Semester = 10501;
        //        public const int Can_Update_Semester = 10502;
        //    }

        //    [Description("106")]
        //    public sealed class WebSiteModule
        //    {
        //        public const int Can_Edit_Event = 10601;
        //        public const int Can_Edit_TopNews = 10602;
        //        public const int Can_Edit_Notice = 10603;
        //        public const int Can_Edit_Upcoming_Event = 10604;
        //    }

        //    [Description("107")]
        //    public sealed class ConvocationModule
        //    {
        //        public const int Can_Change_Convocation = 10701;
        //        public const int Can_Edit_Convocation_Applicant = 10702;
        //        public const int Can_View_Convocation_Applicant = 10703;
        //        public const int Can_Process_VUES = 10704;
        //        public const int Can_Process_OGC = 10705;
        //        public const int Can_Process_Audit = 10706;
        //        public const int Can_Process_OfficeOfRecords = 10707;
        //        public const int Can_Process_All = 10708;
        //        public const int Can_Manage_Convocation = 10709;
        //    }

        //    [Description("108")]
        //    public sealed class GradeSheet
        //    {
        //        public const int Can_Upload_GradeSheet = 10801;
        //        public const int Can_View_GradeSheet = 10802;
        //        public const int Can_Update_GradeSheet = 10803;
        //        public const int Can_View_Audit_Grades = 10804;
        //        public const int Can_Delete_GradeSheet = 10805;

        //    }

        //    [Description("109")]
        //    public sealed class CredentialsModule
        //    {
        //        public const int Can_Change_Group_Permission = 10901;
        //        public const int Can_Edit_Group = 10902;
        //        public const int Can_Edit_User = 10903;
        //    }

        //    [Description("110")]
        //    public sealed class ProbationModule
        //    {
        //        public const int Can_View_Probation_Student_List = 11001;
        //        public const int Can_Save_Appointment = 11001;
        //        public const int Can_Edit_Appointment = 11001;
        //        public const int Can_View_Appointment_List = 11001;
        //        public const int Can_Print_Guardian_Agreement = 11001;
        //        public const int Can_Print_Probation_Counseiling_Report = 11001;
        //    }
        //}

        //[Description("2")]
        //public sealed class Library
        //{
        //    [Description("201")]
        //    public sealed class IssueBook
        //    {
        //        public const int Can_Issue_Book = 20101;
        //    }

        //    [Description("202")]
        //    public sealed class ReturnBook
        //    {
        //        public const int Can_Return_Book = 20201;
        //    }

        //    [Description("203")]
        //    public sealed class ManageBook
        //    {
        //        public const int Can_Manage_Book = 20301;
        //        public const int Can_Manage_BookCopies = 20302;
        //        public const int Can_View_CopyTransactions = 20303;
        //        public const int Can_Import_Book = 20304;
        //    }

        //    [Description("204")]
        //    public sealed class LibrarySettings
        //    {
        //        public const int Can_Manage_Settings = 20401;
        //    }

        //    [Description("205")]
        //    public sealed class Borrower
        //    {
        //        public const int Can_View_Borrower = 20501;
        //        public const int Can_Make_Borrower_Temporary_Inactive = 20502;
        //    }

        //    [Description("206")]
        //    public sealed class Account
        //    {
        //        public const int Can_Make_Fine_Payment = 20601;
        //        public const int Can_View_Fine = 20602;
        //    }

        //    [Description("207")]
        //    public sealed class Supplier
        //    {
        //        public const int Can_Manage_Supplier = 20701;
        //        public const int Can_View_Supplier = 20702;
        //    }

        //    [Description("208")]
        //    public sealed class BookSubject
        //    {
        //        public const int Can_Manage_Subject = 20801;
        //    }

        //    [Description("209")]
        //    public sealed class Email
        //    {
        //        public const int Can_Send_Email = 20901;
        //    }

        //    [Description("210")]
        //    public sealed class Synchronize
        //    {
        //        public const int Can_Synchronize_Borrower = 21001;
        //    }

        //    [Description("211")]
        //    public sealed class UnreturnedBooks
        //    {
        //        public const int Can_View_UnReturnedBooks = 21101;
        //    }

        //    [Description("212")]
        //    public sealed class ChangeBooks
        //    {
        //        public const int Can_Change_BarcodedBooks = 21201;
        //    }

        //    [Description("213")]
        //    public sealed class GenerateBookSpinTags
        //    {
        //        public const int Can_Generate_BookSpinTags = 21301;
        //        public const int Can_Print_BookSpinTags = 21302;
        //    }
            
        //}

        //[Description("3")]
        //public sealed class HumanResource
        //{
        //    [Description("301")]
        //    public sealed class Employee
        //    {
        //        [Description("30101")]
        //        public sealed class Profile
        //        {
        //            public const int CanCreateEmployeeProfile = 3010101;
        //            public const int CanUpdateEmployeeProfile = 3010102;
        //            public const int CanDeleteEmployeeProfile = 3010103;
        //            public const int CanViewEmployeeProfile = 3010104;
        //            public const int CanChangeEmployeeJobStatus = 3010105;
        //            public const int CanChangeUserStatus = 3010106;
        //            public const int CanChangeEmployeeAccountNo = 3010107;
        //            public const int CanChangeEmployeeTinNo = 3010108;
        //            public const int CanChangeEmployeeSupervisor = 3010109;
        //            public const int CanChangeEmployeeDepartment = 3010110;
        //            public const int CanChangeEmployeeDesignations = 3010111;
        //            public const int CanChangeEmployeeClassification = 3010112;
        //            public const int CanChangeEmployeeType = 3010113;
        //            public const int CanChangeEmployeePosition = 3010114;
        //            public const int CanChangeEmployeeJoinningDate = 3010115;
        //            public const int CanChangeEmployeeLeavingDate = 3010116;
        //            public const int CanChangeTrackingId = 3010117;
        //            public const int CanViewEmployeePersonalDetail = 3010118;
        //            public const int CanChangeEmployeePersonalDetail = 3010119;
        //            public const int CanViewEmployeeHrDetail = 3010120;
        //            public const int CanChangeEmployeeHrDetail = 3010121;
        //            public const int CanChangeEmployeeIncrementMonth = 3010122;
        //            public const int CanChangeEmployeeUserStatus = 3010123;
        //            public const int CanChangeEmployeeAcademicDepartment = 3010124;
        //            //class 4
        //            public const int CanCreateEmployeeProfile_Class4 = 3010125;
        //            public const int CanUpdateEmployeeProfile_Class4 = 3010126;
        //            public const int CanViewEmployeeProfile_Class4 = 3010127;
        //            public const int CanViewEmployeeHrDetail_Class4 = 3010128;
        //            public const int CanChangeEmployeeHrDetail_Class4 = 3010129;
        //            public const int CanChangeEmployeeJobStatus_Class4 = 3010130;
        //            public const int CanChangeUserStatus_Class4 = 3010131;
        //            public const int CanChangeEmployeeAccountNo_Class4 = 3010132;
        //            public const int CanChangeEmployeeTinNo_Class4 = 3010133;
        //            public const int CanChangeEmployeeSupervisor_Class4 = 3010134;
        //            public const int CanChangeEmployeeDepartment_Class4 = 3010135;
        //            public const int CanChangeEmployeeDesignations_Class4 = 3010136;
        //            public const int CanChangeEmployeeClassification_Class4 = 3010137;
        //            public const int CanChangeEmployeeType_Class4 = 3010138;
        //            public const int CanChangeEmployeePosition_Class4 = 3010139;
        //            public const int CanChangeEmployeeJoinningDate_Class4 = 3010140;
        //            public const int CanChangeEmployeeLeavingDate_Class4 = 3010141;
        //            public const int CanChangeTrackingId_Class4 = 3010142;
        //            public const int CanViewEmployeePersonalDetail_Class4 = 3010143;
        //            public const int CanChangeEmployeePersonalDetail_Class4 = 3010144;

        //            public const int CanChangeEmployeeIncrementMonth_Class4 = 3010145;
        //            public const int CanChangeEmployeeUserStatus_Class4 = 3010146;
        //            public const int CanChangeEmployeeAcademicDepartment_Class4 = 3010147;
                    
        //        }

        //        [Description("30102")]
        //        public sealed class EmployeeRecord
        //        {
        //            [Description("3010201")]
        //            public sealed class EmployeeParticulars
        //            {
        //                public const int CanViewEmployeeParticulars = 301020101;
        //                public const int CanChangeEmployeeParticulars = 301020102;
        //            }

        //            [Description("3010202")]
        //            public sealed class EmployeeEducation
        //            {
        //                public const int CanViewEmployeeEducation = 301020201;
        //                public const int CanChangeEmployeeEducation = 301020202;

        //            }

        //            [Description("3010203")]
        //            public sealed class EmployeePublication
        //            {
        //                public const int CanViewEmployeePublication = 301020301;
        //                public const int CanChangeEmployeePublication = 301020302;
        //            }

        //            [Description("3010204")]
        //            public sealed class EmployeeDisciplinaryAction
        //            {
        //                public const int CanViewEmployeeDisciplinaryAction = 301020401;
        //                public const int CanChangeEmployeeDisciplinaryAction = 301020402;
        //                public const int CanViewEmployeeDisciplinaryAction_Class4 = 301020403;
        //                public const int CanChangeEmployeeDisciplinaryAction_Class4 = 301020404;
        //            }

        //            [Description("3010205")]
        //            public sealed class EmployeeExperience
        //            {
        //                public const int CanViewEmployeeExperience = 301020501;
        //                public const int CanChangeEmployeeExperience = 301020502;
        //            }

        //            [Description("3010206")]
        //            public sealed class EmploymentHistory
        //            {
        //                public const int CanViewEmploymentHistory = 301020601;
        //                public const int CanChangeEmploymentHistory = 301020602;
        //                public const int CanViewEmploymentHistory_Class4 = 301020603;
        //                public const int CanChangeEmploymentHistory_Class4 = 301020604;
        //                public const int CanChangeEmploymentHistory_Forced = 301020605;
        //            }
        //        }

        //        [Description("30103")]
        //        public sealed class CoursesTakenByFaculty
        //        {
        //            public const int CanViewCoursesTakenByFaculty = 3010301;
        //            public const int CanUpdatePaymentStatusForCourse = 3010301;
        //        }

        //        [Description("30104")]
        //        public sealed class TPE
        //        {
        //            public const int CanViewTPEReport = 3010401;
        //            //public const int CanViewTPEReport = 3010402;
        //        }

        //        [Description("30105")]
        //        public sealed class EmployeeAttendance
        //        {
        //            public const int CanCreateEmployeeAttendance = 3010501;
        //            public const int CanUpdateEmployeeAttendance = 3010502;
        //            public const int CanDeleteEmployeeAttendance = 3010503;
        //            public const int CanViewEmployeeAttendance = 3010504;
        //            public const int CanViewEmployeeAttendance_Class4 = 3010505;
        //            public const int CanCreateEmployeeAttendance_Class4 = 3010506;
        //            public const int CanUpdateEmployeeAttendance_Class4 = 3010507;
        //            public const int CanDeleteEmployeeAttendance_Class4 = 3010508;
        //        }

        //        [Description("30106")]
        //        public sealed class EmployeeLeaveRecord
        //        {
        //            [Description("3010601")]
        //            public sealed class EmployeeLeaveApplication
        //            {
        //                public const int CanCreateEmployeeLeaveApplication = 301060101;
        //                public const int CanUpdateEmployeeLeaveApplication = 301060102;
        //                public const int CanDeleteEmployeeLeaveApplication = 301060103;
        //                public const int CanViewEmployeeLeaveApplication = 301060104;

        //                public const int CanCreateEmployeeLeaveApplication_Class4 = 301060105;
        //                public const int CanUpdateEmployeeLeaveApplication_Class4 = 301060106;
        //                public const int CanDeleteEmployeeLeaveApplication_Class4 = 301060107;
        //                public const int CanViewEmployeeLeaveApplication_Class4 = 301060108;
        //            }

        //            [Description("3010602")]
        //            public sealed class EmployeeLeaveAllocation
        //            {
        //                public const int CanCreateEmployeeLeaveAllocation = 301060201;
        //                public const int CanUpdateEmployeeLeaveAllocation = 301060202;
        //                public const int CanDeleteEmployeeLeaveAllocation = 301060203;
        //                public const int CanViewEmployeeLeaveAllocation = 301060204;
        //                public const int CanSynchronizeEmployeeLeaveAllocation = 301060205;
        //                public const int CanViewEmployeeLeaveAllocationReport = 301060206;

        //                public const int CanCreateEmployeeLeaveAllocation_Class4 = 301060207;
        //                public const int CanUpdateEmployeeLeaveAllocation_Class4 = 301060208;
        //                public const int CanDeleteEmployeeLeaveAllocation_Class4 = 301060209;
        //                public const int CanViewEmployeeLeaveAllocation_Class4 = 301060210;
        //                public const int CanSynchronizeEmployeeLeaveAllocation_Class4 = 301060211;
        //                public const int CanViewEmployeeLeaveAllocationReport_Class4 = 301060212;
        //            }
        //        }

        //        [Description("30107")]
        //        public sealed class EmployeeSalary
        //        {
        //            public const int CanCreateEmployeeSalary = 3010701;
        //            public const int CanUpdateEmployeeSalary = 3010702;
        //            public const int CanDeleteEmployeeSalary = 3010703;
        //            public const int CanViewEmployeeSalary = 3010704;
        //            public const int CanSyncEmployeeSalary = 3010705;
        //            public const int CanViewEmployeeSalary_ExceptHM = 3010706;
        //            public const int CanCreateEmployeeSalary_Class4 = 3010707;
        //            public const int CanUpdateEmployeeSalary_Class4 = 3010708;
        //            public const int CanDeleteEmployeeSalary_Class4 = 3010709;
        //            public const int CanViewEmployeeSalary_Class4 = 3010710;
        //        }
        //        [Description("30109")]
        //        public sealed class EmployeeDocument
        //        {
        //            public const int CanCreateEmployeeDocument = 3010901;
        //            public const int CanUpdateEmployeeDocument = 3010902;
        //            public const int CanDeleteEmployeeDocument = 3010903;
        //        }

        //        [Description("30110")]
        //        public sealed class ResumeDocument
        //        {
        //            public const int Can_View_Employee_Resume = 3011001;
        //            public const int Can_Change_Employee_Resume = 3011002;
        //        }                
        //    }

        //    [Description("302")]
        //    public sealed class SearchEmployees
        //    {
        //        public const int CanSearchEmployees = 30201;
        //    }

        //    [Description("303")]
        //    public sealed class PaySlip
        //    {
        //        public const int CanChangePaySlip = 30301;
        //        //public const int CanUpdatePaySlip = 30302;
        //        //public const int CanDeletePaySlip = 30303;
        //        public const int CanViewPaySlip = 30304;

        //        public const int CanViewMainPayslipReport = 30305;
        //        public const int CanViewPayslipForEmployeesReport = 30306;
        //        public const int CanViewPayslipToExcelForBankReport = 30307;
        //        public const int CanViewDetailPayslipReport = 30308;
        //        public const int CanViewPayslipByFiscalYearReport = 30309;

        //        public const int CanAddPaySlip = 30310;
        //        public const int CanCopyPaySlip = 30311;
        //        public const int CanViewPaySlip_ExceptHM = 30312;
        //        //class 4
        //        public const int CanChangePaySlip_Class4 = 30313;
        //        public const int CanViewPaySlip_Class4 = 30314;
        //        public const int CanAddPaySlip_Class4 = 30315;
        //        public const int CanCopyPaySlip_Class4 = 30316;
        //        public const int CanViewPayslipReport_Class4 = 30317;
        //        public const int CanViewPayslipByFiscalYearReport_Class4 = 30318;

        //    }

        //    [Description("304")]
        //    public sealed class Payroll
        //    {
        //        public const int CanCreatePayroll = 30401;
        //        public const int CanUpdatePayroll = 30402;
        //        public const int CanViewPayroll = 30403;
        //    }

        //    [Description("305")]
        //    public sealed class CodeFiles
        //    {
        //        [Description("30501")]
        //        public sealed class Department
        //        {
        //            public const int CanViewDepartment = 3050101;
        //            public const int CanManageDepartment = 3050102;
        //            //public const int CanDeleteDepartment = 3050103;
        //            //public const int CanViewDepartment = 3050104;
        //        }

        //        [Description("30502")]
        //        public sealed class Designation
        //        {
        //            public const int CanViewDesignation = 3050201;
        //            public const int CanManageDesignation = 3050202;
        //            //public const int CanDeleteDesignation = 3050203;
        //            //public const int CanViewDesignation = 3050204;
        //        }

        //        [Description("30503")]
        //        public sealed class Classification
        //        {
        //            public const int CanViewClassification = 3050301;
        //            public const int CanManageClassification = 3050302;
        //            //public const int CanDeleteClassification = 3050303;
        //            //public const int CanViewClassification = 3050304;
        //        }

        //        [Description("30504")]
        //        public sealed class Position
        //        {
        //            public const int CanViewPosition = 3050401;
        //            public const int CanManagePosition = 3050402;
        //            //public const int CanDeletePosition = 3050403;
        //            //public const int CanViewPosition = 3050404;
        //        }

        //        [Description("30505")]
        //        public sealed class EmployeeType
        //        {
        //            public const int CanViewEmployeeType = 3050501;
        //            public const int CanManageEmployeeType = 3050502;
        //            //public const int CanDeleteEmployeeType = 3050503;
        //            //public const int CanViewEmployeeType = 3050504;
        //        }

        //        [Description("30506")]
        //        public sealed class HrBank
        //        {
        //            public const int CanViewHrbank = 3050601;
        //            public const int CanManageHrbank = 3050602;
        //            //public const int CanDeleteHrbank = 3050603;
        //            //public const int CanViewHrbank = 3050604;
        //        }
        //    }

        //    [Description("306")]
        //    public sealed class LeaveManager
        //    {
        //        public const int CanViewLeaveManager = 30603;
        //    }

        //    //[Description("307")]
        //    //public sealed class ReportManager
        //    //{
        //    //    public const int CanViewReportManager = 30701;
        //    //}

        //    [Description("308")]
        //    public sealed class HolidayTrackManager
        //    {
        //        public const int CanChangeHolidayTrack = 30801;
        //        //public const int CanUpdateHolidayTrack = 30802;
        //        //public const int CanDeleteHolidayTrack = 30803;
        //        public const int CanViewHolidayTrack = 30804;
        //        public const int CanGenerateHoldayTrack = 30805;
        //    }

        //    [Description("309")]
        //    public sealed class AttendanceManager
        //    {
        //        public const int CanViewAttendanceReport = 30901;
        //        public const int CanSyncEmployeeForDevice = 30902;
        //        public const int CanPostAttendance = 30903;
        //        public const int CanGetCampusList = 30904;
        //        public const int CanViewAttendanceReport_Class4 = 30905;
        //    }

        //    [Description("310")]
        //    public sealed class Class4Manager
        //    {
        //        public const int CanCreateClass4Profile = 31001;
        //    }
        //}

        //[Description("4")]
        //public sealed class Inventory
        //{
        //}

        //[Description("5")]
        //public sealed class Admission
        //{
        //    [Description("501")]
        //    public sealed class AdmissionExam
        //    {
        //        public const int CanView = 50101;
        //        public const int CanChange = 50102;

        //    }

        //    [Description("502")]
        //    public sealed class Applicant
        //    {
        //        public const int CanView = 50201;
        //        public const int CanChange = 50202;
        //        public const int CanRegister = 50205;
                
        //    }

        //    [Description("503")]
        //    public sealed class Report
        //    {
        //        public const int CanViewApplicantSummaryReport = 50301;
        //        public const int CanViewAccountsOrReport = 50302;
        //    }

        //    [Description("504")]
        //    public sealed class Result
        //    {
        //        public const int CanChange = 50401;
        //        public const int CanViewApplicantsResultReport = 50402;
        //    }
        //}

        //[Description("6")]
        //public sealed class Accounting
        //{
        //    [Description("601")]
        //    public sealed class Ledger
        //    {
        //        public const int CanView = 60101;
        //        public const int CanSave = 60102;
        //        public const int CanUpdate = 60103;
        //        public const int CanDelete = 60104;
        //    }

        //    [Description("602")]
        //    public sealed class Voucher
        //    {
        //        public const int CanView = 60201;
        //        public const int CanSave = 60202;
        //        public const int CanPost = 60203;
        //        public const int CanUnpost = 60204;
        //        public const int CanDelete = 60205;
        //    }

        //    [Description("602")]
        //    public sealed class Journals
        //    {
        //        public const int CanView = 60201;
        //        public const int CanSave = 60202;
        //        public const int CanPost = 60203;
        //        public const int CanUnpost = 60204;
        //        public const int CanDelete = 60205;
        //    }

        //    [Description("603")]
        //    public sealed class LedgerTransactions
        //    {
        //        public const int CanViewLedgerTransactions = 60301;
        //    }

        //    [Description("604")]
        //    public sealed class Reports
        //    {
        //        public const int CanViewBalanceSheet = 60401;
        //        public const int CanViewIncomeStatement = 60402;
        //        public const int CanViewChartOfAccounts = 60403;
        //    }

        //    [Description("605")]
        //    public sealed class DayBooks
        //    {
        //        public const int CanViewDayBook = 60501;
        //        public const int CanPrintDayBook = 60502;
        //    }
        //}

        //[Description("7")]
        //public sealed class Tools
        //{
        //    [Description("701")]
        //    public sealed class BulkPrint
        //    {
        //        public const int Can_Bulk_Print_SemesterGradeReport = 7011;
        //        public const int Can_Bulk_Print_SemesterResultReport = 7012;
        //    }

        //    [Description("702")]
        //    public sealed class BulkImageUpload
        //    {
        //        public const int Can_Upload_Users_Image_In_Bulk = 7021;
        //    }

        //    [Description("703")]
        //    public sealed class PermitPrint
        //    {
        //        public const int Can_Print_Exam_Permit = 7031;
        //    }

        //    [Description("704")]
        //    public sealed class Logs
        //    {
        //        [Description("7041")]
        //        public sealed class ApplicationErrorLog
        //        {
        //            public const int Can_View_Application_ErrorLog = 70411;
        //        }

        //        [Description("7042")]
        //        public sealed class ActivityLog
        //        {
        //            public const int Can_View_ActivityLog = 70421;
        //        }

        //        [Description("7043")]
        //        public sealed class Log
        //        {
        //            public const int Can_View_Log = 70431;
        //        }
        //    }

        //    [Description("705")]
        //    public sealed class ExanSeatPlanPrint
        //    {
        //        public const int Can_Print_Exam_SeatPlan = 7051;
        //    }

        //    [Description("706")]
        //    public sealed class SectionManager
        //    {
        //        public const int Can_View_Result_ChangeRequest = 7061;
        //        public const int Can_Allow_Result_ChangeRequest = 7062;
        //        public const int Can_Export_Section_Student_List = 7063;
        //        public const int Can_Print_Section_Grade_Report = 7064;
        //        public const int Can_Print_Section_Grade_Summary_Report = 7064;
        //        public const int Can_View_Section_Student_Grade_Report = 7065;
        //        public const int Can_Export_Result_ChangeRequest = 7066;
        //    }

        //    [Description("707")]
        //    public sealed class CurriculumManager
        //    {
        //        public const int Can_View_Curriculum_Manager = 7071;
        //        public const int Can_Change_Curriculum_Manager = 7072;
        //    }

        //    [Description("708")]
        //    public sealed class CurriculumCourseManager
        //    {
        //        public const int Can_View_Curriculum_Course_Manager = 7081;
        //        public const int Can_Change_Curriculum_Course_Manager = 7082;
        //        public const int Can_Print_Curriculum = 7083;
        //        public const int Can_Import_Curriculum_Courses = 7084;
        //    }

        //    [Description("709")]
        //    public sealed class CourseManager
        //    {
        //        public const int Can_View_Course_Manager = 7091;
        //        public const int Can_Change_Course_Manager = 7092;
        //    }

        //    [Description("710")]
        //    public sealed class MajorManager
        //    {
        //        public const int Can_View_Major_Manager = 7101;
        //        public const int Can_Change_Major_Manager = 7102;
        //    }

        //    [Description("711")]
        //    public sealed class UserListManager
        //    {
        //        public const int Can_View_Employee_List_Manager = 7111;
        //        public const int Can_View_Student_List_Manager = 7112;
        //    }

        //    [Description("712")]
        //    public sealed class AgreementManager
        //    {
        //        public const int Can_View_AgreementManager = 7121;
        //        public const int Can_Process_AgreementManager = 7122;
        //    }

        //    [Description("713")]
        //    public sealed class CourseGroupManager
        //    {
        //        public const int Can_View_Course_Group_Manager = 7131;
        //        public const int Can_Change_Course_Group_Course = 7132;
        //        public const int Can_Change_Course_Group = 7133;
        //    }

        //    [Description("714")]
        //    public sealed class WorkProcessDocumentsManager
        //    {
        //        public const int Can_View_WorkProcessDocumentsManager = 7141;
        //        public const int Can_Process_WorkProcessDocumentsManager = 7142;
        //    }

        //    [Description("715")]
        //    public sealed class CourseAssignManager
        //    {
        //        public const int Can_View_Course_Assign_Manager = 7151;
        //        public const int Can_Change_Course_Assign = 7152;
        //        public const int Can_Assign_Course_To_Teacher = 7153;
        //        public const int Can_Resign_Course_From_Teacher = 7154;
        //    }

        //    [Description("716")]
        //    public sealed class BulkSmsSendStudentResultManager
        //    {
        //        public const int Can_View_BulkSms_Student_Result = 7161;
        //        public const int Can_Send_BulkSms_Student_Result = 7162;
        //    }

        //    [Description("717")]
        //    public sealed class SmsManager
        //    {
        //        public const int Can_View_Sms_Manager = 7171;
        //        public const int Can_Send_Sms_Manager = 7172;
        //    }

        //    [Description("718")]
        //    public sealed class BarcodeLabelPrint
        //    {
        //        public const int Can_Print_Barcode_Label = 7181;
        //    }

        //    [Description("719")]
        //    public sealed class DisciplinaryActionsManager
        //    {
        //        public const int Can_View_DisciplinaryActions = 7191;
        //        public const int Can_Manage_DisciplinaryActions = 7192;
        //    }

        //    [Description("720")]
        //    public sealed class EmployeeLeaveApplicationProcessManager
        //    {
        //        public const int Can_View_Employee_Leave_Process = 7201;
        //        public const int Can_Process_Multiple_Employee_Leave = 7202;
        //    }

        //    [Description("721")]
        //    public sealed class BulkSmsSenderManager
        //    {
        //        public const int Can_View_Bulk_Sms_Sender = 7211;
        //        public const int Can_Send_Bulk_Sms_Sender = 7222;
        //        public const int Can_Export_Bulk_Sms_Sender = 7233;
        //    }

        //    [Description("722")]
        //    public sealed class UserProfilePictureExportManager
        //    {
        //        public const int Can_View_User_ProfilePicture_Export = 7221;
        //    }

        //    [Description("723")]
        //    public sealed class EmployeeAttendanceManager
        //    {
        //        public const int Can_View_Employee_Attendance_Manager = 7231;
        //        public const int Can_Change_Employee_Attendance_Manager = 7232;
        //    }

        //    [Description("724")]
        //    public sealed class RegistrationSectionBlockManager
        //    {
        //        public const int Can_View_Registration_Section_Block_Manager = 7241;
        //        public const int Can_Change_Registration_Section_Block_Course = 7242;
        //        public const int Can_Change_Registration_Section_Block = 7243;
        //    }

        //    [Description("725")]
        //    public sealed class EmploymentHistoryManager
        //    {
        //        public const int Can_View_Employment_History_Manager = 7251;
        //    }

        //    [Description("726")]
        //    public sealed class SmsProvider
        //    {
        //        public const int Can_View_Sms_Provider = 7261;
        //        public const int Can_Manage_Sms_Provider = 7262;
        //    }
            
        //}

        //[Description("9")]
        //public sealed class SystemPermission
        //{
        //    [Description("901")]
        //    public sealed class UserInterface
        //    {
        //        public const int Can_Log_UMS_Client = 90101;
        //        public const int Can_Log_Web_Client = 90102;
        //    }

        //    [Description("902")]
        //    public sealed class ApplicationInterface
        //    {
        //        public const int CanPostAttendance = 90201;
        //    }

        //    [Description("903")]
        //    public sealed class ApplicationSettings
        //    {
        //        public const int CanManageWorkProcessStepPermission = 90301;

        //        public const int CanManageUserAccess = 90302;

        //        public const int CanManageUserGroupPermissions = 90303;

        //        public const int CanManageSystemSettings = 90304;

        //        public const int CanResetPassword = 90305;

        //        public const int CanViewSystemSettings = 90306;

        //        public const int CanViewUserGroupUsers = 90307;

        //        public const int CanSaveUserGroup = 90308;

        //        //public const int CanManagePolicyRule = 90309;

        //        [Description("90309")]
        //        public sealed class PolicyRuleManager
        //        {
        //            public const int CanSavePolicy = 9030901;
        //            public const int CanSavePolicyRule = 9030902;
        //            public const int CanDeletePolicyRule = 9030903;
        //            public const int CanManagePolicyRule = 9030904;
        //        }

        //        [Description("90310")]
        //        public sealed class UserPolicyMap
        //        {
        //            public const int CanViewUserPolicyMap = 9031001;
        //            public const int CanSaveUserPolicyMap = 9031002;
        //            public const int CanManageUserPolicyMap = 9031003;
        //        }
        //    }

        //    [Description("904")]
        //    public sealed class WebAdminSite
        //    {
        //        public const int CanViewWebAdminSite = 90401;
        //        public const int CanViewWebReport = 1101;
        //    }            
        //}

        //[Description("10")]
        //public sealed class UserScanDocument
        //{            
        //    [Description("1001")]
        //    public sealed class UserDocumentCategory
        //    {
        //        public const int CanViewUserDocumentCategory = 100101;
        //        public const int CanSaveUserDocumentCategory = 100102;
        //        public const int CanDeleteUserDocumentCategory = 100103;
        //        public const int CanViewEmployeeDocumentCategory = 100104;
        //        public const int CanSaveEmployeeDocumentCategory = 100105;
        //        public const int CanDeleteEmployeeDocumentCategory = 100106;
        //        public const int CanViewClass4DocumentCategory = 100107;

        //    }

        //    [Description("1002")]
        //    public sealed class UserDocumentLocation
        //    {
        //        public const int CanViewUserDocumentLocation = 100201;
        //        public const int CanSaveUserDocumentLocation = 100202;
        //    }

        //    [Description("1003")]
        //    public sealed class UserDocument
        //    {
        //        public const int CanViewUserDocument = 100301;
        //        public const int CanUploadUserDocument = 100302;
        //        public const int CanDeleteUserDocument = 100303;
        //        public const int CanUpdateUserDocument = 100304;
        //        public const int CanPrintAndSaveUserDocumentToDisk = 100305;
        //        public const int CanDeleteUserDocumentTempFolder = 100306;

        //        public const int CanViewEmployeeDocument = 100307;
        //        public const int CanUploadEmployeeDocument = 100308;
        //        public const int CanDeleteEmployeeDocument = 100309;
        //        public const int CanUpdateEmployeeDocument = 100310;
        //        public const int CanPrintAndSaveEmployeeDocumentToDisk = 100311;
        //        public const int CanDeleteEmployeeDocumentTempFolder = 100312;

        //        public const int CanChangeUserDocumentNarration = 100313;
        //        public const int CanChangeEmployeeDocumentNarration = 100314;
        //    }
        //}
        
        //[Description("11")]
        //public sealed class ReportPermission
        //{
            
        //    [Description("1101")]
        //    public sealed class AccountReports
        //    {
        //        public const int ChartOfAccountsReport = 110101;
        //        public const int BalanceSheetReport = 110102;
        //        public const int IncomeStatementReport = 110103;
        //        public const int TrialBalanceSheetReport = 110104;
        //        public const int GeneralJournalReport = 110105;
        //    }

        //    [Description("1102")]
        //    public sealed class AcademicReports
        //    {
        //        public const int InternshipBySemesterReport = 110201;
        //        public const int StudentBySectionReport = 110202;
        //        public const int RegistrationStatusReport = 110203;
        //        public const int FreshmanRegistrationCountReport = 110204;
        //        public const int CurriculumWiseRegistrationCountReport = 110205;
        //        public const int FreshmanStudentListReport = 110206;
        //        public const int SemesterAssessmentSummaryReport = 110207;
        //        public const int ConvocationAttendanceReport = 110208;
        //        public const int ScholarshipSummaryReport = 110209;
        //        public const int VatSummaryReport = 110210;
        //    }

        //    [Description("1103")]
        //    public sealed class LibraryReports
        //    {
        //        public const int BookTransactionsReport = 110301;
        //        public const int BookListReport = 110302;
        //        public const int UnReturnedBooksReport = 110303;
        //        public const int BookSearchViewReport = 110304;

        //    }

        //    [Description("1104")]
        //    public sealed class HrReports
        //    {
        //        public const int UserListReport = 110401;
        //        public const int EmployeeListReport = 110402;
        //        public const int EmployeeJoiningDateReport = 110403;
        //        public const int EmployeeSalaryIncrementReportMonthly = 110404;
        //        public const int EmployeeEducationReport = 110405;
        //        public const int EmployeeSalaryHistoryReport = 110406;
        //        public const int EmployeePayslipDeductionReport = 110407;
        //        public const int EmployeeSalaryIncrementReportYearly = 110408;
        //        public const int EmployeesLeaveProcessReport = 110409;
        //        public const int Can_View_Employees_Resume_Report = 110410;
        //        public const int Can_View_faculty_List_Report = 110411;
        //        public const int Can_View_Gratuity_Report = 110412;
        //        public const int Can_View_Employee_Salary_Increment_Latter = 110413;
        //        public const int Can_View_Employee_Salary_Report = 110414;
        //    }

        //    [Description("1105")]
        //    public sealed class VehicleReports
        //    {
        //        public const int VehicleTypeWiseDetailReport = 110501;
        //        public const int VehicleTripLogReport = 110502;
        //        public const int VehicleMaintenanceReport = 110503;
        //    }
        //}

        //[Description("12")]
        //public sealed class Credential
        //{
        //    public const int CanView = 1201;
        //    public const int CanResetPassword = 1202;
        //    public const int CanLockUnlock = 1203;
        //    public const int CanApproveDisapprove = 1204;
        //    public const int CanResetPasswordAttemptCount = 1205;
        //    public const int CanResetPasswordAnswerAttemptCount = 1206;

        //    public const int CanViewRegisterableDepartment = 1207;
        //    public const int CanSaveRegistrarDepartment = 1208;

        //    public const int CanViewCoordinatorDepartment = 1209;
        //    public const int CanSaveUserDepartment = 1210;

        //}

        //[Description("13")]
        //public sealed class Miscellaneous
        //{

        //    [Description("1301")]
        //    public sealed class SearchUser
        //    {
        //        public const int CanViewSearchUser = 130101;
        //    }

        //    [Description("1302")]
        //    public sealed class DepartmentBank
        //    {
        //        public const int CanViewDepartmentBank = 130201;
        //        public const int CanChangeDepartmentBank = 130202;
        //    }

        //    [Description("1303")]
        //    public sealed class ManageStudentAccess
        //    {
        //        public const int CanViewManageStudentAccess = 130301;
        //        public const int CanChangeManageStudentAccess = 130302;
        //    }
            
            
        //}

        //[Description("14")]
        //public sealed class VehicleModule
        //{
        //    [Description("1401")]
        //    public sealed class Vehicle
        //    {
        //        public const int Can_View_Vehicle = 140101;
        //        public const int Can_Manage_Vehicle = 140102;
        //        public const int Can_Delete_Vehicle = 140103;
        //        public const int Can_Upload_VehicleImage = 140104;
        //        public const int Can_Clear_TempFolder = 140105;
        //    }

        //    [Description("1402")]
        //    public sealed class VehicleTripLog
        //    {
        //        public const int Can_View_VehicleTripLog = 140201;
        //        public const int Can_Manage_VehicleTripLog = 140202;
        //        public const int Can_Delete_VehicleTripLog = 140203;
        //    }

        //    [Description("1403")]
        //    public sealed class VehicleRepair
        //    {
        //        public const int Can_View_VehicleRepairs = 140301;
        //        public const int Can_Manage_VehicleRepair = 140302;
        //        public const int Can_Delete_VehicleRepair = 140303;
        //    }

        //    [Description("1404")]
        //    public sealed class VehicleMake
        //    {
        //        public const int Can_View_VehicleMakes = 140401;
        //        public const int Can_Manage_VehicleMake = 140402;
        //        public const int Can_Delete_VehicleMake = 140403;
        //    }

        //    [Description("1405")]
        //    public sealed class VehicleFitness
        //    {
        //        public const int Can_View_VehicleFitness = 140501;
        //        public const int Can_Manage_VehicleFitness = 140502;
        //        public const int Can_Delete_VehicleFitness = 140503;
        //    }

        //    [Description("1406")]
        //    public sealed class VehicleTaxToken
        //    {
        //        public const int Can_View_VehicleTaxToken = 140601;
        //        public const int Can_Manage_VehicleTaxToken = 140602;
        //        public const int Can_Delete_VehicleTaxToken = 140603;
        //    }

        //    [Description("1407")]
        //    public sealed class VehicleInsurance
        //    {
        //        public const int Can_View_VehicleInsurance = 140701;
        //        public const int Can_Manage_VehicleInsurance = 140702;
        //        public const int Can_Delete_VehicleInsurance = 140703;
        //    }

        //    [Description("1408")]
        //    public sealed class VehicleRoutePermit
        //    {
        //        public const int Can_View_VehicleRoutePermit = 140801;
        //        public const int Can_Manage_VehicleRoutePermit = 140802;
        //        public const int Can_Delete_VehicleRoutePermit = 140803;
        //    }

        //    [Description("1409")]
        //    public sealed class VehicleFuel
        //    {
        //        public const int Can_View_VehicleFuel = 140901;
        //        public const int Can_Manage_VehicleFuel = 140902;
        //        public const int Can_Delete_VehicleFuel = 140903;
        //    }

        //    [Description("1410")]
        //    public sealed class VehicleServicing
        //    {
        //        public const int Can_View_VehicleServicing = 141001;
        //        public const int Can_Manage_VehicleServicing = 141002;
        //        public const int Can_Delete_VehicleServicing = 141003;
        //    }

        //    [Description("1411")]
        //    public sealed class VehicleMaintenance
        //    {
        //        public const int Can_View_VehicleMaintenance = 141101;
        //        public const int Can_Manage_VehicleMaintenance = 141102;
        //        public const int Can_Delete_VehicleMaintenance = 141103;
        //    }

        //    [Description("1412")]
        //    public sealed class VehicleAccessories
        //    {
        //        public const int Can_View_VehicleAccessories = 141201;
        //        public const int Can_Manage_VehicleAccessories = 141202;
        //        public const int Can_Delete_VehicleAccessories = 141203;
        //    }

        //    [Description("1413")]
        //    public sealed class VehicleAccessoriesAllocation
        //    {
        //        public const int Can_View_VehicleAccessoriesAllocation = 141301;
        //        public const int Can_Manage_VehicleAccessoriesAllocation = 141302;
        //        public const int Can_Delete_VehicleAccessoriesAllocation = 141303;
        //    }

        //    [Description("1414")]
        //    public sealed class VehicleMaintenanceType
        //    {
        //        public const int Can_View_VehicleMaintenanceType = 141401;
        //        public const int Can_Manage_VehicleMaintenanceType = 141402;
        //        public const int Can_Delete_VehicleMaintenanceType = 141403;
        //    }
            
        //    [Description("1415")]
        //    public sealed class VehicleScanDocument
        //    {
        //        [Description("14151")]
        //        public sealed class VehicleDocumentCategory
        //        {
        //            public const int Can_View_VehicleDocumentCategory = 1415101;
        //            public const int Can_Save_VehicleDocumentCategory = 1415102;
        //            public const int Can_Delete_VehicleDocumentCategory = 1415103;
        //        }

        //        [Description("14152")]
        //        public sealed class VehicleDocument
        //        {
        //            public const int Can_View_VehicleDocument = 1415201;
        //            public const int Can_Upload_VehicleDocument = 1415202;
        //            public const int Can_Delete_VehicleDocument = 1415203;
        //            public const int Can_Update_VehicleDocument = 1415204;
        //            public const int Can_PrintAndSave_VehicleDocumentToDisk = 1415205;
        //            public const int Can_Delete_VehicleDocumentTempFolder = 1415206;
        //        }
        //    }
        //}

    }
}