using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.IO;
using System.Web;
using EMS.CoreLibrary.Helpers;
using EMS.Framework.Objects;
using EMS.Framework.Utils;
using Newtonsoft.Json;

namespace EMS.Framework.Settings
{
    public class SiteSettings : BaseSettings
    {
        public static SiteSettings Instance
        {
            get
            {
                var instance = GetSiteSettingsFromCache();
                if (instance == null)
                {
                    return new SiteSettings();
                }

                return instance;
            }
        }
        #region Enum Property
        public enum StudentIdGenerateTypeEnum
        {
            FullAutoGenerate=0,
            PrefixAutoGenerate = 1,
            FullManual=2,
            PrefixAutoGenerateAsUGC = 3,
            FullAutoGenerateAsUGC = 4,
        }
        public enum CreditTransBillGenTypeEnum
        {
            Normal = 1,
            Eub = 2,
            //FullManual = 2
        }
        #endregion

        /// <summary>
        /// 15 MB Allowed
        /// 1 MB = 1048576  B
        /// 15 MB = 15728640 B
        /// 1024 MB= 1073741824 B
        /// </summary>

        public static string SiteSettingsBasePath = "~/Content/SiteSettings/" + InstituteKey;
        public static string SiteSettingsFullPath = SiteSettingsBasePath+"/SiteSettings.json";
        public static string IISCacheKeyName = InstituteKey + "siteSettingsInstance";

        public int MaxFileUploadSizeInByte = 15728640;//15 MB
        public string[] AllowedFileExtensions
        {
            get
            {
                return new[] { ".zip", ".rar", ".7zip", ".ppt", ".pptx", ".doc", ".docx", ".xls", ".xlsx", ".pdf", ".jpg", ".png", ".txt", ".csv" };
            }
            //set
            //{
            //    SetSystemSettings(SystemSettings.GradeDocumentPath, value);
            //}
        }

        public string AbsentAttenceSmsTemplate =
            "Dear Parent/Guardian Assalamualaikum. Your son/daughter({0}) was absent in the '{1}' class on {2}. Class Teacher, FU Off: 028000315.";

        public static string Copyright = DateTime.Now.Year + " © Pipilika Soft";//All rights reserved.
        public static string PoweredBy = "Pipilika Soft";//Powered by 
        public static string PoweredByFullText = "Powered By: Pipilika Soft";//Powered by
        public static string CompanyWebsite = "https://pipilikasoft.com/";
        public string PipilikaEmail = "info@pipilikasoft.com"; //public string PoweredByFullText = "Powered By: Pipilika Soft";
        public string PipilikaLogo = "/assets/img/logo/PipilikaSoftLogo.png";
        public string PipilikaCellNo = "01633300999, 01633300888";

        public string ProductBrandNameSolo = "i-EMS";
        //public string ProductNickNameFull = "Scholar Education Automation and Management System";
        public string ProductFullName = "Integrated Education Management System";

        public int SemesterPaymentParticularId
        {
            get
            {
                return 8;
            }
        }

        public double MinimumOnlinePaymentAmount
        {
            get
            {
                try
                {
                    return 10;
                    //var minimumAmount = ConfigurationManager.AppSettings["MinimumOnlinePaymentAmount"];
                    //return Convert.ToDouble(minimumAmount);
                }
                catch (Exception e)
                {
                    return 10;
                }
            }
        }
        public enum InstituteKeyEnum
        {
            /// <summary>
            /// Use for demo
            /// </summary>
            demo = 0,
            /// <summary>
            /// 
            /// </summary>
            eub = 1,
            ius = 2,
            buhs = 3,
            baust = 4,
            fu = 5,
            smuct = 6,
            mist=7,
            giet = 8
        }


        //public static string InstituteKey = ConfigurationManager.AppSettings["InstituteKey"];
        public static string InstituteKey
        {
            get
            {
                return ConfigurationManager.AppSettings["InstituteKey"].ToLower();
            }
        }

        public bool IsPolytechnicInstitute = false;
        public bool IsOperationTerminated = false;
        public string OperationTerminationMessage = "";

        public static InstituteKeyEnum GetInstituteKeyEnum
        {
            get
            {
                var instituteKey = ConfigurationManager.AppSettings["InstituteKey"];
                if (instituteKey.IsValid())
                {
                    //(StatusEnum)Enum.Parse(typeof(StatusEnum), "Active", true);
                    InstituteKeyEnum result;
                    if (!InstituteKeyEnum.TryParse(instituteKey.ToLower(), out result))
                    {
                        throw new Exception("Valid Institute Key Not Found in Site Settings.");
                    }

                    return result;

                }

                throw new Exception("Valid Institute Key Not Found in Site Settings.");
            }
        }



        /// <summary>
        /// Key=pipilika
        /// </summary>

        /// <summary>
        /// EUB Settings 
        /// </summary>

        #region EUB Settings 

        public string EmsLink = "http://iems.eub.edu.bd/";
        public string InstituteWebsite = "https://www.eub.edu.bd/";
        public string InstituteItSupportLink = "https://eub.edu.bd/ict-division/"; //
        public string InstituteAddress = "2/4 Gabtoli, Mirpur, Dhaka 1216.";//"Savar, Dhaka-1342, Bangladesh.";
        public string InstituteEmail = "info@eub.edu.bd";
        public string InstituteName = "European University of Bangladesh";//"Jahangirnagar University";
        public string InstituteShortName = "EUB";//"JU";
        public string InstituteDomain = "eub.edu.bd";//"juniv.edu"
        public string InstituteCreditPrefix = "Coordinated By:";
        public string InstituteCredit = "ICT Division, EUB";

        public string InstituteRegistrationOfficeCellNo = "01968774927, 01968774933";
        public string InstituteExamOfficeCellNo = "01968774927, 01968774933";
        public string InstituteAccountOfficeCellNo = "01968774927, 01968774933";
        public string InstituteAdmissionOfficeCellNo = "01968774927, 01968774933";
        public string InstituteAdmissionOfficeLandPhoneNo = "9133293";
        public string InstituteIctOfficeCellNo = "01968774927, 01968774933";

        public bool IsAutoGenStudentIdPrefix = true;
        public StudentIdGenerateTypeEnum StudentIdGenerateType = StudentIdGenerateTypeEnum.PrefixAutoGenerate;
        public int StudentIdSerialDigitCount = 3;
        public int TotalStudentIdLength = 9;
        public int DefaultClassSectionRange = 80;

        public float AutoGradeGraceMark = (float)1.00;


        public string AbsentGradeIdStr = "201608121918146308";
        public static long AbsentGradeId
        {
            get
            {
                try
                {
                    return Convert.ToInt64(Instance.AbsentGradeIdStr);
                }
                catch (Exception e)
                {
                    return 201608121918146308;
                }
            }
        }

        public string AbsentMarkSymbol = "-";

        public string MaximumImproveExamAllowGradeIdStr = "201608121858240468";// B
        public static long MaximumImproveExamAllowGradeId
        {
            get
            {
                try
                {
                    return Convert.ToInt64(Instance.MaximumImproveExamAllowGradeIdStr);
                }
                catch (Exception e)
                {
                    return 201608121858240468;
                }
            }
        }// B

        public bool IsOnlineResultOpen = false;
        public bool IsBaustOnlineResultOpen = false;

        public bool IsForceVarifyStudentEmail = false;
        public bool IsForceVarifyStudentMobile = true;
        public bool IsForceChangeStudentPassword = false;
        public string HowToLoginTutorialUrl = "https://youtu.be/oEeZESqIM-A";
        public string HowToPayOnlineThroughbKashUrl = "https://www.youtube.com/watch?v=c7QZ4yoSo0M&list=PLNj3FStjMswTAjvPUzTvgziHzgk0Fti68&index=6";
        public string PasswordChangeTutorialUrl = "https://youtu.be/oEeZESqIM-A?t=16";
        public string EmailVarifyTutorialUrl = "https://youtu.be/oEeZESqIM-A?t=299";

        public bool IsMidtermExamEnable = true;

        public bool IsShowAmountInStudentDue = false;

