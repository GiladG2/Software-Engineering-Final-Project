/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Exercises_In_Plans
	(
	Plan_Id int NOT NULL,
	Exercise_Id int NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Exercises_In_Plans ADD CONSTRAINT
	PK_Exercises_In_Plans PRIMARY KEY CLUSTERED 
	(
	Plan_Id,
	Exercise_Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Exercises_In_Plans SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Exercises_In_Plans', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Exercises_In_Plans', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Exercises_In_Plans', 'Object', 'CONTROL') as Contr_Per 