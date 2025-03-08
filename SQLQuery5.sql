USE [F:\C#\אינטרנט\פרויקט יא יב\PROJECT_JIM\PROJECT_GYM\APP_DATA\DB.MDF]
GO

DECLARE	@return_value Int

EXEC	@return_value = [dbo].[spGetAllIdsForArms]

SELECT	@return_value as 'Return Value'

GO