        public int SuppleExamFeeParticularId = 9;
        public string SuppleExamViewName = "Supplementary";

        public int ReAdmissionFeeParticularId = 32;

        public bool IsAutoGenerateSemesterFees = true;
        public int? StartSemesterIdForAutoGenerateSemesterFeesFor4M = null; // null for all semester
        public int? StartSemesterIdForAutoGenerateSemesterFeesFor6M = null; // null for all semester
        public bool IsAutoGenerateOneTimeFees = true;
        public int? StartSemesterIdForAutoGenerateOneTimeFees = null; // null for all semester

        public float MidtermPayableInPercent = 50;

        public CreditTransBillGenTypeEnum CreditTransBillGenType = CreditTransBillGenTypeEnum.Normal;

        public string OnlineStdRegConfirmMessage = "কনফার্মের পরে আপনি কোনও পরিবর্তন করতে পারবেন না।";
        public string InstructionsForStdPaymentInfo = "Dear students: Please pay your payment to bKash.";
        public bool IsOpenSingleScreenMarkEntry = true;


        public StdPaymentCollectionSettings StudentPaymentCollectionPanel = new StdPaymentCollectionSettings();

        public string ParticularIdListForLastSemesterIgnoreBill = "";

        public string SemesterBillNamePrefixName = "Semester Fees";
        public bool IsAppendNumberOfSemesterCountWithSemesterBillName = true;

        public float MinimumCgpaToContinueScholarship = 2.5f;

        public bool IsShowEmployeeContacts = true;
        public bool IsMultiLoginAllow = true;

        #endregion


        /// <summary>
        /// IUS Settings
        /// </summary>
        #region IUS Settings
        //public string EmsLink = "http://ius.pipilikasoft.com/";
        //public string InstituteWebsite = "https://www.ius.edu.bd/"; //"http://www.juniv.edu/"
        //public string InstituteAddress = "Badda, Gulshan-1, Dhaka 1213.";//"Savar, Dhaka-1342, Bangladesh.";
        //public string InstituteEmail = "info@ius.edu.bd";
        //public string InstituteName = "The International University of Scholars";//"Jahangirnagar University";
        //public string InstituteShortName = "IUS";//"JU";
        //public string InstituteDomain = "ius.edu.bd";//"juniv.edu (+880) 29888820-21" 
        //public string InstituteCreditPrefix = "Coordinated By:";
        //public string InstituteCredit = "IUS";


        //public string InstituteRegistrationOfficeCellNo = "(+880) 255054802-03";
        //public string InstituteExamOfficeCellNo = "(+880) 255054802-03";
        //public string InstituteAccountOfficeCellNo = "(+880) 255054802-03";
        //public string InstituteAdmissionOfficeCellNo = "(+880) 255054802-03";
        //public string InstituteAdmissionOfficeLandPhoneNo = "(+880) 255054802-03";
        //public string InstituteIctOfficeCellNo = "(+880) 255054802-03";

        //public bool IsAutoGenStudentIdPrefix = true;
        //public StudentIdGenerateTypeEnum StudentIdGenerateType = StudentIdGenerateTypeEnum.PrefixAutoGenerate;
        //public int StudentIdSerialDigitCount = 4;
        //public int TotalStudentIdLength = 9;
        //public bool AutoAllocateClassSectionOnAdmission = false;
        //public int DefaultClassSectionRange = 80;

        //public float AutoGradeGraceMark = (float)0.00;
        //public string AbsentGradeIdStr = "201608121918146308";
        //public static long AbsentGradeId
        //{
        //    get
        //    {
        //        try
        //        {
        //            return Convert.ToInt64(Instance.AbsentGradeIdStr);
        //        }
        //        catch (Exception e)
        //        {
        //            return 201608121918146308;
        //        }
        //    }
        //}
        //public string AbsentMarkSymbol = "-";
        //public string MaximumImproveExamAllowGradeIdStr = "201608121858240468";// B
        //public static long MaximumImproveExamAllowGradeId
        //{
        //    get
        //    {
        //        try
        //        {
        //            return Convert.ToInt64(Instance.MaximumImproveExamAllowGradeIdStr);
        //        }
        //        catch (Exception e)
        //        {
        //            return 201608121858240468;
        //        }
        //    }
        //}// B


        //public bool IsOnlineResultOpen = false;
        //public bool IsBaustOnlineResultOpen = false;

        //public bool IsForceVarifyStudentEmail = true;
        //public bool IsForceVarifyStudentMobile = true;
        //public bool IsForceChangeStudentPassword = true;
        //public string HowToLoginTutorialUrl = "https://youtu.be/oEeZESqIM-A";
        //public string PasswordChangeTutorialUrl = "https://youtu.be/oEeZESqIM-A?t=16";
        //public string EmailVarifyTutorialUrl = "https://youtu.be/oEeZESqIM-A?t=299";

        //public bool IsMidtermExamEnable = true;

        //public int SuppleExamFeeParticularId = 9;
        //public string SuppleExamViewName = "Supplementary";

        //public bool IsAutoGenerateSemesterFees = true;
        //public int? StartSemesterIdForAutoGenerateSemesterFees = null; // null for all semester
        //public bool IsAutoGenerateOneTimeFees = true;
        //public int? StartSemesterIdForAutoGenerateOneTimeFees = null; // null for all semester

        //public float MidtermPayableInPercent = 50;
        //public CreditTransBillGenTypeEnum CreditTransBillGenType = CreditTransBillGenTypeEnum.Eub;


        //public string OnlineStdRegConfirmMessage = "কনফার্মের পরে আপনি কোনও পরিবর্তন করতে পারবেন না।";
        //public string InstructionsForOnlineStdReg = "Registration Regulations/Guidelines:<br>";

        //public string InstructionsForStdPaymentInfo = "Dear students: For any query about the dues, please contact account department.";
        //public bool IsOpenSingleScreenMarkEntry = true;
        //public StdPaymentCollectionSettings StudentPaymentCollectionPanel => new StdPaymentCollectionSettings
        //{
        //    IsShowAdmissionFeeDue = true,
        //};
        //public string ParticularIdListForLastSemesterIgnoreBill = "";
        //public string SemesterBillNamePrefixName = "Registration Fees";
        //public bool IsAppendNumberOfSemesterCountWithSemesterBillName = false;
        #endregion



        /// <summary>
        /// BAUST
        /// </summary>

        #region BAUST Settings

        //public string EmsLink = "http://iems.baust.edu.bd/";
        //public string InstituteWebsite = "https://www.baust.edu.bd/";
        //public string InstituteItSupportLink = "";
        //public string InstituteAddress = "Saidpur Cantonment, Saidpur.";
        //public string InstituteEmail = "info@baust.edu.bd";
        //public string InstituteName = "Bangladesh Army University of Science and Technology";
        //public string InstituteShortName = "BAUST";
        //public string InstituteDomain = "baust.edu.bd";
        //public string InstituteCreditPrefix = "Coordinated By:";
        //public string InstituteCredit = "BAUST";

        //public string InstituteRegistrationOfficeCellNo = "05526-73403, 01769675554, 01769675553, 01769675552, 01769675551";
        //public string InstituteExamOfficeCellNo = "05526-73403, 01769675554, 01769675553, 01769675552, 01769675551";
        //public string InstituteAccountOfficeCellNo = "05526-73403, 01769675554, 01769675553, 01769675552, 01769675551";
        //public string InstituteAdmissionOfficeCellNo = "05526-73403, 01769675554, 01769675553, 01769675552, 01769675551";
        //public string InstituteAdmissionOfficeLandPhoneNo = "05526-73403, 01769675554, 01769675553, 01769675552, 01769675551";
        //public string InstituteIctOfficeCellNo = "05526-73403, 01769675554, 01769675553, 01769675552, 01769675551";

        //public bool IsAutoGenStudentIdPrefix = true;
        //public StudentIdGenerateTypeEnum StudentIdGenerateType = StudentIdGenerateTypeEnum.PrefixAutoGenerate;
        //public int StudentIdSerialDigitCount = 3;
        //public int TotalStudentIdLength = 9;
        //public bool AutoAllocateClassSectionOnAdmission = false;
        //public int DefaultClassSectionRange = 45;

