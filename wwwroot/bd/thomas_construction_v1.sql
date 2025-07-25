USE [thomas_construction]
GO

/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 7/21/2025 1:24:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 7/21/2025 1:24:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 7/21/2025 1:24:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 7/21/2025 1:24:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 7/21/2025 1:24:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 7/21/2025 1:24:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 7/21/2025 1:24:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Value] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[bill]    Script Date: 7/21/2025 1:24:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bill](
	[id_bill] [int] IDENTITY(1,1) NOT NULL,
	[bill_name] [varchar](50) NOT NULL,
	[details] [text] NULL,
	[active] [bit] NOT NULL,
 CONSTRAINT [PK_bill] PRIMARY KEY CLUSTERED 
(
	[id_bill] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[change_order]    Script Date: 7/21/2025 1:24:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[change_order](
	[id_change] [int] IDENTITY(1,1) NOT NULL,
	[change_date] [date] NULL,
	[amount] [float] NOT NULL,
	[details] [varchar](100) NULL,
	[id_project] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[customer]    Script Date: 7/21/2025 1:24:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[customer](
	[id_customer] [int] IDENTITY(1,1) NOT NULL,
	[customer_name] [varchar](100) NOT NULL,
	[address] [text] NULL,
	[id_state] [int] NULL,
	[city] [varchar](50) NULL,
	[phone] [varchar](50) NULL,
	[zip_code] [varchar](50) NULL,
 CONSTRAINT [PK_customer] PRIMARY KEY CLUSTERED 
(
	[id_customer] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[payment]    Script Date: 7/21/2025 1:24:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[payment](
	[id_payment] [int] IDENTITY(1,1) NOT NULL,
	[payment_date] [date] NULL,
	[amount] [float] NOT NULL,
	[details] [text] NULL,
	[id_project] [int] NOT NULL,
 CONSTRAINT [PK_payment] PRIMARY KEY CLUSTERED 
(
	[id_payment] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[project]    Script Date: 7/21/2025 1:24:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[project](
	[id_project] [int] IDENTITY(1,1) NOT NULL,
	[project_name] [varchar](50) NOT NULL,
	[id_customer] [int] NOT NULL,
	[project_date] [date] NULL,
	[budget] [float] NULL,
	[cost] [float] NULL,
	[total_budget] [float] NULL,
	[downpayment] [float] NULL,
	[profit]  AS ([total_budget]-[cost]),
 CONSTRAINT [PK_project] PRIMARY KEY CLUSTERED 
(
	[id_project] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[project_bill]    Script Date: 7/21/2025 1:24:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[project_bill](
	[id_project_bill] [int] IDENTITY(1,1) NOT NULL,
	[id_bill] [int] NOT NULL,
	[id_project] [int] NOT NULL,
	[amount] [float] NOT NULL,
	[details] [text] NULL,
	[bill_date] [date] NULL,
 CONSTRAINT [PK_project_bill] PRIMARY KEY CLUSTERED 
(
	[id_project_bill] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[receipt]    Script Date: 7/21/2025 1:24:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[receipt](
	[id_receipt] [int] IDENTITY(1,1) NOT NULL,
	[amount] [float] NOT NULL,
	[details] [text] NULL,
	[receipt_date] [date] NULL,
	[id_project] [int] NOT NULL,
 CONSTRAINT [PK_receipt] PRIMARY KEY CLUSTERED 
(
	[id_receipt] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[state]    Script Date: 7/21/2025 1:24:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[state](
	[id_state] [int] IDENTITY(1,1) NOT NULL,
	[state_name] [varchar](50) NOT NULL,
	[state_code] [varchar](50) NOT NULL,
	[active] [bit] NOT NULL,
 CONSTRAINT [PK_state] PRIMARY KEY CLUSTERED 
(
	[id_state] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[supplie]    Script Date: 7/21/2025 1:24:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[supplie](
	[id_supplie] [int] IDENTITY(1,1) NOT NULL,
	[details] [varchar](100) NOT NULL,
	[amount] [int] NOT NULL,
	[price] [float] NOT NULL,
	[id_project] [int] NULL,
	[date_supplie] [date] NULL,
	[total_price]  AS ([amount]*([price]+[price]*(0.0825))),
	[price_tax]  AS ([price]+[price]*(0.0825)),
 CONSTRAINT [PK_supplie] PRIMARY KEY CLUSTERED 
(
	[id_supplie] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[worker]    Script Date: 7/21/2025 1:24:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[worker](
	[id_worker] [int] IDENTITY(1,1) NOT NULL,
	[worker_name] [varchar](50) NOT NULL,
	[active] [bit] NOT NULL,
 CONSTRAINT [PK_worker] PRIMARY KEY CLUSTERED 
(
	[id_worker] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[worker_salary]    Script Date: 7/21/2025 1:24:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[worker_salary](
	[id_salary] [int] IDENTITY(1,1) NOT NULL,
	[id_worker] [int] NOT NULL,
	[id_project] [int] NOT NULL,
	[price_hour] [float] NOT NULL,
	[work_hours] [float] NOT NULL,
	[salary]  AS ([price_hour]*[work_hours]),
	[salary_date] [date] NULL,
 CONSTRAINT [PK_worker_salary] PRIMARY KEY CLUSTERED 
(
	[id_salary] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'7497963E-168F-4EF4-A2A0-5CC3F07F2D51', N'Admin', N'ADMIN', N'CDF56B0D-E112-46E2-A13F-548FB0AF9B38')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'f81be1d3-5cc9-4c77-b938-009472aca44b', N'7497963E-168F-4EF4-A2A0-5CC3F07F2D51')
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'90abe6df-ea2c-473a-b27a-ba6fe05e1a58', N'thomasc', N'THOMASC', NULL, NULL, 1, N'AQAAAAIAAYagAAAAENR9QI5oVOm6ENvDG6FLpCIPH2Frv3EiLN1KbkpCgXfStnxcUuwNzisNNc85ibxF0w==', N'OP3ZOQMVR4ROAFM2BJ64PRPDWN6MJSJ5', N'a42d0f8a-fc4b-4de5-bcfd-46f5333243e9', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'f81be1d3-5cc9-4c77-b938-009472aca44b', N'useradmin', N'USERADMIN', NULL, NULL, 1, N'AQAAAAIAAYagAAAAEH5xi8jUB7Z8fYh2LXrwt5sjMAuRQz1I+dpgevp6zT5aH6DiVAA/jLqRrjWnlwni7A==', N'OPL35UTDR3AXRNDNMQVE67JNJ7PULQSB', N'63642d58-fcc4-4708-be47-0072be8eb4de', NULL, 0, 0, NULL, 1, 0)
GO
SET IDENTITY_INSERT [dbo].[state] ON 

INSERT [dbo].[state] ([id_state], [state_name], [state_code], [active]) VALUES (1, N'Alabama', N'AL', 0)
INSERT [dbo].[state] ([id_state], [state_name], [state_code], [active]) VALUES (2, N'Alaska', N'AK', 0)
INSERT [dbo].[state] ([id_state], [state_name], [state_code], [active]) VALUES (3, N'Arizona', N'AZ', 0)
INSERT [dbo].[state] ([id_state], [state_name], [state_code], [active]) VALUES (4, N'Arkansas', N'AR', 0)
INSERT [dbo].[state] ([id_state], [state_name], [state_code], [active]) VALUES (5, N'California', N'CA', 0)
INSERT [dbo].[state] ([id_state], [state_name], [state_code], [active]) VALUES (6, N'Carolina del Norte', N'NC', 0)
INSERT [dbo].[state] ([id_state], [state_name], [state_code], [active]) VALUES (7, N'Carolina del Sur', N'SC', 0)
INSERT [dbo].[state] ([id_state], [state_name], [state_code], [active]) VALUES (8, N'Colorado', N'CO', 0)
INSERT [dbo].[state] ([id_state], [state_name], [state_code], [active]) VALUES (9, N'Connecticut', N'CT', 0)
INSERT [dbo].[state] ([id_state], [state_name], [state_code], [active]) VALUES (10, N'Dakota del Norte', N'ND', 0)
INSERT [dbo].[state] ([id_state], [state_name], [state_code], [active]) VALUES (11, N'Dakota del Sur', N'SD', 0)
INSERT [dbo].[state] ([id_state], [state_name], [state_code], [active]) VALUES (12, N'Delaware', N'DE', 0)
INSERT [dbo].[state] ([id_state], [state_name], [state_code], [active]) VALUES (13, N'Distrito de Columbia', N'DC', 0)
INSERT [dbo].[state] ([id_state], [state_name], [state_code], [active]) VALUES (14, N'Florida', N'FL', 0)
INSERT [dbo].[state] ([id_state], [state_name], [state_code], [active]) VALUES (15, N'Georgia', N'GA', 0)
INSERT [dbo].[state] ([id_state], [state_name], [state_code], [active]) VALUES (16, N'Hawai', N'HI', 0)
INSERT [dbo].[state] ([id_state], [state_name], [state_code], [active]) VALUES (17, N'Idaho', N'ID', 0)
INSERT [dbo].[state] ([id_state], [state_name], [state_code], [active]) VALUES (18, N'Illinois', N'IL', 0)
INSERT [dbo].[state] ([id_state], [state_name], [state_code], [active]) VALUES (19, N'Indiana', N'IN', 0)
INSERT [dbo].[state] ([id_state], [state_name], [state_code], [active]) VALUES (20, N'Iowa', N'IA', 0)
INSERT [dbo].[state] ([id_state], [state_name], [state_code], [active]) VALUES (21, N'Kansas', N'KS', 0)
INSERT [dbo].[state] ([id_state], [state_name], [state_code], [active]) VALUES (22, N'Kentucky', N'KY', 0)
INSERT [dbo].[state] ([id_state], [state_name], [state_code], [active]) VALUES (23, N'Luisiana', N'LA', 0)
INSERT [dbo].[state] ([id_state], [state_name], [state_code], [active]) VALUES (24, N'Maine', N'ME', 0)
INSERT [dbo].[state] ([id_state], [state_name], [state_code], [active]) VALUES (25, N'Maryland', N'MD', 0)
INSERT [dbo].[state] ([id_state], [state_name], [state_code], [active]) VALUES (26, N'Massachusetts', N'MA', 0)
INSERT [dbo].[state] ([id_state], [state_name], [state_code], [active]) VALUES (27, N'Michigan', N'MI', 0)
INSERT [dbo].[state] ([id_state], [state_name], [state_code], [active]) VALUES (28, N'Minnesota', N'MN', 0)
INSERT [dbo].[state] ([id_state], [state_name], [state_code], [active]) VALUES (29, N'Mississippi', N'MS', 0)
INSERT [dbo].[state] ([id_state], [state_name], [state_code], [active]) VALUES (30, N'Missouri', N'MO', 0)
INSERT [dbo].[state] ([id_state], [state_name], [state_code], [active]) VALUES (31, N'Montana', N'MT', 0)
INSERT [dbo].[state] ([id_state], [state_name], [state_code], [active]) VALUES (32, N'Nebraska', N'NE', 0)
INSERT [dbo].[state] ([id_state], [state_name], [state_code], [active]) VALUES (33, N'Nevada', N'NV', 0)
INSERT [dbo].[state] ([id_state], [state_name], [state_code], [active]) VALUES (34, N'Nueva Hampshire', N'NH', 0)
INSERT [dbo].[state] ([id_state], [state_name], [state_code], [active]) VALUES (35, N'Nueva Jersey', N'NJ', 0)
INSERT [dbo].[state] ([id_state], [state_name], [state_code], [active]) VALUES (36, N'Nueva York', N'NY', 0)
INSERT [dbo].[state] ([id_state], [state_name], [state_code], [active]) VALUES (37, N'Nuevo México', N'NE', 0)
INSERT [dbo].[state] ([id_state], [state_name], [state_code], [active]) VALUES (38, N'Ohio', N'OH', 0)
INSERT [dbo].[state] ([id_state], [state_name], [state_code], [active]) VALUES (39, N'Oklahoma', N'OK', 0)
INSERT [dbo].[state] ([id_state], [state_name], [state_code], [active]) VALUES (40, N'Oregón', N'OR', 0)
INSERT [dbo].[state] ([id_state], [state_name], [state_code], [active]) VALUES (41, N'Pensilvania', N'PA', 0)
INSERT [dbo].[state] ([id_state], [state_name], [state_code], [active]) VALUES (42, N'Rhode Island', N'RI', 0)
INSERT [dbo].[state] ([id_state], [state_name], [state_code], [active]) VALUES (43, N'Tennessee', N'TN', 0)
INSERT [dbo].[state] ([id_state], [state_name], [state_code], [active]) VALUES (44, N'Texas', N'TX', 1)
INSERT [dbo].[state] ([id_state], [state_name], [state_code], [active]) VALUES (45, N'Utah', N'UT', 0)
INSERT [dbo].[state] ([id_state], [state_name], [state_code], [active]) VALUES (46, N'Vermont', N'VT', 0)
INSERT [dbo].[state] ([id_state], [state_name], [state_code], [active]) VALUES (47, N'Virginia', N'VA', 0)
INSERT [dbo].[state] ([id_state], [state_name], [state_code], [active]) VALUES (48, N'Virginia Occidental', N'WV', 0)
INSERT [dbo].[state] ([id_state], [state_name], [state_code], [active]) VALUES (49, N'Washington', N'WA', 0)
INSERT [dbo].[state] ([id_state], [state_name], [state_code], [active]) VALUES (50, N'Wisconsin', N'WI', 0)
INSERT [dbo].[state] ([id_state], [state_name], [state_code], [active]) VALUES (51, N'Wyoming', N'WY', 0)
SET IDENTITY_INSERT [dbo].[state] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoles_NormalizedName]    Script Date: 7/21/2025 1:24:16 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoles_NormalizedName] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUsers_NormalizedEmail]    Script Date: 7/21/2025 1:24:16 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUsers_NormalizedEmail] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)
WHERE ([NormalizedEmail] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUsers_NormalizedUserName]    Script Date: 7/21/2025 1:24:16 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUsers_NormalizedUserName] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[bill] ADD  CONSTRAINT [DF_bill_active]  DEFAULT ((1)) FOR [active]
GO
ALTER TABLE [dbo].[project] ADD  CONSTRAINT [DF_project_cost]  DEFAULT ((0)) FOR [cost]
GO
ALTER TABLE [dbo].[project] ADD  CONSTRAINT [DF_project_downpayment]  DEFAULT ((0)) FOR [downpayment]
GO
ALTER TABLE [dbo].[state] ADD  CONSTRAINT [DF_state_active]  DEFAULT ((1)) FOR [active]
GO
ALTER TABLE [dbo].[worker] ADD  CONSTRAINT [DF_worker_active]  DEFAULT ((1)) FOR [active]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[change_order]  WITH CHECK ADD  CONSTRAINT [FK_change_order_project] FOREIGN KEY([id_project])
REFERENCES [dbo].[project] ([id_project])
GO
ALTER TABLE [dbo].[change_order] CHECK CONSTRAINT [FK_change_order_project]
GO
ALTER TABLE [dbo].[customer]  WITH CHECK ADD  CONSTRAINT [FK_state_customer] FOREIGN KEY([id_state])
REFERENCES [dbo].[state] ([id_state])
GO
ALTER TABLE [dbo].[customer] CHECK CONSTRAINT [FK_state_customer]
GO
ALTER TABLE [dbo].[payment]  WITH CHECK ADD  CONSTRAINT [FK_payment_project] FOREIGN KEY([id_project])
REFERENCES [dbo].[project] ([id_project])
GO
ALTER TABLE [dbo].[payment] CHECK CONSTRAINT [FK_payment_project]
GO
ALTER TABLE [dbo].[project]  WITH CHECK ADD  CONSTRAINT [FK_project_customer] FOREIGN KEY([id_customer])
REFERENCES [dbo].[customer] ([id_customer])
GO
ALTER TABLE [dbo].[project] CHECK CONSTRAINT [FK_project_customer]
GO
ALTER TABLE [dbo].[project_bill]  WITH CHECK ADD  CONSTRAINT [FK_project_bill_project] FOREIGN KEY([id_project])
REFERENCES [dbo].[project] ([id_project])
GO
ALTER TABLE [dbo].[project_bill] CHECK CONSTRAINT [FK_project_bill_project]
GO
ALTER TABLE [dbo].[receipt]  WITH CHECK ADD  CONSTRAINT [FK_receipt_project] FOREIGN KEY([id_project])
REFERENCES [dbo].[project] ([id_project])
GO
ALTER TABLE [dbo].[receipt] CHECK CONSTRAINT [FK_receipt_project]
GO
ALTER TABLE [dbo].[supplie]  WITH CHECK ADD  CONSTRAINT [FK_supplie_project] FOREIGN KEY([id_project])
REFERENCES [dbo].[project] ([id_project])
GO
ALTER TABLE [dbo].[supplie] CHECK CONSTRAINT [FK_supplie_project]
GO
ALTER TABLE [dbo].[worker_salary]  WITH CHECK ADD  CONSTRAINT [FK_worker_salary_project] FOREIGN KEY([id_project])
REFERENCES [dbo].[project] ([id_project])
GO
ALTER TABLE [dbo].[worker_salary] CHECK CONSTRAINT [FK_worker_salary_project]
GO
ALTER TABLE [dbo].[worker_salary]  WITH CHECK ADD  CONSTRAINT [FK_worker_salary_worker] FOREIGN KEY([id_worker])
REFERENCES [dbo].[worker] ([id_worker])
GO
ALTER TABLE [dbo].[worker_salary] CHECK CONSTRAINT [FK_worker_salary_worker]
GO

