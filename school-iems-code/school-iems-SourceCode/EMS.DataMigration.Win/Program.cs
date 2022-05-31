using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using EMS.DataAccess.Data;


namespace EMS.DataMigration.Win
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            TestRegularExp();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new MainForm());
            //DataTransfer();
        }

        public static void DataTransfer()
        {
            EmsDbContext dbInstance=new EmsDbContext();
            var acntList = dbInstance.User_Account.ToList();
            var empList = dbInstance.User_Employee.ToList();
            var stdList = dbInstance.User_Student.ToList();
            try
            {
                Debug.WriteLine($"Start");
                using (DbContextTransaction scope = dbInstance.Database.BeginTransaction())
                {
                    foreach (var acnt in acntList)
                    {
                        if (acnt.UserTypeEnumId==(byte)User_Account.UserTypeEnum.Applicant|| acnt.UserTypeEnumId == (byte)User_Account.UserTypeEnum.Student)
                        {
                            var std = stdList.FirstOrDefault(x => x.UserId == acnt.Id);
                            if (std == null)
                            {
                                Debug.WriteLine($"Student Not Found: {acnt.Id}");
                                continue;
                            }

                            //acnt.JoiningDate = std.DateAdmitted;
                            //acnt.DepartmentId = std.DepartmentId;
                            std.CreditTransfer = std.IncompleteCredits;
                            std.IncompleteCredits = 0;
                        }
                        if (acnt.UserTypeEnumId == (byte)User_Account.UserTypeEnum.Employee)
                        {
                            var emp = empList.FirstOrDefault(x => x.UserId == acnt.Id);
                            if (emp==null)
                            {
                                Debug.WriteLine($"Employee Not Found: {acnt.Id}");
                                continue;
                            }
                            //acnt.JoiningDate = emp.JoiningDate;
                            //acnt.DepartmentId = emp.DepartmentId;
                        }

                    
                     
                        Debug.WriteLine($"Done");
                    }


                    dbInstance.SaveChanges();
                    scope.Commit();
                }
                MessageBox.Show("Compleat");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            


        }

        public static void TestRegularExp()
        {
            String regExp = "^[^<>{}\"/|;:.,~!?@#$%^=&*\\]\\\\()\\[¿§«»ω⊙¤°℃℉€¥£¢¡®©0-9_+]*$";
            String text = "My Name Is & Mohit.o'sh";


            text = RemoveSpecialCharacters(text);
           
            Debug.WriteLine(text);

            //Console.WriteLine("Hello World Mohi");
        }

        public static string RemoveSpecialCharacters(string input)
        {
            Regex r = new Regex("(?:[^a-z0-9 ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            return r.Replace(input, String.Empty);
        }


    }

}