        //public float AutoGradeGraceMark = (float)0.00;
        //public string AbsentMarkSymbol = "A";

        //public string AbsentGradeIdStr = "201608121908256795";
        //public static long AbsentGradeId
        //{
        //    get
        //    {
        //        try
        //        {
        //            return Convert.ToInt64(Instance.AbsentGradeIdStr);
        //        }
        //        catch (Exception e)
        //        {
        //            return 201608121908256795;
        //        }
        //    }
        //}
        //public string MaximumImproveExamAllowGradeIdStr = "201608121858240468";// B
        //public static long MaximumImproveExamAllowGradeId
        //{
        //    get
        //    {
        //        try
        //        {
        //            return Convert.ToInt64(Instance.MaximumImproveExamAllowGradeIdStr);
        //        }
        //        catch (Exception e)
        //        {
        //            return 201608121858240468;
        //        }
        //    }
        //}// B


        //public bool IsOnlineResultOpen = false;
        //public bool IsBaustOnlineResultOpen = false;

        //public bool IsMidtermExamEnable = false;


        //public bool IsForceVarifyStudentEmail = true;
        //public bool IsForceVarifyStudentMobile = true;
        //public bool IsForceChangeStudentPassword = true;
        //public string HowToLoginTutorialUrl = "https://youtu.be/oEeZESqIM-A";
        //public string PasswordChangeTutorialUrl = "https://youtu.be/oEeZESqIM-A?t=16";
        //public string EmailVarifyTutorialUrl = "https://youtu.be/oEeZESqIM-A?t=299";

        //public int SuppleExamFeeParticularId = 9;
        //public string SuppleExamViewName = "Referred/Backlog";

        //public bool IsAutoGenerateSemesterFees = false;
        //public int? StartSemesterIdForAutoGenerateSemesterFees = null; // null for all semester
        //public bool IsAutoGenerateOneTimeFees = false;
        //public int? StartSemesterIdForAutoGenerateOneTimeFees = null; // null for all semester


        //public float MidtermPayableInPercent = 50;
        //public CreditTransBillGenTypeEnum CreditTransBillGenType = CreditTransBillGenTypeEnum.Normal;
        //public string OnlineStdRegConfirmMessage = "কনফার্মের পরে আপনি কোনও পরিবর্তন করতে পারবেন না।";
        //public string InstructionsForOnlineStdReg = "Online Registration নির্দেশিকাঃ<br>১. Online Registration 31-01-2021 এর মধ্যে অবশ্যই সম্পন্ন করতে হবে।<br>২. Online Registration করার পূর্বে আপনার সেমিস্টার কোর্স অফার সঠিকভাবে যাচাই করুন (<a href=\"https://eub.edu.bd/course-offer-fall-2020/\" target='_blank'><i class='menu-icon fa fa-user'></i> এই লিংক  থেকে </a>)।<br>৩. আবেদনের সময় ইয়ার এবং সেমিস্টার সঠিকভাবে সিলেক্ট করে কোর্স গুলো পুনরায় অফারিং কোর্স এর সাথে যাচাই করে আপনার Registration নিশ্চিত করুন।<br>৪. Registration কনফার্ম করার পর কোন পরিবর্তন/বাতিল করার সুযোগ থাকবে না।<br>৫. প্রয়োজনেঃ<br>	i) সিভিল, বিজনেস, ল’, ইংলিশ, আই পি ই, টিএইচএম এবং ইকোনোমিকস<br>	 ক) ০১৯৭৮৮০০৪১০ খ) ০১৭৩১২২০২৩২<br>	ii) টেক্সটাইল, ইইই এবং সিএসই<br>	ক) ০১৬২৮৬২৬৬০৮ খ) ০১৯৬৮৭৭৪৯২৫<br>";
        //public bool IsOpenSingleScreenMarkEntry = true;
        //public StdPaymentCollectionSettings StudentPaymentCollectionPanel => new StdPaymentCollectionSettings
        //{
        //    IsShowAdmissionFeeDue = false,
        //};
        //public string InstructionsForStdPaymentInfo = "Dear students: For any query about the dues, please contact 01810-036141, 01911-240889, 01818-974540, 01971-725578 and 01674-526574.  For payment, deduct Tk 1,000/= from the Mid Term dues and add that to the Final Exam (Assessment) dues.";
        //public string ParticularIdListForLastSemesterIgnoreBill = "";
        //public string SemesterBillNamePrefixName = "Registration Fees";
        //public bool IsAppendNumberOfSemesterCountWithSemesterBillName = false;
        #endregion



        /// <summary>
        /// BUHS Settings
        /// </summary>
        #region BUHS Settings
        //public string EmsLink = "http://buhs.pipilikasoft.com/";
        //public string InstituteWebsite = "https://www.buhs.ac.bd/";
        //public string InstituteItSupportLink = "http://buhs.ac.bd/contact/"; //
        //public string InstituteAddress = "125/1, Darus Salam, Mirpur Dhaka-1216.";//"Savar, Dhaka-1342, Bangladesh.";
        //public string InstituteEmail = "info@buhs.ac.bd";
        //public string InstituteName = "Bangladesh University of Health Sciences";//"Jahangirnagar University";
        //public string InstituteShortName = "BUHS";//"JU";
        //public string InstituteDomain = "buhs.ac.bd";//"juniv.edu"
        //public string InstituteCreditPrefix = "";//"Coordinated By:";
        //public string InstituteCredit = "";// "ICT Division, BUHS";


        //public string InstituteRegistrationOfficeCellNo = "01791042087";
        //public string InstituteExamOfficeCellNo = "01791042087";
        //public string InstituteAccountOfficeCellNo = "01791042087";
        //public string InstituteAdmissionOfficeCellNo = "(880)2- 9010932, 9010952";
        //public string InstituteAdmissionOfficeLandPhoneNo = "(880)2- 9010932, 9010952";
        //public string InstituteIctOfficeCellNo = "01791042087";


        //public bool IsAutoGenStudentIdPrefix = true;
        //public StudentIdGenerateTypeEnum StudentIdGenerateType = StudentIdGenerateTypeEnum.FullAutoGenerate;
        //public int StudentIdSerialDigitCount = 03;
        //public int TotalStudentIdLength = 8;
        //public bool AutoAllocateClassSectionOnAdmission = false;
        //public int DefaultClassSectionRange = 80;



        //public float AutoGradeGraceMark = (float)0.00;
        //public string AbsentGradeIdStr = "201608121918146308";
        //public static long AbsentGradeId
        //{
        //    get
        //    {
        //        try
        //        {
        //            return Convert.ToInt64(Instance.AbsentGradeIdStr);
        //        }
        //        catch (Exception e)
        //        {
        //            return 201608121918146308;
        //        }
        //    }
        //}
        //public string AbsentMarkSymbol = "-";
        //public string MaximumImproveExamAllowGradeIdStr = "201608121858240468";// B
        //public static long MaximumImproveExamAllowGradeId
        //{
        //    get
        //    {
        //        try
        //        {
        //            return Convert.ToInt64(Instance.MaximumImproveExamAllowGradeIdStr);
        //        }
        //        catch (Exception e)
        //        {
        //            return 201608121858240468;
        //        }
        //    }
        //}// B
        //public long MaximummGradeForRetakeCourseGradeId = 201608121908256795;//



        //public bool IsOnlineResultOpen = false;
        //public bool IsBaustOnlineResultOpen = false;

        //public bool IsForceVarifyStudentEmail = true;
        //public bool IsForceVarifyStudentMobile = true;
        //public bool IsForceChangeStudentPassword = true;
        //public string HowToLoginTutorialUrl = "https://youtu.be/oEeZESqIM-A";
        //public string PasswordChangeTutorialUrl = "https://youtu.be/oEeZESqIM-A?t=16";
        //public string EmailVarifyTutorialUrl = "https://youtu.be/oEeZESqIM-A?t=299";

        //public bool IsMidtermExamEnable = false;


        //public int SuppleExamFeeParticularId = 9;
        //public string SuppleExamViewName = "Supplementary";

