/****** Object:  Table [dbo].[tblMySubmissions]    Script Date: 9/23/2020 7:11:23 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblMySubmissions](
[LineID] [int] IDENTITY(1,1) NOT NULL,
[GoCanSubmissionGUID] [nvarchar](50) NULL,
[FormName] [nvarchar](50) NULL,

 CONSTRAINT [PK_tblMySubmissions] PRIMARY KEY CLUSTERED 
(
[LineID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
