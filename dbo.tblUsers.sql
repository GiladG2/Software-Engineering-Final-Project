CREATE TABLE [dbo].[tblUsers] (
    [fldUsername] VARCHAR(50) NOT NULL,
    [fldPassword] NVARCHAR(50) NOT NULL,
    [fldGender]   BIT NULL,
    [fldDate] DATE NULL, 
    [fldGoal] BIT NOT NULL, 
    [fldFirstname] NVARCHAR(50) NOT NULL, 
    [fldPhone] CHAR(12) NOT NULL, 
    [fldCardio] VARCHAR(50) NOT NULL, 
    [fldAcess] INT NOT NULL DEFAULT 0, 
    PRIMARY KEY CLUSTERED ([fldUsername] ASC)
);