        //public bool IsAutoGenerateSemesterFees = true;
        //public int? StartSemesterIdForAutoGenerateSemesterFees = 202102; // null for all semester
        //public bool IsAutoGenerateOneTimeFees = true;
        //public int? StartSemesterIdForAutoGenerateOneTimeFees = 202101; // null for all semester

        //public float MidtermPayableInPercent = 50;
        //public CreditTransBillGenTypeEnum CreditTransBillGenType = CreditTransBillGenTypeEnum.Normal;
        //public string OnlineStdRegConfirmMessage = "কনফার্মের পরে আপনি কোনও পরিবর্তন করতে পারবেন না।";
        //public string InstructionsForOnlineStdReg = "";
        //public bool IsOpenSingleScreenMarkEntry = true;
        //public StdPaymentCollectionSettings StudentPaymentCollectionPanel => new StdPaymentCollectionSettings
        //{
        //    IsShowAdmissionFeeDue = false,
        //};
        //public string ParticularIdListForLastSemesterIgnoreBill = "";
        //public string SemesterBillNamePrefixName = "Registration Fees";
        //public bool IsAppendNumberOfSemesterCountWithSemesterBillName = false;
        #endregion


        /// <summary>
        /// Feni Uni Settings
        /// </summary>
        #region Feni Uni Settings

        //public string EmsLink = "http://fuiems.pipilikasoft.com/";
        //public string InstituteWebsite = "https://feniuniversity.edu.bd/";
        //public string InstituteItSupportLink = "http://feniuniversity.edu.bd/pages/contact/"; //
        //public string InstituteAddress = "Trunk Road, Feni-3900, Bangladesh.";//"Savar, Dhaka-1342, Bangladesh.";
        //public string InstituteEmail = "info@feniuniversity.edu.bd";
        //public string InstituteName = "Feni University";//"Jahangirnagar University";
        //public string InstituteShortName = "FU";//"JU";
        //public string InstituteDomain = "feniuniversity.edu.bd";//"juniv.edu"
        //public string InstituteCreditPrefix = "";//"Coordinated By:";
        //public string InstituteCredit = "";// "ICT Division, BUHS";


        //public string InstituteRegistrationOfficeCellNo = "+88-0331-69131";
        //public string InstituteExamOfficeCellNo = "+88-0331-69131";
        //public string InstituteAccountOfficeCellNo = "+88-0331-69131";
        //public string InstituteAdmissionOfficeCellNo = "+88-0331-69131";
        //public string InstituteAdmissionOfficeLandPhoneNo = "+88-0331-69131";
        //public string InstituteIctOfficeCellNo = "+88-0331-69131";

        //public bool IsAutoGenStudentIdPrefix = true;
        //public StudentIdGenerateTypeEnum StudentIdGenerateType = StudentIdGenerateTypeEnum.PrefixAutoGenerate;
        //public int StudentIdSerialDigitCount = 3;
        //public int TotalStudentIdLength = 9;
        //public bool AutoAllocateClassSectionOnAdmission = false;
        //public int DefaultClassSectionRange = 80;

        //public float AutoGradeGraceMark = (float)0.00;
        //public string AbsentGradeIdStr = "201608121918146308";
        //public static long AbsentGradeId
        //{
        //    get
        //    {
        //        try
        //        {
        //            return Convert.ToInt64(Instance.AbsentGradeIdStr);
        //        }
        //        catch (Exception e)
        //        {
        //            return 201608121918146308;
        //        }
        //    }
        //}
        //public string AbsentMarkSymbol = "-";
        //public string MaximumImproveExamAllowGradeIdStr = "201608121858240468";// B
        //public static long MaximumImproveExamAllowGradeId
        //{
        //    get
        //    {
        //        try
        //        {
        //            return Convert.ToInt64(Instance.MaximumImproveExamAllowGradeIdStr);
        //        }
        //        catch (Exception e)
        //        {
        //            return 201608121858240468;
        //        }
        //    }
        //}// B
        //public long MaximummGradeForRetakeCourseGradeId = 201608121908256795;//



        //public bool IsOnlineResultOpen = false;
        //public bool IsBaustOnlineResultOpen = false;

        //public bool IsForceVarifyStudentEmail = true;
        //public bool IsForceVarifyStudentMobile = true;
        //public bool IsForceChangeStudentPassword = true;
        //public string HowToLoginTutorialUrl = "https://youtu.be/oEeZESqIM-A";
        //public string PasswordChangeTutorialUrl = "https://youtu.be/oEeZESqIM-A?t=16";
        //public string EmailVarifyTutorialUrl = "https://youtu.be/oEeZESqIM-A?t=299";

        //public bool IsMidtermExamEnable = false;


        //public int SuppleExamFeeParticularId = 9;
        //public string SuppleExamViewName = "Supplementary";

        //public bool IsAutoGenerateSemesterFees = true;
        //public int? StartSemesterIdForAutoGenerateSemesterFees = 202003; // null for all semester
        //public bool IsAutoGenerateOneTimeFees = true;
        //public int? StartSemesterIdForAutoGenerateOneTimeFees = 202003; // null for all semester

        //public float MidtermPayableInPercent = 60;
        //public CreditTransBillGenTypeEnum CreditTransBillGenType = CreditTransBillGenTypeEnum.Normal;
        //public StdPaymentCollectionSettings StudentPaymentCollectionPanel => new StdPaymentCollectionSettings
        //{
        //    IsShowAdmissionFeeDue = false,
        //};
        //public string OnlineStdRegConfirmMessage = "কনফার্মের পরে আপনি কোনও পরিবর্তন করতে পারবেন না।";
        //public string InstructionsForOnlineStdReg = "Online Semester Registration(Summer-2021) নির্দেশিকাঃ<br>" +
        //                                                   "১. 30.04.2021 এর মধ্যে সম্পন্ন করতে হবে।<br>" +
        //                                                   "২. Online Registration করার পূর্বে আপনার সেমিস্টার কোর্স অফার সঠিকভাবে যাচাই করুন (<a href=\"https://eub.edu.bd/course-offer-Summer-2021/\" target='_blank'><i class='menu-icon fa fa-user'></i> এই লিংক থেকে </a>)।<br>" +
        //                                                   "৩. আবেদনের সময় ইয়ার এবং সেমিস্টার সঠিকভাবে সিলেক্ট করে কোর্স গুলো পুনরায় অফারিং কোর্স এর সাথে যাচাই করে আপনার Registration নিশ্চিত করুন।<br>" +
        //                                                   "৪. Semester Drop, Readmission এবং Credit Transfer কৃত ছাত্র/ছাত্রীদের কে স্ব স্ব ডিপার্র্টমেন্ট এবং রেজিস্ট্রার অফিসে যোগাযোগ করে রেজিস্ট্রেশন সম্পন্ন করতে হবে।<br>" +
        //                                                   "৫. Registration কনফার্ম করার পর কোন পরিবর্তন/বাতিল করা আপনার পক্ষে সুযোগ থাকবে না।<br>" +
        //                                                   "৬. প্রয়োজনেঃ<br>" +
        //                                                   "	i) সিভিল, বিজনেস, ল, ইংলিশ, আই পি ই, টিএইচএম এবং ইকোনোমিকস বিভাগের এর জন্যঃ <br>" +
        //                                                   "	 ক) 01914464989 খ) 01600108384<br>	" +
        //                                                   "ii) ইইই, টেক্সটাইল এবং সিএসই বিভাগের এর জন্যঃ <br>" +
        //                                                   "	ক) 01968774925";

        //public string ParticularIdListForLastSemesterIgnoreBill = "";
        //public string SemesterBillNamePrefixName = "Registration Fees";
        //public bool IsAppendNumberOfSemesterCountWithSemesterBillName = false;
        //public bool IsOpenSingleScreenMarkEntry = false;

        #endregion

        /// <summary>
        /// SMUCT Uni Settings
        /// </summary>
        #region SMUCT Uni Settings

