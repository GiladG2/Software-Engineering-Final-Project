CREATE TABLE [dbo].[tblAnalyticsHistory] (
    [fldHistory_Id] INT IDENTITY(1,1) NOT NULL,                 -- Identity field for history
    [fldAnalytics_Id] INT NOT NULL,                              -- Foreign key to tblAnalytics
    [fldOperation_Id] INT NOT NULL,                              -- Operation type (1 = Insert, 2 = Update, 3 = Delete)
    [fldOperation_Date] DATETIME NOT NULL DEFAULT GETDATE(),     -- Date and time of the operation
    PRIMARY KEY CLUSTERED ([fldHistory_Id] ASC), 
    CONSTRAINT [FK_tblAnalyticsHistory_Analytics] FOREIGN KEY ([fldAnalytics_Id]) 
        REFERENCES [dbo].[tblAnalytics] ([fldAnalytics_Id]) 
        ON DELETE CASCADE 
        ON UPDATE CASCADE
);
