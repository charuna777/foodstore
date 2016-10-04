USE [FoodStore]
GO

/****** Object:  Table [dbo].[Product]    Script Date: 10/3/2016 10:19:07 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Product](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Description] [varchar](50) NULL,
	[Price] [decimal](18, 0) NOT NULL,
	[Perishble] [bit] NOT NULL CONSTRAINT [DF_Product_Perishble]  DEFAULT ((0)),
	[ExpiresOn] [date] NULL,
	[OnHandQty] [int] NULL,
	[IsDeleted] [bit] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