        //public string EmsLink = "http://smuct.pipilikasoft.com/";
        //public string InstituteWebsite = "https://www.smuct.edu.bd/";
        //public string InstituteItSupportLink = "https://www.smuct.edu.bd/contact.php"; //
        //public string InstituteAddress = "Plot-06, R#-Ave-06, Sector-17/H1, Block-H1, Uttara, Dhaka-1230.";//"Savar, Dhaka-1342, Bangladesh.";
        //public string InstituteEmail = "info@smuct.edu.bd";
        //public string InstituteName = "Shanto-Mariam University of Creative Technology";//"Jahangirnagar University";
        //public string InstituteShortName = "SMUCT";//"JU";
        //public string InstituteDomain = "smuct.edu.bd";//"juniv.edu"
        //public string InstituteCreditPrefix = "";//"Coordinated By:";
        //public string InstituteCredit = "";// "ICT Division, BUHS";


        //public string InstituteRegistrationOfficeCellNo = "+88-01969904000 ";
        //public string InstituteExamOfficeCellNo = "+88-01969904000 ";
        //public string InstituteAccountOfficeCellNo = "+88-01969904000 ";
        //public string InstituteAdmissionOfficeCellNo = "+88-01969904000 ";
        //public string InstituteAdmissionOfficeLandPhoneNo = "+88-01969904000 ";
        //public string InstituteIctOfficeCellNo = "+88-01969904000 ";

        //public bool IsAutoGenStudentIdPrefix = true;
        //public StudentIdGenerateTypeEnum StudentIdGenerateType = StudentIdGenerateTypeEnum.FullAutoGenerate;
        //public int StudentIdSerialDigitCount = 3;
        //public int TotalStudentIdLength = 9;
        //public bool AutoAllocateClassSectionOnAdmission = false;
        //public int DefaultClassSectionRange = 80;

        //public float AutoGradeGraceMark = (float)1.00;
        //public float AutoGradeGraceMarkForPass = (float)5.00;
        //public string AbsentGradeIdStr = "201608121918146308";
        //public static long AbsentGradeId
        //{
        //    get
        //    {
        //        try
        //        {
        //            return Convert.ToInt64(Instance.AbsentGradeIdStr);
        //        }
        //        catch (Exception e)
        //        {
        //            return 201608121918146308;
        //        }
        //    }
        //}
        //public string AbsentMarkSymbol = "Ab";
        //public string MaximumImproveExamAllowGradeIdStr = "201608121858240468";// B
        //public static long MaximumImproveExamAllowGradeId
        //{
        //    get
        //    {
        //        try
        //        {
        //            return Convert.ToInt64(Instance.MaximumImproveExamAllowGradeIdStr);
        //        }
        //        catch (Exception e)
        //        {
        //            return 201608121858240468;
        //        }
        //    }
        //}// B
        //public long MaximummGradeForRetakeCourseGradeId = 201608121908256795;//



        //public bool IsOnlineResultOpen = false;
        //public bool IsBaustOnlineResultOpen = false;

        //public bool IsForceVarifyStudentEmail = true;
        //public bool IsForceVarifyStudentMobile = true;
        //public bool IsForceChangeStudentPassword = false;
        //public string HowToLoginTutorialUrl = "https://youtu.be/oEeZESqIM-A";
        //public string PasswordChangeTutorialUrl = "https://youtu.be/oEeZESqIM-A?t=16";
        //public string EmailVarifyTutorialUrl = "https://youtu.be/oEeZESqIM-A?t=299";

        //public bool IsMidtermExamEnable = false;


        //public int SuppleExamFeeParticularId = 9;
        //public string SuppleExamViewName = "Supplementary";

        //public bool IsAutoGenerateSemesterFees = true;
        //public int? StartSemesterIdForAutoGenerateSemesterFees = 202101; // null for all semester 202201
        //public bool IsAutoGenerateOneTimeFees = true;
        //public int? StartSemesterIdForAutoGenerateOneTimeFees = 202101; // null for all semester

        //public float MidtermPayableInPercent = 50;
        //public CreditTransBillGenTypeEnum CreditTransBillGenType = CreditTransBillGenTypeEnum.Normal;
        //public bool IsOpenSingleScreenMarkEntry = true;

        //public string OnlineStdRegConfirmMessage = "কনফার্মের পরে আপনি কোনও পরিবর্তন করতে পারবেন না।";
        //public string InstructionsForOnlineStdReg = "Registration Regulations/Guidelines:<br>" +
        //                                                   "01. The last date of Online Registration is July 31, 2021.<br>" +
        //                                                   "02. Before the course registration, the dues of the previous semester(s) need to be cleared first and the result(s) of the previous semester must be known.<br>" +
        //                                                   "03. To make sure about the coursecourses offered (for Spring 2021), consult the Advisor/ Coordinator/HoD (Head of Department).<br>" +
        //                                                   "04. The limit on the credit hours for a trimester (4 m) system is 9 to 15 and that for a semester (6 m) system is 12 to 18.<br>" +
        //                                                   "05. First to finish the courses (including the retakes) that have not yet been completed.<br>" +
        //                                                   "06. While registering, the year and semester are to be selected correctly, the chosen courses and the offered courses are to be same.  (After confirming the courses for registration, no change will be possible.)<br>" +
        //                                                   "07. The Credit Transfer students are to collect their course offerings from the respective HoD/Coordinator and then contact the Registration Section for registration.<br>" +
        //                                                   "08. The students following the Open Credit System are to register the courses prescribed for the semester.  For changes, they have to contact the respective HoD/Coordinator and then to contact the Registration Section to make the changes.<br>" +
        //                                                   "<br><b>Registration Procedure:</b><br>" +
        //                                                   "01. From the first screen (after logging in) select ‘Online Registration’. (A new screen will appear.)<br>" +
        //                                                   "02. Select Year and Semester.  (The Course list for the semester will appear.)<br>" +
        //                                                   "03. Add the courses and confirm.<br><br>" +
        //                                                   "If needed please contact your Advisor or the Coordinator (tel. nos. are given below).<br>" +
        //                                                   "FDT-01918683391, AMMT-01712779224, GDM-01776272802, IA-01675622127, CSE/CSIT-01755806143, Arch.-01711678323, BBA-01934457515, Law-01725308252, English-01717238283, SOA-01673950671, IS-01843888905, Bangla-01715641076, GP-01911322268, FineArt-01674841888, Music-01711242065, Dance-01609089129<br><br>Registration Office: - 01999034816, 01988887702, 01999034801, 01988887700, 01988887701, 01999034809";

        //public string InstructionsForStdPaymentInfo = "Dear students: For any query about the dues, please contact 01810-036141, 01911-240889, 01818-974540, 01971-725578 and 01674-526574.  For payment, deduct Tk 1,000/= from the Mid Term dues and add that to the Final Exam (Assessment) dues.";
        //public StdPaymentCollectionSettings StudentPaymentCollectionPanel => new StdPaymentCollectionSettings
        //{
        //    IsShowAdmissionFeeDue = false,
        //};

        //public string ParticularIdListForLastSemesterIgnoreBill = "43";

        //public string SemesterBillNamePrefixName = "Semester Fees";
        //public bool IsAppendNumberOfSemesterCountWithSemesterBillName = true;

        #endregion

        /// <summary>
        /// Mangrove Uni Settings
        /// </summary>
        #region Mangrove Uni Settings

        //public string EmsLink = "http://mist.pipilikasoft.com/";
        //public string InstituteWebsite = "https://www.mangrovebd.org/";
        //public string InstituteItSupportLink = "https://www.mangrovebd.org/contact-directory/"; //
        //public string InstituteAddress = "Jashore Road, Boikali, Khulna, Bangladesh.";//"Savar, Dhaka-1342, Bangladesh.";
        //public string InstituteEmail = "ed@mangrove.edu.bd";
        //public string InstituteName = "Mangrove Institute of Science and Technology";//"Jahangirnagar University";
        //public string InstituteShortName = "MIST";//"JU";
        //public string InstituteDomain = "mangrove.edu.bd";//"juniv.edu"
        //public string InstituteCreditPrefix = "";//"Coordinated By:";
        //public string InstituteCredit = "";// "ICT Division, BUHS"; +8801733371333 | +8801733371777



