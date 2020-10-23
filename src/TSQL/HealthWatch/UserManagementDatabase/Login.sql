USE [Master]
Go
CREATE LOGIN [HealthWatchUserManagementUser] WITH PASSWORD = '***'
GO
GRANT CONNECT TO [HealthWatchUserManagementUser]
GO
USE [HealthWatchUserManagement]
GO
CREATE USER [HealthWatchUserManagementUser] FOR LOGIN [HealthWatchUserManagementUser]
GO
USE [HealthWatchUserManagement]
GO
ALTER USER [HealthWatchUserManagementUser] WITH DEFAULT_SCHEMA=[dbo]
GO
USE [HealthWatchUserManagement]
GO
ALTER ROLE [db_owner] ADD MEMBER [HealthWatchUserManagementUser]
GO

CREATE LOGIN [HealthWatchDataUser] WITH PASSWORD = '***'
GO

USE [HealthWatchData]
GO
CREATE USER [HealthWatchDataUser] FOR LOGIN [HealthWatchDataUser]
GO
GRANT CONNECT TO [HealthWatchDataUser]
GO
USE [HealthWatchData]
GO
ALTER USER [HealthWatchDataUser] WITH DEFAULT_SCHEMA=[dbo]
GO
USE [HealthWatchData]
GO
ALTER ROLE [db_owner] ADD MEMBER [HealthWatchDataUser]
GO
