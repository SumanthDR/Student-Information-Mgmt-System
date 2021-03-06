USE [master]
GO
/****** Object:  Database [dbVidyarthi]    Script Date: 19-03-2022 07:48:32 ******/
CREATE DATABASE [dbVidyarthi]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'dbVidyarthi_Data', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\dbVidyarthi.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'dbVidyarthi_Log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\dbVidyarthi.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [dbVidyarthi] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [dbVidyarthi].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [dbVidyarthi] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [dbVidyarthi] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [dbVidyarthi] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [dbVidyarthi] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [dbVidyarthi] SET ARITHABORT OFF 
GO
ALTER DATABASE [dbVidyarthi] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [dbVidyarthi] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [dbVidyarthi] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [dbVidyarthi] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [dbVidyarthi] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [dbVidyarthi] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [dbVidyarthi] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [dbVidyarthi] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [dbVidyarthi] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [dbVidyarthi] SET  DISABLE_BROKER 
GO
ALTER DATABASE [dbVidyarthi] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [dbVidyarthi] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [dbVidyarthi] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [dbVidyarthi] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [dbVidyarthi] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [dbVidyarthi] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [dbVidyarthi] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [dbVidyarthi] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [dbVidyarthi] SET  MULTI_USER 
GO
ALTER DATABASE [dbVidyarthi] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [dbVidyarthi] SET DB_CHAINING OFF 
GO
ALTER DATABASE [dbVidyarthi] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [dbVidyarthi] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [dbVidyarthi] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [dbVidyarthi] SET QUERY_STORE = OFF
GO
USE [dbVidyarthi]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [dbVidyarthi]
GO
/****** Object:  Table [dbo].[Tbl_RegisterofAdmission]    Script Date: 19-03-2022 07:48:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_RegisterofAdmission](
	[roa_StuId] [int] IDENTITY(1,1) NOT NULL,
	[roa_StuAdmissionNo] [varchar](20) NULL,
	[roa_StuSatsNo] [int] NOT NULL,
	[roa_StuFirstName] [varchar](20) NULL,
	[roa_StuSecondName] [varchar](20) NULL,
	[roa_StuThirdName] [varchar](20) NULL,
	[roa_Gender] [varchar](10) NULL,
	[roa_DOB] [date] NULL,
	[roa_FatherFirstName] [varchar](20) NULL,
	[roa_FatherSecondName] [varchar](20) NULL,
	[roa_FatherThirdName] [varchar](20) NULL,
	[roa_FatherOccupation] [varchar](20) NULL,
	[roa_MotherFirstName] [varchar](20) NULL,
	[roa_MotherSecondName] [varchar](20) NULL,
	[roa_MotherThirdName] [varchar](20) NULL,
	[roa_MotherOccupation] [varchar](20) NULL,
	[roa_AnnualIncome] [bigint] NULL,
	[roa_NoOfDependants] [int] NULL,
	[roa_Nationality] [varchar](20) NULL,
	[roa_Religion] [varchar](20) NULL,
	[roa_Caste] [varchar](20) NULL,
	[roa_MotherTounge] [varchar](20) NULL,
	[roa_PermanentAddress] [varchar](200) NULL,
	[roa_LastSchoolAttended] [varchar](50) NULL,
	[roa_LastStdAdmitted] [int] NULL,
	[roa_PreviousTCNo] [varchar](20) NULL,
	[roa_PreviousTCDate] [date] NULL,
	[roa_StdAdmitted] [int] NULL,
	[roa_StdSection] [varchar](2) NULL,
	[roa_StuDOA] [date] NULL,
	[roa_IsActive] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_SSLCRegistration]    Script Date: 19-03-2022 07:48:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_SSLCRegistration](
	[sr_StuId] [int] IDENTITY(1,1) NOT NULL,
	[sr_roa_Id] [int] NULL,
	[sr_KSEEBCode] [varchar](10) NULL,
	[sr_DiseCode] [varchar](20) NULL,
	[sr_StuAdhaarNo] [varchar](20) NULL,
	[sr_StuFatherPhoneNo] [varchar](11) NULL,
	[sr_StuSocialCategory] [varchar](20) NULL,
	[sr_StuPhysicalCondition] [varchar](20) NULL,
	[sr_MediumOfInstruction] [varchar](20) NULL,
	[sr_Fee] [int] NULL,
	[sr_Language1] [int] NULL,
	[sr_Language2] [int] NULL,
	[sr_Language3] [int] NULL,
	[sr_CoreSubject1] [int] NULL,
	[sr_CoreSubject2] [int] NULL,
	[sr_CoreSubject3] [int] NULL,
	[sr_BankName] [varchar](50) NULL,
	[sr_IFSCcode] [varchar](20) NULL,
	[sr_BankAccNo] [varchar](20) NULL,
	[sr_isActive] [int] NULL,
	[sr_IncomeNo] [varchar](20) NULL,
	[date_SSLCLog] [date] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Subject]    Script Date: 19-03-2022 07:48:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Subject](
	[sub_Id] [int] IDENTITY(1,1) NOT NULL,
	[sub_Code] [varchar](20) NULL,
	[sub_Name] [varchar](20) NULL,
	[sub_Description] [varchar](100) NULL,
	[sub_DOC] [date] NULL,
	[sub_isActive] [int] NULL,
	[sub_type] [varchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_TransferCertificate]    Script Date: 19-03-2022 07:48:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_TransferCertificate](
	[tc_StuId] [int] IDENTITY(1,1) NOT NULL,
	[tc_roa_Id] [int] NULL,
	[tc_StdOnLeaving] [int] NULL,
	[tc_SectionOnLeaving] [varchar](2) NULL,
	[tc_DOL] [date] NULL,
	[tc_TCNo] [varchar](20) NULL,
	[tc_TCDate] [date] NULL,
	[tc_Remarks] [varchar](200) NULL,
	[tc_isActive] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[Prc_AdmissionNoCreation]    Script Date: 19-03-2022 07:48:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Prc_AdmissionNoCreation]
as
begin
select CONCAT(COUNT(roa_StuId)+1,'/',YEAR(GETDATE()),'-',RIGHT(YEAR(GETDATE())+1,2)) as AdmissionNo 
from Tbl_RegisterofAdmission where YEAR(GETDATE()) = YEAR(roa_StuDOA) and roa_IsActive!=0;
end
GO
/****** Object:  StoredProcedure [dbo].[prc_DisplaySSLCRegistration]    Script Date: 19-03-2022 07:48:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[prc_DisplaySSLCRegistration]
(
	@SearchValue varchar(20)
)
as
begin
	select * from Tbl_RegisterofAdmission
	where @SearchValue like(roa_StuAdmissionNo) or @SearchValue like(roa_StuSatsNo);
end
GO
/****** Object:  StoredProcedure [dbo].[prc_DisplaySubjects]    Script Date: 19-03-2022 07:48:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[prc_DisplaySubjects]
(
	@subject_type varchar(100)
)
as
if (@subject_type = 'LANGUAGE')
begin
	select sub_Id,sub_Name from Tbl_Subject s where s.sub_type = 'LANGUAGE';
end
else if (@subject_type = 'CORE')
begin
	select sub_Id,sub_Name from Tbl_Subject s where s.sub_type = 'Core';
end
GO
/****** Object:  StoredProcedure [dbo].[prc_retrievSubjectsSSLC]    Script Date: 19-03-2022 07:48:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[prc_retrievSubjectsSSLC]
(
	@stuid varchar(10),
	@sub varchar(20),
	@subType varchar(20)
)
as
begin
select sub_Name,sub_Id from Tbl_SSLCRegistration sr,Tbl_Subject s 
where sr_StuId = @stuid and s.sub_Id = @sub and sub_type = @subType;
end

GO
/****** Object:  StoredProcedure [dbo].[Prc_SSLCRegistrationInsert]    Script Date: 19-03-2022 07:48:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Prc_SSLCRegistrationInsert]
(
	@flags int,
	@stuId int,
	@roa_id int,
	@kseeb_code varchar(10),
	@dise_Code varchar(20),
	@sr_adhaarNo varchar(20),
	@sr_fatherPhoneNo varchar(10),
	@sr_SocialCategory varchar(20),
	@sr_PhysicalCondition varchar(20),
	@sr_MediumofInstruction varchar(10),
	@sr_Fee varchar(10),
	@sr_Language1_id int,
	@sr_Language2_id int,
	@sr_Language3_id int,
	@sr_CoreSubject1 int,
	@sr_CoreSubject2 int,
	@sr_CoreSubject3 int,
	@sr_BankName varchar(50),
	@sr_IFSCode varchar(20),
	@sr_BankAccNo varchar(50),
	@sr_isActive int,
	@sr_IncomeNo varchar(20),
	@date_SSLCLog date,
	@ERROR varchar(100) out
)
as

if @flags = 1
begin
	if exists ( select sr_roa_Id from Tbl_SSLCRegistration where sr_roa_Id = @roa_id or sr_StuAdhaarNo like @sr_adhaarNo)
	begin
		set @ERROR = 'Student Information already exists / Repeated Adhaar No.'
	end
	else
	if exists (select tc_StuId from Tbl_TransferCertificate where tc_roa_Id = @roa_id and tc_isActive = 1)
	begin
		set @ERROR = 'TC Issued to Student'
	end
	else
	begin
		insert into Tbl_SSLCRegistration
		values(
			@roa_id,@kseeb_code, @dise_Code, @sr_adhaarNo, @sr_fatherPhoneNo,
			@sr_SocialCategory, @sr_PhysicalCondition, @sr_MediumofInstruction,
			@sr_Fee, @sr_Language1_id, @sr_Language2_id, @sr_Language3_id,
			@sr_CoreSubject1, @sr_CoreSubject2, @sr_CoreSubject3, 
			@sr_BankName, @sr_IFSCode, @sr_BankAccNo, @sr_isActive, @sr_IncomeNo, @date_SSLCLog
		);
		set @ERROR = 'Record Saved Successfully'
	end
end
else if @flags = 2
begin
		update Tbl_SSLCRegistration
		set		
		sr_KSEEBCode = @kseeb_code,
		sr_DiseCode = @dise_Code,
		sr_StuAdhaarNo = @sr_adhaarNo,
		sr_StuFatherPhoneNo = @sr_fatherPhoneNo,
		sr_StuSocialCategory = @sr_SocialCategory, 
		sr_StuPhysicalCondition = @sr_PhysicalCondition, 
		sr_MediumOfInstruction = @sr_MediumofInstruction,
		sr_Fee = @sr_Fee, 
		sr_Language1 = @sr_Language1_id, 
		sr_Language2 = @sr_Language2_id, 
		sr_Language3 = @sr_Language3_id,
		sr_CoreSubject1 = @sr_CoreSubject1, 
		sr_CoreSubject2 = @sr_CoreSubject2, 
		sr_CoreSubject3 = @sr_CoreSubject3, 
		sr_BankName = @sr_BankName, 
		sr_IFSCcode = @sr_IFSCode, 
		sr_BankAccNo = @sr_BankAccNo, 
		sr_IncomeNo = @sr_IncomeNo		
		where sr_StuId like @stuId
		and sr_isActive = 1;
		set @ERROR = 'Record Updated Successfully'
end
else if @flags = 3
begin
	update Tbl_SSLCRegistration 
			set 
				sr_isActive = 0
				where sr_StuId like @stuId
		and sr_isActive = 1;
		set @ERROR = 'Record Deleted Successfully'
end
	

GO
/****** Object:  StoredProcedure [dbo].[prc_SubjectInsert]    Script Date: 19-03-2022 07:48:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[prc_SubjectInsert]
(
	@flag int,
	@subId int,
	@code varchar(20),
	@name varchar(20),
	@description varchar(100),
	@creationDate date,
	@isActive int,
	@subType varchar(20),
	@ERROR varchar(100) out
)
as
if @flag = 1
begin
	if exists(select sub_Id from Tbl_Subject where sub_Name = @name and sub_isActive = 1)
	begin
		set @ERROR = 'Subject Code or Name already Exists !';
	end
	else
	begin
		insert into Tbl_Subject values(@code,@name,@description,@creationDate,@isActive,@subType);
		set @ERROR = 'Subject Saved Successfully';
	end
end
else if @flag = 2
begin 
	update Tbl_Subject
		set sub_Code = @code,
			sub_Name = @name,
			sub_type = @subType,
			sub_Description = @description,
			sub_DOC =  @creationDate
			where sub_Id = @subId;
	set @ERROR = 'Subject Updated Successfully';
end
else if @flag = 3
begin
	update Tbl_Subject
		set sub_isActive = 0
			where sub_Id = @subId;
	set @ERROR = 'Subject Deleted Successfully';
end
GO
/****** Object:  StoredProcedure [dbo].[Prc_TransferCertificate]    Script Date: 19-03-2022 07:48:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Prc_TransferCertificate]
(
	@flags int,
	@tc_StuId int,
	@tc_roaid int,
	@tc_stdOnLeaving int,
	@tc_sectionOnLeaving varchar(5),
	@tc_DateOnLeaving date,
	@tc_Number int,
	@tc_Date date,
	@tc_Remarks varchar(100),
	@tc_isActive varchar(100),
	@ERROR varchar(100) out
)
as
if @flags = 1
	begin
	if exists( select tc_roa_Id from Tbl_TransferCertificate where tc_roa_Id = @tc_roaid)
	begin
		set @ERROR = 'Student Transfer Certificate Already Exists !';
	end
	else
	begin
		insert into Tbl_TransferCertificate values
		(
		@tc_roaid,
		@tc_stdOnLeaving,
		@tc_sectionOnLeaving,
		@tc_DateOnLeaving,
		@tc_Number,
		@tc_Date,
		@tc_Remarks,
		@tc_isActive
		);
		set @ERROR = 'Transfer Certificate Saved Successfully';
	end
end
else if @flags = 2
begin
	update Tbl_TransferCertificate 
		set
			tc_roa_Id = @tc_roaid,
			tc_StdOnLeaving = @tc_stdOnLeaving,
			tc_SectionOnLeaving = @tc_sectionOnLeaving,
			tc_DOL = @tc_DateOnLeaving,
			tc_TCNo = @tc_Number,
			tc_TCDate = @tc_Date,
			tc_Remarks = @tc_Remarks		 			 		
		where 
			tc_StuId =  @tc_StuId AND	tc_isActive = 1;
	set @ERROR = 'Transfer Certificate Updated Successfully';
end
else if @flags = 3
begin
	update Tbl_TransferCertificate 
		set
			tc_isActive = 0
		where 
			tc_StuId =  @tc_StuId AND tc_isActive = 1;
	set @ERROR = 'Transfer Certificate Deleted Successfully';
end
GO
/****** Object:  StoredProcedure [dbo].[Prc_ViewRegisterofAdmission]    Script Date: 19-03-2022 07:48:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Prc_ViewRegisterofAdmission]
(
	@flag int,
	@search varchar(20)
)
as
if @flag = 1
begin
	SELECT 
		roa_StuId,roa_StuAdmissionNo,roa_StuSatsNo,
		roa_StuFirstName,roa_StuSecondName,roa_StuThirdName,
		roa_Gender,roa_DOB,roa_FatherFirstName,roa_FatherSecondName,
		roa_FatherThirdName,roa_MotherFirstName,roa_MotherSecondName,
		roa_MotherThirdName,roa_PermanentAddress,roa_Religion,roa_Caste,
		roa_AnnualIncome
	FROM 
		Tbl_RegisterofAdmission where roa_IsActive = 1 AND 
		(roa_StuAdmissionNo like @search OR roa_StuSatsNo like @search);
end
GO
/****** Object:  StoredProcedure [dbo].[Prc_ViewSearchRegisterofAdmission]    Script Date: 19-03-2022 07:48:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Prc_ViewSearchRegisterofAdmission]
(
	@flag int,
	@search varchar(20),
	@dob date,
	@para varchar(20),
	@para1 varchar(20)
)
as

if @flag = 1
begin
	SELECT 
		roa_StuId,roa_StuAdmissionNo,roa_StuSatsNo,
		CONCAT(roa_StuFirstName,' ',roa_StuSecondName,' ',roa_StuThirdName) as stuName,		
		roa_Gender,roa_DOB,
		concat(roa_FatherFirstName,' ',roa_FatherSecondName,' ',roa_FatherThirdName) as fatherName,
		CONCAT(roa_MotherFirstName,' ',roa_MotherSecondName,' ',roa_MotherThirdName) as motherName,
		roa_PermanentAddress,roa_Religion,roa_Caste,roa_MotherTounge as mothertounge,
		roa_AnnualIncome,roa_StdAdmitted,roa_StdSection,roa_StuDOA
	FROM 
		Tbl_RegisterofAdmission where roa_IsActive = 1
		and YEAR(getdate()) like YEAR(roa_StuDOA);
end
else if @flag = 2
begin
SELECT 
		roa_StuId,roa_StuAdmissionNo,roa_StuSatsNo,
		CONCAT(roa_StuFirstName,' ',roa_StuSecondName,' ',roa_StuThirdName) as stuName,		
		roa_Gender,roa_DOB,
		concat(roa_FatherFirstName,' ',roa_FatherSecondName,' ',roa_FatherThirdName) as fatherName,
		CONCAT(roa_MotherFirstName,' ',roa_MotherSecondName,' ',roa_MotherThirdName) as motherName,
		roa_PermanentAddress,roa_Religion,roa_Caste,roa_MotherTounge as mothertounge,
		roa_AnnualIncome,roa_StdAdmitted,roa_StdSection,roa_StuDOA
	FROM 
		Tbl_RegisterofAdmission where roa_IsActive = 1 AND 
		(roa_StuAdmissionNo like @search OR roa_StuSatsNo like @search);
end
else if @flag = 3
begin
SELECT 
		roa_StuId,roa_StuAdmissionNo,roa_StuSatsNo,
		roa_StuFirstName,roa_StuSecondName,roa_StuThirdName,
		roa_Gender,roa_DOB,
		roa_FatherFirstName,roa_FatherSecondName,roa_FatherThirdName,
		roa_FatherOccupation,
		roa_MotherFirstName,roa_MotherSecondName,roa_MotherThirdName,
		roa_MotherOccupation,
		roa_PermanentAddress,roa_Religion,roa_Caste,roa_MotherTounge as mothertounge,
		roa_NoOfDependants,roa_Nationality,roa_LastSchoolAttended,roa_LastStdAdmitted,
		roa_PreviousTCDate,roa_PreviousTCNo,
		roa_AnnualIncome,roa_StdAdmitted,roa_StdSection,roa_StuDOA
	FROM 
		Tbl_RegisterofAdmission where roa_IsActive = 1 AND 
		roa_StuId like @search;
end
else if @flag = 4
begin
SELECT roa_StuId,roa_StuAdmissionNo,roa_StuSatsNo,
		CONCAT(roa_StuFirstName,' ',roa_StuSecondName,' ',roa_StuThirdName) as stuName,		
		roa_Gender,roa_DOB,
		concat(roa_FatherFirstName,' ',roa_FatherSecondName,' ',roa_FatherThirdName) as fatherName,
		CONCAT(roa_MotherFirstName,' ',roa_MotherSecondName,' ',roa_MotherThirdName) as motherName,
		roa_PermanentAddress,roa_Religion,roa_Caste,roa_MotherTounge as mothertounge,
		roa_AnnualIncome,roa_StdAdmitted,roa_StdSection,roa_StuDOA
		from Tbl_RegisterofAdmission where roa_DOB like @dob and roa_IsActive = 1;
end
else if @flag = 5
begin
SELECT roa_StuId,roa_StuAdmissionNo,roa_StuSatsNo,
		CONCAT(roa_StuFirstName,' ',roa_StuSecondName,' ',roa_StuThirdName) as stuName,		
		roa_Gender,roa_DOB,
		concat(roa_FatherFirstName,' ',roa_FatherSecondName,' ',roa_FatherThirdName) as fatherName,
		CONCAT(roa_MotherFirstName,' ',roa_MotherSecondName,' ',roa_MotherThirdName) as motherName,
		roa_PermanentAddress,roa_Religion,roa_Caste,roa_MotherTounge as mothertounge,
		roa_AnnualIncome,roa_StdAdmitted,roa_StdSection,roa_StuDOA
		from Tbl_RegisterofAdmission where year(roa_StuDOA) like @search and roa_IsActive = 1;
end
else if @flag = 6
begin
SELECT roa_StuId,roa_StuAdmissionNo,roa_StuSatsNo,
		CONCAT(roa_StuFirstName,' ',roa_StuSecondName,' ',roa_StuThirdName) as stuName,		
		roa_Gender,roa_DOB,
		concat(roa_FatherFirstName,' ',roa_FatherSecondName,' ',roa_FatherThirdName) as fatherName,
		CONCAT(roa_MotherFirstName,' ',roa_MotherSecondName,' ',roa_MotherThirdName) as motherName,
		roa_PermanentAddress,roa_Religion,roa_Caste,roa_MotherTounge as mothertounge,
		roa_AnnualIncome,roa_StdAdmitted,roa_StdSection,roa_StuDOA
		from Tbl_RegisterofAdmission where ((year(roa_StuDOA) like @search) and (roa_StdAdmitted like @para))and roa_IsActive = 1;
end
else if @flag = 7
begin
SELECT roa_StuId,roa_StuAdmissionNo,roa_StuSatsNo,
		CONCAT(roa_StuFirstName,' ',roa_StuSecondName,' ',roa_StuThirdName) as stuName,		
		roa_Gender,roa_DOB,
		concat(roa_FatherFirstName,' ',roa_FatherSecondName,' ',roa_FatherThirdName) as fatherName,
		CONCAT(roa_MotherFirstName,' ',roa_MotherSecondName,' ',roa_MotherThirdName) as motherName,
		roa_PermanentAddress,roa_Religion,roa_Caste,roa_MotherTounge as mothertounge,
		roa_AnnualIncome,roa_StdAdmitted,roa_StdSection,roa_StuDOA
		from Tbl_RegisterofAdmission where ((year(roa_StuDOA) like @search) and (roa_StdAdmitted like @para) and (roa_StdSection like @para1))and roa_IsActive = 1;
end


GO
/****** Object:  StoredProcedure [dbo].[prc_ViewSearchSSLCRegistration]    Script Date: 19-03-2022 07:48:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[prc_ViewSearchSSLCRegistration]
(
	@flag int,
	@search varchar(20),
	@para varchar(20)
)
as
if @flag = 1
begin
	select sr.sr_StuId,sr.sr_roa_Id,roa.roa_StuAdmissionNo,roa.roa_StuSatsNo,
		CONCAT(roa.roa_StuFirstName,' ',roa.roa_StuSecondName,' ',roa.roa_StuThirdName) as StuName,
		roa.roa_Gender,roa.roa_DOB,
		CONCAT(roa.roa_FatherFirstName,' ',roa.roa_FatherSecondName,' ',roa.roa_FatherThirdName) as fatherName,
		sr.sr_StuAdhaarNo,sr.sr_StuFatherPhoneNo,sr.sr_MediumOfInstruction,sr.sr_IncomeNo
	from Tbl_RegisterofAdmission roa join Tbl_SSLCRegistration sr
		on roa.roa_StuId = sr.sr_roa_Id
	where
		((roa.roa_IsActive = 1 and sr.sr_isActive = 1)
		and 
		(@search like YEAR(roa.roa_StuDOA) or 
		(@search like roa.roa_StuSatsNo) or @search like roa.roa_StuAdmissionNo))
end
else if @flag = 2
begin
	select sr.sr_StuId,sr.sr_roa_Id,roa.roa_StuAdmissionNo,roa.roa_StuSatsNo,
		roa.roa_StuFirstName,roa.roa_StuSecondName,roa.roa_StuThirdName,
		roa.roa_Gender,roa.roa_DOB,
		roa.roa_MotherFirstName,roa.roa_MotherSecondName,roa.roa_MotherThirdName,
		roa.roa_FatherFirstName,roa.roa_FatherSecondName,roa.roa_FatherThirdName,
		roa.roa_PermanentAddress,roa.roa_Religion,roa.roa_AnnualIncome,roa.roa_Caste,
		sr.sr_StuAdhaarNo,sr.sr_StuFatherPhoneNo,sr.sr_MediumOfInstruction,sr.sr_IncomeNo,
		sr.sr_StuSocialCategory,sr.sr_IncomeNo,sr.sr_StuPhysicalCondition,
		sr.sr_MediumOfInstruction,sr.sr_Fee,sr.sr_Language1,sr.sr_Language2,
		sr.sr_Language3,sr.sr_CoreSubject1,sr.sr_CoreSubject2,sr.sr_CoreSubject3,
		sr.sr_BankName,sr.sr_IFSCcode,sr.sr_BankAccNo
	from Tbl_RegisterofAdmission roa join Tbl_SSLCRegistration sr
		on roa.roa_StuId = sr.sr_roa_Id
	where
		((roa.roa_IsActive = 1 and sr.sr_isActive = 1)
		and 
		(sr.sr_StuId like @search));
end
else if @flag = 3
begin
	select sr.sr_StuId,sr.sr_roa_Id,roa.roa_StuAdmissionNo,roa.roa_StuSatsNo,
		CONCAT(roa.roa_StuFirstName,' ',roa.roa_StuSecondName,' ',roa.roa_StuThirdName) as StuName,
		roa.roa_Gender,roa.roa_DOB,
		CONCAT(roa.roa_FatherFirstName,' ',roa.roa_FatherSecondName,' ',roa.roa_FatherThirdName) as fatherName,
		sr.sr_StuAdhaarNo,sr.sr_StuFatherPhoneNo,sr.sr_MediumOfInstruction,sr.sr_IncomeNo
	from Tbl_RegisterofAdmission roa join Tbl_SSLCRegistration sr
		on roa.roa_StuId = sr.sr_roa_Id
	where
		((roa.roa_IsActive = 1 and sr.sr_isActive = 1)
		and 		
		(YEAR(sr.date_SSLCLog) like @search));
end
else if @flag = 4
begin
	select sr.sr_StuId,sr.sr_roa_Id,roa.roa_StuAdmissionNo,roa.roa_StuSatsNo,
		CONCAT(roa.roa_StuFirstName,' ',roa.roa_StuSecondName,' ',roa.roa_StuThirdName) as StuName,
		roa.roa_Gender,roa.roa_DOB,
		CONCAT(roa.roa_FatherFirstName,' ',roa.roa_FatherSecondName,' ',roa.roa_FatherThirdName) as fatherName,
		sr.sr_StuAdhaarNo,sr.sr_StuFatherPhoneNo,sr.sr_MediumOfInstruction,sr.sr_IncomeNo
	from Tbl_RegisterofAdmission roa join Tbl_SSLCRegistration sr
		on roa.roa_StuId = sr.sr_roa_Id
	where
		((roa.roa_IsActive = 1 and sr.sr_isActive = 1)
		and 
		(sr.sr_MediumOfInstruction like @search));
end
else if @flag = 5
begin
	select sr.sr_StuId,sr.sr_roa_Id,roa.roa_StuAdmissionNo,roa.roa_StuSatsNo,
		CONCAT(roa.roa_StuFirstName,' ',roa.roa_StuSecondName,' ',roa.roa_StuThirdName) as StuName,
		roa.roa_Gender,roa.roa_DOB,
		CONCAT(roa.roa_FatherFirstName,' ',roa.roa_FatherSecondName,' ',roa.roa_FatherThirdName) as fatherName,
		sr.sr_StuAdhaarNo,sr.sr_StuFatherPhoneNo,sr.sr_MediumOfInstruction,sr.sr_IncomeNo
	from Tbl_RegisterofAdmission roa join Tbl_SSLCRegistration sr
		on roa.roa_StuId = sr.sr_roa_Id
	where
		((roa.roa_IsActive = 1 and sr.sr_isActive = 1)
		and 
		(sr.sr_MediumOfInstruction like @search) and (year(sr.date_SSLCLog) like @para));
end
GO
/****** Object:  StoredProcedure [dbo].[Prc_ViewSearchSubject]    Script Date: 19-03-2022 07:48:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Prc_ViewSearchSubject]
(
	@flag int,
	@search varchar(50)
)
as
if @flag = 1
begin
	select sub_Id,sub_Code,sub_Name,sub_Description,sub_DOC,sub_isActive,sub_type
	from Tbl_Subject where sub_isActive = 1
end
else if @flag = 2
begin
select sub_Id,sub_Code,sub_Name,sub_Description,sub_DOC,sub_isActive,sub_type
	from Tbl_Subject where sub_isActive = 1 and ( sub_type like '%'+@search+'%'
												or sub_Name like '%'+@search+'%'
												or sub_Code like '%'+@search+'%');
end
else if @flag = 3
begin
select sub_Id,sub_Code,sub_Name,sub_Description,sub_DOC,sub_isActive,sub_type
	from Tbl_Subject where sub_isActive = 1 and sub_Id = @search
end
GO
/****** Object:  StoredProcedure [dbo].[prc_ViewSearchTCRegistration]    Script Date: 19-03-2022 07:48:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[prc_ViewSearchTCRegistration]
(
	@flag int,
	@search varchar(20)
)
as

if @flag = 1
begin
	select tc.tc_StuId,tc.tc_roa_Id,roa.roa_StuAdmissionNo,roa.roa_StuSatsNo,
		CONCAT(roa.roa_StuFirstName,' ',roa.roa_StuSecondName,' ',roa.roa_StuThirdName) as StuName,
		roa.roa_Gender,roa.roa_DOB,
		CONCAT(roa.roa_FatherFirstName,' ',roa.roa_FatherSecondName,' ',roa.roa_FatherThirdName) as fatherName,
		tc.tc_StdOnLeaving,tc.tc_SectionOnLeaving,tc.tc_TCNo,tc.tc_TCDate
	from Tbl_RegisterofAdmission roa join Tbl_TransferCertificate tc
		on roa.roa_StuId = tc.tc_roa_Id
	where
		((roa.roa_IsActive = 1 and tc.tc_isActive = 1) and
		 (@search like YEAR(tc_TCDate) or 
		 (@search like roa.roa_StuAdmissionNo or @search like roa.roa_StuSatsNo)))
end
GO
/****** Object:  StoredProcedure [dbo].[Prc_ViewSubjectSSLCForUpdate]    Script Date: 19-03-2022 07:48:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[Prc_ViewSubjectSSLCForUpdate]
(
	@studentid varchar(10),
	@flag int
)
as
if @flag = 1
begin
	select sslc.sr_CoreSubject1,sub.sub_Name from Tbl_SSLCRegistration sslc join Tbl_Subject sub
	on sslc.sr_CoreSubject1 = sub.sub_Id join Tbl_RegisterofAdmission roa on roa.roa_StuId = sslc.sr_roa_Id
	where roa.roa_StuAdmissionNo like @studentid;
end
else if @flag = 2
begin
	select sslc.sr_CoreSubject2,sub.sub_Name from Tbl_SSLCRegistration sslc join Tbl_Subject sub
	on sslc.sr_CoreSubject2 = sub.sub_Id join Tbl_RegisterofAdmission roa on roa.roa_StuId = sslc.sr_roa_Id
	where roa.roa_StuAdmissionNo like @studentid;
end
else if @flag = 3
begin
	select sslc.sr_CoreSubject3,sub.sub_Name from Tbl_SSLCRegistration sslc join Tbl_Subject sub
	on sslc.sr_CoreSubject3 = sub.sub_Id join Tbl_RegisterofAdmission roa on roa.roa_StuId = sslc.sr_roa_Id
	where roa.roa_StuAdmissionNo like @studentid;
end
else if @flag = 4
begin
	select sslc.sr_Language1,sub.sub_Name from Tbl_SSLCRegistration sslc join Tbl_Subject sub
	on sslc.sr_Language1 = sub.sub_Id join Tbl_RegisterofAdmission roa on roa.roa_StuId = sslc.sr_roa_Id
	where roa.roa_StuAdmissionNo like @studentid;
end
if @flag = 5
begin
	select sslc.sr_Language2,sub.sub_Name from Tbl_SSLCRegistration sslc join Tbl_Subject sub
	on sslc.sr_Language2 = sub.sub_Id join Tbl_RegisterofAdmission roa on roa.roa_StuId = sslc.sr_roa_Id
	where roa.roa_StuAdmissionNo like @studentid;
end
if @flag = 6
begin
	select sslc.sr_Language3,sub.sub_Name from Tbl_SSLCRegistration sslc join Tbl_Subject sub
	on sslc.sr_Language3 = sub.sub_Id join Tbl_RegisterofAdmission roa on roa.roa_StuId = sslc.sr_roa_Id
	where roa.roa_StuAdmissionNo like @studentid;
end
GO
/****** Object:  StoredProcedure [dbo].[Prc_ViewTCRegistration]    Script Date: 19-03-2022 07:48:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Prc_ViewTCRegistration]
(
	@flag int,
	@search varchar(20)
)
as
if @flag = 1 
begin
	SELECT 
		roa_StuId,roa_StuAdmissionNo,
		roa_StuFirstName,roa_StuSecondName,roa_StuThirdName,
		roa_Gender,roa_DOB,roa_FatherFirstName,roa_FatherSecondName,
		roa_FatherThirdName,roa_FatherOccupation
		,roa_MotherFirstName,roa_MotherSecondName,
		roa_MotherThirdName,roa_MotherOccupation,
		roa_PermanentAddress,roa_Religion,roa_Caste,
		roa_AnnualIncome,roa_NoOfDependants,roa_Nationality,
		roa_MotherTounge,roa_LastSchoolAttended,roa_LastStdAdmitted,
		roa_PreviousTCDate,roa_PreviousTCNo,roa_StdAdmitted,roa_StdSection,
		roa_StuDOA, roa_StuSatsNo
	FROM 
		Tbl_RegisterofAdmission where roa_IsActive = 1 AND 
		(roa_StuAdmissionNo like @search OR roa_StuSatsNo like @search);
end
else if @flag = 2
begin
	SELECT 
		roa_StuId,roa_StuAdmissionNo,
		roa_StuFirstName,roa_StuSecondName,roa_StuThirdName,
		roa_Gender,roa_DOB,roa_FatherFirstName,roa_FatherSecondName,
		roa_FatherThirdName,roa_FatherOccupation
		,roa_MotherFirstName,roa_MotherSecondName,
		roa_MotherThirdName,roa_MotherOccupation,
		roa_PermanentAddress,roa_Religion,roa_Caste,
		roa_AnnualIncome,roa_NoOfDependants,roa_Nationality,
		roa_MotherTounge,roa_LastSchoolAttended,roa_LastStdAdmitted,
		roa_PreviousTCDate,roa_PreviousTCNo,roa_StdAdmitted,roa_StdSection,
		roa_StuDOA, roa_StuSatsNo, 
		
		tc.tc_DOL, tc.tc_isActive, tc.tc_Remarks, tc.tc_roa_Id, tc.tc_SectionOnLeaving,
		tc.tc_StdOnLeaving, tc.tc_StuId, tc.tc_TCDate, tc.tc_TCNo
		
	FROM 
		Tbl_RegisterofAdmission roa join Tbl_TransferCertificate tc
		on roa.roa_StuId = tc.tc_roa_Id
		where 1 in (roa_IsActive ,tc_isActive) AND tc.tc_StuId like @search
		
end
GO
/****** Object:  StoredProcedure [dbo].[prcRegisterOfAdmissionInsert]    Script Date: 19-03-2022 07:48:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[prcRegisterOfAdmissionInsert]
(
	@flags int,
	@stuId int,
	@admissionNo varchar(20),
	@satsNo int,
	@stuFirstName varchar(20),
	@stuSecondName varchar(20),
	@stuThirdName varchar(20),
	@stuGender varchar(10),
	@stuDOB date,
	@stuFatherFirstName varchar(20),
	@stuFatherSecondName varchar(20),
	@stuFatherThirdName varchar(20),
	@stuFatherOccupation varchar(20),
	@stuMotherFirstName varchar(20),
	@stuMotherSecondName varchar(20),
	@stuMotherThirdName varchar(20),
	@stuMotherOccupation varchar(20),
	@annualIncome bigint,
	@noOfDependants int,
	@stuNationality varchar(20),
	@stuReligion varchar(20),
	@stuCaste varchar(20),
	@stuMotherTounge varchar(20),
	@stuPermanentAddress varchar(200),
	@stuLastSchoolAttended varchar(100),
	@stuLastStdAdmitted int,
	@stuPreviousTCNo varchar(20),
	@stuPreviousTCDate varchar(20),
	@stuStdAdmitted int,
	@stuStdSection varchar(2),
	@stuDOA date,
	@isActive int,	
	@ERROR varchar(100) out
)
as
if @flags = 1
begin
	if exists(select roa_StuId from Tbl_RegisterofAdmission where roa_StuSatsNo = @satsNo and roa_IsActive = 1)
	begin
		set @ERROR = 'SATS number already exists !';
	end
	else
	begin
	insert into Tbl_RegisterofAdmission values(
												@admissionNo ,
												@satsNo ,
												@stuFirstName ,
												@stuSecondName ,
												@stuThirdName ,
												@stuGender ,
												@stuDOB ,
												@stuFatherFirstName ,
												@stuFatherSecondName ,
												@stuFatherThirdName ,
												@stuFatherOccupation ,
												@stuMotherFirstName ,
												@stuMotherSecondName ,
												@stuMotherThirdName ,
												@stuMotherOccupation ,
												@annualIncome ,
												@noOfDependants ,
												@stuNationality ,
												@stuReligion ,
												@stuCaste ,
												@stuMotherTounge ,
												@stuPermanentAddress ,
												@stuLastSchoolAttended ,
												@stuLastStdAdmitted ,
												@stuPreviousTCNo ,
												@stuPreviousTCDate ,
												@stuStdAdmitted ,
												@stuStdSection ,
												@stuDOA ,
												@isActive
											);
	set @ERROR = 'Record Saved Successfully';
end
end
else if @flags = 2
begin
	update Tbl_RegisterofAdmission set 
										roa_StuAdmissionNo = @admissionNo ,
										roa_StuSatsNo =  @satsNo ,
										roa_StuFirstName= @stuFirstName ,
										roa_StuSecondName = @stuSecondName ,
										roa_StuThirdName = @stuThirdName ,
										roa_Gender = @stuGender ,
										roa_DOB = @stuDOB ,
										roa_FatherFirstName = @stuFatherFirstName ,
										roa_FatherSecondName = @stuFatherSecondName ,
										roa_FatherThirdName = @stuFatherThirdName ,
										roa_FatherOccupation = @stuFatherOccupation ,
										roa_MotherFirstName = @stuMotherFirstName ,
										roa_MotherSecondName = @stuMotherSecondName ,
										roa_MotherThirdName = @stuMotherThirdName ,
										roa_MotherOccupation = @stuMotherOccupation ,
										roa_AnnualIncome = @annualIncome ,
										roa_NoOfDependants = @noOfDependants ,
										roa_Nationality = @stuNationality ,
										roa_Religion = @stuReligion ,
										roa_Caste = @stuCaste ,
										roa_MotherTounge = @stuMotherTounge ,
										roa_PermanentAddress = @stuPermanentAddress ,
										roa_LastSchoolAttended = @stuLastSchoolAttended ,
										roa_LastStdAdmitted = @stuLastStdAdmitted ,
										roa_PreviousTCNo =  @stuPreviousTCNo ,
										roa_PreviousTCDate = @stuPreviousTCDate ,
										roa_StdAdmitted = @stuStdAdmitted ,
										roa_StdSection = @stuStdSection ,
										roa_StuDOA = @stuDOA												
								where roa_StuId = @stuId
								AND roa_IsActive = 1
	set @ERROR = 'Record Updated Successfully';
end
else if @flags = 3
begin
	update Tbl_RegisterofAdmission set 
										roa_IsActive = 0
								where roa_StuId = @stuId
								AND roa_IsActive = 1
	set @ERROR = 'Record Deleted Successfully';
end
GO
USE [master]
GO
ALTER DATABASE [dbVidyarthi] SET  READ_WRITE 
GO