        //public string InstituteRegistrationOfficeCellNo = "+88-01733371333";
        //public string InstituteExamOfficeCellNo = "+88-01733371333";
        //public string InstituteAccountOfficeCellNo = "+88-01733371333";
        //public string InstituteAdmissionOfficeCellNo = "+88-01733371333";
        //public string InstituteAdmissionOfficeLandPhoneNo = "+88-01733371333";
        //public string InstituteIctOfficeCellNo = "+88-01733371333";

        //public bool IsAutoGenStudentIdPrefix = false;
        //public StudentIdGenerateTypeEnum StudentIdGenerateType = StudentIdGenerateTypeEnum.FullManual;
        //public int StudentIdSerialDigitCount = 7;
        //public int TotalStudentIdLength = 9;
        //public bool AutoAllocateClassSectionOnAdmission = false;
        //public int DefaultClassSectionRange = 80;

        //public float AutoGradeGraceMark = (float)1.00;
        //public float AutoGradeGraceMarkForPass = (float)5.00;
        //public string AbsentGradeIdStr = "201608121918146308";
        //public static long AbsentGradeId
        //{
        //    get
        //    {
        //        try
        //        {
        //            return Convert.ToInt64(Instance.AbsentGradeIdStr);
        //        }
        //        catch (Exception e)
        //        {
        //            return 201608121918146308;
        //        }
        //    }
        //}
        //public string AbsentMarkSymbol = "Ab";
        //public string MaximumImproveExamAllowGradeIdStr = "201608121858240468";// B
        //public static long MaximumImproveExamAllowGradeId
        //{
        //    get
        //    {
        //        try
        //        {
        //            return Convert.ToInt64(Instance.MaximumImproveExamAllowGradeIdStr);
        //        }
        //        catch (Exception e)
        //        {
        //            return 201608121858240468;
        //        }
        //    }
        //}// B
        //public long MaximummGradeForRetakeCourseGradeId = 201608121908256795;//



        //public bool IsOnlineResultOpen = false;
        //public bool IsBaustOnlineResultOpen = false;

        //public bool IsForceVarifyStudentEmail = true;
        //public bool IsForceVarifyStudentMobile = true;
        //public bool IsForceChangeStudentPassword = false;
        //public string HowToLoginTutorialUrl = "https://youtu.be/oEeZESqIM-A";
        //public string PasswordChangeTutorialUrl = "https://youtu.be/oEeZESqIM-A?t=16";
        //public string EmailVarifyTutorialUrl = "https://youtu.be/oEeZESqIM-A?t=299";

        //public bool IsMidtermExamEnable = false;


        //public int SuppleExamFeeParticularId = 9;
        //public string SuppleExamViewName = "Supplementary";

        //public bool IsAutoGenerateSemesterFees = true;
        //public int? StartSemesterIdForAutoGenerateSemesterFees = 202101; // null for all semester
        //public bool IsAutoGenerateOneTimeFees = true;
        //public int? StartSemesterIdForAutoGenerateOneTimeFees = 202101; // null for all semester

        //public float MidtermPayableInPercent = 0;
        //public CreditTransBillGenTypeEnum CreditTransBillGenType = CreditTransBillGenTypeEnum.Normal;
        //public bool IsOpenSingleScreenMarkEntry = true;
        //public string OnlineStdRegConfirmMessage = "কনফার্মের পরে আপনি কোনও পরিবর্তন করতে পারবেন না।";
        //public string InstructionsForOnlineStdReg = "Online Registration নির্দেশিকাঃ<br>১. Online Registration 31-01-2021 এর মধ্যে অবশ্যই সম্পন্ন করতে হবে।<br>২. Online Registration করার পূর্বে আপনার সেমিস্টার কোর্স অফার সঠিকভাবে যাচাই করুন (<a href=\"https://eub.edu.bd/course-offer-fall-2020/\" target='_blank'><i class='menu-icon fa fa-user'></i> এই লিংক  থেকে </a>)।<br>৩. আবেদনের সময় ইয়ার এবং সেমিস্টার সঠিকভাবে সিলেক্ট করে কোর্স গুলো পুনরায় অফারিং কোর্স এর সাথে যাচাই করে আপনার Registration নিশ্চিত করুন।<br>৪. Registration কনফার্ম করার পর কোন পরিবর্তন/বাতিল করার সুযোগ থাকবে না।<br>৫. প্রয়োজনেঃ<br>	i) সিভিল, বিজনেস, ল’, ইংলিশ, আই পি ই, টিএইচএম এবং ইকোনোমিকস<br>	 ক) ০১৯৭৮৮০০৪১০ খ) ০১৭৩১২২০২৩২<br>	ii) টেক্সটাইল, ইইই এবং সিএসই<br>	ক) ০১৬২৮৬২৬৬০৮ খ) ০১৯৬৮৭৭৪৯২৫<br>";
        //public StdPaymentCollectionSettings StudentPaymentCollectionPanel => new StdPaymentCollectionSettings
        //{
        //    IsShowAdmissionFeeDue = false,
        //};


        //public string ParticularIdListForLastSemesterIgnoreBill = "";
        //public string SemesterBillNamePrefixName = "Registration Fees";
        //public bool IsAppendNumberOfSemesterCountWithSemesterBillName = false;

        #endregion

        /// <summary>
        /// System Default Settings
        /// </summary>
        #region System Default Settings
        public string InstituteLogoColorSolo = $"/assets/img/logo/{InstituteKey}/InstituteLogoColorSolo.png";
        public string InstituteLogoColorShort = $"/assets/img/logo/{InstituteKey}/InstituteLogoColorShort.png";
        public string InstituteLogoColorFull = $"/assets/img/logo/{InstituteKey}/InstituteLogoColorFull.png";
        public string InstituteLogoBlackWhiteSolo = $"/assets/img/logo/{InstituteKey}/InstituteLogoBlackWhiteSolo.png";
        public string InstituteLogofavicon = $"/assets/img/logo/{InstituteKey}/favicon.png";


        public int DefaultSmsGatewayId = 0;
        public int DefaultErrorEmailSmtpId = 0;
        public int DefaultInfoEmailSmtpId = 0;

        public bool IsSentSmsToParentOnAbsentAttendance = true;
        public bool IsSentEmailToParentOnAbsentAttendance = true;
        public bool IsSentSmsToStudentOnClassMaterialUpload = true;
        public bool IsSentEmalToStudentOnClassMaterialUpload = true;

        #endregion




        #region Student Area
        public StudentSiteSettings Student = new StudentSiteSettings();
        public class StudentSiteSettings
        {
            public bool IsShowRetakeableCourseInOpenCreditRegistration = false;
            public bool IsShowImprovementableCourseInOpenCreditRegistration = false;
            public bool IsStudentLoginOff = false;
            //Registration
            public bool SemesterWiseRegistrationCanView = true;
            public bool SemesterWiseRegistrationCanPrint = true;
            //result
            public bool ResultBySingleSemesterCanView = true;//false; //eub true
            public bool FullGradeReportOfAllSemesterCanView = false;
            public bool SuppleResultCanView = true;
            public bool TransferredCourseResultCanView = false;

            //Payment
            // public bool DebitTransactionCanView = true;// false;//eub true
            public bool DebitTransactionCanView = true;

            // public bool CreditTransactionCanView = false;// false;//eub false
            public bool CreditTransactionCanView = true;

            public bool SemesterPaymentSummaryCanView = true;

            public bool OverallPaymentSummaryCanView = true;

            public bool OnlinePaymentHistoryCanView = true;

            /// <summary>
            /// Please Don't use this settings Directly. Use (OnlinePaymentFacade.IsOpenOnlinePaymentForStudentPanel) use this function 
            /// </summary>
            public bool IsOpenOnlinePayment = true;

            public bool IsEnableSandboxModebKash = false;

            public bool IsOnlyOtherAmountPayOnOnline = false;
            public bool IsAllowAutoSelectLevelTermOnOnlineRegistration = false;

            public string OnlineStudentPaymentTestIds = "";

