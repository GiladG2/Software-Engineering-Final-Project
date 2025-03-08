CREATE TABLE [dbo].[tblUsers]
(
	[fldUsername] NVARCHAR(50) NOT NULL PRIMARY KEY, 
    [fldPassword] NVARCHAR(50) NOT NULL, 
    [fldGender] NVARCHAR(50) NULL, 
)
