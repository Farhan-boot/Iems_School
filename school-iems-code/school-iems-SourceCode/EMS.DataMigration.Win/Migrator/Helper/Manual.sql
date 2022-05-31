--Remove Identity Column before Migration
--1. Acnt_FeeCode
--2. User_Student (Make Null Image Column -> UPDATE [UERPEUB2].[STUDENT] SET [PHOTO] = null)
--3. User_Account
--4. Acnt_StdTransaction (not StdTransactionDetail)
--5. Acnt_CollectionVoucher

--======Identity Column End=======


--Migration Steps:
--1. FeeCode + Policy (Clear: Acnt_FeeCode + Acnt_FeeCodePolicy)
--2. Student + Account
--3. Discount
--4. Education
--5. Address
--6. Migrate Credit (Clear: TransactionDetail + Transaction + CollectionVoucher)
--7. Migrate Debit (Clear: TransactionDetail + Transaction + CollectionVoucher)
--8. Migrate Registration + Result


-- tmp query
-- for manually make pk
-- DECLARE @id bigint 
-- SET @id = 1 
-- UPDATE [dbo].[User_Account]
-- SET @id = id = @id + 1 
-- where id!=1
-- GO 
 
--select * from User_Account where  id>=12000;
--======Query Order========

USE [EubEms3]
GO

--1. FeeCode + Policy
--DELETE FROM [dbo].[Acnt_FeeCodePolicy] 
--DELETE FROM [dbo].[Acnt_FeeCode] 


----2. Student + Account
--DELETE FROM [dbo].[User_Student] where  id>=1000
--DELETE FROM [dbo].[User_Account] where  id>=1000


----DELETE FROM [dbo].[User_Employee] where id>=128
----DELETE FROM [dbo].UAC_PassReset

----5. Address
--DELETE FROM [dbo].[User_ContactAddress] where  UserId>=1000

----4. Education
--DELETE FROM [dbo].[User_Education] where  UserId>=1000

--3. Discount
--DELETE FROM [dbo].[Acnt_StdScholarship] 

--6. TransactionDetail + Transaction + CollectionVoucher
--DELETE FROM [dbo].[Acnt_StdTransactionDetail] 
--DELETE FROM [dbo].[Acnt_StdTransaction] 
--DELETE FROM [dbo].[Acnt_CollectionVoucher] 

--7. For Migrate Registration + Result 

DELETE FROM Aca_ResultComponentBreakdown
DELETE FROM Aca_ResultComponentBreakdownSetting
DELETE FROM Aca_ResultComponent
DELETE FROM Aca_ResultComponentSetting
DELETE FROM Aca_ResultClass
DELETE FROM Aca_SemesterResult
DELETE FROM Aca_ClassTakenByStudent
DELETE FROM Aca_ClassTakenByEmployee
DELETE FROM Aca_ClassAttendanceSmsLog
DELETE FROM Aca_ClassAttendance
DELETE FROM Aca_ClassAttendanceSetting
DELETE FROM Aca_ResultClass
DELETE FROM Aca_ResultClassSetting
DELETE FROM Aca_ClassRoutine
DELETE FROM Aca_ClassMaterialFileMap
DELETE FROM Aca_ClassNotice
DELETE FROM Aca_ClassWaiverStudent
DELETE FROM Aca_Class
DELETE FROM Aca_StudentRegistration

--accounts
--SELECT MAX(IncrementalSuffix)
--  FROM [EubEms3Migration].[dbo].[Acnt_CollectionVoucher]
--  where BankId=2
--UPDATE [dbo].[Acnt_CollectionVoucher]
--   SET [IncrementalSuffix] = 191220
--     where bankid=2
--GO

-- result processing

USE [EubEms3]
GO
UPDATE [dbo].[Aca_ResultClassSetting]
   SET  
      [IsLockedTabulator] = 0
      ,[IsLocekdByAdmin] = 0
 WHERE examid=201905152213438669
GO

UPDATE [dbo].[Aca_ResultComponentSetting]
   SET 
       [IsLockedExaminer] = 0
      ,[IsLockedScrutinizer] = 0
 
	   WHERE examid=201905152213438669
GO

UPDATE [dbo].[Aca_ResultComponent]
   SET 
      [AttendanceTypeEnumId] = 0
GO

DELETE FROM [dbo].[Aca_ResultComponent]
     WHERE examid=201905152213438669
GO
delete        Aca_ClassTakenByEmployee
FROM  Aca_Class INNER JOIN
            Aca_ClassTakenByEmployee ON Aca_Class.Id = Aca_ClassTakenByEmployee.ClassId
			where Aca_Class.SemesterId=201902 and Aca_ClassTakenByEmployee.EmployeeId!=1
			and Aca_ClassTakenByEmployee.EmployeeId!=25
GO



delete        Aca_ClassTakenByEmployee
FROM  Aca_Class INNER JOIN
        Aca_ClassTakenByEmployee ON Aca_Class.Id = Aca_ClassTakenByEmployee.ClassId
		where Aca_Class.SemesterId=201902 and Aca_ClassTakenByEmployee.EmployeeId!=1
		and Aca_ClassTakenByEmployee.EmployeeId!=25

--- Count Student
SELECT distinct
      Aca_ClassTakenByStudent.[StudentId]
     
FROM            Aca_Class INNER JOIN
                         Aca_ClassTakenByStudent ON Aca_Class.Id = Aca_ClassTakenByStudent.ClassId
						 where Aca_Class.SemesterId=201902

---Employee
USE [EubEms3]
GO

UPDATE [dbo].[User_Employee]
   SET 
      [WorkingStatusEnumId] = 1
GO