            public static bool IsOpenOnlinePaymentForStudentPanel
            {
                get
                {
                    if (Instance.Student.IsOpenOnlinePayment || HttpUtil.Profile.UserName.Trim().ToLower() == "test-student".ToLower())
                    {
                        return true;
                    }

                    //todo let other check it.
                    var testStudentsList = Instance.Student.OnlineStudentPaymentTestIds.Split(',');

                    if (testStudentsList != null && testStudentsList.Length > 0)
                    {
                        foreach (var studentId in testStudentsList)
                        {
                            if (HttpUtil.Profile.UserName.Trim().ToLower() == studentId.ToLower())
                            {
                                return true;
                            }
                        }
                    }
                    return false;
                }
            }

            public bool BankPaymentSlipCanView = false;

            //others
            public bool CurriculumOrSyllabusCanView = false;
            public bool FacultyEvaluationCanView = false;

            public bool AdmitCardCanView = true;
            public bool ShowPhotoOnAdmitCard = false;
            public bool LibraryCanView = false;

            public bool OfferedCoursesCanView = false;
            public bool OnlineCourseRegistrationCanApply = true;

            public bool SuppleExamCanView = true;
            public string SuppleExamIdStr = "202103071744192230";
            public static long SuppleExamId
            {
                get
                {
                    try
                    {
                        return Convert.ToInt64(Instance.Student.SuppleExamIdStr);
                    }
                    catch (Exception e)
                    {
                        return 202103071744192230;
                    }
                }
            }
            public TakenClass Class = new TakenClass();

            public class TakenClass
            {
                public bool AttendancesCanView = true;
                public bool ResultsCanView = false;
                public bool CourseMaterialsCanView = true;
                public bool CourseNoticesCanView = true;
            }


            public StudentProfile Profile = new StudentProfile();
            public class StudentProfile
            {
                public bool CanEdit = false;
                public bool CGPACanView = false;
                public bool CompletedCreditsCanView = false;
                public bool CompletedCoursesCanView = false;
            }
            
            public OnlineCourseRegistrationSetting OnlineCourseRegistration = new OnlineCourseRegistrationSetting();
            public class OnlineCourseRegistrationSetting
            {

                public float RegistrationAllowedPaymentDueUpto = -1;
                public float MinimumAdvancePayment = -1;



                /*public string RegistrationStartDate = "";
                public string RegistrationEndDate = "";*/

                //public IsOpenCredit

                //IsOpenRegistrationForRetakeCourse
                //    IsOpenRegistrationForImprovementCourse


                //MaximumImprovementAllowGradeId
                //    MaximumTheoryRetakeCourseAllow

                //MaximumNonTheoryRetakeCourseAllow
                //    MaximumTheoryImprovementCourseAllow

                //MaximumNonTheoryImprovementCourseAllow
                //    MaximumCourseAllowForRegistration
                //MinimumAdvanedPayment
                //    IsEnableLateRegFee
                //LateRegFeeStartDate
                //    LateRegFeeAmount
                //CurrentRegistrationSemester
                //    CanAddFaculty
                //StudentRegistratuinStatus
                //    IsCheckSeatCapacity
                //IsAutoDistributeSection
                //    IsCheckTimeClash
                //RegistrationSemesterId
                //    IsTpeEnable
                //CurrentActiveSemester
                //    IsEnableRegistrationForSubAdmin
                //IsEnableRegistrationForAdmin

                //FacultyInstructionsForMarkEntry

            }


        }

        #endregion

        #region Student Payment Collection
        public class StdPaymentCollectionSettings
        {
            public bool IsShowAdmissionFeeDue = false;
            public bool IsShowMidtermBill = true;
            public bool IsShowMidTermDue = true;
        }


        #endregion

        #region Design Settings

        //public string CurrentTheme
        //{
        //    get
        //    {
        //        object result = GetSetting(ApplicationSettingCollection.CurrentTheme);
        //        if (result == null)
        //        {
        //            SetSetting(ApplicationSettingCollection.CurrentTheme, "Blue");
        //            return "Blue";
        //        }
        //        else
        //            return result.ToString();
        //    }
        //    set { SetSetting(ApplicationSettingCollection.CurrentTheme, value); }
        //}
        //public string ThemeColor
        //{
        //    get
        //    {
        //        object result = GetSetting(ApplicationSettingCollection.ThemeColor);
        //        if (result == null)
        //        {
        //            SetSetting(ApplicationSettingCollection.ThemeColor, "#004ea8");
        //            return "#004ea8";
        //        }
        //        else
        //            return result.ToString();
        //    }
        //    set { SetSetting(ApplicationSettingCollection.ThemeColor, value); }
        //}

        //public string SiteName
        //{
        //    get
        //    {
        //        object result = GetSetting(ApplicationSettingCollection.SiteName);
        //        if (result == null)
        //        {
        //            SetSetting(ApplicationSettingCollection.SiteName, "AIUB");
        //            return "AIUB";
        //        }
        //        else
        //            return result.ToString();
        //    }
        //    set { SetSetting(ApplicationSettingCollection.SiteName, value); }
        //}

        //public string SiteDescription
        //{
        //    get
        //    {
        //        object result = GetSetting(ApplicationSettingCollection.SiteDescription);
        //        if (result == null)
        //        {
        //            SetSetting(ApplicationSettingCollection.SiteDescription, "Where Leaders Are Created");
        //            return "Where Leaders Are Created";
        //        }
        //        else
        //            return result.ToString();
        //    }
        //    set { SetSetting(ApplicationSettingCollection.SiteDescription, value); }
        //}

        //public int PostPerPage
        //{
        //    get
        //    {
        //        object result = GetSetting(ApplicationSettingCollection.PostPerPage);
        //        if (result == null)
        //        {
        //            SetSetting(ApplicationSettingCollection.PostPerPage, 20);
        //            return 20;
        //        }
        //        else
        //            return Convert.ToInt32(result);
        //    }
        //    set { SetSetting(ApplicationSettingCollection.PostPerPage, value); }
        //}

        //public string CategoryBase
        //{
        //    get
        //    {
        //        object result = GetSetting(ApplicationSettingCollection.CategoryBase);
        //        if (result == null)
        //        {
        //            SetSetting(ApplicationSettingCollection.CategoryBase, "/Category");
        //            return "/Category";
        //        }
        //        else
        //            return result.ToString();
        //    }
        //    set { SetSetting(ApplicationSettingCollection.CategoryBase, value); }
        //}

        //public bool CommentNotify
        //{
        //    get
        //    {
        //        object result = GetSetting(ApplicationSettingCollection.CommentNotify);
        //        if (result == null)
        //        {
        //            SetSetting(ApplicationSettingCollection.CommentNotify, true);
        //            return true;
        //        }
        //        else
        //            return Convert.ToBoolean(result);
        //    }
        //    set { SetSetting(ApplicationSettingCollection.CommentNotify, value); }
        //}

        //public bool CommentStatus
        //{
        //    get
        //    {
        //        object result = GetSetting(ApplicationSettingCollection.CommentStatus);
        //        if (result == null)
        //        {
        //            SetSetting(ApplicationSettingCollection.CommentStatus, true);
        //            return true;
        //        }
        //        else
        //            return Convert.ToBoolean(result);
        //    }
        //    set { SetSetting(ApplicationSettingCollection.CommentStatus, value); }
        //}

        //public bool CommentModeration
        //{
        //    get
        //    {
        //        object result = GetSetting(ApplicationSettingCollection.CommentModeration);
        //        if (result == null)
        //        {
        //            SetSetting(ApplicationSettingCollection.CommentModeration, true);
        //            return true;
        //        }
        //        else
        //            return Convert.ToBoolean(result);
        //    }
        //    set { SetSetting(ApplicationSettingCollection.CommentModeration, value); }
        //}

        //public bool ModerationNotify
        //{
        //    get
        //    {
        //        object result = GetSetting(ApplicationSettingCollection.ModerationNotify);
        //        if (result == null)
        //        {
        //            SetSetting(ApplicationSettingCollection.ModerationNotify, true);
        //            return true;
        //        }
        //        else
        //            return Convert.ToBoolean(result);
        //    }
        //    set { SetSetting(ApplicationSettingCollection.ModerationNotify, value); }
        //}

        //public bool CloseCommentForOldPost
        //{
        //    get
        //    {
        //        object result = GetSetting(ApplicationSettingCollection.CloseCommentForOldPost);
        //        if (result == null)
        //        {
        //            SetSetting(ApplicationSettingCollection.CloseCommentForOldPost, true);
        //            return true;
        //        }
        //        else
        //            return Convert.ToBoolean(result);
        //    }
        //    set { SetSetting(ApplicationSettingCollection.CloseCommentForOldPost, value); }
        //}

        //public int CloseCommentDaysOld
        //{
        //    get
        //    {
        //        object result = GetSetting(ApplicationSettingCollection.CloseCommentDaysOld);
        //        if (result == null)
        //        {
        //            SetSetting(ApplicationSettingCollection.CloseCommentDaysOld, 14);
        //            return 14;
        //        }
        //        else
        //            return Convert.ToInt32(result);
        //    }
        //    set { SetSetting(ApplicationSettingCollection.CloseCommentDaysOld, value); }
        //}

        //public bool PageComment
        //{
        //    get
        //    {
        //        object result = GetSetting(ApplicationSettingCollection.PageComment);
        //        if (result == null)
        //        {
        //            SetSetting(ApplicationSettingCollection.PageComment, true);
        //            return true;
        //        }
        //        else
        //            return Convert.ToBoolean(result);
        //    }
        //    set { SetSetting(ApplicationSettingCollection.PageComment, value); }
        //}

        //public int CommentPerPage
        //{
        //    get
        //    {
        //        object result = GetSetting(ApplicationSettingCollection.CommentPerPage);
        //        if (result == null)
        //        {
        //            SetSetting(ApplicationSettingCollection.CommentPerPage, 50);
        //            return 50;
        //        }
        //        else
        //            return Convert.ToInt32(result);
        //    }
        //    set { SetSetting(ApplicationSettingCollection.CommentPerPage, value); }
        //}

        //public int CommentOrder
        //{
        //    get
        //    {
        //        object result = GetSetting(ApplicationSettingCollection.CommentOrder);
        //        if (result == null)
        //        {
        //            SetSetting(ApplicationSettingCollection.CommentOrder, 0);
        //            return 0;
        //        }
        //        else
        //            return Convert.ToInt32(result);
        //    }
        //    set { SetSetting(ApplicationSettingCollection.CommentOrder, value); }
        //}
        //public int NoticeCategory
        //{
        //    get
        //    {
        //        object result = GetSetting(ApplicationSettingCollection.NoticeCategory);
        //        if (result == null)
        //        {
        //            SetSetting(ApplicationSettingCollection.NoticeCategory, 0);
        //            return 0;
        //        }
        //        else
        //            return Convert.ToInt32(result);
        //    }
        //    set { SetSetting(ApplicationSettingCollection.NoticeCategory, value); }
        //}
        //public int NewsCategory
        //{
        //    get
        //    {
        //        object result = GetSetting(ApplicationSettingCollection.NewsCategory);
        //        if (result == null)
        //        {
        //            SetSetting(ApplicationSettingCollection.NewsCategory, 0);
        //            return 0;
        //        }
        //        else
        //            return Convert.ToInt32(result);
        //    }
        //    set { SetSetting(ApplicationSettingCollection.NewsCategory, value); }
        //}
        //public int EventCategory
        //{
        //    get
        //    {
        //        object result = GetSetting(ApplicationSettingCollection.EventCategory);
        //        if (result == null)
        //        {
        //            SetSetting(ApplicationSettingCollection.EventCategory, 0);
        //            return 0;
        //        }
        //        else
        //            return Convert.ToInt32(result);
        //    }
        //    set { SetSetting(ApplicationSettingCollection.EventCategory, value); }
        //}
        //public int OfferedCourseCategory
        //{
        //    get
        //    {
        //        object result = GetSetting(ApplicationSettingCollection.OfferedCourseCategory);
        //        if (result == null)
        //        {
        //            SetSetting(ApplicationSettingCollection.OfferedCourseCategory, 0);
        //            return 0;
        //        }
        //        else
        //            return Convert.ToInt32(result);
        //    }
        //    set { SetSetting(ApplicationSettingCollection.OfferedCourseCategory, value); }
        //}
        //public int CourseOutlineCategory
        //{
        //    get
        //    {
        //        object result = GetSetting(ApplicationSettingCollection.CourseOutlineCategory);
        //        if (result == null)
        //        {
        //            SetSetting(ApplicationSettingCollection.CourseOutlineCategory, 0);
        //            return 0;
        //        }
        //        else
        //            return Convert.ToInt32(result);
        //    }
        //    set { SetSetting(ApplicationSettingCollection.CourseOutlineCategory, value); }
        //}

        #endregion

        #region EmailNotificationSettings

        public SystemEmailSettings SystemEmail = new SystemEmailSettings();
        public class SystemEmailSettings
        {
            public string DomainName = "eub.edu.bd";
            public string Email_SMTP_Server = "smtp.gmail.com";
            public int Email_SMTP_Port = 25;
            public string Email_DisplayName = "eub.edu.bd";
            public string Email_Address = "noreply@eub.edu.bd";
            public string Email_UserName = "noreply@eub.edu.bd";
            public string Email_Password = "3ubgg@#$12";
            public bool Email_EnableSSL = true;
        }

        #endregion

        #region Regular Exam Result Publish Settings

        public string ProgramIdListForResultPublished = "49,50";

        public string CheckProgramForResultOfThoseExamIds = "5";

        #endregion

        #region Admission Area

        public AddNewApplicantForm AddNewApplicantFormSettings = new AddNewApplicantForm();
        public class AddNewApplicantForm
        {
            public bool IsAutoClassSection = true;
            public bool IsAutoAllocateSyllabus = true;
            public bool IsEnableRemarkInput = false;
            public bool IsEnableMobileInput = true;
            public bool IsEnableEmailInput = true;
            public bool IsEnableEnrollTypeSelection = true;
            public bool IsEnableDeptBatchSelection = true;
            public bool IsEnableCampusSelection = false;
            public bool IsEnableAutoRegistration = false;
            public bool IsEnableAutoCourseOffering = false;
            //public bool IsAppendSectionWithId = true;
            public bool IsEnablePreviousEducationInput = true;
            public bool IsEnableSemesterWiseScholarshipInput = true;
            public string UgcVersityCode = "0";
            public bool IsAutoGenerateUgcId = false;
        }

        #endregion

        #region HR Area

        public bool IsPayrollSystemEnabled = false;

        #endregion

        #region Test Area

        public bool IsShowChargeInOnlinePanel = false;

        #endregion

        public static bool ReloadCacheFromFile(out string error)
        {
            error = string.Empty;
            try
            {
                HttpContext.Current.Application[IISCacheKeyName] = LoadSiteSettingsFromFile();
            }
            catch (Exception e)
            {
                error = e.GetBaseException().Message;
                return false;
            }

            return true;
        }
        private static SiteSettings GetSiteSettingsFromCache()
        {
            SiteSettings _siteSettings = null;
            try
            {
                var con = HttpContext.Current;
                _siteSettings = con.Application[IISCacheKeyName] as SiteSettings;
                if (_siteSettings != null)
                {
                    return _siteSettings;
                }
                _siteSettings = LoadSiteSettingsFromFile();
                if (_siteSettings == null)
                {
                    HttpContext.Current.Application[IISCacheKeyName] = new SiteSettings();
                    return new SiteSettings();
                }

                HttpContext.Current.Application[IISCacheKeyName] = _siteSettings;
            }
            catch (Exception e)
            {
                //return Instance;
                return new SiteSettings();
            }
            return _siteSettings;
        }
        private static SiteSettings LoadSiteSettingsFromFile()
        {
            try
            {
                string settingsPath = HttpContext.Current.Server.MapPath(SiteSettingsFullPath);
                if (!File.Exists(settingsPath))
                {
                    return null;
                }
                using (StreamReader file = File.OpenText(settingsPath))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    var siteSettings =  (SiteSettings)serializer.Deserialize(file, typeof(SiteSettings));
                    file.Close();
                    file.Dispose();
                    return siteSettings;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}